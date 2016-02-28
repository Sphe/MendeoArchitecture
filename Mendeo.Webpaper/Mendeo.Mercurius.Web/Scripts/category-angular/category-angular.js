(function () {
    'use strict';

    var catg = angular.module('categoryAngular', []);

    catg.directive('categoryAngular', ['$timeout', '$http', '$q', '$location', 'notifierService', 'categoryService', function ($timeout, $http, $q, $location, notifierSvc, categoryService) {
        return {
            restrict: 'A',
            replace: false,
            scope: {
                fullCategory: "="
            },
            templateUrl: '/scripts/category-angular/category-angular.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    scope.currentCategory = scope.fullCategory;
                    scope.breadCrumbList = [];

                    var runBreadCrumbRequest = function (catId) {
                        categoryService.getBreadCrumb(catId).then(function (result) {
                            scope.breadCrumbList = result.data;
                        });
                    };

                    scope.$watch('fullCategory', function (newValue, oldValue) {
                        if (newValue) {
                            scope.currentCategory = scope.fullCategory;
                        }
                    }, true);

                    scope.$watch('currentCategory', function (newValue, oldValue) {
                        if (newValue && newValue.id) {
                            runBreadCrumbRequest(newValue.id);
                        }
                    }, true);

                    scope.clickToCategory = function (cat) {
                        if (cat.children && cat.children.length > 0) {
                            scope.currentCategory = cat;
                        }
                    };

                    scope.backToRoot = function () {
                        scope.currentCategory = scope.fullCategory;
                    };

                    scope.goToCategoryList = function (e, catId) {
                        e.preventDefault();
                        e.stopPropagation();

                        $location.path("/categoryList/" + "cat-" + catId);
                    };
                });
            }
        };
    }]);

}());