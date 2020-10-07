using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class PreekType
    {
        public PreekType()
        {
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Preek> Preek { get; set; }
    }
}
