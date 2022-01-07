using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.offsetting
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

        private void NewTransaction(double transactionTotal, DateTime dateTime, string p)
        {

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
                        if (subType == "SHARE")
                        {
                            cboShareType.Enabled = true;
                            cboShareType.Focus();
                            cboLoanno.Enabled = false;
                        }
                        else if (subType == "LOAN" || subType == "INTEREST")
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
            string readdata = "select vno,transdate,naration,(sum(amount)/2)Amount from journals where posted =0 group by vno,transdate,naration";
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