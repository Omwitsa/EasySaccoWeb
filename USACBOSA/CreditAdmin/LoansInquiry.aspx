<%@ Page Title="" Language="C#" MasterPageFile="~/CreditAdmin/CreditAdmin.Master" AutoEventWireup="true" CodeBehind="LoansInquiry.aspx.cs" Inherits="USACBOSA.CreditAdmin.LoansInquiry" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {}
        .style2
        {
            width: 248px;
        }
        .style3
        {
            width: 263px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Member Loans Inquiry" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
<hr style="color: Maroon" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label6" runat="server" Text="Member No."></asp:Label>
                        &nbsp;&nbsp;
                        <br />
                        <asp:TextBox ID="txtMemberNo" runat="server" AutoPostBack="True" 
                            ontextchanged="txtMemberNo_TextChanged"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:Label ID="Label7" runat="server" Text="Names"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtNames" runat="server" Width="247px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label8" runat="server" Text="Station"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtStation" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Outstanding Loans"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOLoans" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label10" runat="server" Text="Total Shares"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtTShares" runat="server"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:Label ID="Label11" runat="server" Text="Outstanding Loan Balance"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOLoanBal" runat="server"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:CheckBox ID="chkIndividual" runat="server" Text="Individual" />
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                            Text="View Statement" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Init Amount:"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtInitAmount" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Balance(P)"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtBalp" runat="server" AutoPostBack="True" Width="142px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Balance(P+I)"></asp:Label>
                        <asp:TextBox ID="txtBalppi" runat="server" AutoPostBack="True" Width="114px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Loan No:"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtLoanNo" runat="server" Width="140px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="Accrued B/F:"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Paid B/F:"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                            style="font-weight: 700" Text="REFRESH LOAN" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" 
                EnableParameterPrompt="False" ToolPanelView="None" />
        </asp:View>
    </asp:MultiView>
    <hr style="color: Maroon" />
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
</asp:Content>
