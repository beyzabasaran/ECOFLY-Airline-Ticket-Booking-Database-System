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
    public partial class FlightDetailsReport : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    bindFlightDetailsReport();
                }
                else
                {
                    Response.Redirect("AdminLogin.aspx");
                }
            }
        }

        private void bindFlightDetailsReport()
        {
            DataSet dsGetData = new DataSet();
            SqlCommand sqlCmd = new SqlCommand();
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "ispGetFlightDetailsForUpdation";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dsGetData);
            if (dsGetData.Tables[0].Rows.Count > 0)
            {
                gdFlightDetails.DataSource = dsGetData.Tables[0];
                gdFlightDetails.DataBind();
            }
            else
            {
                gdFlightDetails.DataSource = null;
                gdFlightDetails.EmptyDataText = "No Records Found";
                gdFlightDetails.DataBind();
            }
        }

        protected void gdFlightDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink klnikUpdate = (HyperLink)e.Row.FindControl("hlinkUpdate");
                HiddenField hdnFlightID = (HiddenField)e.Row.FindControl("hdnPNRNo");
                HyperLink hlinkSchedule = (HyperLink)e.Row.FindControl("hlinkAddSchedule");
                HiddenField hdnRouteID = (HiddenField)e.Row.FindControl("hdnRouteID");
                klnikUpdate.NavigateUrl = "FlightDetails.aspx?FlightID=" + hdnFlightID.Value;
                hlinkSchedule.NavigateUrl = "FlightScheduleDetails.aspx?FlightID=" + hdnFlightID.Value + "&RouteID=" + hdnRouteID.Value;
            }
        }

    }
}