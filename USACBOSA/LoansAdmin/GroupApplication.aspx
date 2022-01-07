<%@ Page Title="" Language="C#" MasterPageFile="~/LoansAdmin/LoansAdmin.Master" AutoEventWireup="true" CodeBehind="GroupApplication.aspx.cs" Inherits="USACBOSA.LoansAdmin.GroupApplication" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 163px;
        }
        .auto-style3 {
            width: 166px;
        }
        .auto-style4 {
            width: 263px;
        }
        .auto-style5 {
            width: 163px;
            height: 43px;
        }
        .auto-style6 {
            width: 166px;
            height: 43px;
        }
        .auto-style7 {
            width: 263px;
            height: 43px;
        }
        .auto-style8 {
            height: 43px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hr style="color: Maroon" />
    <table frame="box" style="width:100%;">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">Loan No.<br />
                <asp:TextBox ID="txtLoanNo" runat="server" Width="151px" Enabled="False"></asp:TextBox>
            </td>
            <td class="auto-style6">Group Code<br />
                <asp:TextBox ID="txtGroupCode" runat="server" Width="151px" OnTextChanged="txtGroupCode_TextChanged" AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="auto-style7">Group Name<br />
                <asp:TextBox ID="groupName" runat="server" Width="249px"></asp:TextBox>
            </td>
            <td class="auto-style5">Loan Amount<br />
                <asp:TextBox ID="txtLoanAmount" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="auto-style8"></td>
        </tr>
        
        <tr>
            <td class="auto-style2">
                Loan Code<br />
                <asp:DropDownList ID="loan_code" runat="server" AutoPostBack="True" Height="17px" Width="157px" OnSelectedIndexChanged="loan_code_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">Repay Period<br />
                <asp:TextBox ID="repay_period" runat="server" Width="152px"></asp:TextBox>
            </td>
            <td>Repay Method<br />
                <asp:TextBox ID="repay_method" runat="server" Width="244px"></asp:TextBox>
            </td>
            <td>Interest<br />
                <asp:TextBox ID="txt_interest" runat="server" Width="148px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="auto-style2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
            <td class="auto-style3" id="loan_code">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <hr style="color: Maroon" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Font-Size="8pt" style="margin-top: 0px" 
        Width="874px" PageSize="100" AutoGenerateSelectButton="True"
        >
    </asp:GridView>
</asp:Content>
