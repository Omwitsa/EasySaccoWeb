<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true"  CodeBehind="trbnce.aspx.cs" Inherits="USACBOSA.General_Ledgers.trbnce" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            width: 80%;
        }
        .style2
        {
            width: 100%;
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
                            <table class="style2">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" Text="Start Date"></asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox ID="TextBox1" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
         <asp:CalendarExtender ID="TextBox1_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox1">
                            </asp:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Images/calendar.png" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" style="color: #000000" Text="End Date"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="TextBox2" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
         <asp:CalendarExtender ID="TextBox2_CalendarExtender1" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox2">
                            </asp:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton2" runat="server" 
                                            ImageUrl="~/Images/calendar.png" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
