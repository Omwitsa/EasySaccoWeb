<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="LoanType.aspx.cs" Inherits="USACBOSA.SysAdmin.LoanType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        fieldset {
  font-size:12px;
  padding:10px;
  width:250px;
  line-height:1.8;}
        .style33
        {
        }
        .style34
        {
            width: 158px;
        }
        .style35
        {
        }
        .style36
        {
            width: 151px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Loan Types" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
            <table style="width:100%;">
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label6" runat="server" Text="Loan Code" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                                <asp:DropDownList ID="cboLoanCode" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                <asp:TextBox ID="txtLoanCode" runat="server" 
                        ontextchanged="txtLoanCode_TextChanged" Width="64px"></asp:TextBox>
                            </td>
                            <td class="style35">
                <asp:Label ID="Label7" runat="server" Text="Loan Type" style="float: right"></asp:Label>
                            </td>
                            <td colspan="7">
                <asp:TextBox ID="txtLoanType" runat="server" Width="236px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label8" runat="server" Text="R.Period(Months)" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                <asp:TextBox ID="txtRepayPeriod" runat="server" Width="104px"></asp:TextBox>
                            </td>
                            <td class="style35">
                <asp:Label ID="Label9" runat="server" Text="Repay Method" style="float: right"></asp:Label>
                            </td>
                            <td>
                <asp:DropDownList ID="cboRepayMethod" runat="server" Height="22px" Width="126px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>STL</asp:ListItem>
                    <asp:ListItem>RBAL</asp:ListItem>
                    <asp:ListItem>AMRT</asp:ListItem>
                </asp:DropDownList>
                            </td>
                            <td colspan="6">
                <asp:CheckBox ID="chkbridging" runat="server" Text="Can it Refinance" 
                        AutoPostBack="True" oncheckedchanged="chkbridging_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label10" runat="server" Text="Max. Amount" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                <asp:TextBox ID="txtMaxAmmount" runat="server" Width="106px"></asp:TextBox>
                            </td>
                            <td class="style35">
                <asp:Label ID="Label11" runat="server" Text="Intrest Rate" style="float: right"></asp:Label>
                            </td>
                            <td>
                <asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="6">
                <asp:CheckBox ID="ChkGuarantors" runat="server" Text="Requires Guarantors" 
                        style="float: none" />
                            </td>
                        </tr>
                         <tr>
                            <td class="style34">
                <asp:Label ID="Label12" runat="server" Text="Nums Loans" style="float: right"></asp:Label>
                             </td>
                            <td class="style36">
                <asp:TextBox ID="txtNumOfLoans" runat="server" Width="97px"></asp:TextBox>
                             </td>
                            <td class="style35">
                <asp:Label ID="Label13" runat="server" Text="Max. Loans" style="float: right"></asp:Label>
                             </td>
                            <td>
                    <asp:TextBox ID="txtMaxLoans" runat="server" Width="85px"></asp:TextBox>
                             </td>
                            <td colspan="6">
                <asp:CheckBox ID="chkSelfGuar" runat="server" 
                    Text="Guarantee own loan" />
                             </td>
                        </tr>
                        <tr>
                            <td class="style34">
                    <asp:Label ID="Label21" runat="server" Text="Priority" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                    <asp:TextBox ID="txtpriority" runat="server" Width="78px"></asp:TextBox>
                            </td>
                            <td class="style35">
                <asp:Label ID="Label15" runat="server" Text="MDTI" style="float: right"></asp:Label>
                            </td>
                            <td>
                <asp:TextBox ID="cboMDTI" runat="server" Width="97px"></asp:TextBox>
                            </td>
                            <td colspan="6">
                    <asp:Label ID="Label20" runat="server" Text="Rate" ForeColor="#0000CC" 
                        style="float: none"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                
                            <asp:CheckBox ID="chkAttractPenalty" runat="server" 
                                    Text="Attracts Penalty" style="float: right" AutoPostBack="True" 
                                oncheckedchanged="chkAttractPenalty_CheckedChanged" />
                
                            </td>
                            <td class="style36">
                                    <asp:RadioButton ID="optFixed" runat="server" Text="Fixed" 
                                        AutoPostBack="True" oncheckedchanged="optFixed_CheckedChanged" />
                            </td>
                            <td class="style35">
                            <asp:RadioButton ID="optPercentage" runat="server" Text="Percentage" AutoPostBack="True" 
                                oncheckedchanged="optPercentage_CheckedChanged" style="float: right" />                          
                            </td>
                            <td><asp:TextBox ID="txtPenaltyAmount" runat="server" Width="91px"></asp:TextBox>
                                <asp:Label ID="Label23" runat="server" Text="Value"></asp:Label>
                            </td>
                            <td>
                
                                <asp:RadioButton ID="optdaily" runat="server" Text="Daily" 
                            oncheckedchanged="optdaily_CheckedChanged" AutoPostBack="True" />
                
                            </td>
                            <td colspan="4">
                            
                                <asp:RadioButton ID="optWeekly" runat="server" Text="Weekly" 
                            oncheckedchanged="optWeekly_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:RadioButton ID="optMonthly" runat="server" Text="Monthly" 
                            oncheckedchanged="optMonthly_CheckedChanged" AutoPostBack="True" />
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label14" runat="server" Text="Loan Control A/C" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                    <asp:TextBox ID="txtAccno" runat="server" ontextchanged="cboAccno_TextChanged" 
                        Width="106px" AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="btnLoanAcctsSearch" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" onclick="btnLoanAcctsSearch_Click" 
                        Width="16px" />
                            </td>
                            <td colspan="2">
                <asp:TextBox ID="txtAccNames" runat="server" Width="263px" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td colspan="3">
                <asp:Label ID="Label16" runat="server" Text="Topup(%)"></asp:Label>
                            </td>
                            <td colspan="3">
                <asp:TextBox ID="cboTRB" runat="server" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label17" runat="server" Text="Intrest Control A/C" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                    <asp:TextBox ID="txtAccnoIntrst" runat="server" Width="106px"></asp:TextBox>
                    <asp:ImageButton ID="btnIntrestAcctsSearch" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" 
                        onclick="btnIntrestAcctsSearch_Click" />
                            </td>
                            <td colspan="2">
                <asp:TextBox ID="txtAccNamesIntrest" runat="server" Width="264px"></asp:TextBox>
                            </td>
                            <td colspan="6">
                    <asp:Label ID="Label19" runat="server" Text="What to Charge" 
                        ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                <asp:Label ID="Label18" runat="server" Text="Penalty Control A/C" style="float: right"></asp:Label>
                            </td>
                            <td class="style36">
                    <asp:TextBox ID="txtAccnoPenalty" runat="server" Width="106px"></asp:TextBox>
                    <asp:ImageButton ID="btnPenaltyAcctsSearch" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" 
                        onclick="btnPenaltyAcctsSearch_Click" />
                            </td>
                            <td colspan="2">
                <asp:TextBox ID="txtAccNamesPenalty" runat="server" Width="262px"></asp:TextBox>
                            </td>
                            <td colspan="2">
                    <asp:RadioButton ID="optPrincipal" runat="server" Text="Principal" 
                        oncheckedchanged="optPrincipal_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td colspan="2">
                    <asp:RadioButton ID="optInterest" runat="server" Text="Intrest" 
                        oncheckedchanged="optInterest_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="optBoth" runat="server" Text="Both" 
                                    oncheckedchanged="optBoth_CheckedChanged" AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style34">
                                <asp:Label ID="Label22" runat="server" Text="Search By" style="float: right"></asp:Label>
                            </td>
                            <td class="style33" colspan="3">
                
                <asp:DropDownList ID="cboSearchBy" runat="server" Height="20px" 
                        Width="108px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Account No</asp:ListItem>
                    <asp:ListItem>Account Name</asp:ListItem>
                </asp:DropDownList>
                
                <asp:TextBox ID="txtvalue" runat="server" Width="200px"></asp:TextBox>
                
                <asp:Button ID="btnFindSearch" runat="server" 
                    onclick="btnFindSearch_Click" Text="Find" />
                    <asp:TextBox ID="txtSetValue" runat="server" Visible="False" Width="37px"></asp:TextBox>
                
                            </td>
                            <td colspan="6">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                        Width="67px" style="float: left; font-weight: 700;" />&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                        Text="Update" style="font-weight: 700" />&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Delete" 
                                    style="font-weight: 700" />
                            </td>
                        </tr>
                        </table>
                   
                     <hr style="color: Maroon" />
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
                    BackColor="#FFFFCC" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
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
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" Font-Size="8pt"  PageSize="10" 
                    style="margin-top: 0px" Width="874px" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="AtmSelector" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#999966" />
                </asp:GridView>
        </asp:Content>
