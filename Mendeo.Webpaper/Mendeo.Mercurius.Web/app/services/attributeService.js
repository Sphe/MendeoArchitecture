(function () {
    'use strict';

    var serviceId = 'attributeService';
    angular.module('app').factory(serviceId, ['$http', 'notifierService', attributeService]);

    function attributeService($http, notifierSvc) {

        var getAttributeAutocompleItem = function (query, attributeTypeId) {
            var q = query.item || '';
            return $http.get('/api/Attribute/GetAttributeAutocompleItem?attributeTypeId=' + attributeTypeId + '&query=' + q, { cache: false })
                            .then(function (response) {
                                return response;
                            });
        };

        var getAttributeAutocompleType = function (query) {
            var q = query.type || '';
            return $http.get('/api/Attribute/GetAttributeAutocompleType?query=' + q, { cache: false })
                        .then(function (response) {
                            return response;
                        });
        };        

        var service = {
            getAttributeAutocompleItem: getAttributeAutocompleItem,
            getAttributeAutocompleType: getAttributeAutocompleType
        };

        return service;
    }
})();