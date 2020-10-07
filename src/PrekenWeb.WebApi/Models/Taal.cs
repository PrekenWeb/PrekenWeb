using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Taal
    {
        public Taal()
        {
            Boek = new HashSet<Boek>();
            Gebeurtenis = new HashSet<Gebeurtenis>();
            Mailing = new HashSet<Mailing>();
            NieuwsbriefInschrijving = new HashSet<NieuwsbriefInschrijving>();
            Predikant = new HashSet<Predikant>();
            Preek = new HashSet<Preek>();
            Serie = new HashSet<Serie>();
            Spotlight = new HashSet<Spotlight>();
            Tekst = new HashSet<Tekst>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Boek> Boek { get; set; }
        public virtual ICollection<Gebeurtenis> Gebeurtenis { get; set; }
        public virtual ICollection<Mailing> Mailing { get; set; }
        public virtual ICollection<NieuwsbriefInschrijving> NieuwsbriefInschrijving { get; set; }
        public virtual ICollection<Predikant> Predikant { get; set; }
        public virtual ICollection<Preek> Preek { get; set; }
        public virtual ICollection<Serie> Serie { get; set; }
        public virtual ICollection<Spotlight> Spotlight { get; set; }
        public virtual ICollection<Tekst> Tekst { get; set; }
    }
}
