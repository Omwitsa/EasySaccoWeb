<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/SysAdmin.Master" AutoEventWireup="true" CodeBehind="SocietyParameters.aspx.cs" Inherits="USACBOSA.SysAdmin.SocietyParameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">
        #Text1
        {
            width: 226px;
            height: 88px;
        }
        #Text2
        {
            height: 84px;
            width: 175px;
        }
        .style1
        {
        }
        .style2
        {
            width: 171px;
        }
        .style3
        {
            width: 127px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:Label ID="Label1" runat="server" Text="Society Parameters" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
        <table style="width:100%;">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style1" colspan="3">
                    <asp:Button ID="btnLoadSysparam" runat="server" onclick="btnLoadSysparam_Click" 
                        Text="Load Company Parameters" Width="202px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label>
                </td>
                <td colspan="3">
                        <asp:TextBox ID="txtCompanyName" runat="server" Width="332px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label9" runat="server" Text="Address"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtAddress" runat="server" Width="132px"></asp:TextBox>
                    </td>
                <td class="style3">
                        <asp:Label ID="Label20" runat="server" Text="Check Off Date"></asp:Label>
                    </td>
                <td>
                        <asp:TextBox ID="dtpCheckOffDate" runat="server" Width="145px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Town"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtTown" runat="server"></asp:TextBox>
                    </td>
                <td class="style3">
                    <asp:Label ID="Label10" runat="server" Text="Telephone"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtTelephone" runat="server" Width="200px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label7" runat="server" Text="Fax"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                    </td>
                <td class="style3">
                    <asp:Label ID="Label11" runat="server" Text="Email Address"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtEMail" runat="server" Width="203px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label8" runat="server" Text="Website"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtwebsites" runat="server" 
                        ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                    </td>
                <td class="style3">
                        <asp:Label ID="Label12" runat="server" Text="Physical Address"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtcontactperson" runat="server" Width="204px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label2" runat="server" Text="Membership Maturity"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtMaturity" runat="server" Width="133px"></asp:TextBox>
                    </td>
                <td class="style3">
                        <asp:Label ID="Label3" runat="server" Text="Default Currency"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboCurrency" runat="server" Height="16px" Width="101px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label4" runat="server" Text="Withdarwal Notice"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtWithdrawalNotice" runat="server" Width="134px"></asp:TextBox>
                    </td>
                <td class="style3">
                        <asp:Label ID="Label13" runat="server" Text="Default Rounding"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtRounding" runat="server" Width="101px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label14" runat="server" Text="Dividend Processing Period"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtDivPeriod" runat="server" Width="99px"></asp:TextBox>
                    </td>
                <td class="style3">
                        <asp:Label ID="Label15" runat="server" Text="Significant Loan Balance"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtSLBalance" runat="server" Width="105px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label16" runat="server" Text="Action on Defaulted Intrest"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="cboDefInterestAction" runat="server" AutoPostBack="True" 
                        Height="16px" 
                        onselectedindexchanged="cboDefInterestAction_SelectedIndexChanged" Width="51px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td rowspan="3" colspan="2">
                    <asp:TextBox ID="txtDefInterestAction" runat="server" Height="46px" 
                        TextMode="MultiLine" Width="333px"></asp:TextBox>&nbsp;<br />
                    <asp:TextBox ID="lblFormula" runat="server" TextMode="MultiLine" Width="331px" 
                        Height="24px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label18" runat="server" Text="Minimum Guarantors"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtMinGuar" runat="server"></asp:TextBox>
                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label19" runat="server" Text="Maximum Guarantors"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="txtMaxGuar" runat="server"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label21" runat="server" Text="Suspense"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="cboSuspAccno" runat="server" Height="22px" Width="83px" 
                            AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="btnFindSuspcAC" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" onclick="btnFindSuspcAC_Click" />
                </td>
                <td colspan="2">
                        <asp:TextBox ID="txtAccNames" runat="server" Width="330px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label22" runat="server" Text="Retained Earnings"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="cboREAccno" runat="server" Width="83px" AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="btnFindREAC" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" onclick="btnFindREAC_Click" />
                </td>
                <td colspan="2">
                        <asp:TextBox ID="txtREAccNames" runat="server" Width="326px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label23" runat="server" Text="Creditors"></asp:Label>
                </td>
                <td class="style1">
                        <asp:TextBox ID="cboCreditorsAccno" runat="server" Width="83px" 
                            AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="btnFindCreditorsAC" runat="server" 
                        ImageUrl="~/Images/searchbutton.PNG" onclick="btnFindCreditorsAC_Click" />
                </td>
                <td colspan="2">
                        <asp:TextBox ID="txtCreditorsAccNames" runat="server" Width="329px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="SAVE" />
                </td>
                <td class="style3">
                    <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" 
                        onclick="btnUpdate_Click" />
                </td>
                <td>
                    <asp:TextBox ID="txtSetValue" runat="server" Visible="False" Width="44px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
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
&nbsp; 
</asp:Content>
