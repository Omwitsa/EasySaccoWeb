using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;

namespace USACBOSA.General_Ledgers
{
    public partial class GLInquiry : System.Web.UI.Page
    {
        public string glaccno = "";
        public string GlAccName = "";
        public string GlCode = "";
        public double CurrentBal = 0;
        public string glidno = "";
        public string GlAccNBal = "";
        public string AuditName = "";
        public double GlAccBalance = 0;
        public DateTime EarliestTransDate = new DateTime();
        string NormalBal = "";
        double RangeOpeningBal = 0;
        public double totalDr = 0; public double totalCr = 0;
        public double OpeningBal = 0;
        public double OBal = 0;
        public bool isMember = false;
        System.Data.SqlClient.SqlDataReader dr, dr1,dr2;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
           // dtpFromdate.Text = Convert.ToString(System.DateTime.Today.AddDays(-30).ToString("dd-MM-yyyy"));
            //dtpTodate.Text = Convert.ToString(System.DateTime.Today.ToString("dd-MM-yyyy"));
        }

        protected void btnFindGlAcc_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                loaddetail();
                
                //dtpFromdate.Text = Convert.ToString(System.DateTime.Today.AddDays(-30).ToString("dd-MM-yyyy"));
                //dtpTodate.Text = Convert.ToString(System.DateTime.Today.ToString("dd-MM-yyyy"));
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void loaddetail()
        {
            string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            ds.Dispose();
            da.Dispose();
            Label14.Visible = true;
            cboSearchBy.Visible = true;
            btnFindSearch.Visible = true;
            txtvalue.Visible = true;
            GridView1.Visible = false;
        }

