using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Hangout
{
	public partial class Likes : Page
	{
		public string uid = "";
		protected void Page_Load(object sender, EventArgs e)
		{
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

		//show liked stores button
		protected void Button1_Click(object sender, EventArgs e)
		{
			
			string fileLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"APP_Data/Likes.xml");
			
			if (File.Exists(fileLocation))
			{
				try
				{
					//search for likes
					var xmlStr = File.ReadAllText(fileLocation);
					var str = XElement.Parse(xmlStr);
					//this line has the hard-coded user id, can be changed into the current user id once integrated
					var result = str.Elements("user").Where(x => x.Element("ID").Value.Equals(uid)).ToList()[0].Elements("Store").ToList();


					//reset the textbox for each press on the button
					txt.Text = "";
					string result_txt = "";
					//display the info of all the liked stores
					for (int i = 0; i < result.Count; i++)
					{

						result_txt += "Store Name: " + result[i].Element("Name").Value + "\n";
						result_txt += "Store Type: " + result[i].Attribute("type").Value + "\n";
						result_txt += "Address: " + result[i].Element("Address").Value + "\n";
						result_txt += "Zipcode: " + result[i].Element("Zipcode").Value + "\n";
						result_txt += "Details: " + result[i].Element("Details").Value + "\n";
						result_txt += "Reviews:\n";

						var review = result[i].Element("Reviews").Elements("Review").ToList();
						//display all the reviews
						for (int j = 0; j < review.Count; j++)
						{
							result_txt += review[j].Attribute("star").Value + " stars, " + review[j].Value + "\n";
						}
						result_txt += "\n";

					}
					txt.Text = result_txt;
				}
				catch(System.ArgumentOutOfRangeException)
				{
					txt.Text = "No Liked Stores Yet";
				}

				
			}
			
		}

		//Like button
		protected void Button2_Click(object sender, EventArgs e)
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
				if(user.Elements("Store").Where(x => x.Element("Address").Value.Equals(address.Text)).ToList().Count == 0)
				{
					var newStore = new XElement("Store", new XAttribute("type", storeType.Text),
						   new XElement("Name", storeName.Text),
						   new XElement("Address", address.Text),
						   new XElement("Zipcode", zipcode.Text),
						   new XElement("Details", details.Text),
						   new XElement("Reviews",
						   new XElement("Review", new XAttribute("star", star1.Text), comment1.Text),
						   new XElement("Review", new XAttribute("star", star2.Text), comment2.Text)));
					user.Add(newStore);
					xml.Save(fileLocation);
					Liked.Text = "Successfully liked this store";
				}
				else
				{
					Liked.Text = "This store has already been liked";
				}
				
				
			}
			finally
			{
				
			}



		}
	}
}