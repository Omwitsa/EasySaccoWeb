<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportsViewer.aspx.cs" Inherits="USACBOSA.Reports.ReportsViewer" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        src = "/crystalreportviewers13/js/crviewer/crv.js"
        function goBack() {
            history.go(-1);
        }
</script>
    
    <table align="center" class="style1">
        <tr>
            <td>
                &lt;=<asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="GO Back" />
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" ToolPanelView="None" Font-Size="Small" />
            </td>
        </tr>
    </table>
</asp:Content>