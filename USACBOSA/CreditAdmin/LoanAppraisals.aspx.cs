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
namespace USACBOSA.CreditAdmin
{
    public partial class LoanAppraisals : System.Web.UI.Page
    {
        SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, dr6, DR;
        SqlDataAdapter da;
        string transactionNo = "";
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
                MultiView1.ActiveViewIndex = 0;
                LoadLoanApplications();
                DateTime Appraisdate = DateTime.Today;
                txtAppraisalDate.Text = Appraisdate.ToString("dd-MM-yyyy");

            }
        }
        private double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        private void LoadCollaterals()
        {
            string sehhh = "select ColCode,MemberNo,DocNo,Mktvalue,LoanNo,Balance from COLLOANGUAR where LoanNo='" + txtLoanNo.Text + "'";// And DocNo='" + txtDocumentNo.Text + "'
            da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView3.Visible = true;
            GridView3.DataSource = ds;
            GridView3.DataBind();
            ds.Dispose();
            da.Dispose();
        }
        private void LoadGuarantors()
        {
            try
            {
                string sehhh = "select GuarMemberNo [Guarantor Member No],GuarNames [Guarantor Names],GuarBalance [Balance],GuarAmount [Amount] from vwLoanGuarantors where loanno='" + txtLoanNo.Text + "'";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadLoanApplications()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("Select l.Loanno,l.loancode,m.surname +' '+othernames as membernames,l.loanAmt,l.ApplicDate from Loans l inner join members m on l.memberno=m.memberno where loanno not in(select Loanno from appraisal) and loanno not in(select loanno from loanbal) order by l.applicdate desc");
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

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect commm = new WARTECHCONNECTION.cConnect();
                string selllect = "select MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Rank,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,CompanyCode,PIN,Photo,ShareCap,BankCode,Bname,AuditID,AuditTime,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Age from members where MemberNo='" + txtMemberNo.Text + "'; ";
                dr = commm.ReadDB(selllect);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string nnname = dr["Surname"].ToString();
                        string othernames = dr["OtherNames"].ToString();
                        txtNames.Text = nnname + " " + othernames;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; commm.Dispose(); commm = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Double Guaranteed = 0; Double collatorals = 0;
                MultiView1.ActiveViewIndex = 1;
                this.txtLoanNo.Text = GridView1.SelectedRow.Cells[1].Text;
                WARTECHCONNECTION.cConnect getdetails = new WARTECHCONNECTION.cConnect();
                string myqueryy = "SELECT m.memberno, m.Surname + '  ' + m.OtherNames AS [names],l.loanamt, l.RepayPeriod,l.repaymethod, l.RepayMethod,l.applicdate, l.BasicSalary,L.INTEREST  FROM dbo.LOANTYPE lt RIGHT JOIN  dbo.LOANS l ON lt.LoanCode = l.LoanCode INNER JOIN dbo.MEMBERS m ON l.MemberNo = m.MemberNo WHERE L.LOANNO='" + txtLoanNo.Text + "'";
                dr = getdetails.ReadDB(myqueryy);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtNames.Text = dr["names"].ToString();
                        txtMemberNo.Text = dr["memberno"].ToString();
                        // txtLoanBalance.Text = dr["names"].ToString();
                        txtrepaymethod.Text = dr["repaymethod"].ToString();
                        txtAppliedAmount.Text = dr["LoanAmt"].ToString();
                        txtRPeriod.Text = dr["RepayPeriod"].ToString();
                        txtIntRate.Text = dr["interest"].ToString();
                        WARTECHCONNECTION.cConnect erick = new WARTECHCONNECTION.cConnect();
                        dr5 = erick.ReadDB("SELECT ISNULL(SUM(Amount),0) as Amount from LOANGUAR where Loanno='" + txtLoanNo.Text + "'");
                        if (dr5.HasRows)
                            while (dr5.Read())
                            {
                                Guaranteed = Convert.ToDouble(dr5["Amount"]);
                            }
                        dr5.Close(); dr5.Dispose(); dr5 = null; erick.Dispose();
                        WARTECHCONNECTION.cConnect Jackpot = new WARTECHCONNECTION.cConnect();
                        dr6 = Jackpot.ReadDB("SELECT ISNULL(SUM(Balance),0) as Balance from COLLOANGUAR where Loanno='" + txtLoanNo.Text + "'");
                        if (dr6.HasRows)
                            while (dr6.Read())
                            {
                                collatorals = Convert.ToDouble(dr6["Balance"]);
                            }
                        dr6.Close(); dr6.Dispose(); dr6 = null; Jackpot.Dispose();
                        Double Totalguaranteed = Math.Round((Guaranteed + collatorals), 2);
                        txtRecomendedAmount.Text = Totalguaranteed.ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; getdetails.Dispose(); getdetails = null;

                WARTECHCONNECTION.cConnect getShares = new WARTECHCONNECTION.cConnect();
                String sqld = "SELECT ISNULL(SUM(s.totalshares), 0)Amount FROM vwsharebalance S WHERE s.MemberNo = '" + txtMemberNo.Text + "' and s.usedtoguarantee=1";
                dr1 = getShares.ReadDB(sqld);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        Double shares = Convert.ToDouble(dr1["Amount"]);//.ToString();
                        Double shares1 = Math.Round(shares, 2) * 1;
                        txtTotalShares.Text = shares1.ToString();
                        if (txtTotalShares.Text == "")
                        {
                            txtTotalShares.Text = "0.00";
                        }
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; getShares.Dispose(); getShares = null;

                //get His Outstanding Loans
                WARTECHCONNECTION.cConnect getOutstanding = new WARTECHCONNECTION.cConnect();
                dr2 = getOutstanding.ReadDB("select case when sum(balance) is null then 0 else sum(balance) end  as TotalLoanBalance from loanbal where memberno='" + txtMemberNo.Text + "'");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        txtLoanBalance.Text = dr2["TotalLoanBalance"].ToString();
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; getOutstanding.Dispose(); getOutstanding = null;

                //Get loanShareRatio
                double loanShareRatio = 0;
                WARTECHCONNECTION.cConnect getloanShareRatio = new WARTECHCONNECTION.cConnect();
                dr3 = getloanShareRatio.ReadDB("select LoanToShareRatio from sharetype where UsedToGuarantee=1");
                if (dr3.HasRows)
                    while (dr3.Read())
                    {
                        loanShareRatio = Convert.ToDouble(dr3["LoanToShareRatio"].ToString());

                        Double maxloanamounte = Convert.ToDouble(((loanShareRatio * (Convert.ToDouble(txtTotalShares.Text))) - (Convert.ToDouble(txtLoanBalance.Text))).ToString());
                        Double maxloanamounte1 = Math.Round(maxloanamounte, 2);
                        txtMaxLAmount.Text = maxloanamounte1.ToString();
                    }
                dr3.Close(); dr3.Dispose(); dr3 = null; getloanShareRatio.Dispose(); getloanShareRatio = null;
                getRepayRate(Convert.ToDouble(txtAppliedAmount.Text), Convert.ToDouble(txtIntRate.Text), txtrepaymethod.Text, Convert.ToInt32(txtRPeriod.Text), Convert.ToDouble(txtLoanBalance.Text));

                WARTECHCONNECTION.cConnect getContr = new WARTECHCONNECTION.cConnect();
                dr2 = getContr.ReadDB("select NewContr from SHRVAR where MemberNo='" + txtMemberNo.Text + "' and Subscribed=1");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        txtminimumContrib.Text = Convert.ToDouble(dr2["NewContr"].ToString()).ToString();
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; getContr.Dispose(); getContr = null;

                //get the deductions from the current loans

                double totalrepayable, RepayableInterest, intOwed = 0;
                double mrepayment = 0;
                double Principal = 0;
                double interest = 0;
                double totalinterest = 0;
                double totalPrincial = 0;
                WARTECHCONNECTION.cConnect getLoanDed = new WARTECHCONNECTION.cConnect();
                dr4 = getLoanDed.ReadDB("SELECT C.Amount, LB.Balance, LB.RepayMethod, L.RepayPeriod, LB.Interest FROM LOANBAL LB INNER JOIN  LOANS L ON LB.LoanNo = L.LoanNo INNER JOIN CHEQUES C ON L.LoanNo = C.LoanNo INNER JOIN LOANTYPE LT ON L.LoanCode = LT.LoanCode where lb.balance>0 AND Lb.memberno='" + txtMemberNo.Text + "' and lb.loanno not in(select brgLoanno from bridgingLoan where loanno='" + txtLoanNo.Text + "')");
                if (dr4.HasRows)
                    while (dr4.Read())
                    {
                        double initialAmount = Convert.ToDouble(dr4["Amount"].ToString());
                        double LBalance = Convert.ToDouble(dr4["Balance"].ToString());
                        string rmethod = dr4["RepayMethod"].ToString();
                        int rperiod = Convert.ToInt32(dr4["RepayPeriod"].ToString());
                        double rrate = Convert.ToDouble(dr4["Interest"].ToString());

                        if (rmethod == "AMRT")
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                            mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                            interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                            interest = Math.Ceiling(interest);
                            Principal = Math.Round((mrepayment - interest), 2);
                            RepayableInterest = 0;
                        }
                        if (rmethod == "STL")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 12 / 100));
                            mrepayment = Principal + interest;
                            RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                        }
                        if (rmethod == "RBAL")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                            Principal = initialAmount / rperiod;
                            interest = (rrate / 12 / 100) * LBalance;
                            RepayableInterest = 0;
                        }
                        totalinterest = totalinterest + interest;
                        totalPrincial = totalPrincial + Principal;
                        txtCurrentLoanDeductions.Text = (totalinterest + totalPrincial).ToString();
                        txtTotalDeduction.Text = (totalinterest + totalPrincial).ToString();
                        double newdeduc = Convert.ToDouble(txtNewDeduction.Text);
                        double StatutoryDed = Convert.ToDouble(txtStatutoryDed.Text);
                        double minContrib = Convert.ToDouble(txtminimumContrib.Text);
                        double otherded = Convert.ToDouble(txtOtherDeductions.Text);
                        double CurrentLoanDed = Convert.ToDouble(txtCurrentLoanDeductions.Text);
                        txtTotalDeduction.Text = (minContrib + StatutoryDed + otherded + CurrentLoanDed + newdeduc).ToString();
                    }
                LoadCollaterals();
                LoadGuarantors();
                dr4.Close(); dr4.Dispose(); dr4 = null; getLoanDed.Dispose(); getLoanDed = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getRepayRate(double initialAmount, double rrate, string rmethod, int rperiod, double LBalance)
        {
            try
            {
                double totalrepayable, RepayableInterest, intOwed = 0;
                double mrepayment = 0;
                double Principal = 0;
                double interest = 0;
                if (rmethod == "AMRT")
                {
                    totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                    mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                    interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                    interest = Math.Ceiling(interest);
                    Principal = Math.Round((mrepayment - interest), 2);
                    RepayableInterest = 0;
                }
                if (rmethod == "STL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                    Principal = initialAmount / rperiod;
                    interest = (initialAmount * (rrate / 12 / 100));
                    mrepayment = Principal + interest;
                    RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                }
                if (rmethod == "RBAL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                    Principal = initialAmount / rperiod;
                    interest = (rrate / 12 / 100) * LBalance;
                    RepayableInterest = 0;
                }
                txtNewDeduction.Text = (Principal + interest).ToString();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void getRepayRate_2(double initialAmount, double rrate, string rmethod, int rperiod, double LBalance)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void txtRPeriod_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtLoanNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtLoanBalance_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtBasicSalary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double TotalIncome = (Convert.ToDouble(txtBasicSalary.Text) + Convert.ToDouble(txtOAllowance.Text));
                txtTEarnings.Text = TotalIncome.ToString();
                calculate();
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void calculate()
        {
            //txtOtherDeductions.Text = ((Convert.ToDouble(txtTEarnings.Text)) - (Convert.ToDouble(txtStatutoryDed.Text))).ToString();

            //double totalLoanDeduction = ((Math.Round(Convert.ToDouble(txtNewDeduction.Text), 2)) + (Math.Round(Convert.ToDouble(txtCurrentLoanDeductions.Text), 2)));

            //txtOtherDeductions.Text = ((Convert.ToDouble(txtTotalDeduction.Text)) - (Convert.ToDouble(txtStatutoryDed.Text))).ToString();

            //txtOtherDeductions.Text = Math.Round(Convert.ToDouble(txtOtherDeductions.Text), 2).ToString();

            if (txtExpectedNetSalary.Text == "")
            {
                txtExpectedNetSalary.Text = "0";
            }
            if (txtTEarnings.Text == "")
            {
                txtTEarnings.Text = "0";
            }
            if (txtTEarnings.Text != "0")
            {
                double expectdnetsal = Convert.ToDouble(txtExpectedNetSalary.Text);
                double newdeduct = Convert.ToDouble(txtNewDeduction.Text);
                double tearning = Convert.ToDouble(txtTEarnings.Text);
                txtNetsalaryToGross.Text = (Math.Round((expectdnetsal - ((newdeduct / tearning) * 100)), 0).ToString());
            }
            double eaarnins = 0;
            string two = "2";
            string three = "3";
            double twthird = 0;
            eaarnins = (Convert.ToDouble(two) / Convert.ToDouble(three)) * (Convert.ToDouble(txtTEarnings.Text));

            txt2tHIRD.Text = Math.Round(eaarnins, 2).ToString();
            if (txtTotalDeduction.Text == "")
            {
                txtTotalDeduction.Text = "0";
            }
            double mincontr = Convert.ToDouble(txtminimumContrib.Text);
            double tottdedu = Convert.ToDouble(txtTotalDeduction.Text);
            double tottearn = Convert.ToDouble(txtTEarnings.Text);
            txtExpectedNetSalary.Text = Math.Round((tottearn - tottdedu + mincontr), 0).ToString();
            if (Convert.ToDouble(txtTotalDeduction.Text) != 0 && Convert.ToDouble(txtTEarnings.Text) != 0)
            {
                txtDeductionTogross.Text = (Math.Round(((Convert.ToDouble(txtCurrentLoanDeductions.Text) + Convert.ToDouble(txtNewDeduction.Text) + Convert.ToDouble(txtminimumContrib.Text) + Convert.ToDouble(txtOtherDeductions.Text)) / (Convert.ToDouble(txtTEarnings.Text) - Convert.ToDouble(txtStatutoryDed.Text)) * 100), 0)).ToString();

                if (Convert.ToDouble(txtDeductionTogross.Text) < ((2 / 3) * 100))
                {
                    txtDeductionTogross.BackColor = System.Drawing.Color.HotPink;
                }
                else
                {
                    txtDeductionTogross.BackColor = System.Drawing.Color.Blue;
                }

                txtDeductionTogross.Text = (Math.Round(Convert.ToDouble(txtDeductionTogross.Text), 0)).ToString();
                txtTotalLoanToGross.Text = Math.Round((((Convert.ToDouble(txtCurrentLoanDeductions.Text) + Convert.ToDouble(txtNewDeduction.Text) + Convert.ToDouble(txtminimumContrib.Text) + Convert.ToDouble(txtOtherDeductions.Text))) / (Convert.ToDouble(txtTEarnings.Text) - Convert.ToDouble(txtStatutoryDed.Text)) * 100), 0).ToString();
                txtTotalDedNewloantoGross.Text = ((Convert.ToDouble(txtNewDeduction.Text)) + (Convert.ToDouble(txtOtherDeductions.Text)) / (Convert.ToDouble(txtTEarnings.Text)) * 100).ToString();
                txtTotalDedNewloantoGross.Text = (Math.Round(Convert.ToDouble(txtTotalDedNewloantoGross.Text), 0)).ToString();
                if (txtStatutoryDed.Text == "")
                {
                    txtStatutoryDed.Text = "0";
                }
                if (txtStatutoryDed.Text != "0" && txtTEarnings.Text != "0")
                {
                    txtStatutoryToGross.Text = Math.Round((((Convert.ToDouble(txtStatutoryDed.Text) / (Convert.ToDouble(txtTEarnings.Text)) * 100))), 0).ToString();
                }
            }

            //''///compute sacco deduction to gross
            if (txtminimumContrib.Text == "")
            {
                txtminimumContrib.Text = "0";
            }
            if (txtNewDeduction.Text == "")
            {
                txtNewDeduction.Text = "0";
            }
            if (txtCurrentLoanDeductions.Text == "")
            {
                txtCurrentLoanDeductions.Text = "0";
            }
            if (txtTEarnings.Text == "")
            {
                txtTEarnings.Text = "0";
            }
            double newdedd = Math.Round(Convert.ToDouble(txtNewDeduction.Text), 0);
            double cldedud = Math.Round(Convert.ToDouble(txtCurrentLoanDeductions.Text));
            if (txtTEarnings.Text != "0" && (newdedd + cldedud).ToString() != "0")
            {
                txtcoopDedToGross.Text = (Math.Round(Convert.ToDouble(txtNewDeduction.Text), 0) + (Math.Round(Convert.ToDouble(txtCurrentLoanDeductions.Text), 0)) + (Convert.ToDouble((txtminimumContrib.Text))) / (Convert.ToDouble(txtTEarnings.Text) * 100)).ToString();
                txtcoopDedToGross.Text = Math.Round(Convert.ToDouble(txtcoopDedToGross.Text), 0).ToString();
            }

            if (txtStatutoryDed.Text != "0" && txtTEarnings.Text != "0")
            {
                txtStatutoryToGross.Text = Math.Round((Convert.ToDouble(txtStatutoryDed.Text) / (Convert.ToDouble(txtTEarnings.Text)) * 100), 0).ToString();
            }
        }

        protected void txtOAllowance_TextChanged(object sender, EventArgs e)
        {
            double TotalIncome = (Convert.ToDouble(txtBasicSalary.Text) + Convert.ToDouble(txtOAllowance.Text));
            txtTEarnings.Text = TotalIncome.ToString();
        }

        protected void chk2third_CheckedChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            if (chk2third.Checked == true)
            {
                txtBasicSalary.Enabled = true;
                txtOAllowance.Enabled = true;
            }
            else
            {
                txtBasicSalary.Enabled = false;
                txtOAllowance.Enabled = false;
            }
        }

        protected void btnAppraise_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                double Recommended = 0;
                if (chk2third.Checked == true)
                {
                    double totDed = Convert.ToDouble(txtTotalDeduction.Text);
                    double tHIRD = Convert.ToDouble(txt2tHIRD.Text);
                    if (totDed > tHIRD)
                    {
                        WARSOFT.WARMsgBox.Show("The total deductions bridges the 2/3 Rule");
                        Recommended = 0;
                        return;
                    }
                    else
                    {
                        Recommended = Convert.ToDouble(txtRecomendedAmount.Text);
                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    txtBasicSalary.Text = (1000000000).ToString();

                    btnSave.Enabled = true;

                    Recommended = Convert.ToDouble(txtRecomendedAmount.Text);
                }
                txtRecAmt.Text = Math.Round(Recommended, 2).ToString();
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtRecAmt.Text) > Convert.ToDouble(txtAppliedAmount.Text))
                {
                    WARSOFT.WARMsgBox.Show("You Can't recommend more than the applied amount!");
                    return;
                }
                if (Convert.ToDouble(txtRecAmt.Text) > Convert.ToDouble(txtRecomendedAmount.Text))
                {
                    WARSOFT.WARMsgBox.Show("You Can't Appraise more than the Reccomended amount!");
                    return;
                }
                WARTECHCONNECTION.cConnect myCon = new WARTECHCONNECTION.cConnect();
                dr = myCon.ReadDB("Select * from appraisal where loanno='" + txtLoanNo.Text + "'");
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("This Loan is already appraised");
                        return;
                    }
                dr.Close(); dr.Dispose(); dr = null; myCon.Dispose(); myCon = null;

                if (Convert.ToDouble(txtRecAmt.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("The Recommended amount cannot be Zero");
                    return;
                }

                NewTransaction(Convert.ToDouble(txtRecAmt.Text), Convert.ToDateTime(txtAppraisalDate.Text), "Loan Appraissal" + txtLoanNo.Text, "User");
                if (txtOAllowance.Text == "")
                {
                    txtOAllowance.Text = "0";
                }

                if (txtExpectedNetSalary.Text == "")
                {
                    txtExpectedNetSalary.Text = "0";
                }

                if (txtNetsalaryToGross.Text == "")
                {
                    txtNetsalaryToGross.Text = "0";
                }
                if (txtNewDeduction.Text == "")
                {
                    txtNewDeduction.Text = "0";
                }
                if (txtStatutoryDed.Text == "")
                {
                    txtStatutoryDed.Text = "0";
                }
                if (txtminimumContrib.Text == "")
                {
                    txtminimumContrib.Text = "0";
                }

                if (txtTotalDedNewloantoGross.Text == "")
                {
                    txtTotalDedNewloantoGross.Text = "0";
                }
                if (txtNetsalaryToGross.Text == "")
                {
                    txtNetsalaryToGross.Text = "0";
                }
                if (txtTotalLoanToGross.Text == "")
                {
                    txtTotalLoanToGross.Text = "0";
                }
                if (txtcoopDedToGross.Text == "")
                {
                    txtcoopDedToGross.Text = "0";
                }
                if (txtStatutoryToGross.Text == "")
                {
                    txtStatutoryToGross.Text = "0";
                }
                if (txtTotalLoanToGross.Text == "")
                {
                    txtTotalLoanToGross.Text = "0";
                }

                String del = "truncate table Appraisal1";
                new WARTECHCONNECTION.cConnect().WriteDB(del);

                string saveappraised = "set dateformat dmy insert into Appraisal(LoanNo,AppraisDate,Salary,Allowances,RepayMethod,[Co-opShares],[Co-opLoans],Shares,Loans,Deductions,AmtRecommended,TotalDeductions,Reason,AuditID,memberno,Repayrate,OtherDed,transactionNo,ExpectedNetsalary,DeductionToGross,StatutoryDed,StatutoryDedTogross,TotalDedNewLoanToGross,NetSalaryToGross,TotalLoanToGross,TotalCoopDedToGross,TotalDedToGrossLessstatutory,Principal)Values('" + txtLoanNo.Text + "','" + txtAppraisalDate.Text + "','" + txtBasicSalary.Text + "','" + txtOAllowance.Text + "','" + txtrepaymethod.Text + "','" + txtTotalShares.Text + "','" + txtNewDeduction.Text + "','" + txtTotalShares.Text + "','" + txtRecAmt.Text + "','" + txtTotalDeduction.Text + "','" + txtRecomendedAmount.Text + "','" + txtTotalDeduction.Text + "','" + txtReason.Text + "','" + Session["mimi"].ToString() + "','" + txtMemberNo.Text + "','" + txtNewDeduction.Text + "','" + txtOtherDeductions.Text + "','" + transactionNo + "','" + txtExpectedNetSalary.Text + "','" + txtDeductionTogross.Text + "','" + txtStatutoryDed.Text + "','" + txtStatutoryToGross.Text + "','" + txtTotalDedNewloantoGross.Text + "','" + txtNetsalaryToGross.Text + "','" + txtTotalLoanToGross.Text + "','" + txtcoopDedToGross.Text + "','" + txtTotalLoanToGross.Text + "','" + txtLoanBalance.Text + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(saveappraised);
                string saveappraised1 = "set dateformat dmy insert into Appraisal1(LoanNo,AppraisDate,Salary,Allowances,RepayMethod,[Co-opShares],[Co-opLoans],Shares,Loans,Deductions,AmtRecommended,TotalDeductions,Reason,AuditID,memberno,Repayrate,OtherDed,transactionNo,ExpectedNetsalary,DeductionToGross,StatutoryDed,StatutoryDedTogross,TotalDedNewLoanToGross,NetSalaryToGross,TotalLoanToGross,TotalCoopDedToGross,TotalDedToGrossLessstatutory,Principal)Values('" + txtLoanNo.Text + "','" + txtAppraisalDate.Text + "','" + txtBasicSalary.Text + "','" + txtOAllowance.Text + "','" + txtrepaymethod.Text + "','" + txtTotalShares.Text + "','" + txtNewDeduction.Text + "','" + txtTotalShares.Text + "','" + txtRecAmt.Text + "','" + txtTotalDeduction.Text + "','" + txtRecomendedAmount.Text + "','" + txtTotalDeduction.Text + "','" + txtReason.Text + "','" + Session["mimi"].ToString() + "','" + txtMemberNo.Text + "','" + txtNewDeduction.Text + "','" + txtOtherDeductions.Text + "','" + transactionNo + "','" + txtExpectedNetSalary.Text + "','" + txtDeductionTogross.Text + "','" + txtStatutoryDed.Text + "','" + txtStatutoryToGross.Text + "','" + txtTotalDedNewloantoGross.Text + "','" + txtNetsalaryToGross.Text + "','" + txtTotalLoanToGross.Text + "','" + txtcoopDedToGross.Text + "','" + txtTotalLoanToGross.Text + "','" + txtLoanBalance.Text + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(saveappraised1);

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('Appraisal','Loan Appraised Loanno " + txtLoanNo.Text + "','" + txtAppraisalDate.Text + "','" + txtRecAmt.Text.Trim() + "','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                WARTECHCONNECTION.cConnect upddate = new WARTECHCONNECTION.cConnect();
                string uuppdateStat = "update loans set status=2 where loanno='" + txtLoanNo.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(uuppdateStat);
                WARSOFT.WARMsgBox.Show("Record saved succesfully");
                LoadLoanApplications();
                if (PrintAppraisalReport.Checked == true)
                {
                    if (txtMemberNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Provide MemberNo!");
                    }
                    else
                    {
                        Session["Memberno"] = txtMemberNo.Text;
                        Response.Redirect("~/Reports/AppraisalRPT.aspx", false);
                    }                  
                    
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
     
        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description, string user)
        {
            try
            {
                //  string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription)Values('" + transactionNo + "'," + AmountPaid + ",'" + user + "',Convert(Varchar(10), '" + TransDate + "', 101),'" + Description + "')";
                //  new WARTECHCONNECTION.cConnect().WriteDB(sql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
    }
}
    