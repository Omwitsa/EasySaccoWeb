<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="MPA2.aspx.cs" Inherits="USACBOSA.FinanceAdmin.MPA2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
<!--**********************************************************************************************//-->
<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="..\Scripts\jquery-1.3.1.min.js" > </script>

<script type="text/javascript">


    function PrintElem(elem) {

        Popup($(elem).html());

    }

    function Popup(data) {

        var mywindow = window.open('', 'my div', 'height=300,width=200');

        mywindow.document.write('<html><head><title>my div</title>');

        /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');

        mywindow.document.write('</head><body>');

        mywindow.document.write(data);

        mywindow.document.write('</body></html>');

        mywindow.document.close(); //necessary for IE >= 10

        mywindow.focus(); // necessary for IE >= 10

        mywindow.print();

        mywindow.close();

        return true;

    }

</script>
    <style type="text/css">
        .style30
        {
            width: 24px;
        }
        .style31
        {
            width: 106px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%-- Add Reference to jQuery at Google CDN --%>

<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>

<%-- Register the WebClientPrint script code --%>
    <asp:Label ID="Label5" runat="server" Text="Receipt Posting - MPA" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td>
                    <asp:Label ID="Label6" runat="server" Text="Member No:" style="float: right" 
                        Width="77px"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtMemberNo" runat="server" Height="22px" Width="110px" 
                        ondatabinding="txtMemberNo_DataBinding" 
                        ontextchanged="txtMemberNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </td>
            <td class="style30">
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="F" 
                        Font-Bold="True" />
                </td>
            <td colspan="2">
                    <asp:TextBox ID="txtNames" runat="server" Width="265px"></asp:TextBox>
                </td>
            <td>
                    <asp:Label ID="Label24" runat="server" style="float: right" Text="Receipt No:"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtReceiptNo" runat="server" Width="112px" ReadOnly="True"></asp:TextBox>
                   
                </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label10" runat="server" Text="Bank A/C :" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:DropDownList ID="cboBankAC" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="22px" 
                        Width="110px">
                    </asp:DropDownList>
                </td>
            <td class="style30">
                <asp:Button ID="btnFindBank" runat="server" Font-Bold="True" 
                    onclick="btnFindBank_Click" Text="F" />
            </td>
            <td colspan="2">
                    <asp:TextBox ID="txtBankAC" runat="server" Width="265px"></asp:TextBox>
                </td>
            <td>
                    <asp:Label ID="Label23" runat="server" style="float: right" Text="Serial No:"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtSerialNo" runat="server" Width="112px"></asp:TextBox>
                   
                </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label11" runat="server" Text="Payment Mode:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:DropDownList ID="cboPaymentMode" runat="server" Height="22px" 
                        Width="110px" AutoPostBack="True" 
                        onselectedindexchanged="cboPaymentMode_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>Mpesa</asp:ListItem>
                        <asp:ListItem>Bank Deposit</asp:ListItem>
                        <asp:ListItem>Dividends</asp:ListItem>
                        <asp:ListItem>Mobi Cash</asp:ListItem>
                    </asp:DropDownList>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="2">
                    <asp:CheckBox ID="chkAcruedInterest" runat="server" 
                        Text="Enable Acrued Interest" style="text-align: center" 
                        AutoPostBack="True" oncheckedchanged="chkAcruedInterest_CheckedChanged" />
                    
                </td>
            <td>
                    <asp:Label ID="Label14" runat="server" Text="Date Deposited:" 
                        style="float: right; " Width="95px" ></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtDateDeposited" runat="server" Width="105px" Font-Size="8pt" 
                        TabIndex="10" Height="23px"></asp:TextBox>
                     <asp:CalendarExtender ID="txtDateDeposited_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtDateDeposited">
                            </asp:CalendarExtender>
                   
                </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label15" runat="server" Text="Reference No:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtChequeNo" runat="server" AutoPostBack="True" Width="110px"></asp:TextBox>
                    </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="2">
                    <asp:CheckBox ID="Chckbxprintrcpt" runat="server" Checked="True" 
                        style="color: #000000" Text=" Enable Receipt Print" />
                </td>
            <td>
                    <asp:Label ID="Label21" runat="server" Text="Contribution Date:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                         <asp:TextBox ID="txtContribDate" runat="server" Width="107px" Font-Size="8pt" 
                             Height="22px"></asp:TextBox>
                     <asp:CalendarExtender ID="txtContribDate_CalendarExtender2" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtContribDate">
                            </asp:CalendarExtender>
                   
                </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label16" runat="server" Text="Distributed Amt.:" 
                        style="float: right"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtDistributedAmount" runat="server" Width="110px" ForeColor="#0033CC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="2">
                    <asp:Button ID="btnRefresh" runat="server" Enabled="False" Font-Bold="True" 
                        Height="23px" onclick="btnRefresh_Click" Text="Refresh Expectation" 
                        Width="214px" />
                </td>
            <td colspan="2">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Batch Transactions" 
                        oncheckedchanged="CheckBox1_CheckedChanged" style="float: none" 
                        Visible="False" />
                </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label13" runat="server" Text="Receipt Amount:" 
                        style="float: right"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtReceiptAmount" runat="server"
                        AutoPostBack="True" ontextchanged="txtReceiptAmount_TextChanged" 
                        Width="110px"></asp:TextBox>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td>
                    <asp:Label ID="Label17" runat="server" style="float: left" Text="Balance:"></asp:Label>
                </td>
            <td class="style31">
                    <asp:TextBox ID="txtBalance" runat="server" ForeColor="#0066FF" ReadOnly="True" 
                        Width="130px"></asp:TextBox>
                   
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label19" runat="server" Text="Other Accounts:" 
                        style="float: right"></asp:Label>
                </td>
            <td>
                    <asp:DropDownList ID="cboGlAccountNo" runat="server" Height="22px" 
                        style="float: left" Width="110px" AutoPostBack="True" 
                        onselectedindexchanged="cboGlAccountNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            <td class="style30">
                    <asp:Label ID="lblShareCode" runat="server" Text="Label"></asp:Label>
                </td>
            <td colspan="2">
                    <asp:TextBox ID="txtGlAccountName" runat="server" Width="261px" 
                        AutoPostBack="True" BackColor="#CCFFCC" ReadOnly="True"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="Label26" runat="server" style="float: right" 
                    Text="Payment Amount:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAmoutPayable" runat="server" Width="112px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
     
                    <asp:Label ID="Label25" runat="server" Text="Edited Distributed Amount:" 
                        style="float: right"></asp:Label>
     
                </td>
            <td>
     
                    <asp:TextBox ID="txtEditedDistributedAmount" runat="server" AutoPostBack="True" 
                        ForeColor="#FF3300" ontextchanged="txtEditedDistributedAmount_TextChanged" 
                        Width="110px"></asp:TextBox>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="4">
                    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
                        Text="Add Payment" Width="118px" />
                &nbsp;
                    <asp:Button ID="btnRemove" runat="server" onclick="btnRemove_Click" 
                        Text="Remove Payment" Width="118px" />
                </td>
        </tr>
        <tr>
            <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" style="float: right" Text="Post Payment" 
                        onclick="btnSave_Click" Font-Bold="True" Width="146px" Height="31px" 
                        Font-Size="Medium" />
                </td>
            <td class="style30">
     
                    <asp:TextBox ID="txtMembersAccountId" runat="server" ReadOnly="True" 
                        Visible="False" Width="16px" style="float: right"></asp:TextBox>
     
                </td>
            <td>
                    <asp:Label ID="Label22" runat="server" style="font-weight: 700; float: right;" 
                        Text="Search by" Visible="False" ForeColor="#FF5050"></asp:Label>
                </td>
            <td colspan="3">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="21px" 
                        Visible="False" Width="108px" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>ID No</asp:ListItem>
                        <asp:ListItem>Names</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;
                    <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="229px"></asp:TextBox>
                    &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" 
                        Visible="False" />
                </td>
        </tr>
    </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView2" runat="server" Width="893px" 
                        AutoGenerateSelectButton="True" Font-Size="8pt" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
                    </asp:GridView>
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
