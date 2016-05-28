using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using PrekenWeb.Data.Tables;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class AfbeeldingIndexViewModel
    {
        public List<Afbeelding> Afbeeldingen { get; set; }
    }
    public class AfbeeldingEditViewModel
    {
        public Afbeelding Afbeelding { get; set; }

        [NotMapped, DisplayName("Bestand"), DataType(DataType.Upload)]
        public HttpPostedFileBase Bestand { get; set; }
    }
}
