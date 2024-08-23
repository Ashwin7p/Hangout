using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using HashService;

namespace Hangout
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (HttpContext.Current.Session["msg"] != null) {
                string msg = (string)HttpContext.Current.Session["msg"];
                Label1.Text = msg;
                Label1.ForeColor = System.Drawing.Color.Red;
                HttpContext.Current.Session["msg"] = null; // Clear the stored URL

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*Sanity check of inputs and alow logging in upon success*/
            if ( TextBox1 != null && TextBox2 != null)
            {
                int success = 0;
                string xmlFilePath="";
                HttpCookie httpCookie=null;
                string user = "";
                if (CheckBoxIsStaff.Checked){//is staff
                    httpCookie = new HttpCookie("StaffCookieId");
                    httpCookie["StaffUsername"] = TextBox1.Text;
                    httpCookie["StaffPassword"] = TextBox2.Text;
                    httpCookie.Expires = DateTime.Now.AddMinutes(5);
                    xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Staff.xml");
                    user = "Staff";
                }
                else {
                    httpCookie = new HttpCookie("MemberCookieId");
                    httpCookie["MemberUsername"] = TextBox1.Text;
                    httpCookie["MemberPassword"] = TextBox2.Text;
                    httpCookie.Expires = DateTime.Now.AddMinutes(5);
                    xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/member.xml");
                    user = "Member";
                }


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList userList = xmlDoc.SelectNodes("/Users/User");
                string pwd = HashService.HashHelper.sha256_hash(TextBox2.Text);//hash the password

                foreach (XmlNode userNode in userList)
                {
                    XmlNode usernameNode = userNode.SelectSingleNode("Username");//select the nodes
                    XmlNode passwordNode = userNode.SelectSingleNode("Password");

                    if (usernameNode != null || passwordNode != null)
                    {
                        string storedUsername = usernameNode.InnerText.Trim();
                        string storedPassword = passwordNode.InnerText.Trim();

                        if (storedUsername == TextBox1.Text)
                        {
                            if (storedPassword == pwd)
                            {
                                Label1.Text = "Login Successful, " + user + ": " + TextBox1.Text;
                                success = 1;
                            }
                            else
                            {
                                Label1.Style["color"] = "red";
                                Label1.Text = "Login Failed Check Password";//display msg
                                success = 0;
                            }

                            break;
                        }
                    }
                    else {
                        Label1.Style["color"] = "red";
                        Label1.Text = "Login Failed Check Credentials!!";//display msg
                    }
                }
                if (success == 1)
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
                    Response.Cookies.Add(httpCookie);
                    string url = (string)HttpContext.Current.Session["RequestedUrl"];
                    if (url == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else {
                        Response.Redirect(url);
                    }
                }
                else {
                    Label1.Style["color"] = "red";
                    Label1.Text = "Login Failed Check Credentials!!";//display msg
                }
            }
            else
            {
                Label1.Style["color"] = "red";
                Label1.Text = "username or password missing";//display msg
            }
        }

        protected void SignUpButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}