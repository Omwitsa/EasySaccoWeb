using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace USACBOSA.ClientAdmin
{
    public partial class TrialBalance : System.Web.UI.Page
    {
        String accno, transtype, DocumentNo, accType, accGroup, accName, normalbal;
        Double Suspense, ACCBAL, totalcr, totaldr,OpeningBal, OBal;
        Double Debits = 0;
        Double Credits = 0;
        SqlDataReader dr, dr1, dr2,dr3,dr4;
        SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            String username = Session["mimi"].ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dtpStartDat = Convert.ToDateTime(dtpStartDate.Text,System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
            DateTime dtpFinishDat = Convert.ToDateTime(dtpFinishDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
            try
            {

                string Accno = "";
                double suspense = 0;
                //'    OpeningBal As Double
                double Debits = 0;
                double Credits = 0;
                string transtype = ""; string DocumentNo = ""; string accType = ""; string accGroup = "";
                string accName = ""; string AccMainGroup = "";
                if (dtpStartDat > dtpFinishDat)
                {
                    WARSOFT.WARMsgBox.Show("The StartDate should be Earlier than the FinishDate");
                    return;
                }
                new WARTECHCONNECTION.cConnect().WriteDB("Truncate table TBBALANCE");

                string sql = "SELECT Accno,NormalBal,glaccType,glaccname,glaccGroup,GlAccMainGroup FROM GLSETUP ORDER BY ACCNO";
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr4.HasRows)
                    while (dr4.Read())
                    {
                        //prgStatus.Visible = True
                        //lblStatus.Visible = True
                        //lblAccount.Visible = True
                        //prgStatus.Max = 100
                        //'prgStatus.Min = 0
                        //i = 0
                        //While Not .EOF
                        //    DoEvents
                        //    i = i + 1
                        //    lblStatus.Caption = CStr((i / .RecordCount)) * 100 & " %"
                        //    prgStatus.value = Round((i / .RecordCount) * 100, 0)
                        Accno = dr4["Accno"].ToString();
                        //'                If Accno = "901001" Then MsgBox "Here"
                        //----------- lblAccount =dr["Accno"].ToString(); 
                        accType = dr4["Glacctype"].ToString();
                        accGroup = dr4["GLAccGroup"].ToString();
                        accName = dr4["GlAccName"].ToString();
                        AccMainGroup = dr4["GlAccMainGroup"].ToString();

                        OpeningBal = getGlBalance(Accno, dtpStartDat, dtpStartDat);
                        ACCBAL = getGlBalance2(Accno, dtpStartDat, dtpFinishDat);

                        if (Accno == "901005")
                        {
                            WARSOFT.WARMsgBox.Show("HERE");
                        }

                        if (dr4["NormalBal"].ToString() == "Credit")
                        {
                            if (ACCBAL >= 0)
                            {
                                transtype = "CR";
                                ACCBAL = (ACCBAL);
                            }
                            else
                            {
                                transtype = "DR";
                                ACCBAL = (ACCBAL);
                            }
                        }
                        else
                        {
                            if (ACCBAL >= 0)
                            {
                                transtype = "DR";
                                ACCBAL = (ACCBAL);
                            }
                            else
                            {
                                transtype = "CR";
                                ACCBAL = ACCBAL;
                            }
                        }

                        //'save
                        if (ACCBAL != 0)
                        {
                            if (Accno != "901002")//401002
                            {
                                string mydbsql = "Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],OBAL,DR,CR,GlAccMainGroup) Values('" + Accno + "','" + accName + "'," + ACCBAL + ",'" + transtype + "','" + dtpStartDat + "','" + dtpFinishDat + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0," + OpeningBal + "," + totaldr + "," + totalcr + ",'" + AccMainGroup + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(mydbsql);
                            }
                            else
                            {
                                string mydbsql = "Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],OBAL,DR,CR,GlAccMainGroup) Values('" + Accno + "','" + accName + "'," + ACCBAL + ",'" + transtype + "','" + dtpStartDat + "','" + dtpFinishDat + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0," + OpeningBal + "," + totaldr + "," + totalcr + ",'" + AccMainGroup + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(mydbsql);
                            }
                        }
                        //    }
                        //    //  .MoveNext
                        //}
                        //else
                        //{
                        //    //prgStatus.Visible = False
                        //    //lblStatus.Visible = False
                        //    //lblAccount.Visible = False
                        //}

                        dr2 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT (SELECT isnull(SUM(Amount),0) FROM  tbbalance WHERE GlAccMainGroup='ASSETS' and acctype='Balance Sheet') AS Debits, (SELECT     isnull(SUM(Amount),0) FROM  tbbalance WHERE GlAccMainGroup<>'ASSETS' and acctype='Balance Sheet') AS Credits");
                        if (dr2.HasRows)
                            while (dr2.Read())
                            {
                                double Debitsyangu = Convert.ToDouble(dr2["Debits"]);
                                double Creditsyangu = Convert.ToDouble(dr2["Credits"]);
                                if (Debitsyangu > Creditsyangu)
                                {
                                    Credits = Debitsyangu - Creditsyangu;
                                    ACCBAL = Debitsyangu - Creditsyangu;
                                    transtype = "CR";
                                }
                                else
                                {
                                    Debits = Creditsyangu - Debitsyangu;
                                    ACCBAL = Creditsyangu - Debitsyangu;
                                    transtype = "DR";
                                }

                                //'For BalanceSheet Items, check whether they balance
                                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT  isnull((SELECT SUM(Amount) FROM  tbbalance WHERE GlAccMainGroup='ASSETS' and acctype='Balance Sheet'),0) AS Debits, isnull((SELECT     SUM(Amount) FROM  tbbalance WHERE GlAccMainGroup<>'ASSETS' and acctype='Balance Sheet'),0) AS Credits");
                                if (dr1.HasRows)
                                    while (dr1.Read())
                                    {
                                        double mydebits = Convert.ToDouble(dr1["Debits"]);
                                        double mycredits = Convert.ToDouble(dr1["Credits"]);
                                        if (mydebits > mycredits)
                                        {
                                            Credits = mydebits - mycredits;
                                            ACCBAL = mydebits - mycredits;
                                            transtype = "CR";
                                        }
                                        else
                                        {
                                            Debits = mycredits - mydebits;
                                            ACCBAL = mycredits - mydebits;
                                            transtype = "DR";
                                        }
                                    }
                                else
                                {
                                    return;
                                }
                                dr1.Close(); dr1.Dispose(); dr1 = null;

                                if (ACCBAL != 0)
                                {
                                    string PaccName = "";
                                    Accno = "901001";
                                    string glsql = "SELECT glaccname FROM GLSETUP where accno='901001'";
                                    dr3 = new WARTECHCONNECTION.cConnect().ReadDB(glsql);
                                    if (dr3.HasRows)
                                    {
                                        {
                                            PaccName = dr3["glaccname"].ToString();
                                        }
                                    }
                                    dr3.Close(); dr3.Dispose(); dr3 = null;

                                    new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype], [Closed],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],[GlAccMainGroup]) Values('" + Accno + "','" + PaccName + "'," + ACCBAL + ",'" + transtype + "',0,'" + dtpStartDat + "','" + dtpFinishDat + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0,'" + AccMainGroup + "')");
                                }
                            }
                        dr2.Close(); dr2.Dispose(); dr2 = null;
                    }
                else
                {
                    return;
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
                WARSOFT.WARMsgBox.Show("Process Done");
                lblstatus.Text = "Processing Complete";
                lblaccount.Text = "100%";
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }
        private double getGlBalance(string Accno, DateTime dateTime, DateTime dateTime_2)
        {

            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.cbal,gl.GlAccType from dbo.UDF_GL_OpeningBalance ('" + Accno + "','"+dateTime+"') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + Accno + "'");
            if (dr2.HasRows)
                while (dr2.Read())
                {
                    OBal = Convert.ToDouble(dr2["cbal"]);
                    totalcr = Convert.ToDouble(dr2["CR"]);
                    totaldr = Convert.ToDouble(dr2["DR"]);
                }
               dr2.Close(); dr2.Dispose(); dr2 = null;
            return OBal;
        }
        private double getGlBalance2(string Accno, DateTime dateTime, DateTime dateTime_2)
        {

            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.cbal,gl.GlAccType from dbo.UDF_GL_OpeningBalance ('" + Accno + "','" + dateTime_2 + "') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + Accno + "'");
            if (dr2.HasRows)
                while (dr2.Read())
                {
                    OBal = Convert.ToDouble(dr2["cbal"]);
                    totalcr = Convert.ToDouble(dr2["CR"]);
                    totaldr = Convert.ToDouble(dr2["DR"]);
                }
            dr2.Close(); dr2.Dispose(); dr2 = null;
            return OBal;
        }
        private string getGlPeriodicTrans(string accno, string dtstartdate, string dtpFinishDate)
        {
            WARTECHCONNECTION.cConnect balan = new WARTECHCONNECTION.cConnect();
            String sql = "set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.Transbal,gl.GlAccType from UDF_GL_PeriodicTrans ('" + accno + "','" + dtstartdate + "','" + dtpFinishDate + "') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + accno + "'";
            SqlDataReader DRaccnoa = balan.ReadDB(sql);
            if (DRaccnoa.HasRows)
            {
                while (DRaccnoa.Read())
                {
                    OBal = Convert.ToDouble(DRaccnoa["Transbal"]);
                    totalcr = Convert.ToDouble(DRaccnoa["CR"]);
                    totaldr = Convert.ToDouble(DRaccnoa["DR"]);
                }
            }
            DRaccnoa.Close(); DRaccnoa.Dispose(); DRaccnoa = null;
            return OBal.ToString();

        }
        //private double getGlBalance(string accno, string dtstartdate, string dtpFinishDate)
        //{
        //    WARTECHCONNECTION.cConnect balan = new WARTECHCONNECTION.cConnect();
        //    String sql = "set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.cbal,gl.GlAccType from dbo.UDF_GL_OpeningBalance ('" + accno + "','" + dtpFinishDate + "') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + accno + "'";
        //    SqlDataReader DRaccnob = balan.ReadDB(sql);
        //    if (DRaccnob.HasRows)
        //    {
        //        while (DRaccnob.Read())
        //        {
        //            OBal = Convert.ToDouble(DRaccnob["cbal"]);
        //            totalcr = Convert.ToDouble(DRaccnob["CR"]);
        //            totaldr = Convert.ToDouble(DRaccnob["DR"]);
        //        }
        //    }
        //    DRaccnob.Close(); DRaccnob.Dispose(); DRaccnob = null;
        //    return OBal;

        //}
        protected void Button2_Click(object sender, EventArgs e)
        {
            String para = "FEP SACCO SOCIETY LTD";
            Response.Redirect("~/Reports/ReportsViewer.aspx?reportType=Trial Balance&para=" + para + "");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String para = "FEP SACCO SOCIETY LTD";
            Response.Redirect("~/Reports/ReportsViewer.aspx?reportType=incomeexpenditure&para=" + para + "");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            String para = "FEP SACCO SOCIETY LTD";
            Response.Redirect("~/Reports/ReportsViewer.aspx?reportType=BALANCESHEET&para=" + para + "");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            String para = "FEP SACCO SOCIETY LTD";
            Response.Redirect("~/Reports/ReportsViewer.aspx?reportType=CASHFLOW&para=" + para + "");
        }
    }
}