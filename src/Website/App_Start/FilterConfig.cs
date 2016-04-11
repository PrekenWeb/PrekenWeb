using System.Globalization;
using System.Threading;
using Elmah;
using System.Web.Mvc;
using Prekenweb.Website.Lib;

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
                var taalInfo = TaalInfoHelper.FromRouteData(filterContext.RouteData);

                Thread.CurrentThread.CurrentCulture = taalInfo.CultureInfo;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(taalInfo.CultureInfo.Name);
            }
        }
    }
}