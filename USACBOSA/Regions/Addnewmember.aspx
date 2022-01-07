<%@ Page Title="" Language="C#" MasterPageFile="~/Branches.Master" AutoEventWireup="true" CodeBehind="Addnewmember.aspx.cs" Inherits="USACBOSA.Regions.Addnewmember" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        .style29
        {
        }
        .style30
        {
        }
        .style31
        {
        }
        .style32
        {
            height: 26px;
        }
        .style33
        {
            width: 157px;
            height: 26px;
        }
        .style34
        {
            width: 129px;
            height: 26px;
        }
        .style35
        {
        }
        </style>
         <script type="text/javascript">

             function checkDate(sender, args) {
                 if (sender._selectedDate > new Date()) {
                     alert("You cannot select a day earlier than today!");
                     sender._selectedDate = new Date();
                     //                      set the date back to the current date
                     sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                 }
             }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table style="width:100%;">
            <tr>
                <td>
    <asp:Label ID="Label5" runat="server" Text="Membership Registration" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                <asp:Label ID="Label11" runat="server" Text="Reg Date" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699" 
                        Visible="False"></asp:Label>
                                </td>
                <td>
    <asp:TextBox ID="txtRegDate" runat="server" Width="128px" Font-Size="8pt" TabIndex="10"></asp:TextBox>
         <asp:CalendarExtender ID="txtRegDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtRegDate">
                            </asp:CalendarExtender>
    
    
    
    <asp:ImageButton 
                                ID="ImageButton1" runat="server" 
                        ImageUrl="~/Images/calendar.png" />
                            </td>
            </tr>
    </table>
        <hr style="color: Maroon; height: 2px;" />
        <table style="width:100%;">
            <tr>
                <td>
        <asp:Label ID="Label6" runat="server" Text="Member Type" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
                    <asp:DropDownList ID="cboMemberType" runat="server" AutoPostBack="True" 
                        Height="22px" onselectedindexchanged="cboMemberType_SelectedIndexChanged" 
                        Width="110px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Normal Member</asp:ListItem>
                        <asp:ListItem>Junior Member</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td class="style30">
                    <asp:Label ID="Label29" runat="server" Text="Member Category"></asp:Label>
                                </td>
                <td>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                    Width="110px" ontextchanged="DropDownList1_TextChanged" 
                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                        TabIndex="1" Visible="False">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>County</asp:ListItem>
        <asp:ListItem>Diaspora</asp:ListItem>
        <asp:ListItem>Staff</asp:ListItem>
    </asp:DropDownList>
                            <asp:TextBox ID="txtMemberNumber" runat="server" AutoPostBack="True" 
                        Height="22px" ontextchanged="txtMemberNumber_TextChanged" Width="110px"></asp:TextBox>
                            </td>
                <td colspan="2">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label18" runat="server" Text=" County" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699" 
                        style="color: #666699"></asp:Label>
                                </td>
                <td class="style35">
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                    Width="110px" ontextchanged="DropDownList3_TextChanged" 
                                    onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                        TabIndex="2">
    </asp:DropDownList>
                                </td>
                <td class="style30">
    <asp:Label ID="Label7" runat="server" Text="Member Number." Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td>
                            <asp:TextBox ID="txtMemberNo" runat="server" Width="110px" AutoPostBack="True" 
                                Font-Size="8pt" ontextchanged="txtMemberNo_TextChanged" 
                                ondatabinding="txtMemberNo_DataBinding" Height="22px" TabIndex="3" 
                                ReadOnly="True"></asp:TextBox>
                            </td>
                <td colspan="2" rowspan="6">
                               <asp:Image ID="imgPhoto" runat="server" Height="174px" 
                    ImageUrl="~/Images/photo.jpg" Width="191px" style="margin-left: 0px" />
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label8" runat="server" Text="Surname" Font-Bold="False" Font-Names="Aparajita" 
                        Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
    <asp:TextBox ID="txtSurname" runat="server" Width="110px" Font-Size="8pt" TabIndex="4" 
                        Height="22px"></asp:TextBox>
                            </td>
                <td class="style30">
                            <asp:Label ID="Label9" runat="server" Text="Other Names" 
                        Font-Bold="False" Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td>
    <asp:TextBox ID="txtOtherNames" runat="server" Width="174px" Font-Size="8pt" Font-Bold="True" 
                        Height="21px" TabIndex="5"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label12" runat="server" Text="Gender" Font-Bold="False" Font-Names="Aparajita" 
                        Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                Width="76px" TabIndex="6">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>MALE</asp:ListItem>
        <asp:ListItem>FEMALE</asp:ListItem>
        <asp:ListItem>OTHER</asp:ListItem>
    </asp:DropDownList>
                            </td>
                <td class="style30">
    <asp:Label ID="Label19" runat="server" Text="Station" Font-Bold="False" Font-Names="Aparajita" 
                        Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td>
                    <asp:DropDownList ID="cboStation" runat="server" Height="23px" Width="110px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label10" runat="server" Text="ID Number." Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
    <asp:TextBox ID="txtIdNo" runat="server" Width="110px" AutoPostBack="True" Font-Size="8pt" 
                        TabIndex="7" Height="22px"></asp:TextBox>
                            </td>
                <td class="style30">
    <asp:Label ID="Label14" runat="server" Text="Phone Number." Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td>
    <asp:TextBox ID="txtPhoneNo" runat="server" Width="110px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="8" Height="22px"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="Label15" runat="server" Text="Date of Birth" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
                            <asp:TextBox ID="txtDob" runat="server" Width="92px" AutoPostBack="True" 
                                Font-Size="8pt" TabIndex="15" ontextchanged="txtDob_TextChanged" 
                        Height="22px"></asp:TextBox>
                                
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/calendar.png" Width="16px" 
                                    onclick="ImageButton2_Click" />
                            </td>
                <td class="style30">
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Aparajita" 
                        Font-Size="Large" ForeColor="#666699" Text="Age"></asp:Label>
                                </td>
                <td>
                                    <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="dd-MM-yyyy" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtDob">
                </asp:CalendarExtender>
                    <asp:TextBox ID="txtAge" runat="server" TabIndex="16" Width="70px" Font-Bold="True" 
                                ReadOnly="True" Height="22px"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label21" runat="server" Text="Postal Address" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35" colspan="3">
    <asp:TextBox ID="txtPostalAddress" runat="server" Width="178px" 
                        Font-Size="8pt" TabIndex="12" Height="22px"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="style32">
    <asp:Label ID="Label13" runat="server" Text="Land-Line" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style33">
    <asp:TextBox ID="txtLandLine" runat="server" Width="115px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="14" Height="22px"></asp:TextBox>
                            </td>
                <td class="style34">
                                <asp:Label ID="Label24" runat="server" ForeColor="#3399FF" style="float: none" 
                                    Text="Marital Status" Font-Bold="False" Font-Italic="True" 
                        Font-Names="Aparajita" Font-Size="Large"></asp:Label>
                            </td>
                <td class="style32">
                                <asp:RadioButton ID="rdoMarried" runat="server" 
                    Text="Married" AutoPostBack="True" 
                    oncheckedchanged="rdoMarried_CheckedChanged" TabIndex="19" />
                            &nbsp;
    <asp:RadioButton ID="rdoSingle" runat="server" Text="Single" AutoPostBack="True" 
                                    oncheckedchanged="rdoSingle_CheckedChanged" TabIndex="20" />
                            &nbsp;<asp:RadioButton ID="rdoOthers" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="rdoOthers_CheckedChanged" Text="Others" />
                            </td>
                <td class="style32">
                    Member Photo</td>
                <td class="style32">
                </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label17" runat="server" Text="Pin Number." 
                        Font-Bold="False" Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699" 
                                    Visible="False"></asp:Label>
                                </td>
                <td class="style35">
                                <asp:TextBox ID="txtPinNo" runat="server" Width="137px" AutoPostBack="True" 
                                    Font-Size="8pt" TabIndex="9" Visible="False" Height="22px"></asp:TextBox>
                            </td>
                <td class="style30">
    <asp:Label ID="Label20" runat="server" Text="Email Address" Font-Bold="False" 
                        Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="157px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="11"></asp:TextBox>
                            </td>
                <td>
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                            <asp:Label ID="Label23" runat="server" Text="Company Code" 
                        Font-Bold="False" Font-Names="Aparajita" Font-Size="Large" ForeColor="#666699"></asp:Label>
                                </td>
                <td class="style35">
                                <asp:DropDownList ID="cboCompanyCode" runat="server" Height="21px" Width="90px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="cboCompanyCode_SelectedIndexChanged" 
                        TabIndex="17">
                                </asp:DropDownList>
                            </td>
                <td class="style30" colspan="2">
                                <asp:TextBox ID="txtCompany" runat="server" Width="204px" 
                        ReadOnly="True"></asp:TextBox>
                            </td>
                <td rowspan="4">
