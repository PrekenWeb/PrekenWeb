using System.ComponentModel.DataAnnotations;
using Data.ViewModels;

namespace Prekenweb.Website.Areas.Website.Models
{
    public class Contact
    {
        public TekstPagina TekstPagina { get; set; }

        [Display(Name = "Naam", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName="NaamIsVerplicht", ErrorMessageResourceType=typeof(Resources.Resources))]
        public string Naam { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "EmailIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "Onderwerp", ResourceType = typeof(Resources.Resources))]
        public string Onderwerp { get; set; }
   
        [DataType(DataType.MultilineText)]
        [Display(Name = "Tekst", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "TekstIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Tekst { get; set; }

        public bool Verzonden { get; set; }

    }
}
