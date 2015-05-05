using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Prekenweb.Website.Lib.Hangfire;

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

        public void Application_Error(Object sender, EventArgs e)
        {
            //Do not OutputCache pages with an exception
            Response.Cache.AddValidationCallback(DontCacheCurrentResponse, null);
        }

        private void DontCacheCurrentResponse(HttpContext context, Object data, ref HttpValidationStatus status)
        {
            status = HttpValidationStatus.IgnoreThisRequest;
        }
    }
}