using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.LoansAdmin
{
    public partial class LoanGuarantors : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, DR, dr12, Dr, dr6;
        System.Data.SqlClient.SqlDataAdapter da;
        int maxLGuarantors = 0;
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
                txtGuarNo.Visible = false;
                txtGuarName.Visible = false;
                txtAmtAllocated.Visible = false;
                txtShares1.Visible = false;
                txtAvAmt.Visible = false;
                Label13.Visible = false;
                Label14.Visible = false;
                Label15.Visible = false;
                Label16.Visible = false;
                Label17.Visible = false;
                txtMarketValue.Visible = false;
                Label18.Visible = false;
                Label19.Visible = false;
                Label21.Visible = false;
                Label22.Visible = false;
                txtCollateralDescription.Visible = false;
                txtConsideredValue.Visible = false;
                txtDocumentNo.Visible = false;
                cboCollateralCode.Visible = false;
                Label24.Visible = false;
                TextBox3.Visible = false;
            }
        }
        protected void txtLoanNo_DataBinding(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select LoanNo,MemberNo,LoanCode,ApplicDate,LoanAmt,RepayPeriod from Loans where LoanNo='" + txtLoanNo.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtLoaneeMemberNo.Text = dr["MemberNo"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        protected void txtLoanNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime ddate = DateTime.Today;

                dr = new WARTECHCONNECTION.cConnect().ReadDB("select LoanNo,MemberNo,LoanCode,ApplicDate,LoanAmt,RepayPeriod from Loans where LoanNo='" + txtLoanNo.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtLoaneeMemberNo.Text = dr["MemberNo"].ToString().Trim();
                        txtRepayPeriod.Text = dr["RepayPeriod"].ToString().Trim();
                        txtAmtApproved.Text = dr["LoanAmt"].ToString().Trim();
                        txtTransDate.Text = ddate.ToString("yyyy-MMM-dd").Trim();
                        DR = new WARTECHCONNECTION.cConnect().ReadDB("select MemberNo,Surname,OtherNames from members where MemberNo='" + txtLoaneeMemberNo.Text.Trim() + "'");
                        if (DR.HasRows)
                        {
                            while (DR.Read())
                            {
                                txtLoanApplicant.Text = DR["Surname"].ToString().Trim() + " " + DR["OtherNames"].ToString().Trim();
                            }
                        }
                        DR.Close(); DR.Dispose(); DR = null;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                SumGuarAmount();
                LoadGuarantors();
                if (Convert.ToDouble(txtAmtGuaranteed.Text) == Convert.ToDouble(txtAmtApproved.Text))
                {
                    btnAddGuar.Enabled = false;
                    btnCollaterals.Enabled = false;
                    WARSOFT.WARMsgBox.Show("The amount to guaranteed has been reached."); return;
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void LoadGuarantors()
        {
            try
            {
                string sehhh = "select GuarMemberNo [Guarantor Member No],GuarNames [Guarantor Names],GuarBalance [Balance],GuarAmount [Amount] from vwLoanGuarantors where loanno='" + txtLoanNo.Text + "'";
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
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGuarNo.Text = GridView1.SelectedRow.Cells[1].Text;
        }
        protected void btnAddGuar_Click(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect oMaxGuar = new WARTECHCONNECTION.cConnect();
                dr = oMaxGuar.ReadDB("select maxGuarantors from sysparam");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        maxLGuarantors = Convert.ToInt32(dr["maxGuarantors"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; oMaxGuar.Dispose(); oMaxGuar = null;

                WARTECHCONNECTION.cConnect oLtype = new WARTECHCONNECTION.cConnect();
                string Connectt = "select lt.guarantor,lt.loantype from loantype lt inner join loans l on l.loancode=lt.loancode where l.loanno='" + txtLoanNo.Text + "'";
                dr1 = oLtype.ReadDB(Connectt);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        //string strGuarantor
                        int Guarantor = Convert.ToInt32(dr1["guarantor"].ToString());
                        string LoanType = dr1["loantype"].ToString();
                        if (Guarantor == 0)
                        {
                            WARSOFT.WARMsgBox.Show("" + LoanType + " Does Not Require Guarantors");
                            return;
                        }
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; oLtype.Dispose(); oLtype = null;
                WARTECHCONNECTION.cConnect oMaxLguarantors = new WARTECHCONNECTION.cConnect();
                dr2 = oMaxLguarantors.ReadDB("Select count(loanno)loanno from loanguar where Loanno='" + txtLoanNo.Text + "' AND BALANCE>0 and transfered=0");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        int loncount = Convert.ToInt32(dr2["loanno"].ToString());
                        if (loncount >= maxLGuarantors)
                        {
                            WARSOFT.WARMsgBox.Show("The Loan has reached the maximum guarantorShip");
                            return;
                        }
                    }
                dr2.Close(); dr2.Dispose(); dr2 = null; oMaxLguarantors.Dispose(); oMaxLguarantors = null;

                TextBox1.Text = "OK";
                refreshgridview(txtLoanNo.Text.Trim());
                LoadGuarantors();
                SumGuarAmount();
                ShowGuarantors();
                txtGuarNo.Text = "";
                txtGuarName.Text = "";
                txtAvAmt.Text = "";
                txtShares1.Text = "";
                txtAmtAllocated.Text = "";
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        protected void txtGuarNo_TextChanged(object sender, EventArgs e)
        {
            //clearTexts();
            if (TextBox1.Text == "NO")
            {
                ShowClollaterals();
            }
            if (TextBox1.Text == "OK")
            {
                ShowGuarantors();
            }
            LoadMemberGuarDetails();
        }
        private void ShowGuarantors()
        {
            Button1.Visible = true;
            txtGuarNo.Visible = true;
            txtGuarName.Visible = true;
            txtAmtAllocated.Visible = true;
            txtShares1.Visible = true;
            txtGuarNo.Visible = true;
            Label13.Visible = true;
            Label14.Visible = true;
            Label15.Visible = true;
            Label16.Visible = true;
            txtAvAmt.Visible = true;
            Label17.Visible = true;

            txtMarketValue.Visible = false;
            Label18.Visible = false;
            Label19.Visible = false;
            Label22.Visible = false;
            Label21.Visible = false;

            txtCollateralDescription.Visible = false;
            txtConsideredValue.Visible = false;
            txtDocumentNo.Visible = false;
            cboCollateralCode.Visible = false;
        }
        private void ShowClollaterals()
        {
            Button1.Visible = false;
            txtMarketValue.Visible = true;
            Label18.Visible = true;
            Label19.Visible = true;
            Label21.Visible = true;
            txtCollateralDescription.Visible = true;
            txtConsideredValue.Visible = true;
            txtDocumentNo.Visible = true;
            Label17.Visible = false;
            txtShares1.Visible = false;
            txtAvAmt.Visible = false;
            Label15.Visible = false;
            cboCollateralCode.Visible = true;
            txtGuarNo.Visible = false;
            txtGuarName.Visible = false;
            txtAmtAllocated.Visible = true;
            Label13.Visible = false;
            Label14.Visible = false;
            Label16.Visible = true;
            //txtGuarNo.Text  "";
            Label22.Visible = true;
        }
        private void clearTexts()
        {
            txtGuarName.Text = "";
            txtGuarNo.Text = "";
            txtAvAmt.Text = "";
            txtAmtAllocated.Text = "";
            txtShares1.Text = "";
        }
        private void LoadMemberGuarDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLoanNo.Text))
                {
                    txtGuarNo.Text = "";
                    WARSOFT.WARMsgBox.Show("Kindly, Provide loan number");
                    txtLoanNo.Focus();
                    return;
                }
                double GuarToShare = 0;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(LoanToShareRatio,0)LoanToShareRatio,SelfGuar from SYSPARAM");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string SelfGuar = dr["SelfGuar"].ToString().Trim();
                        GuarToShare = Convert.ToDouble(dr["LoanToShareRatio"].ToString().Trim());
                        if (txtGuarNo.Text == txtLoaneeMemberNo.Text)
                        {
                            if (SelfGuar == "Yes")
                            {
                                WARSOFT.WARMsgBox.Show("Members not allowed to guarantee their own loans");
                                return;
                            }
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;

                string loaneeGroup = "";
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select COMPANYCODE from members where memberno='" + txtLoaneeMemberNo.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        loaneeGroup = dr1["COMPANYCODE"]?.ToString()?.Trim() ?? "";
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;

                //Get guar names
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select surname+' '+othernames [Names],initshares, COMPANYCODE from members where memberno='" + txtGuarNo.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        string guarGroup = dr1["COMPANYCODE"]?.ToString()?.Trim() ?? "";
                        if (!loaneeGroup.ToUpper().Equals(guarGroup.ToUpper()))
                        {
                            txtGuarName.Text = "";
                            txtGuarNo.Text = "";
                            WARSOFT.WARMsgBox.Show($"Loanee and guarantor must be from the same group ({loaneeGroup})");
                            txtGuarNo.Focus();
                            return;
                        }
                        txtGuarName.Text = dr1["Names"].ToString().Trim();
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                //Get shares of guar
                double Shares = 0;
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(amount),0) as totalshares from contrib where memberno='" + txtGuarNo.Text + "' AND sharescode<>'sc' and sharescode<>'E'");
                //(select sharescode from sharetype where usedToGuarantee=1)");
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        Shares = Convert.ToDouble(dr2["totalshares"].ToString().Trim());
                        txtShares1.Text = Shares.ToString();
                    }
                }
                dr2.Close(); dr2.Dispose(); dr2 = null;
                //Get available amount
                Shares = Shares * GuarToShare;
                dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(sum(guarbalance),0) as AmtGuar from vwloanguarantors where guarmemberno='" + txtGuarNo.Text + "'");
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        Double c; Double b = 0;
                        double AmtAV = Convert.ToDouble(dr3["AmtGuar"]);
                        b = Convert.ToDouble(txtShares1.Text);
                        c = b - AmtAV;
                        txtAvAmt.Text = c.ToString();
                    }
                }
                dr3.Close(); dr3.Dispose(); dr3 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "OK")
                {
                    if (txtGuarNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Please select Guarantor");
                        txtGuarNo.Focus();
                        return;
                    }

                    if (txtAmtAllocated.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Please Specify Amount");
                        txtAmtGuaranteed.Focus();
                        return;
                    }
                    WARTECHCONNECTION.cConnect oMaxGuar = new WARTECHCONNECTION.cConnect();
                    dr = oMaxGuar.ReadDB("select maxGuarantors from sysparam");
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            maxLGuarantors = Convert.ToInt32(dr["maxGuarantors"].ToString());
                        }
                    dr.Close(); dr.Dispose(); dr = null; oMaxGuar.Dispose(); oMaxGuar = null;
                    WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                    dr = oSaccoMaster.ReadDB("Select status from members where memberno='" + txtGuarNo.Text + "'");
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            int memStatus = Convert.ToInt32(dr["status"].ToString());
                            if (memStatus != 1)
                            {
                                WARSOFT.WARMsgBox.Show("The members is suspended from Guaranteeing. He is either Withdrawn,Diceased or just suspended");
                                return;
                            }
                            if (txtAvAmt.Text != "" && txtAmtAllocated.Text != "")
                            {

                                if (Convert.ToDouble(txtAvAmt.Text) < Convert.ToDouble(txtAmtAllocated.Text))
                                {
                                    WARSOFT.WARMsgBox.Show("Amount should not be more than " + txtAvAmt.Text + "");
                                    txtAmtAllocated.Focus();
                                    return;
                                }
                            }
                        }
                    dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
                    WARTECHCONNECTION.cConnect MaxGuar = new WARTECHCONNECTION.cConnect();
                    dr3 = MaxGuar.ReadDB("Select distinct count(loanNo)loanNo from loanguar where memberno='" + txtGuarNo.Text + "' AND BALANCE>0 and transfered=0");
                    if (dr3.HasRows)
                        while (dr3.Read())
                        {
                            int ContLoanno = Convert.ToInt32(dr3["loanNo"].ToString());
                            if (ContLoanno >= maxLGuarantors)
                            {
                                WARSOFT.WARMsgBox.Show("The Member has reached the maximum guarantorShip.");
                                return;
                            }
                        }
                    dr3.Close(); dr3.Dispose(); dr3 = null; MaxGuar.Dispose(); MaxGuar = null;
                    //else
                    //{
                    WARTECHCONNECTION.cConnect osql = new WARTECHCONNECTION.cConnect();
                    string sql = "Select lg.*,isnull(vd.Arrears,0)Arrears from loanguar lg left outer join vwdefauters vd on lg.loanno=vd.loanno  where lg.memberno='" + txtGuarNo.Text + "' and transfered=0";
                    dr4 = osql.ReadDB(sql);
                    if (dr4.HasRows)
                        while (dr4.Read())
                        {
                            string Lnumber = dr4["loanno"].ToString().Trim();
                            if (Lnumber == txtLoanNo.Text.Trim())
                            {
                                WARSOFT.WARMsgBox.Show("This member is already a guarantor for this Loan, Please Get Another One!");
                                return;
                            }
                            double Arrears = Convert.ToDouble(dr4["Arrears"].ToString().Trim());
                            if (Arrears > 0)
                            {
                                WARSOFT.WARMsgBox.Show("This Members has Guaranteed a loan which is currently in arrears.");
                                return;
                            }
                            // 'Check if he has guaranteed a quaranteed A defauter
                        }
                    dr4.Close(); dr4.Dispose(); dr4 = null; osql.Dispose(); osql = null;
                    //}
                    //dr3.Close(); dr3.Dispose(); dr3 = null; MaxGuar.Dispose(); MaxGuar = null;
                    Save_Guarantor(txtGuarNo.Text, txtLoanNo.Text, (Convert.ToDouble(txtAmtAllocated.Text)), (Convert.ToDouble(txtAmtAllocated.Text)), "User", (Convert.ToDateTime(txtTransDate.Text)));

                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID,AuditTime )values('LOANGUAR','Added Loan Guarantor Loanno " + txtLoanNo.Text + "','" + txtTransDate.Text + "','" + txtAmtApproved.Text.Trim() + "','" + Session["mimi"].ToString() + "','" + System.DateTime.Now.ToString("hh:mm") + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                    LoadGuarantors();
                    SumGuarAmount();
                    TextBox1.Text = "";
                    WARSOFT.WARMsgBox.Show("Record added sucessfully");
                }
                else if (TextBox1.Text == "NO")
                {
                    //if (txtGuarNo.Text == "")
                    //{
                    //    WARSOFT.WARMsgBox.Show("You must enter the Member no of the Person Giving the Colateral");
                    //    txtGuarNo.Focus();
                    //    return;
                    //}
                    SaveColGuar();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void SaveColGuar()
        {
            try
            {
                if (txtLoanNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Loanno");
                    txtLoanNo.Focus();
                }
                if (cboCollateralCode.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Collateral Code");
                    cboCollateralCode.Focus();
                }
                if (txtCollateralDescription.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please ensure the Collateral Code entered is correct.");
                    cboCollateralCode.Focus();
                }
                if (txtDocumentNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Document No.");
                    cboCollateralCode.Focus();
                }
                if (txtAmtAllocated.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Allocated Amount.");
                    txtAmtAllocated.Focus();
                }
                if (Convert.ToDouble(txtConsideredValue.Text) < Convert.ToDouble(txtAmtAllocated.Text))
                {
                    WARSOFT.WARMsgBox.Show("The Allocated Amount Should Not excceed the Percentage Value");
                    txtAmtAllocated.Focus();
                }
                if (txtAmtAllocated.Text == "" || txtDocumentNo.Text == "" || txtCollateralDescription.Text == "" || cboCollateralCode.Text == "" || txtLoanNo.Text == "" || txtConsideredValue.Text == "")
                {
                }
                else
                {
                    dr5 = new WARTECHCONNECTION.cConnect().ReadDB("Select * from COLLOANGUAR Where LoanNo='" + txtLoanNo.Text + "' And DocNo='" + txtDocumentNo.Text + "'");
                    if (dr5.HasRows)
                    {
                        while (dr5.Read())
                        {
                            string inssdb = ("Update COLLOANGUAR Set mktvalue=" + txtMarketValue.Text + ",Balance=" + txtAmtAllocated.Text + " Where LoanNo='" + txtLoanNo.Text + "' And DocNo='" + txtDocumentNo.Text + "'");
                            new WARTECHCONNECTION.cConnect().WriteDB(inssdb);
                            LoadCollaterals();
                            TextBox1.Text = "";
                            WARSOFT.WARMsgBox.Show("Collateral Updated successfully.");
                            dr5.Close(); dr5.Dispose(); dr5 = null;
                            return;
                        }
                    }
                    else
                    {
                        SAVE_COLLOANGUAR(txtLoanNo.Text, txtLoaneeMemberNo.Text, cboCollateralCode.Text, txtDocumentNo.Text, Convert.ToDouble(txtAmtAllocated.Text), Convert.ToDouble(txtAmtAllocated.Text), Convert.ToDouble(txtMarketValue.Text), Session["mimi"].ToString());
                        string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('COLLOANGUAR','Added Loan Collateral Loanno " + txtLoanNo.Text + "','" + txtTransDate.Text + "','" + txtAmtApproved.Text.Trim() + "','" + Session["mimi"].ToString() + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                        LoadCollaterals();
                        TextBox1.Text = "";
                        txtMarketValue.Text = "";
                        txtConsideredValue.Text = "";
                        txtAmtAllocated.Text = "";
                        txtConsideredValue.Text = "";
                        TextBox3.Text = "";
                        WARSOFT.WARMsgBox.Show("Collateral saved successfully.");
                        dr5.Close(); dr5.Dispose(); dr5 = null;
                        return;
                    }
                    dr5.Close(); dr5.Dispose(); dr5 = null;
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }
        private void LoadCollaterals()
        {
            string sehhh = "select ColCode,MemberNo,DocNo,Mktvalue,LoanNo,Balance from COLLOANGUAR where LoanNo='" + txtLoanNo.Text + "'";// And DocNo='" + txtDocumentNo.Text + "'
            da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }
        private void SAVE_COLLOANGUAR(string Loanno, string mMemberno, string ColCode, string DocNo, double Amount, double balance, double MktValue, string user)
        {
            try
            {
                string writetodatatbase = "Exec SAVE_COLLOANGUAR '" + ColCode + "','" + mMemberno + "','" + DocNo + "'," + Amount + "," + balance + ",'" + Loanno + "','user'";
                new WARTECHCONNECTION.cConnect().WriteDB(writetodatatbase);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void SumGuarAmount()
        {
            Double Balance = 0; Double Amount = 0; Double SUMAmnt = 0;
            try
            {
                dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(SUM(Amount),0)Amount from loanguar WHERE loanno='" + txtLoanNo.Text + "'");
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        Amount = Convert.ToDouble(dr3["Amount"]);//.ToString();
                    }
                }
                dr3.Close(); dr3.Dispose(); dr3 = null;
                dr6 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(SUM(Balance),0)Balance from COLLOANGUAR WHERE loanno='" + txtLoanNo.Text + "'");
                if (dr6.HasRows)
                {
                    while (dr6.Read())
                    {
                        Balance = Convert.ToDouble(dr6["Balance"]);
                    }
                }
                dr6.Close(); dr6.Dispose(); dr6 = null;
                Math.Round(SUMAmnt = Math.Round(Amount + Balance), 2);
                txtAmtGuaranteed.Text = SUMAmnt.ToString();
                if (Convert.ToDouble(txtAmtGuaranteed.Text) == Convert.ToDouble(txtAmtApproved.Text))
                {
                    btnAddGuar.Enabled = false;
                    btnCollaterals.Enabled = false;
                    WARSOFT.WARMsgBox.Show("The amount to guaranteed has been reached."); return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Save_Guarantor(string memberno, string Loanno, double Amount, double balance, string auditid, DateTime TransDate)
        {
            string indb = "Set DateFormat DMY Exec Save_LoanGuar '" + memberno + "','" + Loanno + "'," + Amount + "," + balance + ",'" + auditid + "','" + TransDate + "'";
            new WARTECHCONNECTION.cConnect().WriteDB(indb);

            //'The various guarantor stories
            //--------------------------------- Dim rsMyLoanees As ADODB.Recordset
            double GuarLBalance = 0; double GuarArrears = 0; string HisGuarLoanNo = ""; string HisGuarArrears = "";
            WARTECHCONNECTION.cConnect oRst = new WARTECHCONNECTION.cConnect();
            dr = oRst.ReadDB("Select isnull(lb.balance,0) as LoanBalances,vw.intrOwed,isnull(vw.arrears,0) as Status,isnull(vw.ElapsedMonths,0) as EPeriod,isnull(vw.repayperiod,0) as RPeriod from loanbal lb inner join vwdefauters vw on vw.loanno=lb.loanno where lb.memberno='" + txtGuarNo.Text + "'");
            if (dr.HasRows)
                while (dr.Read())
                {
                    double LoanBalances = Convert.ToDouble(dr["LoanBalances"].ToString());// + GuarLBalance;
                    int EPeriod = Convert.ToInt32(dr["EPeriod"].ToString());
                    int RPeriod = Convert.ToInt32(dr["RPeriod"].ToString());
                    double IntrOwed = Convert.ToDouble(dr["RPeriod"].ToString());
                    double Status = Convert.ToDouble(dr["Status"].ToString());
                    GuarLBalance = GuarLBalance + LoanBalances;
                    if (EPeriod > RPeriod)
                    {
                        GuarArrears = LoanBalances + IntrOwed + GuarArrears;
                    }
                    else
                    {
                        GuarArrears = Status + GuarArrears;
                    }
                    WARTECHCONNECTION.cConnect rsMyLoanees = new WARTECHCONNECTION.cConnect();
                    dr1 = rsMyLoanees.ReadDB("Select vw.Loanno,vw.arrears from vwdefauters vw inner join loanguar lg on vw.loanno=lg.loanno where lg.memberno='" + txtGuarNo.Text + "'");
                    if (dr1.HasRows)
                        while (dr1.Read())
                        {
                            HisGuarLoanNo = dr1["LoanNo"].ToString();
                            HisGuarArrears = dr1["Arrears"].ToString();
                            string todatabase = "Insert into TempStatus(AppliedLoanno,GuarMemberno,GuarLoanbalance,HisArrears,HisGuarLoanNo,HisGuarArrears) Values('" + txtLoanNo.Text + "','" + txtGuarNo.Text + "'," + GuarLBalance + "," + GuarArrears + ",'" + HisGuarLoanNo + "'," + HisGuarArrears + ")";
                            new WARTECHCONNECTION.cConnect().WriteDB(todatabase);
                        }
                    else
                    {
                        HisGuarLoanNo = "";
                        HisGuarArrears = "0";
                        string todatabase = "Insert into TempStatus(AppliedLoanno,GuarMemberno,GuarLoanbalance,HisArrears,HisGuarLoanNo,HisGuarArrears) Values('" + txtLoanNo.Text + "','" + txtGuarNo.Text + "'," + GuarLBalance + "," + GuarArrears + ",'" + HisGuarLoanNo + "'," + HisGuarArrears + ")";
                        new WARTECHCONNECTION.cConnect().WriteDB(todatabase);
                    }
                    dr1.Close(); dr1.Dispose(); dr1 = null; rsMyLoanees.Dispose(); rsMyLoanees = null;
                }
            else
            {
                GuarLBalance = 0;
                GuarArrears = 0;
            }
            dr.Close(); dr.Dispose(); dr = null; oRst.Dispose(); oRst = null;

        }
        protected void btnCollaterals_Click(object sender, EventArgs e)
        {
            refreshgridview(txtLoanNo.Text.Trim());
            Populatecolaterals();
            ShowClollaterals();
            LoadCollaterals();
            TextBox1.Text = "NO";
            txtAmtAllocated.Text = "";
        }
        private void refreshgridview(string Loanno)
        {
            try
            {
                string sehhh = "select Guarmemberno [Guarantor Member No],guarnames [Guarantor Names],GuarAmount [Guaranteed Amount ],guarBalance [Guaranteed Balance] from vwLoanGuarantors where loanno='" + Loanno + "'";
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
        private void Populatecolaterals()
        {
            cboCollateralCode.Items.Add("");
            string col = "SELECT ColCode, Coldescription, Percentage FROM COLLATERALS";
            dr = new WARTECHCONNECTION.cConnect().ReadDB(col);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboCollateralCode.Items.Add(dr["ColCode"].ToString());
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }
        protected void cboCollateralCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("SELECT ColCode, Coldescription, Percentage FROM COLLATERALS where ColCode='" + cboCollateralCode.Text + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Double consideredamnt = 0;
                    txtCollateralDescription.Text = dr["Coldescription"].ToString();
                    Label24.Visible = false;
                    TextBox3.Visible = false;
                    if (cboCollateralCode.Text == "CL001")
                    {
                        Label24.Visible = true;
                        TextBox3.Visible = true;
                    }
                    if (cboCollateralCode.Text == "CL007")
                    {
                        Label24.Visible = true;
                        TextBox3.Visible = true;
                    }
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
            ShowClollaterals();
        }
        protected void txtMarketValue_TextChanged(object sender, EventArgs e)
        {
            dr12 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT ColCode, Coldescription, Percentage FROM COLLATERALS where ColCode='" + cboCollateralCode.Text + "'");
            if (dr12.HasRows)
            {
                while (dr12.Read())
                {
                    Double Parcent = Convert.ToDouble(dr12["Percentage"]);//.ToString();
                    Double mkrtvalue = Convert.ToDouble(txtMarketValue.Text);
                    Double consideredamnt = 0;
                    consideredamnt = Parcent * mkrtvalue / 100;
                    txtConsideredValue.Text = consideredamnt.ToString();
                }
            }
            dr12.Close(); dr12.Dispose(); dr12 = null;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label23.Visible = true;
            DropDownList1.Visible = true;
            TextBox2.Visible = true;
            Button2.Visible = true;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Choose what you want to search by!");
            }
            else if (TextBox2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter Names/IDNo!");
            }
            else
            {
                GridView1.Visible = false;
                try
                {

                    if (DropDownList1.Text == "Names")
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where surname like'%" + TextBox1.Text + "%' or OtherNames like'%" + TextBox1.Text + "%'");
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                    else if (DropDownList1.Text == "IDNo")
                    {

                        da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where IDNo ='" + TextBox1.Text + "'");
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No Details to Show");
                    }
                }
                catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

            }

        }
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGuarNo.Text = GridView2.SelectedRow.Cells[1].Text;
            LoadMemberGuarDetails();
            Label23.Visible = false;
            DropDownList1.Visible = false;
            TextBox2.Visible = false;
            Button2.Visible = false;
            GridView2.Visible = false;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            dr12 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT ColCode, Coldescription, Percentage FROM COLLATERALS where ColCode='" + cboCollateralCode.Text + "'");
            if (dr12.HasRows)
            {
                while (dr12.Read())
                {
                    Double Parcent = Convert.ToDouble(dr12["Percentage"]);//.ToString();
                    Double mkrtvalue = 0;
                    double noofshares = Convert.ToDouble(TextBox3.Text);
                    Double consideredamnt = 0;
                    txtConsideredValue.Text = consideredamnt.ToString();
                    if (cboCollateralCode.Text == "CL001")
                    {
                        mkrtvalue = 160 * noofshares;
                        consideredamnt = Parcent * mkrtvalue / 100;
                        txtMarketValue.Text = mkrtvalue.ToString();
                        txtConsideredValue.Text = consideredamnt.ToString();
                    }
                    if (cboCollateralCode.Text == "CL007")
                    {
                        mkrtvalue = 17 * noofshares;
                        consideredamnt = Parcent * mkrtvalue / 100;
                        txtMarketValue.Text = mkrtvalue.ToString();
                        txtConsideredValue.Text = consideredamnt.ToString();
                    }
                }
            }
            dr12.Close(); dr12.Dispose(); dr12 = null;
        }
    }
}