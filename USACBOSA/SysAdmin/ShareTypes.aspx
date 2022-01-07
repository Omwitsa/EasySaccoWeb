<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="ShareTypes.aspx.cs" Inherits="USACBOSA.SysAdmin.ShareTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
    .style8
    {
    }
    .style9
    {
        width: 174px;
    }
    .style10
    {
        width: 122px;
    }
    .style11
    {
        height: 26px;
    }
    .style12
    {
        width: 122px;
        height: 26px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Share Types" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
        <asp:Label ID="Label6" runat="server" Text="Share Code"></asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="cboShareCode" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboShareCode_SelectedIndexChanged">
                    </asp:DropDownList>
                <asp:TextBox ID="txtShareCode" runat="server" AutoPostBack="True" 
                    ontextchanged="txtShareCode_TextChanged" Width="53px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style11">
                </td>
                <td class="style12">
    <asp:Label ID="Label7" runat="server" Text="Share Type"></asp:Label>
                </td>
                <td class="style11" colspan="4">
    <asp:TextBox ID="txtShareType" runat="server" Width="278px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
    <asp:Label ID="Label8" runat="server" Text="Minimum"></asp:Label>
                </td>
                <td class="style9">
    <asp:TextBox ID="txtminAmount" runat="server">0.00</asp:TextBox>
                </td>
                <td>
    <asp:Label ID="Label10" runat="server" Text="Check Amount" Visible="False"></asp:Label>
                </td>
                <td>
    <asp:TextBox ID="txtAbove" runat="server" Width="126px" Visible="False">0.00</asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
<asp:Label ID="Label11" runat="server" Text="Loans to share ratio"></asp:Label>
                </td>
                <td class="style9">
<asp:TextBox ID="txtLoanToShare" runat="server">0</asp:TextBox>
                </td>
                <td>
<asp:Label ID="Label12" runat="server" Text="Else...." ForeColor="#3333CC" Visible="False"></asp:Label>
                </td>
                <td>
<asp:TextBox ID="txtElse" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
                    &nbsp;</td>
                <td class="style9">
    <asp:CheckBox ID="chkIsMainShares" runat="server" Text="Is main shares" />
                </td>
                <td class="style8" colspan="2">
    <asp:CheckBox ID="chkOffset" runat="server" 
        Text="Can be used to offset a loan" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
                    &nbsp;</td>
                <td class="style9">
    <asp:CheckBox ID="chkWithdrawable" runat="server" Text="Withrawable" />
                </td>
                <td class="style8" colspan="2">
    <asp:CheckBox ID="ChkGuarantors" runat="server" 
    Text="Can be used to guarantee" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
<asp:Label ID="Label13" runat="server" Text="Associated Share Control A/C" style="float: right"></asp:Label>
                </td>
                <td class="style9">
&nbsp;<asp:TextBox ID="txtAcc" runat="server" Width="110px"></asp:TextBox>
                <asp:ImageButton ID="btnFindGlAcc" runat="server" 
                    ImageUrl="~/Images/searchbutton.PNG" onclick="btnFindGlAcc_Click" 
                    Width="16px" />
                </td>
                <td colspan="2">
<asp:TextBox ID="txtAccName" runat="server" Width="252px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                    Width="65px" Font-Bold="True" />
                &nbsp;
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Update" 
                        Font-Bold="True" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Delete" 
                        Font-Bold="True" />
                &nbsp;
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Back" 
                        Font-Bold="True" />
                </td>
                <td>
    <asp:Label ID="Label9" runat="server" Text="Shares Above..." ForeColor="#6600CC" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style10">
                    <asp:Label ID="Label14" runat="server" Text="Search By"></asp:Label>
                </td>
                <td class="style9">
                <asp:DropDownList ID="cboSearchBy" runat="server" Visible="False">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Account No</asp:ListItem>
                    <asp:ListItem>Account Name</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td colspan="3">
                <asp:TextBox ID="txtvalue" runat="server" Width="179px" Visible="False"></asp:TextBox>
            &nbsp;<asp:Button ID="btnFindSearch" runat="server" 
                    onclick="btnFindSearch_Click" Text="Find" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="5" 
                    Width="100%">
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
<asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="5" 
                    Width="100%">
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
                </td>
            </tr>
</table>
<br />
</asp:Content>
