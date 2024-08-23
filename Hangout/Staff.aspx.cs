using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Data.SqlTypes;

namespace Hangout
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Check validation for the page*/
            if (HttpContext.Current.Session["IsAutheticated"] != null && (bool)HttpContext.Current.Session["IsAutheticated"])
            {
                if (HttpContext.Current.Session["IsStaff"] != null && (bool)HttpContext.Current.Session["IsStaff"])
                {

                }
                else
                {
                    HttpContext.Current.Session["msg"] = "Please Login as Staff!!";
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
            Response.Redirect("Default.aspx");
        }

        protected void GrantAdminBtn_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["IsAutheticated"] != null && (bool)HttpContext.Current.Session["IsAutheticated"])
            {
                if (HttpContext.Current.Session["IsStaff"] != null && (bool)HttpContext.Current.Session["IsStaff"])
                {
                    if (checkMember(AdminUsername.Text))//first check if the user is member
                    {
                        makeAdmin(AdminUsername.Text);//then add to admin
                        AdminStatus.Text = AdminUsername.Text + " now has Admin Access";
                    }
                    else
                    {
                        AdminStatus.Text = "The User:" + AdminUsername.Text + " is not a Member!!";
                    }

                }
            }
        }
        public bool checkMember(string username)
        {
            XmlDocument member_base = new XmlDocument();
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(fLocation));
                fLocation = Path.Combine(fLocation, "member.xml");
                member_base.Load(fLocation);
            }
            catch
            {
                return false;
            }
            XmlNode userNode = member_base.SelectSingleNode($"//User[normalize-space(Username)='{username}']");
            if (userNode != null)
            {
                // Username exists, now check the password
                return true;
            }

            return false;
        }
        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");

        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            ClearAllCookies();
            Response.Redirect("Default.aspx");
        }
        public string getPass(string username)
        {
            XmlDocument member_base = new XmlDocument();
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(fLocation));
                fLocation = Path.Combine(fLocation, "member.xml");
                member_base.Load(fLocation);
            }
            catch
            {
                return false.ToString();
            }
            XmlNode userNode = member_base.SelectSingleNode($"//User[normalize-space(Username)='{username}']");
            if (userNode != null)
            {
                // Username exists, now check the password
                return userNode.SelectSingleNode("Password").InnerText;

            }
            return null;
        }
        public bool makeAdmin(string username)
        {
            /*AddedControl to xml*/
            XmlDocument member_base = new XmlDocument();
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(fLocation));
                fLocation = Path.Combine(fLocation, "Staff.xml");
                member_base.Load(fLocation);
            }
            catch
            {
                return false;
            }
            XmlNodeList userNodes = member_base.SelectNodes($"//User[normalize-space(Username)='{username}']");

            if (userNodes.Count == 0)
            {
                // Username doesn't exist, so add the new user
                XmlElement newUser = member_base.CreateElement("User");

                XmlElement usernameElement = member_base.CreateElement("Username");
                usernameElement.InnerText = username;
                newUser.AppendChild(usernameElement);
                XmlElement passwordElement = member_base.CreateElement("Password");
                passwordElement.InnerText = getPass(username);
                newUser.AppendChild(passwordElement);

                member_base.DocumentElement.AppendChild(newUser);

                // Save the XML document
                member_base.Save(fLocation);

                return true; // User added successfully
            }
            return false;
        }

        protected void username_input_TextChanged(object sender, EventArgs e)
        {

        }
        public bool checkAdmin(string username)
        {/*Check if already an admin*/
            XmlDocument member_base = new XmlDocument();
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(fLocation));
                fLocation = Path.Combine(fLocation, "Staff.xml");
                member_base.Load(fLocation);
            }
            catch
            {
                return false;
            }
            XmlNode userNode = member_base.SelectSingleNode($"//User[normalize-space(Username)='{username}']");
            if (userNode != null)
            {
                // Username exists, now check the password
                return true;
            }

            return false;
        }
        protected void make_admin_Click(object sender, EventArgs e)
        {
            if (username_input.Text != null && pwd_input.Text != null)
            {
                if (!checkAdmin(username_input.Text))
                {
                    string xmlFilePath;
                    XmlDocument xmlDoc;
                    xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Staff.xml");//select staff file
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFilePath);
                    XmlNode usersNode = xmlDoc.SelectSingleNode("/Users");
                    if (usersNode != null)
                    {
                        XmlNode newUserNode = xmlDoc.CreateElement("User");

                        XmlNode usernameNode = xmlDoc.CreateElement("Username");
                        usernameNode.InnerText = username_input.Text;

                        XmlNode passwordNode = xmlDoc.CreateElement("Password");
                        passwordNode.InnerText = HashService.HashHelper.sha256_hash(pwd_input.Text);//add inputs afetr hashing password

                        newUserNode.AppendChild(usernameNode);
                        newUserNode.AppendChild(passwordNode);

                        usersNode.AppendChild(newUserNode);

                        xmlDoc.Save(xmlFilePath);
                        AdminStatus.Text = username_input.Text + " now has Admin Access !!";

                    }

                }
                else
                {
                    AdminStatus.Text = "User" + username_input.Text + " Already an Admin!!";
                }
            }
            else
            {
                AdminStatus.Text = "Credentials missing!!";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var fileLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"APP_Data/history.xml");

            if (File.Exists(fileLocation))
            {
                try
                {
                    var xmlStr = File.ReadAllText(fileLocation);
                    var xDoc = XElement.Parse(xmlStr);

                    var users = xDoc.Elements("user");

                    // Reset the textbox for each press on the button
                    TextBox1.Text = "";
                    string result_txt = "";

                    foreach (var user in users)
                    {
                        string userID = user.Element("ID").Value;
                        result_txt += "User ID: " + userID + "\n";

                        var stores = user.Elements("Store").ToList();

                        foreach (var store in stores)
                        {
                            result_txt += "Store Name: " + store.Element("StoreName").Value + "\n";
                            result_txt += "Zipcode: " + store.Element("Zipcode").Value + "\n";
                            result_txt += "Sort By: " + store.Element("SortBy").Value + "\n";
                            result_txt += "Timestamp: " + store.Element("TimeStamp").Value + "\n";
                            result_txt += "\n";
                        }
                    }

                    if (result_txt == "") // If no store information found
                    {
                        TextBox1.Text = "No store information available";
                    }
                    else
                    {
                        TextBox1.Text = result_txt;
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    TextBox1.Text = "Error occurred while processing the XML";
                }
            }


        }

    }
}