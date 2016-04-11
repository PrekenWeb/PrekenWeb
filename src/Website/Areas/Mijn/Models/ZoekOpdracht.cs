using PrekenWeb.Data.Tables;
using System.Collections.Generic;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class OpgeslagenZoekOpdrachten
    { 
        public IEnumerable<ZoekOpdracht> ZoekOpdrachten { get; set; }
    }
}
