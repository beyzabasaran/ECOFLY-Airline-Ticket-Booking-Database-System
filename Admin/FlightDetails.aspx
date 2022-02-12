<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FlightDetails.aspx.cs" Inherits="AirlineTicketBookingProject.Admin.FlightDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" style="margin-top: 5%">
    </div>
    <div class="row centered-form" style="margin-top: 5%">
        <div class="col-lg-8 col-sm-8 col-md-2 col-sm-offset-2 col-md-offset-2">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Add Flight Details</h3>
                </div>
                <div class="panel-body">
                    <asp:ValidationSummary ID="vsRegister" runat="server" CssClass="alert alert-danger" ShowSummary="true" ValidationGroup="vgRegister" />
                    <div id="divMessage" runat="server" />
                    <div class="col-xs-6 col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblFlight" runat="server" Text="Flight No" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtFlightNo" runat="server" class="form-control input-sm floatlabel"/>
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFlightNo" Display="None" ID="rfvFirstName" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Flight No is required." /><br />
                        </div>
                       <div class="form-group">
                            <asp:Label ID="lblSeatRow" runat="server" Text="No Of Seats Rows" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtSeatRows" runat="server" class="form-control input-sm floatlabel" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSeatRows" Display="None" ID="rfVMobileNo" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Seats Row is required." /><br />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Origin" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtOrigin" runat="server" class="form-control input-sm floatlabel" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrigin" Display="None" ID="RequiredFieldValidator1" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Origin is Required" /><br />
                        </div>
                          <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="FlightName" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtFlightName" runat="server" class="form-control input-sm floatlabel" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFlightName" Display="None" ID="RequiredFieldValidator3" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Flight Name is Required" /><br />
                        </div>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblLastName" runat="server" Text="Flight Type" Font-Bold="true"></asp:Label>
                             <asp:DropDownList ID="ddlFlightType" runat="server" class="form-control input-sm floatlabel">
                                 <asp:ListItem Value="0" Text="Select Flight Type"></asp:ListItem>
                                 <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                  <asp:ListItem Value="2" Text="AC"></asp:ListItem>
                             </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlightType" Display="None" ID="rfVLastName" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Last Name is required." /><br />
                        </div>
                        
                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" Text="Total Seats Column" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtSeatColumn" runat="server" class="form-control input-sm floatlabel"  />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSeatColumn" Display="None" ID="rfvPassword" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Seats Column is Require" /><br />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Destination" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtDetination" runat="server" class="form-control input-sm floatlabel" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDetination" Display="None" ID="RequiredFieldValidator2" ValidationGroup="vgRegister"
                                    CssClass="text-danger" ErrorMessage="Destination is Required" /><br />
                        </div>
                       
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" style="width:auto;margin-top:20px;" Text="Add Flight Details" OnClick="btnSave_Click" class="btn btn-info " ValidationGroup="vgRegister"  CausesValidation="True" ViewStateMode="Inherit" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


