using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        { 
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes()); 

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            var trustedHostNames = new[]
            {
                "http://prekenweb.nl",
                "http://sermonweb.org",
                "http://www.sermonweb.org",
                "http://www.prekenweb.nl",
                "http://test.prekenweb.nl"
            };

            config.EnableCors(new EnableCorsAttribute(string.Join(",", trustedHostNames), "*", "GET, POST, OPTIONS, PUT, DELETE"));
        }
    }
}