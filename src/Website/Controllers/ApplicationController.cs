using System.Web.Mvc;

namespace Prekenweb.Website.Controllers
{ 
    public class ApplicationController : Controller
    { 
        public ApplicationController()
        {
            ViewBag.TaalId = TaalId;
        }  

        #region Taal
        public string Taal
        {
            get
            {
                if (RouteData == null) return string.Empty;
                return (string)RouteData.Values["culture"];
            }
            set { }
        }

        public int TaalId
        {
            get
            {
                switch (Taal)
                {
                    case "en":
                        return 2;
                    case "nl":
                    default:
                        return 1;
                }
            }
            set { }
        }

        public string TaalCode
        {
            get
            {
                switch (Taal)
                {
                    case "en":
                        return "en-us";
                    case "nl":
                    default:
                        return "nl-nl";
                }
            }
            set { }
        }
        #endregion 

        public void ClearOutputCaches()
        {
            Response.RemoveOutputCacheItem(Url.Action("Index", "Home", new { area = "Website" }));
            Response.RemoveOutputCacheItem(Url.Action("Index", "Zoeken", new { area = "Website" }));
            //PrekenwebContext.Spotlights.Select(sp => sp.AfbeeldingId).ToList().ForEach(afbeeldingId =>
            //    Response.RemoveOutputCacheItem(Url.Action("HomepageImage", "Home", new { area = "Website", Id = afbeeldingId }))
            //); 
        }
    } 
}
