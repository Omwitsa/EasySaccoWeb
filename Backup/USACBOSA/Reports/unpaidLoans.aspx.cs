using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
namespace USACBOSA.Reports
{
    public partial class unpaidLoans : System.Web.UI.Page
    {
        ReportDocument rprt = new ReportDocument();

        System.Data.SqlClient.SqlDataReader DR;
        System.Data.SqlClient.SqlDataAdapter da;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                // String MemberNo = Session["Memberno"].ToString();
                //da = new WARTECHCONNECTION.cConnect().ReadDB2("select * from LOANBAL where memberno='"+MemberNo+"'");
                //dataSet ds = new DataSet();
                //da.Fill(ds, "loanbal");
                ReportDocument rd = new ReportDocument();
                string path = Server.MapPath("~\\easysacco_reports\\unPaidLoan.rpt");
                rd.Load(path);
                //RptDoc.SetDataSource(ds.Tables[0]);
                // RptDoc.SetParameterValue("MNo", MemberNo);
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
            string path = Server.MapPath("~\\easysacco_reports\\unPaidLoan.rpt");
            rd.Load(path);
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            rd.ExportToHttpResponse
           (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "unPaidLoan");

        }
        protected void criprt_navigation(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            // CrystalReportViewer1.ReportSource = rd;
        }
    }
}