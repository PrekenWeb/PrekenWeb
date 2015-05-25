using System.ComponentModel.DataAnnotations;
using PrekenWeb.Data.ViewModels;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class ResetWachtwoordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Gebruikersnaam", ResourceType = typeof(Resources.Resources))]
        public string Gebruikersnaam { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord", ResourceType = typeof(Resources.Resources))]
        public string Wachtwoord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "WachtwoordCheck", ResourceType = typeof(Resources.Resources))]
        [Compare("Wachtwoord", ErrorMessage = "The password and confirmation password do not match.")]
        public string WachtwoordCheck { get; set; }

        public string Code { get; set; }

        public TekstPagina TekstPagina { get; set; }
    }
}