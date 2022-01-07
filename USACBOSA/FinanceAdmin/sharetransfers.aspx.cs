using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.FinanceAdmin
{
    public partial class sharetransfers : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, DR0, DRx, DR1, DR12, DR13, DR13a, Dr, Dr1, DRv, DR5, Dr12, DR3;
        System.Data.SqlClient.SqlDataAdapter da, da1;
        String day;
        String TransNo;
        int ContraAcc; Double Contribamnt;
        public string shareAcc, transactionNo = "";
        public double transactionTotal, Amount = 0;
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
                DateTime Leo = System.DateTime.Today;
                String Tarehe = Leo.ToString("yyyy/MM/dd");
                LoadDetails();
                if (!IsPostBack)
                {
                    dtpTransDate.Text = Leo.ToString();
                    txtDBalance.Text = "0";
                    txtRBalance.Text = "0";
                    txtDAvailable.Text = "0";
                    txtRAvailable.Text = "0";
                    txtTransAmount.Text = "0";
                    day = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
                    TransNo = day;
                }
            }
        }

        private void LoadDetails()
        {
            try
            {
                cboDCode.Items.Add("");
                cboRCode.Items.Add("");
                DR13 = new WARTECHCONNECTION.cConnect().ReadDB("select sharescode from sharetype order by usedToGuarantee desc");
                if (DR13.HasRows)
                {
                    while (DR13.Read())
                    {
                        cboDCode.Items.Add(DR13["sharescode"].ToString());
                        cboRCode.Items.Add(DR13["sharescode"].ToString());

                    }
                }
                else
                {
                    cboRCode.Items.Clear();
                    cboDCode.Items.Clear();
                }
                DR13.Close(); DR13.Dispose(); DR13 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void TxtDonor_TextChanged(object sender, EventArgs e)
        {
            lodmember();
        }

        private void lodmember()
        {
            try
            {
                if (txtDMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the donor member number to proceed");
                    txtDMemberNo.Focus();
                    return;
                }
                Dr = new WARTECHCONNECTION.cConnect().ReadDB("Select surname + ' ' + othernames as membernames from members where memberno='" + txtDMemberNo.Text + "' ");
                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        txtDNames.Text = Dr["membernames"].ToString();
                        cboDCode_Change();
                    }
                }
                else
                {
                    txtDNames.Text = "";
                    txtDBalance.Text = "0";
                    txtDAvailable.Text = "0";
                }
                Dr.Close(); Dr.Dispose(); Dr = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void cboDCode_Change()
        {
            if (cboDCode.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Donor share code cannot be empty");
                cboDCode.Focus();
                return;
            }
            DR0 = new WARTECHCONNECTION.cConnect().ReadDB("Select sharestype,withdrawable,SharesAcc from sharetype where sharescode='" + cboDCode.Text + "'");
            if (DR0.HasRows)
            {
                while (DR0.Read())
                {
                    //cboDCode.Text = DR0["SharesAcc"].ToString();
                    txtDSharetype.Text = DR0["sharestype"].ToString();
                    bool isWithdrawable = Convert.ToBoolean(DR0["withdrawable"].ToString());
                }
            }
            DR0.Close(); DR0.Dispose(); DR0 = null;

            if (txtDMemberNo.Text != "")
            {
                DR1 = new WARTECHCONNECTION.cConnect().ReadDB(" select isnull(sum(s.amount),0) TotalShares,st.usedtoguarantee from contrib S inner join sharetype st ON S.sharescode=st.sharescode where S.memberno='" + txtDMemberNo.Text + "' and S.sharescode='" + cboDCode.Text + "' group by s.MEMBERNO,st.usedtoguarantee ");
                if (DR1.HasRows)
                {
                    while (DR1.Read())
                    {
                        txtDBalance.Text = DR1["TotalShares"].ToString();
                        string usedtoguaranee = DR1["usedtoguarantee"].ToString();
                        /// 'if used to guarantee, check how much has he committed
                        if (usedtoguaranee == "1")
                        {
                            Dr12 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(balance),0) balance from loanguar where memberno='" + txtDMemberNo.Text + "' and transfered =0");
                            double SUMM = (Convert.ToDouble(Dr12["TotalShares"]) - Convert.ToDouble(Dr12["Balance"]));
                            txtDAvailable.Text = Convert.ToString(SUMM);
                        }
                        else
                        {
                            txtDAvailable.Text = txtDBalance.Text;
                        }
                    }
                }
                else
                {
                    txtDBalance.Text = "0";
                    txtDAvailable.Text = "0";
                }
            }
            else
            {
                txtDBalance.Text = "0";
                txtDAvailable.Text = "0";
            }
        }


        protected void ShareCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect ll = new WARTECHCONNECTION.cConnect();
            string fnme = "Select sharestype,withdrawable,SharesAcc from sharetype where sharescode='" + cboDCode.Text + "' ";
            Dr = ll.ReadDB(fnme);
            if (Dr.HasRows)
                while (Dr.Read())
                {
                    txtDSharetype.Text = Dr["SharesType"].ToString();
                    cboDCode_Change();
                }
            Dr.Close(); Dr.Dispose(); Dr = null;
        }


        protected void Recipient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Dr = new WARTECHCONNECTION.cConnect().ReadDB("Select surname + ' ' + othernames as membernames from members where memberno='" + txtRMemberNo.Text + "' ");
                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        txtRNames.Text = Dr["membernames"].ToString();
                        cboRCode_Change();
                    }
                }
                else
                {
                    txtRNames.Text = "";
                    txtRBalance.Text = "0";
                    txtRAvailable.Text = "0";
                }
                Dr.Close(); Dr.Dispose(); Dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void cboRCode_Change()
        {
            try
            {
                Dr12 = new WARTECHCONNECTION.cConnect().ReadDB("Select sharestype,SharesAcc from sharetype where sharescode='" + cboRCode.Text + "'");
                if (Dr12.HasRows)
                {
                    while (Dr12.Read())
                    {
                        txtRShareType.Text = Dr12["sharestype"].ToString();
                    }
                }
                else
                {
                    cboRCode.Text = "";
                }


                if (txtRMemberNo.Text != "")
                {
                    DR13a = new WARTECHCONNECTION.cConnect().ReadDB("if exists(select * from sharetype where sharescode='" + cboRCode.Text + "' and UsedToGuarantee=1) select S.TotalShares,isnull(sum(LG.balance),0) as CommittedShares from shares S left join LoanGuar LG ON S.memberno=LG.memberno where S.memberno='" + txtRMemberNo.Text + "' and S.sharescode='" + cboRCode.Text + "' group by LG.MEMBERNO,S.tOTALsHARES  else select S.TotalShares,0 as CommittedShares from shares S where S.memberno='" + txtRMemberNo.Text + "' and S.sharescode='" + cboRCode.Text + "'");
                    if (DR13a.HasRows)
                    {
                        while (DR13a.Read())
                        {
                            txtRBalance.Text = DR13a["TotalShares"].ToString();
                            double rbal = Convert.ToDouble(txtRBalance.Text);
                            double CommittedShares = Convert.ToDouble(DR13a["CommittedShares"]);
                            txtRAvailable.Text = (rbal - CommittedShares).ToString();
                        }
                    }
                    else
                    {
                        txtRBalance.Text = "0";
                        txtRAvailable.Text = "0";
                    }
                }
                else
                {
                    txtRBalance.Text = "0";
                    txtRAvailable.Text = "0";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void DrpShareCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect ll = new WARTECHCONNECTION.cConnect();
            string fnme = "Select sharestype,withdrawable,SharesAcc from sharetype where sharescode='" + cboRCode.Text + "' ";
            DR5 = ll.ReadDB(fnme);
            if (DR5.HasRows)
                while (DR5.Read())
                {
                    txtRShareType.Text = DR5["SharesType"].ToString();
                    cboRCode_Change();
                }
            DR5.Close(); DR5.Dispose(); DR5 = null;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtDBalance.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("Insufficient shares to transfer");
                    return;
                }
                if (Convert.ToDouble(txtTransAmount.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("Enter The Amount to Transfer");
                    txtTransAmount.Focus();
                    return;
                }
                if (Convert.ToDouble(txtTransAmount.Text) > Convert.ToDouble(txtDAvailable.Text))
                {
                    WARSOFT.WARMsgBox.Show("Transfer amount is More than the Donor's Available Share.");
                    txtTransAmount.Focus();
                    return;
                }

                if (txtRNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Recipient member is missing!");
                    return;
                }
                else if (txtRShareType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Recipient share code is not given");
                    return;
                }

                if (txtDMemberNo.Text == txtRMemberNo.Text)
                {
                    if (cboDCode.Text == cboRCode.Text)
                    {
                        WARSOFT.WARMsgBox.Show("The Donor and the recepient should not be the Same");
                        txtRMemberNo.Focus();
                        return;
                    }
                }
                NewTransaction(Convert.ToDouble(txtTransAmount.Text), Convert.ToDateTime(dtpTransDate.Text), "Share TransFer From " + txtDMemberNo.Text + " and " + txtRMemberNo.Text);

                //'Transfer from Donor
                SaveContrib(txtDMemberNo.Text, Convert.ToDateTime(dtpTransDate.Text), cboDCode.Text, Convert.ToDouble(txtTransAmount.Text) * (-1), "Non-Cash", txtDMemberNo.Text + "-" + txtRMemberNo.Text, "Non-Cash", "JV", "Transfer To " + txtRMemberNo.Text, transactionNo);

                // 'Transfer to Recepient
                SaveContrib(txtRMemberNo.Text, Convert.ToDateTime(dtpTransDate.Text), cboRCode.Text, Convert.ToDouble(txtTransAmount.Text), "Non-Cash", txtDMemberNo.Text + "-" + txtRMemberNo.Text, "Non-Cash", "JV", "Transfer From " + txtDMemberNo.Text, transactionNo);
                string DRAcc = ""; string CRAcc = "";
                Dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select SharesAcc from Sharetype where sharescode='" + cboDCode.Text + "'");
                if (Dr1.HasRows)
                {
                    while (Dr1.Read())
                    {
                        DRAcc = Dr1["SharesAcc"].ToString();
                    }
                }
                Dr1.Close(); Dr1.Dispose(); Dr1 = null;
                DR12 = new WARTECHCONNECTION.cConnect().ReadDB("select SharesAcc from Sharetype where sharescode='" + cboRCode.Text + "'");
                if (DR12.HasRows)
                {
                    while (DR12.Read())
                    {
                        CRAcc = DR12["SharesAcc"].ToString();
                    }
                }
                DR12.Close(); DR12.Dispose(); DR12 = null;
                Save_GLTRANSACTION(Convert.ToDateTime(dtpTransDate.Text), Convert.ToDouble(txtTransAmount.Text), DRAcc, CRAcc, transactionNo, txtDMemberNo.Text, Session["mimi"].ToString(), "Transfer From " + txtDMemberNo.Text, 1, 1, "JV", transactionNo);


                cboDCode_Change();
                cboRCode_Change();
                WARSOFT.WARMsgBox.Show("Share Transfer Successfull, Donor Balance Would Be " + (Convert.ToDouble(txtDBalance.Text) - Convert.ToDouble(txtTransAmount.Text)) + "");
                txtTransAmount.Text = "0";
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtDAvailable.Text = "";
            txtDBalance.Text = "";
            txtDMemberNo.Text = "";
            txtDNames.Text = "";
            txtDSharetype.Text = "";
            txtRAvailable.Text = "";
            txtRBalance.Text = "";
            txtRemarks.Text = "";
            txtRMemberNo.Text = "";
            txtRNames.Text = "";
            txtRShareType.Text = "";
            txtTransAmount.Text = "";
            cboDCode.Text = "";
            cboRCode.Text = "";
            
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRAcc, string CRAcc, string DocumentNo, string Source, string auditid, string TransDescription, int CashBook, int doc_posted, string chequeno, string transactionNo)
        {
            try
            {
                new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate+ "'," + Amount + ",'" + DRAcc + "','" + CRAcc + "','" + DocumentNo + "','" + Source + "','" + auditid + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','BOSA'");
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void SaveContrib(string memberno, DateTime ContrDate, string SHARECode, double contrAmount, string BankAcc, string Receiptno, string chequeno, string transby, string remarks, string transactionNo)
        {
            try
            {
                int RefNo = 0;
                double TotalShares = 0;
                DateTime lastdate = DateTime.Today;
                DateTime thisDate = ContrDate;
                DR = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(contrib.amount),0)amount  from contrib where contrib.memberno='" + memberno + "' and contrib.sharescode='" + SHARECode + "' group by contrib.memberno");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        TotalShares = Convert.ToDouble(DR["amount"]);
                    }
                }

                TotalShares = TotalShares + contrAmount;

                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into Contrib(memberno,contrdate,refno,Amount,sharebal,transby,ChequeNo,receiptno,remarks,auditid,sharescode,transactionno) Values('" + memberno + "','" + ContrDate + "'," + RefNo + "," + contrAmount + "," + TotalShares + ",'" + transby + "','" + chequeno + "','" + Receiptno + "','" + remarks + "','" + Session["mimi"].ToString() + "','" + SHARECode + "','" + transactionNo + "')");

                DR0 = new WARTECHCONNECTION.cConnect().ReadDB("select IsNull(SharesAcc,0)SharesAcc from sharetype where sharescode='" + SHARECode + "'");
                if (DR0.HasRows)
                {
                    while (DR0.Read())
                    {
                        shareAcc = DR0["SharesAcc"].ToString();
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The Gl Control Account for this scheme(" + SHARECode.Trim() + ") is not set");
                    return;
                }
                DR0.Close(); DR0.Dispose(); DR0 = null;
                Save_GLTRANSACTION(ContrDate, contrAmount, BankAcc, shareAcc, Receiptno, memberno, Session["mimi"].ToString(), remarks, 1, 1, Receiptno, transactionNo);

                shareAcc = BankAcc;

                if (ContrDate < lastdate)
                {
                    RefreshContrib(memberno, SHARECode);
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; ; }
        }

        private void RefreshContrib(string memberno, string SHARECode)
        {
            double conId = 0; double conAmount = 0; double TotShares = 0; string prevmemberno = ""; string prevsharescode = "";

            DR13 = new WARTECHCONNECTION.cConnect().ReadDB("Select memberno,sharescode,contrdate,amount,id from contrib where memberno='" + memberno + "' and sharescode='" + SHARECode + "' order by sharescode,contrdate,Refno");
            if (DR13.HasRows)
            {
                while (DR13.Read())
                {
                    Amount = Convert.ToDouble(DR13["Amount"]);
                    conId = Convert.ToDouble(DR13["Id"].ToString());
                    TotShares = TotShares + Amount;
                    new WARTECHCONNECTION.cConnect().WriteDB("Update contrib set shareBal=" + TotShares + " where id=" + conId + "");
                }
            }
            DR13.Close(); DR13.Dispose(); DR13 = null;
        }
        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description)
        {
            // 'save TransactionNo
            DateTime TimeNow = DateTime.Now;
            TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            //transactionNo = Session["mimi"].ToString()+transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            transactionNo = (Session["mimi"].ToString() + TimeNow.Day.ToString() + TimeNow.Month.ToString() + TimeNow.Year.ToString() + TimeNow.Minute.ToString() + TimeNow.Second.ToString()).ToString();
            transactionTotal = AmountPaid;

            SaveTransaction(transactionNo, transactionTotal, Session["mimi"].ToString(), TransDate, Description);
        }

        private void SaveTransaction(string transactionNo, double transactionTotal, string auditid, DateTime TransDate, string Description)
        {
            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription)Values('" + transactionNo + "'," + Amount + ",'" + auditid + "',Convert(Varchar(10), '" + TransDate + "', 101),'" + Description + "')");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtDMemberNo.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Donor member number is required");
                return;
            }

            //if( isWithdrawable = 1 )
            //{
            //    WARSOFT.WARMsgBox.Show(""+ txtDSharetype.Text + " Is not withdrawable!");
            //        return;
            //    }

            if (Convert.ToDouble(txtTransAmount.Text) <= 0)
            {
                WARSOFT.WARMsgBox.Show("Enter The Amount to withdraw");
                txtTransAmount.Focus();
                return;
            }
            if (Convert.ToDouble(txtTransAmount.Text) > Convert.ToDouble(txtDAvailable.Text))
            {
                WARSOFT.WARMsgBox.Show("Withdraw amount is More than the Available Share");
                txtTransAmount.Focus();
                return;
            }
            NewTransaction(Convert.ToDouble(txtTransAmount.Text), Convert.ToDateTime(dtpTransDate.Text), "Share Withdrawal");

            new WARTECHCONNECTION.cConnect().WriteDB("if not exists (select * from shareWithdrawal where memberno='" + txtDMemberNo + "' and withdrawn=0) Insert into shareWithdrawal(memberno,sharescode,withdrawaldate,amount,auditId,Purpose,withdrawn,transactionno) Values('" + txtDMemberNo.Text + "','" + cboDCode.Text + "','" + dtpTransDate.Text + "'," + txtTransAmount.Text + ",'" + User + "','" + txtRemarks.Text + "',0,'" + transactionNo + "')");

            new WARTECHCONNECTION.cConnect().WriteDB("update members set withdrawn=1 where memberno='" + txtDMemberNo.Text + "'");


            WARSOFT.WARMsgBox.Show("Member Withdrawn permanently");
            return;
            //================================================================================

            try
            {
                string isWithdrawable = "";
                if (txtDMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("");
                    return;
                }
                DR0 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT Withdrawable FROM sharetype where sharescode='" + cboDCode.Text + "'");
                if(DR0.HasRows)
                {
                    while (DR0.Read())
                    {
                        isWithdrawable = DR0["Withdrawable"].ToString();
                    }
                }
                DR0.Close(); DR0.Dispose(); DR0 = null;
                if (isWithdrawable == "1")
                {
                    WARSOFT.WARMsgBox.Show("" + txtDSharetype.Text + " Is not withdrawable!");
                    return;
                }
                if (Convert.ToDouble(txtTransAmount.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("Enter The Amount to withdraw");
                    txtTransAmount.Focus();
                    return;
                }
                if (Convert.ToDouble(txtTransAmount.Text) > Convert.ToDouble(txtDAvailable.Text))
                {
                    WARSOFT.WARMsgBox.Show("Withdraw amount is More than the Available Share");
                    txtTransAmount.Focus();
                    // txtTransAmount = SelectAllText(txtTransAmount)
                }
                NewTransaction(Convert.ToDouble(txtTransAmount.Text), Convert.ToDateTime(dtpTransDate.Text), "Share Withdrawal");

                new WARTECHCONNECTION.cConnect().WriteDB("if not exists (select * from shareWithdrawal where memberno='" + txtDMemberNo + "' and withdrawn=0) Insert into shareWithdrawal(memberno,sharescode,withdrawaldate,amount,auditId,Purpose,withdrawn,transactionno) Values('" + txtDMemberNo.Text + "','" + cboDCode.Text + "','" + dtpTransDate.Text + "'," + txtTransAmount.Text + ",'" + Session["mimi"].ToString() + "','" + txtRemarks.Text + "',0,'" + transactionNo + "')");
                WARSOFT.WARMsgBox.Show("Share Withdrawal has been successfully Booked, you can now proceed to pay");
                return;
                new WARTECHCONNECTION.cConnect().WriteDB("update members set withdrawn=1 where memberno='" + txtDMemberNo.Text + "'");
                new WARTECHCONNECTION.cConnect().WriteDB("update Withdrawalnotice set withdrawdate='" + dtpTransDate.Text + "',transactionno='" + transactionNo + "' where memberno='" + txtDMemberNo.Text + "'");

                WARSOFT.WARMsgBox.Show("Member Withdrawn permanently");

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

    }
}