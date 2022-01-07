using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;

namespace USACBOSA.SysAdmin
{
    public partial class ShareTypes : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr,dr1, DR, DR1, DR2, DR3, DR4, DR5, DR6, DR7;
        System.Data.SqlClient.SqlDataAdapter da;
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
                bindShareslist();
                LoadShare();
            }
        }

        private void LoadShare()
        {
            cboShareCode.Items.Clear();
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select sharescode from sharetype order by sharescode asc");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboShareCode.Items.Add(dr["sharescode"].ToString());
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        private void bindShareslist()
        {
            try
            {
                string sehhh = "select SharesCode [Shares Code],SharesType [Shares Type],SharesAcc [Shares Acc.] from sharetype ORDER BY AuditTime DESC";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
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

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string saved = "";
                if (txtShareCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the share code");
                    txtShareCode.Focus();
                    return;
                }
                if (txtAccName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("ShareType GL Account has not been specified");
                    return;
                }
                if (txtminAmount.Text == "")
                {
                    txtminAmount.Text = "0";
                }

                string sharesCode = txtShareCode.Text.Trim();
                string sharestype = txtShareType.Text.Trim().Replace("'", "");
                string USEDTOGUARANTEE = "";
                string isMainShares = "";
                string usedtooffset = "";
                string iswithdrawable = "";
                if (ChkGuarantors.Checked == true)
                {
                    USEDTOGUARANTEE = "1";
                }
                else
                {
                    USEDTOGUARANTEE = "0";
                }

                if (chkIsMainShares.Checked == true)
                {
                    isMainShares = "1";
                }
                else
                {
                    isMainShares = "0";
                }
                if (chkOffset.Checked == true)
                {
                    usedtooffset = "1";
                }
                else
                {
                    usedtooffset = "0";
                }
                if (chkWithdrawable.Checked == true)
                {
                    iswithdrawable = "1";
                }
                else
                {
                    iswithdrawable = "0";
                }
                string SharesAcc = txtAcc.Text;
                string LowerLimit = txtAbove.Text;
                string ElseRatio = txtElse.Text;
                string LoanToShareRatio = "";
                if (txtLoanToShare.Text == "")
                {
                    LoanToShareRatio = "3";
                }
                else
                {
                    LoanToShareRatio = txtLoanToShare.Text;
                }
                string auditid = Session["mimi"].ToString();
                DateTime audittime = DateTime.Now;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select * from sharetype where sharescode='" + txtShareCode.Text + "'");
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("ShareCode " + "" + sharesCode + " " + " Already exists");
                    // new WARTECHCONNECTION.cConnect().WriteDB("UPDATE ShareType SharesCode set='" + sharesCode + "',SharesType,SharesAcc,LoanToShareRatio,AuditID,AuditTime,IsMainShares,UsedToGuarantee,UsedToOffset,Withdrawable,MinAmount,LowerLimit,ElseRatio)values(,'" + sharestype + "','" + SharesAcc + "','" + LoanToShareRatio + "','" + auditid + "','" + audittime + "','" + isMainShares + "','" + USEDTOGUARANTEE + "','" + usedtooffset + "','" + iswithdrawable + "','" + txtminAmount.Text + "','" + LowerLimit + "','" + ElseRatio + "')"); 
                    return;
                }
                else
                {
                    string issetdata = "Insert into ShareType(SharesCode,SharesType,SharesAcc,LoanToShareRatio,AuditID,AuditTime,IsMainShares,UsedToGuarantee,UsedToOffset,Withdrawable,MinAmount,LowerLimit,ElseRatio)values('" + sharesCode + "','" + sharestype + "','" + SharesAcc + "','" + LoanToShareRatio + "','" + Session["mimi"].ToString() + "','" + System.DateTime.Now.ToString("HH:mm:ss") + "','" + isMainShares + "','" + USEDTOGUARANTEE + "','" + usedtooffset + "','" + iswithdrawable + "','" + txtminAmount.Text + "','" + LowerLimit + "','" + ElseRatio + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(issetdata);
                    clearTexts();
                    bindShareslist();
                    saved = "OK";
                }
                dr.Close(); dr.Dispose(); dr = null;
                if (saved == "OK")
                {
                    WARSOFT.WARMsgBox.Show("Record Saved sucessfully");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtAccName.Text = "";
            txtAcc.Text = "";
            txtElse.Text = "";
            txtLoanToShare.Text = "";
            txtminAmount.Text = "";
            txtShareCode.Text = "";
            txtShareType.Text = "";
            txtvalue.Text = "";
            cboSearchBy.Text = "";
            ChkGuarantors.Checked = false;
            chkIsMainShares.Checked = false;
            chkOffset.Checked = false;
            chkWithdrawable.Checked = false;
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAcc.Text = GridView2.SelectedRow.Cells[1].Text;
            txtAccName.Text = GridView2.SelectedRow.Cells[2].Text;
            GridView2.Visible = false;
            Label14.Visible = false;
            cboSearchBy.Visible = false;
            btnFindSearch.Visible = false;
            txtvalue.Visible = false;
            bindShareslist();
        }

        private void getsharetypedetails(string sharecode)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select SharesCode,SharesType,SharesAcc,PlacePeriod,LoanToShareRatio,Issharecapital,Interest,MaxAmount,Guarantor,AuditID,AuditTime,accno,shareboost,IsMainShares,UsedToGuarantee,ContraAcc,UsedToOffset,Withdrawable,loanquaranto,Priority,MinAmount,PPAcc,LowerLimit,ElseRatio from sharetype where sharescode='"+cboShareCode.Text.Trim()+"'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtShareType.Text = dr["SharesType"].ToString().Trim();
                    txtLoanToShare.Text = dr["LoanToShareRatio"].ToString().Trim();
                    txtAcc.Text = dr["SharesAcc"].ToString().Trim();
                    dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP where AccNo='" + txtAcc.Text.Trim() + "'");
                     if (dr1.HasRows)
                     {
                         while (dr1.Read())
                         {
                             txtAccName.Text = dr1["GLAccName"].ToString();
                         }
                     }
                     dr1.Close(); dr1.Dispose(); dr1 = null;
                    bool ismainshare = Convert.ToBoolean(dr["IsMainShares"].ToString().Trim());
                    if (ismainshare == false)
                    {
                        chkIsMainShares.Checked = false;
                    }
                    else
                    {
                        chkIsMainShares.Checked = true;
                    }
                    bool usedtoguarantee = Convert.ToBoolean(dr["UsedToGuarantee"].ToString().Trim());
                    if (usedtoguarantee==false)
                    {
                        ChkGuarantors.Checked = false;
                    }
                    else
                    {
                        ChkGuarantors.Checked = true;
                    }
                    bool withdraww = Convert.ToBoolean( dr["Withdrawable"].ToString().Trim());
                    if (withdraww==false)
                    {
                        chkWithdrawable.Checked = false;
                    }
                    else
                    {
                        chkWithdrawable.Checked = true;
                    }

                    bool usedtoofset = Convert.ToBoolean(dr["UsedToOffset"].ToString().Trim());
                    if (usedtoofset==false)
                    {
                        chkOffset.Checked = false;
                    }
                    else
                    {
                        chkOffset.Checked = true;
                    }
                }
            }
            dr.Close(); dr.Dispose();dr = null;
        }

        protected void btnFindGlAcc_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();
                cboSearchBy.Visible = true;
                btnFindSearch.Visible = true;
                txtvalue.Visible = true;
                GridView1.Visible = false;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSearchBy.Text == "Account Name")
                {
                    string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP Where GLAccName Like '%" + txtvalue.Text + "%' Order By GLAccName";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                    if (dr.HasRows)
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                        GridView1.Visible = false;
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No such record found, try another option!");
                        txtvalue.Text = "";
                        txtvalue.Focus();
                        return;
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
                else if (cboSearchBy.Text == "Account No")
                {
                    string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP Where AccNo Like '%" + txtvalue.Text + "%' Order By AccNo";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                    if (dr.HasRows)
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                        GridView1.Visible = false;
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No such record found, try another option!");
                        txtvalue.Text = "";
                        txtvalue.Focus();
                        return;
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtShareCode.Text = GridView1.SelectedRow.Cells[1].Text;
                this.txtShareType.Text = GridView1.SelectedRow.Cells[2].Text;
                cboShareCode.Text = txtShareCode.Text;
                getsharetypedetails(txtShareCode.Text);
                GridView2.Visible = false;
                bindShareslist();
              
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtShareCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string saved = "";
                if (txtShareCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("ShareType required");
                    return;
                }
                if (txtAccName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("ShareType GL Account has not been specified");
                    return;
                }
                else
                {
                    string USEDTOGUARANTEE = "";
                    string isMainShares = "";
                    string usedtooffset = "";
                    string iswithdrawable = "";
                    if (ChkGuarantors.Checked == true)
                    {
                        USEDTOGUARANTEE = "1";
                    }
                    else
                    {
                        USEDTOGUARANTEE = "0";
                    }

                    if (chkIsMainShares.Checked == true)
                    {
                        isMainShares = "1";
                    }
                    else
                    {
                        isMainShares = "0";
                    }
                    if (chkOffset.Checked == true)
                    {
                        usedtooffset = "1";
                    }
                    else
                    {
                        usedtooffset = "0";
                    }
                    if (chkWithdrawable.Checked == true)
                    {
                        iswithdrawable = "1";
                    }
                    else
                    {
                        iswithdrawable = "0";
                    }
                    string updait = "set dateformat dmy UPDATE ShareType  set SharesType='" + txtShareType.Text.Trim() + "',SharesAcc='" + txtAcc.Text.Trim() + "',LoanToShareRatio='" + txtLoanToShare.Text.Trim() + "',AuditID='" + Session["mimi"].ToString() + "',AuditTime='" + System.DateTime.Now.ToString("hh:MM:ss") + "',MinAmount='" + txtminAmount.Text + "',IsMainShares='" + isMainShares + "',UsedToGuarantee='" + USEDTOGUARANTEE + "',UsedToOffset='" + usedtooffset + "',Withdrawable='" + iswithdrawable + "' where SharesCode='" + cboShareCode.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(updait);
                    bindShareslist();
                    clearTexts();
                    saved = "OK";
                }
                if (saved == "OK")
                {
                    WARSOFT.WARMsgBox.Show("Share type Updated Successfully");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtShareCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("ShareType required");
                }
                else
                {
                    string remov = "Delete from ShareType where SharesCode='" + cboShareCode.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(remov);
                    clearTexts();
                    LoadShare();
                    WARSOFT.WARMsgBox.Show("Share type Deleted Successfully");
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Changepwd/Bosa.aspx");
        }

        protected void cboShareCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtShareCode.Text=cboShareCode.Text.Trim();
            getsharetypedetails(cboShareCode.Text.Trim());
        }
    }
}