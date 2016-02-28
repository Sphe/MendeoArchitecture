(function () {
    'use strict';

    angular.module('app')
           .factory('environment', ['$rootScope', EnvFunction]);

    function EnvFunction($rootScope) {
        var isDemand = false;
        var isPro = false;
        var isIe = bowser.msie || bowser.msedge;

        return {
            isDemand: function() {
                return isDemand;
            },
            isPro: function() {
                return isPro;
            },
            isIe: function() {
                return isIe;
            },
            setDemand: function (val) {
                isDemand = val;
                $rootScope.$broadcast('demand-changed', { isDemand: isDemand });
            },
            setPro: function (val) {
                isPro = val;
                $rootScope.$broadcast('pro-changed', { isPro: isPro });
            }
        }
    };
})();