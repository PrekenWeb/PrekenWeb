using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class PreekNogTePublicerenViewModel
    {
        public List<Preek> Preken { get; set; }
        public int? FromPreekId { get; set; }
    }
    public class KiesVers
    {
        public string Veldnaam { get; set; }

        [Display(Name = "Boek", ResourceType = typeof(Resources.Resources))]
        public string Boek { get; set; }

        [Display(Name = "Hoofdstuk", ResourceType = typeof(Resources.Resources))]
        public int? Hoofdstuk { get; set; }

        public IQueryable<BoekHoofdstukTekst> Teksten { get; set; }
    }
}
