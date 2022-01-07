﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site.Master" AutoEventWireup="true" CodeBehind="AppraisalRPT.aspx.cs" Inherits="USACBOSA.Reports.AppraisalRPT" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            width: 99%;
            height: 462px;
        }
        .style2
        {
            width: 120%;
            height: 457px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                        =<asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" />
                                <asp:Button ID="Button1" runat="server" Text="EXPORT BUTTON" 
                                onclick="Button1_Click" style="color: #FFFFCC; background-color: #FF0066" />
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" ToolPanelView="None" 
                    onnavigate="criprt_navigation" ToolPanelWidth="50px" Width="900px" height="400px"
                    BestFitPage="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
