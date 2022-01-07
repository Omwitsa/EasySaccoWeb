using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing;
using System.Diagnostics;

namespace USACBOSA.FinanceAdmin
{
    public partial class MPA_Paymentposting : System.Web.UI.Page
    {
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
        public DateTime duedate= DateTime.Today;
        public DateTime duedate2;
       
        public bool NewMember = false;
        public int loops = 0;
        public string rmethod = "";
        public string intRecovery = "";
        public int mdtei = 0;
        public DateTime lastrepay;
        public DateTime Dateissued;

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
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, DR522;
        System.Data.SqlClient.SqlDataAdapter da;

        private double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateDeposited.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex =0;
                    MultiView1.ActiveViewIndex = 0;
                try
                {
                    if (Session["mimi"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
                Generate_VoucherNo();
                LoadBanks();
                if (MultiView1.ActiveViewIndex == 1)
                {

                }
                else
                {
                    LoadPendingPayments();
                }
            }
        }

        private void Generate_VoucherNo()
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
                string myddd = "select isnull(MAX(RIGHT(VoucherNo,3)),0)+1 ccount from Paymentbooking  where VoucherNo like '%VNO%' and year(TransDate)='" + (System.DateTime.Today).Year + "' and month(TransDate)='" + (System.DateTime.Today).Month + "' and day(TransDate)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        int nextno = maxno;
                        txtVoucherNo.Text = "VNO" + ssessionuser + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno).ToString()).PadLeft(3, '0');
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private delegate void setTextDeleg(string text);
        public delegate void myDelegate();

