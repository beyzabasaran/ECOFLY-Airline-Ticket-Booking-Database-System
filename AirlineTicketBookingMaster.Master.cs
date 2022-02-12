using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirlineTicketBookingProject
{
    public partial class AirlineTicketBookingMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FName"] != null) //IF USER IS VALID
            {
                lblName.Text = Convert.ToString(Session["FName"]); //LOAD TO lblName.Text
            }

        }

        protected void btnLogout_Click(object sender, EventArgs e) //IF BUTTON CLICKED
        {
            Session.Abandon();
            Response.Redirect("Login.aspx"); //THEN REDIRECT USER TO LOGIN PAGE
        }
    }
}