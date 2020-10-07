using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Inbox
    {
        public Inbox()
        {
            InboxOpvolging = new HashSet<InboxOpvolging>();
        }

        public int Id { get; set; }
        public int InboxTypeId { get; set; }
        public string Omschrijving { get; set; }
        public string Inhoud { get; set; }
        public int? GebruikerId { get; set; }
        public bool Afgehandeld { get; set; }
        public DateTime Aangemaakt { get; set; }
        public string VanNaam { get; set; }
        public string VanEmail { get; set; }
        public string AanNaam { get; set; }
        public string AanEmail { get; set; }
        public int? PreekId { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
        public virtual InboxType InboxType { get; set; }
        public virtual Preek Preek { get; set; }
        public virtual ICollection<InboxOpvolging> InboxOpvolging { get; set; }
    }
}
