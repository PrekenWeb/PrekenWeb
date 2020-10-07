using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class PreekLezenEnZingen
    {
        public int Id { get; set; }
        public int PreekId { get; set; }
        public byte Sortering { get; set; }
        public string Soort { get; set; }
        public string Omschrijving { get; set; }

        public virtual Preek Preek { get; set; }
    }
}
