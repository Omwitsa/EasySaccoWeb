using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Neodynamic.SDK.Web;
namespace USACBOSA.LoansAdmin
{
    public partial class printreceipt : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, DR1, DR2, DR3, DR13;
        protected void Page_Load(object sender, EventArgs e)
        {

            String memberno = Request.QueryString["1"];
            String names = Request.QueryString["2"];
            string amount = Request.QueryString["3"];
            String rec = Request.QueryString["4"];
            String pmode = Request.QueryString["5"];
            String transactiontype = Request.QueryString["6"];
            String receivedby = Request.QueryString["7"];
            String date = Request.QueryString["8"];
            String time = Request.QueryString["9"];
            String Datedeposited = Request.QueryString["10"];
            String Contributiondate = Request.QueryString["11"];
            string.Format("{0:0,0.0}", amount);
            if (WebClientPrint.ProcessPrintJob(Request))
            {
                bool useDefaultPrinter = (Request["useDefaultPrinter"] == "checked");
                string printerName = Server.UrlDecode(Request["printerName"]);
                string ESC = "0x1B"; //ESC byte in hex notation
                string NewLine = "0x0A"; //LF byte in hex notation
                string cmds = ESC + "@"; //Initializes the printer (ESC @)
                cmds += ESC + "!" + "0x14"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "   FEP SACCO SOCIETY LIMITED";
                cmds += NewLine;
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0) 
                cmds += "   Galana Plaza, third Floor - Galana Road";
                cmds += NewLine;
                cmds += "   P.O BOX 72367-00200, Nairobi, Kenya";//0720170119 | 0705252402 | 0727585682
                cmds += NewLine;
                cmds += "   Tel:  0720170119 | 0727585682";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += "   Email: info@fepsacco.co.ke";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += NewLine;
                cmds += "   MPesa Paybill Number: 929350";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += "   SACCO";
                cmds += "  :" + transactiontype + " RECEIPT";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += NewLine;
                cmds += "   MEMBER NO. :" + memberno + "";
                cmds += NewLine;
                WARTECHCONNECTION.cConnect cnt81 = new WARTECHCONNECTION.cConnect();
                DR = cnt81.ReadDB("SELECT surname + othernames as names FROM members WHERE memberno='" + memberno + "' ");
                cmds += NewLine;
                cmds += "   Names  : ";
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        cmds += DR["names"].ToString();// +" " + DR["Qua"].ToString() + "x" + DR["price"].ToString() + " :" + Convert.ToDouble(DR["Amount"]).ToString("0.00");
                    }
                }
                DR.Close(); DR.Dispose(); DR = null; cnt81.Dispose(); cnt81 = null;
                cmds += NewLine;
                cmds += "   Amount          :" + "Ksh." + amount + "";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += "      Receipt No      :" + rec + "";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += NewLine;
                cmds += "   Payment Mode    :" + pmode.Replace("%20", " ") + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Transaction Type     :" + transactiontype + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Date Deposited       :" + Datedeposited + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Contibution Date     :" + Contributiondate + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Received By     :" + receivedby + "";
                cmds += NewLine;
                cmds += " --------------------------------------------";
                cmds += NewLine;
                cmds += "   Date            :" + date + " ; " + time.Replace("%20", " ") + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     HOLISTICALLY EMPOWERING ENTREPRENEURS";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += NewLine;
                cmds += "  ------------POWERED BY EASYSACCO--------------";
                cmds += NewLine;
                cmds += " ";
                cmds += ESC + "!" + "0x38"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += NewLine + NewLine;
                cmds += "";
                cmds += NewLine;
                cmds += "";
                cmds += NewLine + NewLine;
                cmds += "";
                cmds += NewLine;
                cmds += "";
                cmds += Convert.ToString((char)27) + "@" + Convert.ToString((char)29) + "V" + (char)1;


                cmds += ESC + "!" + "0x14"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "   FEP SACCO SOCIETY LIMITED";
                cmds += NewLine;
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0) 
                cmds += "   Galana Plaza, third Floor - Galana Road";
                cmds += NewLine;
                cmds += "   P.O BOX 72367-00200, Nairobi, Kenya";//0720170119 | 0705252402 | 0727585682
                cmds += NewLine;
                cmds += "   Tel:  0720170119 | 0727585682";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += "   Email: info@fepsacco.co.ke";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += NewLine;
                cmds += "   MPesa Paybill Number: 929350";//info@fepsacco.co.ke
                cmds += NewLine;
                cmds += "   SACCO";
                cmds += "  :" + transactiontype + " RECEIPT";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += NewLine;
                cmds += "   MEMBER NO. :" + memberno + "";
                cmds += NewLine;
                WARTECHCONNECTION.cConnect cnt811 = new WARTECHCONNECTION.cConnect();
                DR = cnt811.ReadDB("SELECT surname + othernames as names FROM members WHERE memberno='" + memberno + "' ");
                cmds += NewLine;
                cmds += "   Names  : ";
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        cmds += DR["names"].ToString();// +" " + DR["Qua"].ToString() + "x" + DR["price"].ToString() + " :" + Convert.ToDouble(DR["Amount"]).ToString("0.00");
                    }
                }
                DR.Close(); DR.Dispose(); DR = null; cnt811.Dispose(); cnt811 = null;
                cmds += NewLine;
                cmds += "   Amount          :" + "Ksh." + amount + "";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += "      Receipt No      :" + rec + "";
                cmds += NewLine;
                cmds += "  -------------------------------------------";
                cmds += NewLine;
                cmds += "   Payment Mode    :" + pmode.Replace("%20", " ") + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Transaction Type     :" + transactiontype + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Date Deposited       :" + Datedeposited + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Contibution Date     :" + Contributiondate + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     Received By     :" + receivedby + "";
                cmds += NewLine;
                cmds += " --------------------------------------------";
                cmds += NewLine;
                cmds += "   Date            :" + date + " ; " + time.Replace("%20", " ") + "";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += "     HOLISTICALLY EMPOWERING ENTREPRENEURS";
                cmds += NewLine;
                cmds += "  --------------------------------------------";
                cmds += NewLine;
                cmds += "  ------------POWERED BY EASYSACCO--------------";
                cmds += NewLine;
                cmds += " ";
                cmds += ESC + "!" + "0x38"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += NewLine + NewLine;
                cmds += "";
                cmds += NewLine;
                cmds += "";
                cmds += NewLine + NewLine;
                cmds += "";
                cmds += NewLine;
                cmds += "";
                cmds += Convert.ToString((char)27) + "@" + Convert.ToString((char)29) + "V" + (char)1;
                //Create a ClientPrintJob and send it back to the client!
                ClientPrintJob cpj = new ClientPrintJob();
                //set ESC/POS commands to print...
                cpj.PrinterCommands = cmds;
                cpj.FormatHexValues = true;
                //set client printer...
                if (useDefaultPrinter || printerName == "null")
                    cpj.ClientPrinter = new DefaultPrinter();
                else
                    cpj.ClientPrinter = new InstalledPrinter(printerName);
                //send it...
                cpj.SendToClient(Response);
                Response.End();
            }
        }
    }
}