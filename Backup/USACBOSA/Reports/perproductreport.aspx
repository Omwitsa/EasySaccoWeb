<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site.Master" AutoEventWireup="true" CodeBehind="perproductreport.aspx.cs" Inherits="USACBOSA.Reports.perproductreport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                        <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" />
                                <asp:Button ID="Button1" runat="server" 
                                style="color: #FFCCCC; background-color: #FF3300" Text="EXPORT BUTTON" 
                                onclick="Button1_Click" />
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" ToolPanelView="None" 
                    onnavigate="criprt_navigation" ToolPanelWidth="50px" Width="50px" height="30px"
                    BestFitPage="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
