using Elmah;
using System.Web.Mvc;

namespace Prekenweb.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
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
    }
}