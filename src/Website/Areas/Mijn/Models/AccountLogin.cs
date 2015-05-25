using System.Diagnostics.CodeAnalysis;
using PrekenWeb.Data.Attributes;
using System.ComponentModel.DataAnnotations;
using PrekenWeb.Data.ViewModels;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class AccountInloggen
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "GebruikersnaamVerplicht", ErrorMessageResourceType = typeof(Resources.Resources)), Display(Name = "Gebruikersnaam", ResourceType = typeof(Resources.Resources)), Tooltip(Name = "TooltipGebruikersnaam", ResourceType = typeof(Resources.Resources))]
        public string Gebruikersnaam { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "WachtwoordVerplicht", ErrorMessageResourceType = typeof(Resources.Resources)), DataType(DataType.Password), Display(Name = "Wachtwoord", ResourceType = typeof(Resources.Resources))]
        public string Wachtwoord { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }

        [Display(Name = "Onthouden", ResourceType = typeof(Resources.Resources))]
        public bool Onthouden { get; set; }

        public TekstPagina TekstPagina { get; set; }

        public bool SocialLogin { get; set; }
    }
}