using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace USACBOSA.CustomServAdmin
{
    public struct DateTimeSpan
    {
        private readonly int years;
        private readonly int months;
        private readonly int days;
        private readonly int hours;
        private readonly int minutes;
        private readonly int seconds;
        private readonly int milliseconds;

        public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            this.years = years;
            this.months = months;
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            this.milliseconds = milliseconds;
        }

        public int Years { get { return years; } }
        public int Months { get { return months; } }
        public int Days { get { return days; } }
        public int Hours { get { return hours; } }
        public int Minutes { get { return minutes; } }
        public int Seconds { get { return seconds; } }
        public int Milliseconds { get { return milliseconds; } }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Years;
            DateTimeSpan span = new DateTimeSpan();
            int officialDay = current.Day;

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Years:
                        if (current.AddYears(years + 1) > date2)
                        {
                            phase = Phase.Months;
                            current = current.AddYears(years);
                        }
                        else
                        {
                            years++;
                        }
                        break;
                    case Phase.Months:
                        if (current.AddMonths(months + 1) > date2)
                        {
                            phase = Phase.Days;
                            current = current.AddMonths(months);
                            if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                current = current.AddDays(officialDay - current.Day);
                        }
                        else
                        {
                            months++;
                        }
                        break;
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
                }
            }

            return span;
        }
    }
    public partial class SharetoLoanOffset : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
        double netShares = 0;
        bool isWithdrawable = false;
        string shareAcc = "";
        double interest = 0;
        double intOwed = 0;
        string Loanno = "";
        string intRecovery = "";
        double IntBalalance = 0;
        string mMemberno = "";
        string rmethod = "";
        int rperiod = 0;
        double rrate = 0;
        double initialAmount = 0;
        double LBalance = 0;
        DateTime lastrepay = DateTime.Today;
        DateTime Dateissued = DateTime.Today;
        DateTime duedate = DateTime.Today;
        string LoanCode = "";
        int mdtei = 0;
        double repayrate = 0;
        string RepayMode = "";
        bool wePenalize = false;
        double loanbalance = 0;
        double LastIntowed = 0;
        double RepaidInterest = 0;
        double RepaidPrincipal = 0;
        double RepayableInterest = 0;
        double totalrepayable = 0;
        double Principal = 0;
        int ActionOnInteretDefaulted = 0;
        double mrepayment = 0;
        string transactionNo = "";
        int PaymentNo = 0;
        string penaltyAcc = "";
        string OverpaymentAcc = "";
        double Penalty = 0;
        string LoanAcc = "";
        string ContraAcc = "";
        string interestAcc = "";
        string PremiumAcc = "";
        string sharesCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["mimi"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }

                LoadShareType();
                dtpTransDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }
            loadLoanGuar();
        }

        private void loadLoanGuar()
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                if (AtmSelector.Checked == true)
                {
                    MultiView1.SetActiveView(View2);
                    string readdata = "select guarMemberno [Guar Memberno],GuarNames [Guar Names],Guaramount [Guanteed Amount],GuarBalance [Guaranteed Balance], loanbalance [Loan Balance] from vwloanguarantors n where loanno= '" + row.Cells[1].Text.Trim() + "' and GuarBalance>5";
                    da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
                    DataSet ds = new DataSet();
                    if (da != null)
                    {
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                    break;
                }
            }
        }

        private void LoadShareType()
        {
            cboSharesType.Items.Clear();
            cboSharesType.Items.Add("");
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select sharescode from sharetype order by usedToGuarantee desc");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboSharesType.Items.Add(dr["sharescode"].ToString());
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }
        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        protected void txtMemberNo_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                txtmemberno_Change();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void cboSharesType_Change()
        {
            try
            {
                double AvailableShares = 0;
                double mLoanBalance = 0;
                // 'get his loan balance
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(balance,0) Balance from loanbal where memberno='" + txtMemberNo.Text.Trim() + "' and balance>10");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        txtlbalance.Text = dr1["Balance"].ToString();
                    }
                }
                else
                {
                    txtlbalance.Text = "0";
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                string sql = "";
                if (txtMemberNo.Text == "")
                {
                    sql = "Select sharestype,0,Withdrawable,sharesAcc from sharetype where sharescode='" + cboSharesType.Text.Trim() + "'";
                }
                else
                {
                    sql = "Select st.sharestype,st.withdrawable,st.sharesacc,isnull(sum(s.amount),0) TotalShares from sharetype st right outer join contrib s on s.sharescode=st.sharescode where st.sharescode='" + cboSharesType.Text.Trim() + "' and s.memberno='" + txtMemberNo.Text.Trim() + "' group by st.sharestype,st.withdrawable,st.sharesacc";
                }
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        txtsharetype.Text = dr1["sharestype"].ToString();
                        txtTotalShares.Text = dr1["TotalShares"].ToString();
                        if (netShares < 0)
                        {
                            netShares = 0;
                        }
                        if (Convert.ToDouble(txtTotalShares.Text) < 0)
                        {
                            txtTotalShares.Text = "0";
                        }

                        isWithdrawable = Convert.ToBoolean(dr1["Withdrawable"]);
                        shareAcc = dr1["SharesAcc"].ToString();
                    }
                }
                else
                {
                    txtsharetype.Text = "";
                    txtTotalShares.Text = "0";
                    isWithdrawable = false;
                    shareAcc = "";
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                //'His Pending Loans
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select lb.loanno,lt.loantype,c.amount,lb.balance,lb.intBalance,lb.intrOwed,lt.isMain,isnull(lb.repayperiod,0) rperiod,c.dateissued from loanbal lb inner join loantype lt on lb.loancode=lt.loancode inner join cheques c on lb.loanno=c.loanno where lb.memberno='" + txtMemberNo.Text.Trim() + "' and lb.balance>10");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        mLoanBalance = 0;
                        double balance = Convert.ToDouble(dr1["balance"]);
                        Loanno = dr1["Loanno"].ToString().Trim();
                        CalculateLoanRepayment(Loanno);
                        mLoanBalance = mLoanBalance + balance + interest + intOwed;

                        string oLoanType = dr1["LoanType"].ToString().Trim();
                        double oAmount = Convert.ToDouble(dr1["amount"].ToString().Trim());
                        double obalance = Convert.ToDouble(dr1["balance"].ToString().Trim());
                        double ointerest = 0;
                        double oAvailableshares = 0;
                        double oPpaid = 0;
                        int RemPeriod = 0;
                        if (intRecovery != "All")
                        {
                            ointerest = intOwed + interest;
                        }
                        else
                        {
                            ointerest = IntBalalance;
                        }
                        if (obalance <= 0)
                        {
                            oAvailableshares = 0;
                        }
                        else
                        {
                            if (isWithdrawable == true)
                            {
                                oAvailableshares = netShares;
                            }
                            else
                            {
                                oAvailableshares = ((obalance + interest + intOwed) / mLoanBalance) * netShares;
                            }
                        }
                        oPpaid = (obalance / oAmount) * 100;
                        string oIsmain = "0";
                        bool ismain = Convert.ToBoolean(dr1["isMain"]);
                        if (ismain == true)
                        {
                            oIsmain = "1";
                        }
                        int orperiod = Convert.ToInt32(dr1["rperiod"]);
                        DateTime oDateissued = Convert.ToDateTime(dr1["Dateissued"].ToString());

                        DateTime compareTo = DateTime.Parse(oDateissued.ToString());
                        DateTime now = DateTime.Parse(dtpTransDate.Text.Trim());
                        var dateSpan = DateTimeSpan.CompareDates(compareTo, now);
                        int monthinyear = Convert.ToInt32(dateSpan.Years) * 12;
                        int months = Convert.ToInt32(dateSpan.Months);
                        int oRemPeriod = orperiod - (monthinyear + months);
                        new WARTECHCONNECTION.cConnect().WriteDB("Delete from loanstooffset where Loanno='" + Loanno + "'");
                        string toddatabace = "insert into loanstooffset(Loanno,LoanType,InitialAmount,Balance,Interest,AvailableShares,Ppaid,IsMain,RemPeriod)values('" + Loanno + "','" + oLoanType + "','" + oAmount + "','" + obalance + "','" + ointerest + "','" + oAvailableshares + "','" + oPpaid + "','" + oIsmain + "','" + oRemPeriod + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(toddatabace);
                    }
                }
                LoadtheLoans();
                if (Convert.ToDateTime(dtpTransDate.Text).Day < mdtei)
                {
                    interest = 0;
                }
                if (IntBalalance <= 0)
                {
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select top 1 IsNull(IntrOwed,0)IntrOwed from repay   where loanno ='" + Loanno + "' order by paymentNo desc");
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            intOwed = Convert.ToDouble(dr2["IntrOwed"].ToString());
                            new WARTECHCONNECTION.cConnect().WriteDB("update loanbal set intrOwed =" + intOwed + "  where Loanno ='" + Loanno + "' ");

                            IntBalalance = intOwed;
                        }
                    }
                }
                if (mLoanBalance < Convert.ToDouble(txtTotalShares.Text.Trim()))
                {
                    AvailableShares = mLoanBalance;
                }
                else
                {
                    AvailableShares = Convert.ToDouble(txtTotalShares.Text.Trim());
                }

                txtRPeriod.Text = "0";
                txtCharges.Text = "0";

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadtheLoans()
        {
            string contr = Loanno.Substring(3, 8);
            string readdata = "Select Loanno,LoanType,InitialAmount,Balance,Interest,AvailableShares,Ppaid,IsMain,RemPeriod from loanstooffset Where loanno= '" + DropDownList1.Text + "'";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        private void CalculateLoanRepayment(string Loanno)
        {
            try
            {
                bool AutoCal = false;
                double penaltyOwed = 0; double DefaultedAmount = 0;
                dr3 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + Loanno + "'");
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr3["TotalInterest"]);
                        RepaidPrincipal = Convert.ToDouble(dr3["TotaRepaid"]);
                    }
                }
                dr3.Close(); dr3.Dispose(); dr3 = null;

                dr3 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT  isnull((select top 1 isnull(IntrOwed,0)IntrOwed  from REPAY  where LoanNo =lb.loanno order by PaymentNo  desc),0) as lastintowed,  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,isnull(VD.Arrears,0) DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate, LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO  WHERE (LB.loanno='" + Loanno + "')");
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {

                        mMemberno = dr3["memberno"].ToString();
                        rmethod = dr3["RepayMethod"].ToString();
                        rperiod = Convert.ToInt32(dr3["RPeriod"].ToString());
                        rrate = Convert.ToDouble(dr3["interest"].ToString());
                        initialAmount = Convert.ToDouble(dr3["InitialAmount"].ToString());
                        intOwed = Convert.ToDouble(dr3["intrOwed"].ToString());

                        LBalance = Convert.ToDouble(dr3["Balance"].ToString());
                        lastrepay = Convert.ToDateTime(dr3["LastDate"].ToString());
                        Dateissued = Convert.ToDateTime(dr3["dateissued"].ToString());
                        duedate = Convert.ToDateTime(dr3["duedate"].ToString());
                        LoanCode = dr3["LoanCode"].ToString();
                        mdtei = Convert.ToInt32(dr3["mdtei"].ToString());
                        repayrate = Convert.ToDouble(dr3["RepRate"].ToString());
                        intRecovery = dr3["intRecovery"].ToString();
                        DefaultedAmount = Convert.ToDouble(dr3["DefAmount"]);
                        penaltyOwed = Convert.ToDouble(dr3["PenaltyOwed"].ToString());
                        loanbalance = LBalance;
                        AutoCal = Convert.ToBoolean(dr3["AutoCalc"].ToString());
                        RepayMode = dr3["RepayMode"].ToString();
                        wePenalize = Convert.ToBoolean(dr3["Penalty"].ToString());
                        ////'Penalty = rst("penalty");
                        IntBalalance = Convert.ToDouble(dr3["intBalance"].ToString());
                        LastIntowed = Convert.ToDouble(dr3["lastintowed"]);

                        if (intOwed == 0 && LastIntowed != 0)
                        {
                            intOwed = LastIntowed;
                        }

                        if (rmethod == "AMRT")
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);

                            if (AutoCal == true)
                            {
                                mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                            }
                            else
                            {
                                mrepayment = repayrate;
                            }

                            if (ActionOnInteretDefaulted == 2)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                            }
                            else
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is Accrued
                            }
                            // double opeAMount=Convert.ToDouble(TextBox1.Text);
                            Principal = Math.Round((mrepayment - interest), 2);
                            RepayableInterest = 0;
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
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnLoanTransfer_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void btnGuarTransfer_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }

        protected void btnLoanConsolidation_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View3);
        }

        protected void Offsett_Click(object sender, EventArgs e)
        {
            try
            {
                double allocatedShare = 0;
                double OffSetAmount = 0;
                string Remark = "";
                double totalOffsets = 0;
                double ticked = 0;
                double offset = 0;
                if (TextBox1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must input amount to offset from totalshares");
                }
                if (TextBox2.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must input offset Date");
                }
                if (DropDownList1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must choose Loanno to OFFSET");
                }
                else
                {
                    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        GridViewRow row = GridView1.Rows[i];
                        CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                        if (AtmSelector.Checked == true)
                        {

                            // 'else: Begin Transaction

                            OffSetAmount = 0;
                            offset = Convert.ToDouble(TextBox1.Text);
                            NewTransaction(Convert.ToDouble(txtlbalance.Text.Trim()), Convert.ToDateTime(dtpTransDate.Text.Trim()), "Share-Loan Offsetting - MemberNo -" + txtMemberNo.Text.Trim());
                            Loanno = row.Cells[1].Text;
                            double overpayment = 0;
                            if (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text) < Convert.ToDouble(TextBox1.Text.Trim()))
                            {
                                overpayment = (Convert.ToDouble(TextBox1.Text.Trim())) - (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text));

                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            Loanno = row.Cells[1].Text;
                            if (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text) > Convert.ToDouble(TextBox1.Text.Trim()))
                            {
                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            else
                            {
                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            OffSetAmount = Math.Round(OffSetAmount + allocatedShare, 0);

                            //'pay the loan now
                            //'But first, Get the shares account which becomes the contra acc
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select SharesAcc from sharetype where sharescode='" + cboSharesType.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    shareAcc = dr1["SharesAcc"].ToString().Trim();
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr = null;
                            bool charge = false;
                            bool penalise = false;

                            SaveRepay(Loanno, Convert.ToDateTime(dtpTransDate.Text), Math.Round(allocatedShare, 0), shareAcc, "Share-Loan Offsetting", 0, 1, "Offseting Loanno - " + Loanno, Session["mimi"].ToString(), Session["mimi"].ToString(), transactionNo, charge, penalise);

                            if (Remark == "")
                            {
                                Remark = Loanno;
                            }
                            else
                            {
                                Remark = Remark + " " + Loanno;
                            }

                            //'Less His/Shares now
                            SaveContrib(txtMemberNo.Text.Trim(), Convert.ToDateTime(dtpTransDate.Text.Trim()), cboSharesType.Text.Trim(), OffSetAmount * (-1), "Non-Cash", "Share-Loan Offsetting", "Non-Cash", Session["mimi"].ToString(), "Offseting Loans - " + Remark, transactionNo);

                            //'Offsetting Fees
                            if (txtOffsettingFee.Text == "")
                            {
                                txtOffsettingFee.Text = "0";
                            }
                            if (Convert.ToDouble(txtOffsettingFee.Text) > 0)
                            {

                            }
                            cboSharesType_Change();
                            WARSOFT.WARMsgBox.Show("Offsetting Completed Successfully");
                            return;
                        }
                        else
                        {
                            WARSOFT.WARMsgBox.Show("No item is selected for this action");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void txtmemberno_Change()
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select m.surname + ' ' + m.othernames as membernames from members m  where m.memberno='" + txtMemberNo.Text + "' ");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtMemberNames.Text = dr["membernames"].ToString();
                        //cboSharesType_Change();
                    }
                }
                else
                {

                    txtMemberNames.Text = "";
                    txtTotalShares.Text = "0";
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveContrib(string memberno, DateTime ContrDate, string SHARECode, double contrAmount, string BankAcc, string Receiptno, string chequeno, string transby, string remarks, string transactionNo)
        {
            try
            {
                int RefNo = 0;
                double TotalShares = 0;
                DateTime lastdate = new DateTime();
                DateTime thisDate = ContrDate;

                //'GET THE REFNO

                string sql = "select isnull(sum(contrib.amount),0)contribamount  from contrib where contrib.memberno='" + memberno + "' and contrib.sharescode='" + SHARECode + "' group by contrib.memberno";
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        TotalShares = Convert.ToDouble(dr2["contribamount"].ToString());
                    }
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
                TotalShares = TotalShares + contrAmount;

                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into Contrib(memberno,contrdate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)Values('" + memberno + "','" + ContrDate + "'," + RefNo + "," + contrAmount + "," + TotalShares + ",'" + transby + "','" + chequeno + "','" + Receiptno + "','" + remarks + "','" + Session["mimi"].ToString() + "','" + SHARECode + "','" + transactionNo + "')");
                string shareAcc = "";
                string sqldata = "select isnull(SharesAcc,0)SharesAcc from sharetype where sharescode='" + SHARECode + "'";
                dr3 = new WARTECHCONNECTION.cConnect().ReadDB(sqldata);
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        shareAcc = dr3["SharesAcc"].ToString();
                        if (shareAcc == "0")
                        {
                            WARSOFT.WARMsgBox.Show("The Gl Control Account for this scheme(" + SHARECode.Trim() + ") is not set");
                            return;
                        }
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Invalid Share code(" + SHARECode + ")");
                    return;
                }
                dr3.Close(); dr3.Dispose(); dr3 = null;
                Save_GLTRANSACTION(ContrDate, contrAmount, BankAcc, shareAcc, Receiptno, memberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveRepay(string Loanno, DateTime DateDeposited, double Amountt, string BankAcc, string Receiptno, int Locked, int Posted, string remarks, string auditid, string transby, string transactionNo, bool penalise, bool charge)
        {
            try
            {
                double mPrincipal = 0; double mInterest = 0;
                double intCharged = 0;
                double intPaid = 0;
                double owedPaid = 0;
                string TempAcc = "";
                DateDeposited = Convert.ToDateTime(TextBox2.Text);
                DateTime dreceived = Convert.ToDateTime(TextBox2.Text);
                double overpadAmnt = 0;
                string interestAcc = ""; string ContraAcc = ""; string LoanAcc = "";
                // Today = Get_Server_Date;
                string csql = "select max(IsNull(paymentno,0))PaymentNo from repay where loanno='" + Loanno + "' ";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(csql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        string pno = dr["PaymentNo"].ToString().Trim();
                        if (pno == "" || pno == null)
                        {
                            PaymentNo = 1;
                        }
                        else
                        {
                            PaymentNo = Convert.ToInt32(dr["PaymentNo"]) + 1;
                        }
                    }
                else
                {
                    PaymentNo = 1;
                }
                dr.Close(); dr.Dispose(); dr = null;
                dreceived = DateDeposited;
                CalculateLoanRepayment(Loanno);
                DateDeposited = dreceived;

                intCharged = interest;
                if (RepayMode == "1")
                {
                    mdtei = mdtei;
                }
                else
                {
                    mdtei = 0;
                }

                if (lastrepay.Month == DateDeposited.Month)
                {
                    interest = 0;
                    intCharged = 0;
                }
                // if (DateDeposited.Day <= mdtei && Amountt > 0)    // ' Means he should not this time be charged interest
                //{
                // intCharged = 0;
                // interest = 0;
                // }
                if (Amountt >= Penalty)
                {
                    Amountt = Amountt - Penalty;
                }
                if (Amountt > 0)
                {
                    if (rmethod == "STL")
                    {
                        while (IntBalalance > 0 && Amountt > 0)
                        {
                            if (Amountt > interest)
                            {
                                mInterest = mInterest + interest;
                                Amountt = Amountt - interest;
                                if (Amountt >= Principal)
                                {
                                    if (mPrincipal + Principal > loanbalance)
                                    {
                                        Principal = loanbalance - mPrincipal;
                                    }
                                    mPrincipal = mPrincipal + Principal;
                                    Amountt = Amountt - Principal;
                                }
                                else
                                {
                                    if (mPrincipal + Principal > loanbalance)
                                    {
                                        Principal = loanbalance - mPrincipal;
                                    }
                                    mPrincipal = mPrincipal + Principal;
                                    Amountt = Amountt - Principal;
                                }
                            }
                            else
                            {
                                mInterest = mInterest + Amountt;
                                Amountt = 0;
                            }
                            IntBalalance = IntBalalance - interest;
                        }
                        if (Amountt > 0)
                        {
                            mPrincipal = mPrincipal + Amountt;
                            Amountt = 0;
                        }
                        Principal = mPrincipal;
                        interest = mInterest;
                    }
                    else
                    {
                        if (Amountt > intOwed)
                        {
                            owedPaid = intOwed;
                            Amountt = Amountt - owedPaid;
                            if (Amountt > interest)
                            {
                                interest = Math.Round(interest, 0);
                                Amountt = Amountt - interest;
                                interest = interest + intOwed;
                                intOwed = 0;
                            }
                            // 'check if clearing the loan eg refinancing etc
                            if (Math.Round(Amountt, 0) > Math.Round(loanbalance, 0))
                            {
                                if (Amountt == loanbalance)
                                {
                                    Principal = Principal;
                                }
                                else
                                {
                                    Principal = loanbalance;

                                    overpadAmnt = Amountt - loanbalance;

                                    //  'remaing money to vover recovery
                                    if (overpadAmnt > 0)
                                    {
                                        // 'GL Transactions
                                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loanacc,lt.interestAcc,lt.PremiumAcc,lt.OverpaymentAcc,IsNull(lt.penaltyAcc,'')PenaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                                        if (dr1.HasRows)
                                            while (dr1.Read())
                                            {
                                                PremiumAcc = dr1["PremiumAcc"].ToString();
                                                LoanAcc = dr1["LoanAcc"].ToString();
                                                interestAcc = dr1["InterestAcc"].ToString();
                                                penaltyAcc = dr1["PenaltyAcc"].ToString();
                                                OverpaymentAcc = dr1["OverpaymentAcc"].ToString();
                                            }
                                        else
                                        {
                                            WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                                            return;
                                        }
                                        dr1.Close(); dr1.Dispose(); dr1 = null;
                                        if (LoanAcc == "" || OverpaymentAcc == "" || interestAcc == "" || (Penalty > 0 && penaltyAcc == ""))
                                        {
                                            WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                                        }
                                        ContraAcc = BankAcc;
                                        Save_GLTRANSACTION(dreceived, overpadAmnt, ContraAcc, OverpaymentAcc, Receiptno, mMemberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
                                        goto loansrepay;
                                    }

                                }
                            }
                            else
                            {
                                if (Amountt == loanbalance)
                                {
                                    Principal = Amountt;
                                }
                                else
                                {
                                    // 'remaing money to cover recovery
                                    Amountt = Amountt;
                                    if (Amountt > Principal && loanbalance >= Amountt)
                                    {
                                        Principal = Amountt;
                                    }

                                }
                            }

                        }

                        else
                        {
                            interest = Amountt + owedPaid;
                            Amountt = 0;
                            Principal = 0;
                        }
                        intPaid = interest;
                    }
                }
            ////////////if (Amountt < 0)// 'REVERSAL!
            ////////////{
            ////////////    intPaid = 0;
            ////////////    intOwed = 0;
            ////////////    owedPaid = 0;
            ////////////    Principal = Amountt;
            ////////////    Amountt = 0;
            ////////////}
            ////////////else if (Amountt == 0)
            ////////////{
            ////////////    intOwed = intOwed + intCharged;
            ////////////    intPaid = 0;
            ////////////    owedPaid = 0;
            ////////////    interest = 0;
            ////////////    Principal = 0;
            ////////////}

            loansrepay:
                loanbalance = Math.Round(loanbalance - Principal, 0);

                string sssql = "set dateformat dmy Insert into Repay(Loanno,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo) Values('" + Loanno + "','" + DateDeposited + "'," + PaymentNo + "," + (Principal + interest) + "," + Principal + "," + interest + "," + intCharged + "," + intOwed + "," + Penalty + "," + IntBalalance + "," + loanbalance + ",'" + Receiptno + "','" + transby + "','" + remarks + "','" + auditid + "','" + transactionNo + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sssql);
                string upsql = "set dateformat dmy UPDATE loanbal set balance=" + loanbalance + ",intrOwed=" + intOwed + " ,intBalance=" + IntBalalance + " ,lastdate='" + DateDeposited + "',duedate='" + duedate + "',penalty=" + Penalty + " where loanno='" + Loanno + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(upsql);

                //'GL Transactions

                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.loanAcc,IsNull(lt.penaltyAcc,'')penaltyAcc,lt.ReceivableAcc from loantype lt inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        LoanAcc = dr2["LoanAcc"].ToString();
                        interestAcc = dr2["InterestAcc"].ToString();
                        penaltyAcc = dr2["PenaltyAcc"].ToString();
                    }
                else
                {
                    WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                    return;
                }
                dr2.Close(); dr2.Dispose(); dr = null;
                if (LoanAcc == "" || interestAcc == "" || (Penalty > 0) && penaltyAcc == "")
                {
                    WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                }

                ContraAcc = BankAcc;

                if (interest > 0)
                {
                    Save_GLTRANSACTION(dreceived, interest, ContraAcc, interestAcc, Receiptno, mMemberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
                }

                if (Penalty > 0)
                {
                    Save_GLTRANSACTION(dreceived, Penalty, ContraAcc, penaltyAcc, Receiptno, mMemberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);

                }

                if (Principal > 0)
                {
                    Save_GLTRANSACTION(dreceived, Principal, ContraAcc, LoanAcc, Receiptno, mMemberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
                }
                // RefreshGuarantors(Loanno) ;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRaccno, string Craccno, string DocumentNo, string Source, string auditid, string TransDescription, int CashBook, int doc_posted, string chequeno, string transactionNo)
        {
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + Source + "','" + auditid + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','Bosa'");
        }

        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description)
        {
            DateTime TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," + AmountPaid + ",'" + Session["mimi"].ToString() + "','" + TransDate + "','" + Description + "','Posted')";
            new WARTECHCONNECTION.cConnect().WriteDB(sql);
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {

        }

        protected void cboTLoanCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboAccno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Dev0_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboLCAccno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnConsolidate_Click(object sender, EventArgs e)
        {

        }

        protected void cboSharesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loans();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnGuarOffset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                if (AtmSelector.Checked == true)
                {
                    Loanno = row.Cells[1].Text;
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("No pending Loan is selected for this action");
                    return;
                }
            }
            int rowCount = GridView2.Rows.Count;
            if (rowCount == 0)
            {
                WARSOFT.WARMsgBox.Show("There is no existing guarantor to be offsetted for this loan");
                return;
            }

            if (Convert.ToDouble(txtTotalShares.Text) > 0)
            {
                WARSOFT.WARMsgBox.Show("The member has some Deposits with which he/she can offset this loan.");
                return;
            }
            string memberno = GridView2.SelectedRow.Cells[1].Text;
            double GBalance = Convert.ToDouble(GridView2.SelectedRow.Cells[3].Text);
            if (Convert.ToDouble(txtlbalance.Text) < GBalance)
            {
                GBalance = Convert.ToDouble(txtlbalance.Text);
            }
            NewTransaction(Convert.ToDouble(txtlbalance.Text), Convert.ToDateTime(dtpTransDate.Text), "GuarLoanOffsetting");
            if (memberno != "")
            {
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB("Select SharesAcc from sharetype where sharescode='" + cboSharesType.Text + "'");
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        shareAcc = dr4["SharesAcc"].ToString().Trim();
                        sharesCode = cboSharesType.Text;
                    }
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
                bool penalise = false; bool charge = false;
                SaveRepay(Loanno, Convert.ToDateTime(dtpTransDate.Text), GBalance, shareAcc, "GuarOffset", 0, 1, "Guarantor Offsetting by " + memberno, Session["mimi"].ToString(), "Offset", transactionNo, penalise, charge);

                dr4 = new WARTECHCONNECTION.cConnect().ReadDB("Select lt.loanAcc from loantype lt inner join loanbal lb on lb.loancode=lt.loancode where lb.loanno='" + Loanno + "'");
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        LoanAcc = dr4["loanAcc"].ToString();
                    }
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
                SaveContrib(memberno, Convert.ToDateTime(dtpTransDate.Text), sharesCode, Convert.ToDouble(GBalance) * (-1), LoanAcc, "GuarOffset", "GuarOffset", "Offset", "Guarantor Offsetting to " + Loanno, transactionNo);
            }
            txtmemberno_Change();
            WARSOFT.WARMsgBox.Show("Guarantor Offsetting Successful");
            return;
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double total;
                double totalshares = 0;
                totalshares = Convert.ToDouble(txtTotalShares.Text);
                double offsetamount = Convert.ToDouble(TextBox1.Text);
                total = totalshares - offsetamount;
                txtTotalShares.Text = Convert.ToString(total);

            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                double allocatedShare = 0;
                double OffSetAmount = 0;
                string Remark = "";
                double totalOffsets = 0;
                double ticked = 0;
                double offset = 0;
                if (TextBox1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must input amount to offset from totalshares");
                }
                if (TextBox2.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must input offset Date");
                }
                else
                {
                    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        GridViewRow row = GridView1.Rows[i];
                        CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                        if (AtmSelector.Checked == true)
                        {

                            // 'else: Begin Transaction

                            OffSetAmount = 0;
                            offset = Convert.ToDouble(TextBox1.Text);
                            NewTransaction(Convert.ToDouble(txtlbalance.Text.Trim()), Convert.ToDateTime(dtpTransDate.Text.Trim()), "Share-Loan Offsetting - MemberNo -" + txtMemberNo.Text.Trim());
                            Loanno = row.Cells[1].Text;
                            double overpayment = 0;
                            if (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text) < Convert.ToDouble(TextBox1.Text.Trim()))
                            {
                                overpayment = (Convert.ToDouble(TextBox1.Text.Trim())) - (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text));

                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            Loanno = row.Cells[1].Text;
                            if (Convert.ToDouble(row.Cells[4].Text) + Convert.ToDouble(row.Cells[5].Text) > Convert.ToDouble(TextBox1.Text.Trim()))
                            {
                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            else
                            {
                                allocatedShare = Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            OffSetAmount = Math.Round(OffSetAmount + allocatedShare, 0);

                            //'pay the loan now
                            //'But first, Get the shares account which becomes the contra acc
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select SharesAcc from sharetype where sharescode='" + cboSharesType.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    shareAcc = dr1["SharesAcc"].ToString().Trim();
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr = null;
                            bool charge = false;
                            bool penalise = false;

                            SaveRepay(Loanno, Convert.ToDateTime(dtpTransDate.Text), Math.Round(allocatedShare, 0), shareAcc, "Share-Loan Offsetting", 0, 1, "Offseting Loanno - " + Loanno, Session["mimi"].ToString(), Session["mimi"].ToString(), transactionNo, charge, penalise);

                            if (Remark == "")
                            {
                                Remark = Loanno;
                            }
                            else
                            {
                                Remark = Remark + " " + Loanno;
                            }

                            //'Less His/Shares now
                            SaveContrib(txtMemberNo.Text.Trim(), Convert.ToDateTime(dtpTransDate.Text.Trim()), cboSharesType.Text.Trim(), OffSetAmount * (-1), "Non-Cash", "Share-Loan Offsetting", "Non-Cash", Session["mimi"].ToString(), "Offseting Loans - " + Remark, transactionNo);

                            //'Offsetting Fees
                            if (txtOffsettingFee.Text == "")
                            {
                                txtOffsettingFee.Text = "0";
                            }
                            if (Convert.ToDouble(txtOffsettingFee.Text) > 0)
                            {

                            }
                            cboSharesType_Change();
                            WARSOFT.WARMsgBox.Show("Offsetting Completed Successfully");
                            return;
                        }
                        else
                        {
                            WARSOFT.WARMsgBox.Show("No item is selected for this action");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void loans()
        {
            DropDownList1.Items.Add("");
            WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
            string GL = "Select lb.loanno,lb.Balance+lb.intrOwed as Balance from loanbal lb  where memberno='" + txtMemberNo.Text + "' and balance>10";
            dr = GLS.ReadDB(GL);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DropDownList1.Items.Add("" + dr["loanno"].ToString() + "");
                }
            }
            dr.Close(); dr.Dispose(); dr = null; GLS.Dispose(); GLS = null;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSharesType_Change();
        }
    }
}

