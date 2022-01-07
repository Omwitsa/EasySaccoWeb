
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Neodynamic.SDK.Web;
namespace USACBOSA.Reports
{
    public partial class mstatement : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (RadioButtonList1.SelectedIndex < 0)
            //{
            //    RadioButtonList1.SelectedIndex = 1;
            //}
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtSno.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Please enter supplier number.");
                txtSno.Focus();
                TextBox1.Focus();
                txtEndDate.Focus();
                return;
            }
            String StartDate = DateTime.Parse(txtEndDate.Text).ToString("01-MM-yyyy");
            DateTime today = DateTime.Today;
            String EndDate = txtEndDate.Text;
            if (RadioButtonList1.SelectedValue == "advanceslip")
            {
                Double kgs = 0;
                Double Gross = 0;
                String kainet = null;

                Double ded = 0;
                WARTECHCONNECTION.cConnect cnt1 = new WARTECHCONNECTION.cConnect();
                DR = cnt1.ReadDB("d_sp_SupNet '" + txtSno.Text + "','" + txtEndDate.Text + "','" + TextBox1.Text + "',0 ");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR[0].ToString() != "" && DR[0].ToString() != null)
                        {
                            kgs = Convert.ToDouble(DR[0]);
                        }
                        else
                        {
                            kgs = 0.00;
                        }
                        if (DR[1].ToString() != "" && DR[1].ToString() != null)
                        {
                            Gross = Convert.ToDouble(DR[1]);
                        }
                        else
                        {
                            Gross = 0.00;
                        }
                        if (DR[2].ToString() != "" && DR[2].ToString() != null)
                        {
                            kainet = DR[2].ToString();
                        }
                        else
                        {
                            kainet = "XXXX XXXX";
                        }
                    }

                }
                DR.Close(); DR.Dispose(); DR = null; cnt1.Dispose(); cnt1 = null;
                WARTECHCONNECTION.cConnect cnt2 = new WARTECHCONNECTION.cConnect();
                DR = cnt2.ReadDB("d_sp_SupNet '" + txtSno.Text + "','" + txtEndDate.Text + "','" + TextBox1.Text + "',1");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR[0].ToString() != "" && DR[0].ToString() != null)
                        {
                            ded = Convert.ToDouble(DR[0]);
                        }
                        else
                        {
                            ded = 0.00;
                        }
                    }
                }
                DR.Close(); DR.Dispose(); DR = null; cnt2.Dispose(); cnt2 = null;
                Double net = 0;
                net = Gross - ded;
                String format = "";
                format = "Advance Slip" +
                "<br/>KOKICHE DAIRY CO-OPERATIVE SOCIETY LTD" +
                 "<br/>........................................" +
                 "<br/>SNo. : " + txtSno.Text +
                 "<br/>Names : " + kainet +
                 "<br/>Issue Items/Services worth not more than" +
                 "<br/>Kshs. : " + net +
                 "<br/>Sign" +
                 "<br/>___________________________" +
                 "<br/>" + Session["mimi"] +
                 "<br/>Date " + DateTime.Now.ToString("dd/mm/yyyy") +
                 ", Time : " + DateTime.Now.ToString("h:mm:ss") +
                 "<br/>........................................";

            }
            if (RadioButtonList1.SelectedValue == "DetailedPOS")
            {
                string url = "printstmt.aspx?1=" + txtSno.Text + "&&2=" + txtEndDate.Text + "&&3=" + TextBox1.Text + "&&4=" + Session["mimi"] + "";
                string s = "window.open('" + url + "', 'popup_window', 'width=350,height=400,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}