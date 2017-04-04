using System.Collections.Generic;
using Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class GebeurtenisIndexViewModel
    {
        public List<Gebeurtenis> Gebeurteniss { get; set; }
    }
    public class GebeurtenisEditViewModel
    {
        public Gebeurtenis Gebeurtenis { get; set; }
    }
}
