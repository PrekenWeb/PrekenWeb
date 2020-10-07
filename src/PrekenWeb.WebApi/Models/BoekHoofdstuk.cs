using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class BoekHoofdstuk
    {
        public BoekHoofdstuk()
        {
            BoekHoofdstukTekst = new HashSet<BoekHoofdstukTekst>();
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public int BoekId { get; set; }
        public string Omschrijving { get; set; }
        public int? Sortering { get; set; }
        public int? OudId { get; set; }

        public virtual Boek Boek { get; set; }
        public virtual ICollection<BoekHoofdstukTekst> BoekHoofdstukTekst { get; set; }
        public virtual ICollection<Preek> Preek { get; set; }
    }
}
