using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;
using Ninject.Web.Common.OwinHost;
using Owin;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using PrekenWeb.Security;
using Prekenweb.Website;
using Prekenweb.Website.Properties;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace Prekenweb.Website
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel);
            app.CreatePerOwinContext(PrekenwebContext.Create); // done via Ninject
            app.CreatePerOwinContext<PrekenWebUserManager>(PrekenWebUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/nl/Mijn/Gebruiker/Inloggen"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<PrekenWebUserManager, Gebruiker, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (Int32.Parse(id.GetUserId())))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie); 

            var twitterOptions = new TwitterAuthenticationOptions
            {
                ConsumerKey = Settings.Default.TwitterCustomerKey,
                ConsumerSecret = Settings.Default.TwitterCustomerSecret,
                Provider = new TwitterAuthenticationProvider()
                {  
                    OnAuthenticated = OnAuthenticated
                }
            };
            app.UseTwitterAuthentication(twitterOptions);

            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = Settings.Default.FacebookAppId,
                AppSecret = Settings.Default.FacebookAppSecret
            };
            facebookOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookOptions); 

        }

        private Task OnAuthenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstoken", context.AccessToken));
            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstokensecret", context.AccessTokenSecret));
            return null;
        }
    } 
}
