using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace Prekenweb.Website.Identity
{
    public class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, int? gebruikerId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            GebruikerId = gebruikerId;
        }

        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public int? GebruikerId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (GebruikerId.HasValue)
            {
                properties.Dictionary["XsrfId"] = string.Format("{0}", GebruikerId.Value);
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}