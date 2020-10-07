using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Tekst
    {
        public int Id { get; set; }
        public string Kop { get; set; }
        public string Tekst1 { get; set; }
        public int TaalId { get; set; }
        public int? PaginaId { get; set; }

        public virtual Pagina Pagina { get; set; }
        public virtual Taal Taal { get; set; }
    }
}
