<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceAdmin/FinanceAdmin.Master" AutoEventWireup="true" CodeBehind="PaymentOptions.aspx.cs" Inherits="USACBOSA.FinanceAdmin.PaymentOptions" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>--%>
 
 
    <style type="text/css">
        .style1
        {
            width: 93px;
        }
    </style>
 
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="Dividends payment - DPA" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                    <asp:Label ID="Label6" runat="server" Text="Member No:" style="float: right" 
                        Width="77px"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="txtMemberNo" runat="server" Height="22px" Width="110px" 
                        ondatabinding="txtMemberNo_DataBinding" 
                         AutoPostBack="True" ontextchanged="txtMemberNo_TextChanged"></asp:TextBox>
                    </td>
            <td>
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="F" 
                        Font-Bold="True" />
                </td>
            <td colspan="2">
                    <asp:TextBox ID="txtNames" runat="server" Width="265px"></asp:TextBox>
                </td>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                    <asp:Label ID="Label27" runat="server" style="float: right" 
                        Text="Net Dividend:"></asp:Label>
                    </td>
            <td class="style1">
                    <asp:TextBox ID="TextBox2" runat="server" ontextchanged="TextBox2_TextChanged" 
                        Width="106px"></asp:TextBox>
            </td>
            <td>
                    &nbsp;</td>
            <td colspan="2">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td>
                    <asp:Label ID="Label29" runat="server" Text="Date of paying dividends"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                     <asp:CalendarExtender ID="TextBox10_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="TextBox10">
                            </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Cash:" 
                        style="float: right; margin-left: 25px; text-align: left;" AutoPostBack="True" 
                        oncheckedchanged="CheckBox2_CheckedChanged"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td class="style1">
                    <asp:DropDownList ID="cboBankAC" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="22px" 
                        Width="110px">
                    </asp:DropDownList>
                </td>
            <td>
                <asp:Button ID="btnFindBank" runat="server" Font-Bold="True" 
                    onclick="btnFindBank_Click" Text="F" />
            </td>
            <td colspan="2">
                    <asp:TextBox ID="txtBankAC" runat="server" Width="170px" Height="21px"></asp:TextBox>
                    <asp:TextBox ID="TextBox8" runat="server" ontextchanged="TextBox8_TextChanged" 
                        style="margin-left: 7px"></asp:TextBox>
                </td>
            <td>
                    <asp:Label ID="Label23" runat="server" style="float: right" Text="GL Contra Source:"></asp:Label>
                </td>
            <td>
                    <asp:DropDownList ID="cboGlAccountNo" runat="server" onselectedindexchanged="cboGlAccountNo_SelectedIndexChanged" 
                        >
                    </asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" Text="F" />
                   
                </td>
        </tr>
        <tr>
            <td class="style31">
                    </td>
            <td class="style31">
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Deposits:" 
                        style="float: right; margin-left: 17px;" AutoPostBack="True" 
                        oncheckedchanged="CheckBox3_CheckedChanged"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            <td class="style1">
                    <asp:DropDownList ID="cboPaymentMode" runat="server" Height="22px" 
                        Width="110px" AutoPostBack="True" 
                        onselectedindexchanged="cboPaymentMode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            <td>
                </td>
            <td colspan="2">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    
                </td>
            <td class="style31">
                    </td>
            <td class="style31">
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                        style="font-weight: 700" Text="PAY" Width="79px" />
            </td>
            <td>
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Loan Repay:" 
                        style="float: right" AutoPostBack="True" 
                        oncheckedchanged="CheckBox4_CheckedChanged"/>
                    </td>
            <td class="style1">
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
              </td>
        
            <td colspan="3">
                &nbsp;
                    &nbsp;<asp:Button ID="Button1" runat="server"  Text="OK" 
                        Visible="True" style="font-weight: 700" onclick="Button1_Click" />
              </td>
        </tr>
    </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView2" runat="server" Width="893px" 
                        AutoGenerateSelectButton="True" Font-Size="8pt" 
        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
                    </asp:GridView>
                     </asp:Content>
