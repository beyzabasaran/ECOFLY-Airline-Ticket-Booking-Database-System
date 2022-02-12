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
    public partial class UserRegistration : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int Registration()
        {
            try
            {
                int ResultCout = 0;
                SqlCommand sqlCmd = new SqlCommand();
                if (connString.State == ConnectionState.Closed)
                {
                    connString.Open();
                }
                //ADDING REGISTERED PERDON TO THE DATABASE
                //TAKING INPUTS AS SQL TABLE PARAMETERS
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@FName", Convert.ToString(txtFirstName.Text));
                sqlCmd.Parameters.AddWithValue("@LName", Convert.ToString(txtLastName.Text));
                sqlCmd.Parameters.AddWithValue("@EmailId", Convert.ToString(txtEmailID.Text));
                sqlCmd.Parameters.AddWithValue("@Address", Convert.ToString(txtAddress.Text));
                sqlCmd.Parameters.AddWithValue("@City", Convert.ToString(txtCity.Text));
                sqlCmd.Parameters.AddWithValue("@PinCode", Convert.ToString(txtPincode.Text));
                sqlCmd.Parameters.AddWithValue("@ContactNo", Convert.ToString(txtMobileNo.Text));
                sqlCmd.Parameters.AddWithValue("@Password", Convert.ToString(txtPassword.Text));
                sqlCmd.Parameters.AddWithValue("@Ret_Val", SqlDbType.BigInt);
                sqlCmd.Parameters["@Ret_Val"].Direction = ParameterDirection.Output;
                sqlCmd.CommandText = "ispUserRegistration";
                sqlCmd.Connection = connString;
                sqlCmd.ExecuteNonQuery();
                //EXECUTENONQUERY RETURNS LINE NUMBER IF COMMANDS ARE UPDATE, INSERT OR DELETE OTHERWISE
                //IT RETURNS -1
                //WE USE IT FOR CHECKING 
                ResultCout = Convert.ToInt32(sqlCmd.Parameters["@Ret_Val"].Value);
                return ResultCout;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int RegistrationStatus = 0;
            RegistrationStatus = Registration();
            if (RegistrationStatus > 0)//IF ADDING REGISTRATION DATA TO DATABASE SUCCESSFUL
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Registration has been done successfully')", true);
            }
            else if (RegistrationStatus == -1) //IF UNSUCCESSFUL
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mobile No already exist please try with another mobile no')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error Occur Please contact your system Administrator')", true);
            }
            txtFirstName.Text = ""; //AFTER CLICK TEXT FIELDS SET TO EMPTY
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtMobileNo.Text = "";
            txtPincode.Text = "";
            txtCity.Text = "";
            txtPassword.Text = "";
            txtEmailID.Text = "";
        }

    }
}