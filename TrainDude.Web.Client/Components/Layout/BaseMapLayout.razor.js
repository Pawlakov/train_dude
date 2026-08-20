let map;
let myLayer;

export function initMap(id, lat, lng, zoom) {
    if (map) {
        return;
    }
    
    map = L.map(id).setView([lat, lng], zoom);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

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
    if (!myLayer) {
        throw new Error('Map has not been initialized.');
    }

    myLayer.addData(geoJson);
}

export function clearGeoJson() {
    if (!myLayer) {
        return;
    }
    
    myLayer.clearLayers();
}