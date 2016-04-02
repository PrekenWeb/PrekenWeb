using System;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Prekenweb.Website.Lib.Hangfire;
using Prekenweb.Website.Areas.Website;
using Prekenweb.Website.Areas.Mijn;
using PrekenWeb.Data;

namespace Prekenweb.Website
{
    public class PrekenwebWebsite : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

            // AreaRegistration.RegisterAllAreas(); // disabled automatic area registration due to unreliable ordering which results in routing problems
            Utils.RegisterArea<MijnAreaRegistration>(RouteTable.Routes, null);
            Utils.RegisterArea<WebsiteAreaRegistration>(RouteTable.Routes, null);

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

    public static class Utils
    {
        public static void RegisterArea<T>(RouteCollection routes, object state) where T : AreaRegistration
        {
            AreaRegistration registration = (AreaRegistration)Activator.CreateInstance(typeof(T));
            AreaRegistrationContext context = new AreaRegistrationContext(registration.AreaName, routes, state);
            string tNamespace = registration.GetType().Namespace;
            if (tNamespace != null)
            {
                context.Namespaces.Add(tNamespace + ".*");
            }
            registration.RegisterArea(context);
        }
    }
}