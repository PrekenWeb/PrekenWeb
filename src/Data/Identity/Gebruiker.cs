using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Identity
{
    [MetadataType(typeof(GebruikerMetaData))]
    public partial class Gebruiker : IdentityUser<int, PrekenWebUserLogin, PrekenWebUserRole, PrekenWebUserClaim>//, IUser<int>
    { 
        public Gebruiker()
        {
            Inboxes = new List<Inbox>();
            InboxOpvolgings = new List<InboxOpvolging>();
            Paginas_AangemaaktDoor = new List<Pagina>();
            Paginas_BijgewerktDoor = new List<Pagina>();
            Preeks_AangemaaktDoor = new List<Preek>();
            Preeks_AangepastDoor = new List<Preek>();
            PreekCookies = new List<PreekCookie>();
            Mailings = new List<Mailing>(); 
        }
        
        [Display(Name = "Naam", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(AllowEmptyStrings = false)]
        public string Naam { get; set; }

        [Display(Name = "LaatstIngelogd", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public DateTime? LaatstIngelogd { get; set; }

        public virtual ICollection<Inbox> Inboxes { get; set; }
        public virtual ICollection<InboxOpvolging> InboxOpvolgings { get; set; }
        public virtual ICollection<Mailing> Mailings { get; set; }
        public virtual ICollection<Pagina> Paginas_AangemaaktDoor { get; set; }
        public virtual ICollection<Pagina> Paginas_BijgewerktDoor { get; set; }
        public virtual ICollection<Preek> Preeks_AangemaaktDoor { get; set; }
        public virtual ICollection<Preek> Preeks_AangepastDoor { get; set; }
        public virtual ICollection<PreekCookie> PreekCookies { get; set; }
        public virtual ICollection<ZoekOpdracht> ZoekOpdrachten { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Gebruiker, int> manager, string defaultAuthenticationTypes = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(this, defaultAuthenticationTypes);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class GebruikerMetaData
    {
        [DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Geen juist emailadres"), Display(Name = "Email", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Display(Name = "Gebruikersnaam", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
    }

}
