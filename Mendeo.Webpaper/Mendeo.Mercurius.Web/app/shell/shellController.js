(function () {
    'use strict';

    angular.module('app.shell')
            .controller('shellController', shellController);

    function shellController() {
        /* jshint validthis:true */
        var vm = this;

        function activate() {
            vm.date = new Date();
        }

        activate();
    }

})();