        protected void btnFindSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSearchBy.Text == "Account Name")
                {
                    string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP Where GLAccName Like '%" + txtvalue.Text + "%' Order By GLAccName";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                    if (dr.HasRows)
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                        GridView1.Visible = false;
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No such record found, try another option!");
                        txtvalue.Text = "";
                        txtvalue.Focus();
                        return;
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
                else if (cboSearchBy.Text == "Account No")
                {
                    string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP Where AccNo Like '%" + txtvalue.Text + "%' Order By AccNo";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                    if (dr.HasRows)
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                        GridView1.Visible = false;
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No such record found, try another option!");
                        txtvalue.Text = "";
                        txtvalue.Focus();
                        return;
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpFromdate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Provide Start Date");
                }
                else
                {
                    this.txtAccno.Text = GridView2.SelectedRow.Cells[1].Text;
                    this.lblGlname.Text = GridView2.SelectedRow.Cells[2].Text;
                    GridView2.Visible = false;
                    Label14.Visible = false;
                    cboSearchBy.Visible = false;
                    btnFindSearch.Visible = false;
                    txtvalue.Visible = false;
                    LoadGlTransactionsQuery();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadGlTransactionsQuery()
        {
            if (txtAccno.Text.Trim() == "")
            {
                return;
            }
            if (Convert.ToDateTime(dtpFromdate.Text) > Convert.ToDateTime(dtpTodate.Text))
            {
                WARSOFT.WARMsgBox.Show("The StartDate should be Earlier than the FinishDate");
                return;
            }
            Get_GL_AccDetails(txtAccno.Text.Trim());

            //lblGlname.Text = GlAccName
            NormalBal = GlAccNBal;
            lblCurrentbalance.Text = CurrentBal.ToString();
            getGlBalance(txtAccno.Text, Convert.ToDateTime(dtpFromdate.Text), Convert.ToDateTime(dtpTodate.Text));
            RangeOpeningBal = OBal;
            txtBalByRange.Text = RangeOpeningBal.ToString();//'getGlBalance(txtAccNo, dtpFromdate, dtpTodate)

            if (NormalBal == "DR")
            {
                RangeOpeningBal = RangeOpeningBal;
            }
            else if (NormalBal == "CR")
            {
                RangeOpeningBal = RangeOpeningBal;
            }
            else
            {
                RangeOpeningBal = 0;
            }
            new WARTECHCONNECTION.cConnect().WriteDB("Truncate table GeneralLedgers");

            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy insert into GeneralLedgers(Transdate,source,Debits,Credits,AccBal,Chequeno,Description,GLName) values('" + dtpFromdate.Text + "','" + txtAccno.Text + "'," + RangeOpeningBal + "," + RangeOpeningBal + "," + RangeOpeningBal + ",'BAL B/F','Opening Balance','" + GlAccName + "')");

            LoadTransactions();
        }

        protected void txtAccno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAccno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter AccountNo!");
                }
                //else if (dtpFromdate.Text == "" || dtpTodate.Text == "")
                //{
                //    WARSOFT.WARMsgBox.Show("Provide daterange");
                //}
                else
                {
                    dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select * from GLSETUP where GlCode='" + txtAccno.Text + "'");
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            lblGlname.Text = dr2["Glaccname"].ToString();
                            loaddetail();
                        }
                    }
                    dr2.Close(); dr2.Dispose(); dr2 = null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void txtAccno_DataBinding(object sender, EventArgs e)
        {

        }

        private double getGlBalance(string Accno, DateTime datStartDateeTime, DateTime EndDate)
        {

            totalCr = 0;
            totalDr = 0;
            //StartDate = StartDate"DD/MM/YYYY");
            //EndDate = Format(EndDate, "DD/MM/YYYY");

            dr1 = new WARTECHCONNECTION.cConnect().ReadDB("set dateformat dmy select gl.Normalbal,op.sumdr DR,op.sumcr CR,op.cbal,gl.GlAccType,op.oBal from dbo.UDF_GL_OpeningBalance ('" + Accno + "','" + EndDate + "') op inner join glsetup gl on op.accno=gl.accno where gl.accno='" + Accno + "'");
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    OBal = Convert.ToDouble(dr1["cbal"]);
                    totalDr = Convert.ToDouble(dr1["DR"]);
                    totalCr = Convert.ToDouble(dr1["CR"]);
                }
            }
            dr1.Close(); dr1.Dispose(); dr1 = null;
            return OBal;
        }

        private void LoadTransactions()
        {
            double BankBal = 0; double bCredits = 0; double bDebits = 0;
            BankBal = RangeOpeningBal;
            //dr1 = new WARTECHCONNECTION.cConnect().ReadDB("SET DATEFORMAT DMY EXEC GETgLtRANSACTIONS '" + txtAccno.Text + "','" + dtpFromdate.Text + "','" + dtpTodate.Text + "'");
            string execute = "SET DATEFORMAT DMY EXEC GETgLtRANSACTIONS '" + txtAccno.Text + "','" + dtpFromdate.Text + "','" + dtpTodate.Text + "'";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(execute);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
            foreach (GridViewRow oItem in GridView1.Rows)
            {
                string TransDate = oItem.Cells[2].Text;
                string Source = oItem.Cells[5].Text;
                string DocumentNo = oItem.Cells[6].Text;
                string chequeno = oItem.Cells[4].Text;
                string TDescription = oItem.Cells[8].Text;
                string transtype = oItem.Cells[3].Text;

                if (transtype == "DR")
                {
                    bDebits = Convert.ToDouble(oItem.Cells[1].Text);
                }
                if (transtype == "CR")
                {
                    bCredits = Convert.ToDouble(oItem.Cells[1].Text);
                }
                if (NormalBal == "Dr")
                {
                    BankBal = Math.Round((BankBal + bDebits - bCredits), 2);
                }
                else
                {
                    BankBal = Math.Round((BankBal + bCredits - bDebits), 2);
                }
                //'save ledgers for reporting

                new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy insert into GeneralLedgers(Transdate,source,Debits,Credits,AccBal,Chequeno,Description,GLName) values('" + TransDate + "','" + Source + "'," + bDebits + "," + bCredits + "," + BankBal + ",'" + chequeno + "','" + TDescription + "','" + GlAccName + "')");
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SET DATEFORMAT DMY select Transdate,source,Debits,Credits,AccBal,Chequeno,Description,GLName from GeneralLedgers order by Transdate");
                DataSet dsgl = new DataSet();
                da.Fill(dsgl);
                GridView1.Visible = true;
                GridView1.DataSource = dsgl;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
        }

        private void Get_GL_AccDetails(string Accno)
        {
            try
            {
                GlAccName = "";
                GlAccNBal = "";
                glaccno = "";
                GlCode = "";
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select G.accno,G.GlAccname,G.GLCode,G.NormalBal,G.openingBal,g.newglopeningbaldate transdate,g.Type,g.currentbal From GLSetUp G where G.AccNo='" + Accno + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        glaccno = dr["Accno"].ToString();
                        GlAccName = dr["GlAccName"].ToString();
                        GlCode = dr["GlCode"].ToString();
                        GlAccNBal = dr["NormalBal"].ToString();
                        if (GlAccNBal == "")
                        {
                            GlAccNBal = "DR";
                        }
                        else if (GlAccNBal != "Debit")
                        {
                            GlAccNBal = "CR";
                        }
                        else
                        {
                            GlAccNBal = "DR";
                        }
                        GlAccBalance = Convert.ToDouble(dr["CurrentBal"]);
                        GlAccBalance = Convert.ToDouble(dr["OpeningBal"]);
                        EarliestTransDate = Convert.ToDateTime(dr["TransDate"]);
                        CurrentBal = Convert.ToDouble(dr["CurrentBal"]);
                        string membrrr = dr["Type"].ToString();
                        if (membrrr == "MEMBER")
                        {
                            isMember = true;
                        }
                        else
                        {
                            isMember = false;
                        }
                    }
                }
                else
                {
                    GlAccName = "";
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnAccountStatement_Click(object sender, EventArgs e)
        {

            try
            {
                if(txtAccno.Text=="")
                {
                    WARSOFT.WARMsgBox.Show("Provide Account No!");
                }
                else
                {
                Response.Redirect("~/Reports/Accountstatement.aspx");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}