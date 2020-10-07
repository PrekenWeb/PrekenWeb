using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public int UserId { get; set; }
        public int? GebruikerId { get; set; }

        public virtual Gebruiker Gebruiker { get; set; }
    }
}
