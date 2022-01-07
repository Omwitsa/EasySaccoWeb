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

namespace USACBOSA.FinanceAdmin
{
    public partial class Trans_Reversals : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr2, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        string TransType;
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
        public string rmethod, intRecovery = "";
        public int rperiod, mdtei = 0;
        public Double rrate = 0;
        public Double initialAmount = 0;
        public string transactionNo = "";
        public Double transactionTotal, loanbalance = 0;
        public Double PrincAmount, IntrAmount, intTotal, IntBalalance = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ShowMemberDetails();
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtReceiptNo_TextChanged(object sender, EventArgs e)
        {
            LoadReceiptDetails();
            Loaddatatogrid();
        }

        private void LoadReceiptDetails()
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from contrib where ReceiptNo='" + txtReceiptNo.Text.Trim() + "' and Posted=2");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("The transaction was already reversed,Confirm from the the statement");
                        btnRefresh.Enabled = false; btnPostReversals.Enabled = false;
                        dr.Close(); dr.Dispose(); dr = null; return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                btnPostReversals.Enabled = true;
                btnRefresh.Enabled = true;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select sum(Amount)as Amount from MembersAccount where ReceiptNo='" + txtReceiptNo.Text.Trim() + "' and Posted=1");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtDistributedAmount.Text = dr["Amount"].ToString();
                        double bal = -1 * Math.Ceiling(Convert.ToDouble(txtDistributedAmount.Text));
                        txtReceiptAmount.Text = bal.ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                string MYType = "";
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select TransType from MembersAccount where ReceiptNo='" + txtReceiptNo.Text.Trim() + "' and Posted=1 order by type");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MYType = dr["TransType"].ToString();

