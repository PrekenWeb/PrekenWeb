using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
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
                ["nl"] = new WebsiteTaal("prekenweb.nl", "nl", 1),
                ["en"] = new WebsiteTaal("sermonweb.org", "en", 2)
            };
        }

        public static WebsiteTaal FromRouteData(RouteData routeData, string hostName = null)
        {
            if (hostName == null && HttpContext.Current?.Request.Url != null)
            {
                hostName = HttpContext.Current?.Request.Url.GetLeftPart(UriPartial.Authority).ToLower();
            }

            if (string.IsNullOrWhiteSpace(routeData?.Values["culture"]?.ToString()) && hostName != null)
            {
                var taalInfoByHostname = Talen.Any(x => hostName.Contains(x.Value.Hostname));
                if (taalInfoByHostname) return Talen.First(x => hostName.Contains(x.Value.Hostname)).Value;
            }

            var cultureRouteValue = (string)routeData?.Values["culture"] ?? _defaultCulture;

            var taalInfo = Talen[cultureRouteValue.ToLower()];
            if (taalInfo == null && hostName != null) taalInfo = Talen.First(x => hostName.Contains(x.Value.Hostname)).Value;

            return taalInfo;
        }
    }
}