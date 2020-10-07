using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class AspNetUserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? GebruikerId { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
        public virtual AspNetRoles Role { get; set; }
    }
}
