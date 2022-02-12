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
    public partial class BookingReport : System.Web.UI.Page
    {
        // DATABASE CONNECTION STRING DECLARED AS GLOBAL VARIABLE
        //IN ORDER TO BE ABLE RUN WITH DIFFERENT DATABASES
        //BY ONLY CHANGING web.config FILE'S DATA SOURCE NAME ACCORDING TO OUR DATASOURCE
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                bingBookingReport();//IF LOGIN SUCCESSFULL
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");//OTHERWISE REDIRECT
            }
        }

        private void bingBookingReport()
        {
            DataSet dsGetData = new DataSet(); //DECLARING DATASET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();//CHECKING CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.CommandText = "ispGetBookingReportByAdmin";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);//ADAPTING DB BY PREDECLARED SQL COMMAND
            sda.Fill(dsGetData); //GETTING DB INFORMATION
            if (dsGetData.Tables[0].Rows.Count > 0) //CHECK IF THERE EXISTS ANY RECORD
            {
                gdTicketReport.DataSource = dsGetData.Tables[0];
                gdTicketReport.DataBind(); //IF THERE IS RECORD THEN KEEP IT FOR TICKET RECORD
            }
            else //IF NO RECORD
            {
                gdTicketReport.DataSource = null;
                gdTicketReport.EmptyDataText = "No Records Found";
                gdTicketReport.DataBind();
            }

        }
    }
}