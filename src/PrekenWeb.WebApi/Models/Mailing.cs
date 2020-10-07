using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Mailing
    {
        public Mailing()
        {
            GebruikerMailing = new HashSet<GebruikerMailing>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public string MailChimpId { get; set; }
        public int TaalId { get; set; }

        public virtual Taal Taal { get; set; }
        public virtual ICollection<GebruikerMailing> GebruikerMailing { get; set; }
    }
}
