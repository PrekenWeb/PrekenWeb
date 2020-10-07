using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class NieuwsbriefInschrijving
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public DateTime Aangemeld { get; set; }
        public DateTime? Afgemeld { get; set; }
        public int TaalId { get; set; }

        public virtual Taal Taal { get; set; }
    }
}
