using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prekenweb.Website
{
    using CaptchaMvc.Controllers;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Administrator/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            ////routes.MapRoute("CaptchaRefresh", "DefaultCaptcha/Refresh", new { controller = "DefaultCaptcha", action = "Refresh" });
            ////routes.MapRoute("CaptchaImage", "DefaultCaptcha/Generate", new { controller = "DefaultCaptcha", action = "Generate" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );

            //var defaultCaptchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            //defaultCaptchaManager.ImageUrlFactory = (helper, pair) => ImageUrlFactory(defaultCaptchaManager, helper, pair);
            //defaultCaptchaManager.RefreshUrlFactory = RefreshUrlFactory; 

            routes.MapMvcAttributeRoutes();
        }

        //private static string RefreshUrlFactory(UrlHelper urlHelper, KeyValuePair<string, ICaptchaValue> keyValuePair)
        //{
        //    return urlHelper.RouteUrl("CaptchaRefresh");
        //}

        //private static string ImageUrlFactory(DefaultCaptchaManager captchaManager, UrlHelper urlHelper, KeyValuePair<string, ICaptchaValue> keyValuePair)
        //{
        //    return urlHelper.RouteUrl("CaptchaImage", new RouteValueDictionary { { captchaManager.TokenParameterName, keyValuePair.Key } });
        //} 
    } 
}