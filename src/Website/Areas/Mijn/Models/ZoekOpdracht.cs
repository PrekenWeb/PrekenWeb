using System.Collections.Generic;
using Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class OpgeslagenZoekOpdrachten
    { 
        public IEnumerable<ZoekOpdracht> ZoekOpdrachten { get; set; }
    }
}
