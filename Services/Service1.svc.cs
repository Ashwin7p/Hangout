using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Xml.Linq;
using HashService;//dll
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public WStrucResult GetWeatherForecast(string zipcode)
        {
            /*
            This acts as a controller to use the available services and fetch the results
            */

            string selectedPlace = GetPlaceFromZipCode(zipcode);// get the place name from zipcode entered 
            if (selectedPlace != null)
            {
                WRoot output = new WRoot();
                WStrucResult weather5DayReport = Weather5day(selectedPlace, ref output); // pass output by reference
                return weather5DayReport;
            }

            return null;// retuen null if not valid zip code

        }
        public WStrucResult Weather5day(string place, ref WRoot output)
        {
            /*
              Fetches the 5 days weather report from the https://api.tomorrow.io, for the input location
              The search in the above api reference matches any geaocoded location possible even if number is input.
             */

            WStrucResult weatherReport = new WStrucResult();
            weatherReport.place = null;
            weatherReport.values = new List<string>();
            weatherReport.message = "";
            // Send a request to https://api.tomorrow.io API
            RestClient client = new RestClient("https://api.tomorrow.io/v4/weather/forecast?location=" + place + "&timesteps=1d&apikey=NzoMf0gnWmW9hOfQbpLTdo4mG35DSAyk");

            //RestRequest for the required endpoint
            RestRequest request = new RestRequest("", Method.Get);
            request.AddHeader("accept", "application/json");


            try
            {
                // Send the Request
                RestResponse response = client.Execute(request);

                // Check the response status
                if (response.IsSuccessful)// Handle success response
                {
                    weatherReport.message = "";
                    // Handle the successful response
                    string content = response.Content;
                    //  dynamic jsonObject = JsonConvert.DeserializeObject(content);
                    WRoot myDeserializedClass = JsonConvert.DeserializeObject<WRoot>(content);
                    output = myDeserializedClass;
                    double temperatureMin;
                    double temperatureMax;
                    int precipitationProbability;
                    weatherReport.place = output.location.name;
                    foreach (Daily day in output.timelines.daily)
                    {
                        temperatureMin = ((day.values.temperatureMin * 9) / 5 + 32);//convert to Fahrenheit from celcius
                        temperatureMax = ((day.values.temperatureMax * 9) / 5 + 32);//convert to Fahrenheit from celcius
                        precipitationProbability = day.values.precipitationProbability;
                        weatherReport.values.Add(day.time + "," + day.values.precipitationProbability.ToString() + "," + Convert.ToInt32(temperatureMin).ToString() + "," + Convert.ToInt32(temperatureMax).ToString());
                    }
                    return weatherReport;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    weatherReport.values = null;//Bad inputs from client
                    return weatherReport;
                }
                else
                {
                    weatherReport.message = "Error: " + response.ErrorMessage;// set output error message to handle user interaction 
                    return weatherReport;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                weatherReport.message = "Error: " + ex.Message;// set output error message to handle user interaction 
                return weatherReport;
            }

        }
        public string  HashPassword(string val)
        {/*DLL Class Library called with reference*/
            return HashService.HashHelper.sha256_hash(val);
        }

        public void PutStringToFile(string fileName, string value)
        {
            /*
               Put the Average Review score rating obtained for the store 
             */
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data"); // Get path from server root to current
            fLocation = Path.Combine(fLocation, fileName); // From current to App_Data
            using (StreamWriter sw = File.CreateText(fLocation))
            {
                if (value.Length > 50)
                    value = value.Substring(0, 50); // Not allow long string in demo code
                sw.WriteLine(value);
            }
        }

        public String GetReviews(string zipcode, string storename)
        {
            /*
                Returns the google reviews obtained for the place id that is obtained from the combination of store name and zipcode
             */
            string selectedPlace = GetPlaceFromZipCode(zipcode); // get the place name from zipcode entered 
            if (selectedPlace != null)
            {
                Root output = new Root();
                StrucResult st = Get_Reviews(storename, selectedPlace); // pass output by reference
                if (st == null)
                {
                    return null; //if no results
                }
                else
                {
                    if (st.result != null && st.result.reviews != null)
                    {
                        int mcount = 0;
                        int mrating = 0;
                        string combinedText = "<h6>" + st.place + "</h6><ul id=\"dynamicList1\">";
                        //keep looping and adding review ratings and scores
                        foreach (var item in st.result?.reviews)
                        {
                            mcount++;
                            mrating += item.rating;
                            combinedText += "<li> <b>Author:</b> " + item.author_name + "</li>";
                            combinedText += "<li> <b>Rating:</b> " + item.rating + ", Relative Time: " + item.relative_time_description + "</li>";
                            combinedText += "<li> <b>text:</b> " + item.text + "<br/><br/><hr/></li>";
                        }
                        string ratingavg = (mrating / mcount).ToString();

                        combinedText += "</ul> ";
                        try
                        {
                            PutStringToFile(storename + "_" + zipcode, ratingavg);//store
                        }
                        catch (Exception e)
                        {//do nothing
                        }

                        return combinedText;

                    }
                    else
                    {
                        string combinedText = "<h6> No Match for input!!</h6>";
                        return combinedText;

                    }


                }

            }

            return null;

        }
        public string GetPlaceFromZipCode(string zipcode)
        {
            /*
              The function fetches the place name(address) from the zipcode input. It uses Google Maps Geocoding API.    
            */
            string apiKey = "AIzaSyCOSysGSaYDdoU4nQKV-NEAOpvAZ2YzP_A"; // api key for fetching results from google api
            {
                try
                {
                    // Send a request to the Geocoding API
                    RestClient client1 = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:" + zipcode + "&key=" + apiKey);

                    //RestRequest for the required endpoint
                    RestRequest request = new RestRequest("", Method.Get);
                    request.AddHeader("accept", "application/json");
                    // Send the Request
                    RestResponse response = client1.Execute(request);

                    if (response.IsSuccessful) // Handle success response
                    {
                        // Handle the successful response
                        string content = response.Content;
                        string place = GetParsedJsonPlacename(content);
                        return place;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null; //for failed response
                }
            }
        }


        static string GetParsedJsonPlacename(string responseBody)
        {
            /*
             Simple function to provide the address of the place
            */
            JObject json = JObject.Parse(responseBody);
            string formattedAddress = json["results"][0]["formatted_address"].ToString();
            return formattedAddress;
        }

        static List<String> GetParsedJsonPlaceId(string responseBody)
        {
            /*
             Simple function to provide place id from json
            */

            JObject json = JObject.Parse(responseBody);
            List<String> rs = new List<String>();
            string firstPlace = json["candidates"][0]["place_id"].ToString();
            rs.Add(firstPlace);
            firstPlace = json["candidates"][0]["name"].ToString();
            rs.Add(firstPlace);
            return rs;
        }


        List<String> GetPlaceId(string selectedPlace, string storename)
        {
            /*
              Fetching Place id from input combination
             */
            string apiKey = "AIzaSyCOSysGSaYDdoU4nQKV-NEAOpvAZ2YzP_A"; // api key for fetching results from google api
            {
                try
                {
                    // Send a request
                    RestClient client1 = new RestClient("https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + storename + " " + selectedPlace + "&inputtype=textquery&fields=name,place_id&key=" + apiKey);

                    //RestRequest for the required endpoint
                    RestRequest request = new RestRequest("", Method.Get);
                    request.AddHeader("accept", "application/json");
                    // Send the Request
                    RestResponse response = client1.Execute(request);

                    if (response.IsSuccessful) // Handle success response
                    {
                        // Handle the successful response
                        string content = response.Content;
                        List<String> firstPlace = GetParsedJsonPlaceId(content);
                        return firstPlace;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null; //for failed response
                }
            }


        }
        StrucResult Get_Reviews(string storename, string selectedPlace)
        {
            //Google api to fetch Reviews for input combination

            StrucResult st = new StrucResult();
            st.message = "";
            selectedPlace = selectedPlace.Split(',')[0];
            st.place = selectedPlace;
            List<String> placeDetails = GetPlaceId(selectedPlace, storename);
            if (placeDetails != null)
            {
                st.place = placeDetails?[1] + " , place_id: " + placeDetails?[0];
                try
                {
                    // Send a request

                    string apiKey = "AIzaSyCOSysGSaYDdoU4nQKV-NEAOpvAZ2YzP_A"; // api key for fetching results from google api

                    RestClient client = new RestClient("https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeDetails[0] + "&fields=reviews&key=" + apiKey);

                    //RestRequest for the required endpoint
                    RestRequest request = new RestRequest("", Method.Get);
                    request.AddHeader("accept", "application/json");
                    RestResponse response = client.Execute(request);
                    if (response.IsSuccessful)// Handle success response
                    {
                        string content = response.Content;
                        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(content);
                        st.result = myDeserializedClass.result;
                    }
                    else
                    {
                        st.message = "Error occured in Service!!";
                    }
                }
                catch (Exception ex)
                {
                    st.message = "Error occured in Service!!";
                }
            }
            else
            {
                return null;
            }

            return st;

        }

        public TRoot GetNearbyPlaces(string storename, string sortByType, string zipcode)
        {
            /*
             This acts as a controller to use the available services and fetch the results
             */

            string selectedPlace = GetPlaceFromZipCode(zipcode); // get the place name from zipcode entered 
            if (selectedPlace != null)
            {
                TRoot output = new TRoot();
                // getStores("coffee", "tempe", output);
                getStores(storename, selectedPlace, sortByType, ref output); // pass output by reference
                return output;
            }

            return null; // retuen null if not valid zip code

        }

        public void getStores(string storename, string place, string sortByType, ref TRoot output)      // method to get 5 day forecast at the given zipcode
        {
            /*
             Fetches the nearby places (restaurants, stores etc) for the user inputs and sets the value of output
             The Foursquare.com API fetches results for US Zipcode only
             */

            // Send a request to the Foursquare API
            RestClient client = new RestClient("https://api.foursquare.com/v3/places");

            //RestRequest for the required endpoint
            RestRequest request = new RestRequest("/search", Method.Get);
            request.AddQueryParameter("query", storename);
            request.AddQueryParameter("near", place); //A string naming a locality in the world (e.g., "Chicago, IL"). If the value is not geocodable, returns an error.
            request.AddQueryParameter("sort", sortByType);

            // Adding headers
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "fsq37Qt7Xtgl9hoEECcjjgJKeQvUtCwwCeTssilN4mvjBkQ=");

            try
            {
                // Send the Request
                RestResponse response = client.Execute(request);


                if (response.IsSuccessful)// Handle success response
                {
                    string content = response.Content;
                    TRoot myDeserializedClass = JsonConvert.DeserializeObject<TRoot>(content);
                    output = myDeserializedClass;
                    output.message = "";
                }
                else
                {
                    Console.WriteLine("Error: " + response.ErrorMessage);
                    output.message = "Error: " + response.ErrorMessage;// set output error message to handle user interaction 
                }
            }
            catch (Exception ex) { output.message = "Error: " + ex.Message; } // set output error message to handle user interaction  
        }
    }
}
