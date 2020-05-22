using System;

namespace Data.Models.Dtos
{ 
    public class Preek
    { 
        public int Id { get; set; }

        public int? BoekhoofdstukId { get; set; }

        public string BijbeltekstOmschrijving { get; set; }

        public int? SerieId { get; set; }

        public int? GebeurtenisId { get; set; }

        public DateTime? DatumAangemaakt { get; set; }

        public DateTime? DatumBijgewerkt { get; set; }

        public string Bestandsnaam { get; set; }

        //public int AantalKeerGedownload { get; set; }

        //public int? OudId { get; set; }

        public int? PredikantId { get; set; }
         
        public int? Hoofdstuk { get; set; }
         
        public string VanVers { get; set; }
         
        public string TotVers { get; set; }
         
        public string Punt1 { get; set; }
         
        public string Punt2 { get; set; }
         
        public string Punt3 { get; set; }
         
        public string Punt4 { get; set; }
         
        public string Punt5 { get; set; }
         
        public int? GemeenteId { get; set; }
         
        public DateTime? DatumPreek { get; set; }
         
        public string Informatie { get; set; }
         
        public string ThemaOmschrijving { get; set; }

        //public int? AfbeeldingId { get; set; }
         
        public int PreekTypeId { get; set; }
         
        public int? LezingCategorieId { get; set; }
         
        //public int TaalId { get; set; }
         
        //public bool Gepubliceerd { get; set; }
         
        public string LezingOmschrijving { get; set; }

        public TimeSpan? Duur { get; set; }

        public int? Bestandsgrootte { get; set; }
         
        public int? VersVanId { get; set; }
         
        public int? VersTotId { get; set; }

        public int? GedeelteVanVersId { get; set; }

        public int? GedeelteTotVersId { get; set; }
         
        public string VersOmschrijving { get; set; }
         
        //public bool AutomatischeTeksten { get; set; }
         
        //public int? AangemaaktDoor { get; set; }
         
        //public int? AangepastDoor { get; set; }

        //public string LeesPreekTekst { get; set; } 
         
        public string BoekHoofdstukOmschrijving { get; set; }
         
        public string LezingCategorieNaam { get; set; }
         
        public string GebeurtenisOmschrijving { get; set; } 
        public string GemeenteNaam { get; set; }
        public string PredikantNaam { get; set; }
        public string PreekTypeNaam { get; set; }
        public string SerieOmschrijving { get; set; }
        public string TaalOmschrijving { get; set; }
         
        public string PreekTitel { get; set; } 
    }
}