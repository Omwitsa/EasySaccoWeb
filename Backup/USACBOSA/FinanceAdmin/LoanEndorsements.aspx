<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="LoanEndorsements.aspx.cs" Inherits="USACBOSA.FinanceAdmin.LoanEndorsements" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {            margin-left: 120px;
        }
        .style9
        {
            height: 25px;
        }
        .style11
        {
        }
        .style13
        {
            width: 72px;
        }
        .style15
        {
            width: 72px;
            height: 25px;
        }
        .style18
        {
            height: 24px;
            }
        .style19
        {
            height: 24px;
            width: 155px;
        }
        .style20
        {
            height: 25px;
            width: 155px;
        }
        .style25
        {
            height: 25px;
            width: 141px;
        }
        .style28
        {
            width: 122px;
        }
        .style29
        {
        }
        .style31
        {
            width: 133px;
        }
        .style32
        {
            width: 210px;
        }
        .style33
        {
            height: 25px;
            width: 105px;
        }
        .style36
        {
            height: 24px;
            }
        .style37
        {
            height: 25px;
            width: 118px;
        }
        .style38
        {
            height: 25px;
            width: 268435392px;
        }
        .style40
        {
            height: 25px;
            width: 131px;
        }
        .style41
        {
            width: 131px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Loan Endorsements" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                style="margin-right: 0px" Text="Export to Excel" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" Font-Size="8pt" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="5" 
                Width="100%" style="margin-top: 5px">
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
        </asp:View>
        
        <asp:View ID="View2" runat="server">
        <hr style="color: Maroon" />
            <table style="width:100%;">
                <tr>
                    <td class="style36">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label6" runat="server" Text="Loan No."></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style19">
                        <asp:TextBox ID="txtLoanNo" runat="server" Width="113px" 
                            ontextchanged="txtLoanNo_TextChanged"></asp:TextBox>
                    </td>
                    <td class="style33">
                        <asp:Label ID="Label7" runat="server" Text="Loan Code"></asp:Label>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                    <td class="style25" colspan="2">
                        <asp:TextBox ID="txtLoanCode" runat="server" ReadOnly="True" Width="109px"></asp:TextBox>
                    </td>
                    <td class="style41">
                        <asp:Label ID="Label8" runat="server" Text="Applicant"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style3" colspan="2">
                        <asp:TextBox ID="txtAplicant" runat="server" ReadOnly="True" Width="253px" 
                            style="margin-left: 0px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style36">
                        <asp:Label ID="Label9" runat="server" Text="Applic. Date"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="style19">
                        <asp:TextBox ID="txtApplicdate" runat="server" Width="115px"></asp:TextBox>
                    </td>
                    <td class="style33">
                        <asp:Label ID="Label10" runat="server" Text="App. Amount"></asp:Label>
                        &nbsp;&nbsp;
                    </td>
                    <td class="style25" colspan="2">
                        <asp:TextBox ID="txtAmtApplied" runat="server" Width="106px"></asp:TextBox>
                    </td>
                    <td class="style41">
                        <asp:Label ID="Label11" runat="server" Text="Approved Amount"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                    <td class="style13" colspan="2">
                        <asp:TextBox ID="txtApprovedAmt" runat="server" Width="98px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style36">
                        <asp:Label ID="Label13" runat="server" Text="Appraisal Date"></asp:Label>
                    </td>
                    <td class="style19">
                        <asp:TextBox ID="txtAppraisdate" runat="server" Width="112px"></asp:TextBox>
                    </td>
                    <td class="style33">
                        <asp:Label ID="Label14" runat="server" Text="Endorsed Amount"></asp:Label>
                    </td>
                    <td class="style25" colspan="2">
                        <asp:TextBox ID="txtEndorsedAmount" runat="server" Width="107px"></asp:TextBox>
                    </td>
                    <td class="style41">
                        <asp:Label ID="Label12" runat="server" Text="Member No"></asp:Label>
                    </td>
                    <td class="style13" colspan="2">
                        <asp:TextBox ID="txtMemberno" runat="server" Width="104px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        <asp:Label ID="Label16" runat="server" Text="Endorsed Date"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:TextBox ID="txtEndorseDate" runat="server" Width="110px"></asp:TextBox>
                    </td>
                    <td class="style33">
                        <asp:Label ID="Label17" runat="server" Text="Repay Method"></asp:Label>
                    </td>
                    <td class="style25" colspan="2">
                        <asp:TextBox ID="txtrepaymethod" runat="server" Width="103px"></asp:TextBox>
                    </td>
                    <td class="style40">
                        <asp:Label ID="Label18" runat="server" Text="Repay Period"></asp:Label>
                    </td>
                    <td class="style15" colspan="2">
                        <asp:TextBox ID="txtrepayperiod" runat="server" Width="102px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style36">
                        <asp:RadioButton ID="optAccept" runat="server" Checked="True" Text="Accept" 
                            AutoPostBack="True" oncheckedchanged="optAccept_CheckedChanged" />
                    </td>
                    <td class="style19">
                        <asp:RadioButton ID="optReject" runat="server" Text="Reject" 
                            AutoPostBack="True" oncheckedchanged="optReject_CheckedChanged" />
                    </td>
                    <td class="style33">
                        <asp:Label ID="Label26" runat="server" Text="Minute No"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtMinuteNo" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="style38" colspan="3">
                        &nbsp;</td>
                    <td class="style9">
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                            oncheckedchanged="CheckBox1_CheckedChanged" Text="Top Up" />
                    </td>
                </tr>
                <tr>
                    <td class="style36" colspan="8">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" Font-Size="8pt" 
                            onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="5" 
                            style="margin-top: 5px" Width="100%">
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
                <tr>
                    <td class="style36">
                        <asp:Label ID="Label19" runat="server" ForeColor="#9966FF" 
                            Text="Decline/ Approve reason"></asp:Label>
                    </td>
                    <td class="style18" colspan="3">
                        <asp:TextBox ID="txtReason" runat="server" Width="246px"></asp:TextBox>
                    </td>
                    <td class="style38" colspan="3">
                        <asp:Label ID="Label27" runat="server" style="float: right" 
                            Text="Member A/C"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtcashamount" runat="server" style="font-weight: 700"></asp:TextBox>
                    </td>
                </tr>
            </table>
            
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style31">
                    <asp:Label ID="Label20" runat="server" Text="Bank Control"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="cboBankAcc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAcc_SelectedIndexChanged" Height="22px">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtBankAccount" runat="server" Width="229px" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtchequeamount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label29" runat="server" Text="Insurance fee"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="Cboinsuranceacc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="Cboinsuranceacc_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtinsuranceacc" runat="server" Width="227px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtinsurancecharges" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label24" runat="server" Text="Processing Fees"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="cboprocessingAcc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboprocessingAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtProcessingAcc" runat="server" Width="226px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtProcessing" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label21" runat="server" Text="Refinancial fee"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="cboPremiumAcc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboPremiumAcc_SelectedIndexChanged" 
                        style="margin-left: 0px">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtPremiumAcc" runat="server" Width="228px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtpremium" runat="server">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label22" runat="server" Text="Intrest on Refinancing"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="cboRefinanceAcc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboRefinanceAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtrefinanceAcc" runat="server" Width="226px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtRefinance" runat="server">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label23" runat="server" Text="Bridging Fees"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="cboBridgingAcc" runat="server" 
                        onselectedindexchanged="cboBridgingAcc_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtBridgingAcc" runat="server" Width="225px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtBridging" runat="server">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label25" runat="server" Text="Other Income"></asp:Label>
                </td>
                <td class="style28">
                    <asp:DropDownList ID="CboSharesBoostingAcc" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="CboSharesBoostingAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style11" colspan="2">
                    <asp:TextBox ID="txtSharesBoostingAcc" runat="server" Width="229px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtSharesBoosting" runat="server">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style29" colspan="2">
                    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
                        style="float: right" Text="Add Charges" Width="118px" />
                </td>
                <td class="style11" colspan="4">
                    <asp:Button ID="btnRemove" runat="server" onclick="btnRemove_Click" 
                        Text="Remove Charges" Width="118px" />
                </td>
            </tr>
            <tr>
                <td class="style29" colspan="2">
                    &nbsp;</td>
                <td class="style11">
                    &nbsp;</td>
                <td class="style32">
                    &nbsp;</td>
                <td class="style11">
                    &nbsp;</td>
                <td class="style11">
                    &nbsp;</td>
            </tr>
    </table>
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
             <hr style="color: Maroon" />
            <asp:Button ID="btnSave" runat="server" Font-Size="12pt" 
                onclick="btnSave_Click" style="font-weight: 700" Text="SAVE" Width="114px" />
    </asp:View>
    </asp:MultiView>
</asp:Content>
