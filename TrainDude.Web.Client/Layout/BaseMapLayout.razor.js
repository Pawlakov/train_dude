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

function makeBound(currentPoint, width, height) {
    var xDifference = width / 2;
    var yDifference = height / 2;
    var southWest = L.point((currentPoint.x - xDifference), (currentPoint.y - yDifference));
    var northEast = L.point((currentPoint.x + xDifference), (currentPoint.y + yDifference));
    return L.latLngBounds(map.containerPointToLatLng(southWest), map.containerPointToLatLng(northEast));
}