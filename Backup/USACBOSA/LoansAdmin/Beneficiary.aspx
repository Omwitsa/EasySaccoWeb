<%@ Page Title="" Language="C#" MasterPageFile="~/LoansAdmin/LoansAdmin.Master" AutoEventWireup="true" CodeBehind="Beneficiary.aspx.cs" Inherits="USACBOSA.LoansAdmin.Beneficiary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            height: 38px;
        }
        .style6
        {
        }
        .style8
        {
            width: 147px;
        }
        .style9
        {
            width: 115px;
        }
        .style10
        {
            width: 127px;
        }
        </style>
        <script type="text/javascript">

            function checkDate(sender, args) {
                if (sender._selectedDate < new Date()) {
//                    alert("You cannot select a day earlier than today!");
                    sender._selectedDate = new Date();
                    // set the date back to the current date
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                }
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Label5" runat="server" Text="Beneficiary / Kin Details" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
&nbsp;<table style="width:100%;">
        <tr>
            <td class="style9">
        <asp:Label ID="Label6" runat="server" Text="Member No.:" style="float: right" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td class="style8">
    <asp:TextBox ID="txtMemberNo" runat="server" ontextchanged="TextBox1_TextChanged" 
                    Width="106px" AutoPostBack="True"></asp:TextBox>
                <asp:ImageButton ID="imgSearchMember" runat="server" 
                    ImageUrl="~/Images/searchbutton.PNG" onclick="imgSearchMember_Click" 
                    Width="17px" Visible="False" />
                </td>
            <td colspan="2">
                <asp:TextBox ID="txtNames" runat="server" Width="288px" ReadOnly="True"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label9" runat="server" Text="Kin No.:" style="float: right" 
                    Width="50px" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                    ForeColor="Black"></asp:Label>
            </td>
            <td class="style8">
                    <asp:TextBox ID="txtKinNo" runat="server" Width="125px" ReadOnly="True"></asp:TextBox>
                </td>
            <td class="style10">
                <asp:Label ID="Label10" runat="server" Text="Kin Names:" style="float: right" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="txtKinNames" runat="server" Width="191px"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                    <asp:Label ID="Label11" runat="server" Text="Relationship:" 
                        style="float: right" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                        ForeColor="Black"></asp:Label>
                </td>
            <td class="style8">
                    <asp:DropDownList ID="cboRelationship" runat="server" Height="22px" 
                        Width="123px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>SPOUSE</asp:ListItem>
                        <asp:ListItem>CHILD</asp:ListItem>
                        <asp:ListItem>SIBLING</asp:ListItem>
                        <asp:ListItem>PARENT</asp:ListItem>
                        <asp:ListItem>IN-LAW</asp:ListItem>
                        <asp:ListItem>FRIEND</asp:ListItem>
                        <asp:ListItem>OTHER</asp:ListItem>
                    </asp:DropDownList>
                </td>
            <td class="style10">
                <asp:Label ID="Label12" runat="server" Text="Address:" style="float: right" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="txtAddress" runat="server" Width="190px"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                    <asp:Label ID="Label13" runat="server" Text="Tel No.:" style="float: right" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td class="style8">
                    <asp:TextBox ID="txtHomeTel" runat="server" Width="121px" Height="22px"></asp:TextBox>
                </td>
            <td class="style10">
                    <asp:Label ID="Label14" runat="server" Text="Office Tel No.:" 
                        style="float: right" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                        ForeColor="Black"></asp:Label>
                </td>
            <td>
                    <asp:TextBox ID="txtOfficeTel" runat="server" Width="118px"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                    <asp:Label ID="Label15" runat="server" Text="Sign Date:" style="float: right" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td class="style8">
                    <asp:TextBox ID="txtSignDate" runat="server" Width="121px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="dd-MM-yyyy" PopupButtonID="imgSignDate" runat="server" TargetControlID="txtSignDate">
                </asp:CalendarExtender>
                      <asp:ImageButton ID="imgSignDate" runat="server" 
                    ImageUrl="~/Images/calendar.png" />
            </td>
            <td class="style10">
                <asp:Label ID="Label16" runat="server" Text="Birth Cert/ID No.:" 
                    style="float: right; color: #000000;" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="txtIdNumber" runat="server" Width="190px"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label17" runat="server" Text="Percentage:" style="float: right" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td class="style8">
                    <asp:TextBox ID="txtPercentage" runat="server" Width="121px" Height="22px"></asp:TextBox>
                </td>
            <td class="style10">
                    <asp:Label ID="Label18" runat="server" Text="Comments:" style="float: right" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black"></asp:Label>
            </td>
            <td rowspan="2">
                    <asp:TextBox ID="txtComments" runat="server" Width="191px" Height="38px" 
                        TextMode="MultiLine"></asp:TextBox>
                 </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label19" runat="server" Text="Witnessed By:" 
                    style="float: right" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" 
                    ForeColor="Black"></asp:Label>
            </td>
            <td class="style6" colspan="2">
                    <asp:TextBox ID="txtWitness" runat="server" Width="179px"></asp:TextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style8">
                <asp:Button ID="btnAddKin" runat="server" onclick="btnAddKin_Click" 
                    style="float: left" Text="Add Kin" Width="79px" Font-Bold="True" 
                    Font-Size="10pt" ForeColor="Black" />
             </td>
            <td class="style10">
             <asp:Button ID="btnSave" runat="server" Text="Save" 
                        Width="88px" onclick="Button1_Click1" Font-Bold="True" 
                    Font-Size="10pt" ForeColor="Black" />
                </td>
            <td>
                    <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                        Text="Update" Width="87px" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="Black" />
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
&nbsp;<br />
<hr style="color: Maroon" />

     <div style="width: 90%; height: 302px; overflow: scroll; margin-left: 45px;" 
        class="style5">
                                        
                     <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#E7E7FF" BorderStyle="None" 
                         BorderWidth="1px" CellPadding="3" 
                                            Font-Size="8pt" Visible="False" Width="892px" 
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
                                        </asp:GridView>                     </div>
     </asp:Content>
