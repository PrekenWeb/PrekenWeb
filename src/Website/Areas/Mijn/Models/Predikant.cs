using System.Collections.Generic;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class PredikantIndexViewModel
    {
        public List<Predikant> Predikanten { get; set; }
    }
    public class PredikantEditViewModel
    {
        public Predikant Predikant { get; set; }
    }
}
