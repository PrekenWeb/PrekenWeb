using System.Collections.Generic;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class SerieIndexViewModel
    {
        public List<Serie> Series { get; set; }
    }
    public class SerieEditViewModel
    {
        public Serie Serie { get; set; }
    }
}
