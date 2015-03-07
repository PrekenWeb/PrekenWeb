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
            //context.MapRoute(name: "DefaultCaptchaRoute", url: "{culture}/DefaultCaptcha/Generate", defaults: new { controller = "DefaultCaptcha", action = "Generate", area="" });
            context.MapRoute(
               name: "iTunesRouting",
               url: "{culture}/iTunes.xml",
               defaults: new { controller = "Home", action = "iTunesPodcast", },
               constraints: new { culture = new CultureDomainConstraint(Enum.GetNames(typeof(Culture))) }
           ).RouteHandler = new MultiCultureMvcRouteHandler();

            context.MapRoute(
               name: "iTunesRoutingIncorrectDomain",
               url: "{culture}/iTunes.xml",
               defaults: new { controller = "Home", action = "iTunesPodcast" },
               constraints: new { culture = new CultureConstraint(Enum.GetNames(typeof(Culture))) }
           ).RouteHandler = new MultiCultureIncorrectDomainMvcRouteHandler();

            context.MapRoute(
                name: "TekstPagina",
                url: "{culture}/pagina/{pagina}",
                defaults: new { controller = "Prekenweb", action = "Pagina" },
                constraints: new { culture = new CultureDomainConstraint(Enum.GetNames(typeof(Culture))) }
            ).RouteHandler = new MultiCultureMvcRouteHandler();

            context.MapRoute(
                name: "TekstPaginaIncorrectDomain",
                url: "{culture}/pagina/{pagina}",
                defaults: new { controller = "Prekenweb", action = "Pagina" },
                constraints: new { culture = new CultureConstraint(Enum.GetNames(typeof(Culture))) }
            ).RouteHandler = new MultiCultureIncorrectDomainMvcRouteHandler();


            context.MapRoute(
                name: "MultiCultiRoute",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = new CultureDomainConstraint(Enum.GetNames(typeof(Culture))) }
            ).RouteHandler = new MultiCultureMvcRouteHandler();

            context.MapRoute(
                name: "MultiCultiRouteIncorrectDomain",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = new CultureConstraint(Enum.GetNames(typeof(Culture))) }
            ).RouteHandler = new MultiCultureIncorrectDomainMvcRouteHandler();

            //context.MapRoute(
            //    "Website_default",
            //    "Website/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                name: "RootUrl",
                //defaults: new { controller = "Home", action = "Index" },
               url: ""
            ).RouteHandler = new RootUrlHandler();

        }
    }

    public class RootUrlHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.Url.Host.EndsWith("localhost")) requestContext.HttpContext.Response.Redirect( requestContext.HttpContext.Request.Url.ToString() + "/nl/" , true);
            if (requestContext.HttpContext.Request.Url.Host.EndsWith("prekenweb.nl")) requestContext.HttpContext.Response.Redirect(string.Format("http://{0}/nl/", requestContext.HttpContext.Request.Url.Host), true);
            if (requestContext.HttpContext.Request.Url.Host.EndsWith("sermonweb.org")) requestContext.HttpContext.Response.Redirect(string.Format("http://{0}/en/", requestContext.HttpContext.Request.Url.Host), true);
            return base.GetHttpHandler(requestContext);
        }
    }

    public class MultiCultureMvcRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var culture = requestContext.RouteData.Values["culture"].ToString();
            var ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

            return base.GetHttpHandler(requestContext);
        }
    }

    public class MultiCultureIncorrectDomainMvcRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var culture = requestContext.RouteData.Values["culture"].ToString();
            var ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            if (!requestContext.HttpContext.Request.Url.Host.EndsWith("localhost"))
            {
                switch (culture)
                {
                    case "nl":
                        requestContext.HttpContext.Response.RedirectPermanent(requestContext.HttpContext.Request.Url.AbsoluteUri.Replace("sermonweb.org", "prekenweb.nl"), true);
                        break;
                    case "en":
                        requestContext.HttpContext.Response.RedirectPermanent(requestContext.HttpContext.Request.Url.AbsoluteUri.Replace("prekenweb.nl", "sermonweb.org"), true);
                        break;
                }
            }
            return base.GetHttpHandler(requestContext);
        }
    }
    public class CultureConstraint : IRouteConstraint
    {
        private string[] _values;
        public CultureConstraint(params string[] values)
        {
            _values = values;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = values[parameterName].ToString();
            return _values.Contains(value);
        }
    }

    public class CultureDomainConstraint : IRouteConstraint
    {
        private string[] _values;
        public CultureDomainConstraint(params string[] values)
        {
            _values = values;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Get the value called "parameterName" from the 
            // RouteValueDictionary called "value"
            string value = values[parameterName].ToString();
            // Return true is the list of allowed values contains 
            // this value.
            //return _values.Contains(value);
            if (httpContext.Request.Url.Host.EndsWith("localhost")) return _values.Contains(value);

            switch (value)
            {
                case "nl":
                    return httpContext.Request.Url.Host.EndsWith("prekenweb.nl");
                case "en":
                    return httpContext.Request.Url.Host.EndsWith("sermonweb.org");
                default:
                    return _values.Contains(value);
            }
        }
    }
    public enum Culture
    {
        unknown = 0,
        nl = 1,
        en = 2
    }
}
