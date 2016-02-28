(function () {
    'use strict';

    var bxslider = angular.module('pvRsSlider', []);

    bxslider.directive('pvRsSlider', ['environment', function (env) {
        return {
            restrict: 'A',
            replace: false,
            scope: true,
            templateUrl: '/scripts/productView-image/productview-image-jcontentwithslider.html',
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var $elmtSlider = elm.find('.j-contentwidthslider');

                    scope.$watch("vm.imageProductViews", function (newVal, oldVal) {
                        if (newVal && !env.isIe()) {
                            setTimeout(function () {
                                var revapi = $elmtSlider.revolution({
                                    delay: 2000,
                                    startwidth: 1170,
                                    startheight: 1400,
                                    startWithSlide: 0,

                                    fullScreenAlignForce: "off",
                                    autoHeight: "off",
                                    minHeight: "off",

                                    shuffle: "off",

                                    onHoverStop: "on",

                                    thumbWidth: 100,
                                    thumbHeight: 100,
                                    thumbAmount: 100,

                                    hideThumbsOnMobile: "off",
                                    hideNavDelayOnMobile: 1500,
                                    hideBulletsOnMobile: "off",
                                    hideArrowsOnMobile: "off",
                                    hideThumbsUnderResoluition: 0,

                                    hideThumbs: 0,
                                    hideTimerBar: "off",

                                    keyboardNavigation: "on",

                                    navigationType: "bullet",
                                    navigationArrows: "solo",
                                    navigationStyle: "round",

                                    navigationHAlign: "center",
                                    navigationVAlign: "bottom",
                                    navigationHOffset: 30,
                                    navigationVOffset: 30,

                                    soloArrowLeftHalign: "left",
                                    soloArrowLeftValign: "center",
                                    soloArrowLeftHOffset: 20,
                                    soloArrowLeftVOffset: 0,

                                    soloArrowRightHalign: "right",
                                    soloArrowRightValign: "center",
                                    soloArrowRightHOffset: 20,
                                    soloArrowRightVOffset: 0,


                                    touchenabled: "on",
                                    swipe_velocity: "0.7",
                                    swipe_max_touches: "1",
                                    swipe_min_touches: "1",
                                    drag_block_vertical: "false",

                                    parallax: "mouse",
                                    parallaxBgFreeze: "on",
                                    parallaxLevels: [10, 7, 4, 3, 2, 5, 4, 3, 2, 1],
                                    parallaxDisableOnMobile: "off",

                                    stopAtSlide: -1,
                                    stopAfterLoops: -1,
                                    hideCaptionAtLimit: 0,
                                    hideAllCaptionAtLilmit: 0,
                                    hideSliderAtLimit: 0,

                                    dottedOverlay: "none",

                                    spinned: "spinner4",

                                    fullWidth: "on",
                                    forceFullWidth: "off",
                                    fullScreen: "off",
                                    fullScreenOffsetContainer: "#topheader-to-offset",
                                    fullScreenOffset: "0px",

                                    panZoomDisableOnMobile: "off",

                                    simplifyAll: "off",

                                    shadow: 0
                                });

                                var mass = [];
                                _.each(newVal, function (i) {
                                    mass.push(i.image.thumbUrl);
                                });

                                $elmtSlider.bind('revolution.slide.onloaded', function () {
                                    $elmtSlider.next('.tp-bullets').find('.bullet').each(function (i) {
                                        $(this).css('background', 'url(' + scope.vm.prefixProductImageUrl + mass[i] + ') no-repeat scroll 0px 0px / cover transparent');
                                    });
                                    $elmtSlider.unbind('revolution.slide.onloaded');
                                });
                            }, 500);
                            
                        }
                    });
                });
            }
        };
    }]);

}());