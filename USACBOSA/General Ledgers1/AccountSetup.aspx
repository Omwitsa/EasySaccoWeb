<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementAdmin/ManagementAdmin.Master" AutoEventWireup="true" CodeBehind="AccountSetup.aspx.cs" Inherits="USACBOSA.offsetting.AccountSetup" %>
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
            width: 138px;
        }
        .style5
        {
            width: 166px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Account Setup" Font-Bold="True" 
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
                                    <td class="style3">
                                        <asp:Label ID="Label5" runat="server" style="color: #000000" 
                                            Text="GL Account No"></asp:Label>
                                        
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="txtaccno" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label6" runat="server" style="color: #000000" 
                                            Text="GL Account Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaccname" runat="server" Height="20px" Width="231px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label7" runat="server" style="color: #000000" 
                                            Text="Account Type"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="cboaccoounttype" runat="server" Height="22px" 
                                            Width="108px" AutoPostBack="True" 
                                            onselectedindexchanged="cboaccoounttype_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Income Statement</asp:ListItem>
                                            <asp:ListItem>Balance Sheet</asp:ListItem>
                                            <asp:ListItem>Retained Earnings</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label8" runat="server" style="color: #000000" 
                                            Text="Account Group"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboAccGroup" runat="server" Height="22px" Width="131px" 
                                            AutoPostBack="True" onselectedindexchanged="cboAccGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label9" runat="server" style="color: #000000" 
                                            Text="Account Sub Group"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="cboaccountgroup" runat="server" Height="23px" 
                                            Width="116px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Assets</asp:ListItem>
                                            <asp:ListItem>Capital</asp:ListItem>
                                            <asp:ListItem>Depreciation Expenses</asp:ListItem>
                                            <asp:ListItem>Expenses</asp:ListItem>
                                            <asp:ListItem>Income</asp:ListItem>
                                            <asp:ListItem>Liabilities</asp:ListItem>
                                            <asp:ListItem>Retained Earnings</asp:ListItem>
                                            <asp:ListItem>Suspense </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label10" runat="server" style="color: #000000" 
                                            Text="Normal Balance"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cbonormalbalance" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label11" runat="server" style="color: #000000" Text="Currency"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="cbocurrency" runat="server" Height="21px" Width="104px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>KES</asp:ListItem>
                                            <asp:ListItem>USD</asp:ListItem>
                                            <asp:ListItem>GBP</asp:ListItem>
                                            <asp:ListItem>TSH</asp:ListItem>
                                            <asp:ListItem>USH</asp:ListItem>
                                            <asp:ListItem>ZAR</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label12" runat="server" style="color: #000000" 
                                            Text="Account Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboAccType" runat="server" Height="25px" Width="113px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Nominal</asp:ListItem>
                                            <asp:ListItem>Member</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label13" runat="server" style="color: #000000" 
                                            Text="Acc Category"></asp:Label>
                                    </td>
                                    <td class="style4" style="color: #000000">
                                        <asp:DropDownList ID="cboacccategory" runat="server" Height="21px" 
                                            Width="140px">
                                            <asp:ListItem>BOSA</asp:ListItem>
                                            <asp:ListItem>Operating Incomes</asp:ListItem>
                                            <asp:ListItem>Operating Expenses</asp:ListItem>
                                            <asp:ListItem>Operating Assets</asp:ListItem>
                                            <asp:ListItem>Operating Liabilities</asp:ListItem>
                                            <asp:ListItem>Investing Activities</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label15" runat="server" style="color: #000000" 
                                            Text="Balance As At"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="dtpTransDate" runat="server"></asp:TextBox>
                                        <asp:Button ID="Button4" runat="server" Text="F" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label14" runat="server" style="color: #000000" 
                                            Text="Sub Category"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="cboSubType" runat="server" Height="22px" Width="112px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Loans</asp:ListItem>
                                            <asp:ListItem>Interests</asp:ListItem>
                                            <asp:ListItem>Shares</asp:ListItem>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                        <asp:Label ID="Label16" runat="server" style="color: #000000" 
                                            Text="Opening Balance"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOpeningBalance" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:CheckBox ID="chkSuspense" runat="server" style="color: #000000" 
                                            Text="Is Suspense AC" AutoPostBack="True" 
                                            oncheckedchanged="chkSuspense_CheckedChanged" />
                                    </td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style5">
                                        <asp:CheckBox ID="chkRetainedEarning" runat="server" style="color: #000000" 
                                            Text="Is the Reain Earnings AC" AutoPostBack="True" 
                                            oncheckedchanged="chkRetainedEarning_CheckedChanged" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        &nbsp;</td>
                                    <td class="style4">
                                        <asp:Button ID="cmdsave" runat="server" onclick="Button1_Click" Text="Save" />
&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="cmdedits" runat="server" onclick="Button2_Click" 
                                            Text="Update" />
                                    </td>
                                    <td class="style5">
                                        <asp:Button ID="cmdcancel" runat="server" onclick="Button3_Click" 
                                            Text="Delete" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3" colspan="4">
                                    <div style="width: 100%; height: 185px; overflow: scroll" class="style5">
                                        
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                            Font-Size="8pt" Visible="False" Width="728px" 
                                            AutoGenerateSelectButton="True" GridLines="Horizontal" 
                                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                        </asp:GridView>
                                         </div>
                                         </td>
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
