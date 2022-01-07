using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.FinanceAdmin
{
    public partial class ReprintReceipt : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            loadgrid();            
        }
        private void MemberNoshow()
        {
            try
            {
                txtReceiptNo.Text = GridView2.SelectedRow.Cells[2].Text;
                string PayNo = "set dateformat dmy select t.ReceiptNo,t.PaymentMode,t.TransactionType,t.amount,t.depositeddate,t.contributiondate,t.auditid, m.surname,m.othernames  from transactions2 t inner join members m on t.memberno=m.memberno  where t.receiptno ='" + txtReceiptNo.Text + "'";
                WARTECHCONNECTION.cConnect ppayno = new WARTECHCONNECTION.cConnect();
                dr = ppayno.ReadDB(PayNo);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtNames.Text = dr["surname"].ToString() + ' ' + dr["othernames"].ToString();
                        txtReceiptNo.Text = dr["ReceiptNo"].ToString();
                        cboPaymentMode.Text = dr["PaymentMode"].ToString();
                        TextBox1.Text = dr["TransactionType"].ToString();
                        txtReceiptAmount.Text = dr["Amount"].ToString();
                        txtDateDeposited.Text = dr["depositeddate"].ToString();
                        txtContribDate.Text = dr["contributiondate"].ToString();
                        txtauditid.Text = dr["auditid"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; ppayno.Dispose(); ppayno = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void loadgrid()
        {
            try
            {
                WARTECHCONNECTION.cConnect oSaccoShare7 = new WARTECHCONNECTION.cConnect();
                dr6 = oSaccoShare7.ReadDB("set dateformat dmy select t.memberno,t.ReceiptNo,t.PaymentMode,t.TransactionType,t.amount,t.depositeddate,t.contributiondate,t.auditid, m.surname,m.othernames  from transactions2 t inner join members m on t.memberno=m.memberno  where m.memberno ='" + txtMemberNo.Text + "'");
                if (dr6.HasRows)
                {
                    GridView2.DataSource = dr6;
                    GridView2.DataBind();
                    GridView2.Visible = true;
                }
                dr6.Close(); dr6.Dispose(); dr6= null; oSaccoShare7.Dispose(); oSaccoShare7 = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Chckbxprintrcpt.Checked == true)
            {
                printreceipt1();
                txtReceiptNo.Text = "";
                cboPaymentMode.Text = "";
                TextBox1.Text = "";
                txtReceiptAmount.Text="";
                txtDateDeposited.Text = "";
                txtContribDate.Text = "";
                txtauditid.Text = "";
            }
        }
        private void printreceipt1()
        {
            try
            {
                DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(0);

                String today = System.DateTime.Today.ToString("yyyy/MM/dd");

                string Date2 = DateTime.Now.ToString("h:mm:ss tt");

                String format2 = "";

                format2 = "</br> -------------LOAN PAYMENT RECEIPT----------------" +
                         "</br> -----FEP SACCO LTD------------------" +
                         "</br> MemberNo :" + txtMemberNo.Text +
                         "</br> Member Names  :" + txtNames.Text +
                         "</br> Amount  :" + txtReceiptAmount.Text +
                         "</br> Receipt Number  :" + txtReceiptNo.Text +
                         "</br> Payment Mode  :" + cboPaymentMode.Text.Replace("%20", " ") +
                         "</br> Transaction Type  :" + TextBox1.Text +
                         "</br> Date Deposited    :" + txtDateDeposited.Text +
                          "</br>Contribution Date  :" + txtContribDate.Text +
                         "</br> -------------------------------------------" +
                         "</br> Received By    :" + User +
                          "</br> --------------------------------------------" +
                          "</br> Received By    :" + User +
                         "</br> --------------------------------------------" +
                         "</br> Date           :" + today + " ; " + Date2.Replace("%20", " ") +
                         "</br> ------------FEP SACCO LTD--------------" +
                         "</br>--HOLISTICALLY NURTURING ENTREPRENEURS--";
                string url = "printreceipt.aspx?1=" + txtMemberNo.Text + "&&2=" + txtNames.Text + "&&3=" + txtReceiptAmount.Text + "&&7=" + Session["mimi"].ToString() + "&&8=" + System.DateTime.Today.ToString("yyyy/MM/dd") + "&&5=" + cboPaymentMode.Text + "&&4=" + txtReceiptNo.Text + "&&6=" + TextBox1.Text + "&&9=" + DateTime.Now.ToString("h:mm:ss tt") + "&&10=" + txtDateDeposited.Text + "&&11=" + txtContribDate.Text + "";//&&4=" + txtBalance.Text + """;//&&4=" + txtBalance.Text + "
                string s = "window.open('" + url + "', 'popup_window', 'width=550,height=600,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
                MemberNoshow();
                //Label22.Visible = false;
                //DropDownList1.Visible = false;
                //TextBox1.Visible = false;
                //Button1.Visible = false;
                //GridView2.Visible = false;
            }
            catch (Exception ex)
            { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

    }
}