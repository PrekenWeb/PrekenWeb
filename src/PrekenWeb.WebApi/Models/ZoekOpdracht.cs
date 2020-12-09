using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class ZoekOpdracht
    {
        public int Id { get; set; }
        public Guid PubliekeSleutel { get; set; }
        public bool LeesPreken { get; set; }
        public bool AudioPreken { get; set; }
        public bool VideoPreken { get; set; }
        public bool Lezingen { get; set; }
        public bool Meditaties { get; set; }
        public int? PredikantId { get; set; }
        public string Predikant { get; set; }
        public int? BoekHoofdstukId { get; set; }
        public string BoekHoofdstuk { get; set; }
        public int? BoekId { get; set; }
        public string Boek { get; set; }
        public int? LezingCategorieId { get; set; }
        public string LezingCategorie { get; set; }
        public int? Hoofdstuk { get; set; }
        public int? GebeurtenisId { get; set; }
        public string Gebeurtenis { get; set; }
        public int? GemeenteId { get; set; }
        public string Gemeente { get; set; }
        public int? SerieId { get; set; }
        public string Serie { get; set; }
        public int TaalId { get; set; }
        public int GebruikerId { get; set; }
        public string Zoekterm { get; set; }
        public int? SorteerOp { get; set; }
        public int SorteerVolgorde { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
    }
}
