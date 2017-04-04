using System.Collections.Generic;
using Data.Tables;

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
