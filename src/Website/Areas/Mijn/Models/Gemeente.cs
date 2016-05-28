using System.Collections.Generic;
using PrekenWeb.Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class GemeenteIndexViewModel
    {
        public List<Gemeente> Gemeentes { get; set; }
    }
    public class GemeenteEditViewModel
    {
        public Gemeente Gemeente { get; set; }
    }
}
