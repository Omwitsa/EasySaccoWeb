<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="USACBOSA.SysAdmin.Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style1
    {
        height: 29px;
    }
    .style2
    {
        height: 31px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Locations" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900" 
    style="position: relative; text-align: center"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style2">
                    </td>
                <td class="style2">
                    <asp:Label ID="Label8" runat="server" style="float: right" 
                        Text="Location Type:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:DropDownList ID="cboLocationtype" runat="server" Height="24px" 
                        Width="146px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                        <asp:ListItem>Diaspora</asp:ListItem>
                        <asp:ListItem>County</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
                <td class="style1">
                    <asp:Label ID="Label6" runat="server" style="float: right" 
                        Text="Location Code:"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtLocationCode" runat="server" Width="151px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style2">
                    <asp:Label ID="Label7" runat="server" style="float: right" Text="Location Name"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtLocationName" runat="server" Width="152px"></asp:TextBox>
                    <asp:TextBox ID="txtLocationId" runat="server" Visible="False" Width="66px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" style="float: right" Text="Save" 
                        onclick="btnSave_Click" />
                </td>
                <td>
                    <asp:Button ID="btnUpdateLocation" runat="server" 
                        onclick="btnUpdateLocation_Click" Text="Update" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                        onclick="btnDelete_Click" />
         
                </td>
            </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="10" 
        Width="100%" onpageindexchanging="GridView1_PageIndexChanging" 
    onpageindexchanged="GridView1_PageIndexChanged">
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
        </asp:Content>
