(function () {
    'use strict';

    var pvTabContent = angular.module('pvTabContent', []);

    pvTabContent.directive('pvTabContent', function () {
        return {
            restrict: 'A',
            replace: true,
            scope: true,
            templateUrl: '/scripts/productview-tabcontent/productview-tabcontent.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var $elmtSlider = elm;
                    $elmtSlider.tabs();
                });
            }
        };
    });

}());