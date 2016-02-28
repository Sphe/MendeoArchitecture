using System.Web;
using System.Web.Optimization;

namespace Mendeo.Mercurius.Bootstrap
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.js",
                        "~/Scripts/jquery.utilities.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js",
                //"~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-animate.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular/addon").Include(
                
                "~/Scripts/breeze.debug.js",
                "~/Scripts/breeze.angular.js",
                "~/Scripts/breeze.directives.js",
                "~/Scripts/angular.breeze.storagewip.js",
                "~/Scripts/breeze.metadata-helper.js",

                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/ng-flow-standalone.js",

                "~/Scripts/textAngular/textAngular-rangy.js",
                "~/Scripts/textAngular/textAngular-sanitize.js",
                "~/Scripts/textAngular/textAngular.js",

                "~/Scripts/angular-re-captcha/angular-re-captcha.js",
                "~/Scripts/ivh-treeview/ivh-treeview.js",
                "~/Scripts/ng-tag-input/ng-tag-input.js",
                "~/Scripts/autocomplete-google-place/autocomplete.js",
                "~/Scripts/select/select.js",
                "~/Scripts/pagination/dirPagination.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/toastr.js",
                "~/Scripts/frexy/jquery.carouFredSel-6.2.1-packed.js",
                "~/Scripts/lodash.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.js",

                "~/app/config.js",
                "~/app/config.breeze.js",
                "~/app/config.exceptionHandler.js",
                "~/app/config.route.js",
                "~/app/environment.js",

                "~/app/core/core.js",
                "~/app/core/appActivitySvc.js",
                "~/app/core/appStatusSvc.js",
                "~/app/core/notifierSvc.js",
                "~/app/core/navigationSvc.js",
                "~/app/core/skDisableWhenBusy.js",
                "~/app/core/storageSvc.js",
                "~/app/core/skAppReady.js",

                "~/app/common/common.js",
                "~/app/common/logger.js",
                "~/app/common/spinner.js",
                "~/app/common/bootstrap/bootstrap.dialog.js",

                "~/app/common/validation/skMatches.js",
                "~/app/common/validation/skAsyncValidators.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/shell").Include(    
                "~/app/shell/shell.js",
                "~/app/shell/shellCtrl.js",
                "~/app/shell/topNavCtrl.js",
                "~/app/shell/skNavLinks.js",
                "~/app/shell/skBusyIndicator.js"                         
                ));

            bundles.Add(new ScriptBundle("~/bundles/security").Include(
                "~/app/security/security.js",
                "~/app/security/accountClientSvc.js",
                "~/app/security/signIn.js",
                "~/app/security/signInCtrl.js",
                "~/app/security/registerCtrl.js",
                "~/app/security/secureHttpInterceptor.js",
                "~/app/security/guardSvc.js",                
                "~/app/security/userSvc.js",
                "~/app/security/skUserInfo.js",
                "~/app/security/externalSignInCtrl.js",
                "~/app/security/externalRegisterCtrl.js",
                "~/app/security/externalAuthSvc.js",
                "~/app/security/restoreUserSvc.js",
                "~/app/security/skChangePassword.js",
                "~/app/security/skCreateLocalLogin.js",
                "~/app/security/skLoginProvider.js",
                "~/app/security/skUserLogin.js",
                "~/app/security/manageCtrl.js",
                "~/app/security/userManagementSvc.js",
                "~/app/security/usedLoginProviderFilter.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/services").Include(
                "~/app/services/routemediator.js",
                "~/app/services/datacontext.js",
                "~/app/services/entityManagerFactory.js",
                "~/app/services/model.js",
                "~/app/services/model.metadata.js",
                "~/app/services/model.validation.js",
                "~/app/services/helper.js",
                "~/app/services/directives.js",

                "~/app/services/repositories/repository.abstract.js",
                "~/app/services/repositories/repository.attendee.js",
                "~/app/services/repositories/repository.lookup.js",
                "~/app/services/repositories/repository.session.js",
                "~/app/services/repositories/repository.speaker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/content").Include(
                "~/app/content/welcome/welcomeCtrl.js",
                "~/app/content/features/featuresCtrl.js",
                "~/app/content/securedWebapiDemo/securedWebapiDemoCtrl.js",
                "~/app/content/wip/wip.js",
                "~/app/content/addProduct/addProduct.js",
                "~/app/content/categoryList/categoryList.js",
                "~/app/content/productView/productView.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/fadeAnimation.css",
                      "~/Scripts/textAngular/textAngular.css",
                      "~/Scripts/ng-tag-input/ng-tag-input.css",
                      "~/Scripts/autocomplete-google-place/autocomplete.css",
                      "~/Scripts/ivh-treeview/ivh-treeview.css",
                      "~/Scripts/ivh-treeview/ivh-treeview-theme.css",
                      "~/Scripts/select/select.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/frexy").Include(
                      "~/Scripts/frexy/bxslider/jquery.bxslider.css",
                      "~/Scripts/frexy/bxslider/radial-progress/style.css",
                       "~/Content/frexy/css/animate.css",
                      "~/Scripts/frexy/bootstrap-progressbar/bootstrap-progressbar-3.2.0.min.css",
                      "~/Scripts/frexy/jqueryui/jquery-ui.css",
                      "~/Scripts/frexy/jqueryui/jquery-ui.structure.css",
                      "~/Scripts/frexy/fancybox/jquery.fancybox.css",
                      "~/Content/frexy/fonts/fonts.css",
                      "~/Content/frexy/css/main-green.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
