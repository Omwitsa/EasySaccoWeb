<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Bank Setup.aspx.cs" Inherits="USACBOSA.SysAdmin.Bank_Setup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 64px;
        }
        .style4
        {
            width: 159px;
        }
        .style5
        {
            width: 130px;
        }
        .style6
        {
            width: 106px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <table style="width:100%;">
            <tr>
                <td>
    <asp:Label ID="Label1" runat="server" Text="Banks Setup" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
      <hr style="color: Maroon; height: 2px;" />
                <table class="style2">
                    <tr>
                        <td>
                            <table class="style2">
                                <tr>
                                    <td class="style6">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000; float: right;" 
                                            Text="Bank Code:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" 
                                            ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" style="color: #000000; float: right;" 
                                            Text="Bank Name:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="220px"></asp:TextBox>
                                    </td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000; float: right;" 
                                            Text="Bank Accno:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" style="color: #000000; float: right;" 
                                            Text="Account Type:" Font-Bold="True" ForeColor="Black" Width="93px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList3" runat="server" Height="21px" Width="101px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Current</asp:ListItem>
                                            <asp:ListItem>Savings</asp:ListItem>
                                            <asp:ListItem>Float</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:Label ID="Label10" runat="server" style="color: #000000; float: right;" 
                                            Text="Phone No:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" style="color: #000000; float: right;" 
                                            Text="Address:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:Label ID="Label12" runat="server" style="color: #000000; float: right;" 
                                            Text="Associated GL:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                            Height="26px" onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                                            Width="124px">
                                        </asp:DropDownList>
                                        <asp:Button ID="Button1" runat="server" Enabled="False" Text="F" 
                                            Font-Bold="True" Height="24px" />
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="276px"></asp:TextBox>
                                    </td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:Label ID="Label9" runat="server" style="color: #000000; float: right;" 
                                            Text="Branch Name:" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox4" runat="server" Width="137px"></asp:TextBox>
                                    </td>
                                    <td colspan="3">
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        &nbsp;</td>
                                    <td colspan="4">
                                        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Save" 
                                            Font-Bold="True" />
                                    &nbsp;
                                        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Update" 
                                            Font-Bold="True" />
&nbsp; <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Delete" Font-Bold="True" />
                                    </td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                  <hr style="color: Maroon; height: 2px;" />
                    <div style="width: 100%; height: 285px; overflow: scroll" class="style5">
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                            Font-Size="8pt" Width="694px" AutoGenerateSelectButton="True" 
                                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                        </asp:GridView>
                                        </div>
</asp:Content>
