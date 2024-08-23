using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Hangout
{
    public partial class StoreDetails : System.Web.UI.Page
    {
        public string uid = "";
        public string address = "";
        public string storeName = "";
        public string storeType = "";
        public string zipCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["storename"] != null && (string)HttpContext.Current.Session["storename"] != "")
            {
                Label1.Text = (string)HttpContext.Current.Session["storename"];
                Label2.Text = (string)HttpContext.Current.Session["address"];

                storeName= (string)HttpContext.Current.Session["storename"];
                address= (string)HttpContext.Current.Session["address"];
                zipCode= (string)HttpContext.Current.Session["zipcode"];
                storeType= (string)HttpContext.Current.Session["storeType"];
            }
            else { 
                //noting passed so keep label name
            }

            if (HttpContext.Current.Session["IsAutheticated"] != null && (bool)HttpContext.Current.Session["IsAutheticated"])
            {
                if (HttpContext.Current.Session["IsMember"] != null && (bool)HttpContext.Current.Session["IsMember"])
                {
                    if (HttpContext.Current.Session["MemberUsername"] != null)
                    {
                        uid = (string)HttpContext.Current.Session["MemberUsername"];//The active member user stored
                    }
                    else if (HttpContext.Current.Session["StaffUsername"] != null)
                    {
                        uid = (string)HttpContext.Current.Session["StaffUsername"];//The active staff user stored
                    }
                }
                else
                {
                    HttpContext.Current.Session["msg"] = "Please Login as Member!!";//If Not Auth redirect to login page
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
              
                /*Get Reviews for a Storename and zipcode*/
                ServiceReference1.Service1Client getReviews = new ServiceReference1.Service1Client();
                string storename = (string)HttpContext.Current.Session["storename"];
                string adrs = (string)HttpContext.Current.Session["address"];
                string zipcode = (string)HttpContext.Current.Session["zipcode"]; //get the session data stored vals

                string res = getReviews.GetReviews(zipcode,storename); //call the service

                if (res != null)// Handle success response
                {
                    if (res.Length == 0)
                    {
                        Label3.Text = "Invalid Inputs!!";
                    }
                    else
                    {
                        string decodedcontent = HttpUtility.HtmlDecode(res);//get the html response
                        Label3.Text = decodedcontent;

                    }
                }
                else
                {
                    Label3.Text = "Error occured in Service!!";
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "Exception" + ex;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client getWeatherForecastService = new ServiceReference1.Service1Client();
            ServiceReference1.WStrucResult resultsList = new ServiceReference1.WStrucResult();
            string zipcode = (string)HttpContext.Current.Session["zipcode"];

            resultsList = getWeatherForecastService.GetWeatherForecast(zipcode);


            if (resultsList != null) //valid zip code entered
            {
                if (resultsList.message == "") //successful response from API request
                {
                    string combinedText = "<h2> Place: " + resultsList.place + "</h2>";
                    if (resultsList.values == null || resultsList.values.Length == 0)
                    {
                        Label4.Text = "No Weather Forecast found for the input ZipCode !!"; // Only US zip codes please!!

                    }
                    else
                    {
                        combinedText += "<ul id=\"dynamicList2\">";

                        foreach (var item in resultsList.values)
                        {

                            string[] valuesObtained = item.Split(','); //Get the values using split operator, as response is sent in a standard
                            combinedText += "<li>" + "Date: " + valuesObtained[0] + ", " + "Precipitation Probability: " + valuesObtained[1] + ", " + "Min Temperature: " + valuesObtained[2] + "&deg;F, " + "Max Temperature: " + valuesObtained[3] + "&deg;F </li>";

                        }
                        combinedText += "</ul> ";

                        Label4.Text = combinedText;
                    }
                }
                else
                {
                   Label4.Text = resultsList.message; // Display error message to user
                }
            }
            else
            {

                Label4.Text = " Please enter valid ZipCode";//invalid zip code entered
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            string fileLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"APP_Data/Likes.xml");

            try
            {


                XElement xml = XElement.Load(fileLocation);

                //this line has the hard-coded user id, can be changed into the current user id once integrated
                //if user currently has no liked stores
                if (xml.Elements("user").Where(x => x.Element("ID").Value.Equals(uid)).ToList().Count == 0)
                {
                    //again this new user id will be changed into the current user id
                    XElement newuser = new XElement("user",
                                new XElement("ID", uid));
                    xml.Add(newuser);
                }
                XElement user = xml.Elements("user").Where(x => x.Element("ID").Value.Equals(uid)).ToList()[0];
                //check if the store has already been liked
                if (user.Elements("Store").Where(x => x.Element("Address").Value.Equals(address)).ToList().Count == 0)
                {
                    var newStore = new XElement("Store", new XAttribute("type", storeType),
                           new XElement("Name", storeName),
                           new XElement("Address", address),
                    new XElement("Zipcode", zipCode),
                    new XElement("Details", ""),//details made blank
                    new XElement("Reviews",
                    new XElement("Review", new XAttribute("star", ""), ""),
                           new XElement("Review", new XAttribute("star", ""), "")));
                    user.Add(newStore);
                    xml.Save(fileLocation);
                    Label5.Text = "Successfully liked this store";
                }
                else
                {
                    Label5.Text = "This store has already been liked";
                }


            }
            finally
            {

            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {//redirect to home page
            Response.Redirect("Default.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {


            string fileLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"APP_Data/Likes.xml");

            if (File.Exists(fileLocation))
            {
                try
                {
                    var xmlStr = File.ReadAllText(fileLocation);
                    var likesXml = XElement.Parse(xmlStr);

                    var currentUserLikes = likesXml.Elements("user")
                        .Where(user => user.Elements("Store")
                            .Any(store => store.Element("Name").Value.Equals(storeName))) // Get current storename liked users
                        .ToList();

                    if (currentUserLikes.Count > 0)
                    {
                        string usersLikedStore = "<h2>Other Users Who Liked the same Store !!</h2>";
                        foreach (var user in currentUserLikes)
                        {
                            usersLikedStore += "<li>User ID: " + user.Element("ID").Value + "</li><br>";//disolay in list output
                            usersLikedStore += "<br>";
                        }
                        Label8.Text = usersLikedStore;
                    }
                    else
                    {
                        Label8.Text = "No users have liked this store yet.";
                    }
                }
                catch (System.Exception ex)
                {
                    Label8.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}