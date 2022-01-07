using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.FinanceAdmin
{
    public partial class BranchesList : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader dr, DR, DR3, DR4;
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
                    LoadBrances();
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
            }
        }
        private void LoadBrances()
        {
            try
            {
               
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("--select branchname--");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches order by branchname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList1.Items.Add(dr["branchname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "--select branchname--")
            {
                WARSOFT.WARMsgBox.Show("You must select the Branch you wish to charge the expense");
            }
            if (TextBox1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Fill in the blanks");
            }
            if (TextBox2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Fill in the blanks");
            }
            else
            {
                string insert = "set dateformat dmy INSERT INTO   tblExpensesCharged(branchname, Expenses, Amount) values('" + DropDownList1.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(insert);
                WARSOFT.WARMsgBox.Show("Details Saved successfully");

                TextBox2.Text = "";
                TextBox1.Text = "";
            }
        }
    }
}