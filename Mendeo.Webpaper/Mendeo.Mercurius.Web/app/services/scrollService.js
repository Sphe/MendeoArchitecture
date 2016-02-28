(function () {
    'use strict';

    var serviceId = 'scrollService';

    var serviceId = 'scrollService';
    angular.module('app').factory(serviceId, ['$location', '$document', '$anchorScroll', scrollService]);

    function scrollService($location, $document, $anchorScroll) {

        var scrollTo = function (id, yForceOffset) {
            var old = $location.hash();
            $location.hash(id);

            $anchorScroll.yOffset = (yForceOffset != null) ? yForceOffset : 100;
            $anchorScroll();
            //reset to old to keep any additional routing logic from kicking in
            $location.hash(old);
        }

        var scrollAnimateTo = function (someElement, yForceOffset) {
            var yOffset = (yForceOffset != null) ? yForceOffset : 100;
            $document.scrollToElement(someElement, yOffset, 2000);
        }

        var service = {
            scrollTo: scrollTo,
            scrollAnimateTo: scrollAnimateTo
        };

        return service;
    }
})();