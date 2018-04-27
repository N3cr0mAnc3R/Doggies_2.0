using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WepApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                "~/External/Scripts/angular.js",
                "~/External/Scripts/angular-aria.js",
                "~/External/Scripts/angular-route.js",
                "~/External/Scripts/angular-messages.js",
                "~/External/Scripts/angular-message-format.js",
                "~/External/Scripts/angular-locale_ru-ru.js",
                "~/External/Scripts/jquery-3.3.1.js",
                "~/External/Scripts/openapi.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                    "~/Scripts/app.js"
                )
                .IncludeDirectory(
                "~/Scripts/Controllers", "*.js", true
               )
               .IncludeDirectory("~/Scripts/Directives", "*.js", true)
               .IncludeDirectory(
                "~/Scripts/Modules", "*.js", true
               ));

            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory(
                "~/External/Styles", "*.css", true
               )
               .Include("~/Content/Styles.css"));
        }
    }
}