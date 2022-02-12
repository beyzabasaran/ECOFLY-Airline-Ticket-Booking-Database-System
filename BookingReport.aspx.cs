using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirlineTicketBookingProject
{
    public partial class BookingReport : System.Web.UI.Page
    {
        #region Global Variable
        SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["AirlineTicketBookingConnectionString"].ToString());
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null) //IF THERE IS USER
                {
                    bindPnrDetails(); //GET PNR DETAILS
                    if (Request.QueryString["Msg"] != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Convert.ToString(Request.QueryString["Msg"]) + "')", true);
                        PropertyInfo isreadonly =
                         typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                         "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                        // MAKING EDITABLE
                        isreadonly.SetValue(this.Request.QueryString, false, null);
                        // REMOVING
                        this.Request.QueryString.Clear();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx"); //REDIRECT TO LOGIN PAGE
                }
            }
        }


        private void bindPnrDetails() //GETTING PNR DETAILS FUNCTION
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
            sqlCmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));
            sqlCmd.CommandText = "ispGetPNRDetails";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd); //ADAPTING DB BY PREDECLARED SQL COMMAND
            sda.Fill(dsGetData); //GETTING DB INFORMATION
            if (dsGetData.Tables != null) //IF THERE EXISTS TABLE
            {
                gdTicketReport.DataSource = dsGetData.Tables[0];
                gdTicketReport.DataBind();
            }
            else //IF NO TABLE
            {
                gdTicketReport.DataSource = null;
                gdTicketReport.EmptyDataText = "No Records Found";
                gdTicketReport.DataBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //TELLING THE COMPILER THAT THE CONTROL IS RENDERED
        }

        private void printTicket(string transactionNo)
        {
            ticket.Visible = true;
            DataSet dsGetData = new DataSet(); //DECLARING DATASET
            SqlCommand sqlCmd = new SqlCommand(); //DECLARING SQL COMMAND
            if (connString.State == ConnectionState.Closed)
            {
                connString.Open(); //CHECKING CONNECTION
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //EXECUTING STORED PROCEDURE IN SQL DB
            //STORED PROCEDURE IS OUR SUBROUTINE FOR ACCESSING TO OUR RELATIONAL
            //DATABASE THAT WE HAVE CREATED 
            sqlCmd.Parameters.AddWithValue("@PNRNo", transactionNo); //GINING THE PARAMETER
            sqlCmd.CommandText = "GetPassengerDetails";
            sqlCmd.Connection = connString;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            sda.Fill(dsGetData);
            gdPaxDetails.DataSource = dsGetData.Tables[0];
            gdPaxDetails.DataBind(); //GETTING THE DATA FROM DATABASE
            //TAKING DATA FROM THE DATABSE TO SHOW ON THE PAGE TEXTBOXES
            lblTransactionNo.Text = Convert.ToString(dsGetData.Tables[1].Rows[0]["PNRNo"]);
            lblFlightName.Text = Convert.ToString(dsGetData.Tables[1].Rows[0]["FlightName"]);
            //lblDepartureTime.Text = Convert.ToString(dsGetData.Tables[1].Rows[0]["DeptTime"]);
            lblTotalAmount.Text = Convert.ToString(dsGetData.Tables[1].Rows[0]["Amount"]);
            lblTotalTickets.Text = Convert.ToString(dsGetData.Tables[1].Rows[0]["TotalTickets"]);
            //USING StringReader,Document,PdfWriter TO MAKE LINK AND DOWNLOAD TICKET PRINT PDF
            //BY THE USER IN THE BOOKING REPORT PAGEAFTER THE BOOKING
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    ticket.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    pdfDoc.NewPage();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachement;filename=Ticket" + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }

        }

        //CREATING THE Download Ticket LINK COMMAND 
        protected void gdTicketReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download Ticket") //CHECKING BY USING GridViewCommandEventArg
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gdTicketReport.Rows[index];
                printTicket(row.Cells[1].Text);
            }
        }

    }
}