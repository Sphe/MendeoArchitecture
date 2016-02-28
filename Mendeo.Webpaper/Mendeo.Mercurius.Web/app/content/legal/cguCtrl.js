(function () {
    'use strict';

    angular.module('app')
            .controller('CguController', CguController);

    function CguController() {
        /* jshint validthis:true */
        var vm = this;

        function activate() {
            vm.date = new Date();
        }

        activate();
    }

})();
