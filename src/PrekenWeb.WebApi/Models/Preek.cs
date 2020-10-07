using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Preek
    {
        public Preek()
        {
            Inbox = new HashSet<Inbox>();
            PreekCookie = new HashSet<PreekCookie>();
            PreekLezenEnZingen = new HashSet<PreekLezenEnZingen>();
        }

        public int Id { get; set; }
        public int? BoekhoofdstukId { get; set; }
        public string BijbeltekstOmschrijving { get; set; }
        public int? SerieId { get; set; }
        public int? GebeurtenisId { get; set; }
        public DateTime? DatumAangemaakt { get; set; }
        public DateTime? DatumBijgewerkt { get; set; }
        public string Bestandsnaam { get; set; }
        public int AantalKeerGedownload { get; set; }
        public int? OudId { get; set; }
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
        public int? AfbeeldingId { get; set; }
        public int PreekTypeId { get; set; }
        public int? LezingCategorieId { get; set; }
        public int TaalId { get; set; }
        public bool Gepubliceerd { get; set; }
        public string LezingOmschrijving { get; set; }
        public TimeSpan? Duur { get; set; }
        public int? Bestandsgrootte { get; set; }
        public int? VersVanId { get; set; }
        public int? VersTotId { get; set; }
        public int? GedeelteVanVersId { get; set; }
        public int? GedeelteTotVersId { get; set; }
        public string VersOmschrijving { get; set; }
        public bool AutomatischeTeksten { get; set; }
        public int? AangemaaktDoor { get; set; }
        public int? AangepastDoor { get; set; }
        public string LeesPreekTekst { get; set; }
        public DateTime? DatumGepubliceerd { get; set; }
        public string SourceFileName { get; set; }
        public string Video { get; set; }
        public string MeditatieTekst { get; set; }

        public virtual Gebruiker AangemaaktDoorNavigation { get; set; }
        public virtual Gebruiker AangepastDoorNavigation { get; set; }
        public virtual Afbeelding Afbeelding { get; set; }
        public virtual BoekHoofdstuk Boekhoofdstuk { get; set; }
        public virtual Gebeurtenis Gebeurtenis { get; set; }
        public virtual BoekHoofdstukTekst GedeelteTotVers { get; set; }
        public virtual BoekHoofdstukTekst GedeelteVanVers { get; set; }
        public virtual Gemeente Gemeente { get; set; }
        public virtual LezingCategorie LezingCategorie { get; set; }
        public virtual Predikant Predikant { get; set; }
        public virtual PreekType PreekType { get; set; }
        public virtual Serie Serie { get; set; }
        public virtual Taal Taal { get; set; }
        public virtual BoekHoofdstukTekst VersTot { get; set; }
        public virtual BoekHoofdstukTekst VersVan { get; set; }
        public virtual ICollection<Inbox> Inbox { get; set; }
        public virtual ICollection<PreekCookie> PreekCookie { get; set; }
        public virtual ICollection<PreekLezenEnZingen> PreekLezenEnZingen { get; set; }
    }
}
