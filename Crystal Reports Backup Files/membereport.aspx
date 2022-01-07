<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="membereport.aspx.cs" Inherits="USACBOSA.FinanceAdmin.membereport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style4
        {
            width: 219px;
        }
        .style5
        {
            height: 20px;
            width: 219px;
        }
        .style6
        {
            width: 171px;
        }
        .style7
        {
            height: 20px;
            width: 171px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">  
    <asp:View ID="View1" runat="server">
     <table style="width: 100%;">
        <tr>
            <td class="style6">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label5" runat="server" style="font-weight: 700" Text="Station"></asp:Label>
                &nbsp;
            </td>
            <td class="style4">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="145px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
            <td class="style5">
                &nbsp;</td>
            <td class="style1">
            </td>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td class="style4">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    style="font-weight: 700" Text="View Report" />
            </td>
            <td>
                &nbsp;</td>
            <td>
         

                &nbsp;</td>
        </tr>
        
  
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>

                &nbsp;</td>
        </tr>
    </table>
         </asp:View>
 <asp:View ID="View2" runat="server">
   
                       
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" ToolPanelView="None" 
                    onnavigate="criprt_navigation" ToolPanelWidth="50px" Width="50px" height="30px"
                    BestFitPage="true" />
                 
                </asp:View>
      </asp:MultiView>
</asp:Content>
