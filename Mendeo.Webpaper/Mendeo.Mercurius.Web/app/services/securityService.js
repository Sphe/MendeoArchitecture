(function () {
    'use strict';

    var serviceId = 'securityService';
    angular.module('app').factory(serviceId, ['$http', 'notifierService', securityService]);

    function securityService($http, notifierSvc) {

        var verifyCaptcha = function (captcha) {
            var request = {};
            return $http.get('/api/Security/VerifyCaptcha?responseCaptcha=' + captcha, { cache: false })
                .success(function (validState) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error verifying the captcha: ' + data, type: 'error' });
                });
        };


        var service = {
            verifyCaptcha: verifyCaptcha
        };

        return service;
    }
})();