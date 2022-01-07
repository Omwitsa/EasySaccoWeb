using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neodynamic.SDK.Web;

namespace USACBOSA
{
    public partial class printreceipt : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String MemberNo = Request.QueryString["1"];
                String Names = Request.QueryString["2"];
                String Amount = Request.QueryString["3"];
                String User = Request.QueryString["4"];
                String date = Request.QueryString["5"];
                String mode = Request.QueryString["6"];
                String ReceiptNo = Request.QueryString["7"];
                String transtype = Request.QueryString["8"];
                if (WebClientPrint.ProcessPrintJob(Request))
                {
                    bool useDefaultPrinter = (Request["useDefaultPrinter"] == "checked");
                    string printerName = Server.UrlDecode(Request["printerName"]);
                    string ESC = "0x1B"; //ESC byte in hex notation
                    string NewLine = "0x0A"; //LF byte in hex notation
                    string cmds = ESC + "@"; //Initializes the printer (ESC @)
                    cmds += ESC + "!" + "0x14"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                    cmds += " ANGAZA AFRIKA SACCO LIMITED";
                    cmds += NewLine;
                    cmds += " P.O BOX 72367-00200,";
                    cmds += NewLine;
                    cmds += " NAIROBI";
                    cmds += NewLine;
                    cmds += " PAYMENT RECEIPT";
                    cmds += NewLine;
                    cmds += "--------------------------------";
                    cmds += NewLine;
                    cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0)
                    cmds += " Member No. :" + MemberNo;
                    cmds += NewLine;
                    cmds += "Names  :" + Names;
                    cmds += NewLine;
                    cmds += "  Amount  :" + Amount + " Ksh";
                    cmds += NewLine;
                    cmds += "Description:" + transtype;
                    cmds += NewLine;
                    cmds += "Payment Mode:" + mode;
                    cmds += NewLine;
                    cmds += "  ------------------------------";
                    cmds += NewLine;
                    cmds += " Date. :" + date;
                    cmds += NewLine;
                    cmds += "  Received By    :" + User;
                    cmds += NewLine;
                    cmds += " Receipt No. :" + ReceiptNo;
                    cmds += NewLine;
                    cmds += " -------------------------------";
                    cmds += NewLine;
                    cmds += "";
                    cmds += "  ------POWERED BY EASY SACCO------";
                    cmds += NewLine;
                    cmds += " ";
                    cmds += ESC + "!" + "0x38"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                    cmds += NewLine + NewLine;
                    cmds += "";
                    cmds += NewLine;
                    cmds += "";
                    cmds += Convert.ToString((char)27) + "@" + Convert.ToString((char)29) + "V" + (char)1;
                    //Create a ClientPrintJob and send it back to the client!
                    string cmds1 = ESC + "@";
                    string cut = ESC + "|100P";
                    cmds1 += cut;
                    ClientPrintJob cpj = new ClientPrintJob();
                    ClientPrintJob cpj2 = new ClientPrintJob();
                    //set ESC/POS commands to print...
                    cpj.PrinterCommands = cmds;
                    cpj.FormatHexValues = true;
                    cpj2.PrinterCommands = cmds1;
                    cpj2.FormatHexValues = true;
                    //set client printer...
                    if (useDefaultPrinter || printerName == "null")
                    {
                        cpj.ClientPrinter = new UserSelectedPrinter();
                        cpj2.ClientPrinter = new UserSelectedPrinter();
                    }
                    else
                    {
                        cpj.ClientPrinter = new InstalledPrinter(printerName);
                        cpj2.ClientPrinter = new InstalledPrinter(printerName);
                        //send it...
                    }
                    cpj.SendToClient(Response);
                    cpj2.SendToClient(Response);
                    Response.End();
                }
            }
            catch (Exception EX) { WARSOFT.WARMsgBox.Show(EX.Message); return; }
        }
    }
}
