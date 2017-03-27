using System;
using System.Globalization;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.ViewModels
{
    public class ZoekresultaatItem
    {
        public Preek Preek { get; set; }
        public ResultaatReden ResultaatReden { get; set; }
        public string Boek
        {
            get
            {
                var boekOmschrijving = string.Empty;
                if (Preek.BoekHoofdstuk != null)
                {
                    boekOmschrijving = Preek.BoekHoofdstuk.Omschrijving;
                    //if (Preek.BoekHoofdstuk.Boek != null)
                    //{
                    //    boekOmschrijving = boekOmschrijving /*+ " (" + Preek.BoekHoofdstuk.Boek.Afkorting + ")"*/;
                    //}
                }
                //if (string.IsNullOrEmpty(boekOmschrijving) && Preek.PreekTypeId == (int)PreekTypeEnum.Lezing && !string.IsNullOrEmpty(Preek.ThemaOmschrijving)) boekOmschrijving = string.Format("{0}: {1}", Resources.Resources.Lezing, Preek.ThemaOmschrijving);
                if (string.IsNullOrEmpty(boekOmschrijving) && Preek.PreekTypeId == (int)PreekTypeEnum.Lezing && !string.IsNullOrEmpty(Preek.LezingOmschrijving)) boekOmschrijving = string.Format("{0}", Preek.LezingOmschrijving);

                if (string.IsNullOrEmpty(boekOmschrijving)) boekOmschrijving = "Onbekend";
                return boekOmschrijving;
            } 
        }
        public string Predikant
        {
            get
            {
                return (Preek.Predikant == null) ? "Onbekend" : Preek.Predikant.VolledigeNaam;
            } 
        }
        public string Gebeurtenis
        {
            get
            {
                return (Preek.Gebeurtenis == null) ? string.Empty : Preek.Gebeurtenis.Omschrijving;
            } 
        }
        public string LezingCategorie
        {
            get
            {
                return (Preek.LezingCategorie == null) ? string.Empty : Preek.LezingCategorie.Omschrijving;
            } 
        }
        public string Gemeente
        {
            get
            {
                return (Preek.Gemeente == null) ? string.Empty : Preek.Gemeente.Omschrijving;
            } 
        }
        public string Serie
        {
            get
            {
                return (Preek.Serie == null) ? string.Empty : Preek.Serie.Omschrijving;
            } 
        }
        public string Hoofdstuk
        {
            get
            {
                return (!Preek.Hoofdstuk.HasValue) ? "Onbekend" : Preek.Hoofdstuk.Value.ToString(CultureInfo.InvariantCulture);
            } 
        }
        public bool ToonBoekAfkorting
        {
            get
            {
                return (Preek.BoekHoofdstuk != null && Preek.BoekHoofdstuk.Boek != null) && Preek.BoekHoofdstuk.Boek.ToonHoofdstukNummer;
            } 
        }
        public string Vers
        {
            get
            {
                return Preek.VersOmschrijving;
                //return (string.IsNullOrWhiteSpace(Preek.VanVers) ? "" : Preek.VanVers) + (string.IsNullOrWhiteSpace(Preek.TotVers) ? "" : " - " + Preek.TotVers);
            } 
        }
        public string ZoekResultaatTekst { get; set; }

        public DateTime? Datum => Preek.DatumGepubliceerd ?? Preek.DatumAangemaakt ?? DateTime.Now;

        public string ResultaatIcon
        {
            get
            {
                switch (ResultaatReden)
                {
                    case ResultaatReden.Boek:
                        return "fa fa-book";
                    case ResultaatReden.Gebeurtenis:
                        return "fa fa-calendar";
                    case ResultaatReden.Predikant:
                        return "fa fa-user";
                    case ResultaatReden.Serie:
                        return "fa fa-list-ol";
                    case ResultaatReden.Tekst:
                        return "fa fa-align-left";
                    default:
                        return "fa fa-question-circle";
                }
            } 
        }

        public string UrlIcon
        {
            get
            {
                switch (Preek.PreekTypeId)
                {
                    case (int)PreekTypeEnum.Peek:
                        return "fa fa-music";
                    case (int)PreekTypeEnum.Lezing:
                        return "fa fa-volume-up";
                    case (int)PreekTypeEnum.LeesPreek:
                        return "fa fa-font";
                    default:
                        return "fa fa-arrow-right";
                }
            } 
        }

        public string UrlTekst
        {
            get
            {
                switch (Preek.PreekTypeId)
                {
                    default:
                    //case (int)PreekTypeEnum.Peek:
                    //case (int)PreekTypeEnum.Lezing:
                        return Prekenweb.Resources.Resources.Luister;
                    case (int)PreekTypeEnum.LeesPreek:
                        return Prekenweb.Resources.Resources.Lees;
                }
            } 
        }

        public string Preektype
        {
            get
            {
                switch (Preek.PreekTypeId)
                { 
                    case (int)PreekTypeEnum.Peek:
                        return Prekenweb.Resources.Resources.Preek;
                    case (int)PreekTypeEnum.Lezing:
                        return Prekenweb.Resources.Resources.Lezing;
                    case (int)PreekTypeEnum.LeesPreek:
                        return Prekenweb.Resources.Resources.LeesPreek;
                } 
                return Preek.PreekType.Omschrijving;
            } 
        }

        public string UrlAction
        {
            get
            {
                switch (Preek.PreekTypeId)
                {
                    case (int)PreekTypeEnum.Peek:
                        return "Open";
                    case (int)PreekTypeEnum.Lezing:
                        return "Open";
                    case (int)PreekTypeEnum.LeesPreek:
                        return "Open";
                    default:
                        return "Open";
                }
            } 
        }
        public string UrlController
        {
            get
            {
                return "Preek";
            } 
        }
    }
}
