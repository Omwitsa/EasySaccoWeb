﻿<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="USACBOSA.Changepwd.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 21px;
        }
        .style14
        {
            width: 108px;
            height: 26px;
        }
        .style15
        {
            width: 208px;
            height: 26px;
            text-align: right;
        }
        .style18
        {
            width: 310px;
            height: 26px;
        }
        .style19
        {
            height: 26px;
        }
        .style22
        {
            text-align: right;
            width: 208px;
        }
        .style20
        {
            color: #000000;
            float: right;
        }
        .style23
        {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table style="width:100%; height: 203px;">
        <tr>
            <td class="style23" colspan="5">
                
                <table style="width:100%; height: 25px;">
            <tr>
                <td>
    <asp:Label ID="Label5" runat="server" Text="System Users" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                
                                </td>
                <td>
                </td>
            </tr>
    </table>
    <hr style="color: Maroon; height: 2px;" />
          <table> 
        <tr>
            <td class="style14">
                <asp:Label ID="Label10" runat="server" CssClass="style20" Text="User Type:"></asp:Label>
            </td>
            <td class="style15">
                <asp:DropDownList ID="DropDownUserTypes" runat="server" Font-Bold="False" 
                    Font-Size="10pt" Height="20px" Width="150px" 
                    onselectedindexchanged="DropDownUserTypes_SelectedIndexChanged">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>System Administrator</asp:ListItem>
                    <asp:ListItem>Finance Officer</asp:ListItem>
                    <asp:ListItem>Loans Officer</asp:ListItem>
                    <asp:ListItem>Customer Service</asp:ListItem>
                    <asp:ListItem>Branch Managers</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style18">
                <asp:Label ID="Label12" runat="server" CssClass="style20" 
                    Text="Uers Full Names:"></asp:Label>
            </td>
            <td class="style18">
                <asp:TextBox ID="txtRUserName" runat="server" Width="247px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style14">
                <asp:Label ID="Label16" runat="server" style="color: #000000; float: right;" 
                    Text="Branch/Station:"></asp:Label>
            </td>
            <td class="style15">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td class="style18">
                <asp:Label ID="Label11" runat="server" CssClass="style20" Text="User Login Id:"></asp:Label>
            </td>
            <td class="style18">
                <asp:TextBox ID="txtUserLoginId" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="style19">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label14" runat="server" CssClass="style20" Text="Password:"></asp:Label>
            </td>
            <td class="style22">
                <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label15" runat="server" CssClass="style20" 
                    Text="Confirm Password:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            </td>
            <td style="width: 310px">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="Save" 
                    Width="68px" style="float: right" />
                </td>
            <td class="style22">
                <asp:Button ID="Button7" runat="server" onclick="Button7_Click" Text="Update" 
                    style="float: left" Width="55px" />
                <asp:Button ID="Button8" runat="server" onclick="Button8_Click" 
                    Text="Reset password" Width="121px" />
            </td>
            <td class="style4">
                &nbsp;
                <asp:Label ID="Label17" runat="server" style="color: #000000; float: right;" 
                    Text="Password Expires After:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="TextBox1" runat="server" Width="40px" 
                    ForeColor="Red">90</asp:TextBox>
                <asp:Button ID="Button6" runat="server" onclick="Button6_Click1" 
                    Text="Return" />
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <hr style="color: Maroon; height: 2px;" />
      <asp:GridView ID="GridView1" runat="server" 
                    AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" PageSize="5" 
                    Width="100%" onselectedindexchanged="GridView1_SelectedIndexChanged" 
                    onpageindexchanging="GridView1_PageIndexChanging">
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
