using System.Collections.Specialized;
using System.Web.Routing;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection collection)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.AllKeys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                routeValueDictionary.Add(key, collection[key]);
            }
            return routeValueDictionary;
        }
    }
}