<%@ Page Title="" Language="C#" MasterPageFile="~/Branches.Master" AutoEventWireup="true" CodeBehind="Membersrpt.aspx.cs" Inherits="USACBOSA.Regions.Membersrpt" %>
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
        }
        .style4
        {
            width: 26px;
        }
        .style5
        {
            width: 70px;
        }
        .style6
        {
            width: 169px;
        }
        .style7
        {
            width: 144px;
        }
        .style8
        {
            width: 62px;
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
                                    <td class="style4">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" Text="Regions"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="21px" Width="106px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label6" runat="server" style="color: #000000" Text="Date From"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
                     <asp:CalendarExtender ID="TextBox1_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox1">
                            </asp:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Images/calendar1.png" />
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000" Text="Date To"></asp:Label>
                                    </td>
                                    <td class="style7">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="113px" Font-Size="8pt" 
                                            TabIndex="10" Height="19px"></asp:TextBox>
                     <asp:CalendarExtender ID="TextBox2_CalendarExtender" Enabled="true" PopupButtonID="ImageButton2" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox2">
                            </asp:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton2" runat="server" 
                                            ImageUrl="~/Images/calendar1.png" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Show" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style5" colspan="2">
                                        &nbsp;</td>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3" colspan="7">
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
