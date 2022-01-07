<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Defaults.aspx.cs" Inherits="USACBOSA.SysAdmin.Defaults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            width: 169px;
        }
        .style4
        {
            width: 119px;
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
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" Text="AccNo"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="txtAccno" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="TxtRemarks" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000" Text="Amount"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="txtAmnt" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" style="color: #000000" 
                                            Text="Contribution"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContribution" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Label ID="Label9" runat="server" style="color: #000000" Text="SharesCode"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        <asp:DropDownList ID="drpsharescode" runat="server" Height="21px" Width="105px">
                                        </asp:DropDownList>
                                    </td>
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
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Save" />
&nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Update" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Delete" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" 
                                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                                            CellPadding="4" onselectedindexchanged="GridView1_SelectedIndexChanged" 
                                            Width="685px">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
