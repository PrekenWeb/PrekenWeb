using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Boek
    {
        public Boek()
        {
            BoekHoofdstuk = new HashSet<BoekHoofdstuk>();
        }

        public int Id { get; set; }
        public string Boeknaam { get; set; }
        public int Sortering { get; set; }
        public int? OudId { get; set; }
        public string Afkorting { get; set; }
        public bool? ToonHoofdstukNummer { get; set; }
        public int TaalId { get; set; }

        public virtual Taal Taal { get; set; }
        public virtual ICollection<BoekHoofdstuk> BoekHoofdstuk { get; set; }
    }
}
