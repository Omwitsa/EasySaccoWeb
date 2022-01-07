using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neodynamic.SDK.Web;
namespace USACBOSA.Reports
{
    public partial class printstmt : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, DR1, DR2, DR3,DR13;
        String txtfile = "";
        Double pric = 0;
        Double qnty = 0;
        Double GPay = 0;
        Double GPay1 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlDataReader DR, DR1, DR2, DR3, DR13;
            Double pric = 0;
            Double qnty = 0;
            Double GPay1 = 0;
            //Is a Print Request?
            string sno = Request.QueryString["1"];
            //string StartDate1 = Request.QueryString["2"].ToString();
            DateTime StartDate1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);
            DateTime today = DateTime.Today;
            DateTime EndDate1 = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            string StartDate2 = StartDate1.ToString("dd-MM-yyyy");
            string EndDate2 = EndDate1.ToString("dd-MM-yyyy");

            string StartDate = Request.QueryString["2"];
            string EndDate = Request.QueryString["3"];
            string auditid = Request.QueryString["4"];
            //WARSOFT.WARMsgBox.Show(StartDate);
            if (WebClientPrint.ProcessPrintJob(Request))
            {
                bool useDefaultPrinter = (Request["useDefaultPrinter"] == "checked");
                string printerName = Server.UrlDecode(Request["printerName"]);
                string ESC = "0x1B"; //ESC byte in hex notation
                string NewLine = "0x0A"; //LF byte in hex notation
                string cut = ESC + "|100P";
                string cmds = ESC + "@"; //Initializes the printer (ESC @)
                cmds += ESC + "!" + "0x00"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "ANGAZA AFRIKA SACCO LIMITED";
                cmds += NewLine;
                cmds += "P.O BOX 10-20403, Mogogosiek.";
                cmds += NewLine;
                cmds += "DETAILED STATEMENT FOR " + DateTime.Parse(EndDate).ToString("MMMM/yyyy");
                //String sno3 = null;
                //DR = new WARTECHCONNECTION.cConnect().ReadDB("SELECT  TOP 1 Trans_Code, Sno   FROM d_Transport WHERE (Sno = '" + sno + "')  ORDER BY auditdatetime DESC");
                //if (DR.HasRows)
                //{
                //    while (DR.Read())
                //    {
                //        if (DR[0].ToString() != "" && DR[0].ToString() != null)
                //        {
                //            sno3 = DR[0].ToString();
                //        }
                //    }
                //}
                //else
                //{
                //    sno3 = "Self";
                //}
                //DR.Close(); DR.Dispose(); DR = null;
                //cmds += NewLine;
                //cmds += "Transporter :" + sno3;
                //cmds += NewLine;
                //cmds += "........................................";
                cmds += NewLine;
                cmds += "Member No :" + sno;
                DR = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDedStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
                if (!DR.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The Member did not contributed for the month specified");
                    return;
                }
                else
                {
                    DR.Read();
                    cmds += NewLine;
                    cmds += "Name :" + DR["NAMES"].ToString();
                    cmds += NewLine;
                    cmds += "........................................";
                    cmds += NewLine;
                    cmds += "DATE           QNTY  PRICE  PAYABLE";
                    cmds += NewLine;
                    cmds += "........................................";
                    DR1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT isnull(SUM(d_Shares.Amnt),0) AS TotalShares FROM d_Shares where d_Shares.Code = '" + sno + "'");
                    pric = Convert.ToDouble(DR["ppu"]);
                    if (DR1.HasRows)
                    {
                        while (DR1.Read())
                        {
                            if (Convert.ToDouble(DR1[0]) > 19999)
                            {
                                pric = pric + 1;
                            }
                        }
                    }
                    DR1.Close(); DR1.Dispose(); DR1 = null;
                    DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDedStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
                    if (DR13.HasRows)
                    {
                        while (DR13.Read())
                        {
                            String tt = DR13["transdate"].ToString();
                            String dt = DateTime.Parse(tt).ToString("dd-MM-yyyy");
                            cmds += NewLine;
                            cmds += "" + dt + "   " + DR13["QSupplied"] + "  " + pric + "   " + pric * Convert.ToDouble(DR13["QSupplied"]) + "";
                            qnty = qnty + Convert.ToDouble(DR["QSupplied"]);
                            GPay1 = GPay1 + (pric * qnty);
                        }
                    }
                    DR13.Close(); DR13.Dispose(); DR13 = null;

                    DR2 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_UpdateGPAYQnty '" + StartDate + "','" + EndDate + "','" + sno + "'");
                    if (DR2.HasRows)
                    {
                        while (DR2.Read())
                        {
                            qnty = Convert.ToDouble(DR2[0]);
                            cmds += NewLine;
                            cmds += "........................................";
                            cmds += NewLine;
                            cmds += "Total Kgs :" + qnty + " Kgs";
                            cmds += NewLine;
                            cmds += "Gross Pay Kshs :" + pric * qnty;
                            cmds += NewLine;
                            cmds += "........................................";
                            GPay1 = pric * qnty;
                            Double subsidy = 0;
                            DR3 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy SELECT     isnull((subsidy),0)   FROM         d_Payroll  WHERE     sno = '" + sno + "' AND endofperiod='" + EndDate + "'");
                            if (DR3.HasRows)
                            {
                                while (DR3.Read())
                                {
                                    subsidy = Convert.ToDouble(DR3[0]);
                                }

                            }
                            DR3.Close(); DR3.Dispose(); DR3 = null;

                            cmds += NewLine;
                            cmds += "Other Income(Subsidy) :" + subsidy + " Kshs";
                            cmds += NewLine;
                            cmds += "Gross + Subsidy Pay Kshs :" + (GPay1 + subsidy);
                            cmds += NewLine;
                            cmds += "........................................";
                            cmds += NewLine;
                            cmds += "DEDUCTIONS";
                            cmds += NewLine;
                            cmds += "........................................";
                            //GPay1 = GPay1 + subsidy;
                        }

                    }
                    DR2.Close(); DR2.Dispose(); DR2 = null;
                    DR = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDeductStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
                    cmds += NewLine;
                    cmds += "DATE        AMOUNT DESCRIPTION";
                    cmds += NewLine;
                    cmds += "........................................";
                    Double TotDeduction = 0;
                    if (DR.HasRows)
                    {
                        while (DR.Read())
                        {
                            String tt1 = DR["date_deduc"].ToString();
                            String dt1 = DateTime.Parse(tt1).ToString("dd-MM-yyyy");
                            cmds += NewLine;
                            cmds += "" + dt1 + " " + Convert.ToDouble(DR["Amount"]).ToString("0.00") + "  " + DR["description"].ToString() + " " + DR["REMARKS"].ToString();
                            TotDeduction = TotDeduction + Convert.ToDouble(DR["Amount"]);
                        }
                    }
                    DR.Close(); DR.Dispose(); DR = null;
                    DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintStmt '" + sno + "','" + EndDate + "'");
                    if (DR13.HasRows)
                    {
                        while (DR13.Read())
                        {
                            cmds += ESC + "!" + "0x00"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                            cmds += NewLine;
                            cmds += "" + EndDate + "  " + DR13["Transport"].ToString() + "Transport";
                            TotDeduction = TotDeduction + Convert.ToDouble(DR13["Transport"]);
                        }

                    }
                    DR13.Close(); DR13.Dispose(); DR13 = null;
                    cmds += NewLine;
                    cmds += "........................................";
                    cmds += NewLine;
                    cmds += "Total Deductions Kshs: " + TotDeduction;
                    cmds += NewLine;
                    cmds += "........................................";
                    cmds += NewLine;
                    cmds += "NET PAY       KSH:" + (GPay1 - TotDeduction);
                    cmds += NewLine;
                    cmds += "........................................";
                    cmds += NewLine;
                    cmds += "BANK DETAILS";
                    cmds += NewLine;
                    cmds += "........................................";
                    DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintStmt '" + sno + "','" + EndDate + "'");
                    if (DR13.HasRows)
                    {
                        DR13.Read();
                        cmds += ESC + "!" + "0x00"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                        cmds += NewLine;
                        cmds += "Bank Name :" + DR13["Bank"].ToString();
                        cmds += NewLine;
                        cmds += "Bank Branch :" + DR13["BBranch"].ToString();
                        cmds += NewLine;
                        cmds += "Account Number:" + DR13["accountnumber"].ToString();
                    }
                    DR13.Close(); DR13.Dispose(); DR13 = null;

                    cmds += NewLine;
                    cmds += "........................................";
                    cmds += NewLine;
                    cmds += "Date :" + DateTime.Now.ToString("dd/mm/yyyy HH:MM:ss ");
                    cmds += NewLine;
                    cmds += " Invest now, for your future";
                    cmds += NewLine;
                    cmds += "---------------------------------------";
                    cmds += NewLine;
                    cmds += "DESIGNED BY AMTECH TECHNOLOGIES LIMITED";
                    cmds += NewLine;
                    cmds += NewLine;
                    cmds += "";
                    cmds += NewLine;
                    cmds += "";
                    cmds += "---------------------------------------";
                    cmds += cut;
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
                }
                string cmds1 = ESC + "@";
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
                    cpj.ClientPrinter = new DefaultPrinter();
                    cpj2.ClientPrinter = new DefaultPrinter();
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

        private void Print()
        {
            throw new NotImplementedException();
        }
        protected void Print23()
        {
            System.Data.SqlClient.SqlDataReader DR, DR1, DR2, DR3, DR13;
            String txtfile = "";
            Double pric = 0;
            Double qnty = 0;
            Double GPay = 0;
            Double GPay1 = 0;
            //Is a Print Request?
            string sno = Request.QueryString["1"];
            string StartDate = Request.QueryString["2"];
            string EndDate = Request.QueryString["3"];
            string auditid = Request.QueryString["4"];


            //bool useDefaultPrinter = (Request["useDefaultPrinter"] == "checked");
            //string printerName = Server.UrlDecode(Request["printerName"]);
            string ESC = "0x1B"; //ESC byte in hex notation
            string NewLine = "0x0A"; //LF byte in hex notation
            string cmds = ESC + "@"; //Initializes the printer (ESC @)
            cmds += ESC + "!" + "0x00"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
            cmds += "      ";
            cmds += "KOKICHE DAIRY CO-OPERATIVE SOCIETY";
            cmds += NewLine;
            cmds += "P.O BOX 10-20403, Mogogosiek.";
            cmds += NewLine;
            cmds += "DETAILED STATEMENT FOR " + DateTime.Now.ToString("MMMM/yyyy");
            String sno3 = null;
            DR = new WARTECHCONNECTION.cConnect().ReadDB("SELECT     TOP 1 Trans_Code, Sno   FROM d_Transport WHERE (Sno = '" + sno + "')  ORDER BY auditdatetime DESC");
            if (DR.HasRows)
            {
                while (DR.Read())
                {
                    if (DR[0].ToString() != "" && DR[0].ToString() != null)
                    {
                        sno3 = DR[0].ToString();
                    }
                    else
                    {
                        sno3 = "Self";
                    }
                }
            }
            DR.Close(); DR.Dispose(); DR = null; 
            cmds += NewLine;
            cmds += "Transporter :" + sno3;
            cmds += NewLine;
            cmds += "........................................";
            cmds += "SNo :" + sno;
            DR = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDedStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
            if (!DR.HasRows)
            {
                WARSOFT.WARMsgBox.Show("The supplier did not supply milk for the month specified");
                return;
            }
            else
            {

                DR.Read();
                cmds += NewLine;
                cmds += "Name :" + DR["NAMES"].ToString();
                cmds += "........................................";
                cmds += NewLine;
                cmds += "DATE   QNTY    PRICE     PAYABLE";
                cmds += "........................................";
                DR1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT isnull(SUM(d_Shares.Amnt),0) AS TotalShares FROM d_Shares where d_Shares.Code = '" + sno + "'");
                pric = Convert.ToDouble(DR["ppu"]);
                if (DR1.HasRows)
                {
                    while (DR1.Read())
                    {
                        if (Convert.ToDouble(DR1[0]) > 19999)
                        {
                            pric = pric + 1;
                        }
                    }
                }
                DR1.Close(); DR1.Dispose(); DR1 = null; 
                DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDedStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
                if (DR13.HasRows)
                {
                    while (DR13.Read())
                    {
                        String tt = DR13["transdate"].ToString();
                        String dt = DateTime.Parse(tt).ToString("dd-MM-yyyy");
                        cmds += NewLine;
                        cmds += "" + dt + "   " + DR13["QSupplied"] + " " + pric + "  " + pric * Convert.ToDouble(DR13["QSupplied"]) + "";
                        qnty = qnty + Convert.ToDouble(DR["QSupplied"]);
                        GPay1 = GPay1 + (pric * qnty);
                    }
                }
                DR13.Close(); DR13.Dispose(); DR13 = null; 
                DR2 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_UpdateGPAYQnty '" + StartDate + "','" + EndDate + "','" + sno + "'");
                if (DR2.HasRows)
                {
                    while (DR2.Read())
                    {
                        qnty = Convert.ToDouble(DR2[0]);
                        cmds += NewLine;
                        cmds += "........................................";
                        cmds += "Total Kgs :" + qnty + " Kgs";
                        cmds += NewLine;
                        cmds += "Gross Pay Kshs :" + pric * qnty;
                        cmds += "........................................";
                        GPay1 = pric * qnty;
                        Double subsidy = 0;
                        DR3 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy SELECT     subsidy   FROM   d_Payroll  WHERE     sno = '" + sno + "' AND endofperiod='" + EndDate + "'");
                        if (DR3.HasRows)
                        {
                            while (DR3.Read())
                            {
                                subsidy = Convert.ToDouble(DR3[0]);
                            }

                        }
                        DR3.Close(); DR3.Dispose(); DR3 = null; 

                        cmds += NewLine;
                        cmds += "Other Income(Subsidy) :" + subsidy + " Kshs";
                        cmds += NewLine;
                        cmds += "Gross + Subsidy Pay Kshs :" + (GPay1 + subsidy);
                        cmds += "........................................";
                        cmds += NewLine;
                        cmds += "DEDUCTIONS";
                        cmds += "........................................";
                        //GPay1 = GPay1 + subsidy;
                    }

                }
                DR2.Close(); DR2.Dispose(); DR2 = null; 
                DR = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintDeductStmt '" + sno + "','" + StartDate + "','" + EndDate + "'");
                cmds += NewLine;
                cmds += "DATE    AMOUNT     DESCRIPTION";
                cmds += "........................................";
                Double TotDeduction = 0;
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        cmds += NewLine;
                        cmds += "" + DR["date_deduc"].ToString() + " " + DR["Amount"].ToString() + " " + DR["description"].ToString() + " " + DR["REMARKS"].ToString();
                        TotDeduction = TotDeduction + Convert.ToDouble(DR["Amount"]);
                    }
                }
                DR.Close(); DR.Dispose(); DR = null;
                DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintStmt '" + sno + "','" + EndDate + "'");
                if (DR13.HasRows)
                {
                    while (DR13.Read())
                    {
                        cmds += ESC + "!" + "0x00"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                        cmds += NewLine;
                        cmds += "" + EndDate + "  " + DR13["Transport"].ToString() + "Transport";
                        TotDeduction = TotDeduction + Convert.ToDouble(DR13["Transport"]);
                    }


                }
                DR13.Close(); DR13.Dispose(); DR13 = null;
                cmds += NewLine;
                cmds += "........................................";
                cmds += "Total Deductions Kshs: " + TotDeduction;
                cmds += NewLine;
                cmds += "........................................";
                cmds += "NET PAY       KSH:" + (GPay1 - TotDeduction);
                cmds += NewLine;
                cmds += "........................................";
                cmds += "BANK DETAILS";
                cmds += NewLine;
                cmds += "........................................";
                DR13 = new WARTECHCONNECTION.cConnect().ReadDB("d_sp_PrintStmt '" + sno + "','" + EndDate + "'");
                if (DR13.HasRows)
                {
                    DR13.Read();

                    cmds += NewLine;
                    cmds += "Bank Name :" + DR13[17].ToString();
                    cmds += NewLine;
                    cmds += "Bank Branch :" + DR13[16].ToString();
                    cmds += NewLine;
                    cmds += "Account Number:" + DR13[15].ToString();
                }
                DR13.Close(); DR13.Dispose(); DR13 = null;

                cmds += NewLine;
                cmds += "........................................";
                cmds += NewLine;
                cmds += "Date :" + DateTime.Now.ToString("dd/mm/yyyy HH:MM:ss AM/PM");
                cmds += NewLine;
                cmds += " Milk is Wealth, Chego ko Makarnatet";
                cmds += NewLine;
                cmds += "---------------------------------------";
                cmds += "DESIGNED BY AMTECH TECHNOLOGIES LIMITED";
                cmds += NewLine;
                cmds += "---------------------------------------";
            }
            ClientPrintJob cpj = new ClientPrintJob();
            //set ESC/POS commands to print...
            cpj.PrinterCommands = cmds;
            cpj.FormatHexValues = true;
            //set client printer...
            cpj.ClientPrinter = new DefaultPrinter();
            //send it...
            cpj.SendToClient(Response);
        }
    }
}