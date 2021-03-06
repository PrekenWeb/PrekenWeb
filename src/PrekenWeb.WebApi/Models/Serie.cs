﻿using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Serie
    {
        public Serie()
        {
            Preek = new HashSet<Preek>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public int? OudId { get; set; }
        public int TaalId { get; set; }

        public virtual Taal Taal { get; set; }
        public virtual ICollection<Preek> Preek { get; set; }
    }
}
