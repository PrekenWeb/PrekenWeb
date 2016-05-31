using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;
using Ninject.Web.Common.OwinHost;
using Owin;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Security;
using Prekenweb.Website;
using Prekenweb.Website.Lib.Hangfire;

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
                //GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire-sqlserver");

                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    AuthorizationFilters = new IAuthorizationFilter[] { new LocalRequestsOnlyAuthorizationFilter() }
                });

                AchtergrondTaken.RegistreerTaken();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Need to register application for autostart, for more information visit: http://docs.hangfire.io/en/latest/deployment-to-production/making-aspnet-app-always-running.html#enabling-service-auto-start", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception("Problem connecting to HangFire database", ex);
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
