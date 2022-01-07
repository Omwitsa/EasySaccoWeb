<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mstatement.aspx.cs" Inherits="USACBOSA.Reports.mstatement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="344px" ForeColor="Black" 
        style="background-color: #FFFFFF">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                        </asp:ToolkitScriptManager>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" 
    Text="Enter Supplier Number and select End Period to Print statement" 
            BackColor="#FFCCCC" ForeColor="Black"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" BackColor="#FFCCCC" ForeColor="Black" 
            Text="Supplier No"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSno" runat="server" TabIndex="1" AutoPostBack="True"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" BackColor="#FFCCCC" ForeColor="Black" 
            Text="Start Date "></asp:Label>
        &nbsp;<asp:TextBox ID="txtEndDate" runat="server"  Width="128px"></asp:TextBox>
         <asp:CalendarExtender ID="txtEndDate_CalendarExtender" Enabled="true"  format = "dd-MM-yyyy" runat="server" 
                                TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
        
        &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/Image/calendar.png" style="color: #0000CC" />
        &nbsp;
        <asp:Label ID="Label8" runat="server" 
            style="color: #000000; background-color: #FFCCCC" Text="End period"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="TextBox1_CalendarExtender1"  Enabled="true" runat="server" 
            format="dd-MM-yyyy" TargetControlID="TextBox1">
        </asp:CalendarExtender>
        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
            ImageUrl="~/Image/calendar.png" style="background-color: #FFFFFF" />
        &nbsp;&nbsp;
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Supplier Statement<br />
        <asp:Panel ID="Panel2" runat="server" BackColor="#FFFFCC" BorderColor="Black" 
            BorderStyle="Solid" Height="169px" style="margin-top: 0px" Width="531px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="270px" 
                ForeColor="Black" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                <asp:ListItem Value="NormalPOS" >Normal Statement(Use POS printer)</asp:ListItem>
                <asp:ListItem Value="DetailedPOS" Selected="True">Detailed Printer (Use POS printer)</asp:ListItem>
                <asp:ListItem Value="NormalA4">Normal Statement (Use normal(A4) printer)</asp:ListItem>
                <asp:ListItem Value="advanceslip">Print Advance Slip</asp:ListItem>
            </asp:RadioButtonList>
            &nbsp;
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" Text="Print" 
                Width="60px" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
