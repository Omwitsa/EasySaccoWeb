using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace USACBOSA.LoansAdmin
{
    public partial class LoanSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        protected void cmdGenerate_Click(object sender, EventArgs e)
        {
            if (txtLoanNo1.Text == "" || txtMemberNo.Text == "" || txtAmount.Text == "" || txtIntRate.Text == "" || txtPeriod.Text == "" || txtStartDate.Text == "")
            {
                WARSOFT.WARMsgBox.Show("please enter all Data");

            }
            else
            {
                try
                {
                    if (txtAmount.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Enter an Amount");
                        txtAmount.Focus();
                        return;
                    }
                    else if (txtIntRate.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Enter Interest Rate");
                        txtIntRate.Focus();
                        return;
                    }
                    if (optAuto.Checked == true && txtPeriod.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Enter Repayment Period");
                        txtPeriod.Focus();
                        return;
                    }
                    string msqll = "delete from loanschd";
                    new WARTECHCONNECTION.cConnect().WriteDB(msqll);
                    double LBalance = 0;
                    string rmethod = "";

                    if (optAmortized.Checked == true)
                    {
                        rmethod = "AMRT";
                    }
                    if (optReducing.Checked == true)
                    {
                        rmethod = "RBAL";
                    }
                    if (optStraight.Checked == true)
                    {
                        rmethod = "STL";
                    }
                    GetLoanSchedule(Convert.ToDouble(txtAmount.Text), Convert.ToDouble(txtIntRate.Text), rmethod, Convert.ToInt32(txtPeriod.Text), LBalance, txtLoanNo1.Text, txtMemberNo.Text.Trim());
                    WARSOFT.WARMsgBox.Show("Loan Schedule Generated sucessfully");
                }
                catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
            }
        }

        private void GetLoanSchedule(double initialAmount, double rrate, string rmethod, int rperiod, double LBalance, string vote, string memberno)
        {

            try
            {
                DateTime DateDeposited = Convert.ToDateTime(txtStartDate.Text);
                string myDateDeposited = "";
                double totalrepayable = 0;
                double minterest = 0;
                double mPrincipal = 0;
                double mAmount = 0;
                int m = rperiod;
                int periodd = 0;
                LBalance = initialAmount;
                if (rmethod == "AMRT")
                {
                    for (m = 0; m < (rperiod); m++)
                    {
                        periodd = m + 1;
                        totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                        double mrepayment = Pmt(rrate, rperiod, initialAmount, 0);
                        minterest = rrate / 12 / 100 * (LBalance);// 'Interest owed is loaded

                        mPrincipal = (mrepayment - minterest);
                        mAmount = mPrincipal + minterest;

                        DateDeposited = DateDeposited.AddMonths(1);
                        string myDate = Convert.ToString(DateDeposited);
                        if (myDate.Contains(' '))
                        {
                            string[] newdate = myDate.Split(' ');
                            myDateDeposited = newdate[0];
                        }
                        LBalance = (LBalance - mPrincipal);
                        totalrepayable = Math.Round(LBalance, 2);
                        mPrincipal = Math.Round(mPrincipal, 2);
                        minterest = Math.Round(minterest, 2);
                        string ISERDATA = "insert  into LOANSCHD(MemberNo,Period,Principal,Interest,Balance,Comments,FmtPer,contrib,Sharebalance)values('" + memberno + "','" + periodd + "','" + mPrincipal + "','" + minterest + "','" + totalrepayable + "','','" + myDateDeposited + "','','')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    }
                }
                if (rmethod == "STL")
                {
                    for (m = 0; m < (rperiod); m++)
                    {
                        periodd = m + 1;
                        totalrepayable = LBalance + (LBalance * (rrate / 12 / 100) * rperiod);
                        mPrincipal = (initialAmount / rperiod);
                        minterest = (initialAmount * (rrate / 12 / 100));
                        mAmount = mPrincipal + minterest;
                        //RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                        DateDeposited = DateDeposited.AddMonths(1);

                        string myDate = Convert.ToString(DateDeposited);
                        if (myDate.Contains(' '))
                        {
                            string[] newdate = myDate.Split(' ');
                            myDateDeposited = newdate[0];
                        }

                        totalrepayable = totalrepayable - mAmount;
                        LBalance = LBalance - mPrincipal;
                        totalrepayable = Math.Round(LBalance, 2);
                        mPrincipal = Math.Round(mPrincipal, 2);
                        minterest = Math.Round(minterest, 2);
                        string ISERDATA = "insert  into LOANSCHD(MemberNo,Period,Principal,Interest,Balance,Comments,FmtPer,contrib,Sharebalance)values('" + memberno + "','" + periodd + "','" + mPrincipal + "','" + minterest + "','" + totalrepayable + "','','" + myDateDeposited + "','','')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    }
                }
                if (rmethod == "RBAL")
                {
                    LBalance = initialAmount;
                    for (int i = 1; i <= rperiod; i++)
                    {
                        periodd = i;
                        mPrincipal = (initialAmount / rperiod);
                        // 'PrincAmount = PrincAmount + Principal
                        minterest = ((rrate / 12 / 100) * LBalance);
                        //intTotal = intTotal + minterest;
                        DateDeposited = DateDeposited.AddMonths(1);
                        string myDate = Convert.ToString(DateDeposited);
                        if (myDate.Contains(' '))
                        {
                            string[] newdate = myDate.Split(' ');
                            myDateDeposited = newdate[0];
                        }
                        LBalance = (LBalance - mPrincipal);
                        totalrepayable = Math.Round(LBalance, 2);
                        mPrincipal = Math.Round(mPrincipal, 2);
                        minterest = Math.Round(minterest, 2);
                        string ISERDATA = "insert  into LOANSCHD(MemberNo,Period,Principal,Interest,Balance,Comments,FmtPer,contrib,Sharebalance)values('" + memberno + "','" + periodd + "','" + mPrincipal + "','" + minterest + "','" + totalrepayable + "','','" + myDateDeposited + "','','')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ToString());
                string myquery = "";
                SqlCommand SqlCommand;
                SqlDataReader reader;
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Open the connection to db
                conn.Open();
                myquery = "select Period,FmtPer AS [Payment Date],(Principal+Balance) AS [Opening Balance],Principal,Interest,(Principal+Interest)AS Payment,Balance AS [Closing Balance] from LOANSCHD order by period";
                SqlCommand = new SqlCommand(myquery, conn);
                adapter.SelectCommand = new SqlCommand(myquery, conn);

                //execute the query
                reader = SqlCommand.ExecuteReader();

                //Assign the results 
                GridView1.DataSource = reader;

                //Bind the data
                GridView1.DataBind();
                conn.Close();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cmdHide_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void cmdprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Provide MemberNo!");
                }
                else
                {
                    Session["Memberno"] = txtMemberNo.Text;
                    Response.Redirect("~/Reports/LoanScheduleReport.aspx", false);
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void ExportToExcel(System.ComponentModel.MarshalByValueComponent DataSource)
        {
            try
            {
                System.IO.StringWriter objStringWriter = new System.IO.StringWriter();
                System.Web.UI.WebControls.DataGrid tempDataGrid = new System.Web.UI.WebControls.DataGrid();
                System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Appraised Loans.xls");
                tempDataGrid.DataSource = DataSource;
                tempDataGrid.DataBind();
                tempDataGrid.HeaderStyle.Font.Bold = true;
                tempDataGrid.RenderControl(objHtmlTextWriter);
                DataSource.Dispose();
                HttpContext.Current.Response.Write(objStringWriter.ToString());
                HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void optAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (optAuto.Checked == true)
            {
                optConstants.Checked = false;
            }
        }

        protected void optConstants_CheckedChanged(object sender, EventArgs e)
        {
            if (optConstants.Checked == true)
            {
                optAuto.Checked = false;
            }
        }

        protected void optEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (optEnd.Checked == true)
            {
                optStart.Checked = false;
            }
        }

        protected void optStart_CheckedChanged(object sender, EventArgs e)
        {
            if (optStart.Checked == true)
            {
                optEnd.Checked = false;
            }
        }

        protected void optAmortized_CheckedChanged(object sender, EventArgs e)
        {
            if (optAmortized.Checked == true)
            {
                optStraight.Checked = false;
                optReducing.Checked = false;
            }
        }

        protected void optReducing_CheckedChanged(object sender, EventArgs e)
        {
            if (optReducing.Checked == true)
            {
                optStraight.Checked = false;
                optAmortized.Checked = false;
            }
        }

        protected void optStraight_CheckedChanged(object sender, EventArgs e)
        {
            if (optStraight.Checked == true)
            {
                optReducing.Checked = false;
                optAmortized.Checked = false;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}