using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirlineTicketBookingProject.Admin
{
    public partial class BoardingDetails : System.Web.UI.Page
    {
        // DATABASE CONNECTION STRING DECLARED AS GLOBAL VARIABLE
        //IN ORDER TO BE ABLE RUN WITH DIFFERENT DATABASES
        //BY ONLY CHANGING web.config FILE'S DATA SOURCE NAME ACCORDING TO OUR DATASOURCE
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //AVOIDING SECOND LOAD
            if (!IsPostBack) //IF PAGE DOES NOT LOADED BEFORE
            {
                if (Session["UserName"] != null)//IF LOGIN SUCCESSFULL
                {

                }
                else
                {
                    Response.Redirect("AdminLogin.aspx"); 
                }
            }
        }

        private int AddBoardingDetails()
        {
            int ResultCout = 0;
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING AN SQL COMMAND
            if (connString.State == ConnectionState.Closed)//CHECKING IF THERE IS CONNECTION
            {
                connString.Open();
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.Parameters.AddWithValue("@RouteID", Convert.ToInt32(Request.QueryString["RouteID"]));
            sqlCmd.Parameters.AddWithValue("@PlaceName", Convert.ToString(txtPlace.Text));
            sqlCmd.Parameters.AddWithValue("@PlaceTime", Convert.ToString(txtArrival.Text));
            sqlCmd.Parameters.AddWithValue("@FlightID", Convert.ToInt32(Request.QueryString["FlightID"]));
            sqlCmd.CommandText = "addBordingDetails";
            sqlCmd.Connection = connString;
            ResultCout = sqlCmd.ExecuteNonQuery();
            //EXECUTENONQUERY RETURNS LINE NUMBER IF COMMANDS ARE UPDATE, INSERT OR DELETE OTHERWISE
            //IT RETURNS -1
            //WE USE IT FOR CHECKING IN THE FOLLOWING FUNCTION
            return ResultCout;
        }
        protected void btnAddBoardingDetails_Click(object sender, EventArgs e)
        {
            int result = AddBoardingDetails();
            if (result == -1) //IF AddBoardingDetails FUNCTION SUCCESSFULL IT WILL RETURN -1
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Boarding Details has been added successfully')", true);
                txtArrival.Text = "";
                txtPlace.Text = "";
            }
            else //OTHERWISE AddBoardingDetails FUNCTION WAS NOT SUCCESSFULL, WARNING MESSAGE SHOWN
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error occur please contact your system administrator')", true);
            }
        }

    }
}