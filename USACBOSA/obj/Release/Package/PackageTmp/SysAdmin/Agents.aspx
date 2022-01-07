<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Agents.aspx.cs" Inherits="USACBOSA.SysAdmin.Agents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            width: 96%;
        }
        .style2
        {
            width: 100%;
        }
        .style4
        {
            width: 174px;
        }
        .style5
        {
            height: 26px;
        }
        .style6
        {
            width: 174px;
            height: 26px;
        }
        .style7
        {
            width: 85px;
        }
        .style8
        {
            height: 26px;
            width: 85px;
        }
        .style9
        {
            width: 100px;
        }
        .style10
        {
            height: 26px;
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Recruitment Staff Registration" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
    <table class="style1">
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                            <table class="style2">
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000; float: right;" 
                                            Text="Staff Names:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>

                                    </td>
                                    <td class="style4">

                                        <asp:TextBox ID="TextBox1" runat="server" Width="268px"></asp:TextBox>

                                    </td>
                                    <td class="style7">

                                        <asp:Label ID="Label13" runat="server" ForeColor="Black" style="float: right" 
                                            Text="Staff No.:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"></asp:Label>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtStaffCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000; float: right;" 
                                            Text="Gender:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="131px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style7">
                                        <asp:Label ID="Label8" runat="server" style="color: #000000; float: right;" 
                                            Text="ID No.:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="126px" AutoPostBack="True" 
                                            ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style10">
                                        <asp:Label ID="Label14" runat="server" ForeColor="Black" style="float: right" 
                                            Text="Occupation:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            Height="17px"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        <asp:TextBox ID="txtOccupation" runat="server" Width="159px"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label15" runat="server" ForeColor="Black" style="float: right" 
                                            Text="Station:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"></asp:Label>
                                    </td>
                                    <td class="style5">
                    <asp:DropDownList ID="cboStation" runat="server" Height="24px" Width="123px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style10">
                                        <asp:Label ID="Label9" runat="server" style="color: #000000; float: right;" 
                                            Text="Land Line No:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="132px"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label10" runat="server" style="color: #000000; float: right;" 
                                            Text="Mobile No.:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="126px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style10">
                                        <asp:Label ID="Label11" runat="server" style="color: #000000; float: right;" 
                                            Text="Address:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                        </td>
                                    <td class="style6">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="153px"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label12" runat="server" style="color: #000000; float: right;" 
                                            Text="Town:" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                            style="color: #000000" Text="Save" Font-Bold="True" Font-Size="10pt" 
                                            ForeColor="Black" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Update" Font-Bold="True" Font-Size="10pt" 
                                            ForeColor="Black" />
&nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Delete" Font-Bold="True" Font-Size="10pt" 
                                            ForeColor="Black" />
                                    </td>
                                    <td class="style7">

                                        <asp:Label ID="Label6" runat="server" style="color: #000000; float: right;" 
                                            Text="Dates:" Visible="False" Font-Bold="True" Font-Names="Tahoma" 
                                            Font-Size="9pt" ForeColor="Black"></asp:Label>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <hr style="color: Maroon" />
                                     <div style="width: 100%; height: 185px; overflow: scroll" class="style5">
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                            Font-Size="8pt" Visible="False" Width="887px" 
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
</asp:Content>