        private void LoadBanks()
        {
            try
            {
                cboBanks.Items.Add("");
                string sql = "select accno from banks";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboBanks.Items.Add(dr["accno"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadPendingPayments()
        {
            try
            {
                new WARTECHCONNECTION.cConnect().WriteDB("truncate table pendingpayments");
                MultiView1.ActiveViewIndex = -0;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ToString());
                string query, query2, query3, query4, myquery;
                SqlCommand SqlCommand;
                SqlDataReader reader;
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Open the connection to db
                conn.Open();
                double bal = 0; double EndorsedBal = 0; string MLoanNo = "";
                string ssssql = "Select balance,LoanNo from cheques where balance>0";
                    dr5 = new WARTECHCONNECTION.cConnect().ReadDB(ssssql);
                    if (dr5.HasRows)
                    {
                        while (dr5.Read())
                        {
                             bal = Convert.ToDouble(dr5["balance"]);
                              MLoanNo = dr5["LoanNo"].ToString().Trim();
                            if (EndorsedBal > bal)
                            {
                                new WARTECHCONNECTION.cConnect().WriteDB("update loans set status=3 where status=3 and LoanNo='" + MLoanNo + "' ");
                            }
                            else
                            {
                                new WARTECHCONNECTION.cConnect().WriteDB("update loans set status=4 where status=3 and LoanNo='" + MLoanNo + "'");
                            }
                        }
                    }
                    dr5.Close(); dr5.Dispose(); dr5 = null;
                //Generating the query to fetch the contact details
                query = "SELECT m.MemberNo, m.Surname + ' ' + m.OtherNames AS membernames, l.LoanAmt, lt.LoanCode, lt.LoanType, l.LoanNo, e.AmtApproved, e.MeetingDate,SUM(IsNull(dd.Amount, 0)) As Deductions  FROM ENDMAIN e INNER JOIN  LOANS l ON e.LoanNo = l.LoanNo INNER JOIN MEMBERS m ON l.MemberNo = m.MemberNo LEFT JOIN LOANTYPE lt ON l.LoanCode = lt.LoanCode LEFT OUTER JOIN DisbursementDeduction dd ON e.LoanNo = dd.LoanNo WHERE  l.status=3 and e.accepted=1 GROUP BY m.MemberNo, l.LoanNo, m.Surname + ' ' + m.OtherNames, l.LoanAmt, lt.LoanCode, lt.LoanType, e.AmtApproved, e.MeetingDate ORDER BY e.MeetingDate DESC";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string MemberNo = dr["MemberNo"].ToString().Replace("'", " ");
                        string MemberNames = dr["membernames"].ToString().Replace("'", " ");
                        string LoanShareCode = dr["LoanNo"].ToString().Replace("'", " ");
                        DateTime DateApproved = Convert.ToDateTime(dr["MeetingDate"].ToString());
                        double AmountPayable = Convert.ToDouble(dr["AmtApproved"].ToString());
                        double Balance = Convert.ToDouble(dr["AmtApproved"].ToString());
                        string Purpose = dr["LoanType"].ToString().Replace("'", " ");
                        string Narration = dr["LoanType"].ToString().Replace("'", " ");
                        double Deductions = Convert.ToDouble(dr["Deductions"]);
                        string isnnndadata = "set dateformat dmy Insert into PendingPayments(MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration,Deductions)values('" + MemberNo + "','" + MemberNames + "','" + LoanShareCode + "' ,'" + DateApproved + "','" + AmountPayable + "','" + Balance + "','Loan','" + Narration + "','" + Deductions + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(isnnndadata);
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                query2 = "SELECT l.memberno,m.surname + ' ' + m.othernames as memberNames,e.LoanNo,lt.LoanType, e.AmtApproved,c.balance,e.MeetingDate FROM ENDMAIN e INNER JOIN   LOANS l ON e.LoanNo = l.LoanNo INNER JOIN  LOANTYPE lt ON l.LoanCode = lt.LoanCode inner join members m on l.memberno=m.memberno inner join cheques c on l.loanno=c.loanno WHERE c.balance>0";
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB(query2);
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        string MemberNo = dr2["MemberNo"].ToString().Replace("'", " ");
                        string MemberNames = dr2["membernames"].ToString().Replace("'", " ");
                        string LoanShareCode = dr2["LoanNo"].ToString().Replace("'", " ");
                        DateTime DateApproved = Convert.ToDateTime(dr2["MeetingDate"].ToString());
                        double AmountPayable = Convert.ToDouble(dr2["AmtApproved"].ToString());
                        double Balance = Convert.ToDouble(dr2["balance"].ToString());
                        string Purpose = dr2["LoanType"].ToString().Replace("'", " ");
                        string Narration = dr2["LoanType"].ToString().Replace("'", " ");
                        string isnnndadata = "set dateformat dmy Insert into PendingPayments(MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration)values('" + MemberNo + "','" + MemberNames + "','" + LoanShareCode + "' ,'" + DateApproved + "','" + AmountPayable + "','" + Balance + "','Loan','" + Narration + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(isnnndadata);
                    }
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
                query3 = "select m.memberno,m.surname+' '+ m.othernames as membernames,s.sharescode,s.Amount,s.withdrawaldate,purpose from members m inner join sharewithdrawal s on m.memberno=s.memberno where s.withdrawn=0";
                dr3 = new WARTECHCONNECTION.cConnect().ReadDB(query3);
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        string MemberNo = dr3["memberno"].ToString().Replace("'", " ");
                        string MemberNames = dr3["membernames"].ToString().Replace("'", " ");
                        string LoanShareCode = dr3["sharescode"].ToString();
                        DateTime DateApproved = Convert.ToDateTime(dr3["withdrawaldate"].ToString());
                        double AmountPayable = Convert.ToDouble(dr3["Amount"].ToString());
                        double Balance = Convert.ToDouble(dr3["Amount"].ToString());
                        string Purpose = dr3["purpose"].ToString().Replace("'", " ");
                        string Narration = dr3["purpose"].ToString().Replace("'", " ");
                        string isnnndadata = "set dateformat dmy Insert into PendingPayments(MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration)values('" + MemberNo + "','" + MemberNames + "','" + LoanShareCode + "' ,'" + DateApproved + "','" + AmountPayable + "','" + Balance + "','Shares','" + Narration + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(isnnndadata);
                    }
                }
                dr3.Close(); dr3.Dispose(); dr3 = null;
                query4 = "select m.memberno,m.surname+' '+ m.othernames as membernames,R.RefCode,R.RefundAmount as Amount,R.BookingDate,R.refundItem from members m inner join Refunds R on m.memberno=R.memberno where R.Done=0";
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB(query4);
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        string MemberNo = dr4["memberno"].ToString().Replace("'"," ");
                        string MemberNames = dr4["membernames"].ToString().Replace("'", " ");
                        string LoanShareCode = dr4["RefCode"].ToString().Replace("'", " ");
                        DateTime DateApproved = Convert.ToDateTime(dr4["BookingDate"].ToString());
                        double AmountPayable = Convert.ToDouble(dr4["Amount"].ToString());
                        double Balance = Convert.ToDouble(dr4["Amount"].ToString());
                        string Purpose = dr4["refundItem"].ToString().Replace("'", " ");
                        string Narration = dr4["refundItem"].ToString().Replace("'", " ");

                        string isnnndadata = "set dateformat dmy Insert into PendingPayments(MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration)values('" + MemberNo + "','" + MemberNames + "','" + LoanShareCode + "' ,'" + DateApproved + "','" + AmountPayable + "','" + Balance + "','Loan','" + Narration + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(isnnndadata);
                    }
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
                //
                double Balanceamount = 0;
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration,Deductions from pendingpayments order by id asc");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        double AmountPayable = Convert.ToDouble(dr1["AmountPayable"]);
                        Balanceamount = Convert.ToDouble(dr1["Balance"]);
                        double Deductions = Convert.ToDouble(dr1["Deductions"]);
                        string LoanShareCode = dr1["LoanShareCode"].ToString();
                        if (Deductions > 0)
                        {
                            Balanceamount = AmountPayable - Deductions;
                        }
                        else
                        {
                            Balanceamount = Balanceamount;
                        }
                        new WARTECHCONNECTION.cConnect().WriteDB("update pendingpayments set Balance='" + Balanceamount + "' where LoanShareCode='" + LoanShareCode + "'");
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                myquery = "select MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration from pendingpayments order by id asc";
                SqlCommand = new SqlCommand(myquery, conn);
                adapter.SelectCommand = new SqlCommand(myquery, conn);

                //execute the query
                reader = SqlCommand.ExecuteReader();

                //Assign the results 
                GridView1.DataSource = reader;

                //Bind the data
                GridView1.DataBind();
                conn.Close();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                if (cboBanks.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Choose the Receiving Control Account");
                    cboBanks.Focus();
                    return;
                }
                if (txtVoucherNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please generate the voucher number");
                    txtVoucherNo.Text = "";
                }
                if (Convert.ToDouble(txtAmountPaid.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("Amount should be greater than zero");
                    return;
                }
                if (Convert.ToDouble(txtBalance.Text) > 0)
                {
                    WARSOFT.WARMsgBox.Show("That Amount is greater than the Remaining Balance, please revise");
                    txtBalance.Focus();
                    return;
                }
                else if (Convert.ToDouble(txtBalance.Text) < 0 && chkIspartpayment.Checked == false)
                {
                    WARSOFT.WARMsgBox.Show("Is this a part Payments?, Please confirm then proceed to post");// here someone needs to check to confirm part payment, Amos 
                    chkIspartpayment.Focus();
                    return;
                }

                if (txtAmountPaid.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Amount should be have a figure on it");
                    return;
                }
                if (cboPaymentMode.Text == "Cheque")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Cheque number Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "Cash")
                {
                    if (txtVoucherNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Cash Receipt No Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "EFT")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("EFT Receipt No Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "Mpesa")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Mpesa Receipt No Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "Zap")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Zap Receipt No Required");
                        return;
                    }
                }

