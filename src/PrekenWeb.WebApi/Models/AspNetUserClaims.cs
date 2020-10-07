using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class AspNetUserClaims
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int? GebruikerId { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
    }
}
