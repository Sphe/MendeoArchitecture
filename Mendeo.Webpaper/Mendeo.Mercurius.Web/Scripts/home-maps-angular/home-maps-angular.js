(function () {
    'use strict';

    var bxslider = angular.module('ngHomeMaps', ['ngGeolocation']);

    bxslider.directive('homeMaps', ['$timeout', '$http', '$q', '$geolocation', 'notifierService', 'productService', 'locationService', function ($timeout, $http, $q, $geolocation, notifierSvc, productService, locationService) {
        return {
            restrict: 'A',
            replace: false,
            scope: true,
            templateUrl: '/scripts/home-maps-angular/home-maps-angular.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var runOnceGeoIp = true;

                    scope.map = { center: { latitude: 40.1451, longitude: -99.6680 }, zoom: 14, bounds: {} };

                    var isDraggable = $(document).width() > 480 ? true : false;

                    var dblClickEvent = function () {
                        if (scope.options && isDraggable === false) {
                            scope.options.draggable = !scope.options.draggable;
                        }
                    }

                    scope.options = {
                        scrollwheel: false,
                        draggable: isDraggable
                    };

                    scope.mapsEvents = {
                        dblclick: dblClickEvent
                    };

                    var createMarker = function (item) {
                        var ret = {};

                        ret.latitude = item.googleAddressLatitude,
                        ret.longitude = item.googleAddressLongitude,
                        ret.product = item,
                        ret.show = false;
                        ret.id = item.productCode;

                        ret.productImageUrl = '//mercurius.mendeo.com/ProductImage/' + scope.getHomeSliderImages(item);

                        ret.onClick = function () {
                            ret.show = !ret.show;
                        };

                        return ret;
                    };
                    scope.randomMarkers = [];

                    scope.currentPlace = {};

                    scope.productGeoLocalised = [];

                    scope.getHomeSliderImages = function (p) {
                        var res = null;

                        _.each(p.productImageMap, function (it) {
                            if (it.productImageTypeID === 5) {
                                res = it.image.thumbUrl;
                                return false;
                            }

                            return true;
                        });

                        return res;
                    };

                    var runProductGeoLocalised = function (latitude, longitude) {
                        productService.getGeoLocalProduct(latitude, longitude).then(function (dtos) {
                            scope.productGeoLocalised = dtos.data;
                        });
                    };

                    scope.$watch('currentPlace', function (newValue, oldValue) {
                        if (newValue && newValue.geometry && newValue.geometry.location) {
                            scope.map.center.latitude = newValue.geometry.location.lat();
                            scope.map.center.longitude = newValue.geometry.location.lng();
                        }
                    }, true);

                    scope.$watch('productGeoLocalised', function (newValue, oldValue) {
                        if (newValue) {
                            var markers = [];
                            for (var i = 0; i < newValue.length; i++) {
                                markers.push(createMarker(newValue[i]));
                            }
                            scope.randomMarkers = markers;
                        }
                    }, true);

                    scope.geoIp = null;

                    var getIpInfoGeo = function () {
                        locationService.getIpLocationInfo().then(function (dtos) {
                            scope.geoIp = dtos.data;
                        });
                    };

                    scope.$watch('geoIp', function (newValue, oldValue) {
                        if (newValue && newValue.latitude && newValue.longitude) {
                            scope.map.center.latitude = newValue.latitude;
                            scope.map.center.longitude = newValue.longitude;
                        }
                    }, true);

                    scope.$watch('map.center', function (newValue, oldValue) {
                        if (newValue && newValue.longitude && newValue.latitude) {
                            runProductGeoLocalised(newValue.latitude, newValue.longitude);
                        }
                    }, true);

                    $geolocation.getCurrentPosition({
                        timeout: 10000,
                        enableHighAccuracy: true
                    }).then(function (position) {
                        if (position && position.coords) {
                            scope.map.center.latitude = position.coords.latitude;
                            scope.map.center.longitude = position.coords.longitude;
                        }
                    }, function () {
                        getIpInfoGeo();
                    });
                });
            }
        };
    }]);

}());