<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="GroupMembers.aspx.cs" Inherits="USACBOSA.SysAdmin.GroupMembers" %>
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
    &nbsp;<asp:Label ID="Label5" runat="server" Text="Group Members Registration" Font-Bold="True" 
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
                    ID="txtCompanyCode" runat="server" Width="138px" ReadOnly="True" 
                    BackColor="#CCCCCC"></asp:TextBox>
            </td>
            <td class="style46">
                <asp:Label ID="Label7" runat="server" Text="Business Name:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style5" colspan="3">
                <asp:TextBox 
                    ID="txtCompanyName" runat="server" Width="258px" ReadOnly="True" 
                    BackColor="#CCCCCC"></asp:TextBox>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style42">
                <asp:Label ID="Label12" runat="server" Text="Member No.:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
                <br />
            </td>
            <td class="style44">
                
                <asp:TextBox ID="txtmemberno" runat="server" Width="117px"></asp:TextBox>
                
                <asp:Button ID="btnFindMember" runat="server" Height="21px" 
                    onclick="btnFindMember_Click" Text="F" />
                
            </td>
            <td class="style47">
                <asp:Label ID="Label15" runat="server" Text="Names:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style34" colspan="2">
                
                <asp:TextBox ID="txtnames" runat="server" style="margin-left: 0px" 
                    Width="258px"></asp:TextBox>
                
            </td>
            <td class="style14">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style42">
                <asp:Label ID="Label8" runat="server" Text="Mobile No:" 
                    style="float: right; color: #000000;" Font-Bold="False"></asp:Label>
            </td>
            <td class="style44">
                <asp:TextBox ID="txtmobileno" runat="server" Width="138px"></asp:TextBox>
            </td>
            <td class="style47">
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" ForeColor="Black" 
                                style="float: right" Text="ID NO.:"></asp:Label>
            </td>
            <td class="style14">
                
                <asp:TextBox ID="txtidno" runat="server" Width="136px"></asp:TextBox>
                        </td>
            <td class="style36">
                </td>
            <td class="style14">
                </td>
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
                <asp:Button ID="btnSave" runat="server" Text="Save" 
                    Font-Bold="True" Width="73px" onclick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                    Width="72px" Font-Bold="True" onclick="btnUpdate_Click" />
                &nbsp;
                </td>
            <td class="style37">
                &nbsp;</td>
        </tr>
        </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="15" 
        Width="100%" onpageindexchanging="GridView1_PageIndexChanging" 
        Height="174px">
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
