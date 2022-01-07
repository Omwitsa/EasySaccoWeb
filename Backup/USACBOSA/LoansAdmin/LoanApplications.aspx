<%@ Page Title="" Language="C#" MasterPageFile="~/LoansAdmin/LoansAdmin.Master" AutoEventWireup="true" CodeBehind="LoanApplications.aspx.cs" Inherits="USACBOSA.LoansAdmin.LoanApplications" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            width: 209px;
        }
        .style16
        {
            width: 190px;
        }
        .style18
        {
            width: 188px;
        }
        .style20
        {
            width: 210px;
        }
        .style21
        {
            width: 243px;
        }
        .style22
        {
            width: 241px;
        }
        .style23
        {
            width: 99px;
        }
        .style24
        {
            width: 97px;
        }
    </style>
    <script type="text/javascript">

//        function checkDate(sender, args) {
//            if (sender._selectedDate < new Date()) {
//                alert("You cannot select a day earlier than today!");
//                sender._selectedDate = new Date();
//                // set the date back to the current date
//                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
//            }
//        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Loan Applications" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
         <table frame="box" style="width: 100%;">
             <tr>
                 <td class="style18">
        <asp:Label ID="Label6" runat="server" Text="Member No"></asp:Label>
                     <br />
                     <asp:TextBox ID="txtMemberNo" runat="server" Width="132px" 
                    ontextchanged="txtMemberNo_TextChanged" ondatabinding="txtMemberNo_DataBinding" 
                         AutoPostBack="True"></asp:TextBox>
                 </td>
                 <td class="style21">
    <asp:Label ID="Label7" runat="server" Text="Member Names"></asp:Label>
                     <br />
                     <asp:TextBox 
                    ID="txtNames" runat="server" Width="193px" AutoPostBack="True"></asp:TextBox>
                 </td>
                 <td class="style20">
    <asp:Label ID="Label8" runat="server" Text="Deposits" style="color: #000000"></asp:Label>
                     <br />
                     <asp:TextBox ID="txtTotalShares1" runat="server" Width="147px"></asp:TextBox>
                 </td>
                 <td>
                <asp:Label ID="Label12" runat="server" Text="Share Capital" style="color: #000000"></asp:Label>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="Label24" runat="server" style="color: #000000" 
                         Text="Matured Shares"></asp:Label>
                     <br />
                <asp:TextBox ID="txtMaturedShares" runat="server" Width="105px" AutoPostBack="True"></asp:TextBox>
                 &nbsp;&nbsp;
                     <asp:TextBox ID="TxttotAmount" runat="server" Width="112px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td class="style18">
                <asp:Label 
        ID="Label11" runat="server" Text="Max. Loan Amount"></asp:Label>
                     <br />
                <asp:TextBox 
        ID="txtMaxLoanAmount" runat="server" Width="131px" AutoPostBack="True"></asp:TextBox>
                 </td>
                 <td class="style21">
                <asp:Label ID="Label13" runat="server" Text="Monthly Principal"></asp:Label>
                     <br />
                     <asp:TextBox 
    ID="txtMonthlyPrincipal" runat="server" Width="156px" AutoPostBack="True"></asp:TextBox>
                 </td>
                 <td class="style20">
    <asp:Label ID="Label9" runat="server" Text="Outstanding Loan"></asp:Label>
                     <br />
                     <asp:TextBox 
                    ID="txtOutstandingLoan" runat="server" Width="125px"></asp:TextBox>
                 </td>
                 <td>
    <asp:Label ID="Label10" runat="server" Text="Oustanding Loan Bal"></asp:Label>
                     <br />
                     <asp:TextBox 
                    ID="txtOutstandingLoanBal" runat="server" Width="99px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td class="style18">
    <asp:Label ID="txtCurrentTotalDeductions" runat="server" 
        Text="Current Total Deductions"></asp:Label>
                     <br />
                     <asp:TextBox ID="txtCurrTotalDeductions" 
                    runat="server" Width="133px" AutoPostBack="True" 
                         ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                 </td>
                 <td class="style21">
                <asp:Label ID="Label14" runat="server" Text="Monthly Intrest"></asp:Label>
                     <br />
                <asp:TextBox 
    ID="txtMonthlyIntrest" runat="server" style="margin-left: 0px" Width="155px" 
                         AutoPostBack="True"></asp:TextBox>
                 </td>
                 <td class="style20">
    <asp:Label ID="Label15" runat="server" Text="Total Monthly Repayment"></asp:Label>
                     <br />
                     <asp:TextBox ID="txtTotalMontlyInstallment" runat="server" Width="127px"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
    </table>
    <table frame="box" style="width: 100%;">
        <tr>
            <td class="style16">
                <asp:Label ID="Label20" runat="server" Text="Loan Code"></asp:Label>
                <br />
                <asp:DropDownList ID="cboLoanCode" runat="server" AutoPostBack="True" 
                    Height="20px" onselectedindexchanged="cboLoanNo_SelectedIndexChanged" 
                    Width="133px">
                </asp:DropDownList>
            </td>
            <td class="style22">
                <asp:Label ID="Label17" runat="server" Text="Loan No."></asp:Label>
                <br />
                <asp:TextBox ID="txtLoanNo" runat="server" Width="152px" AutoPostBack="True" 
                    ForeColor="#6699FF" ReadOnly="True"></asp:TextBox>
                </td>
            <td class="style5">
                <asp:Label ID="Label18" runat="server" Text="Loan Amount"></asp:Label>
                <br />
                <asp:TextBox ID="txtLoanAmount" runat="server" Width="122px"></asp:TextBox>
            </td>
            <td>
