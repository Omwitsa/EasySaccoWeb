<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="MemberRegistration.aspx.cs" Inherits="USACBOSA.SysAdmin.MemberRegistration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
         <style type="text/css">
             .style1
             {
                 height: 26px;
             }
         </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> <div class="style5"overflow: scroll">
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
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
        <hr style="color: Maroon; height: 2px;" />
        <table style="width:100%;">
            <tr>
                <td>
    <asp:Label ID="Label7" runat="server" Text="Member No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                                </td>
                <td>
                            <asp:TextBox ID="txtMemberNo" runat="server" Width="110px" AutoPostBack="True" 
                                Font-Size="8pt" Height="22px" TabIndex="3" 
                                ontextchanged="txtMemberNo_TextChanged"></asp:TextBox>
                                </td>
                <td>
    <asp:Label ID="Label10" runat="server" Text="ID No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                            </td>
                <td>
    <asp:TextBox ID="txtIdNo" runat="server" Width="110px" AutoPostBack="True" Font-Size="8pt" 
                        TabIndex="7" Height="22px" ontextchanged="txtIdNo_TextChanged"></asp:TextBox>
                            </td>
                <td>
                <asp:Label ID="Label11" runat="server" Text="Reg. Date:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float:right"></asp:Label>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtRegDate" runat="server" Width="100px" Font-Size="8pt" TabIndex="10" 
                        Wrap="False" Height="22px"></asp:TextBox>
         <asp:CalendarExtender ID="txtRegDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtRegDate">
                            </asp:CalendarExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/Images/calendar.png" />
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label8" runat="server" Text="Surname:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="Small" ForeColor="Black" style="float: right"></asp:Label>
                                </td>
                <td>
    <asp:TextBox ID="txtSurname" runat="server" Width="110px" Font-Size="8pt" TabIndex="4" 
                        Height="22px"></asp:TextBox>
                            </td>
                <td>
                            <asp:Label ID="Label9" runat="server" Text="O.Names:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                                style="float: right"></asp:Label>
                            </td>
                <td>
    <asp:TextBox ID="txtOtherNames" runat="server" Width="204px" Font-Size="8pt" Font-Bold="True" 
                        Height="22px" TabIndex="5"></asp:TextBox>
                            </td>
                <td rowspan="6" colspan="2">
                               <asp:Image ID="imgPhoto" runat="server" Height="148px" 
                    ImageUrl="~/Images/photo.jpg" Width="214px" style="margin-left: 0px" />
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label19" runat="server" Text="Station:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="Small" ForeColor="Black" style="float: right"></asp:Label>
                                </td>
                <td>
                    <asp:DropDownList ID="cboStation" runat="server" Height="23px" Width="110px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td>
    <asp:Label ID="Label12" runat="server" Text="Gender:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="Small" ForeColor="Black" style="float: right"></asp:Label>
                                </td>
                <td>
    <asp:DropDownList ID="DropDownList2" runat="server" Font-Size="8pt" Height="22px" 
                                Width="100px" TabIndex="6">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>MALE</asp:ListItem>
        <asp:ListItem>FEMALE</asp:ListItem>
        <asp:ListItem>OTHER</asp:ListItem>
    </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td class="style1">
    <asp:Label ID="Label14" runat="server" Text="Phone No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                                </td>
                <td class="style1">
    <asp:TextBox ID="txtPhoneNo" runat="server" Width="110px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="8" Height="22px" 
                        ontextchanged="txtPhoneNo_TextChanged" ></asp:TextBox>
                            </td>
                <td class="style1">
                <asp:Label ID="Label15" runat="server" Text="DOB:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                                </td>
                <td class="style1">
                            <asp:TextBox ID="txtDob" runat="server" Width="92px" AutoPostBack="True" 
                                Font-Size="8pt" TabIndex="15" ontextchanged="txtDob_TextChanged" 
                        Height="22px"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="dd-MM-yyyy" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtDob">
                </asp:CalendarExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/calendar.png" Width="16px" />
                            </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label13" runat="server" Text="Land-Line:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                                </td>
                <td>
    <asp:TextBox ID="txtLandLine" runat="server" Width="110px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="14" Height="22px"></asp:TextBox>
                            </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="Small" ForeColor="Black" Text="Age:" style="float: right"></asp:Label>
                            </td>
                <td>
                    <asp:TextBox ID="txtAge" runat="server" TabIndex="16" Width="92px" Font-Bold="True" 
                                ReadOnly="True" Height="22px"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label17" runat="server" Text="Pin No.:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                                    style="float: right"></asp:Label>
                            </td>
                <td>
                                <asp:TextBox ID="txtPinNo" runat="server" Width="112px" AutoPostBack="True" 
                                    Font-Size="8pt" TabIndex="9" Height="22px"></asp:TextBox>
                            </td>
                <td>
    <asp:Label ID="Label20" runat="server" Text="Email:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                                </td>
                <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="160px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="11" Height="22px"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label24" runat="server" ForeColor="Black" style="float: right" 
                                    Text="Marital Status:" Font-Bold="False" Font-Italic="False" 
                        Font-Names="Aparajita" Font-Size="Large"></asp:Label>
                                </td>
                <td>
                    <asp:DropDownList ID="cboMaritalStatus" runat="server" Height="22px" 
                        Width="110px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Married</asp:ListItem>
                        <asp:ListItem>Single</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                                <asp:Label ID="Label25" runat="server" ForeColor="Black" style="float: right" 
                                    Text="Status:" Font-Bold="False" Font-Italic="False" 
                        Font-Names="Tahoma" Font-Size="Small" Height="17px"></asp:Label>
                                </td>
                <td>
                    <asp:DropDownList ID="cboStatus" runat="server" Height="22px" Width="150px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Withdrawn</asp:ListItem>
                        <asp:ListItem>Deceased</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
    <asp:Label ID="Label21" runat="server" Text="Address:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" 
                        style="float: right"></asp:Label>
                            </td>
                <td>
    <asp:TextBox ID="txtPostalAddress" runat="server" Width="110px" 
                        Font-Size="8pt" TabIndex="12" Height="22px"></asp:TextBox>
                            </td>
                <td>
                    <asp:Label ID="Label29" runat="server" style="float: right" Text="Type:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="Small" ForeColor="Black"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboType" runat="server" Height="22px" Width="150px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Individual</asp:ListItem>
                        <asp:ListItem>Grouped</asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                        <asp:ListItem>Physically Challenged</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;<asp:Button ID="btnFindGroup" runat="server" Height="23px" 
                        Text="Find Group" onclick="btnFindGroup_Click" />
                </td>
                <td colspan="2">
