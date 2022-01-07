<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="SystemUser.aspx.cs" Inherits="USACBOSA.Setup.SystemUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 262px;
        }
        .style7
        {
        }
        .style8
        {
            width: 262px;
            height: 26px;
        }
        .style9
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label5" runat="server" Text="System Users" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        
        <table style="width:100%;">
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
        <asp:Label ID="Label6" runat="server" Text="User Login ID"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server" Width="133px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    </td>
                <td class="style9">
                    <asp:Label ID="Label9" runat="server" Text="User Names"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtName" runat="server" Width="315px"></asp:TextBox>
                </td>
                <td class="style9">
                    </td>
                <td class="style9">
                    </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Label ID="Label7" runat="server" Text="User Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="132px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    </td>
                <td class="style9">
                    <asp:Label ID="Label13" runat="server" Text="Confirm Password"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtConfirm" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td class="style9">
                    </td>
                <td class="style9">
                    </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Label ID="Label8" runat="server" Text="User Group"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboUser" runat="server" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Label ID="Label10" runat="server" Text="Branch"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cbobranch" runat="server" 
                        onselectedindexchanged="DropDownList3_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboStatus" runat="server">
                    </asp:DropDownList>
                    <asp:CheckBox ID="Chksuper" runat="server" Text="Super User" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Label ID="Label12" runat="server" Text="Teller Account"></asp:Label>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkIsTeller" runat="server" Text="Is Teller" 
                        oncheckedchanged="chkIsTeller_CheckedChanged" AutoPostBack="True" />
                </td>
                <td>
                    <asp:TextBox ID="txtTellerGl" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;<asp:ImageButton 
                        ID="ImageButton1" runat="server" ImageUrl="~/Images/searchbutton.PNG" 
                        onclick="ImageButton1_Click" />
&nbsp;<asp:TextBox ID="lblglteller" runat="server" Width="207px" 
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                        Text="Update" />
&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView" runat="server" 
             onselectedindexchanged="GridView_SelectedIndexChanged1" Width="876px" 
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
</asp:Content>
