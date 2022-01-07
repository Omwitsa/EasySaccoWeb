<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="SharesInquiry.aspx.cs" Inherits="USACBOSA.SysAdmin.SharesInquiry" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <%-- <script type="text/javascript">
        var count = 0
    fuction oJump(){
    var lb=document.getElementById("Label1");
    lb.innerText=count;
    count++
    if(lb.innerText==10){
    clearTimeout(obj)}
    }
    fuction run(){
    var obj=setInterval("oJump()",1000);
    }
    </script>--%>
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
            width: 138px;
        }
        .style4
        {
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Member Shares Inquiry" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
        <tr>
            <td class="style2">
                <asp:Label ID="Label6" runat="server" Text="Member No." style="float: right"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtMemberNo" runat="server" 
                    ontextchanged="txtMemberNo_TextChanged" AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label7" runat="server" Text="Member Names" style="float: right"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtNames" runat="server" Width="212px"></asp:TextBox>
                 </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" Text="Station" style="float: right"></asp:Label>
                </td>
            <td class="style3">
                <asp:TextBox ID="txtStation" runat="server"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label10" runat="server" Text="Total Shares" style="float: right"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtTShares" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label9" runat="server" Text="Outstanding Loans" 
                    style="float: right"></asp:Label>
                </td>
            <td class="style3">
                <asp:TextBox ID="txtOLoans" runat="server"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label11" runat="server" Text="Loan Balance" style="float: right"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOLoanBal" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label12" runat="server" Text="Shares Code" style="float: right"></asp:Label>
            </td>
            <td class="style3">
                <asp:DropDownList ID="cboShareCode" runat="server" 
                    onselectedindexchanged="cboShareCode_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style4" colspan="2">
                <asp:TextBox ID="txtsharescode" runat="server" AutoPostBack="True" 
                    Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label13" runat="server" Text="Total Shares(Per Scheme)" 
                    style="float: right"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtTotShares" runat="server" Width="123px"></asp:TextBox>
            </td>
            <td>
            &nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Individual" />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="View Statement" />
            &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="REFRESH CONTRIBUTIONS" 
                    Width="189px" onclick="Button2_Click" style="font-weight: 700" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <hr style="color: #000000" />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="5" 
                    Width="100%" Height="156px">
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
