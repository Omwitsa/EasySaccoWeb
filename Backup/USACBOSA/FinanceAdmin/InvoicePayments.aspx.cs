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
    public partial class InvoicePayments : System.Web.UI.Page
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
                MultiView1.ActiveViewIndex = -0;
                Loaddatatogrid2();
                LoadBanks();
                LoadSuppliers();
                LoadBrances();
                //txtContribDate.Attributes.Add("readonly", "readonly");
                txtDateDeposited.Attributes.Add("readonly", "readonly");
                txtReceiptNo.Attributes.Add("readonly", "readonly");
                Label39.Visible = false;
                Label36.Visible = false;
                LoadBanks2();
                Generate_ReceiptNo();

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
        private void LoadSuppliers()
        {
            try
            {
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select companyname from ag_supplier1  order by companyname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList1.Items.Add(dr["companyname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void saveReceipt(string Receiptno, string reff, string RefNo, string mMemberno, string Mmberno, string Receipt, DateTime TransDate, double Amount, string chequeno, string ptype)
        {
            try
            {
                string todb = "set dateformat dmy INSERT INTO ReceiptBooking (ReceiptNo,Ref,Refno,MemberNo,Companycode,Name,Transdate,Amount, Chequeno, ptype, auditid,datedeposited,draccno) VALUES ('" + Receiptno + "','" + reff + "','" + RefNo + "','" + Mmberno + "','" + Mmberno + "','" + Receipt + "','" + TransDate + "'," + Amount + ",'" + chequeno + "','" + ptype + "','" + Session["mimi"].ToString() + "','" + DateTime.Now + "','" + cboBankAC.Text + "')";

                new WARTECHCONNECTION.cConnect().WriteDB(todb);

                string mysql = "set dateformat dmy Insert into Receiptno(Receiptno,Auditdate,auditid,memberno)values('" + Receiptno + "','" + TransDate + "','" + Session["mimi"].ToString() + "','" + mMemberno + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(mysql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Generate_ReceiptNo()
        {
            try
            {
                string ssessionuser = "";
                string myddd444 = "select userid from useraccounts1  where userloginid='" + Session["mimi"].ToString() + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(myddd444);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ssessionuser = dr["userid"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null;
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                string myddd = "select isnull(MAX(RIGHT(ReceiptNo,3)),0)+1 ccount from ReceiptBooking  where ReceiptNo like '%RCP%' and year(auditdatetime)='" + (System.DateTime.Today).Year + "' and month(auditdatetime)='" + (System.DateTime.Today).Month + "' and day(auditdatetime)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        int nextno = maxno;
                        if (ssessionuser == "1" || ssessionuser == "2" || ssessionuser == "3" || ssessionuser == "4" || ssessionuser == "5" || ssessionuser == "6" || ssessionuser == "7" || ssessionuser == "8" || ssessionuser == "9")
                        {
                            ssessionuser = 0 + ssessionuser;
                        }
                        txtReceiptNo.Text = "RCP" + ssessionuser + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno).ToString()).PadLeft(3, '0');
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            InvoiceNoshow();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            DateTime TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            transactionNo = Session["mimi"].ToString() + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            Label39.Visible = true;
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
                WARSOFT.WARMsgBox.Show("Ledger Account must be selected.");
                cboBankAC.Focus();
                return;
            }
            if (txtDateDeposited.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Date Transacted must be selected.");
                txtDateDeposited.Focus();
                return;
            }
            if (TextBox8.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Chequeno is required");
                return;
            }
            if (TextBox6.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Particulars is required");
                return;
            }
            if (txtReceiptAmount.Text == "")
            {
                WARSOFT.WARMsgBox.Show("You must input the Amount.");
                txtDateDeposited.Focus();
                return;
            }
            if (DropDownList2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("You must select bank account.");
                txtDateDeposited.Focus();
                return;
            }          
            else
            {
                double amount2 = 0;
                string sql33 = "select COALESCE (sum(amount),0) as amount from InvoicePayments where invoiceno='" + TextBox7.Text + "' and transtype='CR'";
                WARTECHCONNECTION.cConnect oSaccoMaster3 = new WARTECHCONNECTION.cConnect();
                dr4 = oSaccoMaster3.ReadDB(sql33);
                if (dr4.HasRows)
                    while (dr4.Read())
                    {
                        amount2 = Convert.ToDouble(dr4["amount"]);
                    }
                dr4.Close(); dr4.Dispose(); dr4 = null; oSaccoMaster3.Dispose(); oSaccoMaster3 = null;
                double amount = 0;
                string sql3 = "select COALESCE (sum(amount),0) as amount from InvoicePayments where invoiceno='" + TextBox7.Text + "' and transtype='DR'";
                WARTECHCONNECTION.cConnect gg = new WARTECHCONNECTION.cConnect();
                dr1 = gg.ReadDB(sql3);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        amount = Convert.ToDouble(dr1["amount"]);
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; gg.Dispose(); gg = null;
                double diff = amount2 - amount;
                if (amount > amount2)
                {
                    WARSOFT.WARMsgBox.Show("You have already cleared the invoice payments");
                }
                else
                {
                    TimeNow = DateTime.Now;
                    transactionNo = Convert.ToString(TimeNow);
                    transactionNo = Session["mimi"].ToString() + transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                    saveReceipt(txtReceiptNo.Text, "INVOICE PAYMENTS", TextBox7.Text, TextBox7.Text, TextBox7.Text, TextBox6.Text, System.DateTime.Today, Convert.ToDouble(txtReceiptAmount.Text), TextBox7.Text, txtremarks.Text);
                    string sql = "set dateformat dmy Insert into InvoicePayments(Invoiceno,chequeno,branchcode,particulars,transdate,Amount,Remarks,transtype,supplieraccno,debitaccno,receiptno,supplierid,supplieraccname,debitaccname,auditid,TransactionNo) Values('" + TextBox7.Text + "','"+TextBox8.Text+"','" + Label36.Text + "','" + TextBox6.Text + "','" + txtDateDeposited.Text + "','" + txtReceiptAmount.Text + "','" + txtremarks.Text + "','" + cbotranstype.Text + "','" + cboBankAC.Text + "','"+DropDownList2.Text+"','" + txtReceiptNo.Text + "','" + Label39.Text + "','"+txtBankAC.Text+"','"+txtDRacc.Text+"','" + Session["mimi"].ToString() + "','" + transactionNo + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(sql);
                    WARSOFT.WARMsgBox.Show("Data Saved Successfully");
                    Save_GLTRANSACTIONS(txtDateDeposited.Text, Convert.ToDouble(txtReceiptAmount.Text), cboBankAC.Text, DropDownList2.Text, TextBox7.Text, Label39.Text, Session["mimi"].ToString(), txtremarks.Text, "1", "1", TextBox8.Text, transactionNo, "bosa");
                    if (amount == amount2)
                    {
                        string update = "set dateformat dmy update InvoicePayments set cleared=1 where invoiceno='" + TextBox7.Text + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(sql);
                    }
                    clearTexts();
                    MultiView1.ActiveViewIndex = -0;
                }
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
        protected void branchname_SelectedIndexChanged(object sender, EventArgs e)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchcode from branches where branchname='" + branchname.Text + "'");
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select supplierid from ag_Supplier1 where CompanyName='" + DropDownList1.Text + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Label39.Text = (dr["supplierid"].ToString());
                    Label39.Visible = true;
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
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
        }
        private static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        private static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        private static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
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
                txtremarks.Text = "";
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnFindBank_Click(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
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

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
                TextBox7.Text = GridView2.SelectedRow.Cells[1].Text;
                InvoiceNoshow();
                Loaddatatogrid();
                GridView2.Visible = false;
            }
            catch (Exception ex)
            { 
                WARSOFT.WARMsgBox.Show(ex.Message); return; 
            }
        }
        private void calculatePAID()
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("m");
                if(dr.HasRows)
                    while (dr.Read())
                    {
                    }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void Loaddatatogrid2()
        {
            try
            {
                string datatable = "select InvoiceNo,BranchCode,SupplierId,chequeno,Particulars,Remarks,SupplierAccno,debitaccno from invoicepayments where Remarks='RECEIVED'";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
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
        private void Loaddatatogrid()
        {
            try
            {
                string datatable = "select InvoiceNo,BranchCode,SupplierId,chequeno,Particulars,Remarks,SupplierAccno,debitaccno from invoicepayments where invoiceno='" + TextBox7.Text.TrimStart().TrimEnd() + "' and transtype='CR'";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
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
        private void InvoiceNoshow()
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from invoicepayments where invoiceno='" + TextBox7.Text + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Label36.Visible = true;
                    Label39.Visible = true;
                    TextBox6.Text = dr["particulars"].ToString();
                    Label39.Text = dr["supplierid"].ToString();
                    Label36.Text = dr["branchcode"].ToString();
                    cboBankAC.Text = dr["supplieraccno"].ToString();
                    TextBox6.Text = dr["particulars"].ToString();
                    //DropDownList2.Text = dr["debitaccno"].ToString();
                    txtremarks.Text = dr["remarks"].ToString();
                    txtBankAC.Text = dr["SupplierAccName"].ToString().Replace("'", " ");
                   // txtDRacc.Text = dr["DebitAccName"].ToString().Replace("'", " ");
                    dr6 = new WARTECHCONNECTION.cConnect().ReadDB("select companyname from ag_Supplier1 where supplierid='" + Label39.Text + "'");
                    if (dr6.HasRows)
                    {
                        while (dr6.Read())
                        {
                            DropDownList1.Text = dr6["companyname"].ToString();
                        }                        
                    }
                    dr6.Close(); dr6.Dispose(); dr6 = null;
                    dr7 = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches where branchcode='" + Label36.Text + "'");
                    if (dr7.HasRows)
                    {
                        while (dr7.Read())
                        {
                            branchname.Text = dr7["branchname"].ToString();
                        }
                    }
                    dr7.Close(); dr7.Dispose(); dr7 = null;
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            Loaddatatogrid();

        }
    }
}