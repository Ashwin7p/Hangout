using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace Hangout
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            // ClearAllCookies();

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
            // ClearAllCookies();
        }
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            /*Create session data to control the access to the pages*/
            
                if (HttpContext.Current.Session != null)
                {
                /*check if coockie is set already*/
                HttpCookie staffCookies = Request.Cookies["StaffCookieId"];
                HttpCookie memberCookies = Request.Cookies["MemberCookieId"];
                if (memberCookies != null)
                {
                    HttpContext.Current.Session["IsAutheticated"] = true;
                    HttpContext.Current.Session["IsMember"] = true;
                    HttpContext.Current.Session["IsStaff"] = false;
                    HttpContext.Current.Session["MemberUsername"] = memberCookies["MemberUsername"];
                }
                if (staffCookies != null)
                {
                    HttpContext.Current.Session["IsAutheticated"] = true;
                    HttpContext.Current.Session["IsMember"] = true;
                    HttpContext.Current.Session["IsStaff"] = true;
                    HttpContext.Current.Session["StaffUsername"] = staffCookies["StaffUsername"];

                }

            }
            
        }

        void Application_Error(object sender, EventArgs e) 
        {
            Exception ex = Server.GetLastError();//get the error to handle
            /*if (ex != null) {
                string errorMessage = "An error occurred: ";
                if (ex is System.Web.HttpUnhandledException)
                {
                    errorMessage += ex.InnerException;
                }
                // string errorMessage = "An error occurred: " + ex.Message;
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

                Console.WriteLine(ex.StackTrace);

                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");


                Response.Redirect("~/ErrorPage.aspx?error="+ errorMessage);
            }*/

            string errorMessage = "An error occurred: ";
            if (ex is System.Web.HttpUnhandledException)
            {
                errorMessage += HttpUtility.UrlEncode(ex.InnerException.ToString());
            }
            else
            {
                errorMessage += HttpUtility.UrlEncode(ex.Message);
            }
            Response.Redirect("~/ErrorPage.aspx?error=" + errorMessage); //redirect to error page with exception message
            Server.ClearError();


        }
    }
}