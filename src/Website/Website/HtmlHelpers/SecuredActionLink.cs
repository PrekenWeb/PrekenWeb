using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Prekenweb.Website.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString SecuredActionLink(this HtmlHelper htmlHelper, string linkName, string actionName, string controllerName, object routeValues, object htmlAttributes, string rol, string icon = "")
        {
            if (!htmlHelper.ViewContext.HttpContext.Request.IsAuthenticated) return null;
            //HttpCookie cookie = htmlHelper.ViewContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (cookie != null)
            //{

            //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (rol.Split(',').Any(r => htmlHelper.ViewContext.HttpContext.User.IsInRole(r)))
                if (string.IsNullOrEmpty(icon))
                {
                    return htmlHelper.ActionLink(linkName, actionName, controllerName, routeValues, htmlAttributes);
                }
                else
                {
                    return htmlHelper.IconActionLink(linkName, actionName, controllerName, routeValues, htmlAttributes, icon);
                }
            //}
            return null;
        }
        public static MvcHtmlString SecuredActionLinkLI(this HtmlHelper htmlHelper, string linkName, string actionName, string controllerName, object routeValues, object htmlAttributes, string rol, string icon = "")
        {

            var defaultLink = SecuredActionLink(htmlHelper, linkName, actionName, controllerName, routeValues, htmlAttributes, rol, icon);
            return defaultLink == null ? null : MvcHtmlString.Create("<li>" + defaultLink + "</li>");
        }
        public static MvcHtmlString SecuredActionLinkOption(this HtmlHelper htmlHelper, string linkName, string actionName, string controllerName, object routeValues, object htmlAttributes, string rol)
        { 
            var defaultLink = SecuredActionLink(htmlHelper, linkName, actionName, controllerName, routeValues, htmlAttributes, rol);
            return defaultLink == null ? null : MvcHtmlString.Create("<Option value='" + defaultLink + "'>" + linkName + "</Option>");
        }

         
    }
}