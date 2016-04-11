using System.Web;
using System.Web.Mvc;

namespace Prekenweb.Website.Lib
{ 
    public class OutputCacheHelpers
    {
        public static void ClearOutputCaches(HttpResponseBase httpResponseBase, UrlHelper urlHelper)
        {
            if (urlHelper == null) return;

            httpResponseBase.RemoveOutputCacheItem(urlHelper.Action("Index", "Home", new { area = "Website" }));
            httpResponseBase.RemoveOutputCacheItem(urlHelper.Action("Index", "Zoeken", new { area = "Website" }));
            //PrekenwebContext.Spotlights.Select(sp => sp.AfbeeldingId).ToList().ForEach(afbeeldingId =>
            //    Response.RemoveOutputCacheItem(Url.Action("HomepageImage", "Home", new { area = "Website", Id = afbeeldingId }))
            //);
        }
    }
}
