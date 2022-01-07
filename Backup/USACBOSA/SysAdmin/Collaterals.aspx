<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Collaterals.aspx.cs" Inherits="USACBOSA.SysAdmin.Collaterals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style1
    {
        width: 4px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Collateral Code" 
                style="float: right"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Collateral Description" 
                style="float: right"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            <asp:Label ID="Label3" runat="server" style="float: right" 
                Text="Percentage Taken"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPercent" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                style="float: right" Text="Save" />
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                Text="Update" />
&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click" />
            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="28px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<div style="width: 100%; height: 285px; overflow: scroll" class="style5">
<asp:GridView ID="GridView" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    onselectedindexchanged="GridView_SelectedIndexChanged" PageSize="5" 
                    Width="100%">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                </div>
</asp:Content>
