var googleMap;

var ViewModel = function () {
    var self = this;
    self.penthouses = ko.observableArray();
    self.error = ko.observable();

    var penthousesUri = '/api/penthouses/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(penthousesUri, 'GET').done(function (data) {
            self.penthouses(data);

            var infowindow = new google.maps.InfoWindow();

            data.forEach((penthouse) => {
                var marker = new google.maps.Marker({
                  position: {
                    lat: penthouse.latitude,
                    lng: penthouse.longitude,
                  },
                  map: googleMap,
                  title: "Hi",
                  label: penthouse.Id,
                  clickable: true
                });


                marker.addListener('click', function() {
                  googleMap.setZoom(5);
                  googleMap.setCenter(marker.getPosition());
                  if (infowindow) {
                        infowindow.close();
                  }
                  var content = '<strong>' + penthouse.name + '</strong><br/>Price: ' + (penthouse.price).formatMoney(2) + ' $';
                  infowindow.setContent(content);
                  infowindow.open(googleMap, marker);
                });
             })
        });
    }

    // Fetch the initial data.
    getAllBooks();
};


function createMap() {
    return new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.166294, lng: -96.389016 },
        zoom: 4
    });
}

google.maps.event.addDomListener(window, 'load', function(){
    googleMap = createMap();
    ko.applyBindings(new ViewModel());

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude
            };
            var marker = new google.maps.Marker({
              position: pos,
              map: googleMap,
              title: "My location",
              label: "YOU",
              clickable: true
            });
          }, function() {
            console.log('Your user location was not fetched successfully!');
          });
        } else {
          console.log('Your browser doesnt support geolocation');
        }

});


Number.prototype.formatMoney = function(c, d, t){
    var n = this, 
        c = isNaN(c = Math.abs(c)) ? 2 : c, 
        d = d == undefined ? "." : d, 
        t = t == undefined ? "," : t, 
        s = n < 0 ? "-" : "", 
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))), 
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};