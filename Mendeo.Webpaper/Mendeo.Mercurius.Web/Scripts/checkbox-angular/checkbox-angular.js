(function () {
    'use strict';

    var bxslider = angular.module('ngCheckbox', []);

    bxslider.directive('ngCheckbox', ['$timeout', '$http', '$q', 'notifierService', function ($timeout, $http, $q, notifierSvc) {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                cbValue: "="
            },
            templateUrl: '/scripts/checkbox-angular/checkbox-angular.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var $elmt = $(elm);
                    $elmt.checkbox();

                    scope.$watch('cbValue', function (newValue, oldValue) {
                        if (newValue === true) {
                            $elmt.checkbox('check');
                        } else {
                            $elmt.checkbox('uncheck');
                        }
                    });

                    $elmt.find('input').off('change');
                    $elmt.find('input').on('change', function () {
                        var val = $(this).is(':checked');
                        scope.$apply(function () {
                            scope.cbValue = val;
                        });
                    });
                });
            }
        };
    }]);

}());