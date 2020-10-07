using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Gemeente
    {
        public Gemeente()
        {
            Predikant = new HashSet<Predikant>();
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Predikant> Predikant { get; set; }
        public virtual ICollection<Preek> Preek { get; set; }
    }
}
