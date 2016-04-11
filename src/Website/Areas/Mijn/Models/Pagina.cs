using System.Collections.Generic;
using PrekenWeb.Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class PaginaIndexViewModel
    {
        public List<Pagina> Paginas { get; set; }
    }
    public class PaginaEditViewModel
    {
        public Pagina Pagina { get; set; }
        public List<Tekst> Teksten { get; set; }
    }
}
