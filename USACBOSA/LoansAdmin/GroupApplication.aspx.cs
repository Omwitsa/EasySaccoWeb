using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.LoansAdmin
{
    public partial class GroupApplication : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr3;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateLoanTypes();
        }

        private void PopulateLoanTypes()
        {
            try
            {

                loan_code.Items.Add("");
                WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
                string ppplll = "Select Loancode From LoanType";
                dr = pploan.ReadDB(ppplll);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        loan_code.Items.Add(dr["Loancode"].ToString().Trim());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; };
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string loanNo = txtLoanNo.Text.Trim();
                string memberNo = "";
                string loanCode = loan_code.Text.Trim();
                string groupCode = txtGroupCode.Text.Trim();
                DateTime applicationDate = DateTime.UtcNow.AddHours(3);
                decimal.TryParse(txtLoanAmount.Text.Trim(), out decimal loanAmount);
                int.TryParse(repay_period.Text.Trim(), out int repayPeriod);
                string repayMethod = repay_method.Text.Trim();
                decimal.TryParse(txt_interest.Text.Trim(), out decimal interest);
                string transactionNo = "";
                int status = 1;

                if (groupCode == "")
                {
                    WARSOFT.WARMsgBox.Show("Group code is required");
                    return;
                }
                if (loanAmount < 1)
                {
                    WARSOFT.WARMsgBox.Show("Loan Amount number is required");
                    txtLoanAmount.Focus();
                    return;
                }

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

                string apply = "Set dateformat dmy insert into Loans(LoanNo,MemberNo,LoanCode,ApplicDate,LoanAmt,RepayPeriod,RepayMethod,Interest,Status,TransactionNo,GroupCode)values('" + loanNo + "','" + memberNo + "','" + loanCode + "','" + applicationDate + "','" + loanAmount + "','" + repayPeriod + "','" + repayMethod + "','" + interest + "','" + status + "','" + transactionNo + "', '" + groupCode + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(apply);

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('Loan Application','Group Loan application loanno " + txtLoanNo.Text.Trim() + "','" + applicationDate + "','" + txtLoanAmount.Text.Trim() + "','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                LoadLoans();
                Cleartexts();
                WARSOFT.WARMsgBox.Show("Loan application details saved sucessfully");
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void Cleartexts()
        {
            txtLoanNo.Text = "";
            txtGroupCode.Text = "";
            txtLoanAmount.Text = "";
        }

        protected void txtGroupCode_TextChanged(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
            pploan = new WARTECHCONNECTION.cConnect();
            dr = pploan.ReadDB("SELECT * FROM COMPANY WHERE CompanyCode = '" + txtGroupCode.Text + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    groupName.Text = dr["CompanyName"].ToString();
                }
            }
            else
            {
                WARSOFT.WARMsgBox.Show("The group does not exist");
                return;
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        protected void loan_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLoanNo(loan_code.Text, txtGroupCode.Text);
            WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
            string ppplll = "Select LoanCode,LoanType,RepayPeriod,Interest,Repaymethod,bridging From LoanType where LoanCode='" + loan_code.Text + "'";
            dr = pploan.ReadDB(ppplll);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    repay_period.Text = dr["RepayPeriod"].ToString().Trim();
                    txt_interest.Text = dr["Interest"].ToString().Trim();
                    repay_method.Text = dr["Repaymethod"].ToString().Trim();
                }
            }
            dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
        }

        private void GetLoanNo(string LoanCode, string groupCode)
        {
            try
            {
                WARTECHCONNECTION.cConnect pploan = new WARTECHCONNECTION.cConnect();
                string suffix = $"{LoanCode}/{groupCode}/";
                string ppplll = "SELECT count(loanno) AS LoanCount FROM LOANS WHERE LoanNo like '" + suffix + "%'";
                dr = pploan.ReadDB(ppplll);
                int LoanCount = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LoanCount = Convert.ToInt32(dr["LoanCount"].ToString());
                        txtLoanNo.Text = suffix + (LoanCount + 1).ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; pploan.Dispose(); pploan = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void LoadLoans()
        {
            try
            {
                string query = "SELECT LOANS.LoanNo [Loan No],loans.ApplicDate [Applic Date],Loans.LoanAmt Amount,isnull(LOANBAL.Balance,Loans.LoanAmt)BALANCE,loans.RepayPeriod [Repay Period], isnull(LOANBAL.LoanCode,Loans.LoanCode) [Loan Code],LOANTYPE.LoanType [Loan Type] FROM LOANBAL RIGHT JOIN (LOANS INNER JOIN LOANTYPE ON LOANS.LoanCode = LOANTYPE.LoanCode) ON LOANBAL.LoanNo = LOANS.LoanNo WHERE LOANS.memberno='" + txtGroupCode.Text + "' ";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(query);
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

    }
}