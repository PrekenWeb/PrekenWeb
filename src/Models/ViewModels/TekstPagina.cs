using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prekenweb.Models.ViewModels
{
    public class TekstPagina
    {
        public string Kop { get; set; }
        public int HuidigeTaalId { get; set; }
        public int PaginaTaalId { get; set; }
        public string Tekst { get; set; }
    }
}
