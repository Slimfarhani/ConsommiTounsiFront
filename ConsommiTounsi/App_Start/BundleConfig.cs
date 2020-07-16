using System.Web;
using System.Web.Optimization;

namespace ConsommiTounsi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendors/nprogress/nprogress.css",
                      "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css",
                      "~/Content/build/css/custom.min.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Content/vendors/jquery/dist/jquery.min.js",
                "~/Content/vendors/bootstrap/dist/js/bootstrap.bundle.min.js",
                "~/Content/vendors/fastclick/lib/fastclick.js",
                "~/Content/vendors/nprogress/nprogress.js",
                "~/Content/vendors/Chart.js/dist/Chart.min.js",
                "~/Content/vendors/jquery-sparkline/dist/jquery.sparkline.min.js",
                "~/Content/vendors/Flot/jquery.flot.js",
                "~/Content/vendors/Flot/jquery.flot.pie.js",
                "~/Content/vendors/Flot/jquery.flot.time.js",
                "~/Content/vendors/Flot/jquery.flot.stack.js",
                "~/Content/vendors/Flot/jquery.flot.resize.js",
                "~/Content/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                "~/Content/vendors/flot-spline/js/jquery.flot.spline.min.js",
                "~/Content/vendors/flot.curvedlines/curvedLines.js",
                "~/Content/vendors/DateJS/build/date.js",
                "~/Content/vendors/moment/min/moment.min.js",
                "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/build/js/custom.min.js"

                ));


            bundles.Add(new StyleBundle("~/bundles/css2").Include(
                "~/Content2/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content2/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/Content2/fonts/themify/themify-icons.css",
                "~/Content2/fonts/Linearicons-Free-v1.0.0/icon-font.min.css",
                "~/Content2/fonts/elegant-font/html-css/style.css",
                "~/Content2/vendor/animate/animate.csss",
                "~/Content2/vendor/css-hamburgers/hamburgers.min.css",
                "~/Content2/vendor/animsition/css/animsition.min.css",
                "~/Content2/vendor/select2/select2.min.css",
                "~/Content2/vendor/daterangepicker/daterangepicker.css",
                "~/Content2/vendor/slick/slick.csss",
                "~/Content2/vendor/lightbox2/css/lightbox.min.css",
                "~/Content2/css/util.css",
                 "~/Content2/css/main.css"

            ));

            bundles.Add(new ScriptBundle("~/bundles/js2").Include("~/Content2/vendor/jquery/jquery-3.2.1.min.js",
               "~/Content2/vendor/animsition/js/animsition.min.js",
               "~/Content2/vendor/bootstrap/js/popper.js",
               "~/Content2/vendor/bootstrap/js/bootstrap.min.js",
               "~/Content2/vendor/select2/select2.min.js",
               "~/Content2/vendor/slick/slick.min.js",
               "~/Content2/js/slick-custom.js",
               "~/Content2/vendor/countdowntime/countdowntime.js",
               "~/Content2/vendor/lightbox2/js/lightbox.min.js",
               "~/Content2/vendor/sweetalert/sweetalert.min.js",
               "~/Content2/js/main.js"

               ));
        }
    }
}
