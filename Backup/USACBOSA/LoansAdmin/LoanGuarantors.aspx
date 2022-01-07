<%@ Page Title="" Language="C#" MasterPageFile="~/LoansAdmin/LoansAdmin.Master" AutoEventWireup="true" CodeBehind="LoanGuarantors.aspx.cs" Inherits="USACBOSA.LoansAdmin.LoanGuarantors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style11
        {
        }
        .style12
        {
        }
        .style15
        {
            width: 148px;
        }
        .style18
        {
            width: 94px;
        }
        .style19
        {
        }
        .style21
        {
            height: 26px;
        }
        .style22
        {
            width: 124px;
            height: 26px;
        }
        .style23
        {
            width: 201px;
            height: 26px;
        }
        .style24
        {
            width: 94px;
            height: 26px;
        }
        .style25
        {
            height: 26px;
        }
        .style28
        {
            width: 148px;
            height: 30px;
        }
        .style29
        {
            width: 124px;
            height: 30px;
        }
        .style30
        {
            width: 201px;
            height: 30px;
        }
        .style31
        {
            width: 94px;
            height: 30px;
        }
        .style32
        {
            height: 30px;
        }
        .style33
        {
            width: 132px;
        }
        .style34
        {
            width: 97px;
        }
        .style35
        {
            width: 124px;
        }
        .style36
        {
        }
        .style37
        {
            width: 201px;
        }
        .style38
        {
            width: 120px;
        }
        .style39
        {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Loan Guarantors" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table frame="box" style="width:100%;">
        <tr>
            <td class="style33">
        <asp:Label ID="Label6" runat="server" Text="Loan No.:" style="float: right"></asp:Label>
                <br />
            </td>
            <td class="style38">
    <asp:TextBox ID="txtLoanNo" runat="server" Width="108px" AutoPostBack="True" 
                    ondatabinding="txtLoanNo_DataBinding" ontextchanged="txtLoanNo_TextChanged"></asp:TextBox>
            </td>
            <td class="style34">
    <asp:Label ID="Label7" runat="server" Text="Member No.:" style="float: right"></asp:Label>
            </td>
            <td class="style39">
    <asp:TextBox ID="txtLoaneeMemberNo" runat="server" ReadOnly="True" Width="109px" 
                    BackColor="#CCFFFF"></asp:TextBox>
            </td>
            <td colspan="4">
    <asp:TextBox ID="txtLoanApplicant" runat="server" Width="307px" ReadOnly="True" 
                    BackColor="#CCFFFF"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style33">
    <asp:Label ID="Label9" runat="server" Text="Repay Period:" style="float: right"></asp:Label>
                <br />
            </td>
            <td class="style38">
    <asp:TextBox ID="txtRepayPeriod" runat="server" Width="106px" BackColor="#CCFFFF"></asp:TextBox>
            </td>
            <td class="style34">
    <asp:Label ID="Label10" runat="server" Text="Amt. Applied:" style="float: right"></asp:Label>
                <br />
            </td>
            <td class="style39">
    <asp:TextBox ID="txtAmtApproved" runat="server" Width="107px" BackColor="#CCFFFF"></asp:TextBox>
            </td>
            <td>
    <asp:Label ID="Label11" runat="server" Text="Amt Guaranteed:" style="float: right"></asp:Label>
                <br />
            </td>
            <td class="style11">
                <asp:TextBox 
                    ID="txtAmtGuaranteed" runat="server" Width="98px" BackColor="#CCFFFF"></asp:TextBox>
            </td>
            <td>
                <br />
            &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style33">
    <asp:Label ID="Label12" runat="server" Text="Application Date:" style="float: right"></asp:Label>
            </td>
            <td class="style12" colspan="2">
    <asp:TextBox ID="txtTransDate" runat="server" Width="122px" ReadOnly="True" 
                    BackColor="#CCFFFF"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/Images/calendar.png" Width="16px" />
            </td>
            <td class="style39">
                <asp:Button ID="btnAddGuar" runat="server" onclick="btnAddGuar_Click" 
                    Text="Add Guarantor" />
            </td>
            <td class="style11" colspan="2">
                <asp:Button ID="btnCollaterals" runat="server" onclick="btnCollaterals_Click" 
                    Text="Add Collaterals" />
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:100%;" frame="box">
        <tr>
            <td class="style31">
                <asp:Label ID="Label13" runat="server" Text="Member No:"></asp:Label>
                </td>
            <td class="style28">
                <asp:TextBox ID="txtGuarNo" runat="server" Width="100px" 
                    ontextchanged="txtGuarNo_TextChanged"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="F" 
                    Visible="False" />
            </td>
            <td class="style29">
                <asp:Label ID="Label14" runat="server" Text="Guarantor Names:" 
                    style="float: right"></asp:Label>
            </td>
            <td class="style30">
                <asp:TextBox ID="txtGuarName" runat="server" Width="204px"></asp:TextBox>
            </td>
            <td class="style31">
                <asp:Label ID="Label17" runat="server" Text="Current Shares:"></asp:Label>
                <br />
            </td>
            <td class="style32">
                <asp:TextBox ID="txtShares1" runat="server"></asp:TextBox>
            </td>
            <td class="style32">
                <br />
            </td>
            <td class="style32">
                </td>
        </tr>
        <tr>
            <td class="style18">
                &nbsp;</td>
            <td class="style15">
                &nbsp;</td>
            <td class="style35">
                <asp:Label ID="Label15" runat="server" Text="Amt. Available:" 
                    style="float: right"></asp:Label>
            </td>
            <td class="style37">
                <asp:TextBox ID="txtAvAmt" runat="server" style="margin-left: 0px" 
                    Height="22px"></asp:TextBox>
            </td>
            <td class="style18">
                <asp:Label ID="Label16" runat="server" Text="Guaranteed Amt"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAmtAllocated" runat="server" Width="101px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label22" runat="server" Text="Collateral Code:" 
                    style="float: right"></asp:Label>
            </td>
            <td class="style21" colspan="3">
                <asp:DropDownList ID="cboCollateralCode" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="cboCollateralCode_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:TextBox ID="txtCollateralDescription" runat="server" Width="198px"></asp:TextBox>
            </td>
            <td class="style24">
                <asp:Label ID="Label21" runat="server" Text="Document No:" style="float: right"></asp:Label>
            </td>
            <td class="style25">
                <asp:TextBox ID="txtDocumentNo" runat="server"></asp:TextBox>
            </td>
            <td class="style25">
                </td>
            <td class="style25">
                </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label24" runat="server" Text="No of shares"></asp:Label>
            </td>
            <td class="style21" colspan="3">
                <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="True" 
                    ontextchanged="TextBox3_TextChanged"></asp:TextBox>
            </td>
            <td class="style24">
                &nbsp;</td>
            <td class="style25">
                &nbsp;</td>
            <td class="style25">
                &nbsp;</td>
            <td class="style25">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label18" runat="server" Text="Market Value:"></asp:Label>
&nbsp;</td>
            <td class="style21">
                <asp:TextBox ID="txtMarketValue" runat="server" AutoPostBack="True" 
                    ontextchanged="txtMarketValue_TextChanged" Width="128px"></asp:TextBox>
            </td>
            <td class="style22">
                <asp:Label ID="Label19" runat="server" Text="Considered Value:" 
                    style="float: right"></asp:Label>
            </td>
            <td class="style23">
                <asp:TextBox ID="txtConsideredValue" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style24">
                &nbsp;</td>
            <td class="style25">
                &nbsp;</td>
            <td class="style25">
            </td>
            <td class="style25">
            </td>
        </tr>
        <tr>
            <td class="style18">
                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="85px"></asp:TextBox>
            </td>
            <td class="style15">
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                    Height="33px" style="float: none; font-weight: 700" Width="86px" />
            </td>
            <td class="style35">
                <asp:Label ID="Label23" runat="server" style="color: #000000; float: right;" Text="Search by" 
                    Visible="False"></asp:Label>
                </td>
            <td class="style36" colspan="3">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="19px" 
                    Visible="False" Width="114px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>IDNo</asp:ListItem>
                    <asp:ListItem>Names</asp:ListItem>
                </asp:DropDownList>
            &nbsp;
                <asp:TextBox ID="TextBox2" runat="server" Visible="False" Width="203px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Search" 
                    Visible="False" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
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
     <div class="style5" style="width: 100%; height: 118px; overflow: scroll">
                    <asp:GridView ID="GridView2" runat="server" Width="893px" 
                        AutoGenerateSelectButton="True" Font-Size="8pt" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
                    </asp:GridView>
                    </div>
    </asp:Content>
