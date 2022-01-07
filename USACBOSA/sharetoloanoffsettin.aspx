<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="sharetoloanoffsettin.aspx.cs" Inherits="USACBOSA.sharetoloanoffsettin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 110%;
            height: 128px;
        }
        .style3
        {
            color: #000000;
            width: 76px;
        }
        .style5
        {
            color: #000000;
        }
        .style6
        {
            width: 96px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                                <asp:Label ID="MemberNo" runat="server" Text="MemberNo"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:TextBox ID="TxtMemberNo" runat="server" AutoPostBack="True" Width="109px" 
                                                    ontextchanged="TxtMemberNo_TextChanged" style="height: 22px"></asp:TextBox>
                                            </td>
                                            <td class="style21">
                                                <asp:Label ID="Names" runat="server" Text="Names"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtNames" runat="server" Width="230px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="TotalShares" runat="server" Text="Total Shares"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:TextBox ID="TxtTotShares" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="ShareCode" runat="server" Text="ShareCode"></asp:Label>
                                            </td>
                                            <td class="style20">
                                                <asp:DropDownList ID="Dropsharecode" runat="server" Height="23px" Width="100px" 
                                                    AutoPostBack="True" onselectedindexchanged="Dropsharecode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style21">
                                                <asp:Label ID="ShareType" runat="server" Text="Share Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextShareType" runat="server" Width="229px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="TotLoanBal" runat="server" Text="Total Loan Bal"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:TextBox ID="TxtBalance" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5" colspan="6">
                                            <asp:GridView ID="GridView1" runat="server" Font-Size="8pt" 
                                                PageSize="100" 
                                                    style="margin-top: 0px" Width="709px" 
                                                    onselectedindexchanged="GridView1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="AtmSelector" runat="server" />
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
                                                <asp:TextBox ID="txtOffsettingfee" runat="server" Width="125px"></asp:TextBox>
                                            </td>
                                            <td class="style21" colspan="3">
                                                <asp:DropDownList ID="DropDownList2" runat="server" Height="25px" Width="97px" 
                                                    AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Button ID="Find" runat="server" Text="F" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="TextBox3" runat="server" Width="194px"></asp:TextBox>
                                            </td>
                                            <td class="style22">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td class="style20">
                                                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                                                    Text="Guarantor Transfer" />
                                            </td>
                                            <td class="style21">
                                                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                                                    Text="Loan Consolidation" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
                                                    Text="Loan Transfer" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Offsett" runat="server" Font-Bold="True" Font-Size="12pt" 
                                                    ForeColor="#0033CC" onclick="Offsett_Click" Text="Off Sett" Width="105px" />
                                            </td>
                                            <td class="style22">
                                                <asp:TextBox ID="dtpTransDate" runat="server" Visible="False"></asp:TextBox>
                                            </td>
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
                                    <table class="style3">
                                        <tr>
                                            <td class="style4">
                                                Loan Type</td>
                                            <td class="style24">
                                                <asp:DropDownList ID="DropDownList3" runat="server" Height="22px" Width="89px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox4" runat="server" Height="23px" Width="243px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Interest Rate</td>
                                            <td>
                                                <asp:TextBox ID="TextBox5" runat="server" Width="106px"></asp:TextBox>
                                                <span class="style25">%</span></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                Repay Period</td>
                                            <td class="style24">
                                                <asp:TextBox ID="TextBox6" runat="server" Width="86px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                Repay Method</td>
                                            <td>
                                                <asp:TextBox ID="TextBox7" runat="server" Width="102px"></asp:TextBox>
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
                                                <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
                                                    Text="TRANSFER" />
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
        <asp:View ID="View3" runat="server">
            <table class="style2">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView5" runat="server" 
                                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" Font-Size="8pt" GridLines="Vertical" Width="711px" 
                                        onselectedindexchanged="GridView5_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="#DCDCDC" />
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#000065" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table class="style3">
                <tr>
                    <td>
                        <table class="style3">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="Dev0" runat="server" Height="26px" Width="92px" 
                                        AutoPostBack="True" onselectedindexchanged="Dev0_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="TxtDevloan0" runat="server" Width="241px"></asp:TextBox>
                                </td>
                                <td class="style6">
                                    &nbsp;</td>
                                <td class="style13">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Loano0" runat="server" Text="Loano(new)"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="Newloan0" runat="server" Width="97px"></asp:TextBox>
                                </td>
                                <td class="style6">
                                    <asp:Label ID="Amnt2" runat="server" Text="Amount"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:TextBox ID="mnt0" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Period0" runat="server" Text="Repay Method"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Rmethod" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="interest0" runat="server" Text="Interest Rate"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:TextBox ID="Rate0" runat="server"></asp:TextBox>
                                    %P.A</td>
                                <td class="style6">
                                    <asp:Label ID="RepayPriod0" runat="server" Text="Repay Period"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:TextBox ID="Rperiod0" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="ChargeAccount" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Chargeperiod0" runat="server" Text=" Charge Account"></asp:Label>
                                </td>
                                <td class="style26">
                                    <asp:DropDownList ID="DropDownList4" runat="server" Height="20px" Width="101px">
                                    </asp:DropDownList>
                                </td>
                                <td class="style6">
                                    <asp:TextBox ID="AccName0" runat="server"></asp:TextBox>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="Rcharges0" runat="server" Text="Rate(Charges)"></asp:Label>
                                    <asp:TextBox ID="Charges0" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Amnt3" runat="server" Text="Amount"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Amont0" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td class="style26">
                                    &nbsp;</td>
                                <td class="style6">
                                    &nbsp;</td>
                                <td class="style13">
                                    <asp:Button ID="Button6" runat="server" onclick="Button1_Click" 
                                        Text="CONSOLIDATE" />
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
