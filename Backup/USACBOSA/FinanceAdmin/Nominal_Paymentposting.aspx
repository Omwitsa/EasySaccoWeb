<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="Nominal_Paymentposting.aspx.cs" Inherits="USACBOSA.FinanceAdmin.Nominal_Paymentposting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
    {
        }
        .style2
        {
    }
        .style13
    {
            width: 146px;
        }
    .style15
    {
        height: 29px;
    }
    .style17
    {
        width: 164px;
        height: 29px;
    }
        .style19
        {
            width: 196px;
        }
        .style20
        {
            width: 84px;
            height: 29px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function clearWord() {
            userWord = "";
            document.forms[0].result.value = "";
        }
        var userWord = "";
        function TrapKey(obj, e) {
            thekey = String.fromCharCode(event.keyCode);
            userWord += thekey;
            for (var i = 0; i < obj.options.length; i++) {
                var txt = obj.options[i].text.toUpperCase();
                document.forms[0].result.value = userWord;
                if (txt.indexOf(userWord) == 0) {
                    obj.options[i].selected = true;
                    obj.options[i].focus();
                    break;
                }
            }
            setTimeout("clearWord()", 3000)
        }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Payment Posting - Nominal" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style19">
                    <asp:Label ID="Label10" runat="server" Text="Source A/C:" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style13">
                    <asp:DropDownList ID="cboBankAC" runat="server" Height="22px" Width="122px" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style15" colspan="3">
                    <asp:DropDownList ID="txtBankAC" runat="server" 
                        onKeyup="TrapKey(this, this.event)" AutoPostBack="True" 
                        Height="22px" Width="289px" 
                        onselectedindexchanged="txtBankAC_SelectedIndexChanged">
                    </asp:DropDownList>              
        

                </td>
                <td class="style17">
                    &nbsp;</td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:Label ID="Label18" runat="server" Text="Payee AC:" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style13">
                    <asp:DropDownList ID="cboaccno" runat="server" Height="21px" 
                        style="float: left" Width="122px" AutoPostBack="True" 
                        onselectedindexchanged="cboaccno_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style15" colspan="4">
                    <asp:TextBox ID="txtAccNames" runat="server" Width="304px" 
                        style="margin-top: 0px"></asp:TextBox>
                </td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:Label ID="Label12" runat="server" Text="VoucherNo:" style="float: right"></asp:Label>
                </td>
                <td class="style13">
                    <asp:TextBox ID="txtVoucherNo" runat="server" Width="121px" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="style20">
                    <asp:Label ID="Label8" runat="server" Text="Receipt Date:" style="float: right" 
                        Width="79px"></asp:Label>
                </td>
                <td class="style15">
                    <asp:TextBox ID="txtReceiptDate" runat="server" Width="113px" 
                        ForeColor="#6600FF" Height="22px"></asp:TextBox>
                </td>
                <td class="style15" colspan="2">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:Label ID="Label11" runat="server" Text="Payment Mode:" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style13">
                    <asp:DropDownList ID="cboPaymentMode" runat="server" Height="25px" 
                        Width="122px" 
                        onselectedindexchanged="cboPaymentMode_SelectedIndexChanged1" 
                        AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Mpesa</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>EFT</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style20">
                    <asp:Label ID="Label22" runat="server" style="float: right" Text="Cheque No:"></asp:Label>
                </td>
                <td class="style1" colspan="3">
                    <asp:TextBox ID="txtmode" runat="server" Width="113px" Height="22px"></asp:TextBox>
                </td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:Label ID="Label20" runat="server" style="float: right" Text="Amount(CR)" 
                        Visible="False"></asp:Label>
                </td>
                <td class="style13">
                    <asp:TextBox ID="txtAmountCR" runat="server" Width="122px" Visible="False">0.00</asp:TextBox>
                </td>
                <td class="style20">
                    <asp:Label ID="Label16" runat="server" style="float: right" Text="Payee:"></asp:Label>
                </td>
                <td class="style1" colspan="3">
                    <asp:TextBox ID="txtPayee" runat="server" Width="196px" Height="21px"></asp:TextBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    <asp:Label ID="Label21" runat="server" style="float: right" Text="Amount(DR)"></asp:Label>
                </td>
                <td class="style13">
                    <asp:TextBox ID="txtAmountDR" runat="server" AutoPostBack="True" 
                        ontextchanged="txtAmountDR_TextChanged" Width="122px">0.00</asp:TextBox>
                </td>
                <td class="style20">
                    <asp:Label ID="Label19" runat="server" Text="Particulars:" style="float: right" 
                        Height="16px"></asp:Label>
                </td>
                <td class="style1" colspan="3">
                    <asp:TextBox ID="txtParticulars" runat="server" TextMode="MultiLine" 
                        Width="233px" Height="41px"></asp:TextBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;</td>
                <td class="style2" colspan="5">
                    <asp:Button ID="btnPost" runat="server" Text="Post Transaction" 
                        onclick="btnPost_Click" Font-Bold="True" Height="33px" Width="149px" />
                </td>
                <td>
                    &nbsp;</td>
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
