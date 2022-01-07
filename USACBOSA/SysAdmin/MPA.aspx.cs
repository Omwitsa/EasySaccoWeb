using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using Neodynamic.SDK.Web;

namespace USACBOSA.SysAdmin
{
    public partial class MPA : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        string TransType;
        static DateTime lastrepaydate;
        public Double totalDr, totalCr = 0;
        public Double OpeningBal = 0;
        public Double Principal = 0;
        public Double mrepayment = 0;
        public Double interest = 0;
        public Double intReceivable = 0;
        public int RepayMode = 0;
        public Double Penalty = 0;
        public Double intOwed = 0;
        public Boolean success = false;
        public Double LBalance = 0;
        public Double rpInterest = 0;
        public Double totalrepayable = 0;
        public Double RepayableInterest = 0;
        public Double penalty2 = 0;
        public string rmethod, intRecovery = "";
        public int rperiod, mdtei = 0;
        public Double rrate = 0;
        public Double initialAmount = 0;
        public string transactionNo = "";
        public Double transactionTotal, loanbalance = 0;
        public Double PrincAmount, IntrAmount, intTotal, IntBalalance = 0;

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
                Generate_ReceiptNo();
                LoadBanks();
                LoadGL();
                txtDateDeposited.Attributes.Add("readonly", "readonly");
                txtContribDate.Attributes.Add("readonly", "readonly");

            }
        }

        private void LoadGL()
        {
            cboGlAccountNo.Items.Add("");
            WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
            string GL = "select s.SharesAcc,g.Glaccname from sharetype s inner join GLSETUP g on s.SharesAcc=g.accno";
            dr = GLS.ReadDB(GL);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboGlAccountNo.Items.Add("" + dr["SharesAcc"].ToString() + "");
                }
            }
            dr.Close(); dr.Dispose(); dr = null; GLS.Dispose(); GLS = null;
        }

        private void LoadBanks()
        {
            cboBankAC.Items.Add("");
            string sql = "select Accno from banks";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    cboBankAC.Items.Add("" + dr["Accno"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }

        private void Generate_ReceiptNo()
        {
            try
            {
                string ssessionuser = "";
                string myddd444 = "select userid from useraccounts1  where userloginid='" + Session["mimi"].ToString() + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(myddd444);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ssessionuser = dr["userid"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null;
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                string myddd = "select isnull(MAX(RIGHT(ReceiptNo,3)),0)+1 ccount from ReceiptBooking  where ReceiptNo like '%RCP%' and year(auditdatetime)='" + (System.DateTime.Today).Year + "' and month(auditdatetime)='" + (System.DateTime.Today).Month + "' and day(auditdatetime)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        int nextno = maxno;
                        if (ssessionuser == "1" || ssessionuser == "2" || ssessionuser == "3" || ssessionuser == "4" || ssessionuser == "5" || ssessionuser == "6" || ssessionuser == "7" || ssessionuser == "8" || ssessionuser == "9")
                        {
                            ssessionuser = 0 + ssessionuser;
                        }
                        txtReceiptNo.Text = "RCP" + ssessionuser + (DateTime.Now.Day.ToString("00")) + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno).ToString()).PadLeft(3, '0');
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void txtMemberNo_DataBinding(object sender, EventArgs e)
        {
            try
            {
                string readdata = "Select m.memberno,m.surname +' '+m.othernames as names, max(isnull(sv.newContr,0)) as Expected,sv.SharesCode,st.sharestype as vote From SHRVAR sv inner join sharetype st on sv.sharescode=st.sharescode inner join members m on sv.memberno=m.memberno Where sv.MemberNo='" + txtMemberNo.Text.Trim() + "' and sv.subscribed=1 GROUP BY sv.SharesCode, st.SharesType, m.MemberNo, m.Surname + ' ' + m.OtherNames";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
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

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.txtEditedDistributedAmount.Text = GridView.SelectedRow.Cells[3].Text;
            this.txtMembersAccountId.Text = GridView.SelectedRow.Cells[1].Text;
        }

        private void LoadMemberDetails(bool NewMember)
        {
            try
            {
                System.Data.SqlClient.SqlDataReader dr, drv;
                string trunk = "delete from MembersAccount where memberno='" + txtMemberNo.Text.Trim() + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(trunk);
                if (NewMember == true)
                {
                    string readdataff = "select Accno,description,contribution,sharescode,amount from defaults where contribution='True' order by accno";
                    WARTECHCONNECTION.cConnect inserttggg = new WARTECHCONNECTION.cConnect();
                    dr = inserttggg.ReadDB(readdataff);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string memberno = txtMemberNo.Text.Trim();
                            string MemberNames = txtNames.Text.Trim().Replace("'", "");
                            string Amount = dr["amount"].ToString();
                            string vote = memberno + dr["sharescode"].ToString();
                            string Principal = "0.00";
                            string Interest = "0.00";
                            string Type = dr["sharescode"].ToString();
                            string TransDescription = dr["description"].ToString();

                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,TransType,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + Interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','Contrib','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null; inserttggg.Dispose(); inserttggg = null;
                }
                else
                {
                    string readdata = "Select m.memberno,m.surname +' '+m.othernames as names, max(isnull(sv.newContr,0)) as Expected,sv.SharesCode,st.sharestype as vote From SHRVAR sv inner join sharetype st on sv.sharescode=st.sharescode inner join members m on sv.memberno=m.memberno Where sv.MemberNo='" + txtMemberNo.Text.Trim() + "' and sv.NewContr>0 and  sv.subscribed=1 GROUP BY sv.SharesCode, st.SharesType, m.MemberNo, m.Surname + ' ' + m.OtherNames";
                    WARTECHCONNECTION.cConnect insertt = new WARTECHCONNECTION.cConnect();
                    dr = insertt.ReadDB(readdata);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string memberno = dr["memberno"].ToString();
                            string MemberNames = dr["names"].ToString().Replace("'", " ");
                            string Amount = dr["Expected"].ToString();
                            string vote = memberno + dr["SharesCode"].ToString();
                            string Principal = "0.00";
                            string Interest = "0.00";
                            string Type = dr["SharesCode"].ToString();
                            string TransDescription = dr["vote"].ToString();

                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,TransType,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + Interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','Contrib','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null; insertt.Dispose(); insertt = null;

                    string readdataLoan = "Select m.memberno,m.surname +' '+m.othernames as names,C.Amount,lt.loantype,lt.MDTEI,LB.LoanNo,LB.LoanCode,LB.MemberNo,LB.Balance,LB.IntrOwed,LB.FirstDate,LB.RepayRate,LB.LastDate,LB.duedate,LB.intrCharged,LB.Interest,LB.Penalty,LB.RepayMethod,LB.Cleared,LB.AutoCalc,LB.IntrAmount,LB.RepayPeriod,LB.Remarks,LB.IntBalance,LB.CategoryCode,LB.InterestAccrued,LB.Defaulter,LB.Processdate,LB.RepayMode From LOANBAL LB Inner Join CHEQUES C On LB.LoanNo=C.LoanNo inner join loantype lt on lb.loancode=lt.loancode inner join members m on lb.memberno=m.memberno Where LB.MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' And LB.Balance>=1 ";
                    WARTECHCONNECTION.cConnect conet = new WARTECHCONNECTION.cConnect();
                    dr = conet.ReadDB(readdataLoan);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int totalrepayable = 0;
                            string memberno = dr["memberno"].ToString();
                            string MemberNames = dr["names"].ToString().Replace("'", " ");
                            txtNames.Text = MemberNames;
                            Double Amount = Convert.ToDouble(dr["Amount"].ToString());
                            Double IntBalance = Convert.ToDouble(dr["IntBalance"].ToString());
                            string vote = dr["LoanNo"].ToString();
                            string Principal = "0.00";
                            Double Interest = Convert.ToDouble(dr["Interest"].ToString().TrimStart().TrimEnd());
                            Double Balance = Convert.ToDouble(dr["Balance"].ToString().TrimStart().TrimEnd());
                            rperiod = Convert.ToInt32(dr["RepayPeriod"].ToString().TrimStart().TrimEnd());
                            string Type = dr["LoanNo"].ToString();
                            string TransDescription = dr["loantype"].ToString();
                            string rmethod = dr["RepayMethod"].ToString().TrimStart().TrimEnd();
                            string lastrepaydate = dr["LastDate"].ToString().TrimStart().TrimEnd();
                            double IntrOwed = Convert.ToDouble(dr["IntrOwed"].ToString());
                            getRepayRate(Amount, Interest, IntrOwed, IntBalance, rmethod, rperiod, Balance, vote, memberno, TransDescription, Type, MemberNames, lastrepaydate);
                        }
                    }
                    else
                    {
                        string readdataLoan3 = "Select m.memberno,m.surname +' '+m.othernames as names,C.Amount,lt.loantype,lt.MDTEI,LB.LoanNo,LB.LoanCode,LB.MemberNo,LB.Balance,LB.IntrOwed,LB.FirstDate,LB.RepayRate,LB.LastDate,LB.duedate,LB.intrCharged,LB.Interest,LB.Penalty,LB.RepayMethod,LB.Cleared,LB.AutoCalc,LB.IntrAmount,LB.RepayPeriod,LB.Remarks,LB.IntBalance,LB.CategoryCode,LB.InterestAccrued,LB.Defaulter,LB.Processdate,LB.RepayMode From LOANBAL LB Inner Join CHEQUES C On LB.LoanNo=C.LoanNo inner join loantype lt on lb.loancode=lt.loancode inner join members m on lb.memberno=m.memberno Where LB.MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' And LB.Balance<0 ";
                        WARTECHCONNECTION.cConnect conett = new WARTECHCONNECTION.cConnect();
                        dr4 = conett.ReadDB(readdataLoan3);
                        if (dr4.HasRows)
                        {
                            while (dr4.Read())
                            {
                                int totalrepayable = 0;
                                string memberno = dr4["memberno"].ToString();
                                string MemberNames = dr4["names"].ToString().Replace("'", " ");
                                txtNames.Text = MemberNames;
                                Double Amount = Convert.ToDouble(dr4["Amount"]);
                                Double IntBalance = Convert.ToDouble(dr4["IntBalance"]);
                                string vote = dr4["LoanNo"].ToString();
                                string Principal = "0.00";
                                Double Interest = Convert.ToDouble(dr4["Interest"]);
                                Double Balance = Convert.ToDouble(dr4["Balance"].ToString().TrimStart().TrimEnd());
                                rperiod = Convert.ToInt32(dr4["RepayPeriod"]);
                                string Type = dr4["LoanNo"].ToString();
                                string TransDescription = dr4["loantype"].ToString();
                                string rmethod = dr4["RepayMethod"].ToString().TrimStart().TrimEnd();
                                string lastrepaydate = dr4["LastDate"].ToString().TrimStart().TrimEnd();
                                double IntrOwed = Convert.ToDouble(dr4["IntrOwed"].ToString());

                                getRepayRate2(Amount, Interest, IntrOwed, IntBalance, rmethod, rperiod, Balance, vote, memberno, TransDescription, Type, MemberNames, lastrepaydate);
                            }
                        }
                        dr4.Close(); dr4.Dispose(); dr4 = null; conett.Dispose(); conett = null;
                    }
                    dr.Close(); dr.Dispose(); dr = null; conet.Dispose(); conet = null;
                }
                Loaddatatogrid();
                LoadDistributedAmounts();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getRepayRate2(double Amount, double Interest, double IntrOwed, double IntBalance, string rmethod, int rperiod, double Balance, string vote, string memberno, string TransDescription, string Type, string MemberNames, string lastrepaydate)
        {
            try
            {
                DateTime DateDeposited = System.DateTime.Today;
                DateTime lastrepay = Convert.ToDateTime(lastrepaydate);
                int datediff = ((DateDeposited.Year - lastrepay.Year) * 12) + DateDeposited.Month - lastrepay.Month;
                int monthdiff = ((DateDeposited.Year - lastrepay.Year) * 12) + (DateDeposited.Month - lastrepay.Month) + (DateDeposited.Day - lastrepay.Day);
                Double pesaYote, totalrepayable = 0;
                double interest_B = 0;
                double interest_A = 0;
                string valret = "";
                Double Amount2 = 0;
                int D = monthdiff;
                int m = datediff;
                if (rmethod == "AMRT")
                {

                    if (m < 1 && chkAcruedInterest.Checked == false && btnRefresh.Enabled == false)
                    {
                        btnRefresh.Enabled = true;
                        WARSOFT.WARMsgBox.Show("You have overpaid " + vote + " for the last " + m + " months, Refresh the Expectation");
                        return;
                    }
                    else if (m < 1 && chkAcruedInterest.Checked == true)
                    {
                        for (m = 1; m > (datediff); m++)
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, LBalance, 0);
                            double mrepayment = Math.Round(Pmt(rrate, rperiod, LBalance, 0), 2);
                            //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                            interest_A = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                            interest_A = Math.Ceiling(interest_A);
                            interest_B = interest_B + interest_A;
                            interest = interest_B;
                            Principal = Math.Round((mrepayment - interest_A), 2);
                            Amount = Principal + interest + interest_A + IntrOwed;
                            RepayableInterest = 0;//'baadaye
                            valret = "ok";
                        }
                        if (valret == "ok")
                        {
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','0','" + (interest + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                            btnRefresh.Enabled = false;
                        }
                    }
                    if (valret != "ok")
                    {
                        totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                        totalrepayable = 0;
                        double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                        //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                        double interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded                       
                        interest = Math.Ceiling(interest);
                        Principal = Math.Round((mrepayment - interest), 2);
                        Principal = 0;
                        Amount = Principal + interest + IntrOwed;
                        RepayableInterest = 0;//'baadaye              
                        string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','0','" + (interest_A + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        btnRefresh.Enabled = false;
                    }
                }
                else if (rmethod == "STL")
                {
                    if (IntBalance >= 0)
                    {

                        if (m > 1 && chkAcruedInterest.Checked == false && btnRefresh.Enabled == false)
                        {
                            btnRefresh.Enabled = true;
                            WARSOFT.WARMsgBox.Show("You have not been paying " + vote + " for the last " + m + " months");
                            return;
                        }
                        for (m = 1; m < (datediff); m++)
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                            Principal = initialAmount / rperiod;
                            //  interest = (initialAmount * (rrate / 12 / 100));
                            interest_A = Math.Round((initialAmount * (rrate / 12 / 100)), 2);// 'Interest owed is loaded
                            interest_A = Math.Ceiling(interest_A);
                            interest_B = interest_B + interest_A;
                            interest = interest_B;

                            RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                            if ((interest + interest_A) > IntBalance)
                            {
                                interest = IntBalance;
                                IntrOwed = IntBalance;
                            }
                            valret = "ok";
                        }
                        Amount = Principal + interest + interest_A;
                        if (valret == "ok")
                        {
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                        if (valret != "ok")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 12 / 100));
                            Amount = Principal + interest;
                            RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                    }
                }
                else if (rmethod == "RBAL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                    Principal = initialAmount / rperiod;
                    interest = (rrate / 12 / 100) * LBalance;
                    Amount = Principal + interest;
                    // 'intrcharged = LoanAmount * (rRate / 200 * (RPeriod + 1)) / RPeriod
                    RepayableInterest = 0;// 'unpredictable
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSPECIAL")
                {
                    Double intTotal = 0;
                    Double PrincAmount = 0;
                    LBalance = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = initialAmount / rperiod;
                        // 'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    interest = intTotal / rperiod;
                    Amount = Principal + interest;
                    RepayableInterest = 0;
                    LBalance = loanbalance;// 'to continue with the previous flow
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSTL")
                {
                    pesaYote = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = pesaYote / rperiod;
                        // 'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    Amount = Principal + interest;
                    IntBalalance = intTotal;
                    RepayableInterest = 0;// 'unpredictable
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "ADV")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Principal = initialAmount / rperiod;
                    interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                    RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Amount = Principal + interest;
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSPECIAL")
                {
                    PrincAmount = 0;
                    LBalance = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = initialAmount / rperiod;
                        //'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    interest = intTotal / rperiod;
                    Amount = Principal + interest;
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("No repay method detected for the loan to be loaded. Contact the administrator");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
            //  getRepayRate = Principal + interest; 
        }

        private void LoadDistributedAmounts()
        {
            try
            {
                string sumAmnt = "select isnull(sum(amount),0)Amount from MembersAccount where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "'";
                WARTECHCONNECTION.cConnect sumamount = new WARTECHCONNECTION.cConnect();
                dr1 = sumamount.ReadDB(sumAmnt);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        double SummedAmnt = Convert.ToDouble(dr1["Amount"]);
                        txtDistributedAmount.Text = Math.Ceiling(SummedAmnt).ToString();
                        double bal = -1 * Math.Ceiling(SummedAmnt);
                        txtBalance.Text = bal.ToString();
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; sumamount.Dispose(); sumamount = null;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loaddatatogrid()
        {
            try
            {
                string datatable = "select Vote,TransDescription,Amount,Principal,Interest,IntOwed [Interest Accrued],MemberNo,MemberNames,Type from MembersAccount where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "'";
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

        private void getRepayRate(Double initialAmount, Double rrate, double IntrOwed, double IntBalance, String rmethod, int rperiod, Double LBalance, string vote, string memberno, string TransDescription, string Type, string MemberNames, string lastrepaydate)
        {
            try
            {
                DateTime DateDeposited = System.DateTime.Today;
                DateTime lastrepay = Convert.ToDateTime(lastrepaydate);
                int datediff = ((DateDeposited.Year - lastrepay.Year) * 12) + DateDeposited.Month - lastrepay.Month;
                int monthdiff = ((DateDeposited.Year - lastrepay.Year) * 12) + (DateDeposited.Month - lastrepay.Month) + (DateDeposited.Day - lastrepay.Day);
                Double pesaYote, totalrepayable = 0;
                double interest_B = 0;
                double interest_A = 0;
                string valret = "";
                Double Amount = 0;
                //int D = monthdiff;
                double interest = 0;
                int m = datediff;
                DateTime DateDeposited2 = Convert.ToDateTime(txtContribDate.Text);

                TimeSpan D = (DateDeposited2 - lastrepay);
                double nofdays = D.TotalDays;
                if (rmethod == "AMRT")
                {
                    //double L = value / 100;
                    if (nofdays >= 0 && nofdays <= 31)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded                       
                    }
                    if (nofdays >= 32 && nofdays <= 63)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 2, 2);// 'Interest owed is loaded                       
                    }
                    if (nofdays >= 64 && nofdays <= 95)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 3, 2);// 'Interest owed is loaded                       
                    }
                    if (nofdays >= 96 && nofdays <= 127)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 4, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 128 && nofdays <= 159)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 5, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 160 && nofdays <= 191)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 6, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 192 && nofdays <= 223)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 7, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 224 && nofdays <= 255)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 8, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 256 && nofdays <= 287)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 9, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 288 && nofdays <= 319)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 10, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 320 && nofdays <= 351)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 11, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 352 && nofdays <= 383)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 12, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 384 && nofdays <= 415)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 13, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 416 && nofdays <= 447)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 14, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 448 && nofdays <= 479)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 15, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 480 && nofdays <= 511)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 16, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 512 && nofdays <= 543)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 17, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 544 && nofdays <= 575)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 18, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 576 && nofdays <= 607)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 19, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 608 && nofdays <= 639)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 20, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 640 && nofdays <= 671)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 21, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 672 && nofdays <= 703)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 22, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 704 && nofdays <= 735)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 23, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 736 && nofdays <= 767)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed) * 24, 2);// 'Interest owed is loaded  
                    }
                    if (nofdays >= 768 && nofdays <= 799)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 25, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 800 && nofdays <= 831)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 26, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 832 && nofdays <= 863)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 27, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 864 && nofdays <= 895)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 28, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 896 && nofdays <= 927)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 29, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 928 && nofdays <= 959)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 30, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 960 && nofdays <= 991)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 31, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 992 && nofdays <= 1023)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 32, 2);// 'Interest owed is loaded                      
                    }
                    if (nofdays >= 1024 && nofdays <= 1054)
                    {
                        interest = Math.Round(rrate / 12 / 100 * (LBalance + IntrOwed) * 33, 2);// 'Interest owed is loaded                      
                    }
                    if (m > 1 && chkAcruedInterest.Checked == false && btnRefresh.Enabled == false)
                    {
                        btnRefresh.Enabled = true;
                        WARSOFT.WARMsgBox.Show("You have not been paying " + vote + " for the last " + m + " months, Refresh the Expectation");

                        string valret2 = "ok";
                        if (valret2 == "ok")
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                            double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                            //'mrepayment = repayrate ' I did this after alot of discussions with lucy                          
                            interest = Math.Ceiling(interest);
                            Principal = Math.Round((mrepayment - interest), 2);
                            Amount = Principal + interest + IntrOwed;
                            RepayableInterest = 0;//'baadaye              
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','0','" + (interest_A + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                            btnRefresh.Enabled = false;
                            return;
                        }

                    }
                    else if (m > 1 && chkAcruedInterest.Checked == true)
                    {
                        for (m = 1; m < (datediff); m++)
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, LBalance, 0);
                            double mrepayment = Math.Round(Pmt(rrate, rperiod, LBalance, 0), 2);
                            //'mrepayment = repayrate ' I did this after alot of discussions with lucy                            
                            interest_A = Math.Ceiling(interest_A);
                            interest_B = interest_B + interest_A;
                            interest = interest_B;
                            Principal = Math.Round((mrepayment - interest_A), 2);
                            Amount = Principal + interest + interest_A + IntrOwed;
                            RepayableInterest = 0;//'baadaye
                            valret = "ok";
                        }
                        if (valret == "ok")
                        {
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','0','" + (interest + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                            btnRefresh.Enabled = false;
                        }
                    }
                    if (valret != "ok")
                    {
                        totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                        double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                        //'mrepayment = repayrate ' I did this after alot of discussions with lucy                                             
                        interest = Math.Ceiling(interest);
                        Principal = Math.Round((mrepayment - interest), 2);
                        Amount = Principal + interest + IntrOwed;
                        RepayableInterest = 0;//'baadaye              
                        string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','0','" + (interest_A + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        btnRefresh.Enabled = false;
                    }

                }
                else if (rmethod == "STL")
                {
                    if (IntBalance >= 0)
                    {

                        if (m > 1 && chkAcruedInterest.Checked == false && btnRefresh.Enabled == false)
                        {
                            btnRefresh.Enabled = true;
                            WARSOFT.WARMsgBox.Show("You have not been paying " + vote + " for the last " + m + " months");
                            string valret3 = "ok";
                            if (valret3 == "ok")
                            {
                                totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                                Principal = initialAmount / rperiod;
                                //  interest = (initialAmount * (rrate / 12 / 100));
                                interest_A = Math.Round((initialAmount * (rrate / 12 / 100)), 2);// 'Interest owed is loaded
                                interest_A = Math.Ceiling(interest_A);
                                interest_B = interest_B + interest_A;
                                interest = interest_B;

                                RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                                if ((interest + interest_A) > IntBalance)
                                {
                                    interest = IntBalance;
                                    IntrOwed = IntBalance;
                                }
                            }
                            valret = "ok";
                        }
                        Amount = Principal + interest + interest_A;
                        if (valret == "ok")
                        {
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                        if (valret != "ok")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 12 / 100));
                            Amount = Principal + interest;
                            RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                            string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod,receiptno)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "','" + txtReceiptNo.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                        }
                    }
                }
                else if (rmethod == "RBAL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                    Principal = initialAmount / rperiod;
                    interest = (rrate / 12 / 100) * LBalance;
                    Amount = Principal + interest;
                    // 'intrcharged = LoanAmount * (rRate / 200 * (RPeriod + 1)) / RPeriod
                    RepayableInterest = 0;// 'unpredictable
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSPECIAL")
                {
                    Double intTotal = 0;
                    Double PrincAmount = 0;
                    LBalance = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = initialAmount / rperiod;
                        // 'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    interest = intTotal / rperiod;
                    Amount = Principal + interest;
                    RepayableInterest = 0;
                    LBalance = loanbalance;// 'to continue with the previous flow
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSTL")
                {
                    pesaYote = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = pesaYote / rperiod;
                        // 'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    Amount = Principal + interest;
                    IntBalalance = intTotal;
                    RepayableInterest = 0;// 'unpredictable
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "ADV")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Principal = initialAmount / rperiod;
                    interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                    RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Amount = Principal + interest;
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else if (rmethod == "RSPECIAL")
                {
                    PrincAmount = 0;
                    LBalance = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        Principal = initialAmount / rperiod;
                        //'PrincAmount = PrincAmount + Principal
                        interest = (rrate / 12 / 100) * LBalance;
                        intTotal = intTotal + interest;
                        LBalance = LBalance - Principal;
                    }
                    interest = intTotal / rperiod;
                    Amount = Principal + interest;
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,receiptno,transtype)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "','" + txtReceiptNo.Text + "','LoanRepayment')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("No repay method detected for the loan to be loaded. Contact the administrator");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
            //  getRepayRate = Principal + interest;
        }
        private double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        protected void cboBankAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select BankName from banks where Accno='" + cboBankAC.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtBankAC.Text = dr["BankName"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPaymentMode.SelectedValue == "Cheque")
            {
                Label15.Visible = true;
                txtChequeNo.Visible = true;
            }
        }

        protected void cboGlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect glname = new WARTECHCONNECTION.cConnect();
            string glnames = "select s.SharesCode,s.SharesAcc,g.Glaccname from sharetype s inner join GLSETUP g on s.SharesAcc=g.accno where g.accno='" + cboGlAccountNo.Text + "'";
            dr = glname.ReadDB(glnames);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtGlAccountName.Text = dr["Glaccname"].ToString().Trim();
                    lblShareCode.Text = dr["SharesCode"].ToString().Trim();
                }
            }
            dr.Close(); dr.Dispose(); dr = null; glname.Dispose(); glname = null;
        }

        protected void txtReceiptAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double balAmount = Convert.ToDouble(txtBalance.Text);
                double receiptAmount = Convert.ToDouble(txtReceiptAmount.Text);
                if (receiptAmount != Convert.ToDouble(txtDistributedAmount.Text))
                {
                    WARSOFT.WARMsgBox.Show("The receipt amount should be the same as the distributed amount");
                    retbalance();
                    return;
                }
                double Balance = receiptAmount + balAmount;
                txtBalance.Text = Balance.ToString();
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void retbalance()
        {
            try
            {
                string sumAmnt = "select sum(amount)Amount from MembersAccount where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "'";
                WARTECHCONNECTION.cConnect sumamount = new WARTECHCONNECTION.cConnect();
                dr1 = sumamount.ReadDB(sumAmnt);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        double SummedAmnt = Convert.ToDouble(dr1["Amount"]);
                        double bal = -1 * Math.Ceiling(SummedAmnt);
                        txtBalance.Text = bal.ToString();
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; sumamount.Dispose(); sumamount = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtEditedDistributedAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {

                double value = 0;
                string loanno2 = "";
                string rmethod4 = "";
                if (txtContribDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("input dates first");
                }
                if (txtDateDeposited.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("input dates first");
                }
                else
                {
                    string lbal3 = "SELECT amount,vote,rmethod FROM  membersaccount WHERE memberno='" + txtMemberNo.Text + "'";
                    WARTECHCONNECTION.cConnect lbalance7 = new WARTECHCONNECTION.cConnect();
                    dr7 = lbalance7.ReadDB(lbal3);
                    if (dr7.HasRows)
                        while (dr7.Read())
                        {
                            value = Convert.ToDouble(dr7["amount"]);
                            loanno2 = dr7["vote"].ToString();
                            rmethod4 = dr7["rmethod"].ToString();
                        }
                    dr7.Close(); dr7.Dispose(); dr7 = null; lbalance7.Dispose(); lbalance7 = null;
                    string lbal2 = "SELECT lastdate FROM  loanbal WHERE LoanNo='" + loanno2 + "'";
                    WARTECHCONNECTION.cConnect lbalance1 = new WARTECHCONNECTION.cConnect();
                    dr6 = lbalance1.ReadDB(lbal2);
                    if (dr6.HasRows)
                        while (dr6.Read())
                        {
                            lastrepaydate = Convert.ToDateTime(dr6["lastdate"]);
                        }
                    dr6.Close(); dr6.Dispose(); dr6 = null; lbalance1.Dispose(); lbalance1 = null;
                    DateTime DateDeposited2 = Convert.ToDateTime(txtContribDate.Text);

                    TimeSpan D = (DateDeposited2 - lastrepaydate);
                    double nofdays = D.TotalDays;
                    if (rmethod4 == "AMRT")
                    {
                        //double L = value / 100;
                        if (nofdays >= 46 && nofdays <= 77)
                        {
                            penalty2 = Math.Round((0.05 * value), 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 78 && nofdays <= 108)
                        {
                            penalty2 = Math.Round((0.05 * value) * 2, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 109 && nofdays <= 139)
                        {
                            penalty2 = Math.Round((0.05 * value) * 3, 2);// 'Interest owed is loaded
                            // Amount = Amount + penalty2;
                        }
                        if (nofdays >= 140 && nofdays <= 170)
                        {
                            penalty2 = Math.Round((0.05 * value) * 4, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 171 && nofdays <= 201)
                        {
                            penalty2 = Math.Round((0.05 * value) * 5, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 202 && nofdays <= 232)
                        {
                            penalty2 = Math.Round((0.05 * value) * 6, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 233 && nofdays <= 268)
                        {
                            penalty2 = Math.Round((0.05 * value) * 7, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 269 && nofdays <= 300)
                        {
                            penalty2 = Math.Round((0.05 * value) * 8, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 301 && nofdays <= 332)
                        {
                            penalty2 = Math.Round((0.05 * value) * 9, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 333 && nofdays <= 364)
                        {
                            penalty2 = Math.Round((0.05 * value) * 10, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 365 && nofdays <= 396)
                        {
                            penalty2 = Math.Round((0.05 * value) * 11, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 397 && nofdays <= 442)
                        {
                            penalty2 = Math.Round((0.05 * value) * 12, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 443 && nofdays <= 488)
                        {
                            penalty2 = Math.Round((0.05 * value) * 13, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 489 && nofdays <= 534)
                        {
                            penalty2 = Math.Round((0.05 * value) * 14, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 535 && nofdays <= 580)
                        {
                            penalty2 = Math.Round((0.05 * value) * 15, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 581 && nofdays <= 626)
                        {
                            penalty2 = Math.Round((0.05 * value) * 16, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                        if (nofdays >= 627 && nofdays <= 672)
                        {
                            penalty2 = Math.Round((0.05 * value) * 17, 2);// 'Interest owed is loaded
                            //Amount = Amount + penalty2;
                        }
                    }
                    string uppdateAmount6 = "update loanbal set penalty='" + penalty2 + "' where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' and loanno='" + txtMembersAccountId.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(uppdateAmount6);
                    string uppdateAmount = "update MembersAccount set Amount='" + txtEditedDistributedAmount.Text + "' where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' and Vote='" + txtMembersAccountId.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(uppdateAmount);
                    Loaddatatogrid();
                    LoadDistributedAmounts();
                }

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Number is required");
                    txtMemberNo.Focus();
                    return;
                }
                if (txtNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Names are required.");
                    txtNames.Focus();
                    return;
                }
                if (cboBankAC.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Bank Account must be selected.");
                    cboBankAC.Focus();
                    return;
                }
                if (txtContribDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Contribution Date must be selected");
                    txtContribDate.Focus();
                    return;
                }
                if (cboPaymentMode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Payment Mode is required");
                    cboPaymentMode.Focus();
                    return;
                }
                if (cboPaymentMode.SelectedValue == "Cheque")
                {
                    if (txtChequeNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Cheque Number is required");
                        txtChequeNo.Focus();
                        return;
                    }
                }
                if (txtReceiptNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Receipt Number is required.");
                    txtReceiptNo.Focus();
                    return;
                }
                if (txtDateDeposited.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Date Deposited must be selected.");
                    txtDateDeposited.Focus();
                    return;
                }
                if (txtSerialNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("The serial/Reference number is required");
                    txtSerialNo.Focus();
                    return;
                }
                if (Convert.ToDouble(txtAmoutPayable.Text.Trim()) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("The amout to add as payment cannot be zero or less than zero");
                    txtAmoutPayable.Focus();
                    return;
                }
                string voddde = "set dateformat dmy select * from MembersAccount where vote='" + txtMemberNo.Text + "" + lblShareCode.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(voddde);
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("there already exist a payment with number " + txtMemberNo.Text + "" + lblShareCode.Text + "");
                    return;
                }
                else
                {
                    if (cboGlAccountNo.Text.Trim() == "")
                    {
                        WARSOFT.WARMsgBox.Show("Please choose the Account to add payment");
                        cboGlAccountNo.Focus();
                        return;
                    }
                    if (txtGlAccountName.Text.Trim() == "")
                    {
                        WARSOFT.WARMsgBox.Show("Please choose the Account name to add payment");
                        txtGlAccountName.Focus();
                        return;
                    }
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type,TransType,receiptno)values('" + txtMemberNo.Text + "" + lblShareCode.Text + "','" + txtGlAccountName.Text + "','" + txtAmoutPayable.Text + "','0.00','0.00','" + txtMemberNo.Text + "','" + txtNames.Text + "','" + lblShareCode.Text + "','Contrib','" + txtReceiptNo.Text + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    LoadDistributedAmounts();
                    Loaddatatogrid();
                    txtAmoutPayable.Text = "0.00";
                    WARSOFT.WARMsgBox.Show("Add payment with number " + txtMemberNo.Text + " " + lblShareCode.Text + " is sucessful");
                    return;
                }
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string delDATA = "delete from MembersAccount where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' and Vote='" + txtMembersAccountId.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(delDATA);
                LoadDistributedAmounts();
                Loaddatatogrid();
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void saveReceipt(string Receiptno, string reff, string RefNo, string mMemberno, string Mmberno, string Receipt, DateTime TransDate, double Amount, string chequeno, string ptype)
        {
            try
            {
                string todb = "set dateformat dmy INSERT INTO ReceiptBooking (ReceiptNo,Ref,Refno,MemberNo,Companycode,Name,Transdate,Amount, Chequeno, ptype, auditid,datedeposited,draccno) VALUES ('" + Receiptno + "','" + reff + "','" + RefNo + "','" + Mmberno + "','" + Mmberno + "','" + Receipt + "','" + TransDate + "'," + Amount + ",'" + chequeno + "','" + ptype + "','" + Session["mimi"].ToString() + "','" + DateTime.Now + "','" + cboBankAC.Text + "')";

                new WARTECHCONNECTION.cConnect().WriteDB(todb);

                string mysql = "set dateformat dmy Insert into Receiptno(Receiptno,Auditdate,auditid,memberno)values('" + Receiptno + "','" + TransDate + "','" + Session["mimi"].ToString() + "','" + mMemberno + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(mysql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Number is required");
                    txtMemberNo.Focus();
                    return;
                }
                if (txtNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Names are required.");
                    txtNames.Focus();
                    return;
                }
                if (cboBankAC.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Bank Account must be selected.");
                    cboBankAC.Focus();
                    return;
                }
                if (txtContribDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Contribution Date must be selected");
                    txtContribDate.Focus();
                    return;
                }
                if (cboPaymentMode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Payment Mode is required");
                    cboPaymentMode.Focus();
                    return;
                }
                if (cboPaymentMode.SelectedValue == "Cheque")
                {
                    if (txtChequeNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Cheque Number is required");
                        txtChequeNo.Focus();
                        return;
                    }
                }
                if (txtReceiptNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Receipt Number is required.");
                    txtReceiptNo.Focus();
                    return;
                }
                if (txtDateDeposited.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Date Deposited must be selected.");
                    txtDateDeposited.Focus();
                    return;
                }
                if (txtSerialNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("The serial/Reference number is required");
                    txtSerialNo.Focus();
                    return;
                }
                if (txtBalance.Text != "0")
                {
                    WARSOFT.WARMsgBox.Show("The Amount Received should be equal to the Amount Distributed.");
                    txtReceiptAmount.Focus();
                    return;
                }
                //retmesage();
                WARTECHCONNECTION.cConnect useddreceit = new WARTECHCONNECTION.cConnect();
                string sql = "select receiptno,chequeno from receiptbooking where receiptno ='" + txtReceiptNo.Text + "'";
                dr = useddreceit.ReadDB(sql);
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The receiptno is already used, Get another One!");
                    txtReceiptNo.Focus();
                    return;
                }
                dr.Close(); dr.Dispose(); useddreceit.Dispose(); useddreceit = null;
                string memebrtrans = "select MemberAccountId,Vote,TransDescription,Amount,rmethod,Principal,Interest,IntOwed,MemberNo,MemberNames,Type,TransType from MembersAccount where MemberNo='" + txtMemberNo.Text + "'";
                WARTECHCONNECTION.cConnect selda = new WARTECHCONNECTION.cConnect();
                dr1 = selda.ReadDB(memebrtrans);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        TransType = dr1["TransType"].ToString();
                        string MemberNo = dr1["MemberNo"].ToString();
                        string MemberNames = dr1["MemberNames"].ToString().Replace("'", " ");
                        double Amount = Convert.ToDouble(dr1["Amount"].ToString());
                        string Vote = dr1["Vote"].ToString();
                        string voteType = dr1["Type"].ToString();
                        string rmethod = dr1["rmethod"].ToString();
                        string TransDescription = dr1["TransDescription"].ToString();
                        double Principal = Convert.ToDouble(dr1["Principal"].ToString());
                        double Interest = Convert.ToDouble(dr1["Interest"].ToString());
                        double IntOwed = Convert.ToDouble(dr1["IntOwed"].ToString());
                        DateTime TimeNow = DateTime.Now;
                        transactionNo = Convert.ToString(TimeNow);
                        transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                        saveReceipt(txtReceiptNo.Text, "MPA", txtSerialNo.Text.Trim(), txtMemberNo.Text, txtMemberNo.Text, TransDescription, System.DateTime.Today, Convert.ToDouble(txtDistributedAmount.Text), txtChequeNo.Text, cboPaymentMode.Text);

                        if (TransType == "Contrib")
                        {
                            SaveTransaction();
                            Vote = voteType;
                            SaveContrib(MemberNo, txtContribDate.Text, txtDateDeposited.Text, Vote, Amount, cboBankAC.Text, txtReceiptNo.Text, txtChequeNo.Text, Session["mimi"].ToString(), TransDescription, transactionNo);
                            //SELECT     TOP (200) ID, MemberNo, TransactionNo, ReceiptNo, PaymentMode, TransactionType, Amount, ContributionDate, DepositedDate, AuditId, AuditTime, transDescription, status FROM         Transactions2
                            string TRANSACT = "set dateformat dmy insert into transactions2(MemberNo,TransactionNo, ReceiptNo, PaymentMode, TransactionType, Amount, ContributionDate, DepositedDate, AuditID)values('" + MemberNo + "','" + transactionNo + "','" + txtReceiptNo.Text + "','" + cboPaymentMode.Text + "','" + TransType + "','" + Amount + "','" + txtContribDate.Text + "','" + txtDateDeposited.Text + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(TRANSACT);

                            string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Contrib','Receipt Posting Shares Code" + Vote + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text.Trim() + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                            WARSOFT.WARMsgBox.Show("Posted Successfully");
                            if (Chckbxprintrcpt.Checked == true)
                            {
                                printreceipt1();
                            }
                            else
                            {
                                WARSOFT.WARMsgBox.Show("Posted Successfully");
                            }
                        }
                        else if (TransType == "LoanRepayment")
                        {
                            SaveTransaction();
                            SaveRepay(rmethod, txtSerialNo.Text, Vote, txtContribDate.Text, Amount, Principal, Interest, IntOwed, cboBankAC.Text, txtReceiptNo.Text, TransDescription, 0, 0, "Loan Recovery Rcpt(" + MemberNames + ")", Session["mimi"].ToString(), MemberNo, transactionNo, 1, 0);

                            string TRANSACT = "set dateformat dmy insert into transactions2(MemberNo,TransactionNo, ReceiptNo, PaymentMode, TransactionType, Amount, ContributionDate, DepositedDate, AuditID)values('" + MemberNo + "','" + transactionNo + "','" + txtReceiptNo.Text + "','" + cboPaymentMode.Text + "','" + TransType + "','" + Amount + "','" + txtContribDate.Text + "','" + txtDateDeposited.Text + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(TRANSACT);

                            string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Contrib','Receipt Posting Shares Code" + Vote + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text.Trim() + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                            if (Chckbxprintrcpt.Checked == true)
                            {
                                DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);

                                String today = System.DateTime.Today.ToString("yyyy/MM/dd");

                                string Date2 = DateTime.Now.ToString("h:mm:ss tt");

                                String format2 = "";

                                format2 = "</br> -------------LOAN PAYMENT RECEIPT----------------" +
                                         "</br> -----FEP SACCO LTD------------------" +
                                         "</br> MemberNo :" + MemberNo +
                                         "</br> Member Names  :" + MemberNames +
                                         "</br> Amount  :" + txtReceiptAmount.Text +
                                         "</br> Receipt Number  :" + txtReceiptNo.Text +
                                         "</br> Payment Mode  :" + cboPaymentMode.Text.Replace("%20", " ") +
                                         "</br> Transaction Type   :" + TransType +
                                         "</br> Date Deposited     :" + txtDateDeposited.Text +
                                         "</br> Contribution Date  :" + txtContribDate.Text +
                                         "</br> -------------------------------------------" +
                                         "</br> Received By    :" + User +
                                          "</br> --------------------------------------------" +
                                          "</br> Received By    :" + User +
                                         "</br> --------------------------------------------" +
                                         "</br> Date           :" + today + " ; " + Date2.Replace("%20", " ") +
                                         "</br> ------------FEP SACCO LTD--------------" +
                                         "</br>--HOLISTICALLY NURTURING ENTREPRENEURS--";
                                string url = "printreceipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtReceiptAmount.Text + "&&7=" + Session["mimi"].ToString() + "&&8=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "&&5=" + cboPaymentMode.Text + "&&4=" + txtReceiptNo.Text + "&&6=" + TransType + "&&9=" + DateTime.Now.ToString("h:mm:ss tt") + "&&10=" + txtDateDeposited.Text + "&&11=" + txtContribDate.Text + "";//&&4=" + txtBalance.Text + "
                                string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                            }
                            WARSOFT.WARMsgBox.Show("Posted Successfully");
                        }
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; selda.Dispose(); selda = null;
                clearTexts();
                Generate_ReceiptNo();
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void printloanreceipt()
        {
            try
            {
                DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);

                String today = System.DateTime.Today.ToString("yyyy/MM/dd");

                string Date2 = DateTime.Now.ToString("h:mm:ss tt");

                String format2 = "";

                format2 = "</br> -------------LOAN PAYMENT RECEIPT----------------" +
                         "</br> -----FEP SACCO LTD------------------" +
                         "</br> MemberNo :" + txtMemberNo.Text +
                         "</br> Member Names  :" + txtNames.Text +
                         "</br> Amount  :" + txtReceiptAmount.Text +
                         "</br> Receipt Number  :" + txtReceiptNo.Text +
                         "</br> Payment Mode  :" + cboPaymentMode.Text.Replace("%20", " ") +
                         "</br> Transaction Type  :" + TransType +
                         "</br> Date Deposited  :" + txtDateDeposited.Text +
                         "</br> Contribution Date  :" + txtContribDate.Text +
                         "</br> -------------------------------------------" +
                         "</br> Received By    :" + User +
                          "</br> --------------------------------------------" +
                          "</br> Received By    :" + User +
                         "</br> --------------------------------------------" +
                         "</br> Date           :" + today + " ; " + Date2.Replace("%20", " ") +
                         "</br> ------------FEP SACCO LTD--------------" +
                         "</br>--HOLISTICALLY NURTURING ENTREPRENEURS--";
                string url = "printreceipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtReceiptAmount.Text + "&&7=" + Session["mimi"].ToString() + "&&8=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "&&5=" + cboPaymentMode.Text + "&&4=" + txtReceiptNo.Text + "&&6=" + TransType + "&&9=" + DateTime.Now.ToString("h:mm:ss tt") + "&&10=" + txtDateDeposited.Text + "&&11=" + txtContribDate.Text + "";//&&4=" + txtBalance.Text + """;//&&4=" + txtBalance.Text + "
                string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void printreceipt1()
        {
            try
            {
                DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);

                String today = System.DateTime.Today.ToString("yyyy/MM/dd");

                string Date2 = DateTime.Now.ToString("h:mm:ss tt");

                String format2 = "";

                format2 = "</br> -------------LOAN PAYMENT RECEIPT----------------" +
                         "</br> -----FEP SACCO LTD------------------" +
                         "</br> MemberNo :" + txtMemberNo.Text +
                         "</br> Member Names  :" + txtNames.Text +
                         "</br> Amount  :" + txtReceiptAmount.Text +
                         "</br> Receipt Number  :" + txtReceiptNo.Text +
                         "</br> Payment Mode  :" + cboPaymentMode.Text.Replace("%20", " ") +
                         "</br> Transaction Type  :" + TransType +
                         "</br> Date Deposited    :" + txtDateDeposited.Text +
                          "</br>Contribution Date  :" + txtContribDate.Text +
                         "</br> -------------------------------------------" +
                         "</br> Received By    :" + User +
                          "</br> --------------------------------------------" +
                          "</br> Received By    :" + User +
                         "</br> --------------------------------------------" +
                         "</br> Date           :" + today + " ; " + Date2.Replace("%20", " ") +
                         "</br> ------------FEP SACCO LTD--------------" +
                         "</br>--HOLISTICALLY NURTURING ENTREPRENEURS--";
                string url = "printreceipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtReceiptAmount.Text + "&&7=" + Session["mimi"].ToString() + "&&8=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "&&5=" + cboPaymentMode.Text + "&&4=" + txtReceiptNo.Text + "&&6=" + TransType + "&&9=" + DateTime.Now.ToString("h:mm:ss tt") + "&&10=" + txtDateDeposited.Text + "&&11=" + txtContribDate.Text + "";//&&4=" + txtBalance.Text + """;//&&4=" + txtBalance.Text + "
                string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            try
            {
                txtMemberNo.Text = "";
                txtBalance.Text = "";
                txtBankAC.Text = "";
                txtChequeNo.Text = "";
                txtDateDeposited.Text = DateTime.Today.ToString();
                txtDistributedAmount.Text = "";
                txtEditedDistributedAmount.Text = "";
                txtGlAccountName.Text = "";
                txtMembersAccountId.Text = "";
                txtReceiptAmount.Text = "";
                txtReceiptNo.Text = "";
                cboBankAC.Text = "";
                cboGlAccountNo.Text = "";
                cboPaymentMode.Text = "";
                txtSerialNo.Text = "";
                txtDateDeposited.Text = "";
                txtContribDate.Text = "";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveRepay(string rmethod, string SERIALNO, string Vote, string DateDeposited, double Amount, double Principal, double Interest, double IntOwed, string BankAC, string ReceiptNo, string TransDescription, int p_4, int p_5, string MemberNames, string User, string MemberNo, string transactionNo, int p_8, int p_9)
        {
            try
            {
                // DateTime lastrepaydate;
                Double Interest2 = 0;
                Double Amount2 = 0;
                double mrepayment2 = 0;
                double mrepayment3 = 0;
                string readdataLoan = "Select m.memberno,m.surname +' '+m.othernames as names,C.Amount,lt.loantype,lt.MDTEI,LB.LoanNo,LB.LoanCode,LB.MemberNo,LB.Balance,LB.IntrOwed,LB.FirstDate,LB.RepayRate,LB.LastDate,LB.duedate,LB.intrCharged,LB.Interest,LB.Penalty,LB.RepayMethod,LB.Cleared,LB.AutoCalc,LB.IntrAmount,LB.RepayPeriod,LB.Remarks,LB.IntBalance,LB.CategoryCode,LB.InterestAccrued,LB.Defaulter,LB.Processdate,LB.RepayMode From LOANBAL LB Inner Join CHEQUES C On LB.LoanNo=C.LoanNo inner join loantype lt on lb.loancode=lt.loancode inner join members m on lb.memberno=m.memberno Where LB.MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' and LB.loanNo='" + Vote + "' And LB.Balance>=1 ";
                WARTECHCONNECTION.cConnect conet = new WARTECHCONNECTION.cConnect();
                dr = conet.ReadDB(readdataLoan);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int totalrepayable = 0;
                        string memberno = dr["memberno"].ToString();
                        //string MemberNames = dr["names"].ToString().Replace("'", " ");
                        // txtNames.Text = MemberNames;
                        Amount2 = Convert.ToDouble(dr["Amount"].ToString());
                        Double IntBalance = Convert.ToDouble(dr["IntBalance"].ToString());
                        string vote = dr["LoanNo"].ToString();
                        //string Principal = "0.00";
                        Interest2 = Convert.ToDouble(dr["Interest"].ToString().TrimStart().TrimEnd());
                        Double Balance = Convert.ToDouble(dr["Balance"].ToString().TrimStart().TrimEnd());
                        rperiod = Convert.ToInt32(dr["RepayPeriod"].ToString().TrimStart().TrimEnd());
                        string Type = dr["LoanNo"].ToString();
                        //string TransDescription = dr["loantype"].ToString();
                        //string rmethod = dr["RepayMethod"].ToString().TrimStart().TrimEnd();
                        string lastrepaydate = dr["LastDate"].ToString().TrimStart().TrimEnd();
                        string firstdate = dr["firstdate"].ToString().TrimStart().TrimEnd();
                        double IntrOwed = Convert.ToDouble(dr["IntrOwed"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; conet.Dispose(); conet = null;
                string lbal8 = "SELECT penalty FROM  LOANBAL WHERE LoanNo='" + Vote + "'";
                WARTECHCONNECTION.cConnect lbalance90 = new WARTECHCONNECTION.cConnect();
                dr4 = lbalance90.ReadDB(lbal8);
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        penalty2 = Convert.ToDouble(dr4["penalty"].ToString());
                        break;
                    }
                }
                dr4.Close(); dr4.Dispose(); dr4 = null; lbalance90.Dispose(); lbalance90 = null;
                string loancode = Vote.Substring(0, 3);
                double TotalOwed = IntOwed + Interest;
                double myAmount = Amount;
                double myCharged = Interest;
                double mybalance = 0;
                double myInterest = 0;
                double MYpenalty = penalty2;
                mrepayment2 = Math.Round(Pmt(Interest2, rperiod, Amount2, 0), 2);
                DateTime DateDeposited2 = Convert.ToDateTime(txtContribDate.Text).AddDays(31);
                string lbal = "SELECT Balance FROM  LOANBAL WHERE LoanNo='" + Vote + "'";
                WARTECHCONNECTION.cConnect lbalance = new WARTECHCONNECTION.cConnect();
                dr = lbalance.ReadDB(lbal);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mybalance = Convert.ToDouble(dr["Balance"].ToString());
                        break;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; lbalance.Dispose(); lbalance = null;

                if (Amount > 0)
                {
                    if (Amount >= TotalOwed)
                    {
                        Amount = Amount - TotalOwed;
                        intOwed = TotalOwed;
                        IntOwed = 0;
                        myInterest = TotalOwed;
                    }
                    else// (Amount < IntOwed)
                    {
                        myInterest = Amount;
                        IntOwed = TotalOwed - Amount;
                        Principal = 0;
                        Amount = 0;
                    }
                    if (Amount > 0)
                    {
                        Principal = Amount;
                        Amount = 0;
                    }
                }
                int PaymentNo = 0;
                string PayNo = "select isnull(max(PaymentNo),0)PaymentNo from Repay where Loanno='" + Vote + "'";
                WARTECHCONNECTION.cConnect ppayno = new WARTECHCONNECTION.cConnect();
                dr = ppayno.ReadDB(PayNo);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        PaymentNo = Convert.ToInt32(dr["PaymentNo"].ToString());
                        PaymentNo = PaymentNo + 1;
                    }
                dr.Close(); dr.Dispose(); dr = null; ppayno.Dispose(); ppayno = null;
                mybalance = mybalance - Principal + MYpenalty;
                string TransactionNo = "";
                string TransNo = "select top 1 TransactionNo from transactions ORDER BY AuditTime DESC";
                WARTECHCONNECTION.cConnect TNo = new WARTECHCONNECTION.cConnect();
                dr = TNo.ReadDB(TransNo);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        TransactionNo = dr["TransactionNo"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; TNo.Dispose(); TNo = null;

                if (rmethod == "STL")
                {
                    double RepayableInterest2 = 0;
                    double Principal2 = 0;
                    RepayableInterest2 = Math.Ceiling((Amount2 * (Interest2 / 12 / 100) * rperiod));
                    Principal2 = Amount2 / rperiod;
                    mrepayment3 = Principal2 + RepayableInterest2;
                    WARTECHCONNECTION.cConnect balala = new WARTECHCONNECTION.cConnect();
                    string innntbal = "SELECT M.IntOwed,LB.IntBalance FROM MembersAccount M INNER JOIN LOANBAL LB ON M.Vote=LB.LoanNo where M.Vote='" + Vote + "'";
                    Dr = balala.ReadDB(innntbal);
                    if (Dr.HasRows)
                        while (Dr.Read())
                        {
                            double Balanc = 0;
                            double intowedd = Convert.ToDouble(Dr["IntOwed"].ToString());
                            double intballala = Convert.ToDouble(Dr["IntBalance"].ToString());
                            if (intowedd > intballala)
                            {
                                Balanc = 0;
                            }
                            else
                            {
                                Balanc = intballala - intowedd;
                            }
                            string sql = "set dateformat dmy Insert into Repay(Loanno,SERIALNO,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,ChequeNo,TransBy,Remarks,auditid,TransactionNo,RepayRate,nextduedate) Values('" + Vote + "','" + SERIALNO + "','" + DateDeposited + "','" + PaymentNo + "'," + myAmount + "," + Principal + "," + myInterest + "," + myCharged + "," + IntOwed + ",'" + MYpenalty + "', '0','" + mybalance + "','" + ReceiptNo + "','" + txtChequeNo.Text.Trim() + "','" + Session["mimi"].ToString() + "','" + TransDescription + "','" + Session["mimi"].ToString() + "','" + TransactionNo + "','" + mrepayment3 + "','" + DateDeposited2 + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(sql);
                            string updatesql = "set dateformat dmy UPDATE loanbal set balance=" + mybalance + ",intrOwed=" + IntOwed + ",intbalance='" + Balanc + "' ,lastdate='" + DateDeposited + "',penalty='" + MYpenalty + "',nextduedate='" + DateDeposited2 + "' where loanno='" + Vote + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updatesql);
                        }
                    Dr.Close(); Dr.Dispose(); Dr = null; balala.Dispose(); balala = null;
                }
                else
                {

                    string sql = "set dateformat dmy Insert into Repay(Loanno,SERIALNO,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo,penalty,RepayRate,nextduedate) Values('" + Vote + "','" + SERIALNO + "','" + DateDeposited + "','" + PaymentNo + "'," + myAmount + "," + Principal + "," + myInterest + "," + myCharged + "," + IntOwed + ", '0','" + mybalance + "','" + ReceiptNo + "','" + Session["mimi"].ToString() + "','" + TransDescription + "','" + Session["mimi"].ToString() + "','" + TransactionNo + "','" + MYpenalty + "','" + mrepayment2 + "','" + DateDeposited2 + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sql);
                    string updatesql = "set dateformat dmy UPDATE loanbal set balance=" + mybalance + ",intrOwed=" + IntOwed + " ,lastdate='" + txtContribDate.Text + "',penalty='" + MYpenalty + "',nextduedate='" + DateDeposited2 + "' where loanno='" + Vote + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(updatesql);
                }
                string LoanAcc = "";
                string interestAcc = "";
                string penaltyAcc = "";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                DR = oSaccoMaster.ReadDB("select lt.interestAcc,lt.loanAcc,lt.penaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Vote + "'");
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
                if (Interest > 0)
                {
                    Save_GLTRANSACTION_INTEREST(DateDeposited, Interest, cboBankAC.Text, interestAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), TransDescription + '-' + "Interest", 1, 1, txtChequeNo.Text, TransactionNo);
                }
                if (penalty2 > 0)
                {
                    Save_GLTRANSACTION_PENALTY(DateDeposited, penalty2, cboBankAC.Text, penaltyAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), TransDescription + '-' + "penalty", 1, 1, txtChequeNo.Text, TransactionNo);
                }
                if (Principal > 0)
                {
                    Save_GLTRANSACTION_PRINCIPAL(DateDeposited, Principal, cboBankAC.Text, LoanAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), TransDescription + '-' + "Principal", 1, 1, txtChequeNo.Text, TransactionNo);
                }
                RefreshGuarantors(Vote);
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void RefreshGuarantors(string Loanno)
        {
            try
            {
                //'get how much was guaranteed
                string mno = "";
                double guarTotal = 0, GuarAmount = 0, guarBalance = 0;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(lg.Amount),0) as TotGuaranteed,isnull(sum(lg.Balance),0) TotGuarBalance,lb.Balance from loanguar lg inner join loanbal lb on lg.loanno=lb.loanno where lg.loanno='" + Loanno + "' and transfered=0 group by lb.loanno,lb.balance");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        guarTotal = Convert.ToDouble(dr["TotGuaranteed"].ToString());
                        loanbalance = Convert.ToDouble(dr["Balance"]);
                        dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select Memberno,Amount From loanguar where loanno='" + Loanno + "' and transfered=0");
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                txtMemberNo.Text = dr2["MemberNo"].ToString();
                                GuarAmount = Convert.ToDouble(dr2["Amount"]);
                                if (loanbalance < 1)
                                {
                                    loanbalance = 0;
                                }
                                else
                                {
                                    loanbalance = loanbalance;
                                }
                                guarBalance = (GuarAmount / guarTotal) * (loanbalance);
                                new WARTECHCONNECTION.cConnect().WriteDB("Update loanGuar set balance=" + guarBalance + " where loanno='" + Loanno + "' and memberno='" + txtMemberNo.Text + "'");
                            }
                        }
                        dr2.Close(); dr2.Dispose(); dr2 = null;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }


        private void Save_GLTRANSACTION_PRINCIPAL(string DateDeposited, double Principal, string cboBankAC, string interestAcc, string ReceiptNo, string MemberNo, string User, string TransDescription, int CashBook, int doc_posted, string ChequeNo, string TransactionNo)
        {
            String insertGL = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "','" + Principal + "','" + cboBankAC + "','" + interestAcc + "','" + ReceiptNo + "','" + MemberNo + "','" + Session["mimi"].ToString() + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + ChequeNo + "','" + TransactionNo + "','BOSA'";
            new WARTECHCONNECTION.cConnect().WriteDB(insertGL);
        }
        private void Save_GLTRANSACTION_PENALTY(string DateDeposited, double Principal, string cboBankAC, string penaltyacc, string ReceiptNo, string MemberNo, string User, string TransDescription, int CashBook, int doc_posted, string ChequeNo, string TransactionNo)
        {
            String insertGL = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "','" + penalty2 + "','" + cboBankAC + "','" + penaltyacc + "','" + ReceiptNo + "','" + MemberNo + "','" + Session["mimi"].ToString() + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + ChequeNo + "','" + TransactionNo + "','BOSA'";
            new WARTECHCONNECTION.cConnect().WriteDB(insertGL);
        }
        private void Save_GLTRANSACTION_INTEREST(string DateDeposited, double interest, string cboBankAC, string interestAcc, string ReceiptNo, string MemberNo, string User, string TransDescription, int CashBook, int doc_posted, string ChequeNo, string TransactionNo)
        {
            try
            {
                String insertGL = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "','" + interest + "','" + cboBankAC + "','" + interestAcc + "','" + ReceiptNo + "','" + MemberNo + "','" + Session["mimi"].ToString() + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + ChequeNo + "','" + TransactionNo + "','BOSA'";
                new WARTECHCONNECTION.cConnect().WriteDB(insertGL);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveContrib(string MemberNo, string contribdate, string DateDeposited, string Vote, double Amount, string BankAC, string ReceiptNo, string ChequeNo, string user, string TransDescription, string transactionNo)
        {
            try
            {
                double TotalShares = 0;
                string sql = "select isnull(sum(contrib.amount),0)amount  from contrib  where contrib.memberno='" + MemberNo + "' and contrib.sharescode='" + Vote + "' group by contrib.memberno";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TotalShares = Convert.ToDouble(dr["amount"]);
                        TotalShares = TotalShares + Amount;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
                string sqlinsert = "set dateformat dmy Insert into Contrib(memberno,contrdate,DepositedDate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)Values('" + MemberNo + "','" + contribdate + "','" + DateDeposited + "','" + txtSerialNo.Text + "'," + Amount + "," + TotalShares + ",'Receipt-" + cboPaymentMode.Text + "','" + ChequeNo + "','" + ReceiptNo + "','" + TransDescription + "','" + Session["mimi"].ToString() + "','" + Vote + "','" + transactionNo + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sqlinsert);

                WARTECHCONNECTION.cConnect SaccoMaster = new WARTECHCONNECTION.cConnect();
                string sqldatta = "select SharesAcc from sharetype where sharescode='" + Vote + "'";
                dr = SaccoMaster.ReadDB(sqldatta);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string SharesAcc = dr["SharesAcc"].ToString();
                        if (SharesAcc == null || SharesAcc == "")
                        {
                            WARSOFT.WARMsgBox.Show("The Gl Control Account for this scheme(" + Vote.Trim() + ") is not set");
                            return;
                        }
                        DateTime TimeNow = DateTime.Now;
                        transactionNo = Convert.ToString(TimeNow);
                        transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                        Save_GLTRANSACTIONS(txtDateDeposited.Text, Amount, cboBankAC.Text, SharesAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), "Member Receipt" + '-' + TransDescription, "1", "1", txtChequeNo.Text, transactionNo, "bosa");
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Invalid Share code(" + Vote + ")");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null; SaccoMaster.Dispose(); SaccoMaster = null;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Save_GLTRANSACTIONS(string DateDeposited, double ReceiptAmount, string BankAC, string SharesAcc, string ReceiptNo, string MemberNo, string User, string MemberReceipt, string p_8, string p_9, string ChequeNo, string transactionNo, string bosa)
        {
            try
            {
                string saccoinsert = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "'," + ReceiptAmount + ",'" + BankAC + "','" + SharesAcc + "','" + ReceiptNo + "','" + MemberNo + "','" + Session["mimi"].ToString() + "','" + MemberReceipt + "'," + p_8 + "," + p_9 + ",'" + ChequeNo + "','" + transactionNo + "','" + bosa + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(saccoinsert);

                //string updreceipt = "Set DateFormat DMY update ReceiptBooking set Draccno='" + BankAC + "',Craccno='" + SharesAcc + "' where";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveTransaction()
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," + txtReceiptAmount.Text + ",'" + Session["mimi"].ToString() + "','" + txtDateDeposited.Text + "','" + "Receipt posting RCPTNO:" + txtReceiptNo.Text + "" + "','Posted')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; };
        }
        private void retmesage()
        {
            WARSOFT.WARMsgBox.Show("Do you want to post this receipt no: " + txtReceiptNo.Text + "?");
            return;
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            if (txtContribDate.Text == "")
            {
                WARSOFT.WARMsgBox.Show("input dates first");
                txtMemberNo.Text = "";
                return;
            }
            if (txtDateDeposited.Text == "")
            {
                WARSOFT.WARMsgBox.Show("input dates first");
                txtMemberNo.Text = "";
                return;
            }
            else
            {
                MemberNoshow();
            }

        }
        private void MemberNoshow()
        {
            try
            {
                bool NewMember = false;
                string companycode = "";
                DR = new WARTECHCONNECTION.cConnect().ReadDB("select companycode from Useraccounts1 where UserLoginid='" + Session["mimi"].ToString() + "'");
                if (DR.HasRows)
                    while (DR.Read())
                    {
                        companycode = DR["companycode"].ToString();
                    }
                DR.Close();
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select m.surname,m.othernames,m.HomeAddr,m.companycode,m.entrance,m.companycode  from members m  where m.memberno ='" + txtMemberNo.Text + "' and m.companycode='" + companycode + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        bool IsGroup = false;
                        txtNames.Text = dr["surname"].ToString() + ' ' + dr["othernames"].ToString();

                        string entrance1 = dr["entrance"].ToString();
                        bool entrance = Convert.ToBoolean(entrance1);
                        if (entrance == true)
                        {
                            NewMember = true;
                        }
                        else
                        {
                            NewMember = false;
                        }
                        if (dr["othernames"].ToString() == "")
                        {
                            IsGroup = true;
                        }
                        else
                        {
                            IsGroup = false;
                        }
                    }
                }
                else
                {
                    txtNames.Text = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
                LoadMemberDetails(NewMember);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label22.Visible = true;
            DropDownList1.Visible = true;
            TextBox1.Visible = true;
            Button1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView.Visible = false;
            try
            {
                string companycode = "";
                DR = new WARTECHCONNECTION.cConnect().ReadDB("select companycode from Useraccounts1 where UserLoginid='" + Session["mimi"].ToString() + "'");
                if (DR.HasRows)
                    while (DR.Read())
                    {
                        companycode = DR["companycode"].ToString();
                    }
                DR.Close();
                if (DropDownList1.Text == "Names")
                {
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where surname like'%" + TextBox1.Text + "%' or OtherNames like'%" + TextBox1.Text + "%' and m.companycode='" + companycode + "'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else if (DropDownList1.Text == "ID No")
                {
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where IDNo ='" + TextBox1.Text + "' and m.companycode='" + companycode + "'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("No Details to Show"); return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
                MemberNoshow();
                Label22.Visible = false;
                DropDownList1.Visible = false;
                TextBox1.Visible = false;
                Button1.Visible = false;
                GridView2.Visible = false;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
        }

        private static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        private static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        private static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            MemberNoshow();
        }

        protected void chkAcruedInterest_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Enabled = true;
        }

        protected void btnFindBank_Click(object sender, EventArgs e)
        {
            Label22.Visible = true;
            DropDownList1.Visible = true;
            TextBox1.Visible = true;
            Button1.Visible = true;
        }
    }
}