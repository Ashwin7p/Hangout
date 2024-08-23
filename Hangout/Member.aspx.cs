using System;
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
using Microsoft.Ajax.Utilities;
using System.Xml.Linq;
using RestSharp;
namespace Hangout
{
    public partial class Member : System.Web.UI.Page
    {
        public string uid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Authentication to Access the member page*/
            if (HttpContext.Current.Session["IsAutheticated"] != null && (bool)HttpContext.Current.Session["IsAutheticated"])
            {
                if (HttpContext.Current.Session["IsMember"] != null && (bool)HttpContext.Current.Session["IsMember"])
                {
                    if (HttpContext.Current.Session["MemberUsername"] != null)
                    {
                        Label13.Text = (string)HttpContext.Current.Session["MemberUsername"];//The active member user stored
                        uid = (string)HttpContext.Current.Session["MemberUsername"];//The active member user stored

                    }
                    else if (HttpContext.Current.Session["StaffUsername"] != null)
                    {
                        Label13.Text = (string)HttpContext.Current.Session["StaffUsername"];//The active staff user stored
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
        protected void ClearAllCookies()
        {
            if (Request.Cookies != null)
            {
                foreach (string cookie in Request.Cookies.AllKeys)
                {
                    HttpCookie currentCookie = Request.Cookies[cookie];
                    currentCookie.Expires = DateTime.Now.AddDays(-1); // Set the cookie's expiration date to the past
                    Response.Cookies.Add(currentCookie); // Add the updated cookie to the response
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ClearAllCookies();
            Response.Redirect("Default.aspx");//Home
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client getNearByStoresService = new ServiceReference1.Service1Client();//Connect to Backend Services
            ServiceReference1.TRoot root = new ServiceReference1.TRoot();
            root = getNearByStoresService.GetNearbyPlaces(TextBox5.Text, DropDownList3.SelectedValue, TextBox3.Text);


            if (root != null)//valid zip code entered
            {
                if (root.message == "")//successful response from API request
                {
                    if (root.results == null || root.results.Length == 0)
                    {
                        Label5.Text = "No Results found for the input Details !!"; // Only US zip codes please!!

                    }
                    else
                    {
                        string redirectURL = "StoreDetails.aspx";

                        string combinedText = "<ul id=\"dynamicList1\">";

                        foreach (var item in root.results)
                        {

                            combinedText += "<li><a href='" + redirectURL + "'>" + item.name + "," + item.location.formatted_address + "</li>";
                            HttpContext.Current.Session["storename"] = item.name;
                            HttpContext.Current.Session["address"] = item.location.formatted_address;
                            HttpContext.Current.Session["zipcode"] = TextBox3.Text;
                            HttpContext.Current.Session["storeType"] = DropDownList3.SelectedValue;

                        }
                        combinedText += "</ul> ";

                        Label5.Text = combinedText;

                    }
                }
                else
                {
                    Label5.Text = root.message;// Display error message to user
                }
            }
            else
            {

                Label5.Text = " Please enter valid ZipCode";//invalid zip code entered
            }

            /*Record Search History of User*/
            string fileLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"APP_Data/history.xml");

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

                //Id
                //StoreName
                //StoreType
                //zipcode
                //timestame
                var newStoreSearched = new XElement("Store", new XElement("StoreName", TextBox5.Text),
                new XElement("Zipcode", TextBox3.Text),
                new XElement("SortBy", DropDownList3.SelectedValue),//details made blank
                new XElement("TimeStamp", DateTime.Now));
                user.Add(newStoreSearched);
                xml.Save(fileLocation);

            }
            finally
            {

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}