&nbsp;
                &nbsp;
    &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="213px" />
                            </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="Small" ForeColor="Black" Text="Agent \ Referee:" 
                        style="float: right"></asp:Label>
                                </td>
                <td>
                    <asp:DropDownList ID="cboAgentId" runat="server" AutoPostBack="True" 
                        Height="20px" onselectedindexchanged="cboAgentId_SelectedIndexChanged" 
                        TabIndex="18" Width="91px">
                    </asp:DropDownList>
                            <asp:Button ID="btnFindAgent" runat="server" 
                        onclick="btnFindAgent_Click" Text="F" />
                            </td>
                <td colspan="2">
                    <asp:TextBox ID="txtAgentNames" runat="server" ReadOnly="True" Width="203px"></asp:TextBox>
                            </td>
                <td colspan="2">
                    <asp:TextBox ID="txtfindsearch" runat="server" Visible="False" Width="53px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnIncomeDetails" runat="server" Text="INCOME DETAILS" 
                        onclick="btnIncomeDetails_Click" Visible="False" />
                </td>
                <td colspan="2">
                                        <asp:Button ID="btnBeneficiary" runat="server" Text="BENEFICIARY DETAILS" 
                                            ForeColor="#990099" onclick="btnBeneficiary_Click1" 
                        TabIndex="29" Visible="False" />
                                        </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                                Width="80px" Font-Bold="True" Font-Size="12pt" TabIndex="27" 
                        Height="30px" style="float: right" />
                </td>
                <td>
&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click1" 
                        Text="Update" Font-Bold="True" Font-Size="12pt" TabIndex="28" 
                        Height="30px" Width="80px" />
                                        &nbsp;
                </td>
                <td>
                                        <asp:Label ID="Label26" runat="server" Text="Search by:" 
                        Font-Bold="False" Font-Names="Aparajita" Font-Size="Large" ForeColor="Black"></asp:Label>
                </td>
                <td colspan="3">
                                        <asp:DropDownList ID="cboSearch" runat="server" 
                        Height="18px" Width="136px" TabIndex="24" 
                                            onselectedindexchanged="cboSearch_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Member No</asp:ListItem>
                                            <asp:ListItem>Names</asp:ListItem>
                                            <asp:ListItem>ID Number</asp:ListItem>
                                        </asp:DropDownList>
&nbsp; <asp:TextBox ID="txtSearch" runat="server" TabIndex="25" Width="114px" ontextchanged="txtSearch_TextChanged"></asp:TextBox>
                    &nbsp;<asp:ImageButton ID="btnSearch" runat="server" Height="19px" ImageUrl="~/Images/searchbutton.PNG" 
                                            onclick="btnSearch_Click" Width="26px" TabIndex="26" />
                </td>
            </tr>
        </table>
        <hr style="color: Maroon" />
                 <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt" 
        onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="15" 
        Width="100%" onpageindexchanging="GridView2_PageIndexChanging" 
        Height="227px" onselectedindexchanging="GridView2_SelectedIndexChanging" 
            AllowSorting="True">
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
