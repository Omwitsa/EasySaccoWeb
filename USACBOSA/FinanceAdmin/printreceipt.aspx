<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printreceipt.aspx.cs" Inherits="USACBOSA.CustomServAdmin.printreceipt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Neodynamic.SDK.Web" %>
<script runat="server">
protected void Page_Init(object sender, EventArgs e)
{
//Is a Print Request?
    
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
<title>How to directly Print ESC/POS Commands without Printer Dialog</title>
</head>
<body onload="javascript:jsWebClientPrint.print('useDefaultPrinter=' + $('#useDefaultPrinter').attr('checked') + '&printerName=' + $('#installedPrinterName').val()); window.close();">
<%-- Store User's SessionId --%>
<input type="hidden" id="sid" name="sid" value="<%=Session.SessionID%>" />
<form id="form2" runat="server">
<h1></h1>
<label class="checkbox">
<input type="checkbox" id="useDefaultPrinter" /> <strong>Use default printer</strong> or...
</label>
<div id="loadPrinters">
<br />
WebClientPrint can detect the installed printers in your machine.
<br />
<input type="button" onclick="javascript:jsWebClientPrint.getPrinters();" value="Load installed printers..." />
<br /><br />
</div>
<div id="installedPrinters" style="visibility:hidden">
<br />
<label for="installedPrinterName">Select an installed Printer:</label>
<select name="installedPrinterName" id="installedPrinterName"></select>
</div>
<br /><br />
<input type="button" style="font-size:18px" onclick="javascript:jsWebClientPrint.print('useDefaultPrinter=' + $('#useDefaultPrinter').attr('checked') + '&printerName=' + $('#installedPrinterName').val());" value="Print Receipt..." />
<script type="text/javascript">.
var wcppGetPrintersDelay_ms = 5000; //5 sec

function wcpGetPrintersOnSuccess(){

<%-- Display client installed printers --%>

if(arguments[0].length > 0){

var p=arguments[0].split("|");

var options = '';

for (var i = 0; i < p.length; i++) {

options += '<option>' + p[i] + '</option>';

}

$('#installedPrinters').css('visibility','visible');

$('#installedPrinterName').html(options);

$('#installedPrinterName').focus();

$('#loadPrinters').hide();                                                       

}else{

alert("No printers are installed in your system.");

}

}

function wcpGetPrintersOnFailure() {

<%-- Do something if printers cannot be got from the client --%>

alert("No printers are installed in your system.");

}

</script>

</form>

<%-- Add Reference to jQuery at Google CDN --%>

<script src="../Scripts/JQuery.min.js" type="text/javascript"></script>

<%-- Register the WebClientPrint script code --%>

<%=Neodynamic.SDK.Web.WebClientPrint.CreateScript()%>

</body>

</html>

