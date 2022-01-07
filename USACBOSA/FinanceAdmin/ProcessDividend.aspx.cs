using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.FinanceAdmin
{
    public partial class ProcessDividend : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        string memberno = "";
        string companycode = "";
        string myshares = "";
        double sharecap = 0;
        double initshares = 0;
        double SharesContr = 0;
        double Totalshares = 0;
        double shareinterest = 0;
        double dividends = 0;
        string names;
        double tax = 0;
        double netdividends = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadShareCodes();
            }
            TextBox2.Text = DateTime.Now.ToShortDateString();
            TextBox6.Attributes.Add("readonly", "readonly");            
        }
        private void LoadShareCodes()
        {

            try
            {

                cboShareCode.Items.Clear();
                cboShareCode.Items.Add("");
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB("select sharescode from ShareType order by ismainshares desc");
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        cboShareCode.Items.Add(dr["sharescode"].ToString().Trim());
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            double percentage = Convert.ToInt32(TextBox1.Text);

            if (TextBox4.Text == "")
            {
                WARSOFT.WARMsgBox.Show("please specify period To");
            }           
            if (percentage > 100)
            {
                WARSOFT.WARMsgBox.Show("percentage should be less than 100%");
            }
            if (cboShareCode.Text == "")
            {
                WARSOFT.WARMsgBox.Show("please select share code");
            }
            calculateDividends();
            Loaddatatogrid();
            WARSOFT.WARMsgBox.Show("Dividends has been processed successfully");
        }
        private void Loaddatatogrid()
        {
            try
            {
                WARTECHCONNECTION.cConnect ggg = new WARTECHCONNECTION.cConnect();
                DR = ggg.ReadDB("select Memberno,names,Current_Tot_Shares,Shares_as_at,Gross_Dividend,net_dividend,divwithtax from tmpdividendpaylist");
                if (DR.HasRows)
                {
                    GridView1.DataSource = DR;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void calculateDividends()
        {
            string truncate = "TRUNCATE TABLE PERTRAN";
            new WARTECHCONNECTION.cConnect().WriteDB(truncate);
            string delete = "Exec Delete_tmpDividendPaylist";
            new WARTECHCONNECTION.cConnect().WriteDB(delete);
            string select = "Select * From PERTRAN";
            new WARTECHCONNECTION.cConnect().WriteDB(select);
            string select2 = "Select * from SYSPARAM";
            new WARTECHCONNECTION.cConnect().WriteDB(select2);
            string update = "Exec Update_ShareCapital";
            new WARTECHCONNECTION.cConnect().WriteDB(select2);
            string date = "Set DateFormat DMY Select * From MEMBERS where ApplicDate<='" + TextBox4.Text + "' order by MemberNo";

            //no of months to loop

            DateTime DateTo = Convert.ToDateTime(TextBox4.Text);

            //DateTime DateFrom = Convert.ToDateTime(TextBox5.Text);
            //int monthdiff = ((DateTo.Year - DateFrom.Year) * 12) + (DateTo.Month - DateFrom.Month);
            //TimeSpan D = (DateTo - DateFrom);
            //double nofdays = D.TotalDays;
           // int m = monthdiff;
            double i;
            double percentage = Convert.ToInt32(TextBox1.Text);
            double taxpercentage = Convert.ToInt32(TextBox5.Text);
            string lbal2 = "Set DateFormat DMY Select sum(c.amount) as MyShares,m.memberno,m.companycode,m.initshares,(m.surname+' '+m.othernames) as names From Contrib c inner join MEMBERS m on m.memberno=c.memberno where c.contrdate<='" + TextBox4.Text + "' and archived <>'1' and withdrawn <>'1' and amount>='10000' and c.sharescode='"+cboShareCode.Text+"' group by m.MemberNo,m.CompanyCode,m.initshares,m.Surname,m.OtherNames";
            WARTECHCONNECTION.cConnect lbalance1 = new WARTECHCONNECTION.cConnect();
            dr6 = lbalance1.ReadDB(lbal2);
            if (dr6.HasRows)
                while (dr6.Read())
                {
                    memberno = dr6["memberno"].ToString().Replace("'", " ");
                    companycode = dr6["companycode"].ToString().Replace("'", " ");
                    initshares = Convert.ToInt32(dr6["initshares"]);
                    names = dr6["names"].ToString().Replace("'", " ");

                    myshares = dr6["myshares"].ToString();
                    if (myshares == "")
                    {
                        myshares = "0";
                    }

                    Totalshares = Convert.ToDouble(myshares);
                    double processingfee = Convert.ToDouble(TextBox6.Text);
                    if (Totalshares != 0)
                    {
                        dividends = Totalshares * (percentage / 100);
                        tax = dividends * (taxpercentage / 100);
                        netdividends = dividends - tax-processingfee;
                        string dividendss = "set dateformat dmy insert into tmpdividendpaylist(Memberno,names,Current_Tot_Shares,Shares_as_at,Gross_Dividend,net_dividend,divwithtax,sharecapital,processingfee)values('" + memberno + "','" + names + "','" + Totalshares + "','" + TextBox4.Text + "','" + dividends + "','"+netdividends+"','"+tax+"','"+cboShareCode.Text+"','"+processingfee+"')";
                        new WARTECHCONNECTION.cConnect().WriteDB(dividendss);
                    }

                }

            dr6.Close(); dr6.Dispose(); dr6 = null; lbalance1.Dispose(); lbalance1 = null;
        }
    }
}