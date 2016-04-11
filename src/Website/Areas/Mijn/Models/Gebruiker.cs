using PrekenWeb.Data;
using PrekenWeb.Data.Attributes;
using PrekenWeb.Data.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class GebruikerIndexViewModel
    {
        [Display(Name = "AlleenBeheerders", ResourceType = typeof(Resources.Resources))]
        public bool AlleenBeheerders { get; set; }

        [Display(Name = "Zoekterm", ResourceType = typeof(Resources.Resources))]
        public string Zoekterm { get; set; }

        public List<Gebruiker> Gebruikers { get; set; }
    }
    public class GebruikerEditViewModel
    {
        public Gebruiker Gebruiker { get; set; }
        public string[] SelectedRoles { get; set; }

        [Display(Name = "SelectedNieuwsbrieven", ResourceType = typeof(Resources.Resources)), Tooltip(Name = "SelectedNieuwsbrievenTooltip", ResourceType = typeof(Resources.Resources))]
        public int[] SelectedNieuwsbrieven { get; set; }

        [Display(Name = "WachtwoordAanpassen", ResourceType = typeof(Resources.Resources))]
        public bool WachtwoordAanpassen { get; set; }

        [DataType(DataType.Password), Display(Name = "HuidigWachtwoord", ResourceType = typeof(Resources.Resources))]
        public string HuidigWachtwoord { get; set; }

        [DataType(DataType.Password), Display(Name = "Wachtwoord", ResourceType = typeof(Resources.Resources))]
        public string Wachtwoord { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Wachtwoord", ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password), Display(Name = "WachtwoordCheck", ResourceType = typeof(Resources.Resources)), Required(AllowEmptyStrings = false)]
        public string WachtwoordCheck { get; set; }

        public static IEnumerable<SelectListItem> GetAllRoles(string[] selectedRoles)
        {
            List<SelectListItem> returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = false,
                Text = "",
                Value = ""
            });
            using (PrekenwebContext context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Roles.ToList().OrderBy(p => p.Name).Select(r =>
                    new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Name,
                        Selected = selectedRoles != null && selectedRoles.Any(rol => rol == r.Name)
                    }));
            }

            return returnValues;
        }

        public IList<UserLoginInfo> Logins { get; set; }
    }
}
