<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="BranchCodes.aspx.cs" Inherits="USACBOSA.SysAdmin.BranchCodes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
    .style1
    {
            width: 103px;
        }
        .style2
        {
            width: 228px;
        }
        .style3
        {
            width: 107px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label8" runat="server" Text="Sacco Branches" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
    <table style="width: 100%;">
    <tr>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Text="Branch Code:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label4" runat="server" Text="Address:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtaddress" runat="server" Width="213px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label2" runat="server" Text="Branch Name:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtName" runat="server" Width="214px"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label5" runat="server" Text="Telephone No.:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTelephone" runat="server" Width="155px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label3" runat="server" Text="Physical Address:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right" Width="98px"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtphysicaladdress" runat="server" Width="212px"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label6" runat="server" Text="Email Address:" Font-Bold="True" 
                Font-Size="9pt" ForeColor="Black" style="float: right"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtBranch" runat="server" Width="157px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                style="float: right" Text="Save" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" ForeColor="Black" />
        </td>
        <td class="style2">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                onclick="btnUpdate_Click" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" ForeColor="Black" />
        </td>
        <td class="style3">
            <asp:Label ID="Label7" runat="server" Text="id" Visible="False"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<hr style="color: Maroon" />
<asp:GridView ID="GridView" runat="server" 
             onselectedindexchanged="GridView_SelectedIndexChanged1" Width="904px" 
                   BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                   CellPadding="4">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
             </asp:GridView>
</asp:Content>