                        if (MYType == "Contrib")
                        {
                            string dracc = "";
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select R.AUDITID,R.PTYPE,R.Draccno,G.Glaccname from ReceiptBooking R INNER JOIN GLSETUP G ON G.Glcode=R.Draccno where R.ReceiptNo='" + txtReceiptNo.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    cboBankAC.Text = dr1["Draccno"].ToString();
                                    txtBankAC.Text = dr1["Glaccname"].ToString();
                                    cboPaymentMode.Text = dr1["PTYPE"].ToString();
                                    txtPostedBy.Text = dr1["AUDITID"].ToString();
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select MemberNo,contrdate,chequeno,depositeddate,refno from CONTRIB where ReceiptNo='" + txtReceiptNo.Text.Trim() + "'");
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    txtMemberNo.Text = dr1["MemberNo"].ToString();
                                    txtContribDate.Text = dr1["contrdate"].ToString();
                                    txtDateDeposited.Text = dr1["depositeddate"].ToString();
                                    txtChequeNo.Text = dr1["chequeno"].ToString();
                                    txtSerialNo.Text = dr1["refno"].ToString();
                                    ShowMemberDetails();
                                }
                            }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loaddatatogrid()
        {
            try
            {
                string datatable = "select Vote,TransDescription,Amount,Principal,Interest,IntOwed [Interest Accrued],MemberNo,MemberNames,Type from MembersAccount where MemberNo='" + txtMemberNo.Text.TrimStart().TrimEnd() + "' and ReceiptNo='" + txtReceiptNo.Text.Trim() + "'";
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

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnPostReversals_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmReversal.Checked == false)
                {
                    WARSOFT.WARMsgBox.Show("You need to confirm the reversal details first, Check and continue");
                    chkConfirmReversal.Focus();
                    return;
                }
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
                if (txtChequeNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Cheque/ Mpesa Reference/ Bank reference Number is required");
                    txtChequeNo.Focus();
                    return;
                }
                if (txtReceiptNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Receipt Number is required.");
                    txtReceiptNo.Focus();
                    return;
                }
                if ((Convert.ToDouble(txtDistributedAmount.Text)) > -10)
                {
                    WARSOFT.WARMsgBox.Show("Sorry the minimum amount to be Reversed is reached, please check payment details first");
                    txtDistributedAmount.Focus();
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
                if (Convert.ToDouble(txtDistributedAmount.Text) >= 0)
                {
                    WARSOFT.WARMsgBox.Show("The Amount to be reversed should be Less than Zero, Debit of the previous amount Posted");
                    txtDistributedAmount.Focus();
                    return;
                }

                string memebrtrans = "set dateformat dmy select MemberAccountId,Vote,TransDescription,Amount,rmethod,Principal,Interest,IntOwed,MemberNo,MemberNames,Type,TransType from MembersAccount where MemberNo='" + txtMemberNo.Text.Trim() + "' and ReceiptNo='" + txtReceiptNo.Text.Trim() + "' and posted=1";
                WARTECHCONNECTION.cConnect selda = new WARTECHCONNECTION.cConnect();
                dr1 = selda.ReadDB(memebrtrans);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        TransType = dr1["TransType"].ToString();
                        string MemberNo = dr1["MemberNo"].ToString();
                        string MemberNames = dr1["MemberNames"].ToString();
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
                        transactionNo = MemberNo + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                        //    saveReceipt(txtReceiptNo.Text, "MPA", txtSerialNo.Text.Trim(), txtMemberNo.Text, txtMemberNo.Text, TransDescription, System.DateTime.Today, Amount, txtChequeNo.Text, cboPaymentMode.Text);

                        if (TransType == "Contrib")
                        {
                            Vote = voteType;
                            SaveContrib(MemberNo, txtContribDate.Text, txtDateDeposited.Text, Vote, Amount, cboBankAC.Text, txtReceiptNo.Text, txtChequeNo.Text, Session["mimi"].ToString(), TransDescription, "REV" + transactionNo);

                            string audittrans = "set dateformat dmy insert into AUDITTRANS(0)values('Contrib','Receipt Posting Reversal>" + Vote + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text.Trim() + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                        }
                        //else if (TransType == "LoanRepayment")
                        //{
                        //    SaveTransaction();
                        //    SaveRepay(rmethod, txtSerialNo.Text, Vote, txtDateDeposited.Text, Amount, Principal, Interest, IntOwed, cboBankAC.Text, txtReceiptNo.Text, TransDescription, 0, 0, "Loan Recovery Rcpt(" + MemberNames + ")", Session["mimi"].ToString(), MemberNo, transactionNo, 1, 0);

                        //    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Contrib','Receipt Posting Shares Code" + Vote + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text.Trim() + "','" + Session["mimi"].ToString() + "')";
                        //    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                        //}
                        string postedd = "SET DATEFORMAT DMY update MembersAccount set Posted=2 where ReceiptNo= '" + txtReceiptNo.Text.Trim() + "' and MemberNo='" + txtMemberNo.Text.Trim() + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(postedd);
                        string contribb = "SET DATEFORMAT DMY update contrib set Posted=2 where ReceiptNo= '" + txtReceiptNo.Text.Trim() + "' and MemberNo='" + txtMemberNo.Text.Trim() + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(contribb);
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Transaction "+txtReceiptNo.Text+" Cannot be Referenced well to complete reversal. Please Confirm"); return;
                }
                WARSOFT.WARMsgBox.Show("Transaction Reversal Posted Successfully");

                dr1.Close(); dr1.Dispose(); dr1 = null; selda.Dispose(); selda = null;
                clearTexts();
                GridView.Visible = false;
                btnPostReversals.Enabled = false;
                btnRefresh.Enabled = false;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtReceiptNo.Text = "";
            txtSerialNo.Text = "";
            txtReceiptAmount.Text = "";
            txtPostedBy.Text = "";
            txtNames.Text = "";
            txtMemberNo.Text = "";
            txtDistributedAmount.Text = "";
            txtDateDeposited.Text = "";
            txtContribDate.Text = "";
            txtChequeNo.Text = "";
            txtBankAC.Text = "";
            cboBankAC.Text = ""; cboPaymentMode.Text = "";
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
                string sqlinsert = "set dateformat dmy Insert into Contrib(memberno,contrdate,depositeddate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)Values('" + MemberNo + "','" + contribdate + "','" + DateDeposited + "'," + txtSerialNo.Text + "," + Amount + "," + TotalShares + ",'Receipt','" + ChequeNo + "','" + ReceiptNo + "','" + TransDescription + "','" + Session["mimi"].ToString() + "','" + Vote + "','" + transactionNo + "')";
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
                        transactionNo = txtMemberNo.Text + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                        Save_GLTRANSACTIONS(txtDateDeposited.Text, Amount, cboBankAC.Text, SharesAcc, txtReceiptNo.Text, txtMemberNo.Text, Session["mimi"].ToString(),  TransDescription+ '-' + txtReceiptNo.Text.Trim() , "1", "1", txtChequeNo.Text, transactionNo, "bosa");
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
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtReceiptAmount_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMemberNo_DataBinding(object sender, EventArgs e)
        {
            ShowMemberDetails();
        }

        private void ShowMemberDetails()
        {
            try
            {
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select m.surname,m.othernames,m.HomeAddr,m.companycode,m.entrance,m.companycode  from members m  where m.memberno ='" + txtMemberNo.Text.Trim() + "'");
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        bool IsGroup = false;
                        txtNames.Text = dr2["surname"].ToString() + ' ' + dr2["othernames"].ToString();
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Member number with number " + txtMemberNo.Text.Trim() + " does not exist, Please confirm"); return;
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                double bal2 = -1 * Math.Ceiling(Convert.ToDouble(txtDistributedAmount.Text));
                txtDistributedAmount.Text = bal2.ToString();
                double bal = -1 * Math.Ceiling(Convert.ToDouble(txtReceiptAmount.Text));
                txtReceiptAmount.Text = bal.ToString();
                string memebrtrans = "select MemberAccountId,Vote,TransDescription,Amount,rmethod,Principal,Interest,IntOwed,MemberNo,MemberNames,Type,TransType from MembersAccount where MemberNo='" + txtMemberNo.Text + "' and ReceiptNo='" + txtReceiptNo.Text.Trim() + "' and posted=1";
                WARTECHCONNECTION.cConnect selda = new WARTECHCONNECTION.cConnect();
                dr1 = selda.ReadDB(memebrtrans);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        string TransDescription = dr1["TransDescription"].ToString();
                        if (TransDescription.Contains("Reversal:"))
                        {
                            string[] TransDescription1 = TransDescription.Split(':');
                            TransDescription = TransDescription1[1];
                        }
                        else
                        {
                            TransDescription = "Reversal:" + TransDescription;
                        }
                        string MemberAccountId = dr1["MemberAccountId"].ToString();
                        double amountinversed = Convert.ToDouble(dr1["Amount"]) * -1;
                        string uudamount = "set dateformat dmy update MembersAccount set Amount='" + amountinversed + "',TransDescription='" + TransDescription + "' where MemberAccountId='" + MemberAccountId + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(uudamount);
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The Transaction could either be not Sucessfully posted or Has been reversed already"); return;
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                Loaddatatogrid();
                //btnPostReversals.ForeColor = System.Drawing.Color.Red;
                //btnRefresh.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
    }
}