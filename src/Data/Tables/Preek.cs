using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using PrekenWeb.Data.Attributes;
using PrekenWeb.Data.Identity;

namespace PrekenWeb.Data.Tables
{ 
    public class Preek
    { 
        public Preek()
        {
            DatumAangemaakt = DateTime.Now;
            DatumBijgewerkt = DateTime.Now;
            AantalKeerGedownload = 0;
            Gepubliceerd = false;
            AutomatischeTeksten = false;
            Inboxes = new List<Inbox>();
            PreekCookies = new List<PreekCookie>();
            PreekLezenEnZingens = new List<PreekLezenEnZingen>();
            
        }

        public int Id { get; set; }

        [UIHint("Boekhoofdstuk"), Display(Name = "Boek", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? BoekhoofdstukId { get; set; }

        [UIHint("Bijbeltekst"), Display(Name = "BijbeltekstOmschrijving", ResourceType = typeof(Prekenweb.Resources.Resources)), DataType(DataType.MultilineText), Tooltip("Maak gebruik van blokhaken om een versnummer te markeren, bijvoorbeeld:<br/>[1] In den beginne...")]
        public string BijbeltekstOmschrijving { get; set; }

        [UIHint("Serie"), Display(Name = "Serie", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? SerieId { get; set; }

        [UIHint("Gebeurtenis"), Display(Name = "Gebeurtenis", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? GebeurtenisId { get; set; }

        [DataType(DataType.DateTime), Display(Name = "AangemaaktOp", ResourceType = typeof(Prekenweb.Resources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = false)]
        public DateTime? DatumAangemaakt { get; set; }

        [DataType(DataType.DateTime), Display(Name = "BijgewerktOp", ResourceType = typeof(Prekenweb.Resources.Resources)), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DatumBijgewerkt { get; set; }

        [Display(Name = "Bestand", ResourceType = typeof(Prekenweb.Resources.Resources)), DataType(DataType.Upload)]
        public string Bestandsnaam { get; set; }

        [Display(Name = "AantalKeerGedownload", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int AantalKeerGedownload { get; set; }

        public int? OudId { get; set; }

        [Required(ErrorMessage = "Predikant is verplicht"), UIHint("Predikant"), Display(Name = "Predikant", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? PredikantId { get; set; }

        [Display(Name = "Hoofdstuk", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? Hoofdstuk { get; set; }

        [Display(Name = "VanVers", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string VanVers { get; set; }

        [Display(Name = "TotVers", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string TotVers { get; set; }

        [Display(Name = "Punt1", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Punt1 { get; set; }

        [Display(Name = "Punt2", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Punt2 { get; set; }

        [Display(Name = "Punt3", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Punt3 { get; set; }

        [Display(Name = "Punt4", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Punt4 { get; set; }

        [Display(Name = "Punt5", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Punt5 { get; set; }

        [UIHint("Gemeente"), Display(Name = "Gemeente", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? GemeenteId { get; set; }

        [DataType(DataType.DateTime), Display(Name = "GepreektOp", ResourceType = typeof(Prekenweb.Resources.Resources)), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DatumPreek { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "OverigeInformatie", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Informatie { get; set; }

        [Display(Name = "Thema", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string ThemaOmschrijving { get; set; }

        public int? AfbeeldingId { get; set; }

        [Required(ErrorMessage = "Preek type is verplicht"), UIHint("PreekType"), Display(Name = "PreekType", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int PreekTypeId { get; set; }

        [UIHint("LezingCategorie"), Display(Name = "LezingCategorie", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? LezingCategorieId { get; set; }

        [UIHint("Taal"), Display(Name = "Taal", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(ErrorMessage = "Taal is verplicht!")]
        public int TaalId { get; set; }

        [Display(Name = "Gepubliceerd", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public bool Gepubliceerd { get; set; }

        [UIHint("LezingOmschrijving"), Display(Name = "LezingOmschrijving", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string LezingOmschrijving { get; set; }

        public TimeSpan? Duur { get; set; }

        public int? Bestandsgrootte { get; set; }

        [UIHint("Vers"), Display(Name = "VanVersId", ResourceType = typeof(Prekenweb.Resources.Resources)), Tooltip("VersVanIdTooltip", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? VersVanId { get; set; }

        [UIHint("Vers"), Display(Name = "TotVersId", ResourceType = typeof(Prekenweb.Resources.Resources)), Tooltip("VersTotIdTooltip", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? VersTotId { get; set; }

        public int? GedeelteVanVersId { get; set; }

        public int? GedeelteTotVersId { get; set; }

        [Display(Name = "VersOmschrijving", ResourceType = typeof(Prekenweb.Resources.Resources)), Tooltip("VersOmschrijvingTooltip", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string VersOmschrijving { get; set; }

        [Display(Name = "AutomatischeTeksten", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public bool AutomatischeTeksten { get; set; }

        [UIHint("Gebruiker"), Display(Name = "AangemaaktDoor", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? AangemaaktDoor { get; set; }

        [UIHint("Gebruiker"), Display(Name = "AangepastDoor", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int? AangepastDoor { get; set; }

        [Display(Name = "LeesPreekTekst", ResourceType = typeof(Prekenweb.Resources.Resources)), DataType(DataType.Html), Tooltip("LeesPreekTekstTooltip", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string LeesPreekTekst { get; set; }

        public virtual ICollection<Inbox> Inboxes { get; set; }
        public virtual ICollection<PreekCookie> PreekCookies { get; set; }
        public virtual ICollection<PreekLezenEnZingen> PreekLezenEnZingens { get; set; }

        [UIHint("BoekHoofdstuk"), Display(Name = "Boek", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public virtual BoekHoofdstuk BoekHoofdstuk { get; set; }

        [Display(Name = "Categorie", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public virtual LezingCategorie LezingCategorie { get; set; }

        public virtual Afbeelding Afbeelding { get; set; }
        public virtual BoekHoofdstukTekst BoekHoofdstukTekst_GedeelteTotVersId { get; set; }
        public virtual BoekHoofdstukTekst BoekHoofdstukTekst_GedeelteVanVersId { get; set; }
        public virtual BoekHoofdstukTekst BoekHoofdstukTekst_VersTotId { get; set; }
        public virtual BoekHoofdstukTekst BoekHoofdstukTekst_VersVanId { get; set; }
        public virtual Gebeurtenis Gebeurtenis { get; set; }
        public virtual Gebruiker Gebruiker_AangemaaktDoor { get; set; }
        public virtual Gebruiker Gebruiker_AangepastDoor { get; set; }
        public virtual Gemeente Gemeente { get; set; }
        public virtual Predikant Predikant { get; set; }
        public virtual PreekType PreekType { get; set; }
        public virtual Serie Serie { get; set; }
        public virtual Taal Taal { get; set; }

        


        public string GetShortBijbelTekst(string zoekTerm)
        {
            if (BijbeltekstOmschrijving.Length < 100)
            {
                return BijbeltekstOmschrijving;
            }
            else
            {
                var returnValue = BijbeltekstOmschrijving;
                var firstOccurrence = returnValue.IndexOf(zoekTerm, 0, StringComparison.CurrentCultureIgnoreCase);
                var startIndex = (firstOccurrence < 30) ? 0 : firstOccurrence - 29;
                var stopIndex = (firstOccurrence + 90 > returnValue.Length) ? returnValue.Length - startIndex : 90;
                var prefix = (startIndex > 0) ? "..." : "";
                var suffix = (stopIndex == 90) ? "..." : "";

                return prefix + returnValue.Substring(startIndex, stopIndex) + suffix;
            }

        }
        [NotMapped]
        public string GebeurtenisTekst
        {
            get
            {
                if (!GebeurtenisId.HasValue) return string.Empty;
                return string.Format("{0}", Gebeurtenis.Omschrijving);
            }
            set { }
        }
        [NotMapped]
        public string BoekTekst
        {
            get
            {
                if (!BoekhoofdstukId.HasValue) return string.Empty;
                return string.Format("{0}", BoekHoofdstuk.Omschrijving, BoekHoofdstuk.Boek.Afkorting, Hoofdstuk);
            }
            set { }
        }

        [NotMapped]
        public string AangepastGebruikerNaam
        {
            get
            {
                if (!AangepastDoor.HasValue) return string.Empty;
                return string.Format("{0}", Gebruiker_AangepastDoor.Naam);
            }
            set { }
        }

        [NotMapped]
        public string AangemaaktGebruikerNaam
        {
            get
            {
                if (!AangemaaktDoor.HasValue) return string.Empty;
                return string.Format("{0}", Gebruiker_AangemaaktDoor.Naam);
            }
            set { }
        } 

        public string GetContentType()
        {
            //if (!File.Exists(this.Bestandsnaam)) throw new Exception("Geen bestand = geen type! :)");
            switch (Path.GetExtension(Bestandsnaam)?.ToLower())
            {
                case "???":
                    // hier een site met alle contenttypes (comment 1)
                    // http://stackoverflow.com/questions/1029740/get-mime-type-from-extension
                    return "";
                case ".mp3":
                    return "audio/mpeg";
                case ".pdf":
                    return "application/pdf";
                default:
                    return "audio/mpeg";
                    throw new Exception("Onbekend bestandsformaat");
            }
        }

        [NotMapped]
        public string PreekTitel { get { return GetPreekTitel(); }
             set { }
        }

        public string GetPreekTitel()
        {
            var titel = string.Empty;
            if (Predikant != null)
            {
                titel += Predikant.VolledigeNaam;
            }
            if (Predikant != null)
            {
                titel += string.Format(" {0} ", Prekenweb.Resources.Resources.Uit);
                titel += GetBoekhoofdstukOmschrijving();
            }
            return titel.Replace("  ", " ");
        }

        public string GetBoekhoofdstukOmschrijving()
        {
            var boekhoofdstukOmschrijving = string.Empty;
            if (BoekHoofdstuk != null)
            {
                boekhoofdstukOmschrijving += BoekHoofdstuk.Omschrijving;

                if (Hoofdstuk.HasValue) boekhoofdstukOmschrijving += " " + Hoofdstuk;
                if (!string.IsNullOrWhiteSpace(VersOmschrijving)) boekhoofdstukOmschrijving += " : " + VersOmschrijving;
            }
            else if (DatumPreek.HasValue)
            {
                boekhoofdstukOmschrijving += string.Format(" {0} {1:d}", Prekenweb.Resources.Resources.Op, DatumPreek.Value);
            }
            return boekhoofdstukOmschrijving;
        }



        public string GetPreekOmschrijvingITunes()
        {
            var omschrijving = string.Empty;
            if (!string.IsNullOrEmpty(ThemaOmschrijving)) omschrijving += string.Format(" {0}: {1}", Prekenweb.Resources.Resources.Thema, ThemaOmschrijving);
            if (!string.IsNullOrEmpty(Punt1)) omschrijving += string.Format(" {0}: {1}", Prekenweb.Resources.Resources.Punt1, Punt1);
            if (!string.IsNullOrEmpty(Punt2)) omschrijving += string.Format(", {0}: {1}", Prekenweb.Resources.Resources.Punt2, Punt2);
            if (!string.IsNullOrEmpty(Punt3)) omschrijving += string.Format(", {0}: {1}", Prekenweb.Resources.Resources.Punt3, Punt3);
            if (!string.IsNullOrEmpty(Punt4)) omschrijving += string.Format(", {0}: {1}", Prekenweb.Resources.Resources.Punt4, Punt4);
            if (!string.IsNullOrEmpty(Punt5)) omschrijving += string.Format(", {0}: {1}", Prekenweb.Resources.Resources.Punt5, Punt5);
            if (!string.IsNullOrEmpty(BijbeltekstOmschrijving)) omschrijving += string.Format(", {0}: {1}", Prekenweb.Resources.Resources.BijbeltekstOmschrijving, BijbeltekstOmschrijving);

            return omschrijving.Replace("  ", " ").TrimStart(',').TrimEnd(',').Trim();
        }

        [NotMapped, Display(Name = "AankondigingenBord", ResourceType = typeof(Prekenweb.Resources.Resources)), Tooltip("AankondigingenBordTooltip", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string AankondigingenBord { get; set; }

        [NotMapped]
        public string LezenZingenSoort1 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 1); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort2 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 2); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort3 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 3); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort4 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 4); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort5 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 5); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort6 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 6); return plz == null ? string.Empty : plz.Soort; } set { } }
        [NotMapped]
        public string LezenZingenSoort7 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 7); return plz == null ? string.Empty : plz.Soort; } set { } }

        [NotMapped]
        public string LezenZingenOmschrijving1 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 1); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving2 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 2); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving3 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 3); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving4 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 4); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving5 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 5); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving6 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 6); return plz == null ? string.Empty : plz.Omschrijving; } set { } }
        [NotMapped]
        public string LezenZingenOmschrijving7 { get { var plz = PreekLezenEnZingens.FirstOrDefault(x => x.Sortering == 7); return plz == null ? string.Empty : plz.Omschrijving; } set { } }

    }

}
