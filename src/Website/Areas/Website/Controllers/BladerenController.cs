
using Prekenweb.Models;
using Prekenweb.Models.ViewModels;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.Lib;
using Prekenweb.Website.Lib.Import;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    [OutputCache(Duration = 3600, VaryByParam = "*")] // 1 uur
    public class BladerenController : ApplicationController
    {
        public ActionResult Index(PreekBladeren viewModel)
        {
            if (viewModel.PredikantId.HasValue) viewModel.Predikant = _context.Predikant.Single(p => p.Id == viewModel.PredikantId.Value).VolledigeNaam;
            if (viewModel.GebeurtenisId.HasValue) viewModel.Gebeurtenis = _context.Gebeurtenis.Single(p => p.Id == viewModel.GebeurtenisId.Value).Omschrijving;
            if (viewModel.BoekHoofdstukId.HasValue) viewModel.Boek = _context.BoekHoofdstuk.Single(p => p.Id == viewModel.BoekHoofdstukId.Value).Omschrijving;
            if (viewModel.SerieId.HasValue) viewModel.Serie = _context.Serie.Single(p => p.Id == viewModel.SerieId.Value).Omschrijving;
            if (viewModel.GemeenteId.HasValue) viewModel.Gemeente = _context.Gemeente.Single(p => p.Id == viewModel.GemeenteId.Value).Omschrijving;
            if (viewModel.LezingCategorieId.HasValue) viewModel.LezingCategorie = _context.LezingCategorie.Single(lc => lc.Id == viewModel.LezingCategorieId.Value).Omschrijving;
            //if (viewModel.PredikantId.HasValue) viewModel.Predikant = _context.Predikant.Single(p => p.Id == viewModel.PredikantId.Value).VolledigeNaam;
            //if (viewModel.PredikantId.HasValue) viewModel.Predikant = _context.Predikant.Single(p => p.Id == viewModel.PredikantId.Value).VolledigeNaam;

            var query = _context
                .Preek
                .Where(p =>
                    ((p.Predikant.Titels + " " + p.Predikant.Voorletters + " " + p.Predikant.Achternaam).Contains(viewModel.Predikant) || string.IsNullOrEmpty(viewModel.Predikant))
                    && (!viewModel.PreekTypeId.HasValue || (viewModel.PreekTypeId.HasValue && p.PreekTypeId == viewModel.PreekTypeId.Value))
                    && (!viewModel.TaalId.HasValue || (viewModel.TaalId.HasValue && p.TaalId == viewModel.TaalId.Value))
                    && (p.BoekHoofdstuk.Boek.Boeknaam.Contains(viewModel.Boek) || p.BoekHoofdstuk.Omschrijving.Contains(viewModel.Boek) || string.IsNullOrEmpty(viewModel.Boek))
                    && (p.Gebeurtenis.Omschrijving.Contains(viewModel.Gebeurtenis) || string.IsNullOrEmpty(viewModel.Gebeurtenis))
                    && (p.Gemeente.Omschrijving.Contains(viewModel.Gemeente) || string.IsNullOrEmpty(viewModel.Gemeente))
                    && (p.LezingCategorie.Omschrijving.Contains(viewModel.LezingCategorie) || string.IsNullOrEmpty(viewModel.LezingCategorie))
                    && (p.Serie.Omschrijving.Contains(viewModel.Serie) || string.IsNullOrEmpty(viewModel.Serie)
                    && (!viewModel.Hoofdstuk.HasValue || (viewModel.Hoofdstuk.HasValue && p.Hoofdstuk == viewModel.Hoofdstuk))
                    && (!viewModel.Vers.HasValue || (viewModel.Vers.HasValue && p.Vers == viewModel.Vers))
                    && (p.TaalId == this.TaalId)
                    && p.Gepubliceerd
                ))
                .Select(p => new ZoekResultaatRegel()
                {
                    Preek = p,
                    ResultaatReden = ResultaatReden.Predikant
                });


            switch (viewModel.SorteerOp)
            {
                case "Predikant":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Predikant.Achternaam) : query.OrderByDescending(q => q.Preek.Predikant.Achternaam);
                    break;
                case "Boek":
                    query = (viewModel.SorteerVolgorde == "ASC")
                        ? query.OrderByDescending(q => q.Preek.BoekHoofdstuk.Boek.Sortering).ThenByDescending(q => q.Preek.BoekHoofdstuk.Sortering).ThenBy(q => q.Preek.Hoofdstuk)
                        : query.OrderBy(q => q.Preek.BoekHoofdstuk.Boek.Sortering).ThenBy(q => q.Preek.BoekHoofdstuk.Sortering).ThenByDescending(q => q.Preek.Hoofdstuk);
                    break;
                case "Hoofdstuk":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Hoofdstuk) : query.OrderByDescending(q => q.Preek.Hoofdstuk);
                    break;
                case "Vers":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Vers) : query.OrderByDescending(q => q.Preek.Vers);
                    break;
                case "Gebeurtenis":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Gebeurtenis.Omschrijving) : query.OrderByDescending(q => q.Preek.Gebeurtenis.Omschrijving);
                    break;
                case "Gemeente":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Gemeente.Omschrijving) : query.OrderByDescending(q => q.Preek.Gemeente.Omschrijving);
                    break;
                case "Serie":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.Serie.Omschrijving) : query.OrderByDescending(q => q.Preek.Serie.Omschrijving);
                    break;
                case "Datum":
                    query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.DatumAangemaakt) : query.OrderByDescending(q => q.Preek.DatumAangemaakt);
                    break;
                default:
                    if (viewModel.BoekHoofdstukId.HasValue)
                    {
                        viewModel.SorteerOp = "Hoofdstuk";
                        viewModel.SorteerVolgorde = "ASC";
                        query = query.OrderBy(zr => zr.Preek.Hoofdstuk);
                    }
                    else
                    {
                        viewModel.SorteerOp = "Datum";
                        viewModel.SorteerVolgorde = (viewModel.SorteerVolgorde == "ASC") ? "ASC" : "DESC";
                        query = (viewModel.SorteerVolgorde == "ASC") ? query.OrderBy(q => q.Preek.DatumAangemaakt) : query.OrderByDescending(q => q.Preek.DatumAangemaakt);
                    }
                    break;
            }
            if (!viewModel.Pagina.HasValue) viewModel.Pagina = 1;
            viewModel.ZoekResultaatRegels = query.Skip((viewModel.Pagina.Value * 50) - 50).Take(50).ToList();
            Session["ZoekParamters"] = Request.QueryString;
            return View(viewModel);
        }

        private List<SelectListItem> getPreekTypes()
        {
            List<SelectListItem> returnItems = new List<SelectListItem>();
            returnItems.Add(new SelectListItem() { Value = "", Text = "" });
            returnItems.AddRange(_context.PreekType.Select(pt => new SelectListItem() { Text = pt.Omschrijving, Value = SqlFunctions.StringConvert((decimal)pt.Id) }));
            return returnItems;
        }

        private List<SelectListItem> getTalen()
        {
            List<SelectListItem> returnItems = new List<SelectListItem>();
            returnItems.Add(new SelectListItem() { Value = "", Text = "" });
            returnItems.AddRange(_context.Taal.Select(t => new SelectListItem() { Text = t.Omschrijving, Value = SqlFunctions.StringConvert((decimal)t.Id) }));
            return returnItems;
        }

        public ActionResult Boek()
        {
            BladerenBoek viewModel = new BladerenBoek();
            viewModel.Hoofdstukken = _context.BoekHoofdstuk.Include("Boek").Where(b => b.Preek.Any(p => p.TaalId == TaalId)).ToList();
            return View(viewModel);
        }
        public ActionResult Predikant()
        {
            BladerenPredikant viewModel = new BladerenPredikant();
            viewModel.Predikanten = _context.Predikant.OrderBy(p => p.Achternaam).Where(p => p.Preek.Any(pr => pr.TaalId == TaalId)).ToList();
            return View(viewModel);
        }
        public ActionResult Gelegenheid()
        {
            BladerenGebeurtenis viewModel = new BladerenGebeurtenis();
            viewModel.Gebeurtenissen = _context.Gebeurtenis.OrderBy(g => g.Sortering).ThenBy(g => g.Omschrijving).Where(g => g.Preek.Any(p => p.TaalId == TaalId)).ToList();
            return View(viewModel);
        }
        public ActionResult Series()
        {
            BladerenSerie viewModel = new BladerenSerie();
            viewModel.Series = _context.Serie.OrderBy(s => s.Omschrijving).Where(s => s.Preek.Any(p => p.TaalId == TaalId)).ToList();
            return View(viewModel);
        }
        public ActionResult Gemeente()
        {
            BladerenGemeente viewModel = new BladerenGemeente();
            viewModel.Gemeentes = _context.Gemeente.OrderBy(s => s.Omschrijving).Where(g => g.Preek.Any(p => p.TaalId == TaalId)).ToList();
            return View(viewModel);
        }
    }
}
