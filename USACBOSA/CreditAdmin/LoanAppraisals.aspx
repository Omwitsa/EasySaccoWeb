<%@ Page Title="" Language="C#" MasterPageFile="~/CreditAdmin/CreditAdmin.Master" AutoEventWireup="true" CodeBehind="LoanAppraisals.aspx.cs" Inherits="USACBOSA.CreditAdmin.LoanAppraisals" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 262px;
        }
        .style6
        {
            width: 317px;
        }
        .style8
        {
            width: 269px;
        }
        .style9
        {
            width: 295px;
        }
        .style10
        {
            width: 289px;
        }
        
        fieldset {
  font-size:12px;
  padding:10px;
  width:250px;
  line-height:15.8;}
    </style>
    <hr style="color: Maroon" />
    <style type="text/css">
        .style1
        {
            width: 266px;
        }
        .style2
        {
            width: 227px;
        }
        .style3
        {
            width: 236px;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 162px;
        }
    </style>
    <script type="text/javascript">

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Loan Appraisals" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View3" runat="server">
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
                    
            </asp:View>
            <hr style="color: Maroon" />

            <asp:View ID="View2" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label6" runat="server" Text="Loan No" style="float: right"></asp:Label>
                            &nbsp;</td>
                        <td class="style2">
                            <asp:TextBox ID="txtLoanNo" runat="server" 
                                ontextchanged="txtLoanNo_TextChanged"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <asp:Label ID="Label7" runat="server" Text="Member No" style="float: right"></asp:Label>
                            &nbsp;</td>
                        <td class="style3">
                            <asp:TextBox ID="txtMemberNo" runat="server" AutoPostBack="True" 
                                ontextchanged="TextBox2_TextChanged"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNames" runat="server" AutoPostBack="True" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label8" runat="server" Text="Loan Balance" style="float: right"></asp:Label>
                            &nbsp;</td>
                        <td class="style2">
                            <asp:TextBox ID="txtLoanBalance" runat="server" 
                                ontextchanged="txtLoanBalance_TextChanged">0.00</asp:TextBox>
                        </td>
                        <td class="style3">
                            <asp:Label ID="Label9" runat="server" Text="Max. Loan Amt" style="float: right"></asp:Label>
                            &nbsp;</td>
                        <td class="style3">
                            <asp:TextBox ID="txtMaxLAmount" runat="server" Width="124px">0.00</asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Total Shares" style="float: right"></asp:Label>
                            &nbsp;</td>
                        
                        <td>
                            <asp:TextBox ID="txtTotalShares" runat="server">0.00</asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
                <hr style="color: Maroon" />
                            <table style="width:100%; height: 118px;">
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label11" runat="server" Text="Appraisal Date" 
                                            style="float: right"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <asp:TextBox ID="txtAppraisalDate" runat="server" Height="22px" Width="111px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Images/calendar.png" />
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label12" runat="server" style="float: right" Text="R. Method"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtrepaymethod" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" style="float: right" 
                                            Text="Applied Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAppliedAmount" runat="server">0.00</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label14" runat="server" style="float: right" 
                                            Text="Decline/Approval Reason"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <asp:TextBox ID="txtReason" runat="server" Width="127px"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label15" runat="server" style="float: right" Text="For Duration"></asp:Label>
                                        &nbsp;</td>
                                    <td class="style8">
                                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" style="float: right" 
                                            Text="Recommended Amt."></asp:Label>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtRecomendedAmount" runat="server" AutoPostBack="True">0.00</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label17" runat="server" Text="Repayment Period" 
                                            style="float: right"></asp:Label>
                                        &nbsp;</td>
                                    <td class="style9">
                                        <asp:TextBox ID="txtRPeriod" runat="server" 
                                            ontextchanged="txtRPeriod_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style8">
                                        <asp:Label ID="Label18" runat="server" style="float: right" Text="Rate(%)"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtIntRate" runat="server"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButton ID="PrintAppraisalReport" runat="server" Checked="True" 
                                            style="font-weight: 700" Text="Print Appraisal Report" />
                                    </td>
                                </tr>
                            </table>
                <table style="width:100%; height: 133px;">
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chk2third" runat="server" Text=" 2/3 Rule met" 
                                            AutoPostBack="True" oncheckedchanged="chk2third_CheckedChanged" 
                                            Checked="True" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt2tHIRD" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Basic Salary"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBasicSalary" runat="server" 
                                            ontextchanged="txtBasicSalary_TextChanged" AutoPostBack="True">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Other Allowances"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOAllowance" runat="server" AutoPostBack="True" 
                                            ontextchanged="txtOAllowance_TextChanged">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Total Earnings"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTEarnings" runat="server" AutoPostBack="True">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="Expected Net salary"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpectedNetSalary" runat="server" AutoPostBack="True">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="Deduction To Gross"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeductionTogross" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                                </td>
                        <td colspan="2">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Monthly Contrib(D/Debit)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtminimumContrib" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="Statutory Ded"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStatutoryDed" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Text="Other Deductions"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherDeductions" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Text="Total Loans(P and I)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrentLoanDeductions" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" Text="New Loan Deductions (P and I)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewDeduction" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Total Deductions"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalDeduction" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Text="Statutory Ded to Gross"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStatutoryToGross" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" 
                                            Text="Total Other Ded + New Loan to Gross"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalDedNewloantoGross" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server" Text="Net Salary to Gross salary"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNetsalaryToGross" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="Total Ded to Gross - Statutory"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalLoanToGross" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="Total Co-op Ded to Gross"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcoopDedToGross" runat="server">0.00</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAppraise" runat="server" onclick="btnAppraise_Click" 
                                style="float: right" Text="Apprais !" Font-Bold="True" Width="83px" />
                        </td>
                        <td>
                            <asp:Label ID="Label36" runat="server" style="float: right" 
                                Text="Appraised Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecAmt" runat="server" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" style="float: right" Text="Save" 
                                onclick="btnSave_Click" Font-Bold="True" Width="81px" />
                        </td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                  <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    PageSize="5" 
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
                <asp:GridView ID="GridView3" runat="server" AutoGenerateSelectButton="True" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Size="8pt" 
                    PageSize="5" 
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
                                
              <%--  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
            </asp:View>
    </asp:MultiView>
    
</asp:Content>
