using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace USACBOSA.ClientAdmin
{

    public partial class LoansInquiry : System.Web.UI.Page
    {
        SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, dr6;
        SqlDataAdapter da;
        String format2;
        public Double totalDr, totalCr = 0;
        public Double OpeningBal = 0;
        public Double Principal = 0;
        public Double mrepayment = 0;
        public Double intReceivable = 0;
        public Double IntrOwed = 0;
        public Boolean success = false;
        public Double LBalance = 0;
        public Double rpInterest = 0;
        public Double totalrepayable = 0;
        public Double RepayableInterest = 0;
        public int rperiod = 0;
        public DateTime lastdate;
        public Double rrate = 0;
        public Double initialAmount = 0;
        public string transactionNo = "";
        public Double transactionTotal, loanbalance = 0;
        public Double PrincAmount, IntrAmount, intTotal = 0;
        public int ActionOnInteretDefaulted = 0;
        public string mMemberno = "";
        public double IntBalalance = 0;
        public string memStatus = "";
        public DateTime FirstDate; public DateTime nextdate;
        public double interest = 0;
        public int RepayMode = 0;
        public double Penalty = 0;
        public double intOwed = 0;
        public DateTime duedate = DateTime.Today;
        public DateTime duedate2;
        public DateTime myDate;
        public bool NewMember = false;
        public int loops = 0;
        public string rmethod = "";
        public string intRecovery = "";
        public int mdtei = 0;
        public DateTime lastrepay;
        public DateTime Dateissued;
        public double IntrCharged = 0;
        public int daysIntoTheMonth = 0;
        public bool saveToGl = false;
        public string sharesCode = "";
        public double Amount = 0;
        public DateTime serverDate;
        public string penaltyAcc = "";
        public string PremiumAcc = "";
        public bool wePenalize = false;

        public double LoanPrincipal = 0;
        public double InterestCharged = 0;
        public string repaymethod = "";
        public double repayrate = 0;
        public double InterestPaid = 0;
        public string LoanCode = "";
        public double period = 0;
        public int PaymentNo = 0;
        public double LoanAmount = 0;
        public double InterestRate = 0;
        public string fromOrg = "";
        public string toOrg = "";

        bool AutoCal = false;
        double penaltyOwed, DefaultedAmount = 0;
        double RepaidInterest, RepaidPrincipal = 0;
        double CurrentTotalDeductions = 0;
        double TotalPrinciple = 0;
        double totalinterest = 0;
        private double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -0;
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                this.txtLoanNo.Text = GridView1.SelectedRow.Cells[1].Text;
                LoadSchedule();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadSchedule()
        {
            try
            {
                WARTECHCONNECTION.cConnect schedule = new WARTECHCONNECTION.cConnect();
                dr2 = schedule.ReadDB("select c.amount as InitAmount,lb.Balance as balance,R.LoanBalance,lb.intbalance,R.intBalance,R.datereceived,R.receiptno,R.remarks,R.principal,R.Interest,R.penalty,r.intrOwed as intOwed,R.IntrCharged,R.repayId,r.auditid,r.audittime from loanbal lb inner join cheques c on lb.loanno=c.loanno right outer join Repay r on lb.loanno=r.loanno where lb.loanno='" + txtLoanNo.Text + "' ORDER BY R.DATERECEIVED DESC,repayid desc");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        txtInitAmount.Text = dr2["InitAmount"].ToString();
                        txtBalp.Text = Convert.ToDouble(dr2["balance"].ToString()).ToString();
                        double bal = Convert.ToDouble(dr2["balance"].ToString());
                        double intOwed = Convert.ToDouble(dr2["intOwed"].ToString());
                        double intbal = Convert.ToDouble(dr2["intbalance"].ToString());
                        txtBalppi.Text = ((bal + intOwed + intbal).ToString());
                        break;

                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; schedule.Dispose(); schedule = null;
                try
                {
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("select R.datereceived [Date Received],R.principal+R.Interest [Total Received],R.receiptno [Receipt No],R.principal,R.Interest,R.IntrCharged [Int Charged],r.intrOwed as [Int Owed],R.LoanBalance as Balance,R.IntBalance [Int Balance],r.penalty [penalty] ,R.remarks [Narration],r.auditid [Audit Id],r.audittime [Audit Time] from loanbal lb inner join cheques c on lb.loanno=c.loanno right outer join Repay r on lb.loanno=r.loanno where lb.loanno='" + txtLoanNo.Text + "' ORDER BY R.DATERECEIVED DESC,repayid desc");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView1.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            LoadMemberDetails();
        }

        private void LoadMemberDetails()
        {
            try
            {
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB("select m.surname,m.othernames,M.CompanyCode , m.Dormant, m.Withdrawn, m.Archived,isnull(sum(s.TotalShares),0) as TotalShares from members m left outer join vwmembershares s on m.memberno=s.memberno where m.memberno='" + txtMemberNo.Text + "' Group by  m.memberno,m.surname,m.othernames,M.CompanyCode , m.Dormant, m.Withdrawn, m.Archived");
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtNames.Text = dr["surname"].ToString() + dr["othernames"].ToString();
                        txtTShares.Text = dr["TotalShares"].ToString();

                        WARTECHCONNECTION.cConnect LoanBal = new WARTECHCONNECTION.cConnect();
                        dr1 = LoanBal.ReadDB("SELECT MemberNo, COUNT(MemberNo) AS LoanCount, ISNULL(SUM(Balance), 0) AS Balance FROM  VWMEMBERLOANS WHERE (MemberNo = '" + txtMemberNo.Text + "') and balance>0 GROUP BY MemberNo");
                        if (dr1.HasRows)
                            while (dr1.Read())
                            {
                                double bal = Convert.ToDouble(dr1["Balance"]);
                                if (bal > 0)
                                {
                                    txtOLoanBal.Text = dr1["Balance"].ToString();
                                    txtOLoans.Text = dr1["LoanCount"].ToString();
                                }
                                else
                                {
                                    txtOLoanBal.Text = "0";
                                    txtOLoans.Text = "0";
                                }
                            }
                        dr1.Close(); dr1.Dispose(); dr1 = null; LoanBal.Dispose(); LoanBal = null;
                        getMemberLoans();
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getMemberLoans()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("Select l.loanno [Loan No],c.dateissued [Date Issued],c.amount [Amount],lb.lastdate [Last Date],lb.intBalance [Int Balance],isnull(l.repayPeriod,lb.repayPeriod) as [Repay Period],lb.balance Balance,lb.loancode [Loan Code],lb.introwed [Int Owed],lt.loantype [Loan Type] from cheques c inner join loanbal lb on c.loanno=lb.loanno inner join Loantype lt on lb.loancode=lt.loancode left outer join loans l on lb.loanno=l.loanno LEFT OUTER JOIN REPAY RP on RP.loanno=lB.loanno where lb.memberno='" + txtMemberNo.Text + "' and lb.balance>0 GROUP BY l.loanno,c.dateissued,c.amount,c.DateIssued,lb.lastdate,lb.intBalance,lb.lastdate, isnull(l.repayPeriod,lb.repayPeriod),lb.loanno,lb.balance,lb.loancode,lb.introwed,lt.loantype order by lb.balance desc,c.dateissued desc");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlDataAdapter da;

            try
            {
                Session["Memberno"] = txtMemberNo.Text;
                Response.Redirect("~/Reports/LoansStatement.aspx", false);
            }
            catch (Exception ex)
            {

                ex.Data.Clear();

            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                 double InterestCharged = 0; Double totalrepayable = 0; Double RepayableInterest = 0;
                 int PAYNO; int PAYNO2; string loanno;
                int MyPosition; Double Interestbalance = 0;
                Double LoanAmount = 0; double IntAmount = 0; Double perc; double paidbf = 0;
                String memberno; Double LBalance = 0;
                double Amount = 0; double intOwed = 0; string rmethod = ""; double rrate = 0;
                string Receiptno;
                double repayid;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select loanno from loanbal where loanno='" + txtLoanNo.Text + "'");//
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        loanno = dr["loanno"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select interest,memberno from loanbal where loanno='" + loanno + "'");//
                        if (dr1.HasRows)
                            while (dr1.Read())
                            {
                                memberno = dr1["memberno"].ToString();
                                perc = Convert.ToDouble(dr1["interest"]);
                                if (perc == null)
                                {
                                    perc = 0;
                                    perc = 12;
                                }
                                if (perc == 0)
                                {
                                    perc = 12;
                                }
                            }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select C.Amount,C.DateIssued,isnull(C.balance,0)balance, c.paidbf,0 introwed,le.interest repayrate,le.repaymethod,le.repayperiod from Cheques C inner join loanbal le ON LE.LOANNO=C.LOANNO where c.LoanNo='" + loanno + "'");//
                        if (dr2.HasRows)
                            while (dr2.Read())
                            {
                                initialAmount = Convert.ToDouble(dr2["Amount"]);
                                LBalance = initialAmount;
                                lastdate = Convert.ToDateTime(dr2["Dateissued"]);
                                paidbf = Convert.ToDouble(dr2["paidbf"]);
                                if (paidbf == null)
                                {
                                    paidbf = 0;
                                }
                                intOwed = Convert.ToDouble(dr2["introwed"]);
                                rmethod = dr2["repaymethod"].ToString();
                                rperiod = Convert.ToInt32(dr2["repayperiod"]);
                                rrate = Convert.ToDouble(dr2["repayrate"]);
                            }
                        dr2.Close(); dr2.Dispose(); dr2 = null;
                        double OpeningBal;
                        OpeningBal = LBalance;
                        PAYNO = 0;
                        PAYNO2 = 0;
                        repayid = 0;
                        dr6 = new WARTECHCONNECTION.cConnect().ReadDB("Select * From REPAY where LoanNo='" + loanno + "' order by DateReceived,RepayID");//
                        if (dr6.HasRows)
                            while (dr6.Read())
                            {
                                PAYNO = PAYNO + 1;
                                repayid = Convert.ToDouble(dr6["repayid"]);
                                lastdate = Convert.ToDateTime(dr6["datereceived"]);
                                PAYNO2 = Convert.ToInt32(dr6["paymentno"]);
                                Amount = Convert.ToDouble(dr6["Amount"]);
                                Receiptno = dr6["Receiptno"].ToString();
                                //Principal = Convert.ToDouble(dr6["principal"]);

                                dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select introwed from loanbal where loanno='" + loanno + "'");//
                                if (dr3.HasRows)
                                    while (dr3.Read())
                                    {
                                        intOwed = Convert.ToDouble(dr3["introwed"]);
                                        //AutoCal = Convert.ToBoolean(dr3["autocalc"]);
                                        if (rmethod == "AMRT")
                                        {
                                            totalrepayable = rperiod * Pmt(rrate/12/100, rperiod, initialAmount, 0);
                                            mrepayment = Math.Round(Pmt(rrate/12/100, rperiod, initialAmount, 0), 2);                                           
                                            if (ActionOnInteretDefaulted == 1)
                                            {
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                                            }
                                            else
                                            {
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is Accrued
                                            }

                                            Principal = Math.Round((mrepayment - interest), 2);
                                            // RepayableInterest = 0;
                                            RepayableInterest = totalrepayable - initialAmount;
                                            IntBalalance = RepayableInterest - RepaidInterest;
                                        }
                                        if (rmethod == "STL")
                                        {
                                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                                            Principal = initialAmount / rperiod;
                                            interest = (initialAmount * (rrate / 100 / rperiod));
                                            RepayableInterest = interest * rperiod;
                                            mrepayment = Principal + interest;
                                            if (interest >= IntBalalance)
                                            {
                                                if (IntBalalance < 0)
                                                {
                                                    interest = 0;
                                                }
                                                else
                                                {
                                                    interest = IntBalalance;
                                                }
                                            }
                                        }
                                        if (rmethod == "RBAL")
                                        {
                                            totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                                            Principal = initialAmount / rperiod;
                                            if (ActionOnInteretDefaulted == 1)
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0); //'Interest owed is loaded
                                            else //'Accrue
                                            {
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is accrued
                                            }
                                            if (AutoCal == false)
                                            {
                                                mrepayment = repayrate;
                                                Principal = mrepayment - interest;
                                            }

                                            RepayableInterest = 0;//'unpredictable
                                        }
                                        if (rmethod == "RSPECIAL")
                                        {
                                            double intTotal = 0;
                                            double actualInterest = Math.Round(rrate / 12 / 100 * (LBalance), 0);
                                            double PrincAmount = 0;
                                            LBalance = initialAmount;
                                            for (int i = 1; i <= rperiod; i++)
                                            {
                                                Principal = initialAmount / rperiod;
                                                interest = (rrate / 12 / 100) * LBalance;
                                                intTotal = intTotal + interest;
                                                LBalance = LBalance - Principal;
                                            }
                                            interest = intTotal / rperiod;
                                            RepayableInterest = 0;
                                            LBalance = loanbalance;// 'to continue with the previous flow
                                        }
                                        if (rmethod == "RSTL")
                                        {
                                            totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                                            Principal = initialAmount / rperiod;
                                            if (ActionOnInteretDefaulted == 1)
                                            {
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0);// 'Interest owed is loaded
                                            }
                                            else // 'Accrue
                                            {
                                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0); //'Interest owed is accrued
                                            }

                                            RepayableInterest = 0; //'unpredictable
                                        }
                                        if (rmethod == "ADV")
                                        {
                                            totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                                            Principal = initialAmount / rperiod;
                                            interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                                            RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                                        }
                                        if (Principal > LBalance)
                                        {
                                            Principal = LBalance;
                                        }
                                    }
                                dr3.Close(); dr3.Dispose(); dr3 = null;
                                if (PAYNO > 1)
                                {
                                    if ((lastdate.Month == myDate.Month) && (lastdate.Year == myDate.Year))
                                    {
                                        IntrCharged = Convert.ToDouble(dr6["IntrCharged"]);
                                        IntrCharged = 0;
                                    }
                                    else
                                    {
                                        IntrCharged = interest;
                                    }
                                }
                                else
                                {
                                    IntrCharged = interest;
                                }
                                if (Amount >= (intOwed + IntrCharged))
                                {
                                    interest = (intOwed + IntrCharged);
                                    intOwed = 0;
                                }
                                else if (Amount > intOwed)
                                {
                                    interest = Amount;
                                    intOwed = (intOwed + IntrCharged) - interest;
                                }
                                else if (Amount > 0)
                                {
                                    interest = Amount;
                                    intOwed = intOwed + (IntrCharged - interest);
                                }
                                else
                                {
                                    intOwed = intOwed + (IntrCharged - interest);
                                    interest = 0;
                                }
                                Interestbalance = Interestbalance + IntrOwed;
                                myDate = lastdate;
                                Principal = Amount - interest;
                                LBalance = LBalance - Principal;
                                string updateRepay = "set dateformat dmy Update REPAY set paymentNo='" + PAYNO + "',principal='" + Principal + "',intrcharged='" + IntrCharged + "',loanbalance=" + LBalance + ",interest='" + interest + "',amount='" + Amount + "',introwed=" + intOwed + " where loanno='" + loanno + "' and repayid='"+repayid+"'";
                                new WARTECHCONNECTION.cConnect().WriteDB(updateRepay);
                              
                            }
                        dr6.Close(); dr6.Dispose(); dr6 = null;
                        if ((interest < 0) || (Principal < 0))
                        {
                            WARSOFT.WARMsgBox.Show("interest and principal is less than zero");
                            return;
                        }
                        else
                        {
                            string updateloanbal = "set dateformat dmy Update loanbal set balance=" + LBalance + ",introwed=" + intOwed + ",lastdate='" + lastdate + "' where loanno='" + loanno + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updateloanbal);
                            WARSOFT.WARMsgBox.Show("Data has been successfully Refreshed");
                            return;
                        }
                    }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FinanceAdmin/LoanSchedule.aspx");
        }
    }
}