using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Prekenweb.Models.Identity;

namespace Prekenweb.Models
{
    [Table("ZoekOpdracht")]
    public class ZoekOpdracht : IEquatable<ZoekOpdracht>
    {
        public int Id { get; set; }

        public Guid PubliekeSleutel { get; set; }

        public bool LeesPreken { get; set; }
        public bool AudioPreken { get; set; }
        public bool Lezingen { get; set; }

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

        public SorteerOp? SorteerOp { get; set; }
        public SorteerVolgorde SorteerVolgorde { get; set; }


        public virtual Gebruiker Gebruiker { get; set; }

        public ZoekOpdracht()
        {
            
            PubliekeSleutel = Guid.NewGuid();
        }
        

        [NotMapped]
        public List<int> PreekTypeIds
        {
            get
            {
                {
                    var preekTypIds = new List<int>();

                    if (AudioPreken) preekTypIds.Add((int)PreekTypeEnum.Peek);
                    if (LeesPreken) preekTypIds.Add((int)PreekTypeEnum.LeesPreek);
                    if (Lezingen) preekTypIds.Add((int)PreekTypeEnum.Lezing);
                    return preekTypIds;
                }
            }
        }

        [NotMapped]
        public string Omschrijving
        {
            get
            {
                {
                    var parts = new List<string>();
                    if (!AudioPreken || !LeesPreken || !Lezingen)
                    {
                        if (AudioPreken && LeesPreken) { parts.Add(Resources.Resources.LeesEnAudioPreken); }
                        else if (AudioPreken && Lezingen) { parts.Add(Resources.Resources.LezingenEnAudioPreken); }
                        else if (Lezingen && LeesPreken) { parts.Add(Resources.Resources.LeesprekenEnLezingen); }
                        else if (LeesPreken) { parts.Add(Resources.Resources.NieuweLeespreken); }
                        else if (Lezingen) { parts.Add(Resources.Resources.NieuweLezingen); }
                        else if (AudioPreken) { parts.Add(Resources.Resources.NieuweAudioPreken); }
                    }
                    if (!string.IsNullOrWhiteSpace(Zoekterm)) parts.Add(string.Format("{0}: {1}", Resources.Resources.Zoekterm, Zoekterm));
                    if (!string.IsNullOrWhiteSpace(Predikant)) parts.Add(string.Format("{0}: {1}", Resources.Resources.Predikant, Predikant));
                    if (!string.IsNullOrWhiteSpace(Gemeente)) parts.Add(string.Format("{0}: {1}", Resources.Resources.Gemeente, Gemeente));
                    if (!string.IsNullOrWhiteSpace(Gebeurtenis)) parts.Add(string.Format("{0}: {1}", Resources.Resources.Gebeurtenis, Gebeurtenis));
                    if (!string.IsNullOrWhiteSpace(Serie)) parts.Add(string.Format("{0}: {1}", Resources.Resources.Serie, Serie));
                    if (SorteerOp.HasValue && SorteerOp.Value != Models.SorteerOp.Datum) parts.Add(string.Format("gesorteerd op: {0}", SorteerOp.Value.ToString()));

                    return string.Join(", ", parts.ToArray());
                }
            }
        }

        public bool Equals(ZoekOpdracht other)
        {
            if (other == null)
                return false;

            return LeesPreken.Equals(other.LeesPreken)
                && AudioPreken.Equals(other.AudioPreken)
                && Lezingen.Equals(other.Lezingen)
                && PredikantId.Equals(other.PredikantId)
                && Predikant.Equals(other.Predikant)
                && BoekHoofdstukId.Equals(other.BoekHoofdstukId)
                && BoekHoofdstuk.Equals(other.BoekHoofdstuk)
                && BoekId.Equals(other.BoekId)
                && Boek.Equals(other.Boek)
                && LezingCategorieId.Equals(other.LezingCategorieId)
                && LezingCategorie.Equals(other.LezingCategorie)
                && Hoofdstuk.Equals(other.Hoofdstuk)
                && GebeurtenisId.Equals(other.GebeurtenisId)
                && Gebeurtenis.Equals(other.Gebeurtenis)
                && GemeenteId.Equals(other.GemeenteId)
                && Gemeente.Equals(other.Gemeente)
                && SerieId.Equals(other.SerieId)
                && TaalId.Equals(other.TaalId)
                && GebruikerId.Equals(other.GebruikerId)
                && Zoekterm.Equals(other.Zoekterm)
                && SorteerOp.Equals(other.SorteerOp)
                && SorteerVolgorde.Equals(other.SorteerVolgorde);
        }
    }
    public enum SorteerOp
    {
        Predikant = 0,
        Boek = 1,
        Hoofdstuk = 2,
        Vers = 3,
        Gebeurtenis = 4,
        Gemeente = 5,
        Serie = 6,
        LezingCategorie = 7,
        Datum = 8
    }
    public enum SorteerVolgorde
    {
        ASC = 0,
        DESC = 1
    }

}
