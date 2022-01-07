<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="SharetoLoanOffset.aspx.cs" Inherits="USACBOSA.FinanceAdmin.SharetoLoanOffset" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 117px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <table class="style2">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <table class="style3">
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style20">
                                                &nbsp;</td>
                                            <td class="style21">
                                                &nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="Loannumber" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style22">
                                                <asp:TextBox ID="dtpTransDate" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="MemberNo" runat="server" Text="MemberNo"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:TextBox ID="txtMemberNo" runat="server" AutoPostBack="True" Width="109px" 
                                                    style="height: 22px" ontextchanged="txtMemberNo_TextChanged1"></asp:TextBox>
                                            </td>
                                            <td class="style21">
                                                <asp:Label ID="Names" runat="server" Text="Names"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMemberNames" runat="server" Width="230px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="TotalShares" runat="server" Text="Total Shares"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:TextBox ID="txtTotalShares" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="ShareCode" runat="server" Text="ShareCode"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:DropDownList ID="cboSharesType" runat="server" Height="23px" Width="100px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="cboSharesType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style21">
                                                <asp:Label ID="ShareType" runat="server" Text="Share Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtsharetype" runat="server" Width="229px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="TotLoanBal" runat="server" Text="Total Loan Bal"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:TextBox ID="txtlbalance" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="Label5" runat="server" Text="Offset Amount"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" 
                                                    ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                                            </td>
                                            <td class="style21">
                                                <asp:Label ID="Label6" runat="server" Text="Offset Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                 <asp:CalendarExtender ID="TextBox2_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox2">
                            </asp:CalendarExtender>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style22">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="Label7" runat="server" Text="Loan No:"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                                    Height="27px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                                                    Width="133px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style21">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style22">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style5" colspan="6">
                                            <asp:GridView ID="GridView1" runat="server" Font-Size="8pt" 
                                                PageSize="100" 
                                                    style="margin-top: 0px" Width="709px" 
                                                    onrowdatabound="GridView1_RowDataBound" 
                                                    onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                                    onselectedindexchanged="GridView1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="AtmSelector" runat="server"
                                                                   />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#999966" />
                                                </asp:GridView>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="offsettingfee" runat="server" Text="Offsetting Fee"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:TextBox ID="txtOffsettingFee" runat="server" Width="125px"></asp:TextBox>
                                            </td>
                                            <td class="style21" colspan="3">
                                                <asp:DropDownList ID="cboAccno" runat="server" Height="25px" Width="97px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="cboAccno_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                                <asp:Button ID="Find" runat="server" Text="F" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtAccNames" runat="server" Width="194px"></asp:TextBox>
                                            </td>
                                            <td class="style22">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style20">
                                                <asp:Button ID="btnGuarTransfer" runat="server"  Text="Guarantor Transfer" 
                                                    onclick="btnGuarTransfer_Click" style="height: 26px" />
                                            </td>
                                            <td class="style21">
                                                <asp:Button ID="btnLoanConsolidation" runat="server" Text="Loan Consolidation" 
                                                    onclick="btnLoanConsolidation_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnLoanTransfer" runat="server" Text="Loan Transfer" 
                                                    onclick="btnLoanTransfer_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Offset" runat="server" Font-Bold="True" Font-Size="12pt" 
                                                    ForeColor="#0033CC" onclick="Offsett_Click" Text="OffSet" Width="105px" />
                                            </td>
                                            <td class="style22">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
     <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table class="style2">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <table class="style3">
                                        <tr>
                                            <td class="style4">
                                                Loan Type</td>
                                            <td class="style24">
                                                <asp:DropDownList ID="cboTLoanCode" runat="server" Height="22px" Width="89px" 
                                                    onselectedindexchanged="cboTLoanCode_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTLoanType" runat="server" Height="23px" Width="243px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Interest Rate</td>
                                            <td>
                                                <asp:TextBox ID="txtTRate" runat="server" Width="106px"></asp:TextBox>
                                                <span class="style25">%</span></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                Repay Period</td>
                                            <td class="style24">
                                                <asp:TextBox ID="txtTRPeriod" runat="server" Width="86px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                Repay Method</td>
                                            <td>
                                                <asp:TextBox ID="txtTRMethod" runat="server" Width="102px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnTransfer" runat="server" Text="TRANSFER" 
                                                    onclick="btnTransfer_Click" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
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
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table class="style2">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" Font-Size="8pt" 
                                                PageSize="100" 
                                                    style="margin-top: 0px" Width="709px" 
                                        AutoGenerateSelectButton="True" onrowdatabound="GridView2_RowDataBound">
                                                    <HeaderStyle BackColor="#999966" />
                                                </asp:GridView>
                                    <br />
                                    <asp:Button ID="btnGuarOffset" runat="server" Text="GUAR. OFFSET!" 
                                        Width="122px" onclick="btnGuarOffset_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table class="style3">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Loan Code"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:DropDownList ID="cboLoanCode" runat="server" AutoPostBack="True" 
                                        Height="26px" onselectedindexchanged="Dev0_SelectedIndexChanged" Width="92px">
                                    </asp:DropDownList>
                                </td>
                                <td class="style6">
                                    <asp:Label ID="Label2" runat="server" Text="Loan Type"></asp:Label>
                                </td>
                                <td class="style13" colspan="3">
                                    <asp:TextBox ID="txtLoanType" runat="server" Width="282px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Loano0" runat="server" Text="Loano(new)"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="Newloan0" runat="server" Width="97px"></asp:TextBox>
                                </td>
                                <td class="style6" colspan="2">
                                    <asp:Label ID="Amnt2" runat="server" Text="Loan Amount + Charges(if any)"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="mnt0" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="interest0" runat="server" Text="Interest Rate"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="txtRate" runat="server" Width="86px"></asp:TextBox>
                                    %P.A</td>
                                <td class="style6">
                                    <asp:Label ID="RepayPriod0" runat="server" Text="Repay Period"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtRPeriod" runat="server" Width="85px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Period0" runat="server" Text="Repay Method"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="txtRMethod" runat="server" Width="77px"></asp:TextBox>
                                </td>
                                <td class="style6">
                                    <asp:Label ID="Rcharges0" runat="server" Text="Rate(Charges)"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtChargeRate" runat="server" Width="83px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="Check1" runat="server" Text="Charge Account" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Amnt3" runat="server" Text="Charge Amount"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="txtCharges" runat="server" Width="82px"></asp:TextBox>
                                </td>
                                <td class="style6">
                                    <asp:Label ID="Chargeperiod0" runat="server" Text=" Charge Account"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:DropDownList ID="cboLCAccno" runat="server" Height="20px"  Width="101px" 
                                        onselectedindexchanged="cboLCAccno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLCAccNames" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td class="style26">
                                    &nbsp;</td>
                                <td class="style6">
                                    &nbsp;</td>
                                <td class="style1">
                                    <asp:Button ID="btnConsolidate" runat="server" 
                                        Text="CONSOLIDATE" onclick="btnConsolidate_Click" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
