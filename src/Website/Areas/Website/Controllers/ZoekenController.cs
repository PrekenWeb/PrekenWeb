using AutoMapper;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Models.Repository;
using Prekenweb.Models.Services;
using PrekenWeb.Security;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.Lib.Identity;
using Prekenweb.Website.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prekenweb.Website.Areas.Website.Controllers
{ 
   [OutputCache(Duration = 3600, VaryByParam = "*", VaryByCustom="user")] // 1 uur
    public class ZoekenController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IZoekenRepository _zoekenRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
       private readonly IPrekenWebUserManager _prekenWebUserManager;

       public ZoekenController(IPrekenwebContext<Gebruiker> context, 
                                IZoekenRepository zoekenRepository,
                                IGebruikerRepository gebruikerRepository,
                                IHuidigeGebruiker huidigeGebruiker,
                                IPrekenWebUserManager prekenWebUserManager)
        {
            _context = context;
            _zoekenRepository = zoekenRepository;
            _gebruikerRepository = gebruikerRepository;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
            ViewBag.Taalkeuze = true;
        }

        public async Task<ActionResult> Index(PreekZoeken viewModel)
        {
            Session["ZoekParameters"] = Request.QueryString; 

            viewModel = await GetPreekZoekenViewModel(viewModel, 50);

            return View(viewModel);
        }

        [Authorize]
        public async Task<ActionResult> ZoekOpdrachtBewaren(PreekZoeken viewModel)
        { 

            viewModel = await GetPreekZoekenViewModel(viewModel, 50);
            Mapper.CreateMap<PreekZoeken, ZoekOpdracht>();
            var zoekOpdracht = Mapper.Map<PreekZoeken, ZoekOpdracht>(viewModel);
            zoekOpdracht.TaalId = TaalId;
            zoekOpdracht.GebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            _context.ZoekOpdrachten.Add(zoekOpdracht);
            await _context.SaveChangesAsync();

            if (Request.IsAjaxRequest())
            {
                return Json(zoekOpdracht.PubliekeSleutel, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index", viewModel);
        }

        public async Task<ActionResult> PartialInlineZoek(PreekZoeken viewModel)
        { 
            viewModel = await GetPreekZoekenViewModel(viewModel, 6);

            return PartialView("PartialInlineZoek", viewModel);
        }

        private async Task<PreekZoeken> GetPreekZoekenViewModel(PreekZoeken viewModel, int pagesize)
        {
            ViewBag.Taalkeuze = !(viewModel.PredikantId.HasValue || viewModel.GebeurtenisId.HasValue || viewModel.BoekId.HasValue || viewModel.BoekHoofdstukId.HasValue || viewModel.SerieId.HasValue || viewModel.GemeenteId.HasValue || viewModel.LezingCategorieId.HasValue);

            viewModel.TaalId = TaalId;
            if (viewModel.Pagina == null) viewModel.Pagina = 1;

            if (!viewModel.LeesPreken && !viewModel.AudioPreken && !viewModel.Lezingen)
            {
                // geen een preektype kiezen is allemaal krijgen! 
                viewModel.LeesPreken = true;
                viewModel.AudioPreken = true;
                viewModel.Lezingen = true;
            }

            // Tekst velden vullen als er een ID mee komt
            RouteValueDictionary redirectRouteValues;
            viewModel.VulTekstVeldenOpBasisVanIDs(_context, out redirectRouteValues);
            if (redirectRouteValues != null) Response.RedirectToRoutePermanent("MultiCultiRouteIncorrectDomain", redirectRouteValues);

            // ViewModel values kopieren naar instantie van ZoekOpdracht 
            Mapper.CreateMap<PreekZoeken, ZoekOpdracht>();
            var zoekOpdracht = Mapper.Map<PreekZoeken, ZoekOpdracht>(viewModel);
            zoekOpdracht.GebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            var zoekService = new ZoekService(_zoekenRepository, _gebruikerRepository);
            viewModel.Zoekresultaat = await zoekService.ZoekOpdrachtUitvoeren(zoekOpdracht, (viewModel.Pagina.Value * pagesize) - pagesize, pagesize, true);

            // Waardes uit de uitgevoerde zoekopdracht weer terugkopieren naar het viewmodel
            Mapper.CreateMap<ZoekOpdracht, PreekZoeken>();
            Mapper.Map(zoekOpdracht, viewModel);

            viewModel.AantalResultaten = viewModel.Zoekresultaat.AantalResultaten;
            viewModel.LezingCatorieen = (!viewModel.LeesPreken && !viewModel.AudioPreken && viewModel.Lezingen) ? await _context.LezingCategories.ToListAsync() : null;

            return viewModel;
        } 

        public ActionResult Boek()
        {
            return View(new ZoekenBoek
            {
                Hoofdstukken = _context
                    .BoekHoofdstuks
                    .Include(x => x.Boek)
                    .Where(b => b.Boek.TaalId == TaalId)
                    .ToList()
            });
        }

        public ActionResult Predikant()
        {
            return View(new ZoekenPredikant
            {
                Predikanten = _context
                    .Predikants.OrderBy(p => p.Achternaam)
                    .Where(p => p.TaalId == TaalId)
                    .ToList()
            });
        }

        public ActionResult Gelegenheid()
        {
            return View(new ZoekenGebeurtenis
            {
                Gebeurtenissen = _context
                        .Gebeurtenis
                        .OrderBy(g => g.Sortering)
                        .ThenBy(g => g.Omschrijving)
                        .Where(g => g.TaalId == TaalId)
                        .ToList()
            });
        }

        public ActionResult Series()
        {
            return View(new ZoekenSerie
            {
                Series = _context.Series.OrderBy(s => s.Omschrijving).Where(s => s.TaalId == TaalId).ToList()
            });
        }

        public ActionResult Gemeente()
        {
            return View(new ZoekenGemeente
            {
                Gemeentes = _context
                    .Gemeentes
                    .OrderBy(s => s.Omschrijving)
                    .Where(g => g.Preeks.Any(p => p.TaalId == TaalId))
                    .ToList()
            });
        }

        public JsonResult Autocomplete(string type, string term)
        {
            switch (type)
            {
                case "Predikant":
                    return Json(_context.Predikants
                    .Where(p => p.TaalId == TaalId && (((p.Titels ?? "") + " " + (p.Voorletters ?? "")).Trim() + " " + ((p.Tussenvoegsels ?? "") + " " + (p.Achternaam ?? "")).Trim()).Contains(term))
                    .Take(10)
                    .ToList()
                    .Select(p => new { p.Id, value = p.VolledigeNaam })
                    .OrderBy(p => p.value)
                    .ToArray(), JsonRequestBehavior.AllowGet);

                case "BoekHoofdstuk":
                    return Json(_context.BoekHoofdstuks.Include(x => x.Boek)
                   .Where(b => b.Omschrijving.Contains(term) && b.Boek.TaalId == TaalId)
                   .Take(10)
                   .ToList()
                   .Select(b => new { b.Id, value = b.Omschrijving })
                   .OrderBy(p => p.value)
                   .ToArray(), JsonRequestBehavior.AllowGet);

                case "Gebeurtenis":
                    return Json(_context.Gebeurtenis
                   .Where(g => g.Omschrijving.Contains(term) && g.TaalId == TaalId)
                   .Take(10)
                   .ToList()
                   .Select(g => new { g.Id, value = g.Omschrijving })
                   .OrderBy(p => p.value)
                   .ToArray(), JsonRequestBehavior.AllowGet);

                case "Gemeente":
                    return Json(_context.Gemeentes
                   .Where(g => g.Omschrijving.Contains(term))
                   .Take(10)
                   .ToList()
                   .Select(g => new { g.Id, value = g.Omschrijving })
                   .OrderBy(p => p.value)
                   .ToArray(), JsonRequestBehavior.AllowGet);

                case "Serie":
                    return Json(_context.Series
                   .Where(s => s.Omschrijving.Contains(term) && s.TaalId == TaalId)
                   .Take(10)
                   .ToList()
                   .Select(s => new { s.Id, value = s.Omschrijving })
                   .OrderBy(p => p.value)
                   .ToArray(), JsonRequestBehavior.AllowGet);

                default:
                    return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}