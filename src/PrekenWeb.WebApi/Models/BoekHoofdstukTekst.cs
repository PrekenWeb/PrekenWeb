using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class BoekHoofdstukTekst
    {
        public BoekHoofdstukTekst()
        {
            PreekGedeelteTotVers = new HashSet<Preek>();
            PreekGedeelteVanVers = new HashSet<Preek>();
            PreekVersTot = new HashSet<Preek>();
            PreekVersVan = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public int BoekHoofdstukId { get; set; }
        public int Hoofdstuk { get; set; }
        public int Vers { get; set; }
        public string Tekst { get; set; }
        public int Sortering { get; set; }

        public virtual BoekHoofdstuk BoekHoofdstuk { get; set; }
        public virtual ICollection<Preek> PreekGedeelteTotVers { get; set; }
        public virtual ICollection<Preek> PreekGedeelteVanVers { get; set; }
        public virtual ICollection<Preek> PreekVersTot { get; set; }
        public virtual ICollection<Preek> PreekVersVan { get; set; }
    }
}