<asp:Label ID="Label16" runat="server" Text="Application Date"></asp:Label>
                <br />
                <asp:TextBox 
                    ID="txtApplicationDate" runat="server" Width="106px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtApplicationDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtApplicationDate">
                            </asp:CalendarExtender>
                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/Images/calendar.png" />
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="Label19" runat="server" Text="Repay Period(M)"></asp:Label>
                <br />
                <asp:TextBox ID="txtRepayPeriod" runat="server" Width="98px" 
                    AutoPostBack="True" ForeColor="#6666FF"></asp:TextBox>
                </td>
            <td class="style22">
                <br />
                <asp:TextBox ID="txtLoanType" runat="server" Width="218px" AutoPostBack="True" 
                    ForeColor="#6600FF"></asp:TextBox>
            </td>
            <td class="style5">
                <asp:Label ID="Label21" runat="server" Text="Intrest Rate"></asp:Label>
                <br />
                <asp:TextBox ID="txtInterest" runat="server" Width="98px" 
                    ontextchanged="TextBox12_TextChanged" AutoPostBack="True" 
                    ForeColor="#6666FF"></asp:TextBox>
                &nbsp;<asp:Label ID="Label23" runat="server" Text="%pa"></asp:Label>
                </td>
            <td>
                <asp:Label ID="Label22" runat="server" Text="Repay Method"></asp:Label>
                <br />
                <asp:TextBox ID="txtRepaymethod" runat="server" Width="107px" 
                    AutoPostBack="True" ForeColor="#6666FF"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                    Width="82px" />
            &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
                    Text="Update" />
            </td>
            <td class="style22">
                
                <asp:Button ID="btnApplications" runat="server" onclick="btnApplications_Click" 
                    Text="View Applications" Visible="False" />
                
            </td>
            <td class="style5">
                <asp:Button ID="btnAddGuarantors" runat="server" Text="Guarantors" 
                    Width="118px" Visible="False" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Font-Size="8pt" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="100" 
                    style="margin-top: 0px" Width="874px" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
    AutoGenerateSelectButton="True">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="AtmSelector" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#999966" />
                </asp:GridView>
    
                <table style="width:100%;">
        <tr>
            <td class="style23">
                <asp:Button ID="btnOK" runat="server" Text="OK" Width="51px" 
                    onclick="btnOK_Click" />
            </td>
            <td class="style24">
                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                    Text="Cancel" />
            </td>
            <td>
                <asp:CheckBox ID="chkTopUp" runat="server" 
                    oncheckedchanged="chkTopUp_CheckedChanged" Text="Top Up" />
            </td>
        </tr>
        <tr>
            <td class="style23">
                &nbsp;</td>
            <td class="style24">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style23">
                &nbsp;</td>
            <td class="style24">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
