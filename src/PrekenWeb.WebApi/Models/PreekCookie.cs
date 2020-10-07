using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class PreekCookie
    {
        public int Id { get; set; }
        public int PreekId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Opmerking { get; set; }
        public TimeSpan? AfgespeeldTot { get; set; }
        public int GebruikerId { get; set; }
        public DateTime? BladwijzerGelegdOp { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
        public virtual Preek Preek { get; set; }
    }
}
