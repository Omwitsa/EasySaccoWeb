<%@ Page Title="" Language="C#" MasterPageFile="~/ClientAdmin/ClientAdmin.Master" AutoEventWireup="true" CodeBehind="TrialBalance.aspx.cs" Inherits="USACBOSA.ClientAdmin.TrialBalance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80%;
        }
    .style2
    {
        width: 100%;
    }
    .style3
    {            width: 622px;
        }
        .style5
        {
            text-align: center;
        }
        .style6
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" class="style1">
        <tr>
            <td>
            <fieldset style="width: 772px; height: 85px; top: auto; right: auto; bottom: auto; left: auto">
            <legend>Generate Trial Balance</legend>
                <table align="center" class="style2">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label1" runat="server" CssClass="style6" Text="StartDate"></asp:Label>
                        &nbsp;<asp:TextBox ID="dtpStartDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtpStartDate_CalendarExtender" runat="server" 
                                TargetControlID="dtpStartDate">
                            </asp:CalendarExtender>
                        &nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" CssClass="style6" Text="FinishDate"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="dtpFinishDate" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:CalendarExtender ID="dtpFinishDate_CalendarExtender" runat="server" 
                                TargetControlID="dtpFinishDate">
                            </asp:CalendarExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblstatus" runat="server" Text="Processing reports......" 
                                Visible="False"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblaccount" 
                                runat="server" Text="10%" Visible="False"></asp:Label>
&nbsp;<asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            </asp:UpdateProgress>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style5" colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Process" 
                                onclick="Button1_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Print Trial Bal" onclick="Button2_Click" 
                                 />
                            <asp:Button ID="Button3" runat="server" Text="Income Statement" 
                                onclick="Button3_Click" />
                            <asp:Button ID="Button4" runat="server" Text="Print Balance Sheet" 
                                onclick="Button4_Click" />
                            <asp:Button ID="Button6" runat="server" Text="Cash Flow Report" 
                                onclick="Button6_Click" />
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                        </td>
                    </tr>
                </table>
            </fieldset>
                </td>
        </tr>
    </table>
</asp:Content>

