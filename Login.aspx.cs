using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirlineTicketBookingProject
{
    public partial class Login : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataSet getUserData()
        {
            DataSet dsGetData = new DataSet();//DECLARING DATASET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open(); //CHECKING CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            //TAKING PARAMETER FOR DATABASE CHECK
            sqlCmd.Parameters.AddWithValue("@EmailId", Convert.ToString(txtUserId.Text));
            sqlCmd.Parameters.AddWithValue("@Password", Convert.ToString(txtPassword.Text));
            sqlCmd.CommandText = "ispUserLogin";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);//ADAPTING DB BY PREDECLARED SQL COMMAND
            sda.Fill(dsGetData);//GETTING DB INFORMATION
            return dsGetData;

        }

        //BUTTON ACTION LISTENED
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataSet dsLogin = getUserData();//CHECKING TAKEN PARAMETERS FROM DATABASE 
            if (dsLogin.Tables[0].Rows.Count > 0) //IF THERE IS RECORD
            {
                Session["UserID"] = Convert.ToInt32(dsLogin.Tables[0].Rows[0]["regId"]); 
                Session["FName"] = Convert.ToString(dsLogin.Tables[0].Rows[0]["Fname"]);
                Session["EmailId"] = Convert.ToString(dsLogin.Tables[0].Rows[0]["EmailId"]);
                //SESSION VARIABLE ARE STORED ON THE SERVER IT IS  UNIQUE FOR EACH USER
                //SESSION AND IT WILL BE REMOVED WHEN SESSION TIMES OUT
                if (Request.QueryString["FlightID"] != null)//IF THERE IS FLIGHT
                {
                    Response.Redirect("PassengerDetailsInfo.aspx?FlightID=" + Request.QueryString["FlightID"] + "&SeatNo=" + Request.QueryString["SeatNo"] + "&TravelDate=" + Request.QueryString["TravelDate"] +
                  "&Origin=" + Request.QueryString["Origin"] + "&Destination=" + Request.QueryString["Destination"] + "&BoardingID=" + Request.QueryString["BoardingID"] + "&Fare=" + Request.QueryString["Fare"]);
                }
                else //IF NO VALID FLIGHT
                {
                    Response.Redirect("Home.aspx");
                }

            }
            else //IF THERE IS RECORD ON THE DATABASE BASED ON LOGIN ENTERED LOGIN IFORMATION
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Credentials Passed,Please check your username and Password')", true);
            }
        }

    }
}