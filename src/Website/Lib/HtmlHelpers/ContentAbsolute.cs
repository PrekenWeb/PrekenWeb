using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")]
        public static string ContentAbsolute(this UrlHelper url, string contentPath)
        {
            return new Uri(GetBaseUrl(url), url.Content(contentPath)).AbsoluteUri;
        } 
    }
}