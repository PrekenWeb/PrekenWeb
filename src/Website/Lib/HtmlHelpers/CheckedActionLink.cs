using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    { 
        public static string CheckedActionLink(this AjaxHelper helper, string linkTitle, string actionName, object routeValues, AjaxOptions ajaxOptions, bool @checked)
        {
            //<span class="fa-check-empty"></span><span class="fa-check"></span>
            var uncheckedSpan = new TagBuilder("span");
            uncheckedSpan.MergeAttribute("class", "fa fa-square-o");
            var checkedSpan = new TagBuilder("span");
            checkedSpan.MergeAttribute("class", "fa fa-check-square-o");
            var link = helper.ActionLink("blaat", actionName, routeValues, ajaxOptions, new { @class = @checked ? "checked" : "unchecked" });
            return link.ToString().Replace("blaat", string.Format("{0}{1}{2}", uncheckedSpan, checkedSpan, linkTitle));
        }

    }
}