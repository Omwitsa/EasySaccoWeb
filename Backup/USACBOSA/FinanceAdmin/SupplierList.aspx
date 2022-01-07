<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="USACBOSA.FinanceAdmin.SupplierList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
        width: 159px;
    }
        .style2
        {
            width: 216px;
        }
        .style3
        {
            width: 166px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
                <asp:Label ID="Label5" runat="server" Text="Suppliers" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="155px" Height="16px">
                </asp:DropDownList>
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
                <asp:Label ID="Label6" runat="server" Text="Expenses" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;</td>
            <td class="style2">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
                <asp:Label ID="Label7" runat="server" Text="Amount Charged" style="font-weight: 700"></asp:Label>
                &nbsp;</td>
            <td class="style2">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click" 
                    style="font-weight: 700" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
