using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Prekenweb.Website.Lib
{
    public class AnonymousOnlyOutputCacheAttribute : OutputCacheAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                Location = OutputCacheLocation.None;
            }

            httpContext.Response.Cache.AddValidationCallback(IgnoreAuthenticated, null);

            base.OnResultExecuting(filterContext);
        }

        private void IgnoreAuthenticated(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            if (context.User.Identity.IsAuthenticated)
                validationStatus = HttpValidationStatus.IgnoreThisRequest;
            else
                validationStatus = HttpValidationStatus.Valid;
        }
    }
}