using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prekenweb.Website.Areas.Website
{
    public class WebsiteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Website";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "iTunesRouting",
                url: "{culture}/iTunes.xml",
                defaults: new { controller = "Home", action = "iTunesPodcast", }
                );

            context.MapRoute(
                name: "TekstPagina",
                url: "{culture}/pagina/{pagina}",
                defaults: new { controller = "Prekenweb", action = "Pagina" }
                );


            context.MapRoute(
                name: "MultiCultiRoute",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            context.MapRoute(
                name: "RootUrl",
               url: ""
            );//.RouteHandler = new RootUrlHandler();

        }
    }
}
