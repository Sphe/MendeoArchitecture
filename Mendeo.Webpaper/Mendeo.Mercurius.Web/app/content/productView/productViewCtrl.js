(function () {
    'use strict';

    var controllerId = 'ProductViewController';
    angular.module('app').controller(controllerId, ['$scope', '$routeParams', '$location', '$http', '$q', '$sce', 'notifierService', 'vcRecaptchaService', 'productService', ProductViewController])
        .filter('retrieveHomeImage', function() {
            return function(input) {
                if (input === null) {
                    return null;
                }

                var prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";
                var thumbUrltmp = _.filter(input, function (it) {
                    return it.productImageTypeID === 5;
                });

                var thumbUrl = "";
                if (thumbUrltmp !== null && thumbUrltmp.length > 0) {
                    thumbUrl = thumbUrltmp[0].image.thumbUrl;
                }

                return prefixProductImageUrl + thumbUrl;
            };
        })
        .filter('retrieveViewLinkImage', function() {
            return function(input) {
                if (input === null) {
                    return null;
                }

                var prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";
                var thumbUrltmp = _.filter(input, function (it) {
                    return it.productImageTypeID === 3;
                });

                var thumbUrl = "";
                if (thumbUrltmp !== null && thumbUrltmp.length > 0) {
                    thumbUrl = thumbUrltmp[0].image.thumbUrl;
                }

                return prefixProductImageUrl + thumbUrl;
            };
        })
        .filter('retrieveFullViewLinkImage', ['$location', function ($location) {
            return function(input) {
                if (input === null) {
                    return null;
                }

                var prefix = "http://" + $location.host() + ":" + $location.port();

                var prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";
                var thumbUrltmp = _.filter(input, function (it) {
                    return it.productImageTypeID === 3;
                });

                var thumbUrl = "";
                if (thumbUrltmp !== null && thumbUrltmp.length > 0) {
                    thumbUrl = thumbUrltmp[0].image.thumbUrl;
                }

                return prefix + prefixProductImageUrl + thumbUrl;
            };
    }]);

    function ProductViewController($scope, $routeParams, $location, $http, $q, $sce, notifierSvc, vcRecaptchaService, productService) {
        var vm = this;

        vm.title = "ProductView";

        vm.productDetail = {};
        vm.productCode = $routeParams.productCode;
        vm.attributeBag = null;
        vm.map = null;
        vm.mapOptions = null;
        vm.mapMarker = null;
        vm.oneLineAddress = null;
        vm.prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";
        vm.productImageUrl = null;
        vm.imageProductViews = null;
        vm.captcha = null;
        vm.showUserEmail = false;
        vm.socialSharedUrl = $location.absUrl();
        vm.productDateCreated = null;
        vm.descriptionHtmlSafe = null;

        var productByImageProductViewLink = function () {
            if (vm.productDetail === {})
                return [];

            return _.filter(vm.productDetail.productImageMap, function (it) {
                return it.productImageTypeID === 3;
            });
        };

        var productByImageProductView = function () {
            if (vm.productDetail === {})
                return [];

            return _.filter(vm.productDetail.productImageMap, function (it) {
                return it.productImageTypeID === 1;
            });
        };

        vm.addProductFavorite = function (isAlreadyUserFavorite) {
            productService.addProductFavorite(isAlreadyUserFavorite, vm.productCode);
        }

        vm.showEmail = function () {
            vm.showUserEmail = true;
        }

        var getDetail = function () {
            var promise = $q.defer();

            $http.get('/api/Product/GetDetail' + '?productCode=' + vm.productCode)
                .success(function (dto) {
                    vm.productDetail = dto;

                    vm.productDateCreated = moment(dto.createdOn).format('LLL');

                    vm.map = { center: { latitude: dto.googleAddressLatitude, longitude: dto.googleAddressLongitude }, zoom: 15 };
                    vm.mapOptions = { scrollwheel: false };
                    vm.oneLineAddress = dto.googleAddressInLine;
                    vm.mapMarker = {
                        id: dto.productCode,
                        coords: {
                            latitude: dto.googleAddressLatitude,
                            longitude: dto.googleAddressLongitude
                        },
                        options: { draggable: false },
                    };
                    vm.imageProductViews = productByImageProductView();

                    vm.descriptionHtmlSafe = $sce.trustAsHtml(dto.productCultureMap[0].description);

                    promise.resolve({ productId: vm.productDetail.productID });
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error loading productDetail: ' + data, type: 'error' });
                });

            return promise.promise;
        };

        vm.productRelated = [];

        var getProductRelated = function (productId) {
            productService.getProductRelated(productId).then(function (dto) {
                vm.productRelated = dto.data;
            });
        };

        vm.productPopular = [];

        var getProductPopular = function () {
            productService.getProductPopular().then(function (dto) {
                vm.productPopular = dto.data;
            });
        };

        var executeProductEventView = function () {
            $http.get('/api/Product/AddProductEvent?productCode=' + vm.productCode)
                .success(function (dto) {
                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error Add Product Event: ' + data, type: 'error' });
                });
        };

        vm.goToCategoryList = function (e, catId) {
            e.preventDefault();
            e.stopPropagation();

            $location.path("/categoryList/" + "cat-" + catId);
        };

        vm.recapchakey = '6Les9f4SAAAAACJIOIajJndm2m6QUtNyrefw-3Hh';
        vm.widgetId = null;

        vm.setResponse = function (response) {
            vm.captcha = response;
        };

        vm.setWidgetId = function (widgetId) {
            vm.widgetId = widgetId;
        };

        activate();

        function activate() {

            getDetail().then(function (res) {
                getProductRelated(res.productId);
            }).then(function () {
                getProductPopular();
            });

            executeProductEventView();
        }
    }

})();