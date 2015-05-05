using System.Configuration;
using System.Threading.Tasks;
using Hangfire;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using PrekenWeb.Security;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prekenweb.Website.Lib.Hangfire;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize]
    public class PreekController : ApplicationController
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
        public async Task<ActionResult> Bewerk(Preek viewModel, HttpPostedFileBase bestand)
        {
            if (viewModel.Gepubliceerd && !User.IsInRole("PreekFiatteren")) ModelState.AddModelError("Gepubliceerd", "Onvoldoende rechten");

            if (!ModelState.IsValid) return View(viewModel);

            _context.Entry(viewModel).State = EntityState.Modified;
            LezenEnZingenBijwerken(viewModel);
            _context.SaveChanges();

            var preek = _context.Preeks.Single(p => p.Id == viewModel.Id);

            try
            {
                viewModel.Bestandsnaam = HandleUpload(bestand,viewModel.Id, viewModel.Bestandsnaam);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
            preek.DatumBijgewerkt = DateTime.Now;
            preek.AangepastDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            ClearOutputCaches();
            _context.SaveChanges();

            BackgroundJob.Enqueue<AchtergrondTaken>(x => x.AnalyseerAudioTaak(preek.Id));

            if (ModelState.IsValid) return RedirectToAction("NogTePubliceren", new { fromPreekId = preek.Id });

            return View(preek);
        }

        private string HandleUpload(HttpPostedFileBase uploadedPreek, int preekId, string oudeBestandsnaam)
        {
            var nieuweBestandsnaam = oudeBestandsnaam;
            if (uploadedPreek == null || uploadedPreek.ContentLength <= 0) return nieuweBestandsnaam;

            var rootFolder = ConfigurationManager.AppSettings["PrekenFolder"];

            nieuweBestandsnaam =
                string.Format(
                    "{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(uploadedPreek.FileName),
                    preekId,
                    Path.GetExtension(uploadedPreek.FileName)
                    );

            if (nieuweBestandsnaam == oudeBestandsnaam || System.IO.File.Exists(string.Format("{0}{1}", rootFolder, nieuweBestandsnaam)))
                nieuweBestandsnaam = string.Format(
                    "{0}_{1}_{2:yyyy-MM-dd_hh-mm-ss}{3}",
                    Path.GetFileNameWithoutExtension(uploadedPreek.FileName),
                    preekId,
                    DateTime.Now,
                    Path.GetExtension(uploadedPreek.FileName)
                    );

            try
            {
                uploadedPreek.SaveAs(Server.MapPath(string.Format("{0}{1}", rootFolder, nieuweBestandsnaam)));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Could not save sermon: {0}", ex.Message));
            }

            try
            {
                if (!string.IsNullOrEmpty(oudeBestandsnaam))
                    System.IO.File.Delete(Server.MapPath(string.Format("{0}{1}", rootFolder, oudeBestandsnaam)));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error deleting existing file, it was probably already deleted. New file uploaded successfully: {0}", ex.Message));
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
                PreekTypeId = (int)PreekTypeEnum.Peek,
                TaalId = TaalId,
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
        public async Task<ActionResult> Maak(Preek viewModel, HttpPostedFileBase bestand)
        {
            if (viewModel.Gepubliceerd && !User.IsInRole("PreekFiatteren")) ModelState.AddModelError("Gepubliceerd", "Onvoldoende rechten");

            if (ModelState.IsValid)
            {
                _context.Preeks.Add(viewModel);
                LezenEnZingenBijwerken(viewModel);
                _context.SaveChanges();

                var preek = _context.Preeks.Single(p => p.Id == viewModel.Id);

                try
                {
                    if (bestand != null)
                        viewModel.Bestandsnaam = HandleUpload(bestand, preek.Id, bestand.FileName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(viewModel);
                }
                preek.DatumBijgewerkt = DateTime.Now;
                preek.AangemaaktDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

                ClearOutputCaches();
                _context.SaveChanges(); 

                BackgroundJob.Enqueue<AchtergrondTaken>(x => x.AnalyseerAudioTaak(preek.Id));

                if (ModelState.IsValid) return RedirectToAction("NogTePubliceren", new { fromPreekId = preek.Id });
                
                return View(preek);  
            }

            if (bestand != null) ModelState.AddModelError("", "Let op, er waren fouten, corrigeer deze maar kies ook opnieuw het bestand want deze is nog niet opgeslagen en wordt ook niet onthouden");
            
            return View(viewModel);
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
            if (!string.IsNullOrWhiteSpace(Request.Form["LezenZingenOmschrijving7"]) || !string.IsNullOrWhiteSpace(Request.Form["LezenZingenSoort7"])) _context.PreekLezenEnZingens.Add(new PreekLezenEnZingen { Soort = Request.Form["LezenZingenSoort7"], Omschrijving = Request.Form["LezenZingenOmschrijving7"], PreekId = viewModel.Id, Sortering = index++ });
        }

        #endregion

        [Authorize(Roles = "PreekFiatteren,PreekToevoegen")]
        public async Task<ViewResult> NogTePubliceren(int? fromPreekId)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

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
                                    && p.TaalId == TaalId
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
                        && p.TaalId == TaalId
                        && p.AangemaaktDoor == gebruikerId
                    ).ToList(),
                FromPreekId = fromPreekId
            });
        }

        [Authorize(Roles = "PreekToevoegen")]
        public ActionResult Verwijder(int id)
        {
            _context.PreekCookies.Where(pc => pc.PreekId == id).ToList().ForEach(pc => _context.PreekCookies.Remove(pc));
            _context.SaveChanges();
            _context.Inboxes.Where(i => i.PreekId == id).ToList().ForEach(i => _context.Inboxes.Remove(i));
            _context.SaveChanges();

            var preek = _context.Preeks.Single(p => p.Id == id);
            try
            {
                string filename = Server.MapPath(string.Format("{0}{1}", ConfigurationManager.AppSettings["PrekenFolder"], preek.Bestandsnaam));
                if (!string.IsNullOrEmpty(preek.Bestandsnaam) && System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
            }
            catch
            {
            }
            _context.Preeks.Remove(preek);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "PreekToevoegen")]
        public ActionResult KiesVers(KiesVers viewModel)
        {
            viewModel.Teksten = _context
                .BoekHoofdstukTeksts
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Where(bh =>
                    bh.BoekHoofdstuk.Boek.TaalId == TaalId
                    && (bh.Hoofdstuk == viewModel.Hoofdstuk || !viewModel.Hoofdstuk.HasValue)
                    && (bh.BoekHoofdstuk.Omschrijving.Contains(viewModel.Boek) || string.IsNullOrEmpty(viewModel.Boek))
                )
                .Take(200);
            return PartialView(viewModel);
        }
    }
}
