<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="Journal Posting.aspx.cs" Inherits="USACBOSA.offsetting.Journal_Posting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 83%;
            height: 517px;
        }
        .style2
        {
            width: 100%;
            height: 514px;
        }
        .style3
        {
            color: #000000;
            width: 81px;
        }
        .style4
        {
    }
        .style5
        {
        }
        .style6
        {
            color: #000000;
        }
        .style9
        {
            color: #000000;
        }
        .style10
        {
            height: 131px;
        }
        .style11
        {
            width: 81px;
            height: 24px;
        }
        .style12
        {
            height: 24px;
        }
        .style13
        {
        }
        .style14
        {
        }
        .style15
        {
        }
        .style16
        {
            height: 48px;
        }
        .style17
        {
            width: 90px;
            color: #000000;
            height: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                            <table class="style2">
                                <tr>
                                    <td class="style3">
                                        Journal No
                                    </td>
                                    <td class="style15">
                                        <asp:TextBox ID="txtJournaNo" runat="server" style="margin-left: 0px"></asp:TextBox>
                                    </td>
                                    <td class="style13">
                                        <asp:Button ID="cmdPrint" runat="server" Text="Print Jv" />
                                    </td>
                                    <td class="style6">
                                        Date</td>
                                    <td>
                                        <asp:TextBox ID="dtpReceiptDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style11">
                                        <asp:Label ID="Label9" runat="server" Text="GL A\C"></asp:Label>
                                    </td>
                                    <td class="style12" colspan="2">
                                        <asp:DropDownList ID="cboAccno" runat="server" Height="19px" Width="78px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;<asp:Button ID="Button2" runat="server" Text="F" />
                                    </td>
                                    <td class="style12">
                                        <asp:Label ID="Label1" runat="server" Text="DR"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtDr" runat="server" style="font-weight: 700" 
                                            AutoPostBack="True" ontextchanged="txtDr_TextChanged">0</asp:TextBox>
                                    </td>
                                    <td class="style12">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="style14">
                                        &nbsp;</td>
                                    <td class="style4" colspan="2">
                                        <asp:TextBox ID="txtAccNames" runat="server" Width="299px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="CR"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCr" runat="server" style="font-weight: 700" 
                                            AutoPostBack="True" ontextchanged="txtCr_TextChanged">0</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label6" runat="server" Text="Member No"></asp:Label>
                                    </td>
                                    <td class="style15">
                                        <asp:TextBox ID="txtMemberNo" runat="server" AutoPostBack="True" Width="118px" 
                                            ontextchanged="txtMemberNo_TextChanged"  ></asp:TextBox>
                                    </td>
                                    <td class="style13" colspan="3">
                                        <asp:Label ID="lblfullnames" runat="server" ForeColor="#6600FF"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label7" runat="server" Text="ShareType"></asp:Label>
                                    </td>
                                    <td class="style15">
                                        <asp:DropDownList ID="cboShareType" runat="server" AutoPostBack="True" 
                                            Height="22px" Width="117px" 
                                            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style13">
                                        <asp:Label ID="lblShareType" runat="server" ForeColor="#6600FF"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        <asp:Label ID="Label3" runat="server" Text="Total DR"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalDr" runat="server" Width="121px" 
                                            style="font-weight: 700">0</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label8" runat="server" Text="Loanno"></asp:Label>
                                    </td>
                                    <td class="style15">
                                        <asp:DropDownList ID="cboLoanno" runat="server" 
                                            onselectedindexchanged="cboLoanno_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style13">
                                        <asp:Label ID="lblLoantype" runat="server" ForeColor="#6600FF"></asp:Label>
                                    </td>
                                    <td class="style6">
                                        <asp:Label ID="Label4" runat="server" Text="Total CR"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalCr" runat="server" Height="22px" Width="121px" 
                                            style="font-weight: 700">0</asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        </td>
                                    <td class="style17">
                                        <asp:Label ID="Label5" runat="server" Text="Journal Narration"></asp:Label>
                                    </td>
                                    <td class="style16" colspan="3">
                                        <asp:TextBox ID="rtpNarration" runat="server" TextMode="MultiLine" 
                                            Width="314px"></asp:TextBox>
                                    </td>
                                    <td class="style16">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="style14">
                                        &nbsp;</td>
                                    <td class="style9" colspan="4">
                                        <asp:Button ID="cmdAdd" runat="server" Text="ADD" 
                                            style="float: none" Width="80px" onclick="cmdAdd_Click" />&nbsp;
&nbsp; <asp:Button ID="cmdRemove" runat="server" Text="REMOVE" onclick="Button5_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="cmdClear" runat="server" Text="REMOVE ALL" 
                                            onclick="Button6_Click" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style5" colspan="5">
                                        <asp:Button ID="cmdUnpostedJV" runat="server" Text="UnPosted Journals" 
                                            onclick="cmdUnpostedJV_Click" />
                                    &nbsp;
                                        &nbsp; 
                                    &nbsp;
                                        &nbsp; <asp:Button ID="cmdRemoveu" runat="server" Text="REMOVE(unposted)" Width="161px" onclick="Button7_Click" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style10" colspan="6">
                                        <asp:GridView ID="GridView1" runat="server" Font-Size="8pt" 
                                                PageSize="100" 
                                                    style="margin-top: 0px" Width="709px" BackColor="White" 
                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                            GridLines="Vertical" 
                                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="AtmSelector" runat="server" AutoPostBack="True"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                    <sortedascendingcellstyle backcolor="#F1F1F1" />
                                                    <sortedascendingheaderstyle backcolor="#0000A9" />
                                                    <sorteddescendingcellstyle backcolor="#CAC9C9" />
                                                    <sorteddescendingheaderstyle backcolor="#000065" />
                                                </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style14" colspan="6">
                                        <asp:Button ID="cmdProcessJournal" runat="server" Text="Process Journal" 
                                            onclick="Button9_Click" />
                                    &nbsp;&nbsp;
                                        <asp:Button ID="cmdPostJournal" runat="server" Text="Post Journal" 
                                            onclick="cmdPostJournal_Click" Width="114px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
