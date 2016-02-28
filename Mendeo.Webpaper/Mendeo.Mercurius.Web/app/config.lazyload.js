// lazyload config

angular.module('app')
    /**
   * jQuery plugin config use ui-jq directive , config the js and css files that required
   * key: function name of the jQuery plugin
   * value: array of the css js file located
   */
  .constant('JQ_CONFIG', {
      moment: ['../Scripts/moment.js'],
      fontawesome_marker: ['../Scripts/fontawesome-marker.min.js'],
      bxslider: ['../Scripts/frexy/bxslider/jquery.bxslider.js',
                    '../Scripts/frexy/bxslider/jquery.bxslider.css'],
      rs: ['../scripts/frexy/rs-plugin/js/jquery.themepunch.tools.js',
                    '../scripts/frexy/rs-plugin/js/jquery.themepunch.revolution.js',
                    '../Scripts/frexy/rs-plugin/css/settings.css'],
      markerwithlabel: ['../Scripts/frexy/markerwithlabel.js'],
      breakpoints: ['../Scripts/frexy/breakpoints.js'],
      jquery_ui: ['../Scripts/frexy/jqueryui/jquery-ui.js',
                    '../Scripts/frexy/jqueryui/jquery-ui.css',
                    '../Scripts/frexy/jqueryui/jquery-ui.structure.css'],


  }
  )
  // oclazyload config
  .config(['$ocLazyLoadProvider', function($ocLazyLoadProvider) {
      // We configure ocLazyLoad to use the lib script.js as the async loader
      $ocLazyLoadProvider.config({
          debug:  false,
          events: true,
          modules: [
              {
                  name: 'material',
                  files: [
                      '../Scripts/angular-aria/angular-aria.js',
                      '../Scripts/angular-material/angular-material.js',
                      '../Scripts/angular-material/angular-material.css'
                  ]
              },
              {
                  name: 'ui_bootstrap_tpls',
                  files: [
                      '../Scripts/angular-ui/ui-bootstrap-tpls.js'
                  ]
              },
              {
                  name: 'flow',
                  files: [
                      '../Scripts/ng-flow-standalone.js'
                  ]
              },
              {
                  name: 'textAngular',
                  files: [
                    '../Scripts/textAngular/textAngular-rangy.js',
                    '../Scripts/textAngular/textAngular.js',
                    '../Scripts/textAngular/textAngular.css'
              ]
              },
              {
                  name: 'captcha',
                  files: ['../Scripts/angular-re-captcha/angular-re-captcha.js']
              },
              {
                  name: 'scroll',
                  files: [
                      '../Scripts/angular-scroll/angular-scroll.js'
                  ]
              },
              {
                  name: 'treeview',
                  files: [
                      '../Scripts/ivh-treeview/ivh-treeview.js',
                      '../Scripts/ivh-treeview/ivh-treeview.css',
                      '../Scripts/ivh-treeview/ivh-treeview-theme.css'
                  ]
              },
              {
                  name: 'taginput',
                  files: [
                      '../Scripts/ng-tag-input/ng-tag-input.js',
                      '../Scripts/ng-tag-input/ng-tag-input.css'
                  ]
              },
              {
                  name: 'myModalAddTypeCtrl',
                  files: [
                      '../Scripts/ng-tag-input/myModalAddTypeCtrl.js'
                  ]
              },
              {
                  name: 'autocomplete',
                  files: [
                      '../Scripts/autocomplete-google-place/autocomplete.js',
                      '../Scripts/autocomplete-google-place/autocomplete.css'
                  ]
              },
              {
                  name: 'select',
                  files: [
                      '../Scripts/select/select.js',
                      '../Scripts/select/select.css'
                  ]
              },
              {
                  name: 'dirPagination',
                  files: [
                      '../Scripts/pagination/dirPagination.js'
                  ]
              },
              {
                  name: 'bxslider_angular',
                  files: [
                      '../Scripts/bxslider-angular/bxslider-angular.js'
                  ]
              },
              {
                  name: 'revolutionslider_angular',
                  files: [
                      '../Scripts/revolutionslider-angular/revolutionslider-angular.js'
                  ]
              },
              {
                  name: 'jcontentwithslider',
                  files: [
                      '../Scripts/productView-image/productview-image-jcontentwithslider.js'
                  ]
              },
              {
                  name: 'tabcontent',
                  files: [
                      '../Scripts/productview-tabcontent/productview-tabcontent.js'
                  ]
              },
              {
                  name: 'home_maps',
                  files: [
                      '../Scripts/home-maps-angular/home-maps-angular.js'
                  ]
              },
              {
                  name: 'google_maps',
                  files: [
                      '../Scripts/angular-google-maps.js'
                  ]
              },
              {
                  name: 'ngScrollTo',
                  files: [
                      '../Scripts/ng-scroll-to/ngScrollTo.js'
                  ]
              },
              {
                  name: 'category_angular',
                  files: [
                      '../Scripts/category-angular/category-angular.js'
                  ]
              },
              {
                  name: 'block_ui',
                  files: [
                      '../Scripts/angular-block-ui/angular-block-ui.js',
                      '../Scripts/angular-block-ui/angular-block-ui.css'
                  ]
              },
              {
                  name: 'css_injector',
                  files: [
                      '../Scripts/angular-css-injector/angular-css-injector.js'
                  ]
              },
              {
                  name: 'social',
                  files: [
                      '../Scripts/angular-social/angular-social.js',
                      '../Content/angular-social/angular-social.css'
                  ]
              },
              {
                  name: 'ngGeolocation',
                  files: [
                      '../Scripts/ngGeolocation/ngGeolocation.js'
                  ]
              },
              {
                  name: 'socialshare',
                  files: [
                      '../Scripts/angular-socialshare/angular-socialshare.js',
                      '../Scripts/angular-socialshare/angular-socialshare.css'
                  ]
              },
              {
                  name: 'myfilters',
                  files: [
                      '../Scripts/filters/myfilters.js'
                  ]
              }
          ]
      });
  }])
;
