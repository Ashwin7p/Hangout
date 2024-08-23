using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string HashPassword(string val); // Hash the code

        [OperationContract]
        String GetReviews(string zipcode, string storename);//Gets review for the store and place input
                                                            //candidate, place,result, review,Root,StrucResult


        [OperationContract]
        TRoot GetNearbyPlaces(string storename, string sortByType, string zipcode); //Fetch all the Places(stores) for the User as per his inputs

        [OperationContract]
        WStrucResult GetWeatherForecast(string zipcode); // Provides the 5 day weather forecast for the zipcode
    }
    public class WRoot
    {
        public Timelines timelines { get; set; }
        public WLocation location { get; set; }
    }
    public class Timelines
    {
        public List<Daily> daily { get; set; }
    }

    public class Daily
    {
        public DateTime time { get; set; }
        public Values values { get; set; }
    }
    public class Values
    {
        public int precipitationProbability { get; set; }

        public double humidityAvg { get; set; }
        public double humidityMax { get; set; }
        public double humidityMin { get; set; }

        public DateTime? moonriseTime { get; set; }
        public DateTime moonsetTime { get; set; }
        public double precipitationProbabilityAvg { get; set; }
        public int precipitationProbabilityMax { get; set; }
        public int precipitationProbabilityMin { get; set; }


        public DateTime sunriseTime { get; set; }
        public DateTime sunsetTime { get; set; }

        public double temperatureAvg { get; set; }
        public double temperatureMax { get; set; }
        public double temperatureMin { get; set; }

        public double visibilityAvg { get; set; }
        public double visibilityMax { get; set; }
        public double visibilityMin { get; set; }

        public double windSpeedAvg { get; set; }
        public double windSpeedMax { get; set; }
        public double windSpeedMin { get; set; }
    }
    public class WLocation
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    public class Candidate
    {
        public string place_id { get; set; }
        public string name { get; set; }

    }

    public class Place
    {
        public List<Candidate> candidates { get; set; }
        public string status { get; set; }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Result
    {
        public List<Review> reviews { get; set; }
    }

    public class Review
    {
        public string author_name { get; set; }
        public string author_url { get; set; }
        public int rating { get; set; }
        public string relative_time_description { get; set; }
        public string text { get; set; }
    }

    public class Root
    {
        public List<object> html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }

    public class StrucResult
    {
        public string message { get; set; }
        public string place { get; set; }
        public Result result { get; set; }
    }
    //////

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Icon icon { get; set; }
    }

    public class Center
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Chain
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Child
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class Circle
    {
        public Center center { get; set; }
        public int radius { get; set; }
    }

    public class Context
    {
        public GeoBounds geo_bounds { get; set; }
    }

    public class GeoBounds
    {
        public Circle circle { get; set; }
    }

    public class Geocodes
    {
        public Main main { get; set; }
        public Roof roof { get; set; }
    }

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string address_extended { get; set; }
        public string census_block { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string dma { get; set; }
        public string formatted_address { get; set; }
        public string locality { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Parent
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public Parent parent { get; set; }
        public List<Child> children { get; set; }
    }

    public class TResult
    {

        public string fsq_id { get; set; }
        public List<Category> categories { get; set; }
        public List<Chain> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public string link { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }
    }

    public class Roof
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class TRoot
    {
        public string message { get; set; }
        public List<TResult> results { get; set; }
        public Context context { get; set; }
    }
    public class WStrucResult
    {
        public string message { get; set; }
        public string place { get; set; }
        public List<string> values { get; set; }
    }
}