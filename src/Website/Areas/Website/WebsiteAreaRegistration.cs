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
                defaults: new { culture= string.Empty, controller = "Home", action = "iTunesPodcast", }
                );

            context.MapRoute(
                name: "TekstPagina",
                url: "{culture}/pagina/{pagina}",
                defaults: new { culture = string.Empty, controller = "Prekenweb", action = "Pagina" }
                );


            context.MapRoute(
                name: "MultiCultiRoute",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { culture = string.Empty, controller = "Home", action = "Index", id = UrlParameter.Optional }
                ).RouteHandler = new RootUrlHandler();

            //context.MapRoute(
            //    name: "RootUrl",
            //    url: "",
            //    defaults: new { culture = string.Empty, controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

        }
    }
    public class RootUrlHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var culturePartInUrl = requestContext.RouteData.Values["culture"]?.ToString();
            if (!string.IsNullOrWhiteSpace(culturePartInUrl)) return base.GetHttpHandler(requestContext);

            if (requestContext.HttpContext.Request.Url.Host.EndsWith("prekenweb.nl")) requestContext.HttpContext.Response.Redirect(string.Format("http://{0}/nl/", requestContext.HttpContext.Request.Url.Host), true);
            else if (requestContext.HttpContext.Request.Url.Host.EndsWith("sermonweb.org")) requestContext.HttpContext.Response.Redirect(string.Format("http://{0}/en/", requestContext.HttpContext.Request.Url.Host), true);
            else requestContext.HttpContext.Response.Redirect(requestContext.HttpContext.Request.Url + "/nl/", true);
            return base.GetHttpHandler(requestContext);
        }
    }
}
