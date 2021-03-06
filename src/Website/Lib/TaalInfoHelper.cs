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
                ["nl"] = new WebsiteTaal(new[]
                {
                    "prekenweb.nl"
#if DEBUG
                    ,"localhost", "http://192.168.", "http://10."
#endif
                }, "nl", 1),
                ["en"] = new WebsiteTaal(new[]
                {
                    "sermonweb.org"
#if DEBUG
                    ,"localhost", "http://192.168.", "http://10."
#endif

                }, "en", 2)
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
                var taalInfoByHostname = Talen.Any(x => x.Value.Hostnames.Any(y => hostName.Contains(y.ToLower())));
                if (taalInfoByHostname) return Talen.First(x => x.Value.Hostnames.Any(y => hostName.Contains(y.ToLower()))).Value;
            }

            var cultureRouteValue = (string)routeData?.Values["culture"] ?? _defaultCulture;


            if (!Talen.ContainsKey(cultureRouteValue.ToLower())) cultureRouteValue = _defaultCulture;

            var taalInfo = Talen[cultureRouteValue.ToLower()];
            if (taalInfo == null && hostName != null) taalInfo = Talen.First(x => x.Value.Hostnames.Any(y => hostName.Contains(y.ToLower()))).Value;

            return taalInfo;
        }
    }
}