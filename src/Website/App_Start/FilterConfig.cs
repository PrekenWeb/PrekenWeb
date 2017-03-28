using System;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using Elmah;
using System.Web.Mvc;
using Prekenweb.Website.Lib;
using System.Linq;

namespace Prekenweb.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureFilter());
        }

        public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                base.OnException(context);
                if (!context.ExceptionHandled)
                    return;
                var httpContext = context.HttpContext.ApplicationInstance.Context;
                var signal = ErrorSignal.FromContext(httpContext);
                signal.Raise(context.Exception, httpContext);
            }
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