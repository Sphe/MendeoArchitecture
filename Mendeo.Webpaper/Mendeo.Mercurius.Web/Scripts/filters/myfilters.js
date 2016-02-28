(function () {
    'use strict';

    var myfilters = angular.module('myfilters', []);

    myfilters.filter('yes_no', function () {
        return function (booltext) {
            if (booltext) {
                return 'Oui';
            }
            return 'Non';
        }
    });


}());