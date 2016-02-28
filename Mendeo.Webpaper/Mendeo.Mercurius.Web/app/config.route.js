(function() {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', []);
    
    // Configure the routes and route resolvers
    app.config(['$routeProvider', '$locationProvider', 'routes', 'JQ_CONFIG', routeConfigurator]);
    function routeConfigurator($routeProvider, $locationProvider, routes, JQ_CONFIG) {

        //#region testing
        // Learning Point:
        // If we are testing, we do NOT want to se the routes. 
        // We did this to prevent the route changes from happening during tests
        if (window.testing) return;
        // some tests fail if this is EVER executed during ANY test in the run
        //#endregion
        
        $locationProvider.html5Mode(true);

        
        $routeProvider.when('/cgu', {
            templateUrl: 'app/content/legal/cgu.html',
            controller: 'CguController',
            controllerAs: 'vm',
            caseInsensitiveMatch: true
        });
        
        $routeProvider.when('/view/:productCode', {
            templateUrl: 'app/content/productView/productView.html',
            controller: 'ProductViewController',
            controllerAs: 'vm',
            caseInsensitiveMatch: true,
            resolve: {
                deps: ['uiLoad', '$ocLazyLoad',
                    function (uiLoad, $ocLazyLoad) {
                        return uiLoad.load(JQ_CONFIG.bxslider)
                        .then(function () {
                            return uiLoad.load(JQ_CONFIG.rs);
                        })
                        .then(function () {
                            return uiLoad.load(JQ_CONFIG.moment);
                        })
                        .then(function () {
                            return uiLoad.load(JQ_CONFIG.jquery_ui);
                        })
                        .then(function () {
                            return $ocLazyLoad.load('css_injector');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('myfilters');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('ui_bootstrap_tpls');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('revolutionslider_angular');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('captcha');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('bxslider_angular');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('google_maps');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('social');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('tabcontent');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('jcontentwithslider');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('/app/content/productView/productViewCtrl.js');
                        });
                    }]
            }
        });
        $routeProvider.when('/categoryList/:querySearch?', {
            templateUrl: 'app/content/categoryList/categoryList.html',
            navPath: '/categoryList',
            controller: 'CategoryListController',
            controllerAs: 'vm',
            caseInsensitiveMatch: true,
            showNav: 'Parcourir les annonces',
            resolve: {
                deps: ['uiLoad', '$ocLazyLoad',
                    function (uiLoad, $ocLazyLoad) {
                        return $ocLazyLoad.load('css_injector')
                        .then(function () {
                            return $ocLazyLoad.load('myfilters');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('ui_bootstrap_tpls');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('autocomplete');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('dirPagination');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('select');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('google_maps');
                        })
                        .then(function () {
                            return $ocLazyLoad.load('/app/content/categoryList/categoryListCtrl.js');
                        });
                    }]
            },
        });
       
        $routeProvider.otherwise({
            redirectTo: '/categoryList'
        });

        //routes.forEach(function (r) {
        //    setRoute(r.url, r.config);
        //});
        
        function setRoute(url, definition) {
            // Sets resolvers for all of the routes
            // 1. Anything you need to do prior to going to a new route
            // 2. Any logic that might prevent the new route ($q.reject)
            definition.resolve = angular.extend(definition.resolve || {}, {
                prime: prime //Learning Point: do not prime as a test
            });
            $routeProvider.when(url, definition);

            return $routeProvider;
        }
    }

})();