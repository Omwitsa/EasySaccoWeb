<%@ Page Title="" Language="C#" MasterPageFile="~/CreditAdmin/CreditAdmin.Master" AutoEventWireup="true" CodeBehind="MembershipRegistration.aspx.cs" Inherits="USACBOSA.CreditAdmin.MembershipRegistration" %>
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
        .style36
        {
            width: 29px;
        }
        .style40
        {
    }
        .style41
        {
            width: 143px;
        }
        .style44
        {
        }
        .style45
        {
            width: 154px;
        }
        .style50
    {
        width: 154px;
        height: 34px;
    }
        .style55
    {
        width: 143px;
        height: 28px;
    }
    .style56
    {
        width: 154px;
        height: 28px;
    }
    .style57
    {
        height: 28px;
    }
        .style58
        {
            width: 68px;
        }
        .style59
        {
            height: 34px;
            width: 68px;
        }
        .style60
    {
        width: 112px;
    }
    .style61
    {
        height: 34px;
        width: 112px;
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
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
        <hr style="color: Maroon; height: 2px;" />
        <table style="width:100%;">
            <tr>
                <td class="style41">
        <asp:Label ID="Label6" runat="server" Text="Member Type:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style45">
                    <asp:DropDownList ID="cboMemberType" runat="server" AutoPostBack="True" 
                        Height="22px" onselectedindexchanged="cboMemberType_SelectedIndexChanged" 
                        Width="110px" ForeColor="Black">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Normal Member</asp:ListItem>
                        <asp:ListItem>Junior Member</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td class="style60" colspan="2">
                    <asp:Label ID="Label29" runat="server" Text="Category:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style58">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                    Width="110px" ontextchanged="DropDownList1_TextChanged" 
                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                        TabIndex="1" Visible="False" ForeColor="Black">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>County</asp:ListItem>
        <asp:ListItem>Diaspora</asp:ListItem>
        <asp:ListItem>Staff</asp:ListItem>
    </asp:DropDownList>
                            <asp:TextBox ID="txtMemberNumber" runat="server" AutoPostBack="True" 
                        Height="22px" ontextchanged="txtMemberNumber_TextChanged" Width="110px" 
                        ForeColor="Black"></asp:TextBox>
                            </td>
                <td class="style40">
  <asp:Label ID="Label18" runat="server" Text=" County:"  Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left;"></asp:Label>
                            </td>
                <td class="style36">
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                    Width="110px" ontextchanged="DropDownList3_TextChanged" 
                                    onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                        TabIndex="2" ForeColor="Black">
    </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td class="style41">
    <asp:Label ID="Label7" runat="server" Text="Member No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style45">
                            <asp:TextBox ID="txtMemberNo" runat="server" Width="110px" AutoPostBack="True" 
                                Font-Size="8pt" ontextchanged="txtMemberNo_TextChanged" 
                                ondatabinding="txtMemberNo_DataBinding" Height="22px" TabIndex="3" 
                                ReadOnly="True" ForeColor="Black"></asp:TextBox>
                                </td>
                <td class="style60" colspan="2">
    <asp:Label ID="Label8" runat="server" Text="Surname:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#000066" style="float: left"></asp:Label>
                                </td>
                <td class="style58">
    <asp:TextBox ID="txtSurname" runat="server" Width="110px" Font-Size="8pt" TabIndex="4" 
                        Height="22px" ForeColor="Black"></asp:TextBox>
                            </td>
                <td class="style40">
                            <asp:Label ID="Label9" runat="server" Text="O.Names:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                                style="float: left"></asp:Label>
                            </td>
                <td class="style36">
    <asp:TextBox ID="txtOtherNames" runat="server" Width="174px" Font-Size="8pt" Font-Bold="True" 
                        Height="21px" TabIndex="5" ForeColor="Black"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="style41">
    <asp:Label ID="Label19" runat="server" Text="Station:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#000066" style="float: left"></asp:Label>
                                </td>
                <td class="style45">
                    <asp:DropDownList ID="cboStation" runat="server" Height="23px" Width="110px" 
                        ForeColor="Black">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td class="style60" colspan="2">
    <asp:Label ID="Label12" runat="server" Text="Gender:" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#000066" style="float: left"></asp:Label>
                                </td>
                <td class="style58">
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Font-Size="8pt" Height="22px" 
                                Width="76px" TabIndex="6" ForeColor="Black">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>MALE</asp:ListItem>
        <asp:ListItem>FEMALE</asp:ListItem>
        <asp:ListItem>OTHER</asp:ListItem>
    </asp:DropDownList>
                            </td>
                <td class="style40">
    <asp:Label ID="Label10" runat="server" Text="ID No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                            </td>
                <td class="style36">
    <asp:TextBox ID="txtIdNo" runat="server" Width="110px" AutoPostBack="True" Font-Size="8pt" 
                        TabIndex="7" Height="22px" ForeColor="Black"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="style41">
    <asp:Label ID="Label14" runat="server" Text="Phone No.:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style45">
    <asp:TextBox ID="txtPhoneNo" runat="server" Width="110px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="8" Height="22px" 
                        ontextchanged="txtPhoneNo_TextChanged" ForeColor="Black" ></asp:TextBox>
                            <asp:Label ID="Label30" runat="server" Text="Label" Visible="False"></asp:Label>
                            </td>
                <td class="style60" colspan="2">
                <asp:Label ID="Label15" runat="server" Text="DOB:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left" Width="31px"></asp:Label>
                                </td>
                <td class="style58">
                            <asp:TextBox ID="txtDob" runat="server" Width="92px" AutoPostBack="True" 
                                Font-Size="8pt" TabIndex="15" ontextchanged="txtDob_TextChanged" 
                        Height="22px" ForeColor="Black"></asp:TextBox>
                                
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/calendar.png" Width="16px" 
                                    onclick="ImageButton2_Click" />
                                    <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="dd-MM-yyyy" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtDob">
                </asp:CalendarExtender>
                            </td>
                <td class="style40">
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#000066" Text="Age:" style="float: left"></asp:Label>
                            </td>
                <td class="style36">
                    <asp:TextBox ID="txtAge" runat="server" TabIndex="16" Width="70px" Font-Bold="True" 
                                ReadOnly="True" Height="22px" ForeColor="Black"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="style41">
    <asp:Label ID="Label13" runat="server" Text="Land-Line:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style45">
    <asp:TextBox ID="txtLandLine" runat="server" Width="115px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="14" Height="22px" ForeColor="Black"></asp:TextBox>
                            </td>
                <td class="style60" colspan="2">
    <asp:Label ID="Label20" runat="server" Text="Email:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style58">
                <asp:TextBox ID="txtEmail" runat="server" Width="157px" AutoPostBack="True" 
                    Font-Size="8pt" TabIndex="11" ForeColor="Black"></asp:TextBox>
                            </td>
                <td class="style40">
                                <asp:Label ID="Label17" runat="server" Text="Pin No.:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                                    Visible="False" style="float: left"></asp:Label>
                            </td>
                <td class="style36">
                                <asp:TextBox ID="txtPinNo" runat="server" Width="137px" AutoPostBack="True" 
                                    Font-Size="8pt" TabIndex="9" Visible="False" Height="22px" 
                                    ForeColor="Black"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="style41">
                                <asp:Label ID="Label24" runat="server" ForeColor="#000066" style="float: left" 
                                    Text="Marital Status:" Font-Bold="False" Font-Italic="False" 
                        Font-Names="Tahoma" Font-Size="10pt"></asp:Label>
                                </td>
                <td class="style45">
                    <asp:DropDownList ID="cboMaritalStatus" runat="server" Height="27px" 
                        Width="120px" ForeColor="Black">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Married</asp:ListItem>
                        <asp:ListItem>Single</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td class="style60" colspan="2">
                                <asp:Label ID="Label25" runat="server" ForeColor="#000066" style="float: left" 
                                    Text="Status:" Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" Height="17px"></asp:Label>
                                </td>
                <td class="style58">
                    <asp:DropDownList ID="cboStatus" runat="server" Height="22px" Width="120px" 
                        ForeColor="Black">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Withdrawn</asp:ListItem>
                        <asp:ListItem>Deceased</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                <td class="style40" rowspan="4">
                                <br />
                            </td>
                <td class="style40" rowspan="4">
                               <asp:Image ID="imgPhoto" runat="server" Height="121px" 
                    ImageUrl="~/Images/photo.jpg" Width="195px" style="margin-left: 0px" />
                            </td>
            </tr>
            <tr>
                <td class="style41">
                <asp:Label ID="Label11" runat="server" Text="Registration Date:"  
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" style=" float: left;" 
                        Width="107px"></asp:Label>
                                </td>
                <td class="style50">
    <asp:TextBox ID="txtRegDate" runat="server" Width="109px" Font-Size="8pt" TabIndex="10" 
                        Wrap="False" Height="21px" ForeColor="Black"></asp:TextBox>
         <asp:CalendarExtender ID="txtRegDate_CalendarExtender" Enabled="true" PopupButtonID="ImageButton1" format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtRegDate">
                            </asp:CalendarExtender>
    
    
    
    <asp:ImageButton 
                                ID="ImageButton1" runat="server" 
                        ImageUrl="~/Images/calendar.png" Visible="False" Width="16px" />
                            </td>
                <td class="style61" colspan="2">
    <asp:Label ID="Label21" runat="server" Text="Address:" Font-Bold="False" 
                        Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style59">
    <asp:TextBox ID="txtPostalAddress" runat="server" Width="158px" 
                        Font-Size="8pt" TabIndex="12" Height="22px" ForeColor="Black"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="style55">
                            <asp:Label ID="Label23" runat="server" Text="Company Code:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                                style="float: left"></asp:Label>
                                </td>
                <td class="style56">
                                <asp:DropDownList ID="cboCompanyCode" runat="server" Height="21px" Width="90px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="cboCompanyCode_SelectedIndexChanged" 
                        TabIndex="17" ForeColor="Black">
                                </asp:DropDownList>
                            </td>
                <td class="style57" colspan="3">
                                <asp:TextBox ID="txtCompany" runat="server" Width="204px" 
                        ReadOnly="True" ForeColor="Black"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="style41">
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#000066" Text="Agent \ Referee:" 
                        style="float: left"></asp:Label>
                                </td>
                <td class="style45">
                    <asp:DropDownList ID="cboAgentId" runat="server" AutoPostBack="True" 
                        Height="20px" onselectedindexchanged="cboAgentId_SelectedIndexChanged" 
                        TabIndex="18" Width="91px" ForeColor="Black">
                    </asp:DropDownList>
                            </td>
                <td class="style44" colspan="3">
                    <asp:TextBox ID="txtAgentNames" runat="server" ReadOnly="True" Width="203px" 
                        ForeColor="Black"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="style41">
                                        <asp:Label ID="Label26" runat="server" Text="Search by:" 
                        Font-Bold="False" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#000066" 
                        style="float: right"></asp:Label>
                </td>
                <td class="style45">
                                        <asp:DropDownList ID="DropDownList5" runat="server" 
                        Height="16px" Width="117px" TabIndex="24" ForeColor="Black">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Member No</asp:ListItem>
                                            <asp:ListItem>Names</asp:ListItem>
                                            <asp:ListItem>ID Number</asp:ListItem>
                                        </asp:DropDownList>
                </td>
                <td class="style30" colspan="3">
                    <asp:TextBox ID="txtSearch" runat="server" TabIndex="25" Width="143px" 
                        ForeColor="Black"></asp:TextBox>
                    <asp:ImageButton ID="btnSearch" runat="server" Height="19px" ImageUrl="~/Images/searchbutton.PNG" 
                                            onclick="btnSearch_Click" Width="26px" TabIndex="26" />
                </td>
                <td class="style40" rowspan="2">
                    &nbsp;</td>
                <td class="style40" rowspan="2">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="173px" />
                            </td>
            </tr>
            <tr>
                <td class="style41">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                                Width="72px" Font-Bold="True" Font-Size="12pt" TabIndex="27" 
                        Height="27px" BorderColor="Silver" ForeColor="Black" BackColor="#33CCCC" 
                                            style="float: right" />
                </td>
                <td class="style29" colspan="2">
&nbsp;<asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click1" 
                        Text="Update" Font-Bold="True" Font-Size="12pt" TabIndex="28" 
                        Height="28px" BorderColor="Silver" ForeColor="Black" BackColor="#33CCCC" />
                </td>
                <td class="style29" colspan="2">
                &nbsp;
                                        <asp:Button ID="btnBeneficiary" runat="server" Text="Beneficiary" 
                                            ForeColor="#990099" onclick="btnBeneficiary_Click1" 
                        TabIndex="29" Font-Bold="True" Font-Size="12pt" BackColor="#33CCCC" />
                                        <asp:Button ID="Button1" runat="server" 
                        onclick="Button1_Click" Text="Button" 
                                            Visible="False" Height="21px" />
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
                    </asp:Content>
