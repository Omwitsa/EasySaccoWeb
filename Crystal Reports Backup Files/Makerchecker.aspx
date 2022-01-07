<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="Makerchecker.aspx.cs" Inherits="USACBOSA.FinanceAdmin.Makerchecker" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {            height: 25px;
        }
        .style2
        {
            width: 212px;
        }
        .style3
        {
            width: 212px;
            height: 21px;
        }
        .style4
        {
            height: 21px;
        }
        .style5
        {
            width: 135px;
        }
        .style6
        {
            height: 21px;
            width: 135px;
        }
        .style7
        {
            width: 228px;
        }
        .style8
        {
            height: 21px;
            width: 228px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style5">
                &nbsp;
            </td>
            <td class="style7">
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1" colspan="3">
                &nbsp;
                <asp:Label ID="Label5" runat="server" Text="MAKER-CHECKER" 
                    style="font-weight: 700; font-size: larger"></asp:Label>
                &nbsp;
                &nbsp;
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                &nbsp;
            </td>
            <td class="style8">
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style6">
                </td>
            <td class="style8">
                </td>
            <td class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <ajaxToolkit:Accordion ID="Accordion1" runat="server" Height="109px" 
                    Width="465px">
                </ajaxToolkit:Accordion>
            </td>
            <td class="style5">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style6">
                </td>
            <td class="style8">
                </td>
            <td class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
