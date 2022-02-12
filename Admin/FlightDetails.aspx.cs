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
    public partial class FlightDetails : System.Web.UI.Page
    {
        // DATABASE CONNECTION STRING DECLARED AS GLOBAL VARIABLE
        //IN ORDER TO BE ABLE RUN WITH DIFFERENT DATABASES
        //BY ONLY CHANGING web.config FILE'S DATA SOURCE NAME ACCORDING TO OUR DATASOURCE
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //AVOIDING SECOND LOAD
            {
                if (Session["UserName"] != null)
                {
                    if (Request.QueryString["FlightID"] != null) //IF THERE EXISTS FLIGHT ID
                    {
                        int FlightID = Convert.ToInt32(Request.QueryString["FlightID"]);
                        FillData(FlightID);
                        btnSave.Text = "Update"; //THEN ITS UPDATE ACTION BY THE ADMIN
                    }
                    else //IF NO FLIGHT ID
                    {
                        btnSave.Text = "Insert";//THEN ITS INSERT ACTION BY THE ADMIN
                    }
                }
                else
                {
                    Response.Redirect("AdminLogin.aspx");//IF LOGIN WAS NOT SUCCESSFULL REDIRECT AGAIN
                }
            }
        }

        private int UpdateData()
        {
            int ResultCout = 0;
            SqlCommand sqlCmd = new SqlCommand(); //SQL COMMAND DECLARATION
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();//CHECK CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;//EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.Parameters.AddWithValue("@FlightID", Convert.ToInt32(Request.QueryString["FlightID"]));
            sqlCmd.Parameters.AddWithValue("@FlightNo", Convert.ToString(txtFlightNo.Text));
            sqlCmd.Parameters.AddWithValue("@FlightName", Convert.ToString(txtFlightName.Text));
            sqlCmd.Parameters.AddWithValue("@FlightType", Convert.ToString(ddlFlightType.SelectedItem.Text));
            sqlCmd.Parameters.AddWithValue("@seatColumn", Convert.ToInt32(txtSeatColumn.Text));
            sqlCmd.Parameters.AddWithValue("@SeatRow", Convert.ToInt32(txtSeatRows.Text));
            sqlCmd.Parameters.AddWithValue("@Origin", Convert.ToString(txtOrigin.Text));
            sqlCmd.Parameters.AddWithValue("@Destination", Convert.ToString(txtDetination.Text));
            sqlCmd.CommandText = "ispUpdateFlightData";
            //UPDATING OUR DB BY THE ADMIN'S UPDATE PAGE TEXT BOX INPUTS
            sqlCmd.Connection = connString;
            ResultCout = sqlCmd.ExecuteNonQuery();
            //EXECUTENONQUERY RETURNS LINE NUMBER IF COMMANDS ARE UPDATE, INSERT OR DELETE OTHERWISE
            //IT RETURNS -1
            //WE USE IT FOR CHECKING IN THE FOLLOWING FUNCTION
            return ResultCout;
        }
        private void FillData(int FlightID)
        {
            DataSet dsGetData = new DataSet(); //DECLARE DATA SET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARE SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open(); //CHECK CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure; //SAME PROCEDURE AS DECLARED ABOVE
            //ENTEGRATING OUR CODE WITH OUR SQL SERVER DATABASE
            sqlCmd.Parameters.AddWithValue("@FlightID", FlightID);
            sqlCmd.CommandText = "ispGetFlightDataByFlightID";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dsGetData);
            if (dsGetData.Tables[0].Rows.Count > 0) //IF THERE EXIST RECORD
            {
                //FILLING TEXT FIELD WITH DATABASE RECORDS BASED ON THEIR PARAMETERS
                txtFlightName.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["FlightName"]);
                txtFlightNo.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["FlightNo"]);
                ddlFlightType.SelectedItem.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["FlighttType"]);
                txtSeatRows.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["SeatRow"]);
                txtSeatColumn.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["SeatColumn"]);
                txtOrigin.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["Origin"]);
                txtDetination.Text = Convert.ToString(dsGetData.Tables[0].Rows[0]["Destination"]);
            }
        }
        private int AddFlightDetails()
        {
            int ResultCout = 0;
            SqlCommand sqlCmd = new SqlCommand(); //DECLARIND SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open();//CHECKING CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //TAKING DATAS FROM THE TEXT FIELD AND ADDING TO THE DATABASE
            sqlCmd.Parameters.AddWithValue("@FlightNo", Convert.ToString(txtFlightNo.Text));
            sqlCmd.Parameters.AddWithValue("@FlightName", Convert.ToString(txtFlightName.Text));
            sqlCmd.Parameters.AddWithValue("@FlighttType", Convert.ToString(ddlFlightType.SelectedItem.Text));
            sqlCmd.Parameters.AddWithValue("@SeatColumn", Convert.ToInt32(txtSeatColumn.Text));
            sqlCmd.Parameters.AddWithValue("@SeatRow", Convert.ToInt32(txtSeatRows.Text));
            sqlCmd.Parameters.AddWithValue("@Origin", Convert.ToString(txtOrigin.Text));
            sqlCmd.Parameters.AddWithValue("@Destination", Convert.ToString(txtDetination.Text));
            sqlCmd.CommandText = "ispAddFlightDetails";
            sqlCmd.Connection = connString;
            ResultCout = sqlCmd.ExecuteNonQuery(); ;
            return ResultCout;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Insert")// CHECKING OUR BUTTON ACTION TYPE
            {
                int result = AddFlightDetails();
                if (result == -1) //IF sqlCmd.ExecuteNonQuery() was successfull
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Flight Details has been added successfully')", true);
                    txtDetination.Text = "";
                    ddlFlightType.SelectedValue = "0";
                    txtOrigin.Text = "";
                    txtFlightNo.Text = "";
                    txtSeatColumn.Text = "";
                    txtSeatRows.Text = "";
                    txtFlightName.Text = "";
                }
                else //OTHERWISE ERROR WARNING SHOWN
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error occur please contact your system administrator')", true);
                }
            }
            else // IF BUTTON ACTION TYPE IS UPDATE
            {
                int result = UpdateData();
                if (result == -1) //IF IT IS SUCCESSFULL
                {
                    Response.Redirect("FlightDetailsReport.aspx"); //REDIRECT
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updation Failed please contact your system administrator')", true);
                }
            }


        }


    }
}