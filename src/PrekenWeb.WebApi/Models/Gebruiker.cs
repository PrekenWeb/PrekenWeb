using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Gebruiker
    {
        public Gebruiker()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            GebruikerMailing = new HashSet<GebruikerMailing>();
            Inbox = new HashSet<Inbox>();
            InboxOpvolging = new HashSet<InboxOpvolging>();
            PaginaAangemaaktDoorNavigation = new HashSet<Pagina>();
            PaginaBijgewerktDoorNavigation = new HashSet<Pagina>();
            PreekAangemaaktDoorNavigation = new HashSet<Preek>();
            PreekAangepastDoorNavigation = new HashSet<Preek>();
            PreekCookie = new HashSet<PreekCookie>();
            ZoekOpdracht = new HashSet<ZoekOpdracht>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public DateTime? LaatstIngelogd { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<GebruikerMailing> GebruikerMailing { get; set; }
        public virtual ICollection<Inbox> Inbox { get; set; }
        public virtual ICollection<InboxOpvolging> InboxOpvolging { get; set; }
        public virtual ICollection<Pagina> PaginaAangemaaktDoorNavigation { get; set; }
        public virtual ICollection<Pagina> PaginaBijgewerktDoorNavigation { get; set; }
        public virtual ICollection<Preek> PreekAangemaaktDoorNavigation { get; set; }
        public virtual ICollection<Preek> PreekAangepastDoorNavigation { get; set; }
        public virtual ICollection<PreekCookie> PreekCookie { get; set; }
        public virtual ICollection<ZoekOpdracht> ZoekOpdracht { get; set; }
    }
}
