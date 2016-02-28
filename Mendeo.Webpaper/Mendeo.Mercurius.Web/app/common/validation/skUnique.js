(function () {
    'use strict';

    angular
        .module('app')
        .directive('skUnique', skUnique);

    skUnique.$inject = [];

    function skUnique() {
        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A',
            scope: {
                skUnique: '&'
            }
        };

        return directive;

        function link(scope, element, attrs, ngModel) {
            var wrappedValidator = function (mv, vv) {
                ngModel.$setValidity('checking', false);

                var res = scope.skUnique({ value: mv || vv });

                if (res) {
                    res.finally(function () {
                        ngModel.$setValidity('checking', true);
                    });
                }

                return res;
            };

            ngModel.$asyncValidators.unique = wrappedValidator;
        }
    }
})();