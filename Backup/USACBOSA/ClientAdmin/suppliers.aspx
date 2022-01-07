<%@ Page Title="" Language="C#" MasterPageFile="~/ClientAdmin/ClientAdmin.Master" AutoEventWireup="true" CodeBehind="suppliers.aspx.cs" Inherits="USACBOSA.ClientAdmin.suppliers" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 106px;
        }
        .style2
        {
            height: 26px;
        }
        .style3
        {
            width: 106px;
            height: 26px;
        }
        .style5
        {
            width: 171px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Add Reference to jQuery at Google CDN --%>

<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>
  <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
<%-- Register the WebClientPrint script code --%>
    <asp:Label ID="Label5" runat="server" Text="Suppliers Registration" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label> 
        <hr style="color: Maroon" />
    <table style="width:100%;">
        <tr>
            <td>
                    <asp:Label ID="Label6" runat="server" Text="Supplier Code:" style="float: right" 
                        Width="116px"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtMemberNo" runat="server" Height="21px" Width="125px" 
                     AutoPostBack="True" ontextchanged="txtMemberNo_TextChanged"></asp:TextBox>
                    </td>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label11" runat="server" Text="Supplier Name:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="23px" Width="272px"></asp:TextBox>
                </td>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td class="style1">
                <asp:Label ID="Label27" runat="server" Text="Email Address"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label15" runat="server" Text="Contact Person:" 
                        style="float: right"></asp:Label>
                    </td>
            <td>
                    <asp:TextBox ID="txtChequeNo" runat="server" AutoPostBack="True" Width="125px" 
                        Height="23px"></asp:TextBox>
                    </td>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td class="style1">
                <asp:Label ID="Label28" runat="server" Text="Contact Title:"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                    <asp:Label ID="Label16" runat="server" Text="Phone Number:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style2">
                    <asp:TextBox ID="txtDistributedAmount" runat="server" Width="125px" ForeColor="#0033CC" 
                        Height="24px"></asp:TextBox>
                </td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style3">
                <asp:Label ID="Label29" runat="server" Text="Fax Number:"></asp:Label>
            </td>
            <td class="style2">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                    </td>
        </tr>
        <tr>
            <td class="style4">
                    <asp:Label ID="Label35" runat="server" Text="Supplier A/C(CR) :" 
                        style="float: right"></asp:Label>
                    &nbsp;</td>
            <td class="style5">
                    <asp:DropDownList ID="cboBankAC" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cboBankAC_SelectedIndexChanged" Height="17px" 
                        Width="128px">
                    </asp:DropDownList>
                </td>
            <td class="style38">
                <asp:Button ID="btnFindBank" runat="server" Font-Bold="True" 
                    onclick="btnFindBank_Click" Text="F" Height="25px" Width="32px" />
            </td>
            <td class="style2">
                    <asp:TextBox ID="txtBankAC" runat="server" Width="265px"></asp:TextBox>
            </td>
            <td class="style3">
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label42" runat="server" Text="Debt ACC(DR)"></asp:Label>
                    :&nbsp; </td>
            <td class="style5">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="17px" Width="127px" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:Button ID="Button2" runat="server" Height="24px" Text="F" 
                    Width="33px" />
            </td>
            <td class="style2">
                    <asp:TextBox ID="txtDRacc" runat="server" Height="23px" Width="264px"></asp:TextBox>
            </td>
            <td class="style3">
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                    <asp:Label ID="Label30" runat="server" Text="Narration:" 
                        style="float: right"></asp:Label>
                </td>
            <td class="style2">
                    <asp:TextBox ID="txtnarration" runat="server" Width="125px" ForeColor="#0033CC" 
                        Height="24px" TextMode="MultiLine"></asp:TextBox>
                </td>
            <td class="style2">
                    &nbsp;</td>
            <td class="style2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label19" runat="server" Text="Address:" 
                        style="float: right"></asp:Label>
                </td>
            <td colspan="4">
                    <asp:TextBox ID="txtGlAccountName" runat="server" Width="398px" 
                       Height="26px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
         
            <td colspan="6">
     
                    <asp:Button ID="Button1" runat="server" 
                        style="font-weight: 700; margin-left: 214px" Text="Add" Width="69px" 
                        Height="26px" onclick="Button1_Click1"  />
                    <asp:Button ID="btnAdd" runat="server" 
                        Text="Modify" Width="88px" Height="25px" 
                        style="font-weight: 700; margin-left: 34px;" onclick="btnAdd_Click1" />
                    <asp:Button ID="btnRemove" runat="server"
                        Text="Save" Width="86px" Height="25px" 
                        style="font-weight: 700; margin-left: 36px;" onclick="btnRemove_Click" />
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
