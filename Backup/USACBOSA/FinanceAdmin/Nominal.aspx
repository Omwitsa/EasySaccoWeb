<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="Nominal.aspx.cs" Inherits="USACBOSA.FinanceAdmin.Nominal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
    }
        .style3
        {
        width: 139px;
    }
        .style4
        {
            width: 164px;
        }
        .style5
        {
        width: 111px;
    }
    .style6
    {
        width: 139px;
        height: 29px;
    }
    .style7
    {
        width: 167px;
        height: 29px;
    }
    .style8
    {
        width: 222px;
        height: 29px;
    }
    .style9
    {
        width: 111px;
        height: 29px;
    }
    .style10
    {
        width: 164px;
        height: 29px;
    }
    .style11
    {
        height: 29px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Receipt Posting - Nominal" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style6">
                    <asp:Label ID="Label12" runat="server" Text="Receipt No:" style="float: right"></asp:Label>
                </td>
                <td class="style11">
                    <asp:TextBox ID="txtReceiptNo" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="style8" align="center" bgcolor="#CCCCCC">
                    Nominal Receipt Payment</td>
                <td class="style9">
                    <asp:Label ID="Label8" runat="server" Text="Receipt Date:" style="float: right"></asp:Label>
                    <br />
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtReceiptDate" runat="server" Width="117px"></asp:TextBox>
                    </td>
                <td class="style11">
                    </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label10" runat="server" Text="Bank A/C(Source) DR:" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style7">
                    <asp:DropDownList ID="cboBankAC" runat="server" Height="22px" Width="140px" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtBankAC" runat="server" Width="210px"></asp:TextBox>
                </td>
                <td class="style9">
                    <asp:Label ID="Label20" runat="server" style="float: right" Text="Amount(DR)"></asp:Label>
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtAmountDR" runat="server" AutoPostBack="True" 
                        ontextchanged="txtAmountDR_TextChanged"></asp:TextBox>
                </td>
                <td class="style11">
                    </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label18" runat="server" Text="Other AC TO CR" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="cboaccno" runat="server" Height="22px" 
                        style="float: left" Width="141px" AutoPostBack="True" 
                        onselectedindexchanged="cboaccno_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtAccNames" runat="server" Width="208px"></asp:TextBox>
                </td>
                <td class="style9">
                    <asp:Label ID="Label21" runat="server" style="float: right" Text="Amount(CR)"></asp:Label>
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtAmountCR" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="style11">
                    </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label11" runat="server" Text="Payment Mode:" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="cboPaymentMode" runat="server" Height="22px" 
                        Width="100px" onselectedindexchanged="cboPaymentMode_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Mpesa</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>EFT</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtmode" runat="server" Width="113px"></asp:TextBox>
                </td>
                <td class="style9">
                    <asp:Label ID="Label16" runat="server" style="float: right" Text="Paid By:"></asp:Label>
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtPayee" runat="server" Width="139px"></asp:TextBox>
                </td>
                <td class="style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label19" runat="server" Text="Particulars:" style="float: right"></asp:Label>
                </td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="txtParticulars" runat="server" TextMode="MultiLine" 
                        Width="378px"></asp:TextBox>
                </td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style4">
                    <asp:Button ID="btnPost" runat="server" Text="Post Transaction" 
                        onclick="btnPost_Click" Font-Bold="True" Width="164px" Height="29px" />
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
