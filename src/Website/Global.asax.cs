using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Prekenweb.Website.Hangfire;

namespace Prekenweb.Website
{
    public class PrekenwebWebsite : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HangfireBootstrapper.Instance.Start();

        }

        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "user")
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    return context.User.Identity.Name;
                }
                else
                {
                    return "";
                }
            }

            return base.GetVaryByCustomString(context, custom);
        }
    }
}