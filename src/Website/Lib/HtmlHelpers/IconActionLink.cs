using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {



        public static MvcHtmlString IconActionLink(this HtmlHelper helper, string linkTitle, string actionName, string controllerName, object routeValues, object htmlAttributes, string icon)
        {
            var iconSpan = new TagBuilder("span");
            iconSpan.MergeAttribute("class", string.Format("fa fa-{0}", icon));

            RouteValueDictionary htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            //if (htmlAttributesDictionary.Any(a => a.Key.ToLower() == "class")) htmlAttributesDictionary["class"] += " Knop IconKnop";
            //else htmlAttributesDictionary.Add("class", "IconKnop");

            var link = helper.ActionLink("{icon}", actionName, controllerName, new RouteValueDictionary(routeValues), htmlAttributesDictionary.ToDictionary(f => f.Key, f => f.Value));

            return new MvcHtmlString(link.ToString().Replace("{icon}", string.Format("{0} {1}", iconSpan, linkTitle)));
        }

    }
}