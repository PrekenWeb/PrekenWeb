using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Identity;
using Data.Tables;
using Data.ViewModels;
using Hangfire;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Lib;
using Prekenweb.Website.Lib.Hangfire;
using PrekenWeb.Security;
using TweetSharp;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize]
    public class PreekController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public PreekController(IPrekenwebContext<Gebruiker> context,
            IHuidigeGebruiker huidigeGebruiker,
            IPrekenWebUserManager prekenWebUserManager)
        {
            _context = context;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
        }

        #region Bewerk
        [Authorize(Roles = "PreekToevoegen")]
        public async Task<ViewResult> Bewerk(int id)
        {
            var preek = _context.Preeks.Single(p => p.Id == id);

            if (!User.IsInRole("PrekenVanAnderenBewerken") && preek.AangemaaktDoor != await _huidigeGebruiker.GetId(_prekenWebUserManager, User)) throw new UnauthorizedAccessException("Cannot edit someone else's sermon");

            //preek.Gepubliceerd = User.IsInRole("PreekFiatteren");
            preek.DatumBijgewerkt = DateTime.Now;

            return View(preek);
        }

        [HttpPost, Authorize(Roles = "PreekToevoegen"), ValidateInput(false)]
        public async Task<ActionResult> Bewerk(Preek viewModel, HttpPostedFileBase bestand, HttpPostedFileBase source)
        {
            if (viewModel.Gepubliceerd)
            {
                if (!viewModel.DatumGepubliceerd.HasValue && !User.IsInRole("PreekFiatteren"))
                {
                    ModelState.AddModelError("Gepubliceerd", @"Onvoldoende rechten");
                }
            }
            else
            {
                viewModel.DatumGepubliceerd = null;
            }

            if (!ModelState.IsValid) return View(viewModel);

            _context.Entry(viewModel).State = EntityState.Modified;
            LezenEnZingenBijwerken(viewModel);
            _context.SaveChanges();

            var preek = _context.Preeks.Include(x => x.Predikant).Single(p => p.Id == viewModel.Id);

            if (viewModel.Gepubliceerd && !viewModel.DatumGepubliceerd.HasValue)
            {
                this.TweetNieuwePreek(preek);
                viewModel.DatumGepubliceerd = DateTime.Now;
            }

            try
            {
                viewModel.Bestandsnaam = HandleUpload(bestand, viewModel.Id, ConfigurationManager.AppSettings["PrekenFolder"], viewModel.Bestandsnaam);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }

            try
            {
                if (source != null)
                    viewModel.SourceFileName = HandleUpload(source, viewModel.Id, ConfigurationManager.AppSettings["LectureSourcesFolder"], source.FileName);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }

            preek.DatumBijgewerkt = DateTime.Now;
            preek.AangepastDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            OutputCacheHelpers.ClearOutputCaches(Response, Url);
            _context.SaveChanges();

            BackgroundJob.Enqueue<AchtergrondTaken>(x => x.AnalyseerAudioTaak(preek.Id));

            if (ModelState.IsValid) return RedirectToAction("NogTePubliceren", new { fromPreekId = preek.Id });

            return View(preek);
        }

        private string HandleUpload(HttpPostedFileBase uploadedPreek, int preekId, string rootFolder, string oudeBestandsnaam)
        {
            var nieuweBestandsnaam = oudeBestandsnaam;
            if (uploadedPreek == null || uploadedPreek.ContentLength <= 0) return nieuweBestandsnaam;

            nieuweBestandsnaam = $"{Path.GetFileNameWithoutExtension(uploadedPreek.FileName)}_{preekId}{Path.GetExtension(uploadedPreek.FileName)}";

            if (nieuweBestandsnaam == oudeBestandsnaam || System.IO.File.Exists($"{rootFolder}{nieuweBestandsnaam}"))
                nieuweBestandsnaam = $"{Path.GetFileNameWithoutExtension(uploadedPreek.FileName)}_{preekId}_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}{Path.GetExtension(uploadedPreek.FileName)}";

            try
            {
                uploadedPreek.SaveAs(Server.MapPath($"{rootFolder}{nieuweBestandsnaam}"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save sermon: {ex.Message}");
            }

            try
            {
                if (!string.IsNullOrEmpty(oudeBestandsnaam))
                    System.IO.File.Delete(Server.MapPath($"{rootFolder}{oudeBestandsnaam}"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting existing file, it was probably already deleted. New file uploaded successfully: {ex.Message}");
            }
            return nieuweBestandsnaam;
        }

        #endregion

        #region Maak
        [Authorize(Roles = "PreekToevoegen")]
        public ActionResult Maak()
        {
            return View(new Preek
            {
                DatumAangemaakt = DateTime.Now,
                DatumBijgewerkt = DateTime.Now,
                Gepubliceerd = User.IsInRole("PreekFiatteren"),
                PreekTypeId = (int)PreekTypeEnum.Preek,
                TaalId = TaalInfoHelper.FromRouteData(RouteData).Id,
                PreekLezenEnZingens = new List<PreekLezenEnZingen>
                {
                    new PreekLezenEnZingen { Soort = Resources.Resources.Zingen, Sortering= 1 },
                    new PreekLezenEnZingen { Soort = Resources.Resources.Lezen, Sortering= 2 },
                    new PreekLezenEnZingen { Soort = Resources.Resources.Zingen, Sortering= 3 },
                    new PreekLezenEnZingen { Soort = Resources.Resources.Zingen, Sortering= 4 },
                    new PreekLezenEnZingen { Soort = Resources.Resources.Zingen, Sortering= 5 },
                }
            });
        }

        [Authorize(Roles = "PreekToevoegen"), HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Maak(Preek viewModel, HttpPostedFileBase bestand, HttpPostedFileBase source)
        {
            if (viewModel.Gepubliceerd)
            {
                if (!viewModel.DatumGepubliceerd.HasValue && !User.IsInRole("PreekFiatteren"))
                {
                    ModelState.AddModelError("Gepubliceerd", @"Onvoldoende rechten");
                }
            }
            else
            {
                viewModel.DatumGepubliceerd = null;
            }

            if (ModelState.IsValid)
            {
                _context.Preeks.Add(viewModel);
                LezenEnZingenBijwerken(viewModel);
                _context.SaveChanges();

                var preek = _context.Preeks.Include(x => x.Predikant).Single(p => p.Id == viewModel.Id);

                if (viewModel.Gepubliceerd && !viewModel.DatumGepubliceerd.HasValue)
                {
                    this.TweetNieuwePreek(preek);
                    viewModel.DatumGepubliceerd = DateTime.Now;
                }

                try
                {
                    if (bestand != null)
                        viewModel.Bestandsnaam = HandleUpload(bestand, preek.Id, ConfigurationManager.AppSettings["PrekenFolder"], bestand.FileName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(viewModel);
                }

                try
                {
                    if (source != null)
                        viewModel.SourceFileName = HandleUpload(source, preek.Id, ConfigurationManager.AppSettings["LectureSourcesFolder"], source.FileName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(viewModel);
                }

                preek.DatumBijgewerkt = DateTime.Now;
                preek.AangemaaktDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

                OutputCacheHelpers.ClearOutputCaches(Response, Url);
                _context.SaveChanges();

                BackgroundJob.Enqueue<AchtergrondTaken>(x => x.AnalyseerAudioTaak(preek.Id));

                if (ModelState.IsValid) return RedirectToAction("NogTePubliceren", new { fromPreekId = preek.Id });

                return View(preek);
            }

            if (bestand != null) ModelState.AddModelError("", @"Let op, er waren fouten, corrigeer deze maar kies ook opnieuw het bestand want deze is nog niet opgeslagen en wordt ook niet onthouden");

            return View(viewModel);
        }

        private void TweetNieuwePreek(Preek preekModel)
        {
            try
            {
                if (TaalInfoHelper.FromRouteData(RouteData).Naam != "nl")
                {
                    return;
                }

                var customerKey = ConfigurationManager.AppSettings["TwitterCustomerKey"];
                var customerSecret = ConfigurationManager.AppSettings["TwitterCustomerSecret"];
                var token = ConfigurationManager.AppSettings["TwitterToken"];
                var tokenSecret = ConfigurationManager.AppSettings["TwitterTokenSecret"];

                // Fix om de connectie met twitter via TLS1.1 of TLS1.2 te laten lopen, via SSL3 gaat het niet goed.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var service = new TwitterService(customerKey, customerSecret, token, tokenSecret);
                var url = Url.Action("Open", null, new { preekModel.Id }, Request.Url.Scheme).Replace("/Mijn", string.Empty); ;
                var preekType = (PreekTypeEnum)preekModel.PreekTypeId;
                var statusUpdate = $"Een nieuwe {preekType.ToString().ToLower()} van {preekModel.Predikant.VolledigeNaam}: {url}";
                service.SendTweet(new SendTweetOptions { Status = statusUpdate });
            }
            catch
            {
            }
        }

        private void LezenEnZingenBijwerken(Preek viewModel)
        {
            _context.PreekLezenEnZingens.Where(plz => plz.PreekId == viewModel.Id).ToList().ForEach(plz => _context.PreekLezenEnZingens.Remove(plz));
            if (viewModel.PreekTypeId != (int)PreekTypeEnum.LeesPreek) return;

            byte index = 1;
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving1"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort1"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort1"], Omschrijving = Request.Form["LezenZingenOmschrijving1"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving2"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort2"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort2"], Omschrijving = Request.Form["LezenZingenOmschrijving2"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving3"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort3"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort3"], Omschrijving = Request.Form["LezenZingenOmschrijving3"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving4"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort4"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort4"], Omschrijving = Request.Form["LezenZingenOmschrijving4"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving5"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort5"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort5"], Omschrijving = Request.Form["LezenZingenOmschrijving5"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving6"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort6"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort6"], Omschrijving = Request.Form["LezenZingenOmschrijving6"], PreekId = viewModel.Id, Sortering = index++ });
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving7"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort7"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort7"], Omschrijving = Request.Form["LezenZingenOmschrijving7"], PreekId = viewModel.Id, Sortering = index });
        }

        #endregion

        [Authorize(Roles = "PreekFiatteren,PreekToevoegen")]
        public async Task<ViewResult> NogTePubliceren(int? fromPreekId)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;
            if (User.IsInRole("PrekenVanAnderenBewerken"))
            {
                return View(new PreekNogTePublicerenViewModel
                {
                    Preken = _context
                                .Preeks
                                .Include(x => x.Predikant)
                                .Include(x => x.Gemeente)
                                .Include(x => x.BoekHoofdstuk)
                                .Where(p =>
                                    !p.Gepubliceerd
                                    && p.TaalId == taalId
                                ).ToList(),
                    FromPreekId = fromPreekId
                });
            }

            // Alleen eigen preken
            return View(new PreekNogTePublicerenViewModel
            {

                Preken = _context
                    .Preeks
                    .Include(x => x.Predikant)
                    .Include(x => x.Gemeente)
                    .Include(x => x.BoekHoofdstuk)
                    .Where(p =>
                        !p.Gepubliceerd
                        && p.TaalId == taalId
                        && p.AangemaaktDoor == gebruikerId
                    ).ToList(),
                FromPreekId = fromPreekId
            });
        }

        [Authorize(Roles = "PreekToevoegen")]
        public ActionResult Verwijder(int id)
        {
            // Remove lecture cookies.
            _context.PreekCookies.Where(pc => pc.PreekId == id).ToList().ForEach(pc => _context.PreekCookies.Remove(pc));
            _context.SaveChanges();

            // Remove inbox (replies).
            var inboxItems = _context.Inboxes.Where(i => i.PreekId == id).ToList();
            inboxItems.ForEach(i =>
            {
                _context.InboxOpvolgings
                    .Where(io => io.InboxId == i.Id).ToList()
                    .ForEach(io => _context.InboxOpvolgings.Remove(io));
                _context.Inboxes.Remove(i);
            });
            _context.SaveChanges();

            var preek = _context.Preeks.Single(p => p.Id == id);
            try
            {
                var lectureFilename = Server.MapPath($"{ConfigurationManager.AppSettings["PrekenFolder"]}{preek.Bestandsnaam}");
                if (!string.IsNullOrEmpty(preek.Bestandsnaam) && System.IO.File.Exists(lectureFilename))
                    System.IO.File.Delete(lectureFilename);
            }
            catch
            {
                // ignored
            }

            try
            {
                var lectureSourceFilename = Server.MapPath($"{ConfigurationManager.AppSettings["LectureSourcesFolder"]}{preek.SourceFileName}");
                if (!string.IsNullOrEmpty(preek.SourceFileName) && System.IO.File.Exists(lectureSourceFilename))
                    System.IO.File.Delete(lectureSourceFilename);
            }
            catch
            {
                // ignored
            }

            _context.Preeks.Remove(preek);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "PreekToevoegen")]
        public ActionResult KiesVers(KiesVers viewModel)
        {
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;
            viewModel.Teksten = _context
                .BoekHoofdstukTeksts
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Where(bh =>
                    bh.BoekHoofdstuk.Boek.TaalId == taalId
                    && (bh.Hoofdstuk == viewModel.Hoofdstuk || !viewModel.Hoofdstuk.HasValue)
                    && (bh.BoekHoofdstuk.Omschrijving.Contains(viewModel.Boek) || string.IsNullOrEmpty(viewModel.Boek))
                )
                .Take(200);
            return PartialView(viewModel);
        }
    }
}
