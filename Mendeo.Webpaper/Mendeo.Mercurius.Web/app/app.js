(function () {
    'use strict';

    var id = 'app';

    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',          // routing
        'ngResource',
        'app.shell',
        'app.core',
        'app.security',

        'common',

        'ui.load',
        'oc.lazyLoad',

        'blockUI',
        'djds4rce.angular-socialshare',
        'ngMaterial',
        'textAngular',
        '720kb.tooltips'
    ]);

    // Handle routing errors and success events
    app.run(['routemediator', '$route', '$window', 'appSettingsService', '$FB',
        function (routemediator, $route, $window, appSettingsService, $FB) {
            //routemediator.setRoutingEventHandlers();
            appSettingsService.siteUrl = $window.location.origin;
            //$FB.init('974069415954129');
        }]);
})();
