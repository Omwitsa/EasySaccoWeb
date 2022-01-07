<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="MembershipDefaults.aspx.cs" Inherits="USACBOSA.Setup.MembershipDefaults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
    <tr>
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
                    <asp:DropDownList ID="cboAccno" runat="server" Height="21px" 
                        style="float: left" Width="99px" AutoPostBack="True" 
                        onselectedindexchanged="cboAccno_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
        <td>
                    <asp:TextBox ID="txtAccNames" runat="server" Width="225px" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Value"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="chkIsContribution" runat="server" AutoPostBack="True" 
                oncheckedchanged="chkIsContribution_CheckedChanged" Text="Is Contribution" />
        </td>
        <td>
            <asp:DropDownList ID="cboSharesCode" runat="server" Height="16px" Width="135px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                Width="71px" />
        </td>
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
    </tr>
</table>
</asp:Content>
