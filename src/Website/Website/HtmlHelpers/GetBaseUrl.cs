using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Prekenweb.Website.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification="Kreeg het niet voor elkaar met twee Uri objecten :(")]
        public static Uri GetBaseUrl(this UrlHelper url)
        {
            var uri = new Uri(
                url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl
            );
            var builder = new UriBuilder(uri)
            {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return builder.Uri;
        }

    }
}