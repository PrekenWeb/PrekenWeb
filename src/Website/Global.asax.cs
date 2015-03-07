using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Prekenweb.Website
{
    public class PrekenwebWebsite : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); 
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            switch (arg)
            {
                case "userName":
                    return context.User.Identity.Name;
            }
            return string.Empty;
        }
    }
}