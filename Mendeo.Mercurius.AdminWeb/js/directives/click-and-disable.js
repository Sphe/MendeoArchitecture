/*
    - Accepts an option, passed by attribute, of a function in the scope that must return a promise for the version using 'finally'
    - On click of the button, calls this function, and disables the button
    - On finally of the promise, it re-enables the button

    <button click-and-disable="functionThatReturnsPromise()">Click me</button>

    $scope.functionThatReturnsPromise = function() {
      return $timeout(angular.noop, 1000);
    }
*/
angular.module('app')
    .directive('clickAndDisableTemporary', function () {
        return {
            scope: {
                clickAndDisableTemporary: '&'
            },
            link: function (scope, iElement, iAttrs) {
                iElement.bind('click', function () {
                    iElement.prop('disabled', true);
                    scope.clickAndDisableTemporary().finally(function () {
                        iElement.prop('disabled', false);
                    })
                });
            }
        };
    })
    .directive('clickAndDisable', function () {
        return {
            scope: {
                clickAndDisable: '&'
            },
            link: function (scope, iElement, iAttrs) {
                iElement.bind('click', function () {
                    iElement.prop('disabled', true);
                    scope.clickAndDisable();
                });
            }
        };
    });