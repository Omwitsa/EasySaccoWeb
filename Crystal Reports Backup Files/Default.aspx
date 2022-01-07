<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="USACBOSA._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
    <style type="text/css">

        .style18
        {
        width: 305px;
        text-align: left;
    }
        .style9
        {
            font-size: medium;
            text-align: center;
        }
        .auto-style1
        {
            text-align: center;
            height: 43px;
        }
        .style22
        {            width: 13px;
        }
        .style25
        {
            height: 17px;
        }
        .style27
        {
            width: 305px;
            text-align: left;
            height: 17px;
        }
        .style28
        {
            height: 6px;
        }
        .style30
        {
            width: 305px;
            text-align: left;
            height: 6px;
        }
        .style37
        {
            width: 305px;
            text-align: left;
            height: 18px;
        }
        .style38
        {
            height: 18px;
        }
        .style39
        {
            width: 369px;
        }
        .style40
        {
            height: 21px;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:98%; height: 247px;">
    <tr>
        <td class="auto-style1" colspan="5">
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td class="style39" rowspan="5">
            <asp:Image ID="Image1" runat="server" Height="148px" 
                    ImageUrl="~/Images/index.jpg" Width="183px" 
                style="margin-top: 0px; float: right;" />
            </td>
        <td class="style22" rowspan="5">
            &nbsp;</td>
        <td class="style37">
            <asp:Label ID="Label1" runat="server" CssClass="style13" style="color: #000000" 
                Text="User Name:"></asp:Label>
        </td>
        <td class="style38">
            </td>
        <td class="style38">
            </td>
    </tr>
    <tr>
        <td class="style27">
            <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td class="style25">
            </td>
        <td class="style25">
            </td>
    </tr>
    <tr>
        <td class="style30">
            <asp:Label ID="Label2" runat="server" CssClass="style13" style="color: #000000" 
                Text="Password:"></asp:Label>
        </td>
        <td class="style28">
            </td>
        <td class="style28">
            </td>
    </tr>
    <tr>
        <td class="style18">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
        </td>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style18">
            <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                style="height: 26px" Text="Login" Width="75px" />
        &nbsp;&nbsp;&nbsp;
            </td>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style9" colspan="5">
       </td>
    </tr>
</table>

    <table style="width:100%; height: 69px;">
        <tr>
            <td class="style40">
                </td>
            <td class="style40">
                </td>
            <td class="style40">
                </td>
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
                &nbsp;</td>
            <td>
               
               </td>
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
                &nbsp;</td>
            <td>
               
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Content>

