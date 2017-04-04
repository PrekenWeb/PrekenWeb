using System.Configuration;
using System.Web.Http;
using Data;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using PrekenWeb.Security;
using WebAPI;

[assembly: OwinStartup(typeof(Startup))]

namespace WebAPI
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration ?? new HttpConfiguration();

            app.CreatePerOwinContext(PrekenwebContext.Create);
            app.CreatePerOwinContext<PrekenWebUserManager>(PrekenWebUserManager.Create);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] {ConfigurationManager.AppSettings["PrekenWeb.Website.AudienceId"]},
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider("PrekenWeb.Website", TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["PrekenWeb.Website.AudienceSecret"]))
                }
            });

            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);

            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel).UseNinjectWebApi(config);

            app.UseWebApi(config);
        }
    }
}