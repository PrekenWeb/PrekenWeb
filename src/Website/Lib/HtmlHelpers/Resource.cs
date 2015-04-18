using System;
using System.Resources;
using System.Web.Mvc;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {

        public static MvcHtmlString Resource<T>(this HtmlHelper<T> html, string key)
        {
            var resourceManager = new ResourceManager(typeof(Resources.Resources));

            var val = resourceManager.GetString(key.Replace(" ", ""));

            // if value is not found return the key itself
            return MvcHtmlString.Create(String.IsNullOrEmpty(val) ? key : val);
        }

    }
}