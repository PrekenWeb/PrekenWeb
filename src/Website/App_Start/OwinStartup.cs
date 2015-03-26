using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
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
using Prekenweb.Website.Hangfire;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace Prekenweb.Website
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel);
            app.CreatePerOwinContext(PrekenwebContext.Create);
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
                ConsumerKey = ConfigurationManager.AppSettings["TwitterCustomerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["TwitterCustomerSecret"],
                Provider = new TwitterAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticated
                }
            };
            app.UseTwitterAuthentication(twitterOptions);

            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = ConfigurationManager.AppSettings["FacebookAppId"],
                AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"]
            };
            facebookOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookOptions);

            try
            {
                app.UseHangfire(config =>
                {
                    config.UseSqlServerStorage("hangfire-sqlserver");
                    config.UseServer();
                    config.UseAuthorizationFilters(new IAuthorizationFilter[] { new LocalRequestsOnlyAuthorizationFilter() });
                });
                AchtergrondTaken.RegistreerTaken();
            }
            catch (SqlException ex)
            {
                // probably wrong db-connection or non-existing db, let DbContext handle this 
            }
        }

        private Task OnAuthenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstoken", context.AccessToken));
            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstokensecret", context.AccessTokenSecret));
            return null;
        }
    }
}
