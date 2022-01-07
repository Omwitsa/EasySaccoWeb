<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="WithdrawalNotice.aspx.cs" Inherits="USACBOSA.FinanceAdmin.WithdrawalNotice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
            height: 148px;
        }
        .style4
        {
        }
        .style5
        {
        }
        .style6
        {
        }
        .style7
        {}
    </style>
     <script type="text/javascript">

         function checkDate(sender, args) {
             if (sender._selectedDate > new Date()) {
                 alert("You cannot select a day earlier than today!");
                 sender._selectedDate = new Date();
                 // set the date back to the current date
                 sender._textbox.set_Value(sender._selectedDate.format(sender._format))
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
          </asp:ScriptManager>
            <table class="style1">
                <tr>
                    <td>
                        <table class="style2">
                            <tr>
                                <td>
                                    <table class="style2">
                                        <tr>
                                            <td colspan="7">
                                                <asp:Label ID="Label5" runat="server" style="color: #000000" Text="Member No"></asp:Label>
                                                &nbsp;
                                                <asp:TextBox ID="txtMemberno" runat="server" AutoPostBack="True" 
                                                    ontextchanged="TextBox1_TextChanged" Width="106px"></asp:TextBox>
                                                &nbsp;<asp:Button ID="Button1" runat="server" Enabled="False" Text="F" />
                                                &nbsp;
                                                <asp:TextBox ID="txtmembernames" runat="server" Width="239px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style6" colspan="3">
                                                <asp:Label ID="Label6" runat="server" style="color: #000000" 
                                                    Text="Membership Status"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="Status" runat="server" style="color: #000099; font-weight: 700;" 
                                                    Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblcompany" runat="server" 
                                                    style="color: #0000CC; font-weight: 700;" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style5" colspan="2">
                                                <asp:CheckBox ID="chkSixty" runat="server" Text="Sixty(60) days Notice" 
                                                    AutoPostBack="True" oncheckedchanged="chkSixty_CheckedChanged" />
                                            </td>
                                            <td class="style7" colspan="4">
                                                <asp:CheckBox ID="chkSeven" runat="server" Text="Seven(7) days Notice" 
                                                    AutoPostBack="True" oncheckedchanged="chkSeven_CheckedChanged" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style5" colspan="2">
                                                <asp:CheckBox ID="CheckBox1" runat="server" 
                                                    Text="Funeral Expenses (60) days Notice" AutoPostBack="True" 
                                                    oncheckedchanged="CheckBox1_CheckedChanged" /></td>
                                            <td class="style7" colspan="4">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style3" colspan="5">
                                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                        Font-Size="8pt" GridLines="Vertical" Height="122px" Width="556px">
                                                        <AlternatingRowStyle BackColor="#DCDCDC" />
                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#000065" />
                                                    </asp:GridView>
                                            </td>
                                            <td class="style3">
                                                <br />
                                                <br />
                                                <br />
                                            </td>
                                            <td class="style3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:GridView ID="GridView2" runat="server" BackColor="White" 
                                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                    Font-Size="7.5pt" GridLines="Vertical" Height="122px" Width="559px">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style6">
                                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                                                    onclick="LinkButton1_Click">Guaranteed Loans</asp:LinkButton>
                                            </td>
                                            <td colspan="3">
                                                <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" 
                                                    onclick="LinkButton2_Click">Members Loans</asp:LinkButton>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style6">
                                                <asp:Label ID="Label7" runat="server" Text="Withdrawal Fee Acc"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="cboAccno" runat="server" Height="16px" Width="80px" 
                                                    AutoPostBack="True" onselectedindexchanged="cboAccno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAccNames" runat="server" Width="309px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style6">
                                                <asp:Label ID="Label8" runat="server" Text="Withdaral Fee"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtWithFee" runat="server" Width="78px">0.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Withdrawal Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="dtpExpWithdrawDate" runat="server" ForeColor="#FF3300"></asp:TextBox>
                                                     <asp:CalendarExtender ID="dtpExpWithdrawDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="dtpExpWithdrawDate">
                            </asp:CalendarExtender>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style6">
                                                <asp:Label ID="Label9" runat="server" Text="Withdawal Reason"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtReason" runat="server" 
                                                    ontextchanged="txtReason_TextChanged" TextMode="MultiLine" Width="209px"></asp:TextBox>
                                            &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4" colspan="6">
                                                <asp:Button ID="btnNotify" runat="server" Enabled="false" onclick="btnNotify_Click" 
                                                    Text="Notify" Width="109px" />
                                            &nbsp;
                                                <asp:Button ID="btnshareoffset" runat="server" onclick="btnshareoffset_Click" 
                                                    Text="Share Transfer/Offsetting" Width="179px" />
&nbsp;
                                                <asp:Button ID="btnWithdaraw" runat="server" Text="Withdraw" 
                                                    onclick="btnWithdaraw_Click" Width="100px" />
                                            </td>
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
