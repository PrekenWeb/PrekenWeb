using System.Collections.Generic;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;

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
