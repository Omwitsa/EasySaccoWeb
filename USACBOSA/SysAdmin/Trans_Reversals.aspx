<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="Trans_Reversals.aspx.cs" Inherits="USACBOSA.FinanceAdmin.Trans_Reversals" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
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
    <style type="text/css">
        .style1
        {
            width: 123px;
        }
        .style2
        {
            width: 118px;
        }
        .style4
        {
            width: 128px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Receipt Posting - Transactions Reversals" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td class="style4">
                    <asp:Label ID="Label24" runat="server" style="float: right" Text="Receipt No:"></asp:Label>
                    </td>
            <td >
                    <asp:TextBox ID="txtReceiptNo" runat="server" Width="110px" 
                        ontextchanged="txtReceiptNo_TextChanged" AutoPostBack="True" Height="22px"></asp:TextBox>
                   
                    </td>
            <td >
                    <asp:Label ID="Label23" runat="server" style="float: right" 
                    Text="Serial No:" Width="63px"></asp:Label>
                   
                    </td>
            <td >
                    <asp:TextBox ID="txtSerialNo" runat="server" Width="137px" BackColor="#CCFFCC"></asp:TextBox>
                   
                    </td>
            <td colspan="2" 
                  </td>
                <asp:Label ID="Label26" runat="server" Font-Size="8pt" ForeColor="#FF6699" 
                    Text="ALWAYS CONFIRM THAT THE DETAILS TO BE REVERSED CORRESPONDS THE PREVIOUS "></asp:Label>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label6" runat="server" Text="Member No:" style="float: right" 
                        Width="77px"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="txtMemberNo" runat="server" Height="20px" Width="110px" 
                        ontextchanged="txtMemberNo_TextChanged" AutoPostBack="True" 
                        ondatabinding="txtMemberNo_DataBinding" BackColor="#CCFFCC" 
                        ReadOnly="True"></asp:TextBox>
                    </td>
            <td colspan="2" >
                    <asp:TextBox ID="txtNames" runat="server" Width="265px" BackColor="#CCFFCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            <td colspan="2">
                    <asp:CheckBox ID="chkAcruedInterest" runat="server" 
                        Text="Enable Acrued Interest" style="text-align: center" 
                        Enabled="False" />
                    
                </td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label10" runat="server" Text="Bank A/C :" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="cboBankAC" runat="server" BackColor="#CCFFCC" Height="22px" 
                        Width="110px" ReadOnly="True"></asp:TextBox>
                </td>
            <td colspan="2" >
                    <asp:TextBox ID="txtBankAC" runat="server" Width="265px" BackColor="#CCFFCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            <td colspan="2">
                    <asp:CheckBox ID="Chckbxprintrcpt" runat="server" Checked="False" 
                        style="color: #000000" Text=" Enable Receipt Print" Enabled="False" />
                </td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label11" runat="server" Text="Payment Mode:" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="cboPaymentMode" runat="server" BackColor="#CCFFCC" 
                        Height="22px" Width="110px" ReadOnly="True"></asp:TextBox>
                </td>
            <td >
                    <asp:TextBox ID="txtChequeNo" runat="server" AutoPostBack="True" Width="110px" 
                        BackColor="#CCFFCC" Height="22px" ReadOnly="True"></asp:TextBox>
                   
                </td>
            <td >
                    <asp:Label ID="Label14" runat="server" Text="Date Deposited:" 
                        style="float: right; " Width="95px" ></asp:Label>
                </td>
            <td colspan="2">
                    <asp:TextBox ID="txtDateDeposited" runat="server" Width="110px" Font-Size="8pt" format = "dd-MM-yyyy"
                        TabIndex="10" Height="22px" BackColor="#CCFFCC"></asp:TextBox>
                     <asp:CalendarExtender ID="txtDateDeposited_CalendarExtender" Enabled="true" 
                        PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtDateDeposited" 
                        onclientdateselectionchanged="checkDate">
                            </asp:CalendarExtender>
                   
                </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label16" runat="server" Text="Distributed Amt.:" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="txtDistributedAmount" runat="server" Width="110px" ForeColor="#0033CC" 
                        ReadOnly="True" BackColor="#CCFFCC"></asp:TextBox>
                    </td>
            <td >
                    &nbsp;</td>
            <td >
                    <asp:Label ID="Label21" runat="server" Text="Contribution Date:" 
                        style="float: right"></asp:Label>
                </td>
            <td>
                         <asp:TextBox ID="txtContribDate" runat="server" Width="110px" Font-Size="8pt" format = "dd-MM-yyyy"
                             Height="22px" BackColor="#CCFFCC"></asp:TextBox>
                     <asp:CalendarExtender ID="txtContribDate_CalendarExtender2" Enabled="true" 
                             PopupButtonID="ImageButton2" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtContribDate" 
                             onclientdateselectionchanged="checkDate">
                            </asp:CalendarExtender>
                    </td>
            <td>
                    <asp:CheckBox ID="chkConfirmReversal" runat="server" Text="Confirm Reversal" />
                    </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label13" runat="server" Text="Receipt Amount:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style1">
                    <asp:TextBox ID="txtReceiptAmount" runat="server"
                        AutoPostBack="True" ontextchanged="txtReceiptAmount_TextChanged" 
                        Width="110px" BackColor="#CCFFCC" ReadOnly="True"></asp:TextBox>
                </td>
            <td class="style1" >
                    <asp:Label ID="Label25" runat="server" style="float: right" Text="Posted By:"></asp:Label>
                    </td>
            <td class="style2" >
                    <asp:TextBox ID="txtPostedBy" runat="server" BackColor="#CCFFCC" 
                        ReadOnly="True"></asp:TextBox>
                    </td>
            <td>
                         <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" 
                             style="float: none; top: 254px; left: 283px; text-align: center" Text="Refresh" 
                             Width="113px" Font-Bold="True" Font-Size="Medium" ForeColor="#FF9933" 
                             Height="30px" />
                   
                    </td>
            <td>
                    <asp:Button ID="btnPostReversals" runat="server" style="float: none" 
                        Text="Post Reversals" Font-Bold="True" Width="139px" Height="31px" 
                        Font-Size="Medium" onclick="btnPostReversals_Click" ForeColor="Red" />
                   
                    </td>
        </tr>
        </table>
    <hr style="color: Maroon" />
                     <asp:GridView ID="GridView" runat="server" 
             onselectedindexchanged="GridView_SelectedIndexChanged1" Width="889px" 
                   BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                   CellPadding="4" Font-Size="8pt" Height="30px" CellSpacing="2" 
        ForeColor="Black">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
             </asp:GridView>
    </asp:Content>


