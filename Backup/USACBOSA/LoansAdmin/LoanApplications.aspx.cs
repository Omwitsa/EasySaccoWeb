using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.LoansAdmin
{
    public partial class LoanApplications : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4,dr5,dr6, DR, Dr, dr12, dr13, dr7, dr8;
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
               
                PopulateLoanTypes();
                btnOK.Visible = false;
                btnCancel.Visible = false;
                chkTopUp.Visible = false;
            }
        }

        private void PopulateLoanTypes()
        {
            try
            {
                
                cboLoanCode.Items.Add("");
                WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
                string ppplll = "Select Loancode From LoanType";
                dr = pploan.ReadDB(ppplll);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboLoanCode.Items.Add(dr["Loancode"].ToString().Trim());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; };
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                string memebr = "Select MEMBERS.IDNo,MEMBERS.Surname,MEMBERS.OtherNames,shares.Totalshares From MEMBERS INNER JOIN shares ON MEMBERS.MemberNo=shares.MemberNo WHERE shares.MemberNo='" + txtMemberNo.Text + "'";
                dr = cooon.ReadDB(memebr);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string surname = dr["Surname"].ToString();
                        txtNames.Text = dr["OtherNames"].ToString();
                        txtNames.Text = surname + " " + txtNames.Text;
                        txtTotalShares1.Text = dr["Totalshares"].ToString();
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void TextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            int ttotalshare = 0;
            double ShareCap = 0;
            Cleartexts();
            try
            {
                int MATURITY = 0;
                DateTime applicdate = DateTime.Today;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select ApplicDate from MEMBERS where MemberNo='" + txtMemberNo.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        applicdate = Convert.ToDateTime(dr["applicdate"]);
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The Member does not exist");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select MATURITY from SYSPARAM");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MATURITY = Convert.ToInt32(dr["MATURITY"]);
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                DateTime date1 = System.DateTime.Today;
                DateTime date2 = applicdate;

                int DateDiff = ((date1.Year - date2.Year) * 12) + (date1.Month - date2.Month);
                //if (DateDiff < 6)
                //{
                //    WARSOFT.WARMsgBox.Show("Member is only " + DateDiff + " months  Old in the society. He is not matured enough to get a loan");
                //    return;
                //}

                WARTECHCONNECTION.cConnect alldda = new WARTECHCONNECTION.cConnect();
                dr = alldda.ReadDB("select sum(isnull(c.amount,0))sharecap,m.surname+' '+m.othernames Names  from contrib c inner join MEMBERS m on c.memberno=m.memberno where c.sharescode ='D' and m.MemberNo='" + txtMemberNo.Text + "' group by m.surname,m.othernames");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Double a;
                        txtNames.Text = dr["Names"].ToString();
                        ShareCap = Convert.ToDouble(dr["sharecap"].ToString());
                        a = ShareCap * 3;
                        // txtMaxLoanAmount.Text = a.ToString();
                        txtTotalShares1.Text = ShareCap.ToString();
                        WARTECHCONNECTION.cConnect alldda1 = new WARTECHCONNECTION.cConnect();
                        dr12 = alldda1.ReadDB("select sum(isnull(c.amount,0))Amount,m.surname+' '+m.othernames Names  from contrib c inner join MEMBERS m on c.memberno=m.memberno where c.sharescode ='SC' and m.MemberNo='" + txtMemberNo.Text + "' group by m.surname,m.othernames");
                        if (dr12.HasRows)
                        {
                            while (dr12.Read())
                            {
                                txtMaturedShares.Text = dr12["Amount"].ToString();

                            }
                        }
                        dr12.Close(); dr12.Dispose();
                        WARTECHCONNECTION.cConnect Gate = new WARTECHCONNECTION.cConnect();
                        dr13 = Gate.ReadDB("Select isnull(Sum(Balance),0) Balance from LOANBAL Where MemberNo='" + txtMemberNo.Text + "'");
                        if (dr13.HasRows)
                        {
                            while (dr13.Read())
                            {
                                Double loanbal = Convert.ToDouble(dr13["Balance"]);
                                Double capital = Convert.ToDouble(txtTotalShares1.Text);
                                TxttotAmount.Text = capital.ToString();
                                Double maturity = capital * 3;
                                Double maturity1 = Math.Round(maturity, 2);
                                Double loanbal1 = Math.Round(loanbal, 2);
                                Double maxloan = maturity1 - loanbal1;
                                txtMaxLoanAmount.Text = maxloan.ToString();
                            }
                        }
                        else
                        {
                            Double capital = Convert.ToDouble(txtTotalShares1.Text);
                            Double capital1 = Math.Round(capital, 2);
                            TxttotAmount.Text = capital1.ToString();
                            Double maturity = capital * 3;
                            Double maturity1 = Math.Round(maturity, 2);
                            txtMaxLoanAmount.Text = maturity1.ToString();
                        }
                    }
                }
                //else
                //{
                //    WARSOFT.WARMsgBox.Show("The member has not made any Contribution to the sacco hence cannot apply for a Loan.");
                //    return;
                //}
                //dr.Close(); dr.Dispose(); dr = null; alldda.Dispose(); alldda = null;

                double LoanBalance = 0;
                WARTECHCONNECTION.cConnect countloans = new WARTECHCONNECTION.cConnect();
                dr = countloans.ReadDB("select isnull(sum(balance),0) as LoanBalance,count(loanno) as LoanCount from loanbal where memberno='" + txtMemberNo.Text + "' and balance>0");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtOutstandingLoan.Text = dr["LoanCount"].ToString();
                        txtOutstandingLoanBal.Text = dr["LoanBalance"].ToString();
                        LoanBalance = Convert.ToDouble(dr["LoanBalance"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; countloans.Dispose(); countloans = null;

                WARTECHCONNECTION.cConnect allshare = new WARTECHCONNECTION.cConnect();
                string sql = ("SELECT (SELECT ISNULL(SUM(amount), 0)as amount FROM  CONTRIB WHERE (MemberNo = '" + txtMemberNo.Text + "') AND (sharescode =(SELECT sharescode FROM sharetype WHERE usedtoguaranTee = 1))) AS Shares,loantoshareratio FROM sharetype WHERE(SharesCode = (SELECT sharescode  FROM  sharetype WHERE usedtoguaranTee = 1))");
                dr = allshare.ReadDB(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        double TotalShares = Convert.ToDouble(dr["Shares"].ToString());
                        double LoanToShare = Convert.ToDouble(dr["loantoshareratio"].ToString());
                        txtMaxLoanAmount.Text = ((LoanToShare * TotalShares) - LoanBalance).ToString();
                        txtTotalShares1.Text = TotalShares.ToString();
                        txtMaturedShares.Text = TotalShares.ToString();
                        break;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; allshare.Dispose(); allshare = null;
                double onewtotalinterest = 0;
                double onewTotalPrinciple = 0;
                double onewCurrentTotalDeductions = 0;
                WARTECHCONNECTION.cConnect conLoan = new WARTECHCONNECTION.cConnect();
                string loannno = "SELECT LOANNO FROM LOANBAL WHERE MEMBERNO='" + txtMemberNo.Text + "' AND BALANCE>0";
                dr2 = conLoan.ReadDB(loannno);
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        string Loanno = dr2["LOANNO"].ToString();
                        Calculate_Loan_Repayment oCalculate_Loan_Repayment = new Calculate_Loan_Repayment();

                        onewCurrentTotalDeductions = oCalculate_Loan_Repayment.getCalculatedLoanRepayment(Loanno);
                        onewtotalinterest = oCalculate_Loan_Repayment.gettotalinterest(Loanno);
                        onewTotalPrinciple = oCalculate_Loan_Repayment.getTotalPrinciple(Loanno);
                        //  oCalculate_Loan_Repayment.getTotalPrinciple();
                        txtTotalMontlyInstallment.Text = onewCurrentTotalDeductions.ToString();
                        txtMonthlyPrincipal.Text = onewTotalPrinciple.ToString();
                        txtMonthlyIntrest.Text = onewtotalinterest.ToString();
                    }
                LoadLoans();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Cleartexts()
        {
            txtMonthlyIntrest.Text = "0.00";
            txtMonthlyPrincipal.Text = "0.00";
            txtMonthlyPrincipal.Text = "";
            txtOutstandingLoan.Text = "";
            txtOutstandingLoanBal.Text = "";
            txtRepaymethod.Text = "";
            txtRepayPeriod.Text = "";
            txtTotalMontlyInstallment.Text = "";
            txtTotalShares1.Text = "";
            TxttotAmount.Text = "";
        }
        private void LoadLoans()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT LOANS.LoanNo [Loan No],loans.ApplicDate [Applic Date],Loans.LoanAmt Amount,isnull(LOANBAL.Balance,Loans.LoanAmt)BALANCE,loans.RepayPeriod [Repay Period], isnull(LOANBAL.LoanCode,Loans.LoanCode) [Loan Code],LOANTYPE.LoanType [Loan Type] FROM LOANBAL RIGHT JOIN (LOANS INNER JOIN LOANTYPE ON LOANS.LoanCode = LOANTYPE.LoanCode) ON LOANBAL.LoanNo = LOANS.LoanNo WHERE LOANS.MemberNo='" + txtMemberNo.Text + "' ");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member number is required");
                    return;
                }
                if (txtNames.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Member names are required");
                    return;
                }
                if (txtLoanNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Loan number is required");
                    return;
                }
                if (txtLoanAmount.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Loan Amount number is required");
                    txtLoanAmount.Focus();
                    return;
                }
                if(txtRepayPeriod.Text=="")
                {
                    WARSOFT.WARMsgBox.Show("Repay period required");
                    txtRepayPeriod.Focus();
                }
                if (txtApplicationDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Application Date is required");
                    txtApplicationDate.Focus();
                    return;
                }
                
                double MaxLoanAmt = Convert.ToDouble(txtMaxLoanAmount.Text);
                double LoanAmount = Convert.ToDouble(txtLoanAmount.Text);
                //if (MaxLoanAmt < LoanAmount)
                //{
                //    WARSOFT.WARMsgBox.Show("The Loan applied should not exceed the maximum loan amount");
                //    return;
                //}
                WARTECHCONNECTION.cConnect rrate = new WARTECHCONNECTION.cConnect();
                string rrat = "select RepayPeriod,LoanCode from LoanType where LoanCode='" + cboLoanCode.Text + "'";
                dr1 = rrate.ReadDB(rrat);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        int RepayPeriod = Convert.ToInt32(dr1["RepayPeriod"].ToString());
                        int Period = Convert.ToInt32(txtRepayPeriod.Text);

                        if (RepayPeriod < Period)
                        {
                            WARSOFT.WARMsgBox.Show("The Period should not exceed the maximum Repay period");
                            return;
                        }
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; rrate.Dispose(); rrate = null;
                WARTECHCONNECTION.cConnect LCode = new WARTECHCONNECTION.cConnect();
                string LoanCodeExist = "select * from Loans  where LoanNo='" + txtLoanNo.Text + "'";
                dr3 = LCode.ReadDB(LoanCodeExist);
                if (dr3.HasRows)
                    while (dr3.Read())
                    {
                        WARSOFT.WARMsgBox.Show("The Loan with the same Loan Number Exist, try another Loan");
                        return;
                    }
                dr3.Close(); dr3.Dispose(); dr3 = null; LCode.Dispose(); LCode = null;

                double intererest = 0;
                double.TryParse(txtInterest.Text, out intererest);
                string apply = "Set dateformat dmy insert into Loans(LoanNo,MemberNo,LoanCode,ApplicDate,LoanAmt,RepayPeriod,RepayMethod,Interest,Status,TransactionNo)values('" + txtLoanNo.Text + "','" + txtMemberNo.Text + "','" + cboLoanCode.Text + "','" + txtApplicationDate.Text + "','" + txtLoanAmount.Text + "','" + txtRepayPeriod.Text + "','" + txtRepaymethod.Text + "','" + intererest + "','1','')";
                new WARTECHCONNECTION.cConnect().WriteDB(apply);

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('Loan Application','Loan application loanno " + txtLoanNo.Text.Trim() + "','" + txtApplicationDate.Text.Trim() + "','" + txtLoanAmount.Text.Trim() + "','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                LoadLoans();
                WARSOFT.WARMsgBox.Show("Loan application details saved sucessfully");
               // return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtMemberNo_DataBinding(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect alldda = new WARTECHCONNECTION.cConnect();
                dr = alldda.ReadDB("select MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex from Members where MemberNo='" + txtMemberNo.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtNames.Text = dr["Surname"].ToString() + " " + dr["OtherNames"].ToString();
                        break;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; alldda.Dispose(); alldda = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }


        private void getLoanNo(string LoanCode, string MemberNo)
        {
            try
            {
                WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
                string ppplll = "select count(loanno)as LoanCount from loanbal where memberno='" + txtMemberNo.Text.Trim() + "' and loancode='" + cboLoanCode.Text.Trim() + "'";
                dr = pploan.ReadDB(ppplll);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int LoanCount = Convert.ToInt32(dr["LoanCount"].ToString());
                        if (LoanCount > 0)
                        {
                            txtLoanNo.Text = cboLoanCode.Text + txtMemberNo.Text + '-' + (LoanCount + 1);
                        }
                        else
                        {
                            txtLoanNo.Text = cboLoanCode.Text + txtMemberNo.Text;
                        }
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtLoanNo.Text = GridView1.SelectedRow.Cells[2].Text;
                txtLoanAmount.Text = GridView1.SelectedRow.Cells[4].Text;
                txtRepayPeriod.Text = GridView1.SelectedRow.Cells[6].Text;
                cboLoanCode.Text = GridView1.SelectedRow.Cells[7].Text;
                loadloantypes();
            }
            catch (Exception ex) { }
        }

        private void loadloantypes()
        {
            WARTECHCONNECTION.cConnect ltypes = new WARTECHCONNECTION.cConnect();
            string ppplll1 = "Select Loancode,Loantype,Interest,Repaymethod From LoanType where Loancode='" + cboLoanCode.Text + "' ";
            dr7 = ltypes.ReadDB(ppplll1);
            if (dr7.HasRows)
            {
                while (dr7.Read())
                {
                   txtLoanType.Text=dr7 ["Loantype"].ToString().Trim();
                   txtInterest.Text = dr7["Interest"].ToString().Trim();
                   txtRepaymethod.Text = dr7["Repaymethod"].ToString().Trim();
                }
            }
            dr7.Close(); dr7.Dispose(); dr7 = null; ltypes.Dispose(); ltypes = null;
        }

        protected void btnApplications_Click(object sender, EventArgs e)
        {
            
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member number is required");
                    txtMemberNo.Focus();
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    chkTopUp.Visible = true;
                    return;
                }
                if (txtNames.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Member names are required");
                    txtNames.Focus();
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    chkTopUp.Visible = true;
                    return;
                }
                if (txtLoanNo.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Loan number is required");
                    txtLoanNo.Focus();
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    chkTopUp.Visible = true;
                    return;
                }
                if (txtLoanAmount.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Loan Amount number is required");
                    txtLoanAmount.Focus();
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    chkTopUp.Visible = true;
                    return;
                }
                double MaxLoanAmt = Convert.ToDouble(txtMaxLoanAmount.Text);
                double LoanAmount = Convert.ToDouble(txtLoanAmount.Text);
                if (MaxLoanAmt < LoanAmount)
                {
                    WARSOFT.WARMsgBox.Show("The Loan applied should not exceed the maximum loan amount");
                    txtLoanAmount.Focus();
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    chkTopUp.Visible = true;
                    return;
                }
                double preloan = 0;
                string retval="OK";
                string Bridg_LoanNo = "";
                double loanbalance = 0;
                double interest =0;
                int BridgeType = 0;
                
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {
                        Bridg_LoanNo = GridView1.Rows[i].Cells[1].Text;
                        loanbalance = Convert.ToDouble(GridView1.Rows[i].Cells[3].Text);
                       Math.Round( preloan = loanbalance + preloan,2);
                        interest = Convert.ToDouble(GridView1.Rows[i].Cells[5].Text);

                        if (chkTopUp.Checked == true)
                        {
                            BridgeType = 2;
                        }
                        else
                        {
                            BridgeType = 1;
                        }
                        retval = "OK";
                    }
                }
                if (retval == "OK")
                {
                    double LAmnt=Convert.ToDouble(txtLoanAmount.Text);
                    if (preloan > LAmnt)
                    {
                        if (chkTopUp.Checked == true)
                        {
                            // the loan here is top up for another loan
                        }
                        else
                        {
                            WARSOFT.WARMsgBox.Show("The loan amount should be >= " + preloan + " For you to refinance");
                            txtLoanAmount.Focus();
                            btnOK.Visible = true;
                            btnCancel.Visible = true;
                            chkTopUp.Visible = true;
                            return;
                        }
                    }
                    new WARTECHCONNECTION.cConnect().WriteDB("Delete from BRIDGINGLOAN where LoanNo='" + txtLoanNo.Text.Trim()+ "'");
                    Save_Bridging_Loan(txtLoanNo.Text, Bridg_LoanNo, loanbalance, interest, BridgeType, (Convert.ToDateTime(txtApplicationDate.Text)), (Convert.ToDouble(txtOutstandingLoanBal.Text)), (Convert.ToDouble(txtInterest.Text)));//, int BridgeType, string auditid,string ErrorMsg);
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
            GridView1.Visible = false;
            btnCancel.Visible = false;
            btnOK.Visible = false;
            chkTopUp.Visible = false;
        }

        private void Save_Bridging_Loan(string Loanno, string Bridg_LoanNo, double loanbalance, double interest, int BridgeType, DateTime ApplicDate, double LoanBal, double Interest)
        {
            string save = "Set DateFormat DMY Exec Save_BridgingLoan '" + Loanno + "','" + ApplicDate + "','" + Bridg_LoanNo + "'," + loanbalance + "," + interest + "," + BridgeType + ",'User'";
            new WARTECHCONNECTION.cConnect().WriteDB(save);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GridView1.Visible = false;
                btnCancel.Visible = false;
                btnOK.Visible = false;
                chkTopUp.Visible = false;
             }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void chkTopUp_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cboLoanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLoanNo(cboLoanCode.Text, txtMemberNo.Text);
            bool bridging = false;
            WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
            string ppplll = "Select LoanCode,LoanType,RepayPeriod,Interest,Repaymethod,bridging From LoanType where LoanCode='" + cboLoanCode.Text + "'";
            dr = pploan.ReadDB(ppplll);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtLoanType.Text = dr["LoanType"].ToString().Trim();
                    txtRepayPeriod.Text = dr["RepayPeriod"].ToString().Trim();
                    txtInterest.Text = dr["Interest"].ToString().Trim();
                    txtRepaymethod.Text = dr["Repaymethod"].ToString().Trim();
                    bridging = Convert.ToBoolean(dr["bridging"].ToString());
                }
            }
            dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
            if (bridging == true)
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT lb.LoanNo,lb.LoanCode,lb.Balance,c.amount,lb.interest,v.percentagePaid as pcgPaid,v.topUp,v.Bridge FROM LOANBAL lb inner join vwBridgeCondition V on lb.loanno=v.loanno inner join LoanType lt on lb.loancode=lt.loancode inner join cheques c on lb.loanno=c.loanno WHERE (lb.MemberNo = '" + txtMemberNo.Text + "') AND (lb.LoanCode IN(SELECT loantobridge FROM loanbridgetype WHERE loancode = '" + cboLoanCode.Text + "')) AND (lb.Balance > 0)");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            checkloanbal();
            btnOK.Visible = true;
            btnCancel.Visible = true;
            chkTopUp.Visible = true;
        }

        private void checkloanbal()
        {
            Double loanamnts = 0; Double loanbalances = 0; Double differences = 0;
            WARTECHCONNECTION.cConnect Lexists = new WARTECHCONNECTION.cConnect();
            string LoanExist1 = "Select * from loans where MemberNo='" + txtMemberNo.Text + "' ";//and Loanno like'%"+cboLoanCode.Text+"%'
            dr5 = Lexists.ReadDB(LoanExist1);
            if (dr5.HasRows)
                while (dr5.Read())
                {
                    Double loanamnts1 = Convert.ToDouble(dr5["LoanAmt"]);
                    loanamnts = Math.Round(loanamnts1, 2);
                }
            dr5.Close(); dr5.Dispose(); dr5 = null; Lexists.Dispose(); Lexists = null;
            WARTECHCONNECTION.cConnect Lexista = new WARTECHCONNECTION.cConnect();
            string LoanExisting = "Select * from loanbal where MemberNo='" + txtMemberNo.Text + "' ";//and Loanno like'%" + cboLoanCode.Text + "%'
            dr6 = Lexista.ReadDB(LoanExisting);
            if (dr6.HasRows)
                while (dr6.Read())
                {
                    Double loanbalances1 = Convert.ToDouble(dr6["Balance"]);
                    loanbalances = Math.Round(loanbalances1, 2);
                }
            dr6.Close(); dr6.Dispose(); dr6 = null; Lexista.Dispose(); LoanExisting = null;

            Double differences1 = loanamnts / 2;
            differences = Math.Round(differences1, 2);
            if (loanbalances > differences)
            {
                WARSOFT.WARMsgBox.Show("You haven't Repayed upto half of your existing Loan!");
            }
            else
            {
                return;
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (txtMemberNo.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter MemberNo to Proceed!");
            }
            else if(txtLoanNo.Text=="")
            {
                WARSOFT.WARMsgBox.Show("Select LOAN to Proceed!");
            }
            else if (txtRepayPeriod.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter RepayPeriod to Proceed!");
            }
            else if (txtLoanAmount.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter Amount Applied");//0789817823
            }
            else
            {
                WARTECHCONNECTION.cConnect appraised = new WARTECHCONNECTION.cConnect();
                string sql = "Select * from appraisal where loanno='" + txtLoanNo.Text + "'";
                dr8 = appraised.ReadDB(sql);
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        WARSOFT.WARMsgBox.Show("LoanNo " + txtLoanNo.Text + " Already Appraised");
                    }
                    dr8.Close(); dr8.Dispose(); dr8 = null; appraised.Dispose(); appraised = null;
                }
                else
                {
                    string updateloan = "Set dateformat dmy Update LOANS set LoanAmt='" + txtLoanAmount.Text + "',RepayPeriod='" + txtRepayPeriod.Text + "',ApplicDate='" + txtApplicationDate.Text + "' where LoanNo='" + txtLoanNo.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(updateloan);
                    WARSOFT.WARMsgBox.Show("LoanNo " + txtLoanNo.Text + " Updated Successfully");
                }
                // dr8.Close(); dr8.Dispose(); dr8 = null; appraised.Dispose(); appraised = null;
            }
        }

       
    }
}