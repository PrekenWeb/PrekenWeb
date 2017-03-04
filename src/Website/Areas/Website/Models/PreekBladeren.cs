using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PrekenWeb.Data;
using PrekenWeb.Data.Attributes;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Services;
using PrekenWeb.Data.Tables;

namespace Prekenweb.Website.Areas.Website.Models
{
    public class PreekZoeken
    {
        public int? PredikantId { get; set; }
        [Display(Name = "Predikant", ResourceType = typeof(Resources.Resources)), Tooltip(Name = "TooltipPredikant", ResourceType = typeof(Resources.Resources))]
        public string Predikant { get; set; }

        public bool HideMinisterFromIndexingRobots { get; set; }

        public int? BoekHoofdstukId { get; set; }
        [Display(Name = "Bijbelboek", ResourceType = typeof(Resources.Resources)), Tooltip(Name = "TooltipBijbelboek", ResourceType = typeof(Resources.Resources))]
        public string BoekHoofdstuk { get; set; }

        public int? BoekId { get; set; }
        [Display(Name = "Boek", ResourceType = typeof(Resources.Resources))]
        public string Boek { get; set; }

        public int? LezingCategorieId { get; set; }
        [Display(Name = "LezingCategorie", ResourceType = typeof(Resources.Resources))]
        public string LezingCategorie { get; set; }

        [Display(Name = "Hoofdstuk", ResourceType = typeof(Resources.Resources)), Tooltip("TooltipHoofdstuk", ResourceType = typeof(Resources.Resources))]
        public int? Hoofdstuk { get; set; }

        [Display(Name = "Leespreken", ResourceType = typeof(Resources.Resources))]
        public bool LeesPreken { get; set; }

        [Display(Name = "Preken", ResourceType = typeof(Resources.Resources))]
        public bool AudioPreken { get; set; }

        [Display(Name = "Lezingen", ResourceType = typeof(Resources.Resources))]
        public bool Lezingen { get; set; }

        public bool Geavanceerd { get; set; }

        public int? GebeurtenisId { get; set; }
        [Display(Name = "Gebeurtenis", ResourceType = typeof(Resources.Resources)), Tooltip("TooltipGebeurtenis", ResourceType = typeof(Resources.Resources))]
        public string Gebeurtenis { get; set; }

        public int? GemeenteId { get; set; }
        [Display(Name = "Gemeente", ResourceType = typeof(Resources.Resources))]
        public string Gemeente { get; set; }

        public int? SerieId { get; set; }
        [Display(Name = "Serie", ResourceType = typeof(Resources.Resources))]
        public string Serie { get; set; }

        [Display(Name = "Taal", ResourceType = typeof(Resources.Resources)), UIHint("Taal")]
        public int? TaalId { get; set; }
        public IEnumerable<SelectListItem> Talen { get; set; }

        [Display(Name = "Zoekterm", ResourceType = typeof(Resources.Resources)), Tooltip("TooltipZoekterm", ResourceType = typeof(Resources.Resources))]
        public string Zoekterm { get; set; }

        public bool? Laatste { get; set; }

        [Display(Name = "SorteerOp", ResourceType = typeof(Resources.Resources))]
        public SorteerOp? SorteerOp { get; set; }
        public SorteerVolgorde SorteerVolgorde { get; set; }

        public int? Pagina { get; set; }

        public int? ZoekOpdrachtId { get; set; }

        public List<LezingCategorie> LezingCatorieen { get; set; }

