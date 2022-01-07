<%@ Page Title="" Language="C#" MasterPageFile="~/Branches.Master" AutoEventWireup="true" CodeBehind="RgnAgents.aspx.cs" Inherits="USACBOSA.Regions.RgnAgents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
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
            width: 174px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                            <table class="style2">
                                <tr>
                                    <td colspan="6">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" 
                                            Text="Agents Names"></asp:Label>

                                    </td>
                                    <td class="style4">

                                        <asp:TextBox ID="TextBox1" runat="server" Width="268px"></asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:Label ID="Label6" runat="server" style="color: #000000" Text="Dates"></asp:Label>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000" Text="Gender"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="131px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" style="color: #000000" Text="Agents IDNo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="145px" AutoPostBack="True" 
                                            ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label9" runat="server" style="color: #000000" 
                                            Text="Land Line No"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="132px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" style="color: #000000" Text="Mobile No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server" Width="144px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label11" runat="server" style="color: #000000" 
                                            Text="Home Address"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="132px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" style="color: #000000" Text="Town"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                            style="color: #000000" Text="Save" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Update" />
&nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Delete" />
                                    </td>
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
                                    <td class="style3" colspan="6">
                                     <div style="width: 100%; height: 185px; overflow: scroll" class="style5">
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                            Font-Size="8pt" Visible="False" Width="728px" 
                                             AutoGenerateSelectButton="True" 
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
                                         </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
