using Prekenweb.Attributes;
using Prekenweb.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class RegistreerViewModel
    {
        [Required, Display(Name = "VolledigeNaam", ResourceType = typeof(Resources.Resources))]
        public string Naam { get; set; }

        [Required, Display(Name = "Gebruikersnaam", ResourceType = typeof(Resources.Resources)), Tooltip("RegistreerGebruikersnaamTooltip", ResourceType = typeof(Resources.Resources))]
        [Remote("GebruikersnaamVrij", "Gebruiker")]
        public string Gebruikersnaam { get; set; }

        [Required, DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Geen juist emailadres"), Display(Name = "Email", ResourceType = typeof(Resources.Resources)), Tooltip("RegistreerEmailTooltip", ResourceType = typeof(Resources.Resources))]
        [Remote("EmailVrij", "Gebruiker")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false), DataType(DataType.Password), Display(Name = "Wachtwoord", ResourceType = typeof(Resources.Resources)), MinLength(5)]
        public string Wachtwoord { get; set; }

        public TekstPagina TekstPagina { get; set; }
    }
}