using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PrekenWeb.Data.Attributes;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class ExterneLoginBevestigingViewModel
    {
        [Required] 
        [EmailAddress(ErrorMessage = "Geen juist emailadres")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources)), Tooltip("RegistreerEmailTooltip", ResourceType = typeof(Resources.Resources))]
        [Remote("EmailVrij", "Gebruiker")]
        public string Email { get; set; }

        [Required]
        [ Display(Name = "Gebruikersnaam", ResourceType = typeof(Resources.Resources)), Tooltip("RegistreerGebruikersnaamTooltip", ResourceType = typeof(Resources.Resources))]
        [Remote("GebruikersnaamVrij", "Gebruiker")]
        public string Naam { get; set; }

        public string ReturnUrl { get; set; }


    }
}