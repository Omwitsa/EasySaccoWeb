<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="ProcessDividend.aspx.cs" Inherits="USACBOSA.FinanceAdmin.ProcessDividend" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td class="style11">
                &nbsp;
            </td>
            <td class="style12">
                </td>
            <td class="style13">
                </td>
            <td class="style14">
                </td>
            <td class="style13">
                &nbsp;
            </td>
            <td class="style17">
                </td>
            <td class="style18">
                </td>
            <td class="style12">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
            </td>
            <td class="style5">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
            </td>
            <td class="style5">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Interest on Deposits" 
                    AutoPostBack="True" />
            </td>
            <td class="style2">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                %</td>
            <td class="style9">
                <asp:Label ID="Label9" runat="server" Text="Tax on Deposits"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                %</td>
            <td class="style7">
                <asp:Label ID="Label7" runat="server" Text="To:"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="TextBox4_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox4">
                            </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" Text="Share Code"></asp:Label>
            </td>
            <td class="style6">
                <asp:DropDownList ID="cboShareCode" runat="server">
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:Label ID="Label10" runat="server" Text="Processing fee"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="TextBox6" runat="server" Text="50"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5" colspan="11">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style2">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="PROCESS" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
