using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class GebruikerMailing
    {
        public int GebruikerId { get; set; }
        public int MailingId { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
        public virtual Mailing Mailing { get; set; }
    }
}
