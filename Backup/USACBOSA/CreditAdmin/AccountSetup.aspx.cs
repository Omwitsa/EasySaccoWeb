using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.CreditAdmin
{
    public partial class AccountSetup : System.Web.UI.Page
    {
         
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, dr6;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            dtpTransDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                LoadGlSetups();
            }
        }

        private void LoadGlSetups()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("select accno [Account Number],Glaccname [Account Name],GlAccMainGroup [Account Group],Glaccgroup [Account Sub Group] from glsetup order by accno");
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtaccno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("AccountNo can not be blank");
                    return;
                }
                if (cboAccType.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Account type cannot is not Optional, please enter before you proceed");
                    return;
                }
                if (cboSubType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Account Sub Type cannot is not Optional, please enter before you proceed");
                    return;
                }
                if (cbonormalbalance.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Normal Balance for this Account");
                    return;
                }
                if (cboaccoounttype.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Type for this Account");
                    return;
                }
                if (cboaccountgroup.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Sub Group for this Account");
                    return;
                }
                if (cboAccGroup.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Group for this Account");
                    return;
                }

                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select * From GLSETUP where AccNo='" + txtaccno.Text + "'");
                if (dr1.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The Gl Accno is already existing. Did you intend to change its details?");
                    return;
                }
                else
                {
                    string Accno = txtaccno.Text;
                    string GlAccName = txtaccname.Text;
                    string Glacctype = "";
                    if (cboaccoounttype.Text.Trim() != "")
                    {
                        Glacctype = cboaccoounttype.Text;
                    }
                    string GLAccGroup = "";
                    if (cboaccountgroup.Text.Trim() != "")
                    {
                        GLAccGroup = cboaccountgroup.Text;
                    }
                    string GlAccMainGroup = "";
                    if (cboAccGroup.Text.Trim() != "")
                    {
                        GlAccMainGroup = cboAccGroup.Text;
                    }
                    string NormalBal = "";
                    if (cbonormalbalance.Text.Trim() != "")
                    {
                        NormalBal = cbonormalbalance.Text;
                    }
                    double OpeningBal = 0;
                    if (txtOpeningBalance.Text.Trim() != "")
                    {
                        OpeningBal = Convert.ToDouble(txtOpeningBalance.Text);
                    }
                    string TransDate = dtpTransDate.Text;
                    string Curr_Code = cbocurrency.Text;
                    string AuditOrg = "";
                    string auditid = Session["mimi"].ToString();
                    DateTime AuditDate = DateTime.Today;
                    string Curr = "";
                    if (cbocurrency.Text.Trim() != "")
                    {
                        Curr = cbocurrency.Text;
                    }
                    string Actuals = "0";
                    string Budgetted = "0";
                    string IsSubLedger = "0";
                    string Type = cboAccType.Text;
                    string AccCategory = "";

                    if (cboacccategory.Text.Trim() != "")
                    {
                        AccCategory = cboacccategory.Text;
                    }
                    double NewGLOpeningBal = 0;
                    if (txtOpeningBalance.Text.Trim() != "")
                    {
                        NewGLOpeningBal = Convert.ToDouble(txtOpeningBalance.Text);
                    }
                    string newglopeningbaldate = dtpTransDate.Text;
                    string subType = cboSubType.Text;
                    string isSuspense = "";
                    string REarningsAcc = "";
                    string SuspenseAcc = "";
                    if (chkSuspense.Checked == true)
                    {
                        isSuspense = "0";
                        SuspenseAcc = txtaccno.Text;
                    }
                    else
                    {
                        isSuspense = "1";
                    }
                    string isREarning = "";
                    if (chkRetainedEarning.Checked == true)
                    {
                        isREarning = "0";
                    }
                    else
                    {
                        isREarning = "1";
                        REarningsAcc = txtaccno.Text;
                    }
                    string todbb = "set dateformat dmy insert into glsetup(Glcode,Glaccname,accno,Glacctype,GlAccMainGroup,Glaccgroup,Normalbal,auditid,auditdate,curr,transdate,acccategory,Type,OpeningBal,NewGlOpeningBal,SubType,isSuspense,isREarning)values('" + Accno + "','" + GlAccName + "','" + Accno + "','" + Glacctype + "','" + GlAccMainGroup + "','" + GLAccGroup + "','" + NormalBal + "','" + auditid + "','" + AuditDate + "','" + Curr + "','" + TransDate + "','" + AccCategory + "','" + Type + "','" + OpeningBal + "','" + NewGLOpeningBal + "','" + subType + "','" + isSuspense + "','" + isREarning + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(todbb);
                    WARSOFT.WARMsgBox.Show("Record Saved Successfully");
                    LoadGlSetups();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtaccno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("AccountNo can not be blank");
                    return;
                }
                if (cboAccType.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Account type cannot is not Optional, please enter before you proceed");
                    return;
                }
                if (cboSubType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Account Sub Type cannot is not Optional, please enter before you proceed");
                    return;
                }
                if (cbonormalbalance.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Normal Balance for this Account");
                    return;
                }
                if (cboaccoounttype.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Type for this Account");
                    return;
                }
                if (cboaccountgroup.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Sub Group for this Account");
                    return;
                }
                if (cboAccGroup.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Indicate the Account Group for this Account");
                    return;
                }

                string Accno = txtaccno.Text;
                string GlAccName = txtaccname.Text;
                string Glacctype = "";
                if (cboaccoounttype.Text.Trim() != "")
                {
                    Glacctype = cboaccoounttype.Text;
                }
                string GLAccGroup = "";
                if (cboaccountgroup.Text.Trim() != "")
                {
                    GLAccGroup = cboaccountgroup.Text;
                }
                string GlAccMainGroup = "";
                if (cboAccGroup.Text.Trim() != "")
                {
                    GlAccMainGroup = cboAccGroup.Text;
                }
                string NormalBal = "";
                if (cbonormalbalance.Text.Trim() != "")
                {
                    NormalBal = cbonormalbalance.Text;
                }
                double OpeningBal = 0;
                if (txtOpeningBalance.Text.Trim() != "")
                {
                    OpeningBal = Convert.ToDouble(txtOpeningBalance.Text);
                }
                //string Bal = !CurrentBal;
                //string CurrentBal = !CurrentBal;
                //string Main = !Main;
                string TransDate = dtpTransDate.Text;
                string Curr_Code = cbocurrency.Text;
                string AuditOrg = "";
                string auditid = Session["mimi"].ToString();
                DateTime AuditDate = DateTime.Today;
                string Curr = "";
                if (cbocurrency.Text.Trim() != "")
                {
                    Curr = cbocurrency.Text;
                }
                string Actuals = "0";
                string Budgetted = "0";
                string IsSubLedger = "0";
                string Type = cboAccType.Text;
                string AccCategory = "";

                if (cboacccategory.Text.Trim() != "")
                {
                    AccCategory = cboacccategory.Text;
                }
                double NewGLOpeningBal = 0;
                if (txtOpeningBalance.Text.Trim() != "")
                {
                    NewGLOpeningBal = Convert.ToDouble(txtOpeningBalance.Text);
                }
                string newglopeningbaldate = dtpTransDate.Text;
                string subType = cboSubType.Text;
                string isSuspense = "";
                string REarningsAcc = "";
                string SuspenseAcc = "";
                if (chkSuspense.Checked == true)
                {
                    isSuspense = "0";
                    SuspenseAcc = txtaccno.Text;
                }
                else
                {
                    isSuspense = "1";
                }
                string isREarning = "";
                if (chkRetainedEarning.Checked == true)
                {
                    isREarning = "0";
                }
                else
                {
                    isREarning = "1";
                    REarningsAcc = txtaccno.Text;
                }
                string todbb = "set dateformat dmy update glsetup set Glaccname='" + GlAccName + "',Glacctype='" + Glacctype + "',GlAccMainGroup='" + GlAccMainGroup + "',Glaccgroup='" + GLAccGroup + "',Normalbal='" + NormalBal + "',auditid='" + auditid + "',auditdate='" + AuditDate + "',curr='" + Curr + "',transdate='" + TransDate + "',acccategory='" + AccCategory + "',Type='" + Type + "',OpeningBal='" + OpeningBal + "',NewGlOpeningBal='" + NewGLOpeningBal + "',SubType='" + subType + "',isSuspense='" + isSuspense + "',isREarning='" + isREarning + "' where accno='"+txtaccno.Text+"'";
                new WARTECHCONNECTION.cConnect().WriteDB(todbb);
                LoadGlSetups();
                WARSOFT.WARMsgBox.Show("Record updated Successfully");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
            String delete = "set dateformat ymd delete from GLSETUP  where accno='" + txtaccno.Text + "'";
            new WARTECHCONNECTION.cConnect().WriteDB(delete);
            WARSOFT.WARMsgBox.Show("Deleted");
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboaccoounttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboAccGroup.Items.Clear();
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select distinct accgroup from category where mainAccount='" + cboaccoounttype.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboAccGroup.Items.Add(dr["accgroup"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboAccGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboAccGroup.SelectedValue == "ASSETS")
                {
                    cbonormalbalance.Text = "Debit";
                }
                if (cboAccGroup.SelectedValue == "LIABILITIES")
                {
                    cbonormalbalance.Text = "Credit";
                }
                if (cboAccGroup.SelectedValue == "INCOME")
                {
                    cbonormalbalance.Text = "Credit";
                }
                if (cboAccGroup.SelectedValue == "EXPENSES")
                {
                    cbonormalbalance.Text = "Debit";
                }
                if (cboAccGroup.SelectedValue == "RETAINED EARNINGS")
                {
                    cbonormalbalance.Text = "Credit";
                }
                if (cboAccGroup.SelectedValue == "CAPITAL")
                {
                    cbonormalbalance.Text = "Credit";
                }
                cboaccountgroup.Items.Clear();

                dr = new WARTECHCONNECTION.cConnect().ReadDB("select Description from category where accgroup='" + cboAccGroup.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboaccountgroup.Items.Add(dr["Description"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void chkSuspense_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSuspense.Checked == true)
            {
                chkRetainedEarning.Checked = false;
            }
        }

        protected void chkRetainedEarning_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetainedEarning.Checked == true)
            {
                chkSuspense.Checked = false;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtaccno.Text = GridView1.SelectedRow.Cells[1].Text;
                txtaccname.Text = GridView1.SelectedRow.Cells[2].Text;

                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("Select Glcode,Glaccname,accno,Glacctype,GlAccMainGroup,Glaccgroup,Normalbal,auditid,auditdate,curr,transdate,acccategory,Type,OpeningBal,NewGlOpeningBal,SubType,isSuspense,isREarning From GLSETUP where AccNo='" + txtaccno.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        cboaccoounttype.SelectedValue = dr1["Glacctype"].ToString();
                        cboAccGroup.Text = dr1["GlAccMainGroup"].ToString();
                        cboaccountgroup.SelectedItem.Text=dr1["Glaccgroup"].ToString();
                        cbonormalbalance.Text = dr1["Normalbal"].ToString();
                        cbocurrency.SelectedValue = dr1["curr"].ToString();
                        cboAccType.SelectedValue = dr1["Type"].ToString();
                        dtpTransDate.Text = dr1["auditdate"].ToString();
                        cboSubType.SelectedValue = dr1["SubType"].ToString();
                        txtOpeningBalance.Text = dr1["OpeningBal"].ToString();
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
    }
}