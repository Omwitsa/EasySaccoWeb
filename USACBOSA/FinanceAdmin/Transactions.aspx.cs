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
    public partial class Transactions : System.Web.UI.Page
    {
        SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
        SqlDataAdapter da;
        static string loanno;
        string DRAcc;
        string CRAcc;
        string documenNo;
        string TempAccNo;
        string source;
        DateTime lastrepay;
        double penalty = 0;
        double INTROWED = 0;
        double loanbalance = 0;
        double paymentno = 0;
        double principal = 0;
        double interest = 0;
        string MEMBERNO;
        string sharescode;
        double Amount = 0;
        string receiptno;
        double REFNO = 0;
        double TotalShares = 0;
        double TotalShare = 0;
        string transadescription;
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
                LoadAppraisedLoans();
                TextBox2.Text = Convert.ToString(DateTime.Now);
            }
        }
        private void LoadAppraisedLoans()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy Select TransactionNo,TransDate,Amount,auditId as UserID,TransDescription from Transactions where Transdate='" + DateTime.Today + "' and status='Posted'");
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
        private void LoadTransactions()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy Select TransactionNo,TransDate,Amount,auditId as UserID,TransDescription from Transactions where Transdate='" +TextBox1.Text + "' and status='Posted'");
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
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(GridView1.SelectedRow.Cells[3].Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                double transactionNo = Convert.ToDouble(GridView1.SelectedRow.Cells[1].Text);
                double amount = Convert.ToDouble(GridView1.SelectedRow.Cells[3].Text);
                if (CheckBox1.Checked == true)
                {
                    CheckBox2.Checked = false;
                    //************Start the Delition process**************
                    //'Start with Loans
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select distinct Loanno,sum(principal) as principal,sum(interest) as interest,sum(intrOwed) as intOwed,max(paymentno) as paymentno,sum(loanbalance) loanbalance from repay where transactionno='" + transactionNo + "' group by loanno");//
                    if (dr2.HasRows)
                        while (dr2.Read())
                        {
                            loanno = dr2["Loanno"].ToString();
                            principal = Convert.ToDouble(dr2["principal"]);
                            interest = Convert.ToDouble(dr2["interest"]);
                            paymentno = Convert.ToDouble(dr2["paymentno"]);
                            loanbalance = Convert.ToDouble(dr2["loanbalance"]);
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT INTROWED,penalty,datereceived FROM REPAY WHERE LOANNO='" + loanno + "' AND PAYMENTNO=" + paymentno + "");//
                            if (dr1.HasRows)
                                while (dr1.Read())
                                {
                                    INTROWED = Convert.ToDouble(dr1["INTROWED"]);
                                    penalty = Convert.ToDouble(dr1["penalty"]);
                                    lastrepay = Convert.ToDateTime(dr1["datereceived"]);
                                }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            paymentno = paymentno + 1;
                            //Just Delete this transaction
                            string updateLoanbal = "set dateformat dmy Update loanbal set balance=" + (loanbalance + principal + interest) + ",intrOwed=" + INTROWED + ",lastDate='" + lastrepay + "' where loanno='" + loanno + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updateLoanbal);
                            //Delete from Repay
                            string DELETEREPAY = "Delete from repay where transactionno='" + transactionNo + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(DELETEREPAY);
                            //Delete from contrib
                            string DELETECONTRIB = "delete from contrib where transactionno='" + transactionNo + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(DELETECONTRIB);
                            //Delete from GL
                            string DELETEGL = "Delete from gltransactions where transactionNo='" + transactionNo + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(DELETEGL);
                            //Update transactions as deleted
                            string updateTransaction = "Update Transactions set Status='DELETED' where transactionno='" + transactionNo + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updateTransaction);
                            WARSOFT.WARMsgBox.Show("Transaction has been deleted successfully");
                        }
                }
                if (CheckBox2.Checked == true)
                {
                    CheckBox1.Checked = false;
                    //************Start the Delition process**************
                    //'Start with Loans
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select distinct Loanno,sum(principal) as principal,sum(interest) as interest,sum(intrOwed) as intOwed,max(paymentno) as paymentno,sum(loanbalance) loanbalance from repay where transactionno='" + transactionNo + "' group by loanno");//
                    if (dr2.HasRows)
                        while (dr2.Read())
                        {
                            loanno = dr2["Loanno"].ToString();
                            principal = Convert.ToDouble(dr2["principal"]);
                            interest = Convert.ToDouble(dr2["interest"]);
                            paymentno = Convert.ToDouble(dr2["paymentno"]);
                            loanbalance = Convert.ToDouble(dr2["loanbalance"]);
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT INTROWED,penalty,datereceived FROM REPAY WHERE LOANNO='" + loanno + "' AND PAYMENTNO=" + paymentno + "");//
                            if (dr1.HasRows)
                                while (dr1.Read())
                                {
                                    INTROWED = Convert.ToDouble(dr1["INTROWED"]);
                                    penalty = Convert.ToDouble(dr1["penalty"]);
                                    lastrepay = Convert.ToDateTime(dr1["datereceived"]);
                                }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            paymentno = paymentno + 1;
                            //Just Delete this transaction
                            string updateLoanbal = "set dateformat dmy Update loanbal set balance=" + (loanbalance + principal) + ",intrOwed=" + INTROWED + ",lastDate='" + lastrepay + "' where loanno='" + loanno + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updateLoanbal);

                            dr3 = new WARTECHCONNECTION.cConnect().ReadDB("Select intbalance,loanbalance from repay where transactionNo='" + transactionNo + "' ");//
                            if (dr3.HasRows)
                                while (dr3.Read())
                                {
                                    double intbalance = Convert.ToDouble(dr3["intbalance"]);
                                    double loanbal = Convert.ToDouble(dr3["loanbalance"]);
                                    string INsertREPAY = "set dateformat dmy Insert into Repay(Loanno,Datereceived,Paymentno,Amount,Principal,Interest,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo) Values('" + loanno + "','" + DateTime.Today + "'," + paymentno + "," + (principal + interest) * (-1) + "," + principal * (-1) + "," + interest * (-1) + "," + INTROWED + "," + penalty + " ," + intbalance * (-1) + "," + (loanbal + principal) + ",'" + transactionNo + "','" + Session["mimi"].ToString() + "','" + "Reversal" + "','" + Session["mimi"].ToString() + "','" + transactionNo + "')";
                                    new WARTECHCONNECTION.cConnect().WriteDB(INsertREPAY);
                                }
                            dr3.Close(); dr3.Dispose(); dr3 = null;
                        }
                    dr2.Close(); dr2.Dispose(); dr2 = null;
                    //NEXT IS SHARES
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select distinct memberno,sharescode,Receiptno,sum(Amount) as Amount from Contrib where transactionno='" + transactionNo + "' group by memberno,sharescode,receiptno");//
                    if (dr2.HasRows)
                        while (dr2.Read())
                        {
                            MEMBERNO = dr2["memberno"].ToString();
                            sharescode = dr2["sharescode"].ToString();
                            Amount = Convert.ToDouble(dr2["Amount"]);
                            receiptno = dr2["receiptno"].ToString();
                            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT max(isnull(REFNO,0)) as REFNO FROM CONTRIB WHERE MEMBERNO='" + MEMBERNO + "' and transactionno='" + transactionNo + "'");//
                            if (dr1.HasRows)
                                while (dr1.Read())
                                {
                                    REFNO = Convert.ToDouble(dr1["REFNO"]);
                                    REFNO = REFNO + 1;
                                }
                            else
                            {
                                REFNO = 1;
                            }
                            dr1.Close(); dr1.Dispose(); dr1 = null;
                            paymentno = paymentno + 1;
                            //Just Delete this transaction
                            string updateLoanbal = "Update loanbal set balance=" + loanbalance + principal + ",intrOwed=" + INTROWED + ",lastDate='" + lastrepay + "' where loanno='" + loanno + "'";
                            new WARTECHCONNECTION.cConnect().WriteDB(updateLoanbal);
                            dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(m.initshares,0) as Total,case when sum(contrib.amount) is null then 0 else sum(contrib.amount)end as Totalshares  from members m left outer join contrib on m.memberno=contrib.memberno where m.memberno='" + MEMBERNO + "' and contrib.sharescode='" + sharescode + "' group by m.initshares");//
                            if (dr3.HasRows)
                                while (dr3.Read())
                                {
                                    TotalShares = Convert.ToDouble(dr3["Total"]) + Convert.ToDouble(dr3["TotalShares"]);
                                }
                            dr3.Close(); dr3.Dispose(); dr3 = null;
                            TotalShare = TotalShares - Amount;
                            if (sharescode == "D")
                            {
                                transadescription = "AKIBA DEPOSITS";
                            }
                            if (sharescode == "SC")
                            {
                                transadescription = "SHARE CAPITAL";
                            }
                            if (sharescode == "E")
                            {
                                transadescription = "ENTRANCE FEES";
                            }
                            string sqlinsert = "set dateformat dmy Insert into Contrib(memberno,contrdate,DepositedDate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno)Values('" + MEMBERNO + "','" + DateTime.Today + "','" + DateTime.Today + "','" + REFNO + "'," + Amount * (-1) + "," + TotalShare + ",'Receipt-Reversal','1','Reversal-" + receiptno + "','" + transadescription + "','" + Session["mimi"].ToString() + "','" + sharescode + "','" + transactionNo + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(sqlinsert);
                        }
                    dr2.Close(); dr2.Dispose(); dr2 = null;
                    //Delete the transaction from gltransactions table
                    dr4 = new WARTECHCONNECTION.cConnect().ReadDB("select Amount,DrAccNo,CrAccNo,DocumentNo,SOURCE from gltransactions where transactionNo='" + transactionNo + "'");//
                    if (dr4.HasRows)
                        while (dr4.Read())
                        {
                            DRAcc = dr4["DrAccNo"].ToString();
                            CRAcc = dr4["DrAccNo"].ToString();
                            Amount = Convert.ToDouble(dr4["Amount"]);
                            documenNo = dr4["DocumentNo"].ToString();
                            source = dr4["SOURCE"].ToString();
                            //switch the accounts
                            TempAccNo = DRAcc;
                            DRAcc = CRAcc;
                            CRAcc = TempAccNo;
                            string saccoinsert = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateTime.Today + "'," + Amount * (-1) + ",'" + DRAcc + "','" + CRAcc + "','" + transactionNo + "','" + source + "','" + Session["mimi"].ToString() + "','Reversal-" + documenNo + "','0','1','','" + transactionNo + "',''";
                            new WARTECHCONNECTION.cConnect().WriteDB(saccoinsert);
                        }
                    dr4.Close(); dr4.Dispose(); dr4 = null;
                    //Update Transaction as Deleted
                    string updateTRans = "Update Transactions set Status='REVERSED' where transactionno='" + transactionNo + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(updateTRans);
                    WARSOFT.WARMsgBox.Show("Transaction has been Reversed successfully");
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                CheckBox2.Checked = false;
            }
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                CheckBox1.Checked = false;

            }
        }
        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You must choose Transaction Date");
                }
                else
                {
                    if (CheckBox3.Checked == true)
                    {
                        dr4 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy select * from transactions where TransDate ='" + TextBox1.Text + "' and status='Posted'");//
                        if (dr4.HasRows)
                            {
                                LoadTransactions();
                            }
                        dr4.Close(); dr4.Dispose(); dr4 = null;
                    }
                    else
                    {
                        dr4 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy select * from transactions where status='Posted' order by audittime desc");//
                        if (dr4.HasRows)
                        {
                            LoadTransactions();
                        }
                        dr4.Close(); dr4.Dispose(); dr4 = null;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}