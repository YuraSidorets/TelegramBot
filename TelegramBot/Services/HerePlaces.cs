using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

using HerePlacesCSharp;

using Newtonsoft.Json;

namespace TelegramBot.Services
{
    public class HerePlaces : IPlacesService
    {
        private PlacesService AssignApiKey()
        {
            return new PlacesService("VKJH0old5VBvC29iPff3", "3l-vJPEKPy-8D8RhvhWw1A");
        }

        public string FindPlace(double latitude, double longitude)
        {
            var placesService = AssignApiKey();

            var placesResponse = placesService.ListPlacesAroundLocation(new GeoCoordinate(latitude, longitude), 3000, "going-out");
            
            var places = new StringBuilder();
            foreach (var place in placesResponse.Result)
            {
                var placepos = place.GeoCoordinates.Latitude.ToString(CultureInfo.CurrentCulture).Replace(".", "d") + "k" +
                               place.GeoCoordinates.Longitude.ToString(CultureInfo.CurrentCulture).Replace(".", "d");
                places.Append("Name:  " + place.Title + "\n");
                places.Append("Place Location: /Map" + placepos + " tap to see map \n");
                places.Append("------------------------------\n");
            }

            return places.ToString();
        }

        public string GetMap(string placeLoc)
        {   
            var position = placeLoc.Split(',');

            var placesService = AssignApiKey();
            var placesResponse = placesService.ListPlacesAroundLocation(new GeoCoordinate(double.Parse(position[0]), double.Parse(position[1])), 500, "going-out");

            var placeView = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                var place = httpClient.GetStringAsync(placesResponse.Result.FirstOrDefault().Url).Result;
                dynamic data = JsonConvert.DeserializeObject(place);
                placeView = data.view;
            }

            return placeView; //$"https://wego.here.com/p/?map={position[0]},{position[1]},15,normal&x=ep";
        }
    }
}