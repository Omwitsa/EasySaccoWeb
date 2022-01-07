using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.General_Ledgers
{
    public partial class TrialBalance : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4;
        System.Data.SqlClient.SqlDataAdapter da;
        double totalCr = 0;
        double totalDr = 0;
        double OpeningBal = 0;
        double ACCBAL = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ScriptManager.GetCurrent(Page) == null)
            //{
            //    Page.Form.Controls.AddAt(0, new ScriptManager());
            //}
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string Accno = "";
                double suspense = 0;
                //'    OpeningBal As Double
                double Debits = 0;
                double Credits = 0;
                string transtype = ""; string DocumentNo = ""; string accType = ""; string accGroup = "";
                string accName = ""; string AccMainGroup = "";
                if (Convert.ToDateTime(dtpStartDate.Text) > Convert.ToDateTime(dtpFinishDate.Text))
                {
                    WARSOFT.WARMsgBox.Show("The StartDate should be Earlier than the FinishDate");
                    return;
                }
                new WARTECHCONNECTION.cConnect().WriteDB("Truncate table TBBALANCE");

                string sql = "SELECT Accno,NormalBal,glaccType,glaccname,glaccGroup,GlAccMainGroup FROM GLSETUP  ORDER BY ACCNO";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
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
                        Accno = dr["Accno"].ToString();
                        //'                If Accno = "901001" Then MsgBox "Here"
                        //----------- lblAccount =dr["Accno"].ToString(); 
                        accType = dr["Glacctype"].ToString();
                        accGroup = dr["GLAccGroup"].ToString();
                        accName = dr["GlAccName"].ToString();
                        AccMainGroup = dr["GlAccMainGroup"].ToString();

                        getGlBalance(Accno, Convert.ToDateTime(dtpStartDate.Text), Convert.ToDateTime(dtpStartDate.Text));
                        getGlBalance(Accno, Convert.ToDateTime(dtpStartDate.Text), Convert.ToDateTime(dtpFinishDate.Text));

                        if (Accno == "901005")
                        {
                            WARSOFT.WARMsgBox.Show("HERE");
                        }

                        if (dr["NormalBal"].ToString() == "Debit")
                        {
                            if (ACCBAL >= 0)
                            {
                                transtype = "DR";
                                ACCBAL = -1 * (ACCBAL);
                            }
                            else
                            {
                                transtype = "CR";
                                ACCBAL = -1 * (ACCBAL);
                            }
                        }
                        else
                        {
                            if (ACCBAL >= 0)
                            {
                                transtype = "CR";
                                ACCBAL = -1 * (ACCBAL);
                            }
                            else
                            {
                                transtype = "DR";
                                ACCBAL = ACCBAL;
                            }
                        }

                        //'save
                        if (ACCBAL != 0)
                        {
                            if (Accno != "901002")
                            {
                                string mydbsql = "Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],OBAL,DR,CR,GlAccMainGroup) Values('" + Accno + "','" + accName + "'," + ACCBAL + ",'" + transtype + "','" + Convert.ToDateTime(dtpStartDate.Text) + "','" + Convert.ToDateTime(dtpFinishDate.Text) + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0," + OpeningBal + "," + totalDr + "," + totalCr + ",'" + AccMainGroup + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(mydbsql);
                            }
                            else
                            {
                                string mydbsql = "Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],OBAL,DR,CR,GlAccMainGroup) Values('" + Accno + "','" + accName + "'," + ACCBAL + ",'" + transtype + "','" + Convert.ToDateTime(dtpStartDate.Text) + "','" + Convert.ToDateTime(dtpFinishDate.Text) + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0," + OpeningBal + "," + totalDr + "," + totalCr + ",'" + AccMainGroup + "')";
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
                        {
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
                                {
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

                                    new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY INSERT INTO [tbbalance] ([AccNo],[AccName], [Amount],[Transtype], [Closed],[StartDate], [EndDate], [AuditID], [AccType], [AccGroup], [BudgetAmount],[GlAccMainGroup]) Values('" + Accno + "','" + PaccName + "'," + ACCBAL + ",'" + transtype + "',0,'" + Convert.ToDateTime(dtpStartDate.Text) + "','" + Convert.ToDateTime(dtpFinishDate.Text) + "','" + Session["mimi"].ToString() + "','" + accType + "','" + accGroup + "',0,'" + AccMainGroup + "')");
                                }
                            }
                            WARSOFT.WARMsgBox.Show("Process Done");
                        }
                    }
                    //  .MoveNext
                }
                else
                {
                    //prgStatus.Visible = False
                    //lblStatus.Visible = False
                    //lblAccount.Visible = False
                }
            }
            catch (Exception ex) { ex.Data.Clear(); }
        }

        private void getGlBalance(string Accno, DateTime StartDate, DateTime EndDate)
        {
            double OBal = 0;
            totalCr = 0;
            totalDr = 0;
            //StartDate = Convert.ToDateTime(StartDate.ToString("DD/MM/YYYY"));
            //EndDate = Convert.ToDateTime(EndDate.ToString("DD/MM/YYYY"));

            string aaasql = "set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.cbal,gl.GlAccType,op.oBal from dbo.UDF_GL_OpeningBalance ('" + Accno + "','" + EndDate + "') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + Accno + "'";
            dr4 = new WARTECHCONNECTION.cConnect().ReadDB(aaasql);
            if (dr4.HasRows)
            {
                while (dr4.Read())
                {
                    OBal = Convert.ToDouble(dr4["cbal"]);
                    OpeningBal = OBal;
                    ACCBAL = OBal;
                    totalDr = Convert.ToDouble(dr4["DR"]);
                    totalCr = Convert.ToDouble(dr4["CR"]);
                }
            }
            dr4.Close(); dr4.Dispose(); dr4 = null;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Trialbalance.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Incomestatement.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Balancesheet.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/cashflow.aspx");
        }
    }
}
    