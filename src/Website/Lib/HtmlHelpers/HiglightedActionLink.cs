using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {

        public static MvcHtmlString HiglightedActionLink(this HtmlHelper helper, string linkTitle, string actionName, string controllerName, object routeValues, object htmlAttributes, string textToHightlight)
        {
            var link = helper.ActionLink("{text}", actionName, controllerName, routeValues, htmlAttributes);

            return new MvcHtmlString(link.ToString().Replace("{text}", HighLight(helper, linkTitle, textToHightlight).ToString()));
        }



    }
}