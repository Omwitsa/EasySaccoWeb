using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace USACBOSA.FinanceAdmin
{
    public partial class LoanEndorsements : System.Web.UI.Page
    {
        SqlDataReader DR, dr, dr1, dr2, dr3, dr4, dr5;
        SqlDataAdapter da;

        Double insurancefee, Processingfee;
        bool AutoCal = false;
        double penaltyOwed, DefaultedAmount = 0;
        double RepaidInterest, RepaidPrincipal = 0;
        double totalrepayable = 0;
        double mrepayment = 0;
        double RepayableInterest = 0;
        double Principal = 0;
        double interest = 0;
        int ActionOnInteretDefaulted = 0;
        double CurrentTotalDeductions = 0;
        double TotalPrinciple = 0;
        double totalinterest = 0;
        double defPenalty = 0;
        double RloanAmt = 0;
        double IntRecovered = 0;
        bool isTopUp = false;
        string rmethod = "";
        string mMemberno = "";
        int rperiod = 0;
        double rrate = 0;
        double initialAmount = 0;
        double intOwed = 0;
        double LBalance = 0;
        DateTime lastrepaydate = new DateTime();
        DateTime duedate = DateTime.Today.AddMonths(1);

        string LoanCode = "";
        int mdtei = 0;
        double repayrate = 0;
        string intRecovery = "";

        double loanbalance = 0;
        string RepayMode = "";
        bool wePenalize = false;
        double IntBalalance = 0;
        string transactionNo = "";
        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
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
                MultiView1.ActiveViewIndex = -0;
                LoadAppraisedLoans();
                getLoanRatio();
                loadthebanks();
                LoadGls();
            }
        }

        private void LoadGls()
        {
            try
            {
                WARTECHCONNECTION.cConnect getGLs = new WARTECHCONNECTION.cConnect();
                dr2 = getGLs.ReadDB("select g.GLCODE,g.Glaccname,l.loanacc from GLSETUP g inner join loantype l on g.Glcode=l.LoanAcc where l.LoanCode='" + txtLoanCode.Text + "'");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        cboBankAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        txtBankAccount.Text = dr2["Glaccname"].ToString().Trim();

                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; getGLs.Dispose(); getGLs = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void getLoanRatio()
        {
            try
            {
                int maturityperiod = 0; double loanShareRatio = 0;
                WARTECHCONNECTION.cConnect getLratio = new WARTECHCONNECTION.cConnect();
                dr2 = getLratio.ReadDB("select isnull(ShareMaturity,0)ShareMaturity,isnull(LoanToShareRatio,0)LoanToShareRatio from sysparam");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        maturityperiod = Convert.ToInt32(dr2["ShareMaturity"].ToString().Trim());
                        loanShareRatio = Convert.ToDouble(dr2["LoanToShareRatio"].ToString().Trim());
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; getLratio.Dispose(); getLratio = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void loadthebanks()
        {
            try
            {
                cboBridgingAcc.Items.Clear();
                cboPremiumAcc.Items.Clear();
                cboRefinanceAcc.Items.Clear();
                cboprocessingAcc.Items.Clear();
                CboSharesBoostingAcc.Items.Clear();
                Cboinsuranceacc.Items.Clear();
                cboBridgingAcc.Items.Add("");
                cboPremiumAcc.Items.Add("");
                cboRefinanceAcc.Items.Add("");
                cboprocessingAcc.Items.Add("");
                CboSharesBoostingAcc.Items.Add("");
                Cboinsuranceacc.Items.Add("");
                WARTECHCONNECTION.cConnect getBanks = new WARTECHCONNECTION.cConnect();
                dr2 = getBanks.ReadDB("select Glcode from GLSETUP");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        cboBridgingAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        cboPremiumAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        cboRefinanceAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        cboprocessingAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        CboSharesBoostingAcc.Items.Add(dr2["GLCODE"].ToString().Trim());
                        Cboinsuranceacc.Items.Add(dr2["GLCODE"].ToString().Trim());
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; getBanks.Dispose(); getBanks = null;

            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void LoadAppraisedLoans()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("Select l.Loanno,l.loancode,m.surname +' '+othernames as membernames,ap.AmtRecommended,ap.AppraisDate,ap.reason from Loans l inner join members m on l.memberno=m.memberno inner join appraisal ap on l.loanno=ap.loanno where l.loanno not in(select Loanno from endmain) and l.loanno not in(select loanno from loanbal) order by ap.Appraisdate desc");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void cboBridgingAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + cboBridgingAcc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtBridgingAcc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                this.txtLoanNo.Text = GridView1.SelectedRow.Cells[1].Text;
                WARTECHCONNECTION.cConnect getdetails = new WARTECHCONNECTION.cConnect();
                string daquery = "Select m.memberno,m.surname+' '+ m.othernames as names, ap.loanno,lt.loanCode,ap.amtRecommended,l.repayMethod,l.repayPeriod,l.applicdate,ap.appraisdate,l.LOANAMT as Applied,ap.amtRecommended as Recommended from members m inner join loans l on m.memberno=l.memberno inner join Appraisal ap on l.loanno=ap.loanno left join Loantype lt on l.loancode=lt.loancode where ap.loanno='" + txtLoanNo.Text + "'";
                dr = getdetails.ReadDB(daquery);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtLoanCode.Text = dr["LoanCode"].ToString();
                        txtMemberno.Text = dr["memberno"].ToString();
                        txtAplicant.Text = dr["names"].ToString();
                        txtApplicdate.Text = dr["applicdate"].ToString();
                        txtAppraisdate.Text = dr["appraisdate"].ToString();
                        txtEndorseDate.Text = dr["appraisdate"].ToString();
                        txtAmtApplied.Text = dr["APPLIED"].ToString();
                        txtApprovedAmt.Text = dr["Recommended"].ToString();
                        txtEndorsedAmount.Text = (Convert.ToDouble(dr["AmtRecommended"])).ToString();
                        txtrepaymethod.Text = dr["repaymethod"].ToString();
                        txtrepayperiod.Text = dr["repayperiod"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; getdetails.Dispose(); getdetails = null;
                insurancefee = 1.5 / 100 * Convert.ToDouble(txtApprovedAmt.Text);
                Processingfee = 0.5 / 100 * Convert.ToDouble(txtApprovedAmt.Text);
                if (Processingfee < 200)
                {
                    Processingfee = 200;
                }
                txtProcessing.Text = Processingfee.ToString();
                txtinsurancecharges.Text = insurancefee.ToString();

                RefinanceAndCharges();
                MultiView1.ActiveViewIndex = 1;
                LoadGls();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void RefinanceAndCharges()
        {
            try
            {
                WARTECHCONNECTION.cConnect getRefine = new WARTECHCONNECTION.cConnect();
                dr1 = getRefine.ReadDB("SELECT ISNULL(SUM(Balance), 0) AS LoanBalance, ISNULL(SUM(IntrOwed), 0) AS intAccrued FROM LOANBAL WHERE LoanNo IN (SELECT brgLoanno FROM bridgingloan WHERE loanno = '" + txtLoanNo.Text + "')");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        RloanAmt = Convert.ToDouble(dr1["LoanBalance"].ToString());
                        IntRecovered = Convert.ToDouble(dr1["intAccrued"].ToString());

                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; getRefine.Dispose(); getRefine = null;
                bool isTopup = false;
                WARTECHCONNECTION.cConnect getBridgingLoan = new WARTECHCONNECTION.cConnect();
                dr2 = getBridgingLoan.ReadDB("Select brgloanno,Bridgetype from BridgingLoan where loanno='" + txtLoanNo.Text + "'");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        string Loanno = dr2["brgloanno"].ToString();
                        int Bridgetype = Convert.ToInt32(dr2["Bridgetype"]);
                        isTopup = true;
                        getCalculatedLoanRepayment(Loanno, Bridgetype);
                    }
                else
                {
                    isTopup = false;
                }
                dr2.Close(); dr2.Dispose(); dr2 = null; getBridgingLoan.Dispose(); getBridgingLoan = null;
                if (isTopup == false)
                {
                    double Totaldeduct = (Convert.ToDouble(RloanAmt)) + Processingfee + insurancefee + totalinterest + defPenalty + (Convert.ToDouble(txtSharesBoosting.Text)) + (Convert.ToDouble(txtpremium.Text)) + (Convert.ToDouble(txtRefinance.Text)) + (Convert.ToDouble(txtBridging.Text));
                    Totaldeduct = Math.Round(Totaldeduct, 0);
                    txtchequeamount.Text = ((Convert.ToDouble(txtEndorsedAmount.Text)) - Totaldeduct).ToString();
                    txtcashamount.Text = txtchequeamount.Text;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getCalculatedLoanRepayment(string Loanno, int Bridgetype)
        {
            try
            {
                WARTECHCONNECTION.cConnect mconn = new WARTECHCONNECTION.cConnect();
                string sql = "SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + Loanno + "'";
                dr = mconn.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr["TotalInterest"].ToString());
                        RepaidPrincipal = Convert.ToDouble(dr["TotaRepaid"].ToString());
                    }
                dr.Close(); dr.Dispose(); dr = null; mconn.Dispose(); mconn = null;

                WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
                string mysql = "SELECT  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE (LB.loanno='" + Loanno + "')";
                dr1 = ConSql.ReadDB(mysql);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        string mMemberno = dr1["memberno"].ToString();
                        string rmethod = dr1["RepayMethod"].ToString();
                        int rperiod = Convert.ToInt32(dr1["RPeriod"].ToString());
                        double rrate = Convert.ToDouble(dr1["interest"].ToString());
                        double initialAmount = Convert.ToDouble(dr1["InitialAmount"].ToString());
                        double intOwed = Convert.ToDouble(dr1["intrOwed"].ToString());
                        double LBalance = Convert.ToDouble(dr1["Balance"].ToString());
                        DateTime lastrepay = Convert.ToDateTime(dr1["LastDate"].ToString());
                        DateTime Dateissued = Convert.ToDateTime(dr1["dateissued"].ToString());
                        DateTime duedate = Convert.ToDateTime(dr1["duedate"].ToString());
                        string LoanCode = dr1["LoanCode"].ToString();
                        int mdtei = Convert.ToInt32(dr1["mdtei"].ToString());
                        double repayrate = Convert.ToDouble(dr1["RepRate"].ToString());
                        string intRecovery = dr1["intRecovery"].ToString();
                        double penaltyOwed = Convert.ToDouble(dr1["PenaltyOwed"].ToString());
                        double loanbalance = Convert.ToDouble(LBalance);
                        AutoCal = Convert.ToBoolean(dr1["AutoCalc"].ToString());
                        string RepayMode = dr1["RepayMode"].ToString();
                        bool wePenalize = Convert.ToBoolean(dr1["Penalty"].ToString());
                        double IntBalalance = Convert.ToDouble(dr1["intBalance"].ToString());

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

                            RepayableInterest = 0;
                        }
                        if (rmethod == "ADV")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                            RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                        }

                        int Mdtei = 0;

                        WARTECHCONNECTION.cConnect CMtdei = new WARTECHCONNECTION.cConnect();
                        string StrMtdei = "Select LT.MDTEI,LB.LastDate from loantype LT inner join LOANBAL LB ON LT.LoanCode=LB.LoanCode where LT.LoanCode='" + txtLoanCode.Text + "' and LB.LoanNo='" + Loanno + "'";
                        dr = CMtdei.ReadDB(StrMtdei);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                string Mitei = dr["MDTEI"].ToString();
                                Mdtei = Convert.ToInt32(Mitei);
                                lastrepaydate = Convert.ToDateTime(dr["LastDate"].ToString());

                                if (lastrepaydate == Convert.ToDateTime(txtEndorseDate.Text))
                                {
                                    interest = 0;
                                }
                                // mdtei = IIf(RepayMode = 1, mdtei, 0)
                                string[] dat = (txtEndorseDate.Text).Split('/');
                                string day = dat[0];
                                int theday = (Convert.ToInt32(day));
                                if (theday <= Mdtei)  //' Means he should not this time be charged interest
                                {
                                    interest = 0;
                                }
                            }
                        dr.Close(); dr.Dispose(); dr = null; CMtdei.Dispose(); CMtdei = null;

                        CurrentTotalDeductions = Principal + interest;
                        TotalPrinciple = TotalPrinciple + Principal;
                        totalinterest = totalinterest + interest;
                        IntRecovered = totalinterest + intOwed;
                        txtpremium.Text = (Math.Round(Convert.ToDouble(txtpremium.Text), 0)).ToString();
                        if (Bridgetype == 2)
                        {
                            double Premium = Convert.ToDouble(txtpremium.Text);
                            double Refinance = Convert.ToDouble(txtRefinance.Text);
                            double BridgingCharges = Convert.ToDouble(txtBridging.Text);
                            double Totaldeduct = Premium + Refinance + BridgingCharges;
                            txtchequeamount.Text = ((Convert.ToDouble(txtApprovedAmt.Text)) - Totaldeduct).ToString();
                            txtcashamount.Text = txtchequeamount.Text;
                        }
                        else
                        {
                            double Totaldeduct = (Convert.ToDouble(RloanAmt)) + Processingfee + insurancefee + totalinterest + defPenalty + (Convert.ToDouble(txtSharesBoosting.Text)) + (Convert.ToDouble(txtpremium.Text)) + (Convert.ToDouble(txtRefinance.Text)) + (Convert.ToDouble(txtBridging.Text)) + (Convert.ToDouble(txtProcessing.Text));
                            Totaldeduct = Math.Round(Totaldeduct, 0);
                            txtchequeamount.Text = ((Convert.ToDouble(txtEndorsedAmount.Text)) - Totaldeduct).ToString();
                            txtcashamount.Text = txtchequeamount.Text;
                        }
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboBankAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                WARTECHCONNECTION.cConnect bankcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + cboBankAcc.Text + "'";
                dr = bankcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtBankAccount.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; bankcc.Dispose(); bankcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtLoanNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string xxx = "";
                MultiView1.ActiveViewIndex = 1;
                double RloanAmt = 0;
                double balamount = 0;
                string BRGLoan = "";
                double disbursementBalance = 0;
                if (txtLoanNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Plese supply loanno");
                    txtLoanNo.Focus();
                    return;
                }
                if (txtEndorsedAmount.Text == "")
                {
                    txtEndorsedAmount.Text = "0";
                    if (Convert.ToDouble(txtEndorsedAmount.Text) <= 0)
                    {
                        WARSOFT.WARMsgBox.Show("Plese Enter the Endorsed Amount");
                        txtEndorsedAmount.Focus();
                        return;
                    }
                }
                if (Convert.ToDouble(txtApprovedAmt.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("The Approved Amount Must be greater than Zero");
                    return;
                }
                else if (Convert.ToDouble(txtEndorsedAmount.Text) > Convert.ToDouble(txtApprovedAmt.Text))
                {
                    WARSOFT.WARMsgBox.Show("The Endorsed Amount Must not be greater than the Approved");
                    return;
                }
                //if (Convert.ToInt32(txtEndorseDate.Text) < Convert.ToInt32(txtApplicdate.Text))
                //{
                //    WARSOFT.WARMsgBox.Show("Issue Date should not be greater than loan Application Date");
                //}
                if (txtcashamount.Text == "")
                {
                    txtcashamount.Text = "0";
                }

                double cashamount = Convert.ToDouble(txtcashamount.Text);

                NewTransaction(cashamount, (Convert.ToDateTime(txtEndorseDate.Text)), "Loan Clearance");
                double transactionTotal = Convert.ToDouble(txtEndorsedAmount.Text);

                // 'Refinance Issues

                double Totaldeduct = 0;

                dr = new WARTECHCONNECTION.cConnect().ReadDB("SELECT LoANNo,BALANCE, ISNULL(IntrOwed, 0) AS intAccrued FROM LOANBAL WHERE LoanNo IN (SELECT brgLoanno FROM bridgingloan WHERE loanno = '" + txtLoanNo.Text + "')");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        new WARTECHCONNECTION.cConnect().WriteDB("Delete from  DisbursementDeduction where loanno='" + txtLoanNo.Text + "'");
                        //'GET THE LOAN aCCOUNT FIRST TO BE USED TO REFINANCE
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select LoanAcc from loantype where loancode=(select loancode from loans where loanno='" + txtLoanNo.Text + "')");
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                string RefinancingAcc = dr1["LoanAcc"].ToString();
                            }
                        }
                        else
                        {
                            WARSOFT.WARMsgBox.Show("The LoanAccount could not be established");
                            return;
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        //'THE LOOP THROUGH THE THE REFINANCED LOANS

                        RloanAmt = Convert.ToDouble(dr["balance"]);

                        BRGLoan = dr["Loanno"].ToString();
                        Calculate_Loan_Repayment(BRGLoan);

                        if (rmethod == "STL")
                        {
                            interest = IntBalalance;
                        }
                        else
                        {
                            if (RepayMode != "1")
                            {
                                mdtei = 0;
                            }
                            if (lastrepaydate.Month == Convert.ToDateTime(txtEndorseDate.Text).Month)
                            {
                                interest = 0;
                            }
                            string[] dat = (txtEndorseDate.Text).Split('/');
                            string day = dat[0];
                            int theday = (Convert.ToInt32(day));
                            if (theday <= mdtei)  //' Means he should not this time be charged interest
                            {
                                interest = 0;
                            }
                        }
                        double Penalty = 0;
                        IntRecovered = Math.Round(interest, 0) + intOwed + Penalty;

                        Totaldeduct = Totaldeduct + RloanAmt + IntRecovered;
                        balamount = RloanAmt + IntRecovered;
                        if (cboBankAcc.Text == "" || txtBankAccount.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please Select at least The Bank Control Account to affect Topup/Refinance");
                            cboBankAcc.Focus();
                            return;
                        }
                        bool isTopUp = false;

                        string Bridgetype = "";
                        double EndorsedAmount = 0;
                        dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select Bridgetype from BridgingLoan where loanno='" + txtLoanNo.Text + "'");
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                Bridgetype = dr2["Bridgetype"].ToString();
                                EndorsedAmount = Convert.ToDouble(txtEndorsedAmount.Text);
                            }
                        }
                        dr2.Close(); dr2.Dispose(); dr2 = null;
                        if (Bridgetype == "2")
                        {
                            isTopUp = true;
                            EndorsedAmount = EndorsedAmount + Totaldeduct;

                            disburseLoan(txtLoanNo.Text, EndorsedAmount, txtEndorseDate.Text, transactionNo, disbursementBalance);

                            //  'if it's a top up, Just make the balance of the topped up loan Zero
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Update loanbal set balance=0,lastdate='" + txtEndorseDate.Text + "' where loanno='" + BRGLoan + "'");
                            string LoanAcc = "";
                            dr3 = new WARTECHCONNECTION.cConnect().ReadDB("Select LoanAcc from loantype where loancode=(select loancode from loanbal where loanno='" + BRGLoan + "')");
                            if (dr3.HasRows)
                            {
                                while (dr3.Read())
                                {
                                    LoanAcc = dr3["LoanAcc"].ToString();
                                }
                            }
                            else
                            {
                                WARSOFT.WARMsgBox.Show("The LoanAccount could not be established");
                                return;
                            }
                            dr3.Close(); dr3.Dispose(); dr3 = null;
                        }
                        else
                        {
                            isTopUp = false;
                            string LoanAcc = "";
                            dr3 = new WARTECHCONNECTION.cConnect().ReadDB("Select LoanAcc from loantype where loancode=(select loancode from loanbal where loanno='" + BRGLoan + "')");
                            if (dr3.HasRows)
                            {
                                while (dr3.Read())
                                {
                                    LoanAcc = dr3["LoanAcc"].ToString();
                                }
                            }
                            else
                            {
                                WARSOFT.WARMsgBox.Show("The LoanAccount could not be established");
                                return;
                            }
                            dr3.Close(); dr3.Dispose(); dr3 = null;
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Rloanno,Amount,Description,AuditId) Values('" + txtEndorseDate.Text + "','" + LoanAcc + "','" + txtLoanNo.Text + "','" + BRGLoan + "'," + balamount + ",'LR','" + Session["mimi"].ToString() + "')");
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;

                // Other deductions as specified in the list
                if (optReject.Checked == false)
                {

                    if (txtpremium.Text == "")
                    {
                        txtpremium.Text = "0";
                    }
                    if (Convert.ToDouble(txtpremium.Text) > 0)
                    {
                        if (cboPremiumAcc.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("You must choose the premium Account");
                            return;
                        }
                    }
                    if (txtpremium.Text != "")
                    {
                        if (Convert.ToDouble(txtpremium.Text) > 0)
                        {
                            if (cboPremiumAcc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the premium Account");
                                return;
                            }

                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno) Values('" + txtEndorseDate.Text + "','" + cboPremiumAcc.Text + "','" + txtLoanNo.Text + "'," + txtpremium.Text + ",'" + txtPremiumAcc.Text + "','" + Session["mimi"].ToString() + "','FEE')");
                        }
                    }

                    if (txtRefinance.Text != "")
                    {
                        if (Convert.ToDouble(txtRefinance.Text) > 0)
                        {
                            if (cboRefinanceAcc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the Refinance Income Account");
                                return;
                            }
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + cboRefinanceAcc.Text + "','" + txtLoanNo.Text + "'," + txtRefinance.Text + ",'" + txtrefinanceAcc.Text + "','" + Session["mimi"].ToString() + "','FEE')");

                        }
                    }

                    if (txtBridging.Text != "")
                    {
                        if (Convert.ToDouble(txtBridging.Text) > 0)
                        {
                            if (cboBridgingAcc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the Bridging Income Account");
                                return;
                            }
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + cboBridgingAcc.Text + "','" + txtLoanNo.Text + "'," + txtBridging.Text + ",'" + txtBridgingAcc.Text + "','" + Session["mimi"].ToString() + "','FEE')");
                        }
                    }

                    if (txtProcessing.Text != "")
                    {
                        if (Convert.ToDouble(txtProcessing.Text) > 0)
                        {
                            if (cboprocessingAcc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the Processing Income Account");
                                return;
                            }
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + cboprocessingAcc.Text + "','" + txtLoanNo.Text + "'," + txtProcessing.Text + ",'" + txtProcessingAcc.Text + "','" + Session["mimi"].ToString() + "','FEE')");
                        }
                    }
                    if (txtSharesBoosting.Text != "")
                    {
                        if (Convert.ToDouble(txtSharesBoosting.Text) > 0)
                        {
                            if (CboSharesBoostingAcc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the ShareBoosting Account");
                                return;
                            }
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + CboSharesBoostingAcc.Text + "','" + txtLoanNo.Text + "'," + txtSharesBoosting.Text + ",'" + txtSharesBoostingAcc.Text + "','User','FEE')");
                        }
                    }
                    if (txtinsurancecharges.Text != "")
                    {
                        if (Convert.ToDouble(txtinsurancecharges.Text) > 0)
                        {
                            if (Cboinsuranceacc.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("You must choose the Insurance charges Account");
                                return;
                            }
                            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + Cboinsuranceacc.Text + "','" + txtLoanNo.Text + "'," + txtinsurancecharges.Text + ",'" + txtinsuranceacc.Text + "','" + Session["mimi"].ToString() + "','FEE')");
                        }
                    }
                }
                //'Save it as Endorsed
                SaveEndorsed();
                MultiView1.ActiveViewIndex = -0;
                if (optReject.Checked== false)
                {
                    WARSOFT.WARMsgBox.Show("Record saved Successfully");
                    return;
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Record Rejected Successfully");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        }
        private void SaveEndorsed()
        {
            WARTECHCONNECTION.cConnect Connex = new WARTECHCONNECTION.cConnect();
            dr4 = Connex.ReadDB("select loanno from endmain where loanno='" + txtLoanNo.Text + "'");
            if (dr4.HasRows)
            {
                while (dr4.Read())
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy update endmain set minuteno='" + txtMinuteNo.Text + "',meetingdate='" + txtEndorseDate.Text + "',AmtApproved=" + txtchequeamount.Text + " where loanno='" + txtLoanNo.Text + "'");
                }
            }
            else
            {
                string accepted = "";
                if (optAccept.Checked == true)
                {
                    accepted = "1";
                }
                else
                {
                    accepted = "0";
                }
                string inddb = "set dateformat dmy Insert into Endmain(Loanno,MinutenO,MeetingDate,AmtApproved,Accepted,Reasons,Remarks,AuditId,TransactionNo)Values('" + txtLoanNo.Text + "','" + txtMinuteNo.Text + "','" + txtEndorseDate.Text + "'," + Convert.ToDouble(txtEndorsedAmount.Text) + ",'" + accepted + "','" + txtReason.Text + "','Endorsed','" + Session["mimi"].ToString() + "','" + transactionNo + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(inddb);
            }
            dr4.Close(); dr4.Dispose(); dr4 = null; Connex.Dispose(); Connex = null;

            new WARTECHCONNECTION.cConnect().WriteDB("update loans set status=3 where loanno='" + txtLoanNo.Text + "'");
            LoadAppraisedLoans();
        }

        private void disburseLoan(string Loanno, double Amount, string Dateissued, string transactionNo, double balance)
        {
            try
            {
                dr5 = new WARTECHCONNECTION.cConnect().ReadDB("Select l.memberno,lt.loanCode,l.interest,l.repayMethod,l.repayPeriod from loans l inner join Loantype lt on l.loancode=lt.loancode where l.loanno='" + Loanno + "'");
                if (dr5.HasRows)
                {
                    while (dr5.Read())
                    {
                        LoanCode = dr5["LoanCode"].ToString();
                        mMemberno = dr5["memberno"].ToString();
                        rrate = Convert.ToDouble(dr5["interest"].ToString());
                        rperiod = Convert.ToInt32(dr5["repayperiod"].ToString());
                        rmethod = dr5["repaymethod"].ToString();
                    }
                }
                dr5.Close(); dr5.Dispose(); dr5 = null;
                //'start transaction here
                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy INSERT INTO CHEQUES( [LoanNo], [Amount],[Balance], [DateIssued], [AuditID], [Remarks],[TransactionNo])VALUES ( '" + Loanno + "', " + Amount + "," + balance + ", '" + Dateissued + "', '" + Session["mimi"].ToString() + "', 'Loan Disbursed','" + transactionNo + "')");

                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy INSERT INTO LOANBAL (loanno,memberno,loancode,balance,lastdate,firstdate,interest,repayperiod,repayMethod,auditid,TransactionNo) VALUES('" + Loanno + "','" + mMemberno + "','" + LoanCode + "'," + Amount + ",'" + Dateissued + "','" + duedate + "'," + rrate + "," + rperiod + ",'" + rmethod + "','User','" + transactionNo + "')");

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('LOANBAL','Loan Disbursed Loanno " + Loanno + "','" + Dateissued + "','0','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Calculate_Loan_Repayment(string LoanNo)
        {
            try
            {
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + LoanNo + "'");
                if (dr4.HasRows)
                    while (dr4.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr4["TotalInterest"].ToString());
                        RepaidPrincipal = Convert.ToDouble(dr4["TotaRepaid"].ToString());
                    }
                dr4.Close(); dr4.Dispose(); dr4 = null;

                WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
                string mysql = "SELECT  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE (LB.loanno='" + LoanNo + "')";
                dr5 = ConSql.ReadDB(mysql);
                if (dr5.HasRows)
                    while (dr5.Read())
                    {
                        mMemberno = dr5["memberno"].ToString();
                        rmethod = dr5["RepayMethod"].ToString();
                        rperiod = Convert.ToInt32(dr5["RPeriod"].ToString());
                        rrate = Convert.ToDouble(dr5["interest"].ToString());
                        initialAmount = Convert.ToDouble(dr5["InitialAmount"].ToString());
                        intOwed = Convert.ToDouble(dr5["intrOwed"].ToString());
                        LBalance = Convert.ToDouble(dr5["Balance"].ToString());
                        DateTime lastrepay = Convert.ToDateTime(dr5["LastDate"].ToString());
                        DateTime Dateissued = Convert.ToDateTime(dr5["dateissued"].ToString());
                        DateTime duedate = Convert.ToDateTime(dr5["duedate"].ToString());
                        LoanCode = dr5["LoanCode"].ToString();
                        mdtei = Convert.ToInt32(dr5["mdtei"].ToString());
                        repayrate = Convert.ToDouble(dr5["RepRate"].ToString());
                        intRecovery = dr5["intRecovery"].ToString();
                        penaltyOwed = Convert.ToDouble(dr5["PenaltyOwed"].ToString());
                        loanbalance = Convert.ToDouble(LBalance);
                        AutoCal = Convert.ToBoolean(dr5["AutoCalc"].ToString());
                        RepayMode = dr5["RepayMode"].ToString();
                        wePenalize = Convert.ToBoolean(dr5["Penalty"].ToString());
                        IntBalalance = Convert.ToDouble(dr5["intBalance"].ToString());

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
                        CurrentTotalDeductions = Principal + interest;
                        TotalPrinciple = TotalPrinciple + Principal;
                        totalinterest = totalinterest + interest;

                    }
                dr5.Close(); dr5.Dispose(); dr5 = null; ConSql.Dispose(); ConSql = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description)
        {
            try
            {
                string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription)Values('" + transactionNo + "'," + AmountPaid + ",'User',Convert(Varchar(10), '" + TransDate + "', 101),'" + Description + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboPremiumAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + cboPremiumAcc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtPremiumAcc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboRefinanceAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + cboRefinanceAcc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtrefinanceAcc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboprocessingAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + cboprocessingAcc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtProcessingAcc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void CboSharesBoostingAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + CboSharesBoostingAcc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtSharesBoostingAcc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter("Select l.Loanno,l.loancode,m.surname +' '+othernames as membernames,ap.AmtRecommended,ap.AppraisDate from Loans l inner join members m on l.memberno=m.memberno inner join appraisal ap on l.loanno=ap.loanno where l.loanno not in(select Loanno from endmain) and l.loanno not in(select loanno from loanbal) order by ap.Appraisdate desc", Connection);

            try
            {
                adapter.Fill(ds);
                ExportToExcel(ds);
            }
            catch (Exception ex)
            {
                Connection.Close();
            }
        }

        private void ExportToExcel(System.ComponentModel.MarshalByValueComponent DataSource)
        {
            try
            {
                System.IO.StringWriter objStringWriter = new System.IO.StringWriter();
                System.Web.UI.WebControls.DataGrid tempDataGrid = new System.Web.UI.WebControls.DataGrid();
                System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Appraised Loans.xls");
                tempDataGrid.DataSource = DataSource;
                tempDataGrid.DataBind();
                tempDataGrid.HeaderStyle.Font.Bold = true;
                tempDataGrid.RenderControl(objHtmlTextWriter);
                DataSource.Dispose();
                HttpContext.Current.Response.Write(objStringWriter.ToString());
                HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void optReject_CheckedChanged(object sender, EventArgs e)
        {
            optAccept.Checked = false;
            Label21.Visible = false;
            Label22.Visible = false;
            Label23.Visible = false;
            Label20.Visible = false;
            Label25.Visible = false;
            Label29.Visible = false;
            Label24.Visible = false;
            cboBankAcc.Visible = false;
            cboBridgingAcc.Visible = false;
            Cboinsuranceacc.Visible = false;
            cboprocessingAcc.Visible = false;
            CboSharesBoostingAcc.Visible = false;
            cboPremiumAcc.Visible = false;
            cboRefinanceAcc.Visible = false;
            txtinsuranceacc.Visible = false;
            txtPremiumAcc.Visible = false;
            txtProcessingAcc.Visible = false;
            txtSharesBoostingAcc.Visible = false;
            txtBridgingAcc.Visible = false;
            txtrefinanceAcc.Visible = false;
            txtBankAccount.Visible = false;
            txtchequeamount.Visible = false;
            txtinsurancecharges.Visible = false;
            txtProcessing.Visible = false;
            txtpremium.Visible = false;
            txtRefinance.Visible = false;
            txtBridging.Visible = false;
            txtSharesBoosting.Visible = false;

        }
        protected void optAccept_CheckedChanged(object sender, EventArgs e)
        {
            optReject.Checked = false;
        }
        protected void Cboinsuranceacc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect GLACcc = new WARTECHCONNECTION.cConnect();
                string sql = "select GLACCNAME from glsetup where accno='" + Cboinsuranceacc.Text + "'";
                dr = GLACcc.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtinsuranceacc.Text = dr["GLACCNAME"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; GLACcc.Dispose(); GLACcc = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string issdat = "set dateformat dmy Insert into DisbursementDeduction(Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno)Values('" + txtEndorseDate.Text + "','" + CboSharesBoostingAcc.Text + "','" + txtLoanNo.Text + "'," + txtSharesBoosting.Text + ",'" + txtSharesBoostingAcc.Text + "','" + Session["mimi"].ToString() + "','FEE')";
                new WARTECHCONNECTION.cConnect().WriteDB(issdat);
                LoadDistributedAmounts();
                Loaddatatogrid();
                CboSharesBoostingAcc.Items.Add("");
                txtSharesBoostingAcc.Text = "";
                txtSharesBoosting.Text = "0.00";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loaddatatogrid()
        {
            try
            {
                string datatable = "select id,Transdate,Accno,Loanno,Amount,Description,AuditId,RLoanno from DisbursementDeduction where loanno='" + txtLoanNo.Text.TrimStart().TrimEnd() + "'";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView.Visible = true;
                GridView.DataSource = ds;
                GridView.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string delDATA = "delete from DisbursementDeduction where id='" + GridView.SelectedRow.Cells[1].Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(delDATA);
                LoadDistributedAmounts();
                Loaddatatogrid();
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadDistributedAmounts()
        {
            try
            {
                string sumAmnt = "select isnull(sum(amount),0)Amount from DisbursementDeduction where loanno='" + txtLoanNo.Text.TrimStart().TrimEnd() + "'";
                WARTECHCONNECTION.cConnect sumamount = new WARTECHCONNECTION.cConnect();
                dr1 = sumamount.ReadDB(sumAmnt);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        double SummedAmnt = Convert.ToDouble(dr1["Amount"]);
                        double Totaldeduct = SummedAmnt + Convert.ToDouble(txtinsurancecharges.Text) + Convert.ToDouble(txtProcessing.Text) + Convert.ToDouble(txtpremium.Text) + Convert.ToDouble(txtRefinance.Text) + Convert.ToDouble(txtBridging.Text);
                        Totaldeduct = Math.Round(Totaldeduct, 2);
                        txtchequeamount.Text = ((Convert.ToDouble(txtEndorsedAmount.Text)) - Totaldeduct).ToString();
                        txtcashamount.Text = txtchequeamount.Text;
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; sumamount.Dispose(); sumamount = null;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        }
        private void loadoutstanding()
        {
            MultiView1.ActiveViewIndex = 1;
            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select loanno,loancode,memberno,balance from loanbal where memberno='" + txtMemberno.Text + "' and balance>0 order by balance desc");
            if (dr1.HasRows)
            {
                GridView2.Visible = true;
                GridView2.DataSource = dr1;
                GridView2.DataBind();
            }
            else
            {
                WARSOFT.WARMsgBox.Show("The Member has no outstanding loans to top up");
            }
            dr1.Close(); dr1.Dispose(); dr1 = null;
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                this.txtLoanNo.Text = GridView1.SelectedRow.Cells[1].Text;
                loadoutstanding();

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                this.txtLoanNo.Text = GridView1.SelectedRow.Cells[1].Text;
                string loanNNO = GridView2.SelectedRow.Cells[1].Text;
                WARTECHCONNECTION.cConnect getdetails = new WARTECHCONNECTION.cConnect();
                string daquery = "Select m.memberno,m.surname+' '+ m.othernames as names, ap.loanno,lt.loanCode,ap.amtRecommended,l.repayMethod,l.repayPeriod,l.applicdate,ap.appraisdate,l.LOANAMT as Applied,ap.amtRecommended as Recommended from members m inner join loans l on m.memberno=l.memberno inner join Appraisal ap on l.loanno=ap.loanno left join Loantype lt on l.loancode=lt.loancode where ap.loanno='" + txtLoanNo.Text + "'";
                dr = getdetails.ReadDB(daquery);
                if (dr.HasRows)
                    while (dr.Read())
                    {

                        string loanco = dr["LoanCode"].ToString();
                        double TOTLAM = Convert.ToDouble(dr["Recommended"]);
                        string membernno = dr["memberno"].ToString();
                        double totalendorsed = 0;
                        double TotalsEnd = 0;
                        if (loanco == "INU")
                        {
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select loancode,loanno,balance from loanbal where loanno='" + loanNNO + "' and balance>0 order by balance desc");
                            if (dr1.HasRows)
                                while (dr1.Read())
                                {
                                    string loann = dr1["loancode"].ToString();
                                    string loanns = dr1["loanno"].ToString();
                                    double BAL = Convert.ToDouble(dr1["balance"]);
                                    if (BAL > 0)
                                    {

                                        if (loann == "WEZ"||loann=="INU"||loann=="INV")
                                        {
                                            totalendorsed = TOTLAM - BAL;
                                            txtApprovedAmt.Text = Convert.ToString(totalendorsed);
                                            txtEndorsedAmount.Text = Convert.ToString(totalendorsed);
                                            txtAplicant.Text = dr["names"].ToString();
                                            txtApplicdate.Text = dr["applicdate"].ToString();
                                            txtAppraisdate.Text = dr["appraisdate"].ToString();
                                            txtEndorseDate.Text = dr["appraisdate"].ToString();
                                            txtAmtApplied.Text = dr["APPLIED"].ToString();
                                            txtrepaymethod.Text = dr["repaymethod"].ToString();
                                            txtrepayperiod.Text = dr["repayperiod"].ToString();
                                            string wez = "update loanbal set balance=0 where loanno='" + loanns + "'";
                                            new WARTECHCONNECTION.cConnect().WriteDB(wez);
                                            string sql = "set dateformat dmy Insert into Repay(Loanno,SERIALNO,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,ChequeNo,TransBy,Remarks,auditid,TransactionNo) Values('" + loanns + "','1','" + DateTime.Today + "','1'," + BAL + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'0', '0'," + 0 + ",'TOP UP','0','" + Session["mimi"].ToString() + "','TOP UP BY---" + Session["mimi"].ToString() + "','" + Session["mimi"].ToString() + "','B/F')";
                                            new WARTECHCONNECTION.cConnect().WriteDB(sql);
                                            string LoanAcc = "";
                                            string interestAcc = "";
                                            string penaltyAcc = "";
                                            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                                            DR = oSaccoMaster.ReadDB("select lt.interestAcc,lt.loanAcc,lt.penaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + loanns + "'");
                                            if (DR.HasRows)
                                            {
                                                while (DR.Read())
                                                {
                                                    LoanAcc = DR["LoanAcc"].ToString();
                                                    interestAcc = DR["InterestAcc"].ToString();
                                                    penaltyAcc = DR["PenaltyAcc"].ToString();
                                                    if (LoanAcc == "" || interestAcc == "")
                                                    {
                                                        WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                                                        return;
                                                    }
                                                }
                                            }
                                            DR.Close(); DR.Dispose(); DR = null; oSaccoMaster.Dispose();
                                            string GL = "set dateformat dmy Insert into GLTRANSACTIONS(AMOUNT,TRANSDATE,DOCUMENTno,DRACCNO,CRACCNO,SOURCE,TRANSDESCRIPT,AUDITID,TRANSACTIONNO) Values('" + BAL + "','" + DateTime.Today + "','1'," + cboBankAcc.Text + ",'" + LoanAcc + "','" + txtMemberno.Text + "','TOP UP for---" + txtMemberno.Text + "','" + Session["mimi"].ToString() + "','B/F')";
                                            new WARTECHCONNECTION.cConnect().WriteDB(GL);
                                        }
                                        else
                                        {
                                            txtLoanCode.Text = dr["LoanCode"].ToString();
                                            txtMemberno.Text = dr["memberno"].ToString();
                                            txtApprovedAmt.Text = dr["Recommended"].ToString();
                                            txtEndorsedAmount.Text = (Convert.ToDouble(dr["AmtRecommended"])).ToString();
                                            txtAplicant.Text = dr["names"].ToString();
                                            txtApplicdate.Text = dr["applicdate"].ToString();
                                            txtAppraisdate.Text = dr["appraisdate"].ToString();
                                            txtEndorseDate.Text = dr["appraisdate"].ToString();
                                            txtAmtApplied.Text = dr["APPLIED"].ToString();
                                            txtrepaymethod.Text = dr["repaymethod"].ToString();
                                            txtrepayperiod.Text = dr["repayperiod"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        txtLoanCode.Text = dr["LoanCode"].ToString();
                                        txtMemberno.Text = dr["memberno"].ToString();
                                        txtApprovedAmt.Text = dr["Recommended"].ToString();
                                        txtEndorsedAmount.Text = (Convert.ToDouble(dr["AmtRecommended"])).ToString();
                                        txtAplicant.Text = dr["names"].ToString();
                                        txtApplicdate.Text = dr["applicdate"].ToString();
                                        txtAppraisdate.Text = dr["appraisdate"].ToString();
                                        txtEndorseDate.Text = dr["appraisdate"].ToString();
                                        txtAmtApplied.Text = dr["APPLIED"].ToString();
                                        txtrepaymethod.Text = dr["repaymethod"].ToString();
                                        txtrepayperiod.Text = dr["repayperiod"].ToString();
                                    }
                                }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                        }
                        else
                        {

                            txtLoanCode.Text = dr["LoanCode"].ToString();
                            txtMemberno.Text = dr["memberno"].ToString();
                            txtApprovedAmt.Text = dr["Recommended"].ToString();
                            txtEndorsedAmount.Text = (Convert.ToDouble(dr["AmtRecommended"])).ToString();
                            txtAplicant.Text = dr["names"].ToString();
                            txtApplicdate.Text = dr["applicdate"].ToString();
                            txtAppraisdate.Text = dr["appraisdate"].ToString();
                            txtEndorseDate.Text = dr["appraisdate"].ToString();
                            txtAmtApplied.Text = dr["APPLIED"].ToString();
                            txtrepaymethod.Text = dr["repaymethod"].ToString();
                            txtrepayperiod.Text = dr["repayperiod"].ToString();
                        }
                    }
                dr.Close(); dr.Dispose(); dr = null;
                insurancefee = 1.5 / 100 * Convert.ToDouble(txtApprovedAmt.Text);
                Processingfee = 0.5 / 100 * Convert.ToDouble(txtApprovedAmt.Text);
                if (Processingfee < 200)
                {
                    Processingfee = 200;
                }
                txtProcessing.Text = Processingfee.ToString();
                txtinsurancecharges.Text = insurancefee.ToString();

                RefinanceAndCharges();
                MultiView1.ActiveViewIndex = 1;
                LoadGls();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

    }
}