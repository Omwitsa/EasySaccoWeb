<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site.Master" AutoEventWireup="true" CodeBehind="AppliedLoans.aspx.cs" Inherits="USACBOSA.Reports.AppliedLoans" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        .style3
        {
            width: 277px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style3">
                <table class="style2">
                    <tr>
                        <td>
                        =<asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" />
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" ToolPanelView="None" Font-Size="Small" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
