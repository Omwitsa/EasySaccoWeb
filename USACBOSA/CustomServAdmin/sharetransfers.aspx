<%@ Page Title="" Language="C#" MasterPageFile="~/CustomServAdmin/CustomServAdmin.Master" AutoEventWireup="true" CodeBehind="sharetransfers.aspx.cs" Inherits="USACBOSA.CustomServAdmin.sharetransfers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80%;
        margin-right: 0px;
    }
        .style2
        {
            width: 110%;
        height: 362px;
    }
        .style32
        {
            width: 138px;
            height: 22px;
        }
        .style33
        {
            color: #000000;
            width: 76px;
            height: 22px;
        }
        .style34
        {
            width: 96px;
            height: 22px;
        }
        .style35
        {
            color: #000000;
            height: 22px;
        }
        .style36
        {
            width: 194px;
            height: 22px;
        }
        .style37
        {
            height: 22px;
        }
        .style44
        {
            height: 288px;
        }
        .style49
        {
            width: 138px;
            height: 7px;
        }
        .style50
        {
            color: #000000;
            width: 76px;
            height: 7px;
        }
        .style51
        {
            width: 96px;
            height: 7px;
        }
        .style52
        {
            color: #000000;
            height: 7px;
        }
        .style53
        {
            width: 194px;
            height: 7px;
        }
        .style54
        {
            height: 7px;
        }
        .style55
        {
            color: #000000;
            width: 76px;
            height: 25px;
        }
        .style56
        {
            width: 96px;
            height: 25px;
        }
        .style57
        {
            color: #000000;
            height: 25px;
        }
        .style58
        {
            width: 194px;
            height: 25px;
        }
        .style59
        {
            color: #000000;
            width: 138px;
            height: 25px;
        }
        .style60
        {
            height: 25px;
        }
    .style61
    {
        color: #000000;
        width: 76px;
        height: 27px;
    }
    .style62
    {
        width: 96px;
        height: 27px;
    }
    .style63
    {
        color: #000000;
        height: 27px;
    }
    .style64
    {
        width: 194px;
        height: 27px;
    }
    .style65
    {
        color: #000000;
        width: 138px;
        height: 27px;
    }
    .style66
    {
        height: 27px;
    }
    .style71
    {
        color: #000000;
        width: 138px;
        height: 20px;
    }
    .style72
    {
        color: #000000;
        width: 76px;
        height: 20px;
    }
    .style73
    {
        width: 96px;
        height: 20px;
    }
    .style74
    {
        color: #000000;
        height: 20px;
    }
    .style75
    {
        width: 194px;
        height: 20px;
    }
    .style76
    {
        height: 20px;
    }
    .style77
    {
        color: #000000;
        width: 76px;
        height: 11px;
    }
    .style78
    {
        width: 96px;
        height: 11px;
    }
    .style79
    {
        color: #000000;
        height: 11px;
    }
    .style80
    {
        width: 194px;
        height: 11px;
    }
    .style81
    {
        color: #000000;
        width: 138px;
        height: 11px;
    }
    .style82
    {
        height: 11px;
    }
    .style83
    {
        width: 758px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style83">
                <table class="style2">
                    <tr>
                        <td class="style44">
                            <table class="style2">
                                <tr>
                                    <td class="style72">
                                        Donor</td>
                                    <td class="style73">
                                        <asp:TextBox ID="txtDMemberNo" runat="server" Height="19px" Width="108px" 
                                            ontextchanged="TxtDonor_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style74">
                                        Names</td>
                                    <td class="style75">
                                        <asp:TextBox ID="txtDNames" runat="server" Width="198px"></asp:TextBox>
                                    </td>
                                    <td class="style71">
                                        Total Shares</td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtDBalance" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style61">
                                        Share Code</td>
                                    <td class="style62">
                                        <asp:DropDownList ID="cboDCode" runat="server" Height="21px" Width="107px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ShareCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style63">
                                        Share Type</td>
                                    <td class="style64">
                                        <asp:TextBox ID="txtDSharetype" runat="server" Height="23px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="style65">
                                        Shares Available</td>
                                    <td class="style66">
                                        <asp:TextBox ID="txtDAvailable" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style77">
                                        Recipient</td>
                                    <td class="style78">
                                        <asp:TextBox ID="txtRMemberNo" runat="server" Width="73px" AutoPostBack="True" 
                                            ontextchanged="Recipient_TextChanged"></asp:TextBox>
                                        <asp:Button ID="Button4" runat="server" Text="F" />
                                    </td>
                                    <td class="style79">
                                        Names</td>
                                    <td class="style80">
                                        <asp:TextBox ID="txtRNames" runat="server" Width="195px"></asp:TextBox>
                                    </td>
                                    <td class="style81">
                                        Total</td>
                                    <td class="style82">
                                        <asp:TextBox ID="txtRBalance" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style55">
                                        Share Code</td>
                                    <td class="style56">
                                        <asp:DropDownList ID="cboRCode" runat="server" AutoPostBack="True" 
                                            Height="22px" onselectedindexchanged="DrpShareCode_SelectedIndexChanged" 
                                            Width="93px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style57">
                                        Share Type</td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtRShareType" runat="server" Width="197px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        Available Shares</td>
                                    <td class="style60">
                                        <asp:TextBox ID="txtRAvailable" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style33">
                                        Amount</td>
                                    <td class="style34">
                                        <asp:TextBox ID="txtTransAmount" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style35">
                                        Narration</td>
                                    <td class="style36">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="196px"></asp:TextBox>
                                    </td>
                                    <td class="style32">
                                        </td>
                                    <td class="style37">
                                        <asp:TextBox ID="dtpTransDate" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style50">
                                        </td>
                                    <td class="style51">
                                        </td>
                                    <td class="style52">
                                        <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Width="78px" 
                                            onclick="Button1_Click" Font-Bold="True" />
                                    </td>
                                    <td class="style53">
&nbsp;<asp:Button ID="Button2" runat="server" Text="Withdraw" Width="71px" onclick="Button2_Click" />
&nbsp;</td>
                                    <td class="style49">
                                        </td>
                                    <td class="style54">
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
