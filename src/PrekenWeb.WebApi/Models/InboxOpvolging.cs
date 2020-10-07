using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class InboxOpvolging
    {
        public int Id { get; set; }
        public int InboxId { get; set; }
        public int? GebruikerId { get; set; }
        public DateTime Aangemaakt { get; set; }
        public string Onderwerp { get; set; }
        public string Tekst { get; set; }
        public bool VerstuurAlsMail { get; set; }
        public DateTime? Verstuurd { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
        public virtual Inbox Inbox { get; set; }
    }
}
