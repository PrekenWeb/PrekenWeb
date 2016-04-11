using System.Globalization;
using System.IO;
using CaptchaMvc.Attributes;
using Microsoft.Reporting.WebForms;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;
using PrekenWeb.Security;
using Prekenweb.Website.Lib.Cache;
using SharpEpub;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Prekenweb.Website.Areas.Website.Models;
using Prekenweb.Website.Lib;
using VikingErik.Mvc.ResumingActionResults;
using ContentDisposition = System.Net.Mime.ContentDisposition;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class PreekController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly ITekstRepository _tekstRepository;
        private readonly IPrekenwebCache _cache;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public PreekController(IPrekenwebContext<Gebruiker> context,
                               ITekstRepository tekstRepository,
                               IPrekenwebCache cache,
                               IHuidigeGebruiker huidigeGebruiker,
                               IPrekenWebUserManager prekenWebUserManager)
        {
            _context = context;
            _tekstRepository = tekstRepository;
            _cache = cache;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
        }

        public async Task<ActionResult> Open(int id)
        {
            var viewModel = new PreekOpen();

            viewModel.Preek = _context
                .Preeks
                .Include(x => x.PreekType)
                .Include(x => x.Predikant)
                .Include(x => x.Serie)
                .Include(x => x.Gebeurtenis)
                .Include(x => x.Gemeente)
                .Include(x => x.PreekLezenEnZingens)
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .SingleOrDefault(p => p.Id == id && p.Gepubliceerd);

            if (viewModel.Preek == null) return HttpNotFound("Preek bestaat niet (meer)");

            if (viewModel.Preek.TaalId != TaalInfoHelper.FromRouteData(RouteData).Id)
            {
                var taal = "nl";
                if (viewModel.Preek.TaalId == 2) taal = "en";
                return RedirectToActionPermanent("Open", new { Id = id, Culture = taal });
            }

            viewModel.Preek.AantalKeerGedownload++;
            _context.SaveChanges();

            viewModel.Titel = viewModel.Preek.GetPreekTitel();

            DateTime? laatsteBezoek;
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            viewModel.Cookie =   EnsureCookie(id, gebruikerId, out laatsteBezoek);
            viewModel.LaatsteBezoek = laatsteBezoek;

            ViewBag.TaalKeuze = false;

            return View(viewModel);
        }

        private PreekCookie EnsureCookie(int preekId, int gebruikerId, out DateTime? laatsteBezoek)
        {
            laatsteBezoek = new DateTime?();

            if (!Request.IsAuthenticated) return new PreekCookie();

            var preekCookie = _context.PreekCookies.SingleOrDefault(pc => pc.PreekId == preekId && pc.GebruikerId == gebruikerId);
            if (preekCookie == null)
            {
                preekCookie = new PreekCookie
                {
                    DateTime = DateTime.Now,
                    Opmerking = string.Empty,
                    PreekId = preekId,
                    GebruikerId = gebruikerId
                };
                _context.PreekCookies.Add(preekCookie);
                _context.SaveChanges();

                _cache.RemoveCached("NieuwePreken");
            }
            else
            {
                laatsteBezoek = preekCookie.DateTime;

                preekCookie.DateTime = DateTime.Now;
                _context.SaveChanges();
            }
            return preekCookie;
        }


        public ActionResult ZoekPredikant(string zoekterm)
        {
            var resultaten = _context.Predikants.Where(p => p.Achternaam.Contains(zoekterm)).Select(p => new { id = p.Id, label = p.Achternaam }).ToList();
            return Json(resultaten, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult Download(int id, bool? inline)
        //{
        //    if (!inline.HasValue) inline = false;
        //    Preek preek = new Preek();
        //    using (PrekenwebContext context = new PrekenwebContext())
        //    {
        //        preek = _context.Preeks.AsNoTracking().Single(p => p.Id == id);
        //    }
        //    _context.Dispose();
        //    //Response.AppendHeader("Content-Disposition", new ContentDisposition { FileName = preek.Bestandsnaam, Inline = inline.Value }.ToString());

        //    return new ResumingFilePathResult(Server.MapPath("~/Content/Preken/" + preek.Bestandsnaam), preek.GetContentType());

        //    //return new RangeFilePathResult(, , DateTime.Now, 10000);
        //    //if (inline.Value || Request.Headers["Range"] != null)
        //    //    return File("~/Content/Preken/" + preek.Bestandsnaam, preek.GetContentType());
        //    //else
        //    //    return new MonitoredPreekFileResult(Server.MapPath("~/Content/Preken/" + preek.Bestandsnaam), preek.GetContentType(), id);
        //}

        public ActionResult Download(int id, bool? inline, string format = "PDF")
        {
            if (!inline.HasValue) inline = false;
            var preek = _context
                 .Preeks
                 .Include(x => x.PreekType)
                 .Include(x => x.Predikant)
                 .Include(x => x.Serie)
                 .Include(x => x.Gebeurtenis)
                 .Include(x => x.Gemeente)
                 .Include(x => x.PreekLezenEnZingens)
                 .Include(x => x.BoekHoofdstuk)
                 .Include(x => x.BoekHoofdstuk.Boek)
                 .AsNoTracking()
                 .SingleOrDefault(p => p.Id == id && p.Gepubliceerd);

            if (preek == null) return HttpNotFound("Preek bestaat niet, niet meer of is nog niet gepubliceerd!");

            //Hook: Verbreek verbinding, sommige request duren in potentie erg lang vanwege de ResumingFilePathResult results, DB connectie is vanaf nu niet meer nodig
            //_context.Database.Connection.Close();
            _context.Dispose();

            if (preek.PreekTypeId == (int)PreekTypeEnum.LeesPreek && (string.IsNullOrEmpty(preek.Bestandsnaam) || format == "EPUB"))
            {
                var filename = preek.GetPreekTitel();
                foreach (var c in Path.GetInvalidFileNameChars())
                {
                    filename = filename.Replace(c.ToString(CultureInfo.InvariantCulture), "");
                }
                switch (format)
                {
                    case "PDF":
                        {
                            Response.AppendHeader("Content-Disposition", new ContentDisposition { FileName = filename + ".pdf", Inline = inline.Value }.ToString());

                            return File(getGeneratedLeespreek(preek, "PDF"), "application/pdf");
                        }
                    case "Word": return File(getGeneratedLeespreek(preek, "Word"), "application/msword", filename + ".doc");
                    case "HTML": return Content(preek.LeesPreekTekst);
                    case "EPUB": return GenerateEpub(preek, filename);

                    default: throw new Exception("Huh?");
                }
            }
            if (inline.Value)
            {
                return new ResumingFilePathResult(Server.MapPath("~/Content/Preken/" + preek.Bestandsnaam), preek.GetContentType());
            }

            Response.AppendHeader("Content-Disposition", new ContentDisposition { FileName = preek.Bestandsnaam, Inline = false }.ToString());
            return File("~/Content/Preken/" + preek.Bestandsnaam, preek.GetContentType());

        }

        public async Task<ActionResult> LegBladwijzer(int preekId)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            var preekCookie = await _context.PreekCookies.SingleOrDefaultAsync(x => x.PreekId == preekId && x.GebruikerId == gebruikerId);
            if (preekCookie == null)
            {
                preekCookie = new PreekCookie
                {
                    GebruikerId = gebruikerId,
                    PreekId = preekId
                };
                _context.PreekCookies.Add(preekCookie);
                await _context.SaveChangesAsync();
            }
            preekCookie.BladwijzerGelegdOp = DateTime.Now;
            await _context.SaveChangesAsync();
            if (Request.IsAjaxRequest())
            {
                return null;
            }
            return RedirectToAction("Open", new { Id = preekId });
        }

        public async Task<ActionResult> VerwijderBladwijzer(int preekId)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            var preekCookie = await _context.PreekCookies.SingleOrDefaultAsync(x => x.PreekId == preekId && x.GebruikerId == gebruikerId);
            if (preekCookie == null) return null;

            preekCookie.BladwijzerGelegdOp = null;
            await _context.SaveChangesAsync();

            if (Request.IsAjaxRequest())
            {
                return null;
            }
            return RedirectToAction("Open", new { Id = preekId });
        }

        private ActionResult GenerateEpub(Preek preek, string filename)
        {
            var epub = new EpubOnFly();
            epub.Metadata.Creator = "PrekenWeb.nl";
            //epub.Metadata.Title = "asd";
            epub.Metadata.Title = WebUtility.HtmlEncode(preek.GetPreekTitel());
            //epub.Structure.Directories.ImageFolder = "Image";
            //epub.AddImage("img1.jpg", bytes);
            epub.AddCss("style.css", @"
                            .LeespreekAfdruk {
                                padding-right: 20px;
                            }

                                .LeespreekAfdruk .Thema {
                                    font-size: 20pt;
                                    text-align: center;
                                    font-weight: bold;
                                    padding-bottom: 10px;
                                }

                                .LeespreekAfdruk .Titel {
                                    text-align: center;
                                }

                                .LeespreekAfdruk .Subtitel {
                                    text-align: center;
                                }

                                .LeespreekAfdruk .HeleLeespreek {
                                    border: 0px;
                                    padding: 0px;
                                } 

                                .HeleLeespreek {
                                    border-radius: 3px;
                                    border: 1px solid #808080;
                                    background-color: white;
                                    font-family: Calibri, Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
                                    font-size: 12.5pt;
                                    margin-bottom: 20px;
                                    float: left;
                                    padding: 20px 20px 20px 20px;
                                    /*max-height:600px;*/
                                    overflow: auto;
                                }

                                    .HeleLeespreek .HeleLeespreekTable {
                                        margin-bottom: 20px;
                                        width: 100%;
                                    }

                                        .HeleLeespreek .HeleLeespreekTable .ColSoort {
                                            width: 100px;
                                            text-align: right;
                                        }

                                        .HeleLeespreek .HeleLeespreekTable .ColOmschrijving {
                                            width: 150px;
                                        }

                                    .HeleLeespreek h1, .HeleLeespreek h2, .HeleLeespreek h3 {
                                        font-size: 22px;
                                        margin: 10px 0px 0px 0px;
                                        padding: 0px;
                                    }

                                    .HeleLeespreek p {
                                        margin-top: 0px;
                                        margin-bottom: 0px;
                                        font-size: 12.5pt;
                                        font-family: Calibri, Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
                                        text-align: justify;
                                    }
                        ");
            var lezenEnZingen = string.Empty;
            foreach (var plz in preek.PreekLezenEnZingens.OrderBy(x => x.Sortering))
            {
                lezenEnZingen += @"
                            <tr>
                                <td></td>
                                <td class='ColSoort'>" + WebUtility.HtmlEncode(plz.Soort) + @" :</td>
                                <td class='ColOmschrijving'>" + WebUtility.HtmlEncode(plz.Omschrijving) + @"</td>
                            </tr>";
            }

            epub.AddContent("leespreektekst.html", @"
                             <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.1//EN' 'http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd'>
                                <html lang='nl' xmlns='http://www.w3.org/1999/xhtml'>
                                <head>
                                    <meta http-equiv='content-type' content='text/html; charset=iso-8859-15'></meta>
                                    <link rel='stylesheet' type='text/css' href='Css/style.css'></link>
                                </head>
                                <body> 
                                    <div class='LeespreekAfdruk'>
                                        <div class='HeleLeespreek'>
                                            <div class='Thema'>" + WebUtility.HtmlEncode(preek.ThemaOmschrijving) + @"</div>
                                            <div class='Titel'>" + WebUtility.HtmlEncode(preek.GetPreekTitel()) + @"</div>
                                            <div class='Subtitel'>" + string.Format("{0}", preek.GebeurtenisId.HasValue ? '(' + WebUtility.HtmlEncode(preek.Gebeurtenis.Omschrijving) + ')' : string.Empty) + @"</div>
                                            <table class='HeleLeespreekTable'>" + lezenEnZingen + @"</table>
                                            " + preek.LeesPreekTekst + @"
                                        </div> 
                                    </div> 
                                </body>
                                </html>");

            return File(epub.BuildToBytes(), "application/epub+zip ", filename + ".epub");
        }

        // Wordt vooralsnog niet meer gebruikt
        private byte[] getGeneratedLeespreek(Preek preek, string format)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType;
            string encoding;
            string extension;

            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Content/Leespreek.rdlc";
            viewer.LocalReport.SetParameters(new List<ReportParameter>
            {
                new ReportParameter("Thema",preek.ThemaOmschrijving),
                new ReportParameter("Subtitel", preek.GebeurtenisId.HasValue ? string.Format("({0})", preek.Gebeurtenis.Omschrijving) : string.Empty),
                new ReportParameter("PreekId",preek.Id.ToString(CultureInfo.InvariantCulture)),
                new ReportParameter("Titel",preek.GetPreekTitel()) 
            });

            viewer.LocalReport.DataSources.Add(new ReportDataSource("Preek", new List<Preek> { preek }));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("LezenEnZingen", preek.PreekLezenEnZingens));

            return viewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        }

        public async Task<ActionResult> GegevensAanvullen(int preekId)
        {
            var preek = await _context.Preeks.SingleOrDefaultAsync(p => p.Id == preekId);
            if (preek == null) return HttpNotFound();

            return View(new GegevensAanvullen
            {
                TekstPagina = _tekstRepository.GetTekstPagina("gegevens-aanvullen", TaalInfoHelper.FromRouteData(RouteData).Id),
                PreekId = preekId,
                Preek = preek,
                Verzonden = false
            });
        }

        [HttpPost, CaptchaVerify("Captcha is not valid")]
        public ActionResult GegevensAanvullen(GegevensAanvullen viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("gegevens-aanvullen", TaalInfoHelper.FromRouteData(RouteData).Id);
            viewModel.TekstPaginaCompleet = _tekstRepository.GetTekstPagina("gegevens-aanvullen-compleet", TaalInfoHelper.FromRouteData(RouteData).Id);

            if (ModelState.IsValid)
            {
                var inbox = new Inbox
                {
                    Afgehandeld = false,
                    VanNaam = viewModel.Naam,
                    VanEmail = viewModel.Email,
                    Inhoud = viewModel.Aanvulling.Replace(Environment.NewLine, "<br/>"),
                    Omschrijving = viewModel.Onderwerp,
                    InboxTypeId = _context.InboxTypes.Single(it => it.Omschrijving == "Aanvulling op preek").Id,
                    Aangemaakt = DateTime.Now,
                    PreekId = viewModel.PreekId
                };
                _context.Inboxes.Add(inbox);
                _context.SaveChanges();
                viewModel.Verzonden = true;
            }

            viewModel.Preek = _context.Preeks.Single(p => p.Id == viewModel.PreekId);
            return View(viewModel);
        }

        public ActionResult Bijbelgedeelte(int? versVanId, int? versTotid)
        {
            var teksten = new List<BoekHoofdstukTekst>();

            var beginTekst = _context.BoekHoofdstukTeksts.Include(x => x.BoekHoofdstuk).Include(x => x.BoekHoofdstuk.Boek).SingleOrDefault(bt => bt.Id == versVanId);
            var eindTekst = _context.BoekHoofdstukTeksts.Include(x => x.BoekHoofdstuk).Include(x => x.BoekHoofdstuk.Boek).SingleOrDefault(bt => bt.Id == versTotid);

            if (beginTekst != null && eindTekst != null)
            {
                teksten.AddRange(
                        _context
                        .BoekHoofdstukTeksts
                        .Include(x => x.BoekHoofdstuk)
                        .Include(x => x.BoekHoofdstuk.Boek)
                        .OrderBy(bt => bt.Sortering)
                        .Where(bt =>
                            bt.Sortering >= beginTekst.Sortering
                            && bt.Sortering <= eindTekst.Sortering
                            && (bt.BoekHoofdstuk.BoekId == beginTekst.BoekHoofdstuk.BoekId || bt.BoekHoofdstuk.BoekId == eindTekst.BoekHoofdstuk.BoekId)
                        )/*.Take(200)*/);
            }
            else if (beginTekst != null) teksten.Add(beginTekst);
            else if (eindTekst != null) teksten.Add(eindTekst);

            return PartialView(new BijbelGedeelteViewModel
            {
                Teksten = teksten
            });
        }

        [Authorize]
        public async Task<object> UpdateTimePlayed(double timePlayed, int cookieId)
        {
            var cookie = _context.PreekCookies.SingleOrDefault(x => x.Id == cookieId);
            if (cookie == null || cookie.GebruikerId != await _huidigeGebruiker.GetId(_prekenWebUserManager, User))
            {
                throw new HttpException("This file or cookie does not exist!");
            }
            var seconds = Convert.ToInt64(timePlayed * 10000000);
            if (seconds == 0) return null;

            cookie.AfgespeeldTot = new TimeSpan(seconds);
            _context.SaveChanges();

            return Json(bool.TrueString, JsonRequestBehavior.AllowGet);
        }
    }
}
