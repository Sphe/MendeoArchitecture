jQuery(function ($) {
    if ($('.b-google-map').length > 0) {
        var centerLat, centerLng, locations;
        var locationsSet = $('.b-google-map').data('locationSet');

        switch (locationsSet) {
            case 'contact-us':
                centerLat = 35.793856;
                centerLng = -91.621256;
                locations = [
                    {title: 'Heading office', lat: centerLat, long: centerLng}
                ];
                break;
            default:
                centerLat = 35.362961015414056;
                centerLng = -90.71299265625;
                locations = [
                    {title: 'Coogee Beach', lat: 35.027332, long: -89.88796},
                    {title: 'Cronulla Beach', lat: 34.727332, long: -88.86796},
                    {title: 'Manly Beach', lat: 34.627332, long: -91.84796},
                    {title: 'Maroubra Beach', lat: 34.527332, long: -92.82796},
                    {title: 'Bondi Beach', lat: 34.927332, long: -90.90796}
                ];
        }

        var map = FrexyGoogleMap.init(new google.maps.LatLng(centerLat, centerLng));
        map.addMarkers(locations);
    }
});

var FrexyGoogleMap = {
    selMapView: '.b-google-map__map-view',
    selInfoWindowTemplate: '.b-google-map__info-window-template',
    selMarkerTemplate: '.marker-template',
    init: function(centerLatLng) {
        var myOptions = {
            zoom: 9,
            scrollwheel: false,
            center: centerLatLng,
            mapTypeControlOptions: {
                mapTypeIds: ['greyed']
            },
            mapTypeId: 'greyed',
            mapTypeControl: false
        };

        this.map = new google.maps.Map($(this.selMapView)[0], myOptions);

        // set map greyed style
        var featureOpts = [
            {
                stylers: [
                    { visibility: 'simplified' },
                    { "saturation": -100 },
                    { "lightness": 50 }
                ]
            }
        ];
        var customMapType = new google.maps.StyledMapType(featureOpts, {name: 'Greyed Style'});
        this.map.mapTypes.set('greyed', customMapType);

        // info window
        var infoWindowTemplate = $(this.selInfoWindowTemplate);
        this.infoWindow = new google.maps.InfoWindow({
            content: infoWindowTemplate.html(),
            maxWidth: infoWindowTemplate.data('width')
//            ,pixelOffset: new google.maps.Size(0, 60)
        });

        this.markers = [];

        this.markerIcon = $(this.selMarkerTemplate).attr('src');
        if (this.markerIcon.length == 0) {
            this.markerIcon = {
                path: fontawesome.markers.MAP_MARKER,
                scale: 0.7,
                strokeWeight: 0.2,
                strokeColor: 'black',
                strokeOpacity: 1,
                fillColor: '#6c6b6b',
                fillOpacity: 1
            };
        }

        return this;
    },
    showHiddenLabel: function() {
        if (this.currentMark && this.currentMark.hasOwnProperty('markerLabel')) {
            this.currentMark.markerLabel.isHidden = false;
            this.currentMark.markerLabel.draw();
        }
    },
    openInfoWindow: function (marker) {
        this.infoWindow.close();
        this.showHiddenLabel();
    //                infoWindow.setContent(locations[i][0]);
        if (marker.hasOwnProperty('markerLabel')) {
            marker.markerLabel.isHidden = true;
            marker.markerLabel.draw();
        }
        this.currentMark = marker;
        this.infoWindow.open(this.map, marker);
    },
    addMarkers: function (locations) {
        var me = this;

        for (var i = 0; i < locations.length; i++) {
            var location = locations[i];
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(location['lat'], location['long']),
                title: location['title'],
                map: this.map,
                icon: this.markerIcon
            });

            // markers placement
            var markerTemplate = $(this.selMarkerTemplate);

            var markerLabel = markerTemplate.data('label');
            if (markerLabel.length > 0) {
                var label = new MarkerLabel({
                    map: this.map,
                    labelClass: 'f-google-map__marker-label f-primary-b text-uppercase',
                    labelText: markerLabel
                });
                label.bindTo('position', marker);
                label.bindTo('visible', marker);
                label.bindTo('clickable', marker);
                label.bindTo('zIndex', marker);
                marker.markerLabel = label;
            }

            google.maps.event.addListener(marker, 'click', function () {
                me.openInfoWindow(this);
            });

            this.markers.push(marker);
            google.maps.event.addListener(this.infoWindow, 'closeclick', this.showHiddenLabel);
        }

        // open info window
        setTimeout(function () {
            me.openInfoWindow(me.markers[$(me.selInfoWindowTemplate).data('selected-marker')]);
        }, 1500);
    }
};