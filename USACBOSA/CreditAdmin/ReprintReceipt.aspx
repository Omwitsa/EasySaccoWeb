<%@ Page Title="" Language="C#" MasterPageFile="~/CreditAdmin/CreditAdmin.Master" AutoEventWireup="true" CodeBehind="ReprintReceipt.aspx.cs" Inherits="USACBOSA.CreditAdmin.ReprintReceipt" %>
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
        .style30
        {
            width: 24px;
        }
        .style31
        {
            width: 106px;
        }
        .style32
        {
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
            <td class="style32">
                    <asp:Label ID="Label6" runat="server" Text="Member No:" style="float: right" 
                        Width="77px"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtMemberNo" runat="server" Height="22px" Width="110px" 
                        ontextchanged="txtMemberNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </td>
            <td class="style30">
                    <asp:Button ID="Button2" runat="server"  Text="F" 
                        Font-Bold="True" />
                </td>
            <td colspan="2">
                    <asp:TextBox ID="txtNames" runat="server" Width="265px"></asp:TextBox>
                </td>
            <td>
                    <asp:Label ID="Label24" runat="server" style="float: right" Text="Audit ID:"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtauditid" runat="server" Width="118px" 
                        Height="22px"></asp:TextBox>
                   
                </td>
        </tr>
        <tr>
            <td class="style32">
                    <asp:Label ID="Label11" runat="server" Text="Payment Mode:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="cboPaymentMode" runat="server" Width="109px"></asp:TextBox>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
            <td>
                    <asp:Label ID="Label14" runat="server" Text="Date Deposited:" 
                        style="float: right; " Width="95px" ></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtDateDeposited" runat="server" Width="118px" Font-Size="8pt" 
                        TabIndex="10" Height="20px"></asp:TextBox>
                   
                </td>
        </tr>
        <tr>
            <td class="style32">
                    <asp:Label ID="Label15" runat="server" Text="Receipt No:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtReceiptNo" runat="server" Width="110px"></asp:TextBox>
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
                         <asp:TextBox ID="txtContribDate" runat="server" Width="118px" Font-Size="8pt" 
                             Height="21px" ></asp:TextBox>
                   
                </td>
        </tr>
        <tr>
            <td class="style32">
                    <asp:Label ID="Label25" runat="server" Text="Trans Type:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="110px"></asp:TextBox>
            </td>
            <td class="style30">
                &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td>
                         &nbsp;</td>
        </tr>
        <tr>
            <td class="style32">
                    <asp:Label ID="Label13" runat="server" Text="Amount:" 
                        style="float: right"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtReceiptAmount" runat="server"
                       
                        Width="110px"></asp:TextBox>
                </td>
            <td class="style30">
                &nbsp;</td>
            <td class="style31">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style32">
                    &nbsp;</td>
            <td>
                    <asp:Button ID="btnSave" runat="server" style="float: right" Text="Reprint Receipt" 
                        onclick="btnSave_Click" Font-Bold="True" Width="146px" Height="31px" 
                        Font-Size="Medium" />
                </td>
            <td class="style30">
                &nbsp;</td>
            <td class="style31">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style32" colspan="6">
    <asp:GridView ID="GridView2" runat="server" Width="893px" 
                        AutoGenerateSelectButton="True" Font-Size="8pt" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
                    </asp:GridView>
                </td>
        </tr>
        </table>
    <hr style="color: Maroon" />
    </asp:Content>
