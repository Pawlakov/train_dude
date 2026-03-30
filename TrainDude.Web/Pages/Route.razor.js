export var map;
export var myLayer;

export function initMap(id, lat, long, zoom) {
    map = L.map(id).setView([lat, long], zoom);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var stationMarkerBorderStyle = {
        fillColor: "#000000",
        color: "#000000",
        weight: 2,
        opacity: 1,
        fillOpacity: 1
    };
    var stationMarkerFillStyle = {
        fillColor: "#ffffff",
        color: "#ffffff",
        weight: 0,
        opacity: 1,
        fillOpacity: 1
    };

    myLayer = L.geoJSON(undefined, {
        pointToLayer: function (feature, latlng) {
            return L.circleMarker(latlng, {
                radius: 8,
                fillColor: '#ff7800',
                color: '#000',
                weight: 1,
                opacity: 1,
                fillOpacity: 0.8
            });
        }
    }).addTo(map);
}

export function addGeoJson(geoJson) {
    var geoObject = JSON.parse(geoJson);

    myLayer.addData(geoObject);
}