using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class SocietyParameters : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
        System.Data.SqlClient.SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["mimi"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
        }

        private void Load_Records()
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyCode,LoanInterest,ShareInterest,MinGuarantors,MaxGuarantors,LoanToShareRatio,maturity,MinTotShares,MaxLoans,MaxContribPeriod,BankInterest,MinDivPeriod,DefFundID,DeductAmt,SelfGuar,GuarShareRatio,CompanyName,AuditID,AuditTime,ShareMaturity,withdrawalnotice,InsPremium,ShareCapital,Depositinterest,Defaultingperiod,OffsettingPercentage,sharecap,refinancecommision,loandefaulted,regfees,bylaws,dormancyperiod,address,telephone,email,fax,town,shareloanpriority,LoanRecoveryOption,DefaultedInterestAction,SuspenseAcc,RearningsAcc,CheckOffDate,creditorsacc,vat,Rounding,SLBalance,PhysicalAddress,Website from sysparam");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtCompanyName.Text = dr["CompanyName"].ToString();
                        txtAddress.Text = dr["address"].ToString();
                        txtTelephone.Text = dr["Telephone"].ToString();
                        txtEMail.Text = dr["email"].ToString();
                        txtcontactperson.Text = dr["PhysicalAddress"].ToString();
                        txtFax.Text = dr["Fax"].ToString();
                        txtTown.Text = dr["town"].ToString();
                        txtwebsites.Text = dr["WebSite"].ToString();
                        txtMaturity.Text = dr["maturity"].ToString();
                        txtDivPeriod.Text = dr["MinDivPeriod"].ToString();
                        txtWithdrawalNotice.Text = dr["WithdrawalNotice"].ToString();
                        txtRounding.Text = dr["Rounding"].ToString();
                        txtSLBalance.Text = dr["SLBalance"].ToString();
                        txtMinGuar.Text = dr["MinGuarantors"].ToString();
                        txtMaxGuar.Text = dr["maxGuarantors"].ToString();
                        dtpCheckOffDate.Text = dr["CheckOffDate"].ToString();
                        //cbocurrency.Text = dr["Currency"].ToString();
                        cboSuspAccno.Text = dr["SuspenseAcc"].ToString();
                        // cboAccno(0).Text = cboAccno(0).List(0)
                        
                        string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP where AccNo='" + cboSuspAccno.Text + "'";
                        dr3 = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                txtAccNames.Text = dr3["GLAccName"].ToString();
                            }
                        }
                        dr3.Close(); dr3.Dispose(); dr3 = null;

                        cboREAccno.Text = dr["REarningsAcc"].ToString();
                        // cboAccno(1).Text = cboAccno(1).List(0)
                        string sehhhdr1 = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP where AccNo='" + cboREAccno.Text + "'";
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB(sehhhdr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                txtREAccNames.Text = dr1["GLAccName"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;

                        cboCreditorsAccno.Text = dr["CreditorsAcc"].ToString();
                        // cboAccno(2).Text = cboAccno(2).List(0)
                        string sehhhdr1dr2 = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP where AccNo='" + cboCreditorsAccno.Text + "'";
                        dr2 = new WARTECHCONNECTION.cConnect().ReadDB(sehhhdr1dr2);
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                txtCreditorsAccNames.Text = dr2["GLAccName"].ToString();
                            }
                        }
                        dr2.Close(); dr2.Dispose(); dr2 = null;

                        cboDefInterestAction.SelectedValue = dr["DefaultedInterestAction"].ToString();
                        getDefIntrestDetails();
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string CompanyName = ""; string MinGuarantors, maxGuarantors, PhysicalAddress, WebSite, address, Telephone, email, Fax, town, maturity, WithdrawalNotice, MinDivPeriod, Rounding, SLBalance = "";
                if (txtCompanyName.Text == "")
                {
                    CompanyName = "SACCO SOCIETY";
                }
                else
                {
                    CompanyName = txtCompanyName.Text.Trim().Replace("'", "");
                }

                address = txtAddress.Text;
                Telephone = txtTelephone.Text;
                email = txtEMail.Text;
                Fax = txtFax.Text;
                town = txtTown.Text;
                maturity = txtMaturity.Text;
                WithdrawalNotice = txtWithdrawalNotice.Text;
                MinDivPeriod = txtDivPeriod.Text;
                Rounding = txtRounding.Text;
                //'!Currency = cbocurrency.Text;
                string SuspenseAcc = cboSuspAccno.Text;
                string REarningsAcc = cboREAccno.Text;
                string CreditorsAcc = cboCreditorsAccno.Text;
                if (cboDefInterestAction.Text == "")
                {
                    cboDefInterestAction.Text = "0";
                }
                int DefaultedInterestAction = Convert.ToInt32(cboDefInterestAction.Text);
                SLBalance = txtSLBalance.Text;
                MinGuarantors = txtMinGuar.Text;
                maxGuarantors = txtMaxGuar.Text;
                PhysicalAddress = txtcontactperson.Text;
                WebSite = txtwebsites.Text;
                string todbb = "set dateformat dmy insert into sysparam(MinGuarantors,MaxGuarantors,LoanToShareRatio,maturity,MinDivPeriod,SelfGuar,GuarShareRatio,CompanyName,AuditID,AuditTime,withdrawalnotice,address,telephone,email,fax,town,DefaultedInterestAction,SuspenseAcc,RearningsAcc,CheckOffDate,creditorsacc,Rounding,SLBalance,PhysicalAddress,Website)values('" + MinGuarantors + "','" + maxGuarantors + "','','" + maturity + "','" + MinDivPeriod + "','','','" + CompanyName + "','" + Session["mimi"] + "','" + System.DateTime.Now + "','" + WithdrawalNotice + "','" + address + "','" + Telephone + "','" + email + "','" + Fax + "','" + town + "','" + DefaultedInterestAction + "','" + SuspenseAcc + "','" + REarningsAcc + "','" + dtpCheckOffDate.Text + "','" + CreditorsAcc + "','" + Rounding + "','" + SLBalance + "','" + PhysicalAddress + "','" + WebSite + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(todbb);
                WARSOFT.WARMsgBox.Show("Record saved sucessfully");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindSuspcAC_Click(object sender, ImageClickEventArgs e)
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
                //cboSearchBy.Visible = true;
                //btnFindSearch.Visible = true;
                //txtvalue.Visible = true;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSetValue.Text == "1")
                {
                    this.cboSuspAccno.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtAccNames.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    // btnFindGlAcc.Visible = false;
                    //cboSearchBy.Visible = false;
                    //btnFindSearch.Visible = false;
                    //txtvalue.Visible = false;
                }
                if (txtSetValue.Text == "2")
                {
                    this.cboREAccno.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtREAccNames.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    //cboSearchBy.Visible = false;
                    //btnFindSearch.Visible = false;
                    //txtvalue.Visible = false;
                }
                if (txtSetValue.Text == "3")
                {
                    this.cboCreditorsAccno.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.txtCreditorsAccNames.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    //cboSearchBy.Visible = false;
                    //btnFindSearch.Visible = false;
                    //txtvalue.Visible = false;
                    //LoadLoanTypes();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindREAC_Click(object sender, ImageClickEventArgs e)
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
                //cboSearchBy.Visible = true;
                //btnFindSearch.Visible = true;
                //txtvalue.Visible = true;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindCreditorsAC_Click(object sender, ImageClickEventArgs e)
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
                //cboSearchBy.Visible = true;
                //btnFindSearch.Visible = true;
                //txtvalue.Visible = true;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboDefInterestAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDefIntrestDetails();
        }

        private void getDefIntrestDetails()
        {
            try
            {
                if (cboDefInterestAction.Text == "1")
                {
                    txtDefInterestAction.Text = "Defaulted Interest is Accrued for payment next month .";
                    lblFormula.Text = "I = (rate*(Balance(P)) + A.I ";
                }
                if (cboDefInterestAction.Text == "2")
                {
                    txtDefInterestAction.Text = "Defaulted Interest is Loaded with the balance in calculation of this months interest. But you still recover the accrued interest.";
                    lblFormula.Text = "I = rate*(Balance(P) + A.I)+ A.I";
                }
                if (cboDefInterestAction.Text == "3")
                {
                    txtDefInterestAction.Text = "Defaulted Interest is Loaded with the balance in calculation of this months interest. ";
                    lblFormula.Text = "I = rate*(Balance(P) + A.I)";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string CompanyName = ""; string MinGuarantors = ""; string maxGuarantors = ""; string PhysicalAddress = ""; string WebSite = ""; string address = ""; string Telephone = ""; string email = ""; string Fax = ""; string town = ""; string maturity = ""; string WithdrawalNotice = ""; string MinDivPeriod = ""; string Rounding = ""; string SLBalance = "";
                if (txtCompanyName.Text == "")
                {
                    CompanyName = "SACCO SOCIETY";
                }
                else
                {
                    CompanyName = txtCompanyName.Text.Trim().Replace("'", "");
                }

                address = txtAddress.Text;
                Telephone = txtTelephone.Text;
                email = txtEMail.Text;
                Fax = txtFax.Text;
                town = txtTown.Text;
                maturity = txtMaturity.Text;
                WithdrawalNotice = txtWithdrawalNotice.Text;
                MinDivPeriod = txtDivPeriod.Text;
                Rounding = txtRounding.Text;
                //'!Currency = cbocurrency.Text;
                string SuspenseAcc = cboSuspAccno.Text;
                string REarningsAcc = cboREAccno.Text;
                string CreditorsAcc = cboCreditorsAccno.Text;
                if (cboDefInterestAction.Text == "")
                {
                    cboDefInterestAction.Text = "0";
                }
                int DefaultedInterestAction = Convert.ToInt32(cboDefInterestAction.Text);
                SLBalance = txtSLBalance.Text;
                MinGuarantors = txtMinGuar.Text;
                maxGuarantors = txtMaxGuar.Text;
                PhysicalAddress = txtcontactperson.Text;
                WebSite = txtwebsites.Text;
                string updatetodbb = "set dateformat dmy update sysparam set MinGuarantors='" + MinGuarantors + "',MaxGuarantors='" + maxGuarantors + "',maturity='" + maturity + "',MinDivPeriod='" + MinDivPeriod + "',CompanyName='" + CompanyName + "',AuditID='" + Session["mimi"] + "',AuditTime='" + System.DateTime.Now + "',withdrawalnotice='" + WithdrawalNotice + "',address='" + address + "',telephone='" + Telephone + "',email='" + email + "',fax='" + Fax + "',town='" + town + "',DefaultedInterestAction='" + DefaultedInterestAction + "',SuspenseAcc='" + SuspenseAcc + "',RearningsAcc='" + REarningsAcc + "',CheckOffDate='" + dtpCheckOffDate.Text + "',creditorsacc='" + CreditorsAcc + "',Rounding='" + Rounding + "',SLBalance='" + SLBalance + "',PhysicalAddress='" + PhysicalAddress + "',Website='" + WebSite + "' where CompanyName='" + CompanyName + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(updatetodbb);
                WARSOFT.WARMsgBox.Show("Records updated sucessfully");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnLoadSysparam_Click(object sender, EventArgs e)
        {
            Load_Records();
        }
    }
}