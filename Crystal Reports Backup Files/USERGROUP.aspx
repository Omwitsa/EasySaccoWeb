<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="USERGROUP.aspx.cs" Inherits="Easyma.SetUp.USERGROUP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style7
        {
            color: #000000;
        }
        .style8
        {
            text-align: center;
        }
        .style9
        {
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td colspan="2">
                <hr style="height: 5px" />
            </td>
        </tr>
        <tr>
            <td class="style9" colspan="2">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="#FF9900" Text="USERGROUPS"></asp:Label>
                        </td>
        </tr>
        <tr>
            <td class="style9">
                            <asp:Label ID="Label1" runat="server" Text="Group ID" CssClass="style7"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="TextBox2" runat="server" Width="297px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="style9">
                            <asp:Label ID="Label2" runat="server" Text="User Group Name" CssClass="style7"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="297px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label6" runat="server" CssClass="style7" 
                    style="font-size: large" Text="User Group's Rights"></asp:Label>
                <hr style="height: 5px" />
            </td>
        </tr>
        <tr>
            <td class="style9" colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="rowHover" ForeColor="#333333" GridLines="None" Height="205px" RowStyle-CssClass="rowHover" ShowFooter="True" Width="841px">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <FooterTemplate>
                                            <asp:Button ID="Button8" runat="server" onclick="Button1_Click" Text="SAVE" />
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="grdHeaderCheckBox(this);" Text="All" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id" HeaderText="ID" />
                                    <asp:BoundField DataField="menu" HeaderText="Menu" />
                                    <asp:BoundField DataField="Alias" HeaderText="Alias" />
                                    <asp:BoundField DataField="Enabled" HeaderText="Enabled" />
                                    <asp:BoundField DataField="RegDate" HeaderText="RegDate" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#333300" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                                    </td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Add" />
                            <asp:Button ID="Button2" runat="server" Text="Edit" />
                            <asp:Button ID="Button7" runat="server" onclick="Button7_Click" Text="Save" />
                            <asp:Button ID="Button5" runat="server" Text="Cancel" onclick="Button5_Click" />
                            <asp:Button ID="Button6" runat="server" Text="Close" onclick="Button6_Click" />
                                    </td>
        </tr>
        <tr>
            <td colspan="2">
                            &nbsp;</td>
        </tr>
        </table>
</asp:Content>
