﻿using System.Web;
using System.Web.Optimization;

namespace ConsommiTounsi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/site.css"
                    ));


            bundles.Add(new StyleBundle("~/bundles/cssAdmin").Include(
                     "~/ContentAdmin/vendors/bootstrap/dist/css/bootstrap.min.css",
                     "~/ContentAdmin/vendors/font-awesome/css/font-awesome.min.css",
                     "~/ContentAdmin/vendors/nprogress/nprogress.css",
                     "~/ContentAdmin/vendors/bootstrap-daterangepicker/daterangepicker.css",
                     "~/ContentAdmin/vendors/iCheck/skins/flat/green.cs",
                     "~/ContentAdmin/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                     "~/ContentAdmin/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                     "~/ContentAdmin/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                     "~/ContentAdmin/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                     "~/ContentAdmin/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css",
                     "~/ContentAdmin/build/css/custom.min.css",
                     "~/ContentAdmin/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                     "~/ContentAdmin/vendors/pnotify/dist/pnotify.css",
                     "~/ContentAdmin/vendors/pnotify/dist/pnotify.buttons.css",
                     "~/ContentAdmin/vendors/google-code-prettify/bin/prettify.min.css",
                     "~/ContentAdmin/vendors/select2/dist/css/select2.min.css",
                     "~/ContentAdmin/vendors/switchery/dist/switchery.min.css",
                     "~/ContentAdmin/vendors /starrr/dist/starrr.css",
                     
                     "~/ContentAdmin/vendors/pnotify/dist/pnotify.nonblock.css"
                     
                    ));


                        bundles.Add(new ScriptBundle("~/bundles/jsAdmin").Include("~/ContentAdmin/vendors/jquery/dist/jquery.min.js",
                "~/ContentAdmin/vendors/bootstrap/dist/js/bootstrap.bundle.min.js",
                "~/ContentAdmin/vendors/fastclick/lib/fastclick.js",
                "~/ContentAdmin/vendors/nprogress/nprogress.js",
                "~/ContentAdmin/vendors/Chart.js/dist/Chart.min.js",
                "~/ContentAdmin/vendors/jquery-sparkline/dist/jquery.sparkline.min.js",
                "~/ContentAdmin/vendors/Flot/jquery.flot.js",
                "~/ContentAdmin/vendors/Flot/jquery.flot.pie.js",
                "~/ContentAdmin/vendors/Flot/jquery.flot.time.js",
                "~/ContentAdmin/vendors/Flot/jquery.flot.stack.js",
                "~/ContentAdmin/vendors/Flot/jquery.flot.resize.js",
                "~/ContentAdmin/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                "~/ContentAdmin/vendors/flot-spline/js/jquery.flot.spline.min.js",
                "~/ContentAdmin/vendors/flot.curvedlines/curvedLines.js",
                "~/ContentAdmin/vendors/DateJS/build/date.js",
                "~/ContentAdmin/vendors/moment/min/moment.min.js",
                "~/ContentAdmin/vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/ContentAdmin/build/js/custom.min.js",
                "~/ContentAdmin/vendors/iCheck/icheck.min.js",
                "~/ContentAdmin/vendors/datatables.net/js/jquery.dataTables.min.js",
                "~/ContentAdmin/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/ContentAdmin/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/ContentAdmin/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/ContentAdmin/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                "~/ContentAdmin/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                "~/ContentAdmin/vendors/datatables.net-buttons/js/buttons.print.min.js",
                "~/ContentAdmin/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                "~/ContentAdmin/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                "~/ContentAdmin/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                "~/ContentAdmin/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                "~/ContentAdmin/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                "~/ContentAdmin/vendors/jszip/dist/jszip.min.js",
                "~/ContentAdmin/vendors/pdfmake/build/pdfmake.min.js",
                "~/ContentAdmin/vendors/pdfmake/build/vfs_fonts.js"

                ));

                    
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
            
            bundles.Add(new ScriptBundle("~/bundles/js2").Include(
                "~/Content2/vendor/jquery/jquery-3.2.1.min.js",
               "~/Content2/vendor/animsition/js/animsition.min.js",
               "~/Content2/vendor/bootstrap/js/popper.js",
               "~/Content2/vendor/bootstrap/js/bootstrap.min.js",
               "~/Content2/vendor/select2/select2.min.js",
               "~/Content2/vendor/slick/slick.min.js",
               "~/Content2/js/slick-custom.js",
               "~/Content2/vendor/countdowntime/countdowntime.js",
               "~/Content2/vendor/lightbox2/js/lightbox.min.js",
               "~/Content2/vendor/sweetalert/sweetalert.min.js",
               "~/Content2/js/main.js",
               "~/Content2/vendor/tilt/tilt.jquery.min.js"


               ));
            bundles.Add(new StyleBundle("~/bundles/productcss").Include(
                "~/Content2/vendor/noui/nouislider.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/productjs").Include(
               "~/Content2/vendor/noui/nouislider.min.js",
               "~/Content2/vendor/daterangepicker/daterangepicker.js",
               "~/Content2/vendor/daterangepicker/moment.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/loginjs").Include(
               "~/ContentLogin/vendor/jquery/jquery-3.2.1.min.js",
               "~/ContentLogin/vendor/bootstrap/js/popper.js",
               "~/ContentLogin/vendor/bootstrap/js/bootstrap.min.js",
               "~/ContentLogin/vendor/select2/select2.min.js",
               "~/ContentLogin/vendor/tilt/tilt.jquery.min.js",
               "~/ContentLogin/js/main.js"

               ));
            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
               "~/ContentLogin/css/main.css",
               "~/ContentLogin/css/util.css",
               "~/ContentLogin/vendor/select2/select2.min.css",
               "~/ContentLogin/vendor/css-hamburgers/hamburgers.min.css",
               "~/ContentLogin/vendor/animate/animate.css",
               "~/ContentLogin/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
               "~/ContentLogin/vendor/bootstrap/css/bootstrap.min.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/registercss").Include(
                "~/ContentRegister/vendor/mdi-font/css/material-design-iconic-font.min.css",
                "~/ContentRegister/vendor/font-awesome-4.7/css/font-awesome.min.css",
                "~/ContentRegister/vendor/select2/select2.min.css",
                "~/ContentRegister/vendor/datepicker/daterangepicker.css",
                "~/ContentRegister/css/main.css"
               ));
            bundles.Add(new ScriptBundle("~/bundles/registerjs").Include(
                "~/ContentRegister/vendor/jquery/jquery.min.js",
                "~/ContentRegister/vendor/select2/select2.min.js",
                "~/ContentRegister/vendor/datepicker/moment.min.js",
                "~/ContentRegister/vendor/datepicker/daterangepicker.js",
                "~/ContentRegister/js/global.js"
               ));


        }
    }
}
