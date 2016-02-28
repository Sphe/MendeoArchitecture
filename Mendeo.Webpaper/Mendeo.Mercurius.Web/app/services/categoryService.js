(function () {
    'use strict';

    var serviceId = 'categoryService';
    angular.module('app').factory(serviceId, ['$http', 'notifierService', categoryService]);

    function categoryService($http, notifierSvc) {

        var getBreadCrumb = function (catId) {
            return $http.get('/api/Category/GetBreadCrumb?categoryId=' + catId)
                        .success(function (dtos) {
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Error category breadcrumb: ' + data, type: 'error' });
                        });
        }


        var loadCategoryTree = function () {
            return $http.get('/api/Category/GetCategoryTree')
                .success(function (dto) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error loading category tree: ' + data, type: 'error' });
                });
        };

        var service = {
            getBreadCrumb: getBreadCrumb,
            loadCategoryTree: loadCategoryTree
        };

        return service;
    }
})();