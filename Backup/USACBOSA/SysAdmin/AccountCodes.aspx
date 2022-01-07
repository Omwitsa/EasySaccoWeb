<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="AccountCodes.aspx.cs" Inherits="USACBOSA.SysAdmin.AccountCodes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80%;
        }
        .style2
        {
            width: 100%;
            height: 335px;
        }
        .style3
        {
        }
        .style4
        {
            width: 92px;
        }
        .style5
        {
            width: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            </table>
    <table class="style1">
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                            <table class="style2">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" Text="Category ID"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        
                                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" 
                                            ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" style="color: #000000" 
                                            Text="Account Group"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="110px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" style="color: #000000" Text="Main Group"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DropDownList2" runat="server" Height="28px" Width="125px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" style="color: #000000" 
                                            Text="Descriptions"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="226px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style3" colspan="3">
                                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Save" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Update" Width="61px" />
&nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Delete" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                    <div style="width: 100%; height: 256px; overflow: scroll" class="style5">
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                            Font-Size="8pt" Visible="False" Width="728px" 
                                            AutoGenerateSelectButton="True" Height="91px" 
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
