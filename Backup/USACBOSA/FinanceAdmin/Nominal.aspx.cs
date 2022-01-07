using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace USACBOSA.FinanceAdmin
{
    public partial class Nominal : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, dr2, DR, Dr;
        System.Data.SqlClient.SqlDataAdapter da;
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
                Generate_ReceiptNo();
                LoadGL();
                Loadbanks();
                DateTime rdate = DateTime.Today;
                txtReceiptDate.Text = rdate.ToString("dd-MM-yyyy");
            }
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
                string myddd = "select isnull(MAX(RIGHT(ReceiptNo,3)),0)+1 ccount from ReceiptBooking  where ReceiptNo like '%RCP%' and year(TransDate)='" + (System.DateTime.Today).Year + "' and month(TransDate)='" + (System.DateTime.Today).Month + "' and day(TransDate)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        int nextno = maxno;
                        txtReceiptNo.Text = "RCP" + ssessionuser + (System.DateTime.Today).Day + (System.DateTime.Today).Month + Right((((System.DateTime.Today).Year).ToString()), 2) + "-" + ((nextno).ToString()).PadLeft(3, '0');
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void Loadbanks()
        {
            try
            {
                // cboBankAC.Items.Clear();
                cboBankAC.Items.Add("");
                WARTECHCONNECTION.cConnect GLS11 = new WARTECHCONNECTION.cConnect();
                string GL2 = "select accno from banks";
                dr2 = GLS11.ReadDB(GL2);
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        cboBankAC.Items.Add("" + dr2["accno"].ToString() + "");
                    }
                }
                dr2.Close(); dr2.Dispose(); dr2 = null; GLS11.Dispose(); GLS11 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadGL()
        {
            try
            {
                //cboaccno.Items.Clear();
                cboaccno.Items.Add("");
                WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
                string GL = "select Glaccname,accno from GLSETUP";
                dr = GLS.ReadDB(GL);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboaccno.Items.Add("" + dr["accno"].ToString() + "");
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; GLS.Dispose(); GLS = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboBankAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select bankname from banks where accno='" + cboBankAC.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtBankAC.Text = dr["bankname"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void cboaccno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select GLACCNAME from GLSETUP where accno='" + cboaccno.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtAccNames.Text = dr["GLACCNAME"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtAmountDR_TextChanged(object sender, EventArgs e)
        {
            txtAmountCR.Text = txtAmountDR.Text;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReceiptNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please generate the receiptno");
                    return;
                }
               
                if (cboBankAC.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("You Must Select the Bank Control Account");
                    return;
                }
                if (txtmode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Payment Mode  Required");
                    return;
                }
                if (cboPaymentMode.Text == "Cash")
                {
                    if (txtReceiptNo.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Cash ReceiptNo Reference Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "EFT")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("EFT Receipt No Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "Mpesa")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Mpesa Receipt No Required");
                        return;
                    }
                }
                if (cboPaymentMode.Text == "Zap")
                {
                    if (txtmode.Text == "")
                    {
                        WARSOFT.WARMsgBox.Show("Zap Receipt No Required");
                        return;
                    }
                }
               
                string sql = "select receiptno,chequeno from receiptbooking where receiptno in ('" + txtReceiptNo.Text + "')";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(sql);
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("Either receiptno or Chequeno is already used, Get another One!");
                }
                if (cboPaymentMode.Text == "Cheque")
                {
                    txtmode.Visible = true;
                }
                else
                {
                    txtmode.Visible = false;
                }
                DateTime TimeNow = DateTime.Now;
                string transactionNo = Convert.ToString(TimeNow);
                transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
                string companyname = "";
                Save_GLTRANSACTION(Convert.ToDateTime(txtReceiptDate.Text), Convert.ToDouble(txtAmountDR.Text), cboBankAC.Text, cboaccno.Text, txtReceiptNo.Text, Session["mimi"].ToString(), txtParticulars.Text, 0, txtmode.Text, transactionNo);
                saveReceipt(txtReceiptNo.Text, "Nominal", "RefNo", companyname, companyname, "Receipt", Convert.ToDateTime(txtReceiptDate.Text), Convert.ToDouble(txtAmountDR.Text), txtmode.Text, cboPaymentMode.Text);
                WARSOFT.WARMsgBox.Show("Receipt payment posted sucessfully");
                clearTexts();
                Generate_ReceiptNo();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            try
            {
                txtAccNames.Text = "";
                txtAmountCR.Text = "";
                txtAmountDR.Text = "";
                txtBankAC.Text = "";
                txtmode.Text = "";
                txtParticulars.Text = "";
                txtPayee.Text = "";
                txtReceiptNo.Text = "";
                cboaccno.Text = "";
                cboBankAC.Text = "";
                cboPaymentMode.Text = "";
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void saveReceipt(string Receiptno, string reff, string RefNo, string mMemberno, string Mmberno, string Receipt, DateTime TransDate, double Amount, string chequeno, string ptype)
        {
            try
            {
                string todb = "set dateformat dmy INSERT INTO ReceiptBooking (ReceiptNo,Ref,Refno,MemberNo,Companycode,Name,Transdate,Amount, Chequeno, ptype, auditid,datedeposited) VALUES ('" + Receiptno + "','" + reff + "','" + RefNo + "','" + Mmberno + "','" + Mmberno + "','" + Receipt + "','" + TransDate + "'," + Amount + ",'" + chequeno + "','" + ptype + "','" + Session["mimi"].ToString() + "','" + DateTime.Now + "')";

                new WARTECHCONNECTION.cConnect().WriteDB(todb);

                string mysql = "set dateformat dmy Insert into Receiptno(Receiptno,Auditdate,auditid,memberno)values('" + Receiptno + "','" + TransDate + "','" + Session["mimi"].ToString() + "','" + mMemberno + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(mysql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRaccno, string Craccno, string DocumentNo, string auditid, string TransDescription, int doc_posted, string chequeno, string transactionNo)
        {
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + DRaccno + "','" + auditid + "','" + TransDescription + "','0'," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','bosa'");
        }

        protected void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
            //throw new NotImplementedException();
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
    }
}