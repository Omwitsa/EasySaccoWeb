<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="TrialBalance.aspx.cs" Inherits="USACBOSA.General_Ledgers.TrialBalance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80%;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 215px;
        }
        .style4
        {
            width: 72px;
        }
        .style5
        {
            width: 69px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" Text="Start Date"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        
                                         <asp:TextBox ID="dtpStartDate" runat="server" Width="130px" Font-Size="8pt" 
                                             TabIndex="10" Height="25px"></asp:TextBox>
                     <asp:CalendarExtender ID="dtpStartDate_CalendarExtender" Enabled="true" 
                                             PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="dtpStartDate">
                            </asp:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Images/calendar.png" />
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label6" runat="server" style="color: #000000" Text="End Date"></asp:Label>
                                    </td>
                                    <td>
                                        
                                         <asp:TextBox ID="dtpFinishDate" runat="server" Width="128px" Font-Size="8pt" 
                                             TabIndex="10" Height="24px"></asp:TextBox>
                     <asp:CalendarExtender ID="dtpFinishDate_CalendarExtender" Enabled="true" 
                                             PopupButtonID="ImageButton2" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="dtpFinishDate">
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
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        <asp:Button ID="btnProcess" runat="server" Text="Process" 
                                            Width="64px" onclick="btnProcess_Click" />
                                        <asp:Button ID="Button2" runat="server" 
                                            Text="Print Income statement" Width="148px" onclick="Button2_Click" />
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="Button5" runat="server" 
                                            Text="Cummulative TB" Visible="False" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        <asp:Button ID="Button3" runat="server" 
                                            Text="Print TB" onclick="Button3_Click" />
                                        <asp:Button ID="Button4" runat="server" 
                                            Text="Print Balancesheet" Width="137px" onclick="Button4_Click" />
                                    </td>
                                    <td class="style5">
                                        <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
                                            Text="Cashflow " Width="79px" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td class="style5">
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
