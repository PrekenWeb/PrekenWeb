using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Afbeelding
    {
        public Afbeelding()
        {
            Preek = new HashSet<Preek>();
            Spotlight = new HashSet<Spotlight>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public string Bestandsnaam { get; set; }
        public string ContentType { get; set; }

        public virtual ICollection<Preek> Preek { get; set; }
        public virtual ICollection<Spotlight> Spotlight { get; set; }
    }
}
