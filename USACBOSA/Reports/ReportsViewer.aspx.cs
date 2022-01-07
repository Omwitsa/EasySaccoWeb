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

namespace USACBOSA.Reports
{
    public partial class ReportsViewer : System.Web.UI.Page
    {
      
        String path;
        System.Data.SqlClient.SqlDataAdapter da;
        ReportDocument rd;
        ReportDocument RptDoc2 = new ReportDocument();
        System.Data.SqlClient.SqlDataReader DR;
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (RptDoc2!= null)
            {
                RptDoc2.Close();
                RptDoc2.Dispose();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                
                string param1 = Request.QueryString["reportType"];

                string cre = Request.QueryString["para"];
                //reportType=intakeaudi
                switch (Request.QueryString["reportType"])
                {
                   
                    case "members":

                        ReportDocument rd = new ReportDocument();
                        string path = Server.MapPath("~\\easysacco_reports\\members.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "listofcigs":

                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\ListCIG.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "MembersPERCIG"://listofcigpercounty

                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\MembersPERCIG.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "listofcigpercounty"://listofcigpercounty

                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CIGREPORTPERCOUNTY.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "members2":

                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\DeliveryListRpt.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "ShareStatement":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\ShareStatement.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "Trial Balance"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Trial Balance.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "CreditorsStatement"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CreditorsStatement.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "CASHFLOW"://LoansIssued-Detailedregion
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\cashflowstatement.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "LoansIssued-Detailedregion"://LoansIssued-Detailedregion
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoansIssued-Detailedregion.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "RiskPortfolioanalysis"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Risk Portfolio Analysis.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "CollectionSheett2"://PRODUCTOLB
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CollectionSheett2.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "PRODUCTOLB"://COLLECTIONPERREGION
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PRODUCTOLB.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "COLLECTIONPERREGION"://COLLECTIONPERREGION
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\DefaultersCollectionRange.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "DefaultersCollection"://DefaultersCollection
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\DefaultersCollection.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "aginganalysispergraph"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\aginganalysisPERREGGRAPH1.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "LoanTypesPAR"://Trial Balance
                         rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoanTypesPAR.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "PAR"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PARPERCOUNTY.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "aginganalysisperregion"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\aginganalysisPERREGION.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "incomeexpenditure"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\incomeexpenditure.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "BALANCESHEET"://Trial Balance
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\BalanceSheeet.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//set dateformat dmy SELECT * FROM AppraisalRpt
                    case "Dividend List":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Dividend List.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "AppliedLoans":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Applied Loans.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;
                    case "Loansissued":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoansIssued-Detailed.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loansoverpayment
                    case "Loansoverpayment":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Loan_Overpayment.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loansoverpayment
                    case "LoansDue":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoansDue.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//LoanRecovery
                    case "LoanRecovery":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\periodicLoanRecovery.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SummaryRepayments
                    case "SummaryRepayments":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Summary_Repayments.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//periodicLoanRecovery
                    case "periodicLoan":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PeriodicLoan_Recovery.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//unPaidLoan
                    case "withdrawal":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\withdrawal.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//unPaidLoan
                    case "withdrawalPERREGION":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\withdrawalPERREGION.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//unPaidLoan
                    case "periodicLoanRecovery":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\periodicLoanRecovery.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//unPaidLoan
                    case "unPaidLoan":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\unPaidLoan.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//LoanBalances
                    case "LoanBalances":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Loan_Balances.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//LoanBalances
                    case "Loanbalanceperloan":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Loan_Balances.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due
                    case "Loansdue":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Loans Due.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//Expecteddues
                    case "Expecteddues":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\ExpectedduesPerRegion.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//ExpectedduesOverall
                    case "ExpectedduesOverall":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\ExpectedduesPeriodically.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//ExpectedduesOverall
                    case "Budgets":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Budgets.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//CollectionSheett3
                    case "Savingsperiod":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Savingsperiod.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//CollectionSheett3
                    case "CollectionSheett3":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CollectionSheett3.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due//CollectionSheett3
                    case "Registered":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Registered.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//Loans Due
                    case "AuditTrail_Time":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\AuditTrail_Time.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "ChequesReceived":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\ChequesReceived.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "LoansStatement":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoansStatement.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "BalancesAsAt":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\BalancesAsAt.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "RejectedLoans":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\RejectedLoansReport.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;

                    case "Defaulters":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PeriodicLoanDefaultersOverall.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//AppliedLoansPerRegion
                    case "AppliedLoansPerRegion":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Applied LoansPerRegion.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//AppliedLoansPerRegion
                    case "Defaultersregion":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PeriodicLoanDefaultersPerRegion.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//partialDefaulters
                    case "partialDefaulters":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\partialDefaulters.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//partialDefaulters
                    case "fullyDefaulters":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\FullyDefaulters.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//newGuarantors
                    case "newGuarantors":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Guarantors.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//CollectionSheet2019
                    case "CollectionSheet2019PERREGION":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CollectionSheet2019PERREGION.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//individualsGuarantors
                    case "CollectionSheet2019":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\CollectionSheet2019.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//individualsGuarantors
                    case "individualsGuarantors":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\GuaranteedLoans.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SharesVariations
                    case "SharesVariations":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\SharesVariations.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//ShareholdersListing
                    case "ShareholdersListing":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Shareholders Listing.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SHARES AND LOAN SUMMARY
                    case "grouped":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\SHARES AND LOAN SUMMARY.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SHARES AND LOAN SUMMARY
                    case "ungrouped":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\LoanShareSummaries-member.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SHARES AND LOAN SUMMARY
                    case "piriodicshares":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\PriodicShares.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SummaryRepaymentsPERREGION
                    case "SummaryRepaymentsPERREGION":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\SummaryRepaymentsPERREGION.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//SummaryRepaymentsPERREGION
                    case "PeriodicShares":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\Shares_asat.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//PeriodicShares
                    case "aginganalysis":
                        rd = new ReportDocument();
                        path = Server.MapPath("~\\easysacco_reports\\aginganalysis.rpt");
                        rd.Load(path);
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.DataBind();
                        break;//aginganalysis
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
                return;
            }
          
        }


        private ReportDocument customerReport;
        private void ConfigureCrystalReports()
        {
            customerReport = new ReportDocument();
            string reportPath = Server.MapPath("~\\bin\\ReportsAmtech\\suppliersregister.rpt");
            customerReport.Load(reportPath);
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EasyMAConnectionString"].ConnectionString);
            System.Data.SqlClient.SqlDataAdapter adpt = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM d_suppliers", conn);

            DataSet dataSet = new DataSet();

            adpt.Fill(dataSet, "d_suppliers");
            customerReport.SetDataSource(dataSet);
            CrystalReportViewer1.ReportSource = customerReport;
        }
        private static string getValue(string par)
        {
            return par;
        }

        public void getReport(string para)
        {
            disReport(para);

        }
        public static void disReport(string ppw)
        {
            /*ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~\\bin\\ReportsAmtech\\pettycashvoucher.rpt");
            rd.Load(path);
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.SelectionFormula = ppw;
            CrystalReportViewer1.DataBind();*/
        }

        protected void criprt_navigation(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            CrystalReportViewer1.ReportSource = rd;
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

        }
    }