<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="MPA_Paymentposting.aspx.cs" Inherits="USACBOSA.FinanceAdmin.MPA_Paymentposting" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        mywindow.document.close(); // necessary for IE >= 10

        mywindow.focus(); // necessary for IE >= 10

        mywindow.print();

        mywindow.close();

        return true;

    }

</script>
<script language="javascript" type="text/javascript">

    function EnterTab(e) {
        var intKey = window.Event ? e.which : e.KeyCode;


        if (intKey == 13)
            e.returnValue = false;
    }
</script>

<script type="text/javascript">.
var wcppGetPrintersDelay_ms = 50; //5 sec

function wcpGetPrintersOnSuccess(){

<%-- Display client installed printers --%>

if(arguments[0].length > 0){

var p=arguments[0].split("|");

var options = '';

for (var i = 0; i < p.length; i++) {

options += '<option>' + p[i] + '</option>';

}

$('#installedPrinters').css('visibility','visible');

$('#installedPrinterName').html(options);

$('#installedPrinterName').focus();

$('#loadPrinters').hide();                                                       

}else{

alert("No printers are installed in your system.");

}

}

function wcpGetPrintersOnFailure() {

<%-- Do something if printers cannot be got from the client --%>

alert("No printers are installed in your system.");

}

</script>
<!--**********************************************************************************************//-->
    <style type="text/css">
        .style22
        {
            width: 115px;
        }
        .style23
        {
            width: 156px;
        }
        .style24
        {
    }
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
    <asp:Label ID="Label5" runat="server" Text="Payment Posting - MPA" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
         <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
            
        <asp:GridView ID="GridView1" runat="server"  Width="889px" 
                   BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                   CellPadding="4" Font-Size="8pt" Height="30px" CellSpacing="2" 
        ForeColor="Black" onselectedindexchanged="GridView1_SelectedIndexChanged">
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
             </asp:View>
            
             <asp:View ID="View2" runat="server">
      <table style="width:100%;">
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label6" runat="server" style="float: right" Text="Member No:"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:TextBox ID="txtMemberNo" runat="server" 
                             ontextchanged="txtMemberNo_TextChanged1" Width="110px" Height="22px"></asp:TextBox>
                         <asp:ImageButton ID="imgSearchMember" runat="server" 
                             ImageUrl="~/Images/searchbutton.PNG" onclick="imgSearch_Click" 
                             Visible="False" />
                     </td>
                     <td colspan="3">
                         <asp:TextBox ID="txtNames" runat="server" Width="290px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label10" runat="server" style="float: right" 
                             Text="Bank AC TO DR"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:DropDownList ID="cboBanks" runat="server" AutoPostBack="True" 
                             Height="22px" onselectedindexchanged="cboBanks_SelectedIndexChanged" 
                             Width="110px">
                         </asp:DropDownList>
                     </td>
                     <td colspan="3">
                         <asp:TextBox ID="lblbankname" runat="server" style="margin-left: 0px" 
                             Width="292px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label12" runat="server" style="float: right" Text="Voucher No:"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:TextBox ID="txtVoucherNo" runat="server" Font-Bold="True" 
                             ForeColor="#0066FF" Width="110px" Height="22px" ReadOnly="True"></asp:TextBox>
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label7" runat="server" style="float: right" Text="Collected By:"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtNarration" runat="server" Width="174px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label11" runat="server" style="float: right" 
                             Text="Payment Mode:"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:DropDownList ID="cboPaymentMode" runat="server" AutoPostBack="True" 
                             Height="22px" onselectedindexchanged="cboPaymentMode_SelectedIndexChanged" 
                             Width="110px">
                             <asp:ListItem></asp:ListItem>
                             <asp:ListItem>Cash</asp:ListItem>
                             <asp:ListItem>Cheque</asp:ListItem>
                             <asp:ListItem>Mpesa</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label20" runat="server" style="float: right" 
                             Text="Collector ID:"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="CollectorID" runat="server" Width="110px" Height="22px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label15" runat="server" style="float: right" Text="Cheque No:"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:TextBox ID="txtmode" runat="server" Width="107px"></asp:TextBox>
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label14" runat="server" style="float: right" 
                             Text="Payment Date:" Width="85px"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtDateDeposited" runat="server" Height="22px" 
                             style="margin-left: 0px" Width="107px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         <asp:Label ID="Label21" runat="server" style="float: right" 
                             Text="Payment Purpose:"></asp:Label>
                     </td>
                     <td class="style23">
                         <asp:CheckBox ID="chkLoans" runat="server" Text="Loans" />
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label16" runat="server" style="float: right" 
                             Text="Distributed Amt.:"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtDistributedAmount" runat="server" ForeColor="#0033CC" 
                              Width="107px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         &nbsp;</td>
                     <td class="style23">
                         <asp:CheckBox ID="chkShares" runat="server" Text="Shares" />
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label13" runat="server" style="float: right" 
                             Text="Payment Amount:"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtAmountPaid" runat="server" AutoPostBack="True" 
                             ontextchanged="txtReceiptAmount_TextChanged" Width="108px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         &nbsp;</td>
                     <td class="style23">
                         <asp:CheckBox ID="chkIspartpayment" runat="server" style="float: none" 
                             Text="Is part payment?" />
                     </td>
                     <td class="style22">
                         <asp:Label ID="Label17" runat="server" style="float: right" 
                             Text="Balance Amount:" Width="100px"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtBalance" runat="server" AutoPostBack="True" 
                             ForeColor="#0066FF" Width="108px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24">
                         &nbsp;</td>
                     <td class="style23">
                         <asp:Button ID="btnSave" runat="server" Font-Bold="True" 
                             onclick="btnSave_Click" style="float: none; margin-bottom: 8px;" 
                             Text="Post Payment" Width="123px" />
                     </td>
                     <td class="style22">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style24" colspan="6">
                         <asp:Label ID="Label22" runat="server" BackColor="#FF9900" BorderStyle="None" 
                             style="font-weight: 700" Text="PAYMENT RECEIPT" Width="159px"></asp:Label>
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
             </asp:View>
             </asp:MultiView>
</asp:Content>
