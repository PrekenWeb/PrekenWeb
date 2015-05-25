using System.ComponentModel.DataAnnotations;
using PrekenWeb.Data.ViewModels;

namespace Prekenweb.Website.ViewModels
{
    public class PreekToevoegen
    {
        public TekstPagina TekstPagina { get; set; }

        [Display(Name = "Naam", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NaamIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Naam { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "EmailIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "Telefoon", ResourceType = typeof(Resources.Resources))]
        public string Telefoon { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Opmerking", ResourceType = typeof(Resources.Resources))]
        public string Tekst { get; set; }

        public bool Verzonden { get; set; }

    }
}
