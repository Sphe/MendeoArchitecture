module.exports = {
  angular:{
    src:[
      'bower_components/jquery/dist/jquery.min.js',

      'bower_components/lodash/lodash.js',

      'bower_components/angular/angular.js',
      
      'bower_components/angular-animate/angular-animate.js',
      'bower_components/angular-cookies/angular-cookies.js',
      'bower_components/angular-resource/angular-resource.js',
      'bower_components/angular-sanitize/angular-sanitize.js',
      'bower_components/angular-touch/angular-touch.js',

      'bower_components/angular-ui-router/release/angular-ui-router.js', 
      'bower_components/ngstorage/ngStorage.js',
      'bower_components/angular-ui-utils/ui-utils.js',

      'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
     
      'bower_components/oclazyload/dist/ocLazyLoad.js',
     
      'bower_components/angular-translate/angular-translate.js',
      'bower_components/angular-translate-loader-static-files/angular-translate-loader-static-files.js',
      'bower_components/angular-translate-storage-cookie/angular-translate-storage-cookie.js',
      'bower_components/angular-translate-storage-local/angular-translate-storage-local.js',

      'js/*.js',
      'js/directives/*.js',
      'js/services/*.js',
      'js/filters/*.js',
      'js/controllers/bootstrap.js'
    ],
    dest:'angular/js/app.src.js'
  }
}
