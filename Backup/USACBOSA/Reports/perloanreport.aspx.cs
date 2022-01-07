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

namespace USACBOSA.Reports
{
    public partial class perloanreport : System.Web.UI.Page
    {
        ReportDocument rprt = new ReportDocument();

        System.Data.SqlClient.SqlDataReader DR;
        System.Data.SqlClient.SqlDataAdapter da;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //String MemberNo = Session["Memberno"].ToString();
                //da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT LOANBAL.MemberNo, CHEQUES.Amount,MEMBERS.OtherNames, MEMBERS.Surname,LOANBAL.LoanNo,LOANBAL.LoanCode,LOANBAL.duedate,LOANBAL.RepayRate,LOANBAL.LastDate,LOANBAL.IntrOwed,CHEQUES.DateIssued  FROM  BOSA LOANBAL LOANBAL INNER JOIN MEMBERS MEMBERS ON LOANBAL.MemberNo=MEMBERS.MemberNo INNER JOIN CHEQUES CHEQUES ON LOANBAL.LoanNo=CHEQUES.LoanNo");
                //DataSet ds = new DataSet();
                //da.Fill(ds, "loanbal");
                ReportDocument rd = new ReportDocument();
                string path = Server.MapPath("~\\easysacco_reports\\periodicLoanRecovery.rpt");
                rd.Load(path);
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.DataBind();

            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect cnt6 = new WARTECHCONNECTION.cConnect();
            DR = cnt6.ReadDB("SELECT SUPERUSER,branchcode FROM UserAccounts1 WHERE UserLoginID='" + Session["mimi"].ToString() + "'");
            if (DR.HasRows)
            {
                while (DR.Read())
                {
                    int A;

                    A = Convert.ToInt32(DR["SUPERUSER"]);
                    if (A == 1)
                    {
                        Response.Redirect("~/SysAdmin/SystemAdmin.aspx", false);
                    }
                    if (A == 2)
                    {
                        Response.Redirect("~/FinanceAdmin/FinanceAdmin.aspx", false);
                    }
                    if (A == 3)
                    {
                        Response.Redirect("~/LoansAdmin/LoansAdmin.aspx", false);
                    }

                    if (A == 4)
                    {
                        Response.Redirect("~/CustomServAdmin/CustomServAdmin.aspx", false);
                    }
                    if (A == 5)
                    {
                        Response.Redirect("~/ManagementAdmin/ManagementAdmin.aspx", false);
                    }
                    if (A == 6)
                    {
                        Response.Redirect("~/HR_Admin/HR_Admin.aspx", false);
                    }
                }
            }
            DR.Close(); DR.Dispose(); DR = null; cnt6.Dispose(); cnt6 = null;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~\\easysacco_reports\\periodicLoanRecovery.rpt");
            rd.Load(path);
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            rd.ExportToHttpResponse
           (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "periodicLoanRecovery");

        }
        protected void criprt_navigation(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            // CrystalReportViewer1.ReportSource = rd;
        }
    }
}