<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementAdmin/ManagementAdmin.Master" AutoEventWireup="true" CodeBehind="Variation.aspx.cs" Inherits="USACBOSA.ManagementAdmin.Variation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
<script type = "text/javascript">
    function grdHeaderCheckBox(objRef) {
        var grd = objRef.parentNode.parentNode.parentNode;
        var inputList = grd.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
</script>
    <style type="text/css">
        .style1
        {
            width: 118px;
        }
        .style2
        {
            width: 134px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Label ID="Label4" runat="server" Text="Member Share/Savings Variations" Font-Bold="True" 
        Font-Size="14pt" ForeColor="#FF9900"></asp:Label>
        <hr style="color: Maroon" />
 
    <table style="width:100%;">
        <tr>
            <td>
                            <asp:Label ID="Label1" runat="server" Text="Member No:" 
                    style="float: right"></asp:Label>
                        </td>
            <td class="style2">
                            <asp:TextBox ID="txtMemberNo" runat="server" 
                                ontextchanged="txtMemberNo_TextChanged"></asp:TextBox>
                        </td>
            <td class="style1">
                <asp:Label ID="Label10" runat="server" style="float: right" Text="Names:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMemberNames" runat="server" Width="240px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="Label5" runat="server" Text="Company:" 
                    style="float: right"></asp:Label>
                        </td>
            <td class="style2">
                            <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                        </td>
            <td class="style1">
                            <asp:Label ID="Label2" runat="server" Text="Reg Date:" 
                    style="float: right"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txtRegDate" runat="server"></asp:TextBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="Label8" runat="server" Text="Share Code:" 
                    style="float: right"></asp:Label>
                        </td>
            <td class="style2">
                            <asp:TextBox ID="txtShareCode" runat="server"></asp:TextBox>
                        </td>
            <td class="style1">
                            <asp:Label ID="Label9" runat="server" Text="Share Type:" 
                    style="float: right"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txtShareType" runat="server" Width="240px"></asp:TextBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="Label3" runat="server" Text="Variation Date:" 
                    style="float: right"></asp:Label>
                        </td>
            <td class="style2">
                            <asp:TextBox ID="dtpShareVarDate" runat="server"></asp:TextBox>
                        </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                            <asp:CheckBox ID="chkSubscribe" runat="server" Text="Subscribe" />
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="Label7" runat="server" Text="Default Amount:" 
                    style="float: right"></asp:Label>
                        </td>
            <td class="style2">
                            <asp:TextBox ID="txtDefAmount" runat="server" ForeColor="#CC0066" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
            <td class="style1">
                            <asp:Label ID="Label6" runat="server" Text="Subscribed Amount:" 
                    style="float: right"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txtSubscribedAmount" runat="server" Font-Bold="True"></asp:TextBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                                Width="120px" Font-Bold="True" />
                        </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
                <hr style="color: Maroon" />
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                            Font-Size="8pt" Visible="False" Width="802px" 
                                             AutoGenerateSelectButton="True" 
                                             onselectedindexchanged="GridView1_SelectedIndexChanged" ForeColor="Red">
                                            <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="CheckAll" 
                                                            onclick = "grdHeaderCheckBox(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="AtmSelector" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                        </asp:GridView>
</asp:Content>
