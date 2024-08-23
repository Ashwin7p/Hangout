using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HashService;

namespace Hangout
{
    public partial class _Default : Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            // ClearAllCookies();
        }

        protected void TriggerErrorButton_Click(object sender, EventArgs e)
        {
            
                // Simulate an error by dividing by zero
                throw new Exception("This is a simulated Error.");
            
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Session["RequestUrl"] = "Member.aspx";
            HttpContext.Current.Session["RequestedUrl"]= "Member.aspx";
            Response.Redirect("Member.aspx");
            // Response.Redirect("Login.aspx");
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["RequestedUrl"] = "Staff.aspx";
            Response.Redirect("Staff.aspx");
            //Response.Redirect("Login.aspx");
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            if (hash_input.Text.Length!=0)
            {
                hash_output.Text = HashService.HashHelper.sha256_hash(hash_input.Text);
            }
            else {
                hash_input.Text = "Please enter Text!!";
                hash_output.Text = "";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length != 0)
            {
                ServiceReference2.Service1Client stemWords = new ServiceReference2.Service1Client();//Connect to Backend Services

                TextBox2.Text = stemWords.stemString(TextBox1.Text);

                // TextBox2.Text = HashService.HashHelper.sha256_hash(TextBox1.Text);
            }
            else
            {
                TextBox2.Text = "Please enter Text!!";
                TextBox2.Text = "";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Likes.aspx");
        }
    }
}