        public string Title
        {
            get
            {
                if (Laatste.HasValue && Laatste.Value) return string.Concat(Resources.Resources.Laatste, " 50 ");
                if (GebeurtenisId.HasValue) { return string.Concat(Resources.Resources.PrekenVoorGebeurtenis, " ", Gebeurtenis); }
                if (SerieId.HasValue) { return string.Concat(Resources.Resources.PrekenUitDeSerie, " ", Serie); }
                if (GemeenteId.HasValue) { return string.Concat(Resources.Resources.Gemeente, " ", Gemeente); }
                if (LezingCategorieId.HasValue) { return string.Concat(Resources.Resources.LezingCategorie, " ", LezingCategorie); }
                if (BoekHoofdstukId.HasValue)
                {
                    return Hoofdstuk.HasValue 
                        ? string.Join(" ", Resources.Resources.Prekenuit, BoekHoofdstuk, Hoofdstuk.Value) 
                        : string.Concat(Resources.Resources.Prekenuit, " ", BoekHoofdstuk);
                }
                if (PredikantId.HasValue) { return string.Concat(Resources.Resources.PrekenVanPredikant, " ", Predikant); }
                if (BoekId.HasValue) { return string.Concat(Resources.Resources.PrekenInBoek, " ", Boek); }
                return Resources.Resources.PrekenZoeken;
            }
            set { }
        }

        public int AantalResultaten { get; set; }

        public Zoekresultaat Zoekresultaat { get; set; }

        public void VulTekstVeldenOpBasisVanIDs(IPrekenwebContext<Gebruiker> context, out RouteValueDictionary redirectRoute)
        {
            redirectRoute = null;

            if (PredikantId.HasValue)
            { 
                var predikant = context.Predikants.SingleOrDefault(p => p.Id == PredikantId.Value);
                if (predikant == null) throw new KeyNotFoundException("Deze predikant bestaat niet");

                if (predikant.TaalId != TaalId)
                    redirectRoute = new RouteValueDictionary(new { culture = predikant.Taal.Code.Trim(), controller = "Zoeken", action = "Index", PredikantId = PredikantId });

                HideMinisterFromIndexingRobots = predikant.HideFromIndexingRobots;
            }

            if (GebeurtenisId.HasValue)
            {

                var gebeurtenis = context.Gebeurtenis.SingleOrDefault(p => p.Id == GebeurtenisId.Value);
                if (gebeurtenis == null) throw new KeyNotFoundException("Deze gebeurtenis bestaat niet");
                else if (gebeurtenis.TaalId != TaalId)
                {
                     redirectRoute = new RouteValueDictionary(new { culture = gebeurtenis.Taal.Code.Trim(), controller = "Zoeken", action = "Index", GebeurtenisId = GebeurtenisId });
                }
            }

            if (BoekId.HasValue)
            {
                Boek = context.Boeks.SingleOrDefault(b => b.Id == BoekId.Value)?.Boeknaam;
                if (Boek == null) throw new KeyNotFoundException("Dit boek bestaat niet");
            }

            if (BoekHoofdstukId.HasValue)
            {

                var hoofdstuk = context.BoekHoofdstuks.SingleOrDefault(p => p.Id == BoekHoofdstukId.Value);
                if (hoofdstuk == null) throw new KeyNotFoundException("Dit hoofdstuk bestaat niet");
                else if (hoofdstuk.Boek.TaalId != TaalId)
                {
                    redirectRoute = new RouteValueDictionary(new { culture = hoofdstuk.Boek.Taal.Code.Trim(), controller = "Zoeken", action = "Index", BoekHoofdstukId = BoekHoofdstukId });
                }
            }

            if (SerieId.HasValue)
            {
                var serie = context.Series.SingleOrDefault(p => p.Id == SerieId.Value);
                if (serie == null) throw new KeyNotFoundException("Deze serie bestaat niet");
                else if (serie.TaalId != TaalId)
                {
                     redirectRoute = new RouteValueDictionary(new { culture = serie.Taal.Code.Trim(), controller = "Zoeken", action = "Index", SerieId = SerieId });
                }
            }

            if (GemeenteId.HasValue)
            {
                Gemeente = context.Gemeentes.SingleOrDefault(p => p.Id == GemeenteId.Value)?.Omschrijving; 
                if (Gemeente == null) throw new KeyNotFoundException("Deze gemeente bestaat niet");
            }

            if (LezingCategorieId.HasValue)
            {
                LezingCategorie = context.LezingCategories.SingleOrDefault(lc => lc.Id == LezingCategorieId.Value)?.Omschrijving;
                if (LezingCategorie == null) throw new KeyNotFoundException("Deze lezing categorie bestaat niet");
            }
        }
    }


}