                string dsql = "select voucherno from PaymentBooking where voucherno='" + txtVoucherNo.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(dsql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        WARSOFT.WARMsgBox.Show("That voucherno is already used, Get another One!");
                        return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;

               
                //'/////Check Kama Analibia Vitu Vingine,
                // '/////and also we don't forget to Pay for the refinance if applicable-----Cosmas

                string myItem = "";
                // 'save TransactionNo
                double transactionTotal = Convert.ToDouble(txtAmountPaid.Text);

                NewTransaction(transactionTotal, Convert.ToDateTime(txtDateDeposited.Text), "Cheque Issue");
                SaveTransaction(transactionTotal, Session["mimi"].ToString(), Convert.ToDateTime(txtDateDeposited.Text), "Payment Posting - " + txtVoucherNo.Text + "");


                myItem = GridView1.SelectedRow.Cells[7].Text;

                if (myItem == "Loan")
                {
                    double LoanAmount = Convert.ToDouble(GridView1.SelectedRow.Cells[5].Text);
                    double amt = Convert.ToDouble(txtAmountPaid.Text); //'IIf(.ListItems(j).ListSubItems(3) > 0, .ListItems(j).ListSubItems(3), .ListItems(j).ListSubItems(2))
                    double balance = Convert.ToDouble(txtBalance.Text) * (-1); //'amt - CDbl(txtAmountPaid(0))
                    string Loanno = GridView1.SelectedRow.Cells[3].Text;

                    EffectLoanPayment(Loanno, txtVoucherNo.Text, amt, balance, cboBanks.Text, Convert.ToDateTime(txtDateDeposited.Text), Session["mimi"].ToString(), transactionNo, txtmode.Text);
                    string LoanAcc = "";
                    string ContraAcc = "";

                    dr = new WARTECHCONNECTION.cConnect().ReadDB("select loanAcc from loantype lt inner join loans l on l.loancode=lt.loancode where l.loanno='" + Loanno + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            LoanAcc = dr["loanAcc"].ToString();
                            ContraAcc = cboBanks.Text;
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                    //  'If there was charges, pay
                    dr3 = new WARTECHCONNECTION.cConnect().ReadDB("Select  DISTINCT TransDate,Accno,dd.LoanNo,Amount,dd.AuditTime,dd.AuditId,RLOANNO,DESCRIPTION from disbursementdeduction dd  where dd.loanno='" + Loanno + "' and  dd.rloanno='FEE'");
                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            string Craccno = dr3["Accno"].ToString();
                            double Amount = Convert.ToDouble(dr3["Amount"]);
                            string DESCRIPTION = dr3["DESCRIPTION"].ToString();

                            Save_GLTRANSACTION(Convert.ToDateTime(txtDateDeposited.Text), Amount, LoanAcc, Craccno, txtVoucherNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), "Loan Disbursement Deduction"+' '+ DESCRIPTION , 0, 1, txtmode.Text, transactionNo);
                        }
                    }
                    dr3.Close(); dr3.Dispose(); dr3 = null;
                     new WARTECHCONNECTION.cConnect().WriteDB("Update loans set status=4 where loanno='" + Loanno + "'");

