using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using HashService;

namespace Hangout
{
    public partial class SignUp : System.Web.UI.Page
    {
        public static string captchaImageString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //regenerate();
           

        }
        private byte[] imageToByteArray(System.Drawing.Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); //convert image to byte
                return ms.ToArray();
            }
        }
        protected void SignUpButton_Click(object sender, EventArgs e)
        {
            /*Sanity check and vrfication of Captcha entered*/
            if (TextBox1.Text.Length != 0 && TextBox2.Text.Length != 0)
            {
                if (captchaUserInput.Text != null)
                {
                    Console.WriteLine(captchaImageString);
                    if (captchaUserInput.Text.Equals(captchaImageString))
                    {
                        bool isStaff = CheckBoxIsStaff.Checked;
                        string xmlFilePath;
                        XmlDocument xmlDoc;
                        if (isStaff)//load appropriate xml 
                        {

                            xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Staff.xml");
                            xmlDoc = new XmlDocument();
                            xmlDoc.Load(xmlFilePath);
                        }
                        else
                        {

                            xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/member.xml");
                            xmlDoc = new XmlDocument();
                            xmlDoc.Load(xmlFilePath);
                        }


                        XmlNode usersNode = xmlDoc.SelectSingleNode("/Users"); //Create Users under Users list

                        if (usersNode != null)
                        {
                            XmlNode newUserNode = xmlDoc.CreateElement("User");

                            XmlNode usernameNode = xmlDoc.CreateElement("Username");
                            usernameNode.InnerText = TextBox1.Text;

                            XmlNode passwordNode = xmlDoc.CreateElement("Password");
                            passwordNode.InnerText = HashService.HashHelper.sha256_hash(TextBox2.Text);

                            newUserNode.AppendChild(usernameNode);
                            newUserNode.AppendChild(passwordNode);

                            usersNode.AppendChild(newUserNode);

                            xmlDoc.Save(xmlFilePath);

                        }
                        Label1.Text = "Sign Up Success!";
                        HttpContext.Current.Session["RequestedUrl"] = null;//clear if any
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Invalid Captcha! " + captchaUserInput.Text + " ==> " + captchaImageString;//display msg
                    }

                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Invalid Captcha! " + captchaUserInput.Text + " ==> " + captchaImageString; //display msg
                }

               

                /*if (TextBox1.Text != null && TextBox2.Text != null)
                {
                    bool isStaff = CheckBoxIsStaff.Checked;

                    if (isStaff)
                    {

                    }
                    else { 



                    }
                    HttpCookie httpCookie = new HttpCookie("StaffCookieId");
                    httpCookie["StaffUsername"] = TextBox1.Text;
                    httpCookie["StaffPassword"] = TextBox2.Text;
                    httpCookie.Expires = DateTime.Now.AddDays(30);

                    string xmlFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Staff.xml");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFilePath);
                    XmlNodeList userList = xmlDoc.SelectNodes("/Users/User");

                    foreach (XmlNode userNode in userList)
                    {
                        XmlNode usernameNode = userNode.SelectSingleNode("Username");
                        XmlNode passwordNode = userNode.SelectSingleNode("Password");

                        if (usernameNode != null && passwordNode != null)
                        {
                            string storedUsername = usernameNode.InnerText.Trim();
                            string storedPassword = passwordNode.InnerText.Trim();

                            if (storedUsername == TextBox1.Text)
                            {
                                // Utitlity utitlity = new Utitlity();
                                string passwordHashed = HashService.HashHelper.sha256_hash(storedPassword);
                                if (storedPassword.Equals(passwordHashed))
                                {
                                    Label1.Text = "Login Successful, " + TextBox1.Text;
                                    Response.Redirect("Staff.aspx");
                                }
                                else
                                {
                                    Label1.Text = "Login Failed Check Password";
                                }


                            }
                        }
                    }

                    Response.Cookies.Add(httpCookie);
                }
                else
                {
                    Label1.Text = "username or password missing";
                }*/
            }
            else {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Enter Inputs!!";
            }

        }

        protected void RegenerateCaptcha_Click(object sender, EventArgs e)
        {
            string passwordValue = TextBox2.Text;

            // Regenerate the captcha
            regenerate();

            // Restore the TextBox2.Text value
            TextBox2.Text = passwordValue;
        }
        private void regenerate()
        {
            var service = new CapthaService.ServiceClient();
            captchaImageString = service.GetVerifierString("6");
            // Console.WriteLine(captchaImageString);
            Stream s = service.GetImage(captchaImageString);
            System.Drawing.Image image = System.Drawing.Image.FromStream(s);
            captchaImageBox.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageToByteArray(image));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}