&nbsp;<asp:Image ID="imgPhoto1" runat="server" Height="78px" 
                    ImageUrl="~/Images/photo.jpg" Width="192px" style="margin-left: 0px" />
                            </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Aparajita" 
                        Font-Size="Large" ForeColor="#666699" Text="Agent \ Referee"></asp:Label>
                </td>
                <td class="style35">
                    <asp:DropDownList ID="cboAgentId" runat="server" AutoPostBack="True" 
                        Height="20px" onselectedindexchanged="cboAgentId_SelectedIndexChanged" 
                        TabIndex="18" Width="91px">
                    </asp:DropDownList>
                </td>
                <td class="style30" colspan="2">
                    <asp:TextBox ID="txtAgentNames" runat="server" ReadOnly="True" Width="203px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label25" runat="server" ForeColor="#3399FF" style="float: right" 
                                    Text="Status" Font-Bold="False" Font-Italic="True" 
                        Font-Names="Aparajita" Font-Size="Large" Height="17px"></asp:Label>
                            </td>
                <td class="style31" colspan="2">
    <asp:RadioButton ID="rdoActive" runat="server" Text="Active" AutoPostBack="True" 
                                    oncheckedchanged="rdoActive_CheckedChanged" TabIndex="21" 
                        Checked="True" />
                            &nbsp;
    <asp:RadioButton ID="rdoWithdrawn" runat="server" Text="Withdrawn" AutoPostBack="True" 
                                    oncheckedchanged="rdoWithdrawn_CheckedChanged" TabIndex="22" />
                            &nbsp;
    <asp:RadioButton ID="rdoDeceased" runat="server" Text="Deceased" AutoPostBack="True" 
                                    oncheckedchanged="rdoDeceased_CheckedChanged" TabIndex="23" />
                            </td>
                <td>
                    <asp:RadioButton ID="rdoDormant" runat="server" AutoPostBack="True" 
                        oncheckedchanged="rdoDormant_CheckedChanged" Text="Dormant" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                                        <asp:Label ID="Label26" runat="server" Text="Search by:" 
                        Font-Bold="False" Font-Names="Aparajita" Font-Size="Large"></asp:Label>
                </td>
                <td class="style29" colspan="3">
                                        <asp:DropDownList ID="DropDownList5" runat="server" 
                        Height="18px" Width="136px" TabIndex="24">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Member No</asp:ListItem>
                                            <asp:ListItem>Names</asp:ListItem>
                                            <asp:ListItem>ID Number</asp:ListItem>
                                        </asp:DropDownList>
