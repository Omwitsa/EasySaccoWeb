using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace USACBOSA.FinanceAdmin
{
    public partial class WithdrawalNotice : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader Dr1, Dr2, Dr3;
        System.Data.SqlClient.SqlDataAdapter da;
        public string shareAcc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dtpnotice.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //dtpExpWithdrawDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                loadwthdralAcc();
            }
        }

        private void loadwthdralAcc()
        {
            cboAccno.Items.Clear();
            Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select accno from glsetup order by accno asc");
            if (Dr1.HasRows)
            {
                while (Dr1.Read())
                {
                    cboAccno.Items.Add(Dr1["accno"].ToString());
                }
            }
            Dr1.Close(); Dr1.Dispose(); Dr1 = null;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

            Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select m.surname+' '+ m.otherNames as MemberNames,c.CompanyName,m.withdrawn  from members m left outer join company c on m.companycode=c.companycode where m.memberno='" + txtMemberno.Text.Trim() + "'");
            if (Dr1.HasRows)
            {
                while (Dr1.Read())
                {
                    txtmembernames.Text = Dr1["MemberNames"].ToString().Trim();
                    lblcompany.Text = Dr1["CompanyName"].ToString().Trim();
                    int withdrawn = Convert.ToInt32(Dr1["withdrawn"]);
                    if (withdrawn == 0)
                    {
                        Status.Text = "ACTIVE";
                    }
                    else if (withdrawn == 1)
                    {
                        Status.Text = "WITHDRAWN";
                    }
                }
            }
            Dr1.Close(); Dr1.Dispose(); Dr1 = null;
            //GetMemberWithdrawaldetails();
            LoadMemberShares();
            LoadLoadGuaranteed();
        }

        private void GetMemberWithdrawaldetails()//noticeperiod,withdrawalFee
        {
            Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select Memberno,noticeperiod,withdrawalFee,BookingDate,Reason,WithdrawDate,Transactionno from WithdrawalNotice where memberno='" + txtMemberno.Text.Trim() + "' and Transactionno=''");
            if (Dr1.HasRows)
            {
                while (Dr1.Read())
                {
                    txtReason.Text = Dr1["Reason"].ToString();
                    txtWithFee.Text = Dr1["withdrawalFee"].ToString();
                    dtpExpWithdrawDate.Text = Dr1["BookingDate"].ToString();
                    string nperiod = Dr1["noticeperiod"].ToString().Trim();
                    if (nperiod == "60")
                    {
                        chkSixty.Checked = true;
                        chkSeven.Checked = false;
                    }
                    else if (nperiod == "7")
                    {
                        chkSeven.Checked = true;
                        chkSixty.Checked = false;
                    }
                }
            }
            Dr1.Close(); Dr1.Dispose(); Dr1 = null;
        }
        private void LoadLoadGuaranteed()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select LoanNo,LoaneeNames,GuarAmount,GuarBalance,Loanbalance from vwloanguarantors where guarmemberno= '" + txtMemberno.Text.Trim() + "'");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            ds.Dispose();
            da.Dispose();
        }
        private void LoadMemberShares()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select sharescode,Sharestype,TotalShares from vwMembershares where memberno='" + txtMemberno.Text.Trim() + "' and Totalshares<>0");
            DataSet dss = new DataSet();
            da.Fill(dss);
            GridView1.Visible = true;
            GridView1.DataSource = dss;
            GridView1.DataBind();
            dss.Dispose();
            da.Dispose();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LoadMemberLOans();
        }

        private void LoadMemberLOans()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT Loanno,LoanCode,Loantype,Balance FROM VWMEMBERLOANS WHERE MEMBERNO='" + txtMemberno.Text.Trim() + "' and balance>0");
            DataSet dsa = new DataSet();
            da.Fill(dsa);
            GridView2.Visible = true;
            GridView2.DataSource = dsa;
            GridView2.DataBind();
            dsa.Dispose();
            da.Dispose();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LoadLoadGuaranteed();
        }

        protected void btnNotify_Click(object sender, EventArgs e)
        {
            try
            {
                Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select * from withdrawalnotice where memberno='" + txtMemberno.Text.Trim() + "'");
                if (Dr1.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The Member is already booked to withdraw, thanks");
                    return;
                }
                if (txtMemberno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the member number");
                    txtMemberno.Focus();
                    return;
                }
                if (txtReason.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Give the reason for withdrawing");
                    txtReason.Focus();
                    return;
                }
                if (chkSeven.Checked == false && chkSixty.Checked == false)
                {
                    WARSOFT.WARMsgBox.Show("Please choose the withdrawal notice period");
                    return;
                }
                int noticeperiod = 60;
                if (chkSeven.Checked == true)
                {
                    noticeperiod = 7;
                }
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = 'W' + '-' + 'N' + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                string todabb = "set datefORMAT DMY insert into withdrawalnotice(memberno,BookingDate,noticeperiod,ExpWithdrawDate,withdrawalFee,reason,Transactionno) values('" + txtMemberno.Text.Trim() + "','" + dtpExpWithdrawDate.Text.Trim() + "'," + noticeperiod + ",'" + dtpExpWithdrawDate.Text.Trim() + "','" + txtWithFee.Text.Trim() + "','" + txtReason.Text.Trim() + "','" + transactionNo.Trim() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(todabb);
                WARSOFT.WARMsgBox.Show("Member successfully Booked for withdrawal");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtReason_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnWithdaraw_Click(object sender, EventArgs e)
        {
            try
            {
                // 'Check if he had booked to withdraw and is matured
                DateTime matureddate = DateTime.Today;
                double withdrawalFee = Convert.ToDouble(txtWithFee.Text);
                //Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select Bookingdate,noticeperiod from withdrawalnotice where memberno='" + txtMemberno.Text.Trim() + "'");
                //if (Dr1.HasRows)
                //{
                //    while (Dr1.Read())
                //    {
                //        DateTime bookingdate = Convert.ToDateTime(Dr1["bookingDate"]);
                //        int noticeperiod = Convert.ToInt32(Dr1["noticeperiod"]);
                //        matureddate = bookingdate.AddDays(noticeperiod);
                //    }
                //}
                //else
                //{
                //    WARSOFT.WARMsgBox.Show("The Member hasn't placed a notice for this withdrawal");
                //    return;
                //}

                //if (matureddate > System.DateTime.Today)
                //{
                //    WARSOFT.WARMsgBox.Show("The Member has not matured to withdraw membership");
                //    return;
                //}
                Dr3 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT Loanno,LoanCode,Loantype,Balance FROM VWMEMBERLOANS WHERE MEMBERNO='" + txtMemberno.Text.Trim() + "' and balance>0");
                if (Dr3.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The member has uncleared loans, please clear them first");
                    return;
                }
                Dr3.Close(); Dr3.Dispose(); Dr3 = null;
                Dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select LoanNo,LoaneeNames,GuarAmount,GuarBalance,Loanbalance from vwloanguarantors where guarmemberno= '" + txtMemberno.Text.Trim() + "'");
                if (Dr3.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The member has issues with Guarantorship.");
                    return;
                }
                Dr3.Close(); Dr3.Dispose(); Dr3 = null;
                if (withdrawalFee > 0 && txtAccNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Withdrawal fee account cannot be Blank or you havent choosen the control account!");
                    return;
                }
                //'you can now withdraw
                //'Retrieve the mainshares for refunding
                string SchemeCode = "";
                double SharesAmt = 0;
                Dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select s.sharescode,isnull(sum(s.amount),0) TotalShares from contrib s inner join sharetype st on s.sharescode=st.sharescode where st.ismainshares=1 and s.memberno='" + txtMemberno.Text + "' group by s.sharescode having isnull(sum(s.amount),0)>0 ");
                if (Dr2.HasRows)
                {
                    while (Dr2.Read())
                    {
                        SharesAmt = Convert.ToDouble(Dr2["TotalShares"]);
                        SchemeCode = Dr2["sharesCode"].ToString().Trim();
                    }
                }
                else
                {
                    //WARSOFT.WARMsgBox.Show("The is no money in the main shares account for this member.");
                    //return;
                }
                Dr2.Close(); Dr2.Dispose(); Dr2 = null;
                if (withdrawalFee > SharesAmt)
                {
                    //WARSOFT.WARMsgBox.Show("The withdrawal fee is more than the share balance");
                    //return;
                }
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = 'W' + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," + SharesAmt + ",'" + Session["mimi"].ToString() + "','" + Convert.ToDateTime(dtpExpWithdrawDate.Text) + "','Membership withdrawal','Posted')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
                if (withdrawalFee > 0 && SharesAmt > 0)
                {
                    SaveContrib(txtMemberno.Text, Convert.ToDateTime(dtpExpWithdrawDate.Text), SchemeCode, withdrawalFee * (-1), cboAccno.Text.Trim(), "Membership Withdrawal", "Membership Withdrawal", "Membership Withdrawal", "Membership Withdrawal (" + txtMemberno.Text + ")", transactionNo);
                    Dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select s.sharescode,isnull(sum(s.amount),0) TotalShares from contrib s inner join sharetype st on s.sharescode=st.sharescode where st.ismainshares=1 and s.memberno='" + txtMemberno.Text + "' group by s.sharescode having isnull(sum(s.amount),0)>0 ");
                    if (Dr2.HasRows)
                    {
                        while (Dr2.Read())
                        {
                            SharesAmt = Convert.ToDouble(Dr2["TotalShares"]);
                            SchemeCode = Dr2["sharesCode"].ToString().Trim();
                        }
                    }
                    Dr2.Close(); Dr2.Dispose(); Dr2 = null;
                }
                else
                {
                    SharesAmt = SharesAmt - withdrawalFee;
                }
                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into shareWithdrawal(memberno,sharescode,withdrawalcharges,withdrawaldate,amount,auditId,Purpose,transactionNo)Values('" + txtMemberno.Text + "','" + SchemeCode + "','" + withdrawalFee + "','" + dtpExpWithdrawDate.Text + "'," + SharesAmt + ",'" + Session["mimi"].ToString().Trim() + "','Membership Withdrawal','" + transactionNo + "')");
                if (CheckBox1.Checked == false)
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy update members set withdrawn=1 where memberno='" + txtMemberno.Text.Trim() + "'");

                    new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy update Withdrawalnotice set withdrawdate='" + dtpExpWithdrawDate.Text + "',transactionno='" + transactionNo + "' where memberno='" + txtMemberno.Text.Trim() + "'");

                    WARSOFT.WARMsgBox.Show("Member has Withdrawn permanently");
                    return;
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("Member Account has been taken over now by Next Of Kin");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void SaveContrib(string memberno, DateTime ContrDate, string SHARECode, double contrAmount, string BankAcc, string Receiptno, string chequeno, string transby, string remarks, string transactionNo)
        {
            int RefNo = 0;
            double TotalShares = 0;
            DateTime lastdate = DateTime.Today;
            DateTime thisDate = ContrDate;
            string TempAcc = "";
            //'GET THE REFNO
            //ContrDate = ContrDate.ToString("DD/MM/YYYY");

            Dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(contrib.amount),0)amount  from contrib  where contrib.memberno='" + memberno + "' and contrib.sharescode='" + SHARECode + "' group by contrib.memberno");
            if (Dr3.HasRows)
            {
                while (Dr3.Read())
                {
                    TotalShares = Convert.ToDouble(Dr3["amount"]);
                }
            }
            Dr3.Close(); Dr3.Dispose(); Dr3 = null;
            TotalShares = TotalShares + contrAmount;

            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into Contrib(memberno,contrdate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno) Values('" + memberno + "','" + ContrDate + "'," + RefNo + "," + contrAmount + "," + TotalShares + ",'" + transby + "','" + chequeno + "','" + Receiptno + "','" + remarks + "','" + Session["mimi"].ToString().Trim() + "','" + SHARECode + "','" + transactionNo + "')");

            //'Effect the Payments to the GL
            //'First, Get the Relevant Accounts
            Dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select IsNull(SharesAcc,0)SharesAcc from sharetype where sharescode='" + SHARECode + "'");
            if (Dr3.HasRows)
            {
                while (Dr3.Read())
                {
                    shareAcc = Dr3["SharesAcc"].ToString();
                    if (shareAcc == "0")
                    {
                        WARSOFT.WARMsgBox.Show("The Gl Control Account for this scheme(" + SHARECode + ") is not set");
                        return;
                    }
                }
            }
            else
            {
                WARSOFT.WARMsgBox.Show("Invalid Share code(" + SHARECode + ")");
                return;
            }
            Dr3.Close(); Dr3.Dispose(); Dr3 = null;
            if (BankAcc == "Non-Cash")
            {
                BankAcc = shareAcc;
            }
            else
            {
                if (Convert.ToDouble(contrAmount) < 0)
                {
                    TempAcc = BankAcc;
                    BankAcc = shareAcc;
                    shareAcc = TempAcc;
                    contrAmount = Math.Abs(contrAmount);
                }
                Save_GLTRANSACTION(ContrDate, contrAmount, BankAcc, shareAcc, Receiptno, memberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo, "BOSA");
                shareAcc = BankAcc;
            }

            if (ContrDate < lastdate)
            {
                RefreshContrib(memberno, SHARECode);
            }
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRaccno, string Craccno, string DocumentNo, string Source, string auditid, string TransDescription, int CashBook, int doc_posted, string chequeno, string transactionNo, string Module)
        {
            DateTimeFormatInfo fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + Source + "','" + auditid + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','" + Module + "'");
        }

        private void RefreshContrib(string mMemberno, string sharesCode)
        {
            double conId = 0;
            double TotShares = 0;

            Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select memberno,sharescode,contrdate,amount,id from contrib where memberno='" + mMemberno + "' and sharescode='" + sharesCode + "' order by sharescode,contrdate,Refno");
            if (Dr1.HasRows)
            {
                while (Dr1.Read())
                {
                    double Amount = Convert.ToDouble(Dr1["Amount"]);
                    conId = Convert.ToDouble(Dr1["Id"]);
                    TotShares = TotShares + Amount;
                    new WARTECHCONNECTION.cConnect().WriteDB(" set dateformat dmy Update contrib set shareBal=" + TotShares + " where id=" + conId + "");
                }
            }
            Dr1.Close(); Dr1.Dispose(); Dr1 = null;
        }

        protected void chkSeven_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the member number");
                    txtMemberno.Focus();
                    return;
                }
                if (chkSeven.Checked == true)
                {
                    string SchemeCode = "";
                    double SharesAmt = 0;
                    double withAmount = 0;
                    Dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select s.sharescode,isnull(sum(s.amount),0) TotalShares from contrib s inner join sharetype st on s.sharescode=st.sharescode where st.ismainshares=1 and s.memberno='" + txtMemberno.Text + "' group by s.sharescode having isnull(sum(s.amount),0)>0 ");
                    if (Dr2.HasRows)
                    {
                        while (Dr2.Read())
                        {
                            SharesAmt = Convert.ToDouble(Dr2["TotalShares"]);
                            SchemeCode = Dr2["sharesCode"].ToString().Trim();
                            withAmount = 0.1 * SharesAmt;
                        }
                    }
                    Dr2.Close(); Dr2.Dispose(); Dr2 = null;
                    txtWithFee.Text = (1000 + withAmount).ToString();
                    chkSixty.Checked = false;
                    CheckBox1.Checked = false;
                    //dtpExpWithdrawDate.Text = (Convert.ToDateTime(dtpnotice.Text).AddDays(7)).ToString();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void chkSixty_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMemberno.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter the member number");
                txtMemberno.Focus();
                return;
            }
            if (chkSixty.Checked == true)
            {
                txtWithFee.Text = "1000.00";
                chkSeven.Checked = false;
                CheckBox1.Checked = false;
                //dtpExpWithdrawDate.Text = (Convert.ToDateTime(dtpnotice.Text).AddDays(60)).ToString();
            }
        }

        protected void btnshareoffset_Click(object sender, EventArgs e)
        {

        }

        protected void cboAccno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select glaccname from GLSETUP where accno='" + cboAccno.Text.Trim() + "'");
            if (Dr1.HasRows)
            {
                while (Dr1.Read())
                {
                    txtAccNames.Text = Dr1["glaccname"].ToString().Trim();
                }
            }
            Dr1.Close(); Dr1.Dispose(); Dr1 = null;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMemberno.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter the member number");
                txtMemberno.Focus();
                return;
            }
            if (CheckBox1.Checked == true)
            {
                txtWithFee.Text = "1000.00";
                chkSeven.Checked = false;
                chkSixty.Checked = false;
                //dtpExpWithdrawDate.Text = (Convert.ToDateTime(dtpnotice.Text).AddDays(60)).ToString();
            }
        }
    }
}