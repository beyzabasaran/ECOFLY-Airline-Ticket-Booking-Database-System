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
    public partial class RouteDetails : System.Web.UI.Page
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
                    bindBoardingDetails();
                }
                else
                {
                    Response.Write("AdminLogin.aspx"); //IF LOGIN UNSUCCESSFUL REDIRECT
                }
            }
        }

        private void bindBoardingDetails()
        {
            DataSet dsGetData = new DataSet();//DECLARING DATASET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open(); //CHECK CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.CommandText = "ispGetRouteDetails";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dsGetData); //GETTING THE DATABASE DATA
            if (dsGetData.Tables[0].Rows.Count > 0) //IF THERE EXISTS RECORD
            {
                gdRouteDetails.DataSource = dsGetData.Tables[0];
                gdRouteDetails.DataBind();
            }
            else //OTHERWISE ERROR MESSAGE SHOWN
            {
                gdRouteDetails.DataSource = null;
                gdRouteDetails.EmptyDataText = "No Records Found";
                gdRouteDetails.DataBind();
            }
        }

        //USING GridViewRowEvent TO USE SOME FEATURES SUCH AS HYPERLINK
        //SINCE IT IS HiddenField IT WON'T SEEN BUT IT IS STORED
        //WE AIMED NOT TO SHOW AN ID VALUE OF TABLE LIKE FLIGHTID, ROUTEID
        //BECAUSE USERS ARE NOT CONCERNED WITH THIS DATA SO IT WAS UNNECESSARY TO SHOW
        protected void gdRouteDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink klnikUpdate = (HyperLink)e.Row.FindControl("hlinkBoarding");
                HiddenField hdnFlightID = (HiddenField)e.Row.FindControl("hdnFlightID");
                HiddenField hdnRouteID = (HiddenField)e.Row.FindControl("hdnRouteID");
                klnikUpdate.NavigateUrl = "BoardingDetails.aspx?FlightID=" + hdnFlightID.Value + "&RouteID=" + hdnRouteID.Value;
            }
        }

    }
}