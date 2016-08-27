using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn
{
    public class MijnAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mijn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            context.Routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            context.MapRoute(
                "Mijn_default",
                "{culture}/Mijn/{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}
