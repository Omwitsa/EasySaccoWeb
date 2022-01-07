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

namespace USACBOSA.ManagementAdmin
{
    public partial class SharesInquiry : System.Web.UI.Page
    {
        SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, dr6;
        SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadShareCodes();
            }
            //Label14.Visible = false;
        }

        private void LoadShareCodes()
        {

            cboShareCode.Items.Clear();
            cboShareCode.Items.Add("");
            try
            {
                //cboShareCode.Items.Clear();
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB("select sharescode from ShareType order by ismainshares desc");
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        cboShareCode.Items.Add(dr["sharescode"].ToString().Trim());
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        }
        protected void btnRefreshContribution_Click(object sender, EventArgs e)
        {

        }
        protected void cboShareCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                WARTECHCONNECTION.cConnect oSaccoShare = new WARTECHCONNECTION.cConnect();
                dr3 = oSaccoShare.ReadDB("Select Sharestype from sharetype where sharescode='" + cboShareCode.Text + "'");
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        txtsharescode.Text = dr3["Sharestype"].ToString();
                    }
                }
                dr3.Close(); dr3.Dispose(); dr3 = null; oSaccoShare.Dispose(); oSaccoShare = null;
                LoadShares();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadShares()
        {
            try
            {
                WARTECHCONNECTION.cConnect oSaccoShare7 = new WARTECHCONNECTION.cConnect();
                dr5 = oSaccoShare7.ReadDB("Select c.contrdate [Contr Date],c.Amount,c.ChequeNo [Cheque No],c.ReceiptNo [Receipt No],c.Remarks [Description],c.TransBy [Trans By],sharebal [Share Bal] from contrib c where c.memberno='" + txtMemberNo.Text + "' AND c.SHARESCODE='" + cboShareCode.Text + "'order by c.contrdate DESC,c.id desc");
                if (dr5.HasRows)
                {
                    GridView1.DataSource = dr5;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                dr5.Close(); dr5.Dispose(); dr5 = null; oSaccoShare7.Dispose(); oSaccoShare7 = null;

                GetTotalShares();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        }

        private void GetTotalShares()
        {
            try
            {
                double TotalShares = 0;
                WARTECHCONNECTION.cConnect oSaccoShare = new WARTECHCONNECTION.cConnect();
                dr3 = oSaccoShare.ReadDB("Select c.contrdate,c.amount,c.ChequeNo,c.ReceiptNo,c.Remarks,c.TransBy,sharebal,id from contrib c where c.memberno='" + txtMemberNo.Text + "' AND c.SHARESCODE='" + cboShareCode.Text + "'order by c.contrdate DESC,c.id desc");
                if (dr3.HasRows)
                    while (dr3.Read())
                    {
                        double Amount = Convert.ToDouble(dr3["amount"].ToString());
                        TotalShares = TotalShares + Amount;
                        txtTotShares.Text = TotalShares.ToString();
                    }
                dr3.Close(); dr3.Dispose(); dr3 = null; oSaccoShare.Dispose(); oSaccoShare = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            LoadMemberDetails();
        }
        private void LoadMemberDetails()
        {
            GridView1.Visible = false;
            cboShareCode.Text = "";
            try
            {
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB("select  m.surname,m.othernames,M.CompanyCode , m.Dormant, m.Withdrawn, m.Archived,isnull(sum(s.TotalShares),0) as TotalShares from members m left outer join vwmembershares s on m.memberno=s.memberno where m.memberno='" + txtMemberNo.Text + "' Group by  m.memberno,m.surname,m.othernames,M.CompanyCode , m.Dormant, m.Withdrawn, m.Archived");
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtNames.Text = dr["surname"].ToString() + dr["othernames"].ToString();
                        //  txtTShares.Text = dr["TotalShares"].ToString();
                        try
                        {
                            WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                            string memebr = "SELECT ISNULL(SUM(Amount),0)Amount from Contrib where sharescode!='E' and MemberNo='" + txtMemberNo.Text + "'";
                            dr6 = cooon.ReadDB(memebr);
                            if (dr6.HasRows)
                            {
                                while (dr6.Read())
                                {

                                    txtTShares.Text = dr6["Amount"].ToString();
                                }
                            }
                            dr6.Close(); dr6.Dispose(); cooon.Dispose(); cooon = null;
                        }
                        catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

                        WARTECHCONNECTION.cConnect LoanBal = new WARTECHCONNECTION.cConnect();
                        dr1 = LoanBal.ReadDB("SELECT MemberNo, COUNT(MemberNo) AS LoanCount, ISNULL(SUM(Balance), 0) AS Balance FROM  VWMEMBERLOANS WHERE (MemberNo = '" + txtMemberNo.Text + "') GROUP BY MemberNo");
                        if (dr1.HasRows)
                            while (dr1.Read())
                            {
                                txtOLoanBal.Text = dr1["Balance"].ToString();
                                txtOLoans.Text = dr1["LoanCount"].ToString();
                            }
                        dr1.Close(); dr1.Dispose(); dr1 = null; LoanBal.Dispose(); LoanBal = null;
                        //  getMemberLoans();
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtMemberNo.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Provide MemberNo!");
            }
            else
            {
                Session["Memberno"] = txtMemberNo.Text;
                Response.Redirect("~/Reports/statements.aspx", false);
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            double id, Amount = 0, TotShares = 0;
            String memberno, sharescode;
            DateTime Today;
            int count = 0;
            double sharebal = 0;
            if (CheckBox1.Checked == true)
            {
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select memberno,sharescode,contrdate,amount,id from contrib where memberno='" + txtMemberNo.Text + "' order by sharescode,contrdate,Refno");//
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        id = Convert.ToDouble(dr2["id"]);
                        sharescode = dr2["sharescode"].ToString();
                        memberno = dr2["memberno"].ToString();
                        Amount = 0;
                        TotShares = 0;
                        string select = "set dateformat dmy if exists (select  MemberNo, TotalShares, TransDate, LastDivDate, AuditID, AuditTime, loanbal, sharescode, Statementshares, Initshares from shares where memberno ='" + memberno + "' and sharescode='" + sharescode + "') UPDATE SHARES SET totalshares=" + TotShares + " where memberno='" + memberno + "' and sharescode='" + sharescode + "'" + " else insert into shares(memberno,sharescode,totalshares,transdate,auditId) values('" + memberno + "','" + sharescode + "'," + TotShares + ",'" + DateTime.Today + "','Trigger')";
                        new WARTECHCONNECTION.cConnect().WriteDB(select);
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null;
                dr5 = new WARTECHCONNECTION.cConnect().ReadDB("Select memberno,sharescode,contrdate,amount,id from contrib where memberno='" + txtMemberNo.Text + "' order by sharescode,contrdate,Refno");//
                if (dr5.HasRows)
                    while (dr5.Read())
                    {
                        id = Convert.ToDouble(dr5["id"]);
                        sharescode = dr5["sharescode"].ToString();
                        memberno = dr5["memberno"].ToString();
                        Amount = Convert.ToDouble(dr5["amount"]);
                        dr4 = new WARTECHCONNECTION.cConnect().ReadDB("Select totalshares from shares where memberno='" + txtMemberNo.Text + "' and sharescode='" + sharescode + "'");//
                        if (dr4.HasRows)
                            while (dr4.Read())
                            {
                                sharebal = Convert.ToDouble(dr4["totalshares"]);
                            }
                        dr4.Close(); dr4.Dispose(); dr4 = null;
                        TotShares = sharebal + Amount;
                        string update = "Update contrib set shareBal=" + TotShares + " where id=" + id + "";
                        new WARTECHCONNECTION.cConnect().WriteDB(update);
                        string select5 = "if exists (select  MemberNo, TotalShares, TransDate, LastDivDate, AuditID, AuditTime, loanbal, sharescode, Statementshares, Initshares from shares where memberno ='" + memberno + "' and sharescode='" + sharescode + "') UPDATE SHARES SET totalshares=" + TotShares + " where memberno='" + memberno + "' and sharescode='" + sharescode + "'" + " else insert into shares(memberno,sharescode,totalshares,transdate,auditId) values('" + memberno + "','" + sharescode + "'," + TotShares + ",'" + DateTime.Today + "','Trigger')";
                        new WARTECHCONNECTION.cConnect().WriteDB(select5);
                        WARSOFT.WARMsgBox.Show("Shares has been Refreshed successfully");
                    }
                dr5.Close(); dr5.Dispose(); dr5 = null;
            }
            else
            {
                //dr3 = new WARTECHCONNECTION.cConnect().ReadDB("Select memberno,sharescode,contrdate,amount,id from contrib order by memberno,sharescode,contrdate,Refno");
                //if (dr3.HasRows)
                //    while (dr3.Read())
                //    {
                //        id = Convert.ToDouble(dr3["id"]);
                //        sharescode = dr3["sharescode"].ToString();
                //        memberno = dr3["memberno"].ToString();
                //        Amount = 0;
                //        TotShares = 0;
                //        string select = "set dateformat dmy if exists (select  MemberNo, TotalShares, TransDate, LastDivDate, AuditID, AuditTime, loanbal, sharescode, Statementshares, Initshares from shares where memberno ='" + memberno + "' and sharescode='" + sharescode + "') UPDATE SHARES SET totalshares=" + TotShares + " where memberno='" + memberno + "' and sharescode='" + sharescode + "'" + " else insert into shares(memberno,sharescode,totalshares,transdate,auditId) values('" + memberno + "','" + sharescode + "'," + TotShares + ",'" + DateTime.Today + "','Trigger')";
                //        new WARTECHCONNECTION.cConnect().WriteDB(select);
                //        Amount = Convert.ToDouble(dr3["amount"]);
                //        TotShares = TotShares + Amount;
                //        string update = "Update contrib set shareBal=" + TotShares + " where id=" + id + "";
                //        new WARTECHCONNECTION.cConnect().WriteDB(update);
                //        string select5 = "if exists (select  MemberNo, TotalShares, TransDate, LastDivDate, AuditID, AuditTime, loanbal, sharescode, Statementshares, Initshares from shares where memberno ='" + memberno + "' and sharescode='" + sharescode + "') UPDATE SHARES SET totalshares=" + TotShares + " where memberno='" + memberno + "' and sharescode='" + sharescode + "'" + " else insert into shares(memberno,sharescode,totalshares,transdate,auditId) values('" + memberno + "','" + sharescode + "'," + TotShares + ",'" + DateTime.Today + "','Trigger')";
                //        new WARTECHCONNECTION.cConnect().WriteDB(select5);
                //        count++;
                //        Label14.Visible = true;
                //        Label14.Text = count.ToString();  
                //    }
                //dr3.Close(); dr3.Dispose(); dr3 = null;
                //WARSOFT.WARMsgBox.Show("Shares has been Refreshed successfully");
            }

        }
    }
}