(function () {
    'use strict';


    var serviceId = 'locationService';
    angular.module('app').factory(serviceId, ['$http', 'notifierService', locationService]);

    function locationService($http, notifierSvc) {

        var getIpLocationInfo = function () {
            return $http.get('/api/Location/GetIpLocationInfo')
                        .success(function (dtos) {
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Error geo ip: ' + data, type: 'error' });
                        });
        };

        var service = {
            getIpLocationInfo: getIpLocationInfo
        };

        return service;
    }
})();