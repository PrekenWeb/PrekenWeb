using System.Collections.Generic;
using System.Web.Routing;

namespace Prekenweb.Website.Lib
{
    public static class TaalInfoHelper
    {
        private static readonly string _defaultCulture = "nl";

        private static readonly Dictionary<string, WebsiteTaal> Talen;

        static TaalInfoHelper()
        {
            Talen = new Dictionary<string, WebsiteTaal>
            {
                ["nl"] = new WebsiteTaal("nl", 1),
                ["en"] = new WebsiteTaal("en", 2)
            };
        }

        public static WebsiteTaal FromRouteData(RouteData routeData)
        {
            var cultureRouteValue = (string)routeData?.Values["culture"] ?? _defaultCulture;

            return Talen[cultureRouteValue.ToLower()];
        }
    }
}