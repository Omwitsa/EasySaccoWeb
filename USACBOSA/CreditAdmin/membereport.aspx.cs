using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using System.Configuration;
using System.Data.SqlClient;
namespace USACBOSA.CreditAdmin
{
    public partial class membereport : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, dr, dr1, dr2, dr3, dr4, drU;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add("");
            string locationss = "SELECT LocationName from Locations";
            dr = new WARTECHCONNECTION.cConnect().ReadDB(locationss);
            if (dr.HasRows)
                while (dr.Read())
                {
                    DropDownList1.Items.Add(dr["LocationName"].ToString().Trim());
                }
            dr.Close(); dr.Dispose(); dr = null;
        }
        protected void criprt_navigation(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            // CrystalReportViewer1.ReportSource = rd;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlDataAdapter da;

            try
            {
                MultiView1.ActiveViewIndex = 1;
                string sql = ("SELECT MEMBERS.Station,MEMBERS.MemberNo, MEMBERS.IDNo, MEMBERS.Surname, MEMBERS.OtherNames, MEMBERS.Sex, MEMBERS.PhoneNo, COMPANY.CompanyName, COMPANY.Address FROM   MEMBERS MEMBERS INNER JOIN COMPANY COMPANY ON MEMBERS.CompanyCode=COMPANY.CompanyCode  ORDER BY MEMBERS.Station");
                //SqlDataAdapter da = new SqlDataAdapter();
                DataTable ds = new DataTable();
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sql);
                da.Fill(ds);
                ReportDocument rd = new ReportDocument();
                string path = Server.MapPath("~\\Reports\\CrystalReport2.rpt");
                rd.Load(path);
                rd.SetDataSource(ds);
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {

                ex.Data.Clear();

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
            DR.Close(); DR.Dispose(); DR = null;
        }
    }
}