&nbsp;&nbsp; <asp:TextBox ID="txtSearch" runat="server" TabIndex="25" Width="114px"></asp:TextBox>
                    <asp:ImageButton ID="btnSearch" runat="server" Height="19px" ImageUrl="~/Images/searchbutton.PNG" 
                                            onclick="btnSearch_Click" Width="26px" TabIndex="26" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style29" colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                                Width="72px" Font-Bold="True" Font-Size="12pt" TabIndex="27" 
                        style="height: 29px" />
                            &nbsp;
                    <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click1" 
                        Text="Update" Font-Bold="True" Font-Size="12pt" TabIndex="28" />
                </td>
                <td>
                                        <asp:Button ID="btnBeneficiary" runat="server" Text="Beneficiary" 
                                            ForeColor="#990099" onclick="btnBeneficiary_Click1" 
                        TabIndex="29" />
                                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" 
                                            Visible="False" />
                                    </td>
                <td>
                    Member Signature..</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                <div class="style5" style="width: 100%; height: 118px; overflow: scroll">
                    <asp:GridView ID="GridView2" runat="server" Width="893px" 
                        AutoGenerateSelectButton="True" Font-Size="8pt" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#009933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
    </table>
        <hr style="color: Maroon" />
         <div class="style5" style="width: 100%; height: 147px; overflow: scroll">
         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="15" 
        Width="100%" onpageindexchanging="GridView1_PageIndexChanging" 
        Height="227px" 
    onselectedindexchanging="GridView1_SelectedIndexChanging">
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
                    </asp:Content>
