using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using PrekenWeb.Data.Identity;

namespace PrekenWeb.Security
{
    public class PrekenWebAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // no client validation yet
            context.Validated();
            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<PrekenWebUserManager>();

            Gebruiker gebruiker;
            try
            {
                gebruiker = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch
            {
                context.SetError("server_error");
                context.Rejected();
                return;
            }

            if (gebruiker == null)
            {
                // The resource owner credentials are invalid or resource owner does not exist.
                context.SetError("access_denied", "The resource owner credentials are invalid or resource owner does not exist.");
                context.Rejected();
                return;
            }

            try
            {
                var identity = await userManager.CreateIdentityAsync(gebruiker, DefaultAuthenticationTypes.ExternalBearer);
                context.Validated(identity);
            }
            catch
            {
                context.SetError("server_error");
                context.Rejected();
            }
        }
    }
}
