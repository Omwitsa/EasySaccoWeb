<%@ Page Title="" Language="C#" MasterPageFile="~/HR_Admin/HR_Admin.Master" AutoEventWireup="true" CodeBehind="CompanySetup.aspx.cs" Inherits="USACBOSA.HR_Admin.CompanySetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 159px;
            height: 24px;
        }
        .style5
        {
            height: 24px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;<asp:Label ID="Label5" runat="server" Text="Group / Company Registration" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />

    <table style="width:100%;">
        <tr>
            <td class="style41">
                <asp:Label ID="Label6" runat="server" Text="Business Code:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>&nbsp;<br />
            </td>
            <td class="style4">
                <asp:TextBox 
                    ID="txtCompanyCode" runat="server" Width="138px" BackColor="#CCCCFF" 
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style46">
                <asp:Label ID="Label7" runat="server" Text="Business Name:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style5" colspan="3">
                <asp:TextBox 
                    ID="txtCompanyName" runat="server" Width="258px"></asp:TextBox>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style42">
                <asp:Label ID="Label12" runat="server" Text="Physical Location:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
                <br />
            </td>
            <td class="style44">
                
                <asp:TextBox ID="TextBox1" runat="server" Width="138px"></asp:TextBox>
                
            </td>
            <td class="style47">
                <asp:Label ID="Label15" runat="server" Text="Contact Person:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style34" colspan="2">
                
                <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 0px" 
                    Width="258px"></asp:TextBox>
                
            </td>
            <td class="style14">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style42">
                <asp:Label ID="Label8" runat="server" Text="Telephone No:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style44">
                <asp:TextBox ID="txtTelephone" runat="server" Width="138px"></asp:TextBox>
            </td>
            <td class="style47">
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" ForeColor="Black" 
                                style="float: right" Text="Years in Business:"></asp:Label>
            </td>
            <td class="style14">
                
                <asp:TextBox ID="txtNoYears" runat="server" Width="136px"></asp:TextBox>
                        </td>
            <td class="style36">
                </td>
            <td class="style14">
                </td>
        </tr>
        <tr>
            <td class="style42">
                <asp:Label ID="Label10" runat="server" Text="No of Employees:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style44">
                <asp:TextBox ID="txtNoUsers" runat="server" Width="138px"></asp:TextBox>
            </td>
            <td class="style47">
                <asp:Label ID="Label11" runat="server" Text="Business Status:" 
                    style="float: right; color: #000000;" ForeColor="Blue" Font-Bold="False"></asp:Label></td>
            <td class="style34">
                
                <asp:DropDownList ID="cboBusinesstatus" runat="server" Height="23px" 
                    Width="139px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Company Limited</asp:ListItem>
                    <asp:ListItem>Group</asp:ListItem>
                    <asp:ListItem>Partnership</asp:ListItem>
                    <asp:ListItem>Sole Proprietorship</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td class="style36">
                &nbsp;</td>
            <td class="style14">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style43">
                            <asp:Label ID="Label14" runat="server" Text="Email Address:" 
                                style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style45">
                <asp:TextBox ID="txtEmailAddress" runat="server" Width="138px"></asp:TextBox>
            </td>
            <td class="style48">
                            <asp:Label ID="Label9" runat="server" Text="Postal Address:" 
                                style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style35" colspan="2">
                <asp:TextBox ID="txtAddress" runat="server" Width="214px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="style43">
                            &nbsp;</td>
            <td class="style12" colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="Button1_Click" 
                    Font-Bold="True" Width="73px" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Update" 
                    Width="72px" Font-Bold="True" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddMembers" runat="server" Font-Bold="True" 
                    ForeColor="Black" onclick="btnAddMembers_Click" style="margin-left: 0px" 
                    Text="ADD MEMBERS" />
            </td>
            <td class="style37">
                &nbsp;</td>
        </tr>
        </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView" runat="server" 
             onselectedindexchanged="GridView_SelectedIndexChanged1" Width="904px" 
                   BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                   CellPadding="4">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
             </asp:GridView>
             <style type="text/css">
                 .style12
                 {
        height: 28px;
        }
                 .style14
                 {
                     height: 10px;
                 }
                 .style34
                 {
                     height: 10px;
                     }
                 .style35
                 {
                     height: 28px;
                     }
                 .style36
                 {
                     height: 10px;
                     width: 201px;
                 }
                 .style37
                 {
                     height: 28px;
                     width: 201px;
                 }
                 .style41
                 {
                     height: 24px;
                     width: 125px;
                 }
                 .style42
                 {
                     height: 10px;
                     width: 125px;
                 }
                 .style43
                 {
                     height: 28px;
                     width: 125px;
                 }
                 .style44
                 {
                     height: 10px;
                     width: 159px;
                 }
                 .style45
                 {
                     height: 28px;
                     width: 159px;
                 }
                 .style46
                 {
                     height: 24px;
                     width: 126px;
                 }
                 .style47
                 {
                     height: 10px;
                     width: 126px;
                 }
                 .style48
                 {
                     height: 28px;
                     width: 126px;
                 }
    </style>
    </asp:Content>
   