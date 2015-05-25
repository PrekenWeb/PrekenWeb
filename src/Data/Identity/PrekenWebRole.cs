using Microsoft.AspNet.Identity.EntityFramework;

namespace PrekenWeb.Data.Identity
{
    public class PrekenWebRole : IdentityRole<int, PrekenWebUserRole>
    {
        public PrekenWebRole() { }
        public PrekenWebRole(string name) { Name = name; }
    }
}
