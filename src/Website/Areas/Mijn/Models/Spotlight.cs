using System.Collections.Generic;
using Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class SpotlightIndexViewModel
    {
        public List<Spotlight> Spotlights { get; set; }
    }
    public class SpotlightEditViewModel
    {
        public Spotlight Spotlight { get; set; }
    }
}
