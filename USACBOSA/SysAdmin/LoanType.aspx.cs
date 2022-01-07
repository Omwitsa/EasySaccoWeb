using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class LoanType : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
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
                LoadLoanTypes();
                optMonthly.Checked = true;
            }
        }


        private void LoadLoanTypes()
        {
            try
            {
                string sehhh = "select LoanCode [Loan Code],LoanType [Loan Type] from loantype ORDER BY ID DESC";
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

        protected void txtLoanCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select LoanCode,LoanType,MinimumPaidForBridging,MinimumPaidForTopup,LoanAcc,InterestAcc,PenaltyAcc,SchemeCode,RepayPeriod,Interest,MaxAmount,Guarantor,AuditID,AuditTime,useintRange,accno,IntAccno,EarningRation,Penalty,DefaultLoanno,NSSF,bankloan,OtherDeduct,priority,contraAccount,Bridging,ContraAcc,MaxLoans,Repaymethod,PremiumAcc,PremiumContraAcc,Bridgefees,periodrepaid,WaitingPeriod,ID,AccruedAcc,PPAcc,MDTEI,intrecovery,isMain,selfGuarantee,ReceivableAcc from LOANTYPE where LoanCode='" + txtLoanCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtLoanCode.Text = dr["LoanCode"].ToString();
                        txtLoanType.Text = dr["LoanType"].ToString();
                        txtInterestRate.Text = dr["interest"].ToString();
                        txtMaxAmmount.Text = dr["MaxAmount"].ToString();
                        txtRepayPeriod.Text = dr["repayperiod"].ToString();

                        txtpriority.Text = Convert.ToInt32(dr["Priority"]).ToString();
                        cboRepayMethod.SelectedValue = dr["repaymethod"].ToString();
                        int Guarantorvalue = Convert.ToInt32(dr["Guarantor"].ToString());
                        if (Guarantorvalue == 1)
                        {
                            ChkGuarantors.Checked = true;
                        }
                        else
                        {
                            ChkGuarantors.Checked = false;
                        }
                        string bridgingvalue = dr["bridging"].ToString();
                        if (bridgingvalue == "True")
                        {
                            chkbridging.Checked = true;
                        }
                        else
                        {
                            chkbridging.Checked = false;
                        }
                        // chkPenalty = IIf(!Penalty = True, vbChecked, vbUnchecked)
                        string selfGuarr = dr["selfGuarantee"].ToString();
                        if (selfGuarr == "True")
                        {
                            chkSelfGuar.Checked = true;
                        }
                        else
                        {
                            chkSelfGuar.Checked = false;
                        }
                        txtAccno.Text = dr["LoanAcc"].ToString();
                    }
                }
                else
                {
                    txtLoanType.Text = "";
                    txtInterestRate.Text = "0";
                    txtMaxAmmount.Text = "0";
                    txtRepayPeriod.Text = "0";
                    //txtpriority.Text = "";
                    //Editing = False
                    cboRepayMethod.Text = "";
                    ChkGuarantors.Checked = false;
                    chkbridging.Checked = false;
                    //chkPenalty.value = vbUnchecked
                    //cboMDTI.Text = 0
                    //cboTRB.Text = 0
                    WARSOFT.WARMsgBox.Show("Loan type with that code "+txtLoanCode.Text+" does not exist");
                    txtLoanCode.Focus();

                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
                string sql = "select GLACCNAME from glsetup where accno='" + txtAccno.Text + "'";
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        txtAccNames.Text = dr2["GLACCNAME"].ToString();
                    }
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void optdaily_CheckedChanged(object sender, EventArgs e)
        {
            if (optdaily.Checked == true)
            {
                optMonthly.Checked = false;
                optWeekly.Checked = false;
            }
        }

        protected void optMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (optMonthly.Checked == true)
            {
                optdaily.Checked = false;
                optWeekly.Checked = false;
            }
        }

        protected void optWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (optWeekly.Checked == true)
            {
                optdaily.Checked = false;
                optMonthly.Checked = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string pMode = "";
                string pRate = "";
                double pValue = 0;
                int ChargeItem = 0;
                bool isPenalisable = false;
                if (txtLoanCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the Loan Code");
                    txtLoanCode.Focus();
                    return;
                }
                if (txtLoanType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the Loan Type");
                    txtLoanType.Focus();
                    return;
                }
                if (txtMaxAmmount.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the Maximum Amount");
                    txtMaxAmmount.Focus();
                    return;
                }
                if (txtInterestRate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the Interest Rate");
                    txtInterestRate.Focus();
                    return;
                }
                if (txtRepayPeriod.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the Repay Period");
                    txtRepayPeriod.Focus();
                    return;
                }

                if (cboRepayMethod.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("CHOOSE THE REPAYMETHOD FOR THIS LOAN");
                    cboRepayMethod.Focus();
                    return;
                }
                string Guarantor = "";
                string bridging = "";
                string selfGuarantee = "";
                if (ChkGuarantors.Checked == true)
                {
                    Guarantor = "1";
                }
                else if (ChkGuarantors.Checked == false)
                {
                    Guarantor = "0";
                }
                if (chkbridging.Checked == true)
                {
                    bridging = "1";
                }
                else if (chkbridging.Checked == false)
                {
                    bridging = "0";
                }
                if (chkSelfGuar.Checked == true)
                {
                    selfGuarantee = "1";
                }
                else if (chkSelfGuar.Checked == false)
                {
                    selfGuarantee = "0";
                }
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select loancode from loantype where loancode= '" + txtLoanCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("Loan type with same code exists");
                        txtLoanCode.Text = "";
                        return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                DateTime AuditTime = DateTime.Now;
                string SAVEDATA = "set dateformat dmy Insert into LoanType(LoanCode,LoanType,MinimumPaidForBridging,MinimumPaidForTopup,LoanAcc,InterestAcc,PenaltyAcc,SchemeCode,RepayPeriod,Interest,MaxAmount,Guarantor,AuditID,AuditTime,priority,Bridging,MaxLoans,Repaymethod,PPAcc,MDTEI,selfGuarantee)values('" + txtLoanCode.Text + "','" + txtLoanType.Text + "','" + cboTRB.Text + "','" + cboTRB.Text + "','" + txtAccno.Text + "','" + txtAccnoIntrst.Text + "','" + txtAccnoPenalty.Text + "','','" + txtRepayPeriod.Text + "','" + txtInterestRate.Text + "','" + txtMaxAmmount.Text + "','" + Guarantor + "','User','" + AuditTime + "','" + txtpriority.Text + "','" + bridging + "','" + txtMaxLoans.Text + "','" + cboRepayMethod.Text + "','" + txtAccno.Text + "','" + cboMDTI.Text + "','" + selfGuarantee + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(SAVEDATA);
                //
                // 'refinancing Issues
                new WARTECHCONNECTION.cConnect().WriteDB("Delete from loanbridgetype where loancode='" + txtLoanCode.Text + "'");
                for (int i = 0; i <= GridView3.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView3.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {
                        string sqlssql = "set dateformat dmy INSERT INTO loanbridgetype (loancode, loantobridge)VALUES('" + txtLoanCode.Text + "','" + GridView3.Rows[i].Cells[2].Text + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(sqlssql);
                    }
                }
                if (chkAttractPenalty.Checked == true)
                {
                    isPenalisable = true;
                }
                if (isPenalisable == true) //'Penalty issues
                {
                    if (optFixed.Checked == true)
                    {
                        pMode = "Fixed";
                    }
                    if (optdaily.Checked == true)
                    {
                        pRate = "Daily";
                    }
                    else if (optWeekly.Checked == true)
                    {
                        pRate = "Weekly";
                    }
                    else
                    {
                        pRate = "Monthly";
                    }
                }
                else
                {
                    pMode = "Percentage";
                    pRate = "Monthly";
                }

                if (optPrincipal.Checked == true)
                {
                    ChargeItem = 0;
                }
                if (optInterest.Checked == true)
                {
                    ChargeItem = 1;
                }
                else
                {
                    ChargeItem = 2;
                }

                if (txtPenaltyAmount.Text == "")
                {
                    pValue = 0;
                }
                else
                {
                    pValue = Convert.ToDouble(txtPenaltyAmount.Text);
                }

                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select * from penalty where loancode='" + txtLoanCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        new WARTECHCONNECTION.cConnect().WriteDB("Update penalty set mode='" + pMode + "',rate='" + pRate + "',value='" + pValue + "',penalty=1,ChargeItem=" + ChargeItem + " where loancode='" + txtLoanCode.Text + "'");
                    }
                }
                else
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("Insert into penalty(loancode,mode,rate,[value],ChargeItem) values('" + txtLoanCode.Text + "','" + pMode + "','" + pRate + "'," + pValue + "," + ChargeItem + ")");
                }
                WARSOFT.WARMsgBox.Show("Record Successfully Saved!");
                LoadLoanTypes();
                puclicclass();
                return;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void puclicclass()
        {
            throw new NotImplementedException();
        }


        protected void cboAccno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select GLACCNAME from glsetup where accno='" + txtAccno.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtAccNames.Text = dr["GLACCNAME"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSearchAcc_Click(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSetValue.Text == "1")
                {
                    this.txtAccno.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtAccNames.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    // btnFindGlAcc.Visible = false;
                    cboSearchBy.Visible = false;
                    btnFindSearch.Visible = false;
                    txtvalue.Visible = false;
                    LoadLoanTypes();
                }
                if (txtSetValue.Text == "2")
                {
                    this.txtAccnoIntrst.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtAccNamesIntrest.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    cboSearchBy.Visible = false;
                    btnFindSearch.Visible = false;
                    txtvalue.Visible = false;
                    LoadLoanTypes();
                }
                if (txtSetValue.Text == "3")
                {
                    this.txtAccnoPenalty.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtAccNamesPenalty.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    cboSearchBy.Visible = false;
                    btnFindSearch.Visible = false;
                    txtvalue.Visible = false;
                    LoadLoanTypes();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtLoanCode.Text = GridView1.SelectedRow.Cells[1].Text;
                this.txtLoanType.Text = GridView1.SelectedRow.Cells[2].Text;
                getloantypedetails(txtLoanCode.Text.Trim());
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getloantypedetails(string Loancode)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select LoanCode,Mode,Rate,value,ChargeItem from penalty where LoanCode='" + txtLoanCode.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int ChargeItem = Convert.ToInt32(dr["ChargeItem"].ToString());
                        if (ChargeItem == 0)
                        {
                            optPrincipal.Checked = true;
                        }
                        if (ChargeItem == 1)
                        {
                            optInterest.Checked = true;
                        }
                        if (ChargeItem == 2)
                        {
                            optBoth.Checked = true;
                        }
                        txtvalue.Text = dr["value"].ToString();
                        string Rate = dr["Rate"].ToString();
                        if (Rate == "Daily")
                        {
                            optdaily.Checked = true;
                        }
                        if (Rate == "Weekly")
                        {
                            optWeekly.Checked = true;
                        }
                        if (Rate == "Monthly")
                        {
                            optMonthly.Checked = true;
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select LoanCode,LoanType,MinimumPaidForBridging,MinimumPaidForTopup,LoanAcc,InterestAcc,PenaltyAcc,SchemeCode,RepayPeriod,Interest,MaxAmount,Guarantor,useintRange,accno,IntAccno,EarningRation,Penalty,DefaultLoanno,NSSF,bankloan,OtherDeduct,priority,contraAccount,Bridging,ContraAcc,MaxLoans,Repaymethod,PremiumAcc,PremiumContraAcc,Bridgefees,periodrepaid,WaitingPeriod,ID,AccruedAcc,PPAcc,MDTEI,intrecovery,isMain,selfGuarantee,ReceivableAcc from LoanType where LoanCode='" + txtLoanCode.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtRepayPeriod.Text = dr["RepayPeriod"].ToString();
                        txtMaxAmmount.Text = dr["MaxAmount"].ToString();
                        cboRepayMethod.Text = dr["Repaymethod"].ToString();
                        cboTRB.Text = dr["MinimumPaidForTopup"].ToString();
                        cboMDTI.Text = dr["MDTEI"].ToString();
                        txtpriority.Text = dr["priority"].ToString();
                        txtMaxAmmount.Text = dr["MaxAmount"].ToString();
                        txtMaxLoans.Text = dr["MaxLoans"].ToString();
                        txtInterestRate.Text = dr["Interest"].ToString();
                        bool penalt = Convert.ToBoolean(dr["Penalty"].ToString());
                        if (penalt == true)
                        {
                            chkAttractPenalty.Checked = true;
                        }
                        else
                        {
                            chkAttractPenalty.Checked = false;
                        }
                        bool bridging = Convert.ToBoolean(dr["Bridging"].ToString());
                        if (bridging == true)
                        {
                            chkbridging.Checked = true;
                        }
                        else
                        {
                            chkbridging.Checked = false;
                        }
                        bool guaraownloan = Convert.ToBoolean(dr["selfGuarantee"].ToString());
                        if (guaraownloan == true)
                        {
                            chkSelfGuar.Checked = true;
                        }
                        else
                        {
                            chkSelfGuar.Checked = false;
                        }
                        int guar = Convert.ToInt32(dr["Guarantor"]);
                        if (guar == 1)
                        {
                            ChkGuarantors.Checked = true;
                        }
                        else
                        {
                            ChkGuarantors.Checked = false;
                        }
                        txtAccno.Text = dr["LoanAcc"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select glaccname from GLSETUP where accno='" + txtAccno.Text + "'");
                        {
                            while (dr1.Read())
                            {
                                txtAccNames.Text = dr1["glaccname"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        txtAccnoIntrst.Text = dr["InterestAcc"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select glaccname from GLSETUP where accno='" + txtAccnoIntrst.Text + "'");
                        {
                            while (dr1.Read())
                            {
                                txtAccNamesIntrest.Text = dr1["glaccname"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        txtAccnoPenalty.Text = dr["PenaltyAcc"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select glaccname from GLSETUP where accno='" + txtAccnoPenalty.Text + "'");
                        {
                            while (dr1.Read())
                            {
                                txtAccNamesPenalty.Text = dr1["glaccname"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnLoanAcctsSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtSetValue.Text = "1";
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

        protected void btnIntrestAcctsSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtSetValue.Text = "2";
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

        protected void btnPenaltyAcctsSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtSetValue.Text = "3";
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


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string pMode = "";
                string pRate = "";
                double pValue = 0;
                int ChargeItem = 0;
                bool isPenalisable = false;
                string Guarantor = "";
                string bridging = "";
                string selfGuarantee = "";
                DateTime AuditTime = DateTime.Now;
                if (ChkGuarantors.Checked == true)
                {
                    Guarantor = "1";
                }
                else if (ChkGuarantors.Checked == false)
                {
                    Guarantor = "0";
                }
                if (chkbridging.Checked == true)
                {
                    bridging = "1";
                }
                else if (chkbridging.Checked == false)
                {
                    bridging = "0";
                }
                if (chkSelfGuar.Checked == true)
                {
                    selfGuarantee = "1";
                }
                else if (chkSelfGuar.Checked == false)
                {
                    selfGuarantee = "0";
                }

                string sql = "set dateformat dmy update LoanType set LoanCode='" + txtLoanCode.Text + "',LoanType='" + txtLoanType.Text + "',MinimumPaidForBridging='" + cboTRB.Text + "',MinimumPaidForTopup='" + cboTRB.Text + "',LoanAcc='" + txtAccno.Text + "',InterestAcc='" + txtAccnoIntrst.Text + "',PenaltyAcc='" + txtAccnoPenalty.Text + "',SchemeCode='',RepayPeriod='" + txtRepayPeriod.Text + "',Interest='" + txtInterestRate.Text + "',MaxAmount='" + txtMaxAmmount.Text + "',Guarantor='" + Guarantor + "',AuditID='" + Session["mimi"].ToString() + "',AuditTime='" + System.DateTime.Now.ToString("hh:mm:ss") + "',priority='" + txtpriority.Text + "',Bridging='" + bridging + "',MaxLoans='" + txtMaxLoans.Text + "',Repaymethod='" + cboRepayMethod.Text + "',PPAcc='" + txtAccno.Text + "',MDTEI='" + cboMDTI.Text + "',selfGuarantee='" + selfGuarantee + "' where LoanCode='" + txtLoanCode.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
                if (chkAttractPenalty.Checked == true)
                {
                    isPenalisable = true;
                }
                if (isPenalisable == true) //'Penalty issues
                {
                    if (optFixed.Checked == true)
                    {
                        pMode = "Fixed";
                    }
                    if (optdaily.Checked == true)
                    {
                        pRate = "Daily";
                    }
                    else if (optWeekly.Checked == true)
                    {
                        pRate = "Weekly";
                    }
                    else
                    {
                        pRate = "Monthly";
                    }
                }
                else
                {
                    pMode = "Percentage";
                    pRate = "Monthly";
                }

                if (optPrincipal.Checked == true)
                {
                    ChargeItem = 0;
                }
                if (optInterest.Checked == true)
                {
                    ChargeItem = 1;
                }
                else
                {
                    ChargeItem = 2;
                }

                if (txtPenaltyAmount.Text == "")
                {
                    pValue = 0;
                }
                else
                {
                    pValue = Convert.ToDouble(txtPenaltyAmount.Text);
                }

                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select * from penalty where loancode='" + txtLoanCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        new WARTECHCONNECTION.cConnect().WriteDB("Update penalty set mode='" + pMode + "',rate='" + pRate + "',value='" + pValue + "',penalty=1,ChargeItem=" + ChargeItem + " where loancode='" + txtLoanCode.Text + "'");
                    }
                }
                else
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("Insert into penalty(loancode,mode,rate,[value],ChargeItem) values('" + txtLoanCode.Text + "','" + pMode + "','" + pRate + "'," + pValue + "," + ChargeItem + ")");
                }
                WARSOFT.WARMsgBox.Show("" + txtLoanType.Text + " updated sucessfully");
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void optPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (optPrincipal.Checked == true)
            {
                optBoth.Checked = false;
                optInterest.Checked = false;
            }
        }

        protected void optBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (optBoth.Checked == true)
            {
                optPrincipal.Checked = false;
                optInterest.Checked = false;
            }
        }

        protected void optInterest_CheckedChanged(object sender, EventArgs e)
        {
            if (optInterest.Checked == true)
            {
                optPrincipal.Checked = false;
                optBoth.Checked = false;
            }
        }

        protected void chkAttractPenalty_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }

        protected void optFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (optFixed.Checked == true)
            {
                optPercentage.Checked = false;
            }
        }

        protected void optPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (optPercentage.Checked == true)
            {
                optFixed.Checked = false;
            }
        }

        protected void chkbridging_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbridging.Checked == true)
                {
                    string sehhh = "select LoanCode [Loan Code],LoanType [Loan Type] from loantype ORDER BY ID DESC";
                    da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView3.Visible = true;
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    GridView1.Visible = false;
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    GridView3.Visible = false;
                    LoadLoanTypes();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtLoanCode.Text == "")
            {
                WARSOFT.WARMsgBox.Show("LoanCode required");
            }
            else
            {
                String delete = "Delete from loantype where LoanCode='" + txtLoanCode.Text +"'";
                new WARTECHCONNECTION.cConnect().WriteDB(delete);
                WARSOFT.WARMsgBox.Show("Deleted successfully");
                LoadLoanTypes();
                return;
            }
        }

        protected void cboLoanCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}