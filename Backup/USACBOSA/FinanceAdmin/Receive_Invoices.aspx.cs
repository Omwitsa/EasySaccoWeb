using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using Neodynamic.SDK.Web;

namespace USACBOSA.FinanceAdmin
{
    public partial class Receive_Invoices : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        string TransType;
        static DateTime lastrepaydate;
        public Double totalDr, totalCr = 0;
        public Double OpeningBal = 0;
        public Double Principal = 0;
        public Double mrepayment = 0;
        public Double interest = 0;
        public Double intReceivable = 0;
        public int RepayMode = 0;
        public Double Penalty = 0;
        public Double intOwed = 0;
        public Boolean success = false;
        public Double LBalance = 0;
        public Double rpInterest = 0;
        public Double totalrepayable = 0;
        public Double RepayableInterest = 0;
        public Double penalty2 = 0;
        public string rmethod, intRecovery = "";
        public int rperiod, mdtei = 0;
        public Double rrate = 0;
        public Double initialAmount = 0;
        public string transactionNo = "";
        public Double transactionTotal, loanbalance = 0;
        public Double PrincAmount, IntrAmount, intTotal, IntBalalance = 0;

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
                LoadBanks();
                LoadBrances();
                txtContribDate.Attributes.Add("readonly", "readonly");
                txtDateDeposited.Attributes.Add("readonly", "readonly");
                Label36.Visible = false;
                LoadBanks2();

            }
        }       
        private void LoadBanks()
        {
            cboBankAC.Items.Add("");
            string sql = "select Accno from GLSETUP";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    cboBankAC.Items.Add("" + dr["Accno"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }
        private void LoadBanks2()
        {
            DropDownList2.Items.Add("");
            string sql = "select Accno from GLSETUP";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    DropDownList2.Items.Add("" + dr["Accno"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }        
        private void LoadBrances()
        {
            try
            {

                branchname.Items.Clear();
                branchname.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches order by branchname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        branchname.Items.Add(dr["branchname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadsupplierAccounts()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy select  Invoiceno,transdate,Amount,Duedate,supplieraccno,debitaccno from InvoicePayments where invoiceno='" + TextBox7.Text + "'");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            transactionNo = Session["mimi"].ToString() + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            Button2.Enabled = false;
            Label36.Visible = true;
            if (TextBox7.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Fill in the invoice Number");
                return;
            }
            if (branchname.Text == "")
            {
                WARSOFT.WARMsgBox.Show("You must select the branch name");
                return;
            }
            if (cboBankAC.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Bank Account must be selected.");
                cboBankAC.Focus();
                return;
            }
            if (txtContribDate.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Contribution Date must be selected");
                txtContribDate.Focus();
                return;
            }
            if (txtDateDeposited.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Date Transacted must be selected.");
                txtDateDeposited.Focus();
                return;
            }
            if (TextBox6.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Particulars is required");
            }
            if (txtReceiptAmount.Text == "")
            {
                WARSOFT.WARMsgBox.Show("You must input the Amount.");
                txtDateDeposited.Focus();
                return;
            }
            else
            {
                string TransNo = "select invoiceno from InvoicePayments where invoiceno='"+TextBox7.Text+"' ";
                WARTECHCONNECTION.cConnect TNo = new WARTECHCONNECTION.cConnect();
                dr = TNo.ReadDB(TransNo);
                if (!dr.HasRows)
                    {
                        string sql = "set dateformat dmy Insert into InvoicePayments(Invoiceno,branchcode,particulars,transdate,openingDate,Amount,OpeningBalance,Duedate,Remarks,transtype,supplieraccno,debitaccno,supplierid,supplieraccname,debitaccname,auditid,TransactionNo) Values('" + TextBox7.Text + "','" + Label36.Text + "','" + TextBox6.Text + "','" + txtDateDeposited.Text + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text + "','"+txtReceiptAmount.Text+"','" + txtContribDate.Text + "','" + txtremarks.Text + "','" + cbotranstype.Text + "','" + cboBankAC.Text + "','" + DropDownList2.Text + "','" + TextBox9.Text + "','" + txtBankAC.Text + "','" + txtDRacc.Text + "','" + Session["mimi"].ToString() + "','" + transactionNo + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(sql);
                        LoadsupplierAccounts();
                        WARSOFT.WARMsgBox.Show("Data Saved Successfully");
                        Save_GLTRANSACTIONS(txtDateDeposited.Text, Convert.ToDouble(txtReceiptAmount.Text), DropDownList2.Text, cboBankAC.Text, TextBox7.Text, TextBox9.Text, Session["mimi"].ToString(), txtremarks.Text, "1", "1", TextBox7.Text, transactionNo, "bosa");
                        clearTexts();        
                    }
                dr.Close(); dr.Dispose(); dr = null; TNo.Dispose(); TNo = null;
            }           
        }
        private void Save_GLTRANSACTIONS(string DateDeposited, double ReceiptAmount, string DrAcc, string CrAcc, string InvoiceNo, string supplierId, string User, string description, string p_8, string p_9, string ChequeNo, string transactionNo, string bosa)
        {
            try
            {
                string saccoinsert = "Set DateFormat DMY Exec Save_GLTRANSACTION '" + DateDeposited + "'," + ReceiptAmount + ",'" + DrAcc + "','" + CrAcc + "','" + InvoiceNo + "','" + supplierId + "','" + Session["mimi"].ToString() + "','" + description + "'," + p_8 + "," + p_9 + ",'" + ChequeNo + "','" + transactionNo + "','" + bosa + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(saccoinsert);

                //string updreceipt = "Set DateFormat DMY update ReceiptBooking set Draccno='" + BankAC + "',Craccno='" + SharesAcc + "' where";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void clearTexts()
        {
            try
            {
                
                txtBankAC.Text = "";
                TextBox7.Text = "";
                txtDateDeposited.Text = DateTime.Today.ToString();
                TextBox6.Text = "";
                txtReceiptAmount.Text = "";
                cboBankAC.Text = "";
                txtDateDeposited.Text = "";
                txtContribDate.Text = "";
                txtremarks.Text = "";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void branchname_SelectedIndexChanged(object sender, EventArgs e)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchcode from branches where branchname='"+branchname.Text+"'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Label36.Text = (dr["branchcode"].ToString());
                    Label36.Visible = true;
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }
        protected void cboBankAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Glaccname from GLSETUP where Accno='" + cboBankAC.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtBankAC.Text = dr["Glaccname"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindBank_Click(object sender, EventArgs e)
        {
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Glaccname from GLSETUP where Accno='" + DropDownList2.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtDRacc.Text = dr["Glaccname"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
            string TransNo = "SELECT companyname,supplieraccno,debitaccno,SupplierAccName,DebitAccName FROM  ag_Supplier1 where SupplierID = '" +TextBox9.Text+ "'";
           dr2= new WARTECHCONNECTION.cConnect().ReadDB(TransNo);
            if (dr2.HasRows)
                while (dr2.Read())
                {
                    TextBox8.Text = dr2["CompanyName"].ToString().Replace("'", " ");
                    DropDownList2.Text = dr2["debitaccno"].ToString();
                    cboBankAC.Text = dr2["supplieraccno"].ToString();
                    txtBankAC.Text = dr2["SupplierAccName"].ToString().Replace("'", " ");
                    txtDRacc.Text = dr2["DebitAccName"].ToString().Replace("'", " ");
                }
            dr2.Close(); dr2.Dispose(); dr2 = null;
            //LoadsupplierAccounts();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        private void loadInvoices()
        {
            //this.txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
            string TransNo = "set dateformat dmy select Invoiceno,transdate,Amount,Duedate,supplieraccno,debitaccno from InvoicePayments where invoiceno='" + TextBox7.Text + "'";
            WARTECHCONNECTION.cConnect TNoo = new WARTECHCONNECTION.cConnect();
            dr2 = TNoo.ReadDB(TransNo);
            if (dr2.HasRows)
                while (dr2.Read())
                {
                   TextBox7.Text = dr2["Invoiceno"].ToString().Replace("'", " ");
                   txtDateDeposited.Text = dr2["transdate"].ToString().Replace("'", " ");
                   txtReceiptAmount.Text = dr2["Amount"].ToString().Replace("'", " ");
                   txtContribDate.Text = dr2["Duedate"].ToString().Replace("'", " ");
                   cboBankAC.Text = dr2["supplieraccno"].ToString().Replace("'", " ");
                   DropDownList2.Text = dr2["debitaccno"].ToString().Replace("'", " ");
                }
            dr2.Close(); dr2.Dispose(); dr2 = null; TNoo.Dispose(); TNoo = null;
        }
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInvoices();
            btnSave.Enabled = false;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string TransNo = "select invoiceno from InvoicePayments where invoiceno='" + TextBox7.Text + "' ";
            WARTECHCONNECTION.cConnect TNo = new WARTECHCONNECTION.cConnect();
            dr = TNo.ReadDB(TransNo);
            if (dr.HasRows)
            {
                string sql = "set dateformat dmy UPDATE InvoicePayments branchcode='" + Label36 + "',particulars='" + TextBox6.Text + "',transdate='" + txtDateDeposited.Text + "',Amount='"+txtReceiptAmount.Text+"',Duedate='"+txtContribDate.Text+"',Remarks='"+txtremarks.Text+"',supplieraccno='"+cboBankAC.Text+"',debitaccno='"+DropDownList2.Text+"',supplieraccname='"+txtBankAC.Text+"',debitaccname='"+txtDRacc.Text+"' where invoiceno='"+TextBox7.Text+"'";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
                LoadsupplierAccounts();
                WARSOFT.WARMsgBox.Show("Data updated Successfully");
                Save_GLTRANSACTIONS(txtDateDeposited.Text, Convert.ToDouble(txtReceiptAmount.Text), DropDownList2.Text, cboBankAC.Text, TextBox7.Text, TextBox9.Text, Session["mimi"].ToString(), txtremarks.Text, "1", "1", TextBox7.Text, transactionNo, "bosa");
                clearTexts();
            }
            dr.Close(); dr.Dispose(); dr = null; TNo.Dispose(); TNo = null;
        }

    }
}