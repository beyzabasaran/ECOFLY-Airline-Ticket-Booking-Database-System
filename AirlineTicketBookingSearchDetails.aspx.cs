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
    public partial class AirlineTicketBookingSearchDetails : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindSearchDetails();
            }
        }

        private void bindSearchDetails()
        {
            DataSet dsGetData = new DataSet(); //DECLARING DATASET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open(); //CHECKING CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.Parameters.AddWithValue("@Origin", Convert.ToString(Request.QueryString["Origin"]));
            sqlCmd.Parameters.AddWithValue("@Destination", Convert.ToString(Request.QueryString["Destination"]));
            sqlCmd.Parameters.AddWithValue("@TravelDate", Convert.ToString(Request.QueryString["TravelDate"]));
            sqlCmd.CommandText = "ispGetAvailableFlightDetails";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd); //ADAPTING DB BY PREDECLARED SQL COMMAND
            sda.Fill(dsGetData); //GETTING DB INFORMATION
            if (dsGetData.Tables[0].Rows.Count > 0)  //CHECK IF THERE EXISTS ANY RECORD
            {
                hlinkSearch.Visible = false; //MAKE UNVISIBLE
                gvFlightDetails.DataSource = dsGetData.Tables[0]; //GETTING DATA FROM DATABASE
                gvFlightDetails.DataBind(); //AND LOAD TO FLIGHT DETAILS
            }
            else //CHECK NO RECORD
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Trip not available,Please search again with different date')", true);
                hlinkSearch.Visible = true;
            }
        }

        //BOUNDING DATA WITH HIDDEN FIELD IN ORDER TO SHOW ONLY RELATED AND WANTED SQL TABLE DATA
        protected void gvFlightDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnNewFlightID = (HiddenField)e.Row.FindControl("hdnFlightID");
                HiddenField hdnNewSeatRow = (HiddenField)e.Row.FindControl("hdnSeatRow");
                HiddenField hdnNewSeatCol = (HiddenField)e.Row.FindControl("hdnSeatColumn");
                HyperLink hlnkSelect = (HyperLink)e.Row.FindControl("hplnkSelect");
                Label lblFare = (Label)e.Row.FindControl("lblFare");
                hlnkSelect.NavigateUrl = "SeatDetails.aspx?FlightID=" + hdnNewFlightID.Value + "&Row=" + hdnNewSeatRow.Value + "&Column=" + hdnNewSeatCol.Value +
                    "&Origin=" + Request.QueryString["Origin"] + "&Destination=" + Request.QueryString["Destination"] +
                    "&TravelDate=" + Request.QueryString["TravelDate"] + "&Fare=" + lblFare.Text;
                //SETTING URL TO LINK  WHEN  HYPERLINK IN THE COLUMN IS CLICKED BY THE USER
            }
        }

    }
}