<%@ Page Title="" Language="C#" MasterPageFile="~/LoansAdmin/LoansAdmin.Master" AutoEventWireup="true" CodeBehind="LoanSchedule.aspx.cs" Inherits="USACBOSA.LoansAdmin.LoanSchedule" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
    .style1
    {
        height: 26px;
    }
        .style2
        {
        }
        .style3
        {
            height: 26px;
            width: 206px;
        }
        .style4
        {
        }
        .style5
        {
            height: 26px;
            width: 123px;
        }
        .style6
        {
            height: 24px;
        }
        .style7
        {
            width: 123px;
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label8" runat="server" Text="Loan Schedule" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table style="width:100%;">
    <tr>
        <td class="style2">
            <asp:Label ID="Label1" runat="server" style="float: right" Text="Loan No."></asp:Label>
        </td>
        <td class="style4">
            <asp:TextBox ID="txtLoanNo1" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label2" runat="server" style="float: right" Text="Member No."></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMemberNo" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="Label9" runat="server" style="float: right" Text="Amount"></asp:Label>
        </td>
        <td class="style4">
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label10" runat="server" style="float: right" 
                Text="Interest Rate"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtIntRate" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="Label5" runat="server" style="float: right" 
                Text="Repayment Period (M)"></asp:Label>
        </td>
        <td class="style4">
            <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label4" runat="server" style="float: right" Text="Options"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="optAuto" runat="server" Text="Automatic" 
                AutoPostBack="True" oncheckedchanged="optAuto_CheckedChanged" 
                Checked="True" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label7" runat="server" style="float: right" 
                Text="Starting Period (Yr/Month)"></asp:Label>
        </td>
        <td class="style5">
            <asp:TextBox ID="txtStartDate" runat="server" Width="114px" Font-Size="8pt" 
                TabIndex="10" Height="20px"></asp:TextBox>
                     <asp:CalendarExtender ID="txtStartDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtStartDate">
                            </asp:CalendarExtender>
            <asp:ImageButton ID="imgStartDate" runat="server" 
                ImageUrl="~/Images/calendar.png" />
        </td>
        <td class="style1">
            </td>
        <td class="style1">
            <asp:RadioButton ID="optConstants" runat="server" Text="Constants" 
                AutoPostBack="True" oncheckedchanged="optConstants_CheckedChanged" 
                Visible="False" />
        </td>
        <td class="style1">
            </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="Label3" runat="server" style="float: right" 
                Text="Repayment Method"></asp:Label>
        </td>
        <td class="style4">
            <asp:RadioButton ID="optStraight" runat="server" Text="Straight Line" 
                AutoPostBack="True" Checked="True" 
                oncheckedchanged="optStraight_CheckedChanged" />
        </td>
        <td>
            <asp:Label ID="Label6" runat="server" style="float: right" 
                Text="Payment Time (Amortized)" Visible="False"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="optEnd" runat="server" Text="End of Period" 
                oncheckedchanged="optEnd_CheckedChanged" AutoPostBack="True" 
                Visible="False" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style6">
        </td>
        <td class="style7">
            <asp:RadioButton ID="optReducing" runat="server" Text="Reducing Balance" 
                AutoPostBack="True" oncheckedchanged="optReducing_CheckedChanged" />
        </td>
        <td class="style6">
        </td>
        <td class="style6">
            <asp:RadioButton ID="optStart" runat="server" Text="Start of Period" 
                oncheckedchanged="optStart_CheckedChanged" AutoPostBack="True" 
                Visible="False" />
        </td>
        <td class="style6">
            </td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td class="style4">
            <asp:RadioButton ID="optAmortized" runat="server" Text="Amortized" 
                AutoPostBack="True" oncheckedchanged="optAmortized_CheckedChanged" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td class="style4" colspan="3">
            <asp:Button ID="cmdGenerate" runat="server" onclick="cmdGenerate_Click" 
                style="float: left" Text="Generate" />
            <asp:Button ID="cmdView" runat="server" onclick="cmdView_Click" 
                Text="View Schedule" />
            <asp:Button ID="cmdprint" runat="server" onclick="cmdprint_Click" 
                Text="Print Schedule" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td class="style4">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" class="style2">
            <asp:GridView ID="GridView1" runat="server" Width="862px" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>
