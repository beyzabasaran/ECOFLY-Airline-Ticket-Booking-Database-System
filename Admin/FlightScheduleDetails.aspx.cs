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
    public partial class FlightScheduleDetails : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //AVOIDING SECOND LOAD 
            {
                if (Session["UserName"] != null)
                {

                }
                else
                {
                    Response.Redirect("AdminLogin.aspx"); //IF LOGIN UNSUCCESSFUL REDIRECT
                }
            }
        }

        private int addFlightScheduleData()
        {
            int ResultCout = 0;
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();//CHECK CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.Parameters.AddWithValue("@Date", Convert.ToString(txtDate.Text));
            sqlCmd.Parameters.AddWithValue("@FlightID", Convert.ToInt32(Request.QueryString["FlightID"]));
            sqlCmd.Parameters.AddWithValue("@Fare", Convert.ToDecimal(txtFare.Text));
            sqlCmd.Parameters.AddWithValue("@EstimatdTime", Convert.ToString(txtTravelTime.Text));
            sqlCmd.Parameters.AddWithValue("@ArrivalTime", Convert.ToString(txtArrival.Text));
            sqlCmd.Parameters.AddWithValue("@DepartureTime", Convert.ToString(txtDeparture.Text));
            sqlCmd.Parameters.AddWithValue("@RouteID", Convert.ToInt32(Request.QueryString["RouteID"]));
            sqlCmd.CommandText = "ispAddFlightSchedule";
            sqlCmd.Connection = connString;
            ResultCout = sqlCmd.ExecuteNonQuery(); 
            //EXECUTENONQUERY RETURNS LINE NUMBER IF COMMANDS ARE UPDATE, INSERT OR DELETE OTHERWISE
            //IT RETURNS -1
            //WE USE IT FOR CHECKING IN THE FOLLOWING FUNCTION
            return ResultCout;
        }
        protected void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            int result = addFlightScheduleData(); 
            if (result == -1) //IF addFlightScheduleData() IS SUCESSFULL
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Flight Schedule Details has been added successfully')", true);
                txtDeparture.Text = "";
                txtDate.Text = "";
                txtArrival.Text = "";
                txtFare.Text = "";
            }
            else //OTHERWISE WARNING MESSAGE SHOWN
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error occur please contact your system administrator')", true);
            }
        }

    }
}