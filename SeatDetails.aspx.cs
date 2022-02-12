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
    public partial class SeatDetails : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                lblForm.Text = Convert.ToString(Request.QueryString["Origin"]);
                lblTo.Text = Convert.ToString(Request.QueryString["Destination"]);
                DateTime dtNEw = DateTime.ParseExact(Convert.ToString(Request.QueryString["TravelDate"]), "dd/MM/yyyy", null);
                lbldate.Text = String.Format("{0:ddd,d MMM,yyyy}", dtNEw);
                bingBoardigPoints();
                string bookedSeatNo = "";
                DataTable dt = getBookedSeat();
                foreach (DataRow dr in dt.Rows)
                {
                    bookedSeatNo += Convert.ToString(dr["SeatNo"]) + ",";
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "paramFN1", "getSeatLayout('" + Convert.ToInt32(Request.QueryString["Row"]) + "','" + Convert.ToInt32(Request.QueryString["Column"]) + "','" + bookedSeatNo + "','" + Convert.ToDecimal(Request.QueryString["Fare"]) + "');", true);
            }
        }


        private void bingBoardigPoints()//GETTING BOARDING PLACES TABLE DATA FOR PASSENGERS
        {
            DataTable dsGetData = new DataTable();
            SqlCommand sqlCmd = new SqlCommand();
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FlightID", Convert.ToInt32(Request.QueryString["FlightID"]));
            sqlCmd.CommandText = "ispGetBoardingPoints";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dsGetData);
            if (dsGetData.Rows.Count > 0)//IF THERE EXISTS RECORD
            {
                dsGetData.Columns.Add(new DataColumn("Value", System.Type.GetType("System.String"), "PlaceName + ' - ' + PlaceTime"));
                ddlBoardingpoints.DataSource = dsGetData;
                ddlBoardingpoints.DataTextField = "Value";
                ddlBoardingpoints.DataValueField = "StandId";
                ddlBoardingpoints.DataBind();
            }
            ddlBoardingpoints.Items.Insert(0, new ListItem("Select Boarding Point", "0"));
            //First item for user choice bar
        }

        private DataTable getBookedSeat()//GETTING SEAT BOOKING DATA FROM DATABASE
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand();
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FlightID", Convert.ToInt32(Request.QueryString["FlightID"]));
            sqlCmd.Parameters.AddWithValue("@TravelDate", Convert.ToString(Request.QueryString["TravelDate"]));
            sqlCmd.CommandText = "ispGetBookedSeatNo";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dt);
            return dt;
        }

        //IF PAYMENT BUTTON CLICKED REDIRECT TO GIVING RELATED MESSAGE
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            lblSelectedSeat.Text = Request.Form[hdnSeatNo.UniqueID];
            lblPerSeat.Text = Request.Form[hdnFare.UniqueID];
            if (Session["UserID"] != null)
            {
                Response.Redirect("PassengerDetailsInfo.aspx?FlightID=" + Request.QueryString["FlightID"] + "&SeatNo=" + lblSelectedSeat.Text + "&TravelDate=" + Request.QueryString["TravelDate"] +
                "&Origin=" + Request.QueryString["Origin"] + "&Destination=" + Request.QueryString["Destination"] + "&BoardingID=" + ddlBoardingpoints.SelectedValue + "&Fare=" + lblPerSeat.Text);
            }
            else
            {
                Response.Redirect("Login.aspx?FlightID=" + Request.QueryString["FlightID"] + "&SeatNo=" + lblSelectedSeat.Text + "&TravelDate=" + Request.QueryString["TravelDate"] +
                "&Origin=" + Request.QueryString["Origin"] + "&Destination=" + Request.QueryString["Destination"] + "&BoardingID=" + ddlBoardingpoints.SelectedValue + "&Fare=" + lblPerSeat.Text);
            }

        }

    }
}