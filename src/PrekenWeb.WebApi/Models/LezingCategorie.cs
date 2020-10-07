using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class LezingCategorie
    {
        public LezingCategorie()
        {
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public int? OudId { get; set; }

        public virtual ICollection<Preek> Preek { get; set; }
    }
}
