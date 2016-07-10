using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.Places;
using Google.Maps.StaticMaps;

namespace TelegramBot.DataHelpers
{
    public class GooglePlaces
    {
        private  static void AssignApiKey()
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyB6Mpkjz3RflA7_-JA9wWDfiTZmIwSu2-c"));
        }

        public static string FindPlace(double latitude, double longitude)
        {
            AssignApiKey();
            var placesRequest = new NearbySearchRequest();
            placesRequest.Location = new LatLng(latitude, longitude);
            placesRequest.Types = new[] { PlaceType.Bar, PlaceType.Cafe, PlaceType.ArtGallery, PlaceType.Food, PlaceType.Restaurant, PlaceType.MovieTheater, PlaceType.Park };
            placesRequest.Radius = 1000;
            placesRequest.Sensor = true;
            placesRequest.RankBy = RankBy.Distance;
            placesRequest.OpenNow = true;
           
            var placesResponse = new PlacesService().GetResponse(placesRequest);
            
            StringBuilder places = new StringBuilder();
            foreach (var place in placesResponse.Results)
            {
                var placepos = place.Geometry.Location.Latitude.ToString(CultureInfo.CurrentCulture).Replace(".", "d") + "k" +
                               place.Geometry.Location.Longitude.ToString(CultureInfo.CurrentCulture).Replace(".", "d");
               places.Append("Name:  "+place.Name+"\n");
               places.Append("Rating: " + place.Rating + "\n");
               places.Append("Place Location: /Map" + placepos + " \n");
               places.Append("-------------------------------\n");
            }

            return places.ToString();
        }

        public static string GetMap(string placeLoc)
        {
            AssignApiKey();

            var position = placeLoc.Split(',');
            Location location = new Location($"{position[0]},{position[1]}");

            var map = new StaticMapRequest();
            map.Center = location;
            map.Size = new System.Drawing.Size(400, 400);
            map.Zoom = 14;
            map.Sensor = false;

            map.Markers = new MapMarkersCollection() { location };

            return map.ToUri().ToString();
        }
    }
}