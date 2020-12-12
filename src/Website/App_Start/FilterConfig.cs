namespace Prekenweb.Website
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;

    using Prekenweb.Website.Lib;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CultureFilter());
        }

        public class CultureFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                var hostName = filterContext.HttpContext.Request.Url?.GetLeftPart(UriPartial.Authority).ToLower();
                var taalInfo = TaalInfoHelper.FromRouteData(filterContext.RouteData, hostName);

                if (hostName != null && !taalInfo.Hostnames.Any(hn => hostName.Contains(hn.ToLower()))) filterContext.Result = new HttpNotFoundResult();

                Thread.CurrentThread.CurrentCulture = taalInfo.CultureInfo;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(taalInfo.CultureInfo.Name);
            }
        }
    }
}