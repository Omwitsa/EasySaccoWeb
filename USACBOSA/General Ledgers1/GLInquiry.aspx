<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="GLInquiry.aspx.cs" Inherits="USACBOSA.General_Ledgers.GLInquiry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
    }
        .style3
        {
        width: 155px;
    }
        .style4
        {
            width: 213px;
        }
        .style5
        {
            width: 167px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="General Ledger Inquiry" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
         <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style5">
                    <asp:Label ID="Label6" runat="server" Text="Account No" style="float: right"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtAccno" runat="server" Width="110px" 
                        ontextchanged="txtAccno_TextChanged" ondatabinding="txtAccno_DataBinding" 
                        AutoPostBack="True"></asp:TextBox>
                <asp:ImageButton ID="btnFindGlAcc" runat="server" 
                    ImageUrl="~/Images/searchbutton.PNG" onclick="btnFindGlAcc_Click" 
                    Width="16px" />
                </td>
                <td colspan="4">
                    <asp:TextBox ID="lblGlname" runat="server" Width="289px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label7" runat="server" Text="From Date" style="float: right"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="dtpFromdate" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
                     <asp:CalendarExtender ID="dtpFromdate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="dtpFromdate">
                            </asp:CalendarExtender>
                    <asp:ImageButton ID="imgFromDate" runat="server" 
                        ImageUrl="~/Images/calendar.png" />
                </td>
                <td class="style1">
                    <asp:Label ID="Label8" runat="server" Text="To Date"></asp:Label>
                </td>
                <td class="style4">
                    <asp:TextBox ID="dtpTodate" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
                     <asp:CalendarExtender ID="dtpTodate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="dtpTodate">
                            </asp:CalendarExtender>
                    <asp:ImageButton ID="imgToDate" runat="server" 
                        ImageUrl="~/Images/calendar.png" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label9" runat="server" Text="As by the Start of Range" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtBalByRange" runat="server"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:Label ID="Label10" runat="server" Text="Book Balance"></asp:Label>
                </td>
                <td class="style4">
                    <asp:TextBox ID="lblCurrentbalance" runat="server"></asp:TextBox>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label14" runat="server" Text="Search By" style="float: right" 
                        Visible="False"></asp:Label>
                </td>
                <td class="style3">
                <asp:DropDownList ID="cboSearchBy" runat="server" Visible="False">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Account No</asp:ListItem>
                    <asp:ListItem>Account Name</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td class="style1" colspan="2">
                <asp:TextBox ID="txtvalue" runat="server" Width="179px" Visible="False"></asp:TextBox>
            &nbsp;<asp:Button ID="btnFindSearch" runat="server" 
                    onclick="btnFindSearch_Click" Text="Find" Visible="False" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnAccountStatement" runat="server" Text="View Statement" 
                        Width="147px" onclick="btnAccountStatement_Click" />
                </td>
            </tr>
            </table>
            <hr style="color: Maroon" />
       <asp:GridView ID="GridView2" runat="server" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt"  PageSize="5" 
                    Width="100%" AutoGenerateSelectButton="True" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
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
</asp:Content>
