using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Globalization;


namespace USACBOSA.FinanceAdmin
{
    public partial class PaymentOptions : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr3,dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        double myNetBalance = 0;
        double myNetDividents = 0;
        double myNewTotal = 0;
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
                LoadBanks();
                LoadGL();
                LoadShares();
                loans();
                Loaddividends();
                TextBox6.Text ="0";
                TextBox7.Text = "0";
                TextBox8.Text = "0";
                Label28.Visible=false;
            }
        }
        private void LoadGL()
        {
            cboGlAccountNo.Items.Add("");
            WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
            string GL = "select Accno from banks";
            dr = GLS.ReadDB(GL);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboGlAccountNo.Items.Add("" + dr["Accno"].ToString() + "");
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
        private void loans()
        {
            DropDownList2.Items.Add("");
            WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
            string GL = "Select lb.loanno,lb.Balance+lb.intrOwed as Balance from loanbal lb  where memberno='" + txtMemberNo.Text + "' and balance>1";
            dr = GLS.ReadDB(GL);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DropDownList2.Items.Add("" + dr["Accno"].ToString() + "");
                }
            }
            dr.Close(); dr.Dispose(); dr = null; GLS.Dispose(); GLS = null;
        }
      
        private void Loaddividends()
        {
            string readdata = "select Memberno,names,Current_Tot_Shares,Shares_as_at,Gross_Dividend,net_dividend,divwithtax,paid,sharecapital from tmpdividendpaylist where paid=0";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select lb.Balance+lb.introwed as balance,lt.loanAcc from loanbal lb inner join loantype lt on lb.loancode=lt.loancode where loanno='" & cboLoanno.Text & "'
            try
            {
                string sql = "select lb.Balance+lb.introwed as balance,lt.loanAcc from loanbal lb inner join loantype lt on lb.loancode=lt.loancode where loanno='" + DropDownList2.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        TextBox5.Text = dr["balance"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadShares()
        {
            cboPaymentMode.Items.Add("");
            string sql = "Select sharesCode from sharetype order by sharescode asc";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    cboPaymentMode.Items.Add("" + dr["sharesCode"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }
        protected void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select s.Totalshares,st.sharesacc from shares s inner join sharetype st on s.sharescode=st.sharescode where s.memberno='" & txtMemberno & "' and s.sharescode='" & cboSharesCode & "'")
            try
            {
                string sql = "Select s.Totalshares,st.sharesacc from shares s inner join sharetype st on s.sharescode=st.sharescode where s.memberno='" + txtMemberNo.Text + "' and s.sharescode='" + cboPaymentMode.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        TextBox4.Text = dr["Totalshares"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void txtMemberNo_DataBinding(object sender, EventArgs e)
        {
            try
            {
                string readdata = "Select m.memberno,m.surname +' '+m.othernames as names, max(isnull(sv.newContr,0)) as Expected,sv.SharesCode,st.sharestype as vote From SHRVAR sv inner join sharetype st on sv.sharescode=st.sharescode inner join members m on sv.memberno=m.memberno Where sv.MemberNo='" + txtMemberNo.Text.Trim() + "' and sv.subscribed=1 GROUP BY sv.SharesCode, st.SharesType, m.MemberNo, m.Surname + ' ' + m.OtherNames";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Label22.Visible = true;
            //DropDownList1.Visible = true;
            //TextBox1.Visible = true;
            Button1.Visible = true;
        }

        protected void btnFindBank_Click(object sender, EventArgs e)
        {

        }
         private void SaveTransaction()
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," +TextBox2.Text + ",'" + Session["mimi"].ToString() + "','" + TextBox10.Text + "','" + "Divident Payment for-" + txtMemberNo.Text + "" + "','Posted')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; };
        }
        
        
        private void Save_GLTRANSACTIONS(DateTime DateDeposited, double ReceiptAmount, string BankAC, string SharesAcc,  string receiptno,string MemberNo,string User, string MemberReceipt, string p_8, string p_9, string ChequeNo, string transactionNo, string bosa)
        {
            try
            {
                string saccoinsert = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "'," + ReceiptAmount + ",'" + BankAC + "','" + SharesAcc + "','"+receiptno+"','" + MemberNo + "','" + Session["mimi"].ToString() + "','" + MemberReceipt + "'," + p_8 + "," + p_9 + ",'" + ChequeNo + "','" + transactionNo + "','" + bosa + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(saccoinsert);

                //string updreceipt = "Set DateFormat DMY update ReceiptBooking set Draccno='" + BankAC + "',Craccno='" + SharesAcc + "' where";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                double TotalShares = 0;
                string myshares = "";                
                if (myNetDividents < myNewTotal)
                {
                    WARSOFT.WARMsgBox.Show("This Amount is greater than the Member's declared Benefit, Consider Revising");
                    return;
                }
                if (cboBankAC.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Select the Source Account for this transaction-Dividend A/c");
                    return;
                }
                if (Label28.Text == "PAID")
                {
                    WARSOFT.WARMsgBox.Show("This Payment is already done!");
                    return;
                }
                else
                {

                    SaveTransaction();

                    if (Convert.ToDouble(TextBox8.Text) > 0)
                    {
                        //Save_GLTRANSACTIONS(TextBox9.Text, CashAmount, cboBankAC.Text, SharesAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), "Member Receipt" + '-' + TransDescription, "1", "1", txtChequeNo.Text, transactionNo, "bosa");
                        Save_GLTRANSACTIONS(Convert.ToDateTime(TextBox10.Text),Convert.ToDouble(TextBox8.Text), cboBankAC.Text, cboGlAccountNo.Text,"Cash Dividend Payment", txtMemberNo.Text, Session["mimi"].ToString(), "Member Cash" + '-' +"Dividend Payment", "1", "1", "1", transactionNo,"bosa");
                        //Save_GLTRANSACTIONS(Convert.ToDateTime(TextBox10.Text), Convert.ToDouble(TextBox8.Text), cboBankAC.Text, cboGlAccountNo.Text, 1, txtMemberNo.Text, Session["mimi"].ToString(), "Member Receipt", "Dividend Payment", 1, 1, TextBox11.Text, transactionNo);
                        string dividendsss = "set dateformat dmy Update tmpDividendpaylist set paid=1 where memberno='" +txtMemberNo.Text+ "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(dividendsss);
                        WARSOFT.WARMsgBox.Show("Dividends saved successfully");
                    }
                    if (Convert.ToDouble(TextBox7.Text) > 0)
                    {
                        string lbal2 = " select isnull(sum(contrib.amount),0) as TotalShares  from contrib where memberno='" + txtMemberNo.Text + "' and sharescode='" +cboPaymentMode.Text+ "'";
                        WARTECHCONNECTION.cConnect lbalance1 = new WARTECHCONNECTION.cConnect();
                        dr6 = lbalance1.ReadDB(lbal2);
                        if (dr6.HasRows)
                            while (dr6.Read())
                            {

                                myshares = dr6["TotalShares"].ToString();
                                if (myshares == "")
                                {
                                    myshares = "0";
                                }

                                TotalShares = Convert.ToDouble(myshares);
                                TotalShares = TotalShares + Convert.ToDouble(TextBox7.Text);
                                string dividendss = "set dateformat dmy Insert into Contrib(memberno,contrdate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)values('" + txtMemberNo.Text + "','" + TextBox10.Text + "','0'," + TextBox7.Text + "," + TotalShares + ",'Dividend to shares','Non-Cash','Share Dividend payment','Dividend Capitalization for-" + txtMemberNo.Text + "','" + Session["mimi"].ToString() + "','"+cboPaymentMode.Text+"','" + transactionNo + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(dividendss);
                                string dividendsss = "set dateformat dmy Update tmpDividendpaylist set paid=1 where memberno='" +txtMemberNo.Text+ "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(dividendsss);
                                WARSOFT.WARMsgBox.Show("Dividends saved successfully");
                            }


                        dr6.Close(); dr6.Dispose(); dr6 = null; lbalance1.Dispose(); lbalance1 = null;
                    }

                    if (Convert.ToDouble(TextBox6.Text) > 0)
                    {
                      bool penalise = false; bool charge = false;
                      SaveRepay(Loanno, Convert.ToDateTime(TextBox10.Text),Convert.ToDouble(TextBox6.Text), shareAcc, "Dividend to Loan", 0, 1, "Dividend Interest - " + Loanno, Session["mimi"].ToString(), Session["mimi"].ToString(), transactionNo, charge, penalise);
                      string dividendsss = "set dateformat dmy Update tmpDividendpaylist set paid=1 where memberno='" + txtMemberNo.Text + "'";
                      new WARTECHCONNECTION.cConnect().WriteDB(dividendsss);
                      WARSOFT.WARMsgBox.Show("Dividends saved successfully");
                    }
                    
                }
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox8.Text = "0";
                TextBox7.Text = "0";
                TextBox6.Text = "0";

            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        
        protected void cboGlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select s.Totalshares,st.sharesacc from shares s inner join sharetype st on s.sharescode=st.sharescode where s.memberno='" & txtMemberno & "' and s.sharescode='" & cboSharesCode & "'")
            try
            {
                string sql = "select bankname from banks where accno='" + cboBankAC.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        TextBox9.Text = dr["bankname"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
            txtNames.Text = GridView2.SelectedRow.Cells[2].Text;
            TextBox2.Text = GridView2.SelectedRow.Cells[6].Text;
            //Label30.Text = GridView2.SelectedRow.Cells[8].Text;
            Label28.Visible = true;
            if (GridView2.SelectedRow.Cells[7].Text == "1")
            {
                this.Label28.Text = "PAID";
                this.Label28.BackColor = Color.Red;
            }
            else
            {
                this.Label28.Text = "UNPAID";
                this.Label28.BackColor = Color.Red;
            }
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                CheckBox3.Checked = false;
                CheckBox4.Checked = false;                
            }
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true)
            {
                CheckBox2.Checked = false;
                CheckBox4.Checked = false;
            }
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox4.Checked == true)
            {
                CheckBox3.Checked = false;
                CheckBox2.Checked = false;
            }
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            compute();
            if (myNewTotal > myNetBalance)
            {
                WARSOFT.WARMsgBox.Show("You have not enough Divident balance, Please Revise");
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
           myNetDividents =Convert.ToDouble(TextBox2.Text);
           myNetBalance = myNetDividents;
        }
        private void compute()
        {
            myNewTotal = Convert.ToDouble(TextBox6.Text) + Convert.ToDouble(TextBox6.Text) +Convert.ToDouble(TextBox6.Text);
        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            compute();
            if (myNewTotal > myNetBalance)
            {
                WARSOFT.WARMsgBox.Show("You have not enough Divident balance, Please Revise");
            }

        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            compute();
            if (myNewTotal > myNetBalance)
            {
                WARSOFT.WARMsgBox.Show("You have not enough Divident balance, Please Revise");
            }
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
                DateDeposited = Convert.ToDateTime(TextBox10.Text, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
                DateTime dreceived = Convert.ToDateTime(TextBox10.Text, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
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
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + Source + "','" + auditid + "','"+TransDescription+"'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','Bosa'");
        }
        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
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
                            Principal = Math.Round((mrepayment- interest), 2);
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
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; 
            }
        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            MemberNoshow();
        }

        private void MemberNoshow()
        {
            try
            {
                bool NewMember = false;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select m.surname,m.othernames,m.HomeAddr,m.companycode,m.entrance,m.companycode,T.net_dividend  from members m inner join TMPDIVIDENDPAYLIST T on m.memberno=T.memberno   where m.memberno  ='" + txtMemberNo.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        bool IsGroup = false;
                        txtNames.Text = dr["surname"].ToString() + ' ' + dr["othernames"].ToString();
                        TextBox2.Text = dr["net_dividend"].ToString();
                        //string entrance1 = dr["net_dividend"].ToString();
                       
                    }
                }
                else
                {
                    txtNames.Text = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
                //LoadMemberDetails(NewMember);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Label28.Text == "PAID")
            {
                WARSOFT.WARMsgBox.Show("This Payment is already done!");
                return;
            }
            else
            {
                DropDownList2.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select lb.loanno,lb.Balance+lb.intrOwed as Balance from loanbal lb  where memberno='" + txtMemberNo.Text + "' and balance>1");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList2.Items.Add("" + dr["loanno"].ToString() + "");
                    }
                }
            }
        }
    }
}