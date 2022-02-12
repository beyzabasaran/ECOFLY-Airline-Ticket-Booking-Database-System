<%@ Page Title="" Language="C#" MasterPageFile="~/AirlineTicketBookingMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AirlineTicketBookingProject.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top:6%">
        <div class="row">
            <div class="col-lg-12">
                <asp:Image ID="imgFlight" ImageAlign="AbsMiddle" ImageUrl="~/img/home.jpg" style="width:100%" runat="server" />
            </div>
            <div class="col-lg-12" style="margin-top:2%">
                <div class="panel panel-default">
                    <div class="panel-heading">
                         <div class=" panel-title">
                        <h3>Introduction To The Airline Flight Booking</h3>
                    </div>
                       </div>
                    <div class="panel-body">
                        <p style="font-size:large">
                            Airline Flight Booking System is Web Based application,That works with in centralised network.
                        </p>
                    </div>
                </div>
            </div>
             <div class="col-lg-12" style="margin-top:2%">
                <div class="panel panel-default">
                    <div class="panel-heading">
                         <div class=" panel-title">
                        <h3>Why Online Flight Booking is Important?</h3>
                    </div>
                       </div>
                    <div class="panel-body" style="font-size:large">
                           <ul>
                               <li>
                                  Economic & Easy 
                               </li>
                               <li>
                                   Quick & Saves Time   
                               </li>

                           </ul>
                       
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>


