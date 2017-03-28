using System.Web.Optimization;

namespace Prekenweb.Website
{
    public class BundleConfig
    { 
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/bootstrap*",
                "~/Scripts/jquery.unobtrusive-ajax*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.jplayer.js",
                "~/Scripts/jquery.easing*",
                "~/Scripts/jquery.qtip*",
                "~/Scripts/lofslider*",
                "~/Scripts/jquery.ba-throttle-debounce*",
                "~/Scripts/jquery-ui-timepicker-addon*",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/jquery.cycle.js",
                "~/Scripts/require.js",
                "~/Scripts/main.js",
                "~/Scripts/css3-mediaqueries.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/normalize.css",
                "~/Content/bootstrap*",
                "~/Content/font-awesome*",
                "~/Content/jquery-ui-timepicker-addon.css",
                "~/Content/forms.css",
                "~/Content/jquery.qtip*",
                "~/Content/site.css",
                "~/Content/mediaQueries.css" ));
        }
    }
}