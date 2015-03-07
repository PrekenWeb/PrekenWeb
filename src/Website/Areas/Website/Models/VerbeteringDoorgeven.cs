using Prekenweb.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Website.ViewModels
{
    public class VerbeteringDoorgeven
    {
        public TekstPagina TekstPagina { get; set; }

        [Display(Name = "Naam", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NaamIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Naam { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "EmailIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "Onderwerp", ResourceType = typeof(Resources.Resources))]
        public string Onderwerp { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Idee", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "TekstIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Tekst { get; set; }

        public bool Verzonden { get; set; }

    }
}
