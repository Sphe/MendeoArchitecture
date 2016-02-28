(function () {
    'use strict';

    var bxslider = angular.module('ngBxSlider', []);

    bxslider.directive('bxSlider', ['environment', function (env) {
        return {
            restrict: 'A',
            replace: false,
            scope: true,
            templateUrl: '/scripts/bxslider-angular/bxslider-angular.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var target = elm.find('.j-carousel-secondary');
                    var instance = null;

                    var carouselSettings = {
                        controls: true,
                        maxSlides: 3,
                        minSlides: 1,
                        moveSlides: 2,
                        pager: false,
                        slideMargin: 0,
                        nextText: '',
                        prevText: '',
                        auto: true
                    };

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

                    scope.$watch('varCollection', function (newValue, oldValue) {
                        if (newValue) {
                            if (instance) {
                                instance.destroySlider();
                            }

                            if (!env.isIe()) {
                                setTimeout(function () {
                                    instance = target.bxSlider($.extend({}, carouselSettings, {
                                        slideMargin: 28,
                                        slideWidth: 370
                                    }));
                                }, 500);
                            } else {
                                setTimeout(function () {
                                    instance = target.bxSlider({
                                        auto: false,
                                        pager: false,
                                        controls: false,
                                        maxSlides: 3,
                                        slideMargin: 28,
                                        slideWidth: 370
                                    });
                                }, 500);
                            }
                        }
                    }, true);

                    if (attrs.bxSlider === "offers") {

                        scope.$watch('vm.productTopOffers', function (newValue, oldValue) {
                            if (newValue) {
                                if (!env.isIe()) {
                                    scope.varCollection = newValue;
                                } else {
                                    scope.varCollection = _.first(newValue, 3);
                                }
                            }
                        }, true);

                    } else if (attrs.bxSlider === "demands") {

                        scope.$watch('vm.productTopDemands', function (newValue, oldValue) {
                            if (newValue) {
                                if (!env.isIe()) {
                                    scope.varCollection = newValue;
                                } else {
                                    scope.varCollection = _.first(newValue, 3);
                                }
                            }
                        }, true);

                    } else if (attrs.bxSlider === "related") {

                        scope.$watch('vm.productRelated', function (newValue, oldValue) {
                            if (newValue) {
                                if (!env.isIe()) {
                                    scope.varCollection = newValue;
                                } else {
                                    scope.varCollection = _.first(newValue, 3);
                                }
                            }
                        }, true);

                    }
                });
            }
        };
    }]);

    bxslider.directive('bxSliderHomeFooter', ['environment', function (env) {
        return {
            restrict: 'A',
            replace: false,
            scope: {
                title: '='
            },
            templateUrl: '/scripts/bxslider-angular/bxslider-angular-footer.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    //scope.$apply(function () {
                    //    scope.slides = scope.slideit;
                    //});
                    var sliderSettings = {
                        mode: 'fade',
                        auto: true,
                        pager: false
                    };

                    var $bxsliderHomeFooter = elm.find('.j-slider-primary').bxSlider($.extend({}, sliderSettings, {
                        controls: false,
                        pager: true
                    }));
                });
            }
        };
    }]);

}());