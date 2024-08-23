using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hangout
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        public string Errormsg { get; set; } // Ensure Errormsg is declared at the class level

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["error"]))
                {
                    Errormsg = Request.QueryString["error"];
                }
                else
                {
                    Errormsg = "An error occurred.";
                }
            }

        }
        protected void home_button_clicked(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}