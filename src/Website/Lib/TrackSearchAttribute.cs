using System.Web.Mvc;

namespace Prekenweb.Website.Lib
{
    public class TrackSearchAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.HttpContext.Session != null)
                filterContext.HttpContext.Session["ZoekParameters"] = filterContext.HttpContext.Request.QueryString;
        }
    }
}