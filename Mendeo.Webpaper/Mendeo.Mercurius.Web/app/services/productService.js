(function () {
    'use strict';


    var serviceId = 'productService';
    angular.module('app').factory(serviceId, ['$http', 'notifierService', productService]);

    function productService($http, notifierSvc) {

        var addProductFavorite = function (isAlreadyUserFavorite, productCode) {

            if (!isAlreadyUserFavorite) {
                $http.get('/api/Product/AddProductFavorite' + '?productCode=' + productCode)
                    .success(function (dto) {
                        notifierSvc.show({ message: 'Ajouté au favoris', type: 'info' });
                    })
                    .error(function (data) {
                        notifierSvc.show({ message: 'Error Add Product Favorite: ' + data, type: 'error' });
                    });
            }
            else {
                notifierSvc.show({ message: 'Ce produit est déjà dans vos favoris', type: 'warning' });
            }
        }

        var getFavoriteProducts = function () {
            return $http.get('/api/Product/GetFavoriteProducts')
                .success(function (dto) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error loading favorite products : ' + data, type: 'error' });
                });
        };

        var getGeoLocalProduct = function (latitude, longitude) {
            return $http.get('/api/Product/GetGeoLocalProduct?latitude=' + latitude + '&longitude=' + longitude)
                .success(function (dtos) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error product geolocalized: ' + data, type: 'error' });
                });
        };

        var getProductPopular = function () {
            return $http.get('/api/Product/GetProductPopular')
                        .success(function (dto) {
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Error product related: ' + data, type: 'error' });
                        });
        };

        var getProductRelated = function (productId) {

            var url = '/api/Product/GetProductRelated';

            if (productId) {
                url = '/api/Product/GetProductRelated?productId=' + productId;
            }

            return $http.get(url)
                        .success(function (dto) {
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Error product related: ' + data, type: 'error' });
                        });
        };

        var getTopDemands = function () {
            return $http.get('/api/Product/GetTopDemands')
                    .success(function (dtos) {
                    })
                    .error(function (data) {
                        notifierSvc.show({ message: 'Error top demands: ' + data, type: 'error' });
                    });
        };

        var getTopOffers = function () {
            return $http.get('/api/Product/GetTopOffers')
                .success(function (dtos) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error top offers: ' + data, type: 'error' });
                });
        };

        var index = function (productId) {
            return $http.get('/api/Product/Index' + '?productId=' + productId)
                        .success(function (dto) {
                            notifierSvc.show("Product Indexed.");
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Error Index: ' + data, type: 'error' });
                        });
        };        

        var postProduct = function (dto) {
            return $http.post('/api/Product/PostProduct', dto)
                        .success(function (result) {
                            notifierSvc.show("Product Added.");
                        })
                        .error(function (data) {
                            notifierSvc.show({ message: 'Post product error: ' + data, type: 'error' });
                        });
        };


        var removeFavoriteProducts = function (dto) {

            return $http.post('/api/Product/RemoveFavoriteProducts', dto)
                .success(function (dtos) {
                    notifierSvc.show({ message: 'Suppression de la sélection favoris réussie', type: 'info' });
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error removing favorite products : ' + data, type: 'error' });
                });
        };
        

        var service = {
            addProductFavorite: addProductFavorite,
            getFavoriteProducts: getFavoriteProducts,
            getGeoLocalProduct: getGeoLocalProduct,
            getProductPopular: getProductPopular,
            getProductRelated: getProductRelated,
            getTopDemands: getTopDemands,
            getTopOffers: getTopOffers,
            index: index,
            postProduct: postProduct,
            removeFavoriteProducts: removeFavoriteProducts
        };

        return service;
    }
})();