                    string Narration = "";
                    dr4 = new WARTECHCONNECTION.cConnect().ReadDB("Select Narration from PendingPayments where Memberno='" + txtMemberNo.Text + "'");
                    if (dr4.HasRows)
                        while (dr4.Read())
                        {
                            Narration = dr4["Narration"].ToString();
                        }
                    dr4.Close(); dr4.Dispose(); dr4 = null;
                    string sqlsql = "set dateformat dmy INSERT INTO PaymentBooking (VoucherNo,Memberno,PayeeDesc,Ccode,Name,Transdate,Amount, Chequeno, Ptype, auditid,datedeposited,draccno,craccno) VALUES ('" + txtVoucherNo.Text + "','" + txtMemberNo.Text + "','" + txtNames.Text + "','','" + Narration + "--" + txtmode.Text + "','" + txtDateDeposited.Text + "'," + amt + ",'" + txtmode.Text + "','" + cboPaymentMode.Text + "','" + Session["mimi"].ToString() + "','" + txtDateDeposited.Text + "','" + LoanAcc + "','" + ContraAcc + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sqlsql);

                    // 'if this was a refinance loan, release the older loan
                    string myloanno = "";
                    myloanno = "";
                    double Amountt = 0;
                    string BankAcc = "";
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select dd.rloanno,dd.amount from disbursementdeduction dd inner join bridgingloan bl on dd.rloanno=bl.brgloanno where dd.loanno='" + Loanno + "' and dd.description='LR' and bl.paid=0");
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            myloanno = dr2["RLOANNO"].ToString();
                            Amountt = Convert.ToDouble(dr2["Amount"].ToString());

                            //  'get the loanacc for the refinancing loan to use as the paying account

                            dr4 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loanacc from loantype lt inner join loans l on l.loancode=lt.loancode where l.loanno='" + Loanno + "'");
                            if (dr4.HasRows)
                                while (dr4.Read())
                                {
                                    BankAcc = dr4["LoanAcc"].ToString();
                                }
                            dr4.Close(); dr4.Dispose(); dr4 = null;

                            bool crge = true;
                            bool penalise = false;
                            SaveRepay(myloanno, Convert.ToDateTime(txtDateDeposited.Text), Amountt, BankAcc, txtVoucherNo.Text, 0, 1, "Loan Refinanced ", Session["mimi"].ToString(), "Refinancing", transactionNo, penalise, crge);
                        }
                        new WARTECHCONNECTION.cConnect().ReadDB("Update bridgingloan set paid=1 where brgloanno in ('" + myloanno + "')");
                    }
                    dr2.Close(); dr2.Dispose(); dr2 = null;
                }
                if (myItem == "Shares")
                {
                    double amt = Convert.ToDouble(txtAmountPaid.Text);
                    double sharebals = Convert.ToDouble(txtBalance.Text) * (-1);
                    string ContraAcc = cboBanks.Text;
                    string Description = GridView1.SelectedRow.Cells[8].Text;
                    SaveContrib(txtMemberNo.Text, Convert.ToDateTime(txtDateDeposited.Text), GridView1.SelectedRow.Cells[3].Text, (amt * -1), cboBanks.Text, txtVoucherNo.Text, txtmode.Text, Session["mimi"].ToString(), Description, transactionNo);

                    string sql = "set dateformat dmy INSERT INTO PaymentBooking (VoucherNo,Memberno,Ccode,Name,Transdate,Amount, Chequeno, Ptype, auditid,datedeposited,draccno,craccno,PayeeDesc,Transactionno) VALUES ('" + txtVoucherNo.Text + "','" + txtMemberNo.Text + "','','" + GridView1.SelectedRow.Cells[1].Text + "--" + Description + "','" + txtDateDeposited.Text + "'," + amt + ",'" + txtmode.Text + "','" + Description + "','" + Session["mimi"].ToString() + "','" + txtDateDeposited.Text + "','" + cboBanks.Text + "','" + ContraAcc + "','" + txtNames.Text + "','" + transactionNo + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sql);

                    new WARTECHCONNECTION.cConnect().WriteDB("Update shareWithdrawal set withdrawn=1 where memberno='" + txtMemberNo.Text + "' and sharescode='" + GridView1.SelectedRow.Cells[3].Text + "'");
                }
                if (myItem == "LoanRefund")
                {
                    LoanAmount = Convert.ToDouble(GridView1.SelectedRow.Cells[3].Text);
                    double amt = Convert.ToDouble(txtAmountPaid.Text); //'IIf(.ListItems(j).ListSubItems(3) > 0, .ListItems(j).ListSubItems(3), .ListItems(j).ListSubItems(2))
                    double balance = Convert.ToDouble(txtBalance.Text) * (-1); //'amt - CDbl(txtAmountPaid(0))
                    string Loanno = GridView1.SelectedRow.Cells[2].Text;
                    string Description = GridView1.SelectedRow.Cells[8].Text;
                    string ContraAcc = cboBanks.Text;
                    bool charge = false;
                    bool penalise = false;
                    SaveRepay(Loanno, Convert.ToDateTime(txtDateDeposited.Text), LoanAmount * (-1), cboBanks.Text, txtVoucherNo.Text, 0, 0, "Loan Refund", Session["mimi"].ToString(), "Refund", transactionNo, charge, penalise);

                    string psql = "set dateformat dmy INSERT INTO PaymentBooking (VoucherNo,Memberno,Ccode,Name,Transdate,Amount, Chequeno, Ptype, auditid,datedeposited,draccno,craccno,PayeeDesc) VALUES ('" + txtVoucherNo.Text + "','" + txtMemberNo.Text + "','','" + GridView1.SelectedRow.Cells[2].Text + "--" + Description + "','" + txtDateDeposited.Text + "'," + amt + ",'" + txtmode.Text + "','" + Description + "','" + Session["mimi"].ToString() + "','" + txtDateDeposited.Text + "','" + cboBanks.Text + "','" + ContraAcc + "','" + txtNames.Text + "','" + transactionNo + "')"; ;
                    new WARTECHCONNECTION.cConnect().WriteDB(psql);
                    new WARTECHCONNECTION.cConnect().WriteDB("Update Refunds set done=1 where refcode='" + Loanno + "' and done=0");

                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Refunds','Loan Refund Code" + Loanno + "','" + txtDateDeposited.Text + "','" + LoanAmount + "','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                }
                //'So, if there was no error, Commit the transaction
                LoadPendingPayments();
                Generate_VoucherNo();
              //  printa();
               // PrintVoucher();
                //string url = "Receipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtAmountPaid.Text + "&&4=" + txtBalance.Text + "&&5=" + Session["mimi"].ToString() + "&&6=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "";
                //string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                WARSOFT.WARMsgBox.Show("Voucher Posted successfully. Print Voucher?");
                //clearTexts();
                
              //  return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void printa()
        {
            DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);

            String today = System.DateTime.Today.ToString("yyyy/MM/dd");
            WARTECHCONNECTION.cConnect cnt522 = new WARTECHCONNECTION.cConnect();
            DR522 = cnt522.ReadDB("SET  dateformat DMY Select MemberNo,Surname,othernames from MEMBERS Where MemberNo='" + txtMemberNo.Text + "'");
            if (DR522.HasRows)
                while (DR522.Read())
                {
                    string Date2 = DateTime.Now.ToString("h:mm:ss tt");

                     format2 = Label22.Text;

                    format2 = "</br> -------------PAYMENT RECEIPT----------------" +
                             "</br> -----FEP SACCO LTD------------------" +
                             "</br> MemberNo :" + txtMemberNo.Text +
                             "</br> Member Names  :" + txtNames.Text +
                             "</br> Amount  :" + txtAmountPaid.Text +
                             "</br> Loan Balance  :" + txtBalance.Text +
                             "</br> -------------------------------------------" +
                             "</br> Received By    :" + User +
                              "</br> --------------------------------------------" +
                              "</br> Received By    :" + User +
                             "</br> --------------------------------------------" +
                             "</br> Date           :" + today + " ; " + Date2 +
                             "</br> ------------FEP SACCO LTD--------------" +
                             "</br>--HOLISTICALLY NURTURING ENTREPRENEURS--";
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += PrintDocumentOnPrintPage;
                    printDocument.Print();
                    //Response.Redirect("~/Activities/printreceipt.aspx?1=" + TextBox1.Text + "&&2=" + Label28.Text + "&&3=" + TextBox2.Text + "&&4=" + Cum20 + "&&5=" + Label30.Text + "&&6=" + Session["mimi"] + "&&7=" + TextBox3.Text + "&&8=" + Date2 + "");
                    string url = "Receipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtAmountPaid.Text + "&&4=" + txtBalance.Text + "&&5=" + Session["mimi"].ToString() + "&&6=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "";
                    string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
          
            //e.Graphics.DrawString(this.format2, this.format2.Font, Brushes.Black, 10, 10);

        }

        private void PrintVoucher()
        {
            Response.Redirect("~/Reports/PrintVoucher.aspx", false);
        }

        private void clearTexts()
        {
            txtAmountPaid.Text = "";
            txtBalance.Text = "";
            txtDistributedAmount.Text = "";
            txtMemberNo.Text = "";
            txtmode.Text = "";
            txtNames.Text = "";
            txtNarration.Text = "";
            txtVoucherNo.Text = "";
            cboBanks.Text = "";
            cboPaymentMode.Text = "";
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
                DateTime dreceived = new DateTime();
                double overpadAmnt = 0;
                string interestAcc = ""; string ContraAcc = ""; string LoanAcc = "";
                // Today = Get_Server_Date;
                string csql = "select max(IsNull(paymentno,0))PaymentNo from repay where loanno='" + Loanno + "' ";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(csql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        PaymentNo = Convert.ToInt32(dr["PaymentNo"]) + 1;
                    }
                else
                {
                    PaymentNo = 1;
                }
                dr.Close(); dr.Dispose(); dr = null;
                dreceived = DateDeposited;
                Calculate_Loan_Repayment(Loanno);
                DateDeposited = dreceived;

                intCharged = interest;
                if (RepayMode == 1)
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
                if (DateDeposited.Day <= mdtei && Amountt > 0)    // ' Means he should not this time be charged interest
                {
                    intCharged = 0;
                    interest = 0;
                }
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
                                    goto loansrepay;
                                    overpadAmnt = Amountt - loanbalance;

                                    //  'remaing money to vover recovery
                                    if (overpadAmnt > 0)
                                    {
                                        // 'GL Transactions
                                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.PremiumAcc,IsNull(lt.penaltyAcc,'')PenaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                                        if (dr1.HasRows)
                                            while (dr1.Read())
                                            {
                                                PremiumAcc = dr1["PremiumAcc"].ToString();
                                                LoanAcc = dr1["LoanAcc"].ToString();
                                                interestAcc = dr1["InterestAcc"].ToString();
                                                penaltyAcc = dr1["PenaltyAcc"].ToString();
                                            }
                                        else
                                        {
                                            WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                                            return;
                                        }
                                        dr1.Close(); dr1.Dispose(); dr1 = null;
                                        if (LoanAcc == "" || interestAcc == "" || (Penalty > 0 && penaltyAcc == ""))
                                        {
                                            WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                                            return;
                                        }

                                        ContraAcc = BankAcc;
                                        Save_GLTRANSACTION(dreceived, overpadAmnt, ContraAcc, PremiumAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
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
                if (Amountt < 0)// 'REVERSAL!
                {
                    intPaid = 0;
                    intOwed = 0;
                    owedPaid = 0;
                    Principal = Amountt;
                    Amountt = 0;
                }
                else if (Amountt == 0)
                {
                    intOwed = intOwed + intCharged;
                    intPaid = 0;
                    owedPaid = 0;
                    interest = 0;
                    Principal = 0;
                }

            loansrepay:
                loanbalance = Math.Round(loanbalance - Principal, 0);

                FirstDate = DateDeposited;

                string sssql = "set dateformat dmy Insert into Repay(Loanno,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo) Values('" + Loanno + "','" + DateDeposited + "'," + PaymentNo + "," + (Principal + interest) + "," + Principal + "," + interest + "," + intCharged + "," + intOwed + "," + Penalty + "," + IntBalalance + "," + loanbalance + ",'" + Receiptno + "','" + transby + "','" + remarks + "','" + auditid + "','" + transactionNo + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sssql);
                string upsql = "set dateformat dmy UPDATE loanbal set balance=" + loanbalance + ",intrOwed=" + intOwed + " ,intBalance=" + IntBalalance + " ,lastdate='" + DateDeposited + "',duedate='" + duedate + "',penalty=" + Penalty + " where loanno='" + Loanno + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(upsql);

                //'GL Transactions

                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.loanAcc,IsNull(lt.penaltyAcc,'')penaltyAcc,lt.ReceivableAcc from loantype lt inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        LoanAcc = dr2["LoanAcc"].ToString();
                        interestAcc = dr2["InterestAcc"].ToString();
                        penaltyAcc = dr2["PenaltyAcc"].ToString();
                        //  'receivableAcc = IIf(IsNull(rst("ReceivableAcc")), "", rst("ReceivableAcc"))
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                    return;
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
                if (LoanAcc == "" || interestAcc == "" || (Penalty > 0) && penaltyAcc == "")
                {
                    WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                }
                ContraAcc = BankAcc;

                if (interest > 0)
                {
                    Save_GLTRANSACTION(dreceived, interest, ContraAcc, interestAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                }
                if (Penalty > 0)
                {
                    Save_GLTRANSACTION(dreceived, Penalty, ContraAcc, penaltyAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                }
                if (Principal > 0)
                {
                    Save_GLTRANSACTION(dreceived, Principal, ContraAcc, LoanAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                }
                // RefreshGuarantors(Loanno) ;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Calculate_Loan_Repayment(string Loanno)
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
                        mMemberno = dr1["memberno"].ToString();
                        rmethod = dr1["RepayMethod"].ToString();
                        rperiod = Convert.ToInt32(dr1["RPeriod"].ToString());
                        rrate = Convert.ToDouble(dr1["interest"].ToString());
                        initialAmount = Convert.ToDouble(dr1["InitialAmount"].ToString());
                        intOwed = Convert.ToDouble(dr1["intrOwed"].ToString());
                        LBalance = Convert.ToDouble(dr1["Balance"].ToString());
                        lastrepay = Convert.ToDateTime(dr1["LastDate"].ToString());
                        Dateissued = Convert.ToDateTime(dr1["dateissued"].ToString());
                        duedate = Convert.ToDateTime(dr1["duedate"].ToString());
                        LoanCode = dr1["LoanCode"].ToString();
                        mdtei = Convert.ToInt32(dr1["mdtei"].ToString());
                        repayrate = Convert.ToDouble(dr1["RepRate"].ToString());
                        intRecovery = dr1["intRecovery"].ToString();
                        //bool DefaultedAmount = Convert.ToBoolean(dr1["DefAmount"].ToString());//].ToString(); = True, 0, dr1["DefAmount"].ToString();].ToString();
                        penaltyOwed = Convert.ToDouble(dr1["PenaltyOwed"].ToString());
                        loanbalance = Convert.ToDouble(LBalance);
                        AutoCal = Convert.ToBoolean(dr1["AutoCalc"].ToString());
                        RepayMode = Convert.ToInt32(dr1["RepayMode"].ToString());
                        wePenalize = Convert.ToBoolean(dr1["Penalty"].ToString());
                        IntBalalance = Convert.ToDouble(dr1["intBalance"].ToString());

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
                            // RepayableInterest = 0;
                            RepayableInterest = totalrepayable - initialAmount;
                            IntBalalance = RepayableInterest - RepaidInterest;
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
                        interest = interest;
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void EffectLoanPayment(string Loanno, string chequeno, double pesaYaCheque, double balance, string ContraAcc, DateTime Dateissued, string StaffNo, string transactionNo, string lcode)
        {
            try
            {
                //duedate = duedate.AddMonths(1);
                double chequeAmt = 0;
                double LoanAmount = 0;
                duedate = Convert.ToDateTime(txtDateDeposited.Text);
                string PPAcc = ""; string names = ""; string mMemberno = ""; int rperiod = 0; double rrate = 0; string interestAcc = ""; string LoanAcc = ""; string rmethod = "";
                string sqldd = "set dateformat dmy select l.applicdate,l.loancode,l.loanamt,l.memberno,l.repayperiod,l.repaymethod,lt.interest,LT.PPACC,lt.interestacc,lt.loanacc,m.surname+' '+othernames as names  from loans l inner join loantype lt on l.loancode=lt.loancode inner join Appraisal ap on l.loanno=ap.loanno inner join members m on l.memberno=m.memberno where l.loanno='" + Loanno + "'";
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB(sqldd);
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        DateTime applicdate = Convert.ToDateTime(dr4["applicdate"]);
                        if (Dateissued < applicdate)
                        {
                            WARSOFT.WARMsgBox.Show("Issue Date should not be greater than  " + Dateissued + " You can use loan Application Date " + applicdate + "  as Issue date.");
                        }
                        LoanAcc = dr4["LoanAcc"].ToString();
                        interestAcc = dr4["interestAcc"].ToString();
                        lcode = dr4["LoanCode"].ToString();
                        mMemberno = dr4["memberno"].ToString();
                        rmethod = dr4["repaymethod"].ToString();
                        rrate = Convert.ToDouble(dr4["interest"].ToString());
                        rperiod = Convert.ToInt32(dr4["repayperiod"].ToString());
                        PPAcc = dr4["PPAcc"].ToString();
                        LoanAmount = Convert.ToDouble(dr4["LoanAmt"].ToString());
                        names = dr4["names"].ToString();
                        duedate2 = Dateissued.AddMonths(rperiod);
                    }
                }
                else
                {
                    dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select LoanAcc,InterestAcc from loantype where loancode='" + lcode + "'");
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            LoanAcc = dr1["LoanAcc"].ToString();
                            interestAcc = dr1["interestAcc"].ToString();
                        }
                    }
                    dr1.Close(); dr1.Dispose(); dr1 = null;
                    LoanAmount = pesaYaCheque;
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
                //duedate = DateAdd("m", 1, Dateissued);;

                getRepayRate(LoanAmount, rrate, rmethod, rperiod);

                //'save to cheques

                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select loanno from cheques where loanno='" + Loanno + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        if (pesaYaCheque > 0)
                        {
                            new WARTECHCONNECTION.cConnect().WriteDB("Update cheques set balance=" + balance + " where loanno='" + Loanno + "'");
                            //'FOR THE PART PAYMENT

                            Save_GLTRANSACTION(Dateissued, pesaYaCheque, PPAcc, ContraAcc, chequeno, mMemberno, Session["mimi"].ToString(), "Loan Disbursement (" + mMemberno + ")", 0, 1, chequeno, transactionNo);
                        }
                    }
                }
                else
                {
                    //'save to cheques
                    string sqldada = "set dateformat dmy Insert into cheques(loanno,chequeno,amount,balance,dateissued,AuditId,TransactionNo)Values('" + Loanno + "','" + chequeno + "'," + LoanAmount + "," + balance + ", '" + Dateissued + "', 'user','" + transactionNo + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sqldada);

                    //'save to loanbal
                    string sqllbal = "set dateformat dmy insert into loanbal(loanno,loancode,balance,intBalance,memberno,auditid,lastdate,interest,repaymethod,repayperiod,duedate,firstDate,autocalc,repayrate,TransactionNo) values('" + Loanno + "','" + lcode + "'," + LoanAmount + "," + RepayableInterest + ",'" + mMemberno + "','User','" + Dateissued + "'," + rrate + ",'" + rmethod + "','" + rperiod + "','" + duedate2 + "','" + duedate + "',1," + rrate + ",'" + transactionNo + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sqllbal);

                    // 'Change the status of this loan as Disbursed
                    if (balance <= 0)
                    {
                        string sqlupp = "update loans set status=4 where loanno='" + Loanno + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(sqlupp);
                    }
                    // 'FOR THE PART PAYMENT
                    Save_GLTRANSACTION(Dateissued, pesaYaCheque, LoanAcc, ContraAcc, chequeno, mMemberno, Session["mimi"].ToString(), "Loan Disbursement (" + names + ")", 1, 1, chequeno, transactionNo);
                }
                
                if (balance > 0)
                {
                    //Save_GLTRANSACTION(Dateissued, balance, LoanAcc, PPAcc, chequeno, mMemberno, "User", "Member Payment", 0, 1, chequeno, transactionNo);
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void getRepayRate(double LoanAmount, double rrate, string rmethod, int rperiod)
        {
            try
            {
                DateTime DateDeposited = Convert.ToDateTime(txtDateDeposited.Text);
                //DateTime lastrepay = Convert.ToDateTime(lastrepaydate);
                //int datediff = ((DateDeposited.Year - lastrepay.Year) * 12) + DateDeposited.Month - lastrepay.Month;

                double pesaYote, totalrepayable = 0;
                double interest_B = 0;
                double interest_A = 0;
                string valret = "";
                double Amount = 0;
                //int m = datediff;

                if (rmethod == "AMRT")
                {
                    totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                    double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                    //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                    double interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                    interest = Math.Ceiling(interest);
                    Principal = Math.Round((mrepayment - interest), 2);
                    Amount = Principal + interest + IntrOwed;
                    //RepayableInterest = 0;
                    RepayableInterest = totalrepayable - initialAmount;
                   
                }
                if (rmethod == "STL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                    Principal = initialAmount / rperiod;
                    interest = (initialAmount * (rrate / 12 / 100));
                    Amount = Principal + interest;
                    RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                }
                if (rmethod == "RBAL")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                    Principal = initialAmount / rperiod;
                    interest = (rrate / 12 / 100) * LBalance;
                    Amount = Principal + interest;
                    // 'intrcharged = LoanAmount * (rRate / 200 * (RPeriod + 1)) / RPeriod
                    RepayableInterest = 0;// 'unpredictable
                }
                if (rmethod == "RSPECIAL")
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
                    LBalance = loanbalance;//'to continue with the previous flow
                }
                if (rmethod == "RSTL")
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
                }
                if (rmethod == "ADV")
                {
                    totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Principal = initialAmount / rperiod;
                    interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                    RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                    Amount = Principal + interest;
                }
                if (rmethod == "RSPECIAL")
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
                }
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
                string sql = "select isnull(sum(contrib.amount),0)  from contrib where contrib.memberno='" + memberno + "' and contrib.sharescode='" + SHARECode + "' group by contrib.memberno";
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        TotalShares = Convert.ToDouble(dr2[""].ToString());
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
                if (cboPaymentMode.Text == "Cash")
                {
                    BankAcc = shareAcc;
                }
                else
                {
                    string TempAcc = "";
                    if (Convert.ToDouble(contrAmount) < 0)
                    {
                        TempAcc = BankAcc;
                        BankAcc = shareAcc;
                        shareAcc = TempAcc;
                        contrAmount = -1 * (contrAmount);
                    }
                    Save_GLTRANSACTION(ContrDate, contrAmount, BankAcc, shareAcc, Receiptno, memberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
                    shareAcc = BankAcc;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRaccno, string Craccno, string DocumentNo, string Source, string auditid, string TransDescription, int CashBook, int doc_posted, string chequeno, string transactionNo)
        {
            // Save_GLTRANSACTION(dreceived, interest, ContraAcc, interestAcc, Receiptno, mMemberno, User, remarks, 1, 1, Receiptno, transactionNo);
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + Source + "','" + auditid + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','Bosa'");
        }

        private void SaveTransaction(double transactionTotal, string user, DateTime TransDate, string Description)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                transactionNo = Convert.ToString(TimeNow);
                transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();

                string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription)Values('" + transactionNo + "'," + transactionTotal + ",'" + user + "',Convert(Varchar(10), '" + TransDate + "', 101),'" + Description + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                double transactionTotal = AmountPaid;

                SaveTransaction(transactionNo, transactionTotal, Session["mimi"].ToString(), TransDate, Description);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveTransaction(string transactionNo, double transactionTotal, string p, DateTime TransDate, string Description)
        {
            
        }
        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void txtMemberNo_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                if (cboPaymentMode.Text == "Cheque")
                {
                    txtmode.Visible = true;
                    Label15.Visible = true;
                    Label15.Text = "Cheque No:";
                }
                else if(cboPaymentMode.Text == "Mpesa")
                {
                    txtmode.Visible = true;
                    Label15.Visible = true;
                    Label15.Text = "Mpesa No:";
                }
                else
                {
                    txtmode.Enabled = false;
                    Label15.Enabled = false;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void imgSearchBankAC_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgSearchCompany_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void txtReceiptAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Balance = 0;
                double balAmount = Convert.ToDouble(txtBalance.Text);
                double Amountpaid = Convert.ToDouble(txtAmountPaid.Text);

                if (Amountpaid != Convert.ToDouble(txtDistributedAmount.Text) && chkIspartpayment.Checked == true)
                {
                    Balance = Amountpaid + Convert.ToDouble(txtBalance.Text);

                    txtBalance.Text = Balance.ToString();
                }
                if (Amountpaid > Convert.ToDouble(txtDistributedAmount.Text))
                {
                    WARSOFT.WARMsgBox.Show("The Amount paid should not be more than the distributed amount");
                    txtAmountPaid.Text = "0.00";
                    txtAmountPaid.Focus();
                    return;
                }
                if (Amountpaid != Convert.ToDouble(txtDistributedAmount.Text) && chkIspartpayment.Checked != true)
                {
                    WARSOFT.WARMsgBox.Show("The receipt amount should be the same as the distributed amount or confirm if it is a part payment");
                    //retbalance();
                    return;
                }
                else
                {
                    Balance = Amountpaid + balAmount;
                    txtBalance.Text = Balance.ToString();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

        }

        protected void cboBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                string sql = "select bankname from banks where accno='" + cboBanks.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblbankname.Text = dr["bankname"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                txtMemberNo.Text = GridView1.SelectedRow.Cells[1].Text;
                txtNames.Text = GridView1.SelectedRow.Cells[2].Text;

                txtDistributedAmount.Text = GridView1.SelectedRow.Cells[6].Text;
                txtBalance.Text = (Convert.ToDouble(GridView1.SelectedRow.Cells[6].Text) * -1).ToString();

                NewVoucher();
                Generate_VoucherNo();
                //txtDateDeposited.Text = DateTime.Today.ToString("dd-MM-yyyy");
                string purpose = GridView1.SelectedRow.Cells[7].Text;
                if (purpose == "Loan")
                {
                    chkLoans.Checked = true;
                }
                else if (purpose == "Shares")
                {
                    chkShares.Checked = true;
                }
                else
                {
                    chkShares.Checked = false;
                    chkLoans.Checked = false;
                }
                loadgridloan();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void loadgridloan()
        {
            try
            {

                da = new WARTECHCONNECTION.cConnect().ReadDB2("Select MemberNo,MemberNames,LoanShareCode,DateApproved,AmountPayable,Balance,Purpose,Narration from PendingPayments where LoanShareCode='"+GridView1.SelectedRow.Cells[3].Text+"'");
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

        private void NewVoucher()
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("exec getNextNumber 'Paymentbooking','Voucherno'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int vouch = Convert.ToInt32(dr[0]);
                    txtVoucherNo.Text = "V/NO-" + (vouch + 1);
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        protected void imgDateDeposited_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
            //throw new NotImplementedException();
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
    }
}