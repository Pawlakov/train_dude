export var map;
export var myLayer;

export function initMap(id, lat, long, zoom) {
    map = L.map(id).setView([lat, long], zoom);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var geojsonMarkerOptions = {
        radius: 10,
        fillColor: "#ffffff",
        color: "#000000",
        weight: 1,
        opacity: 1,
        fillOpacity: 1
    };
    myLayer = L.geoJSON(undefined, {
        pointToLayer: function (feature, latlng) {
            return L.circleMarker(latlng, geojsonMarkerOptions);
        }
    }).addTo(map);
}

export function addGeoJson(geoJson) {
    var geoObject = JSON.parse(geoJson);

    myLayer.addData(geoObject);
}