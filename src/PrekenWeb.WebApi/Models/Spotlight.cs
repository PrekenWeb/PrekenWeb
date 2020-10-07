using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Spotlight
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Subtitel { get; set; }
        public string LinkTitel { get; set; }
        public string Url { get; set; }
        public int AfbeeldingId { get; set; }
        public int Sortering { get; set; }
        public int TaalId { get; set; }
        public bool NieuwVenster { get; set; }

        public virtual Afbeelding Afbeelding { get; set; }
        public virtual Taal Taal { get; set; }
    }
}
