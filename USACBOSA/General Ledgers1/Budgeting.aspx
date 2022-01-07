<%@ Page Title="" Language="C#" MasterPageFile="~/Bosa.Master" AutoEventWireup="true" CodeBehind="Budgeting.aspx.cs" Inherits="USACBOSA.General_Ledgers.Budgeting" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
         <script type="text/javascript">

             function checkDate(sender, args) {
                 if (sender._selectedDate < new Date()) {
                     alert("You cannot select a day earlier than today!");
                     sender._selectedDate = new Date();
                     // set the date back to the current date
                     sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                 }
             }
    </script>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="GL Contra Account"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="LBLGLCONTRA" runat="server"></asp:TextBox>
            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/Images/searchbuttonIV.PNG" onclick="ImageButton2_Click" />
            </td>
            <td class="style1">
                <asp:TextBox ID="glName1" runat="server" Width="239px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Budget Year"></asp:Label>
            </td>
            <td>
            <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="yyyy" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtBudgetYear">
                </asp:CalendarExtender>
                <asp:TextBox ID="txtBudgetYear" runat="server" Width="78px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/Images/calendar.png" />
            </td>
            <td>
                <asp:RadioButton ID="optSpread" runat="server" Text="Spread Amount" 
                    AutoPostBack="True" Checked="True" 
                    oncheckedchanged="optSpread_CheckedChanged" />
&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="optFixed" runat="server" Text="Fixed Amount" 
                    AutoPostBack="True" oncheckedchanged="optFixed_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Spread Amount"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtbudgettedamount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Actuals"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="lblactuals" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                    Text="Update" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
            <div style="width: 100%; height: 185px; overflow: scroll" class="style5">
                                        
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                            Font-Size="8pt" Visible="False" Width="728px" 
                                            AutoGenerateSelectButton="True" GridLines="Horizontal" 
                                            onselectedindexchanged="GridView1_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
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
                </div>
                </td>
        </tr>
    </table>
    
</asp:Content>
