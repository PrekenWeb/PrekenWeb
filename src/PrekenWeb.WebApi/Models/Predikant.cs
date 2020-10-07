using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Predikant
    {
        public Predikant()
        {
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public string Titels { get; set; }
        public string Voorletters { get; set; }
        public string Achternaam { get; set; }
        public string Gemeente { get; set; }
        public string LevensPeriode { get; set; }
        public int? OudId { get; set; }
        public int? GemeenteId { get; set; }
        public string Tussenvoegsels { get; set; }
        public string Opmerking { get; set; }
        public int TaalId { get; set; }
        public bool HideFromIndexingRobots { get; set; }
        public bool HideFromPodcast { get; set; }

        public virtual Gemeente GemeenteNavigation { get; set; }
        public virtual Taal Taal { get; set; }
        public virtual ICollection<Preek> Preek { get; set; }
    }
}
