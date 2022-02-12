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
    public partial class Home : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //AVOIDING SECOND PAGE LOAD
            {
                bindOriginCity();//ORIGIN CITY OF THE FLIGHT
                bindDestinationCity();//DESTINATION CITY OF THE FLIGHT
            }
        }

        private void bindOriginCity()
        {
            //ACCESSING CELL VALUE OF DATA TABLE
            DataSet dsOrigin = getCity();
            if (dsOrigin.Tables[0].Rows.Count > 0) //IF THERE IS RECORD
            {
                ddlOrigin.DataSource = dsOrigin.Tables[0];
                //GET DATA FROM DATABASE TABLES WHERE COLUMN NAME CityName
                ddlOrigin.DataTextField = "CityName"; 
                ddlOrigin.DataValueField = "CityName";
                ddlOrigin.DataBind();
            }
            ddlOrigin.Items.Insert(0, new ListItem("-Select City--", "0"));
            //INSERT ITEM 0 AS DECLARATION AS Select City


        }

        private void bindDestinationCity()
        {
            //ACCESSING CELL VALUE OF DATA TABLE
            DataSet dsOrigin = getCity();
            if (dsOrigin.Tables[0].Rows.Count > 0)//IF THERE IS RECORD
            {
                ddlDestination.DataSource = dsOrigin.Tables[0];
                //GET DATA FROM DATABASE TABLES WHERE COLUMN NAME CityName
                ddlDestination.DataTextField = "CityName";
                ddlDestination.DataValueField = "CityName";
                ddlDestination.DataBind();
            }
            ddlDestination.Items.Insert(0, new ListItem("-Select City--", "0"));
            //INSERT ITEM 0 AS DECLARATION AS Select City

        }

        private DataSet getCity()
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
            sqlCmd.CommandText = "ispGetCity";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd); //ADAPTING DB BY PREDECLARED SQL COMMAND
            sqlCmd.ExecuteNonQuery();
            sda.Fill(dsGetData); //GETTING DB INFORMATION
            return dsGetData;
        }

        //IF SEARCH BUTTON CLICKED THEN REDIRECT USER TO RELATIVE PAGE
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("AirlineTicketBookingSearchDetails.aspx?Origin=" + ddlOrigin.SelectedItem.Text + "&Destination=" + ddlDestination.SelectedItem.Text + "&TravelDate=" + txtDate.Text);
        }


    }
}