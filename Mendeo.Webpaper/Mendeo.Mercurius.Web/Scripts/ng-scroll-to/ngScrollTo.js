(function () {
    'use strict';

    var bxslider = angular.module('scrollOnClick', []);

    bxslider.directive('scrollOnClick', function () {
        return {
            restrict: 'A',
            replace: false,
            scope: true,
            link: function (scope, elm, attrs) {
                elm.ready(function () {
                    var idToScroll = attrs.href;
                    elm.on('click', function (e) {
                        var $target;
                        if (idToScroll) {
                            $target = $(idToScroll);
                        } else {
                            $target = elm;
                        }
                        //$("body").animate({ scrollTop: $target.offset().top - 50 }, "slow"); //adding the banner height (50)
                        $('html, body').animate({
                            scrollTop: $target.offset().top
                        }, 2000);

                        e.preventDefault();
                        e.stopPropagation();
                    });
                });
            }
        };
    });
}());