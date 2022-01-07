<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="Receive_Invoices.aspx.cs" Inherits="USACBOSA.FinanceAdmin.Receive_Invoices" %>
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

        mywindow.document.close(); // necessary for IE >= 10

        mywindow.focus(); // necessary for IE >= 10

        mywindow.print();

        mywindow.close();

        return true;

    }

</script>
   
   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Add Reference to jQuery at Google CDN --%>

<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>

<%-- Register the WebClientPrint script code --%>
        <asp:Label ID="Label5" runat="server" Text="Expenses Entry" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td class="style4" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label43" runat="server" Text="RECEIVE PAYMENTS" 
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
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label37" 
                        runat="server" Text="Supplier Code:"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox9" runat="server" AutoPostBack="True" 
                        ontextchanged="TextBox9_TextChanged" style="margin-left: 0px"></asp:TextBox>
                    </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                    <asp:TextBox ID="TextBox8" runat="server" Width="265px"></asp:TextBox>
            </td>
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
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label 
                        ID="Label44" runat="server" Text="Invoice No:"></asp:Label>
            </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style3">
                    <asp:Label ID="Label38" runat="server" Text="Due Date:" 
                        style="float: right"></asp:Label>
                    </td>
            <td colspan="2">
                         <asp:TextBox ID="txtContribDate" runat="server" Width="124px" Font-Size="8pt" 
                             Height="20px"></asp:TextBox>
                     <asp:CalendarExtender ID="txtContribDate_CalendarExtender" Enabled="true" 
                        PopupButtonID="ImageButton2" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtContribDate">
                            </asp:CalendarExtender>
                    </td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    <asp:Label ID="Label10" runat="server" Text="Branch Name:" 
                        style="float: right"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td class="style5">
                    <asp:DropDownList ID="branchname" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="branchname_SelectedIndexChanged" Height="22px" 
                        Width="128px">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td class="style38">
                    <asp:Label ID="Label36" runat="server" Text="Label"></asp:Label>
                </td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label40" runat="server" Text="Remarks:"></asp:Label>
            </td>
            <td colspan="2">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    <asp:Label ID="Label35" runat="server" Text="Supplier A/C(CR) :" 
                        style="float: right"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
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
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label42" runat="server" Text="Debt ACC(DR)"></asp:Label>
                    :</td>
            <td class="style5">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="17px" Width="127px" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:Button ID="Button1" runat="server" Height="24px" Text="F" 
                    Width="37px" onclick="Button1_Click" />
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
            <td class="style6">
                    &nbsp;</td>
            <td class="style4">
                    <asp:Label ID="Label11" runat="server" Text="Particulars:" 
                        style="float: right"></asp:Label>
                    </td>
            <td class="style5">
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="128px"></asp:TextBox>
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
                    &nbsp;</td>
            <td class="style4">
                    <asp:Label ID="Label16" runat="server" Text="Transtype:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style5">
                    <asp:DropDownList ID="cbotranstype" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="22px" 
                        Width="127px">
                        <asp:ListItem>CR</asp:ListItem>
                    </asp:DropDownList>
                </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
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
            <td class="style6">
                    &nbsp;</td>
            <td class="style36" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" style="float: right" Text="Receive Invoices" 
                        onclick="btnSave_Click" Font-Bold="True" Width="179px" Height="31px" 
                        Font-Size="Medium" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="style38">
                    &nbsp;</td>
            <td class="style2">
                <br />
                <asp:Button ID="Button2" runat="server" Text="MODIFY" onclick="Button2_Click" 
                    style="font-weight: 700" Height="32px" Width="142px" />
            </td>
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
        <tr>
            <td class="style6" colspan="7">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                        Font-Size="8pt" onselectedindexchanged="GridView2_SelectedIndexChanged" 
                        Width="893px">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                    </asp:GridView>
            </td>
            <td class="style31">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td class="style36" colspan="2">
                    &nbsp;</td>
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
                     </asp:Content>
