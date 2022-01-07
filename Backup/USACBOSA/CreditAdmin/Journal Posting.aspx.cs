﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.CreditAdmin
{
    public partial class Journal_Posting : System.Web.UI.Page
    {

        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
        String transactionno = System.DateTime.Now.ToString("hh:mm:ss:ampm");
        String Type;
        private static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
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
        string LoanOverpaymentAcc = "";
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
                cboAccno.Items.Clear();
                cboAccno.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select accno from glsetup order by accno asc");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboAccno.Items.Add(dr["accno"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                //'load shareCodes
                cboShareType.Items.Clear();
                cboShareType.Items.Add("");
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select sharescode from sharetype");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        cboShareType.Items.Add(dr1["sharescode"].ToString());
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                // totalamount = 0;
                // pushed = 0;
                getJVnumber();

                dtpReceiptDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                loadUnpostedjournalDetails();
            }
        }

        private void loadUnpostedjournalDetails()
        {

        }
        private void getJVnumber()
        {
            try
            {
                int nextno = 0; int nextno1 = 0;
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                string myddd = "select isnull(MAX(RIGHT(VNO,3)),0)+1 ccount from journalsListing where VNO like '%JV%' and year(TransDate)='" + (System.DateTime.Today).Year + "' and month(TransDate)='" + (System.DateTime.Today).Month + "' and day(TransDate)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        nextno = maxno;
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
                WARTECHCONNECTION.cConnect oSaccoMaster11 = new WARTECHCONNECTION.cConnect();
                string myddd11 = "select isnull(MAX(RIGHT(VNO,3)),0)+1 ccount from journals where VNO like '%JV%' and year(TransDate)='" + (System.DateTime.Today).Year + "' and month(TransDate)='" + (System.DateTime.Today).Month + "' and day(TransDate)= '" + (System.DateTime.Today).Day + "'";
                dr1 = oSaccoMaster11.ReadDB(myddd11);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        int maxno = Convert.ToInt32(dr1[0].ToString());
                        nextno1 = maxno;
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; oSaccoMaster11.Dispose(); oSaccoMaster11 = null;
                if (nextno > nextno1)
                {
                    txtJournaNo.Text = "JV" + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno).ToString()).PadLeft(3, '0');
                }
                else if (nextno1 > nextno)
                {
                    txtJournaNo.Text = "JV" + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno1).ToString()).PadLeft(3, '0');
                }
                else if (nextno1 == nextno)
                {
                    txtJournaNo.Text = "JV" + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno1).ToString()).PadLeft(3, '0');
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                double ncr = 0;
                double ndr = 0;
                double ncr1 = 0;
                double ndr1 = 0;
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow myrow = GridView1.Rows[i];
                    CheckBox myAtmSelector = (CheckBox)myrow.FindControl("AtmSelector");
                    if (myAtmSelector.Checked == true)
                    {
                        ncr = Convert.ToDouble(myrow.Cells[5].Text.Trim());
                        ndr = Convert.ToDouble(myrow.Cells[4].Text.Trim());
                        ndr1 = ndr1 + ndr;
                        ncr1 = ncr1 + ncr;

                    }
                }
                txtTotalDr.Text = ndr1.ToString();
                txtTotalCr.Text = ncr1.ToString();
                if (Convert.ToDouble(txtTotalDr.Text) != Convert.ToDouble(txtTotalCr.Text))
                {
                    WARSOFT.WARMsgBox.Show("The journal is not balancing, please rectify");
                }
                else if (rtpNarration.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Naration is Required");
                    return;
                }
                else
                {
                    WARTECHCONNECTION.cConnect nOono = new WARTECHCONNECTION.cConnect();
                    String MDTEI = ("select vno from journals where vno='" + txtJournaNo.Text.Trim() + "'");
                    dr = nOono.ReadDB(MDTEI);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            WARSOFT.WARMsgBox.Show("The Voucherno is already Processed, maybe awaiting Posting");
                            return;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                        {
                            GridViewRow row = GridView1.Rows[i];
                            CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                            if (AtmSelector.Checked == true)
                            {
                                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select JLID,VNO,ACCNO,NAME,NARATION,MEMBERNO,SHARETYPE,Loanno,AMOUNT,TRANSTYPE,AUDITID,TRANSDATE,AUDITDATE,POSTED,POSTEDDATE,Transactionno from journalsListing where VNO='" + row.Cells[1].Text.Trim() + "'");
                                if (dr1.HasRows)
                                {
                                    while (dr1.Read())
                                    {
                                        string JLID = dr1["JLID"].ToString();
                                        string VNO = dr1["VNO"].ToString();
                                        string ACCNO = dr1["ACCNO"].ToString();
                                        string NAME = dr1["NAME"].ToString();
                                        string NARATION = dr1["NARATION"].ToString();
                                        string MEMBERNO = dr1["MEMBERNO"].ToString();
                                        string SHARETYPE = dr1["SHARETYPE"].ToString();
                                        string Loanno = dr1["Loanno"].ToString();
                                        string AMOUNT = dr1["AMOUNT"].ToString();
                                        string TRANSTYPE = dr1["TRANSTYPE"].ToString();
                                        string TRANSDATE = dr1["TRANSDATE"].ToString();
                                        string Transactionno = dr1["Transactionno"].ToString();

                                        String sql = "set dateformat dmy insert into Journals(accno,name,Naration,memberno,vno,Amount,Transtype,TRANSDATE,AuditId,Posted,Loanno,sharetype,transactionno) Values('" + ACCNO + "','" + NAME + "','" + NARATION + "','" + MEMBERNO + "', '" + VNO + "'," + AMOUNT + ",'" + TRANSTYPE + "','" + System.DateTime.Today + "','" + Session["mimi"].ToString() + "',0,'" + Loanno + "','" + SHARETYPE + "','" + Transactionno + "')";
                                        new WARTECHCONNECTION.cConnect().WriteDB(sql);
                                        new WARTECHCONNECTION.cConnect().WriteDB("delete from JournalsListing where JLID='" + JLID + "'");
                                    }
                                }
                                dr1.Close(); dr1.Dispose(); dr1 = null;
                            }
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
                WARSOFT.WARMsgBox.Show("The journal has been processed sucessfully");
                Loadunpostedjournals();
                cmdProcessJournal.Enabled = false;
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string accno = "";
                accno = cboAccno.Text;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select GLACCNAME,TYPE,SUBTYPE from glsetup where accno='" + accno + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtAccNames.Text = dr["GLACCNAME"].ToString();
                        string subType = dr["SubType"].ToString();
                        if (subType == "MEMBER")
                        {
                            //txtMemberno.Enabled = false;
                            //cmdFind.Enabled = true;
                            cboLoanno.Enabled = false;
                            cboAccno.Enabled = false;
                            lblLoantype.Enabled = false;
                            lblShareType.Enabled = false;
                            //  cmdSearchLoan.Enabled = true;
                            //cboAccno_KeyPress 13;
                        }
                        else
                        {
                            //txtMemberno.Enabled = true;
                            //txtMemberno.Text = "";
                            cboShareType.Text = "";
                            cboLoanno.Items.Clear();
                            cboLoanno.Text = "";
                            cboLoanno.Enabled = true;
                            //cmdFind.Enabled = false;
                            //cmdSearchLoan.Enabled = false;
                        }
                    }
                }
                else
                {
                    txtAccNames.Text = "";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cmdPostJournal_Click(object sender, EventArgs e)
        {
            try
            {
                bool debitJournal = false; bool creditJournal = false;
                string NormalBal = ""; string Effect = ""; string Source = "";
                double jvSubAmount = 0;
                int Dr = 0; int Cr = 0;
                string DRAcc = "";
                string CRAcc = "";
                string Loanno = "";
                string sharesCode = "";

                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {

                        txtJournaNo.Text = row.Cells[1].Text.Trim();
                        rtpNarration.Text = row.Cells[3].Text.Trim();
                        txtTotalCr.Text = row.Cells[4].Text.Trim();
                        txtTotalDr.Text = row.Cells[4].Text.Trim();
                        dr = new WARTECHCONNECTION.cConnect().ReadDB("select vno,TRANSTYPE,accno,amount from journals where vno='" + txtJournaNo.Text + "'");
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                jvSubAmount = Convert.ToDouble(dr["amount"]);//.ToString().Trim();
                                string jdd = dr["TRANSTYPE"].ToString().Trim();
                                if (jdd == "CR")
                                {
                                    creditJournal = true;
                                    CRAcc = dr["accno"].ToString().Trim();
                                }
                                else if (jdd == "DR")
                                {
                                    debitJournal = true;
                                    DRAcc = dr["accno"].ToString().Trim();
                                }
                            }
                        }
                        else
                        {
                            WARSOFT.WARMsgBox.Show("The Above Journal has not been processed");
                            return;
                        }
                        dr.Close(); dr.Dispose(); dr = null;
                        if (Convert.ToDouble(txtTotalDr.Text) != Convert.ToDouble(txtTotalCr.Text))
                        {
                            WARSOFT.WARMsgBox.Show("The journal is not balancing, please rectify");
                            return;
                        }
                        string memberno = txtMemberNo.Text;
                        double transactionTotal = Convert.ToDouble(txtTotalCr.Text);
                        NewTransaction(transactionTotal, Convert.ToDateTime(dtpReceiptDate.Text), "Journal Posting");
                        DateTime TimeNow = DateTime.Now;
                        string transactionNo = Convert.ToString(TimeNow);
                        transactionNo = txtJournaNo.Text + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                        bool penalise = false; bool charge = false;
                        //SaveRepay(Loanno, Convert.ToDateTime(dtpReceiptDate.Text), txtTotalDr.Text, LoanAcc, "Overpayment", 0, 1, "Overpayment Journal posting by - " + Loanno, Session["mimi"].ToString(), Session["mimi"].ToString(), transactionNo, charge, penalise);
                        SaveRepay(Loanno, Convert.ToDateTime(dtpReceiptDate.Text), Convert.ToDouble(txtTotalDr.Text), LoanAcc, "Overpayment", 0, 1, "Journal posting of Overpayment of " + memberno, Session["mimi"].ToString(), "Jounal Posting", transactionNo, penalise, charge);
                        double loanbal = 0;
                        SaveContrib(txtMemberNo.Text, dtpReceiptDate.Text, dtpReceiptDate.Text, cboShareType.Text, Convert.ToDouble(txtTotalCr.Text), cboAccno.Text, txtJournaNo.Text,"1", Session["mimi"].ToString(),rtpNarration.Text, transactionNo);
                        dr5 = new WARTECHCONNECTION.cConnect().ReadDB("select balance from loanbal where loanno='" + cboLoanno.Text + "'");
                        if (dr5.HasRows)
                            while (dr5.Read())
                            {
                                loanbal = Convert.ToDouble(dr5["balance"]);
                            }
                        dr5.Dispose(); dr5.Close(); dr5 = null;

                        Save_GLTRANSACTION(Convert.ToDateTime(dtpReceiptDate.Text), jvSubAmount, DRAcc, CRAcc, txtJournaNo.Text, memberno, Session["mimi"].ToString().Trim(), rtpNarration.Text, 0, 1, txtJournaNo.Text, transactionNo);

                        new WARTECHCONNECTION.cConnect().WriteDB("update journals set posted=1 where vno='" + txtJournaNo.Text + "'");
                        WARSOFT.WARMsgBox.Show("Journal Posted Successfully");
                        getJVnumber();

                        txtTotalCr.Text = "0";
                        txtTotalDr.Text = "0";
                        Clear();

                    }

                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Save_GLTRANSACTION(DateTime DateDeposited, double jvSubAmount, string DRAcc, string CRAcc, string JournaNo, string memberno, string auditid, string narration, int p_4, int p_5, string p_6, string transactionNo)
        {
            string saccoinsert = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "'," + jvSubAmount + ",'" + DRAcc + "','" + CRAcc + "','" + JournaNo + "','" + memberno + "','" + Session["mimi"].ToString() + "','" + narration + "'," + p_4 + "," + p_5 + ",'" + JournaNo + "','" + transactionNo + "','bosa'";
            new WARTECHCONNECTION.cConnect().WriteDB(saccoinsert);
        }
        private void Clear()
        {
            txtJournaNo.Text = "";
            cboAccno.Text = "";
            txtAccNames.Text = "";
            txtCr.Text = "";
            txtDr.Text = "";
            txtTotalCr.Text = "";
            txtTotalDr.Text = "";
            txtMemberNo.Text = "";
            rtpNarration.Text = "";
            cboShareType.Text = "";
            lblfullnames.Text = "";
            lblLoantype.Text = "";
            lblShareType.Text = "";
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
                DateDeposited = Convert.ToDateTime(dtpReceiptDate.Text);
                DateTime dreceived = Convert.ToDateTime(dtpReceiptDate.Text);
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
                //CalculateLoanRepayment(Loanno);
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
                Principal = Convert.ToDouble(txtTotalDr.Text);
                loanbalance = 0.00;
                interest = 0.00;
                intCharged = 0.00;
                intOwed = 0.00;
                IntBalalance = 0.00;
                Penalty = 0.00;
                double loanbal = 0;
                // 'GL Transactions
                dr5 = new WARTECHCONNECTION.cConnect().ReadDB("select balance from loanbal where memberno='" + txtMemberNo.Text + "' and balance<0");
                if (dr5.HasRows)
                    while (dr5.Read())
                    {
                        loanbal = Convert.ToDouble(dr5["balance"]);
                    }
                dr5.Dispose(); dr5.Close(); dr5 = null;
                if (loanbal < 0.0)
                {
                    dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loanacc,lt.interestAcc,lt.PremiumAcc,lt.LoanOverpaymentAcc,lt.OverpaymentAcc,IsNull(lt.penaltyAcc,'')PenaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.memberno='" + txtMemberNo.Text + "'");
                    if (dr1.HasRows)
                        while (dr1.Read())
                        {
                            PremiumAcc = dr1["PremiumAcc"].ToString();
                            LoanAcc = dr1["LoanAcc"].ToString();
                            interestAcc = dr1["InterestAcc"].ToString();
                            penaltyAcc = dr1["PenaltyAcc"].ToString();
                            OverpaymentAcc = dr1["OverpaymentAcc"].ToString();
                            LoanOverpaymentAcc = dr1["LoanOverpaymentAcc"].ToString();
                        }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                        return;
                    }
                    dr1.Close(); dr1.Dispose(); dr1 = null;
                    if (LoanAcc == "" || OverpaymentAcc == "" || LoanOverpaymentAcc=="" || interestAcc == "" || (Penalty > 0 && penaltyAcc == ""))
                    {
                        WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                    }
                    ContraAcc = BankAcc;
                    //Save_GLTRANSACTION(dreceived, overpadAmnt, ContraAcc, OverpaymentAcc, Receiptno, mMemberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);
                    goto loansrepay;

                loansrepay:
                    //loanbalance = Math.Round(loanbalance - Principal, 0);

                    string sssql = "set dateformat dmy Insert into Repay(Loanno,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo) Values('" + cboLoanno.Text + "','" + DateDeposited + "'," + PaymentNo + "," + txtTotalDr.Text + "," + Principal + "," + interest + "," + intCharged + "," + intOwed + "," + Penalty + "," + IntBalalance + "," + loanbalance + ",'" + Receiptno + "','" + transby + "','" + remarks + "','" + auditid + "','" + transactionNo + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sssql);
                    string upsql = "set dateformat dmy UPDATE loanbal set balance=" + loanbalance + ",intrOwed=" + intOwed + " ,intBalance=" + IntBalalance + " ,lastdate='" + DateDeposited + "',duedate='" + duedate + "',penalty=" + Penalty + " where loanno='" + cboLoanno.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(upsql);

                    //'GL Transactions

                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.loanAcc,IsNull(lt.penaltyAcc,'')penaltyAcc,lt.ReceivableAcc from loantype lt inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + cboLoanno.Text + "'");
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
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        

        private void NewTransaction(double transactionTotal, DateTime dateTime, string p)
        {
            DateTime TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," + txtTotalDr.Text + ",'" + Session["mimi"].ToString() + "','" + dtpReceiptDate.Text + "','" + rtpNarration.Text + "','Posted')";
            new WARTECHCONNECTION.cConnect().WriteDB(sql);

        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select st.sharestype from sharetype ST where ST.sharescode='" + cboShareType.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblShareType.Text = dr["sharestype"].ToString();
                    }
                }
                else
                {
                    lblShareType.Text = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboLoanno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoanno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Select the loan account");
                    cboLoanno.Focus();
                    return;
                }

                dr = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loantype from loantype lt inner join loanbal lb on lt.loancode=lb.loancode where lb.Loanno='" + cboLoanno.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblLoantype.Text = dr["loantype"].ToString();
                    }
                }
                else
                {
                    lblLoantype.Text = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cmdNewJournal_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select surname,othernames,HomeAddr,companycode  from members  where memberno ='" + txtMemberNo.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblfullnames.Text = dr["surname"].ToString().Trim() + "  " + dr["othernames"].ToString().Trim();
                    }
                }
                else
                {
                    lblfullnames.Text = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
                getShareNLoansbyMember(txtMemberNo.Text.Trim());

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getShareNLoansbyMember(string p)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select subtype from Glsetup  where accno ='" + cboAccno.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string subType = dr["subtype"].ToString();
                        if (subType == "Shares" || subType == "OTHERS")
                        {
                            //cboShareType.Enabled = true;
                            //cboLoanno.Enabled = false;
                            cboLoanno.Items.Clear();
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select loanno from loanbal where memberno='" + txtMemberNo.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    cboLoanno.Items.Add(dr1["loanno"].ToString());
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            cboShareType.Enabled = true;
                            cboShareType.Focus();
                            cboLoanno.Enabled = true;
                            cboLoanno.Focus();
                            if (cboLoanno.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("Select the loan account");
                                cboLoanno.Focus();
                                return;
                            }

                            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loantype from loantype lt inner join loanbal lb on lt.loancode=lb.loancode where lb.Loanno='" + cboLoanno.Text + "'");
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    lblLoantype.Text = dr2["loantype"].ToString();
                                }
                            }
                            else
                            {
                                lblLoantype.Text = "";
                            }
                            dr2.Close(); dr2.Dispose(); dr2 = null;
                        }
                        else if (subType == "Loans" || subType == "Interests")
                        {
                            cboLoanno.Items.Clear();
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select loanno from loanbal where memberno='" + txtMemberNo.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    cboLoanno.Items.Add(dr1["loanno"].ToString());
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            cboShareType.Enabled = false;
                            cboLoanno.Enabled = true;
                            cboLoanno.Focus();
                            if (cboLoanno.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("Select the loan account");
                                cboLoanno.Focus();
                                return;
                            }

                            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.loantype from loantype lt inner join loanbal lb on lt.loancode=lb.loancode where lb.Loanno='" + cboLoanno.Text + "'");
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    lblLoantype.Text = dr2["loantype"].ToString();
                                }
                            }
                            else
                            {
                                lblLoantype.Text = "";
                            }
                            dr2.Close(); dr2.Dispose(); dr2 = null;
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cmdUnpostedJV_Click(object sender, EventArgs e)
        {
            try
            {
                Loadunpostedjournals();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loadunpostedjournals()
        {
            string readdata = "select vno,transdate,naration,sum(amount)Amount from journals where posted =0 group by vno,transdate,naration";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double Amt = 0;
                double AMOUNT_DR = 0;
                double AMOUNT_CR = 0;
                string trxtype = "";
                transactionno = Session["mimi"].ToString().Trim() + transactionno;

                if (Convert.ToDouble(txtTotalDr.Text) > 0)
                {
                    Amt = Convert.ToDouble(txtTotalDr.Text);
                    AMOUNT_DR = Amt;
                    trxtype = "DR";
                }
                if (Convert.ToDouble(txtTotalCr.Text) > 0)
                {
                    Amt = Convert.ToDouble(txtTotalCr.Text);
                    AMOUNT_CR = Amt;
                    trxtype = "CR";
                }
                if (rtpNarration.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the narration before adding the journal");
                    rtpNarration.Focus();
                    return;
                }
                string ShareType = "";
                string LType = "";
                if (lblShareType.Text != "")
                {
                    ShareType = cboShareType.Text.Trim();
                }
                if (lblLoantype.Text != "")
                {
                    LType = cboLoanno.Text.Trim();
                }
                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy insert into JournalsListing(VNO,ACCNO,NAME,NARATION,MEMBERNO,SHARETYPE,Loanno,AMOUNT_DR,AMOUNT_CR,AMOUNT,TRANSTYPE,AUDITID,TRANSDATE,AUDITDATE,POSTED,POSTEDDATE,Transactionno)values('" + txtJournaNo.Text.Trim() + "','" + cboAccno.Text.Trim() + "','" + txtAccNames.Text.Trim() + "','" + rtpNarration.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + ShareType + "','" + LType.Trim() + "','" + AMOUNT_DR + "','" + AMOUNT_CR + "','" + Amt + "','" + trxtype + "','" + Session["mimi"].ToString().Trim() + "','" + dtpReceiptDate.Text.Trim() + "','" + System.DateTime.Now + "','0','','" + transactionno + "')");
                LoadJournalsListing(txtJournaNo.Text.Trim());
                cmdProcessJournal.Enabled = true;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void SaveContrib(string MemberNo, string contribdate, string DateDeposited, string Vote, double Amount, string BankAC, string ReceiptNo, string ChequeNo, string user, string TransDescription, string transactionNo)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                transactionNo = Convert.ToString(TimeNow);
                transactionNo = txtMemberNo.Text + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();               
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
                string sqlinsert = "set dateformat dmy Insert into Contrib(memberno,contrdate,DepositedDate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)Values('" + txtMemberNo.Text + "','" + dtpReceiptDate.Text + "','" + dtpReceiptDate.Text + "','" +txtJournaNo.Text + "'," + txtTotalCr.Text + "," + TotalShares + ",'Journal posting for member,-" + txtMemberNo.Text + "','1','" + txtJournaNo.Text + "','" +lblShareType.Text + "','" + Session["mimi"].ToString() + "','" + cboShareType.Text + "','"+transactionno+"')";
                new WARTECHCONNECTION.cConnect().WriteDB(sqlinsert);

                WARTECHCONNECTION.cConnect SaccoMaster = new WARTECHCONNECTION.cConnect();
                string sqldatta = "select SharesAcc from sharetype where sharescode='" + cboShareType.Text + "'";
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
                       Save_GLTRANSACTIONS(dtpReceiptDate.Text,Convert.ToDouble(txtTotalCr.Text), cboAccno.Text, SharesAcc, txtJournaNo.Text, txtMemberNo.Text, Session["mimi"].ToString(), "Member Receipt" + '-' + TransDescription, "1", "1","1", transactionNo, "bosa");
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

        private void LoadJournalsListing(string JournaNo)
        {
            try
            {
                string readdata = "select vno,transdate,naration,AMOUNT_DR,AMOUNT_CR from JournalsListing where posted =0 and vno='" + JournaNo.Trim() + "' group by vno,transdate,naration,AMOUNT_DR,AMOUNT_CR";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(readdata);
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

        protected void txtDr_TextChanged(object sender, EventArgs e)
        {
            txtTotalDr.Text = txtDr.Text;
            txtDr.Text = "0";
            txtTotalCr.Text = "0";
        }

        protected void txtCr_TextChanged(object sender, EventArgs e)
        {
            txtTotalCr.Text = txtCr.Text;
            txtCr.Text = "0";
            txtTotalDr.Text = "0";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
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