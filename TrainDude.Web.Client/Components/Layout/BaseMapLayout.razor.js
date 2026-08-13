export var map;
export var myLayer;

export function initMap(id, lat, long, zoom) {
    map = L.map(id).setView([lat, long], zoom);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);
    
    clearGeoJson();
}

export function addGeoJson(geoJson) {
    var geoObject = JSON.parse(geoJson);

    myLayer.addData(geoObject);
}

export function clearGeoJson() {
    if (myLayer !== undefined) {
        map.removeLayer(myLayer);
    }
    
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