using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stamp;

namespace StampTest
{
	public partial class StampImage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			StampControl stampControl = new StampControl();
			stampControl.Name = Request.QueryString.Get("name");
			stampControl.Department = Request.QueryString.Get("department");
			stampControl.Date = Request.QueryString.Get("date");

			string filePath = @"C:\temp\stamp.gif";
			stampControl.CreateImage(filePath);

			Response.WriteFile(filePath);
		}
	}
}
