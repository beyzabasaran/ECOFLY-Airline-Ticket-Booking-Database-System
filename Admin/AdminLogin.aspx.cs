using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirlineTicketBookingProject.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PAGE LOAD MADE WITH THIS FUNCTION
        }

        //WHEN ADMIN LOGIN BUTTON CLICKED NAME & PASSWORD CHECK WILL BE MADE WITH THIS FUNCTION
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "admin" && txtPassword.Text == "admin")
            {
                Session["UserName"] = "Admin";
                //IF NAME & PASSWORD CORRECT IT WILL BE DIRECTED TO FlightDetailsReport.aspx PAGE
                Response.Redirect("FlightDetailsReport.aspx");
            }
            else
            {
                //IF NAME & PASSWORD INCORRECT ALERT MESSAGE WILL BE SHOWN
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Username and password is incorrect please enter valid credentials')", true);
            }
        }
    }
}