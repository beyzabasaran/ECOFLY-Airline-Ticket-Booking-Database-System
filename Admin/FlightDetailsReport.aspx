<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FlightDetailsReport.aspx.cs" Inherits="AirlineTicketBookingProject.Admin.FlightDetailsReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top:6%">
        <asp:GridView ID="gdFlightDetails" runat="server" EmptyDataText="No Record Found...." AutoGenerateColumns="False" AllowPaging="true" PageSize="20" CssClass="table table-hover table-bordered"
                    Width="100%" Font-Size="12" OnRowDataBound="gdFlightDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField >
                       <asp:TemplateField HeaderText="Flight ID">
                           <ItemTemplate>
                               <asp:Label ID="lblFlightID" runat="server" Text='<%# Eval("FlightId") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Flight No">
                           <ItemTemplate>
                               <asp:Label ID="lblFlightNo" runat="server" Text='<%# Eval("FlightNo") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="Flight Name">
                           <ItemTemplate>
                               <asp:Label ID="lblFlightName" runat="server" Text='<%# Eval("FlightName") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Flight Type">
                           <ItemTemplate>
                               <asp:Label ID="lblFlightType" runat="server" Text='<%# Eval("FlighttType") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Seat Columns">
                           <ItemTemplate>
                               <asp:Label ID="lblSeatCol" runat="server" Text='<%# Eval("SeatColumn") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Seat Row">
                           <ItemTemplate>
                               <asp:Label ID="lblSeatRow" runat="server" Text='<%# Eval("SeatRow") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Origin">
                           <ItemTemplate>
                               <asp:Label ID="lblOrigin" runat="server" Text='<%# Eval("Origin") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Destination">
                           <ItemTemplate>
                               <asp:Label ID="lblDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlinkUpdate" runat="server" >Update Details</asp:HyperLink>
                                 <asp:HyperLink ID="hlinkAddSchedule" runat="server" >Add Flight Schedule</asp:HyperLink>
                                <asp:HiddenField ID="hdnPNRNo" runat="server" Value='<%# Eval("FlightId") %>' />
                                 <asp:HiddenField ID="hdnRouteID" runat="server" Value='<%# Eval("RouteID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </div>
</asp:Content>


