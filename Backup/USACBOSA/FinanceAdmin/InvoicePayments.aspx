<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="InvoicePayments.aspx.cs" Inherits="USACBOSA.FinanceAdmin.InvoicePayments" %>
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
   
    <style type="text/css">
        .style1
        {
            width: 184px;
        }
        .style2
        {
            width: 262px;
        }
        .style3
        {
            width: 62px;
        }
        .style4
        {
        }
        .style5
        {
            width: 171px;
        }
        .style6
        {
            width: 126px;
            height: 26px;
        }
        .style7
        {
            width: 171px;
            height: 26px;
        }
        .style8
        {
            height: 26px;
        }
        .style9
        {
            width: 262px;
            height: 26px;
        }
        .style10
        {
            width: 62px;
            height: 26px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Add Reference to jQuery at Google CDN --%>

<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>

<%-- Register the WebClientPrint script code --%>
        <asp:Label ID="Label5" runat="server" Text="Expenses Entry" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
    <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" Font-Size="8pt" 
                onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="5" 
                Width="100%" style="margin-top: 5px">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
                     </asp:View>
        
        <asp:View ID="View2" runat="server">
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td class="style4" colspan="2">
                    <asp:Label ID="Label44" runat="server" Text="INVOICE PAYMENTS" 
                        style="font-weight: 700; font-size: larger"></asp:Label>
            </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style3">
                    &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label 
                        ID="Label34" runat="server" Text="Invoice No:"></asp:Label>
            &nbsp;
            </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox7" runat="server" ontextchanged="TextBox7_TextChanged" 
                        AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style3">
                    <asp:Label ID="Label43" runat="server" Text="ReceiptNo:"></asp:Label>
            </td>
            <td class="style1">
                    <asp:TextBox ID="txtReceiptNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="Label37" 
                        runat="server" Text="Supplier Name:"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="166px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
            <td class="style38">
                    <asp:Label ID="Label39" runat="server" Text="Label" 
                        style="background-color: #FF9933"></asp:Label>
            </td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style3">
                    <asp:Label ID="Label41" runat="server" Text="Transaction Date:"></asp:Label>
                    </td>
            <td colspan="2">
                    <asp:TextBox ID="txtDateDeposited" runat="server" Width="126px" Font-Size="8pt" 
                        TabIndex="10" Height="21px"></asp:TextBox>
                     <asp:CalendarExtender ID="txtDateDeposited_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtDateDeposited">
                            </asp:CalendarExtender>
                    </td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label10" runat="server" Text="Branch Name:" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:DropDownList ID="branchname" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="branchname_SelectedIndexChanged" Height="22px" 
                        Width="128px">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td class="style38">
                    <asp:Label ID="Label36" runat="server" Text="Label" 
                        style="background-color: #FF6600"></asp:Label>
                </td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                <asp:Label ID="Label40" runat="server" Text="Remarks:"></asp:Label>
            </td>
            <td colspan="2">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label45" runat="server" Text="Cheque No:"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label35" runat="server" Text="Supplier A/C(DR) :" 
                        style="float: right"></asp:Label>
                    &nbsp;</td>
            <td class="style5">
                    <asp:DropDownList ID="cboBankAC" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="17px" 
                        Width="128px">
                    </asp:DropDownList>
                </td>
            <td class="style38">
                <asp:Button ID="btnFindBank" runat="server" Font-Bold="True" 
                    onclick="btnFindBank_Click" Text="F" Height="25px" Width="34px" />
            </td>
            <td class="style2">
                    <asp:TextBox ID="txtBankAC" runat="server" Width="265px"></asp:TextBox>
            </td>
            <td class="style3">
                    &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label42" runat="server" Text="Credit ACC(CR):"></asp:Label>
                    &nbsp;</td>
            <td class="style5">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="17px" Width="127px" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:Button ID="Button1" runat="server" Height="23px" Text="F" 
                    Width="34px" style="font-weight: 700" onclick="Button1_Click" />
            </td>
            <td class="style2">
                    <asp:TextBox ID="txtDRacc" runat="server" Height="23px" Width="264px"></asp:TextBox>
            </td>
            <td class="style3">
                    &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label11" runat="server" Text="Particulars:" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="128px" 
                        ></asp:TextBox>
                </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                    <asp:Label ID="Label16" runat="server" Text="Transtype:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style7">
                    <asp:DropDownList ID="cbotranstype" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="22px" 
                        Width="127px">
                        <asp:ListItem>DR</asp:ListItem>
                    </asp:DropDownList>
                </td>
            <td class="style8">
                    </td>
            <td class="style9">
                </td>
            <td class="style10">
                </td>
            <td colspan="2" class="style8">
                    </td>
            <td colspan="2" class="style8">
                    </td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label13" runat="server" Text="Invoice Amount:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style5">
                    <asp:TextBox ID="txtReceiptAmount" runat="server"
                        
                        Width="124px" Height="22px"></asp:TextBox>
                </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style31">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4" colspan="8">
    
                </td>
        </tr>
        <tr>
            <td class="style36" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" style="float: right" Text="Post Payments" 
                        onclick="btnSave_Click" Font-Bold="True" Width="146px" Height="31px" 
                        Font-Size="Medium" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td class="style31">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <hr style="color: Maroon" />
      </asp:View>
    </asp:MultiView>
                     </asp:Content>
