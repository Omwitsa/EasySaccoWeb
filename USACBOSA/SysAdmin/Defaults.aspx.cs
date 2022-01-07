using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class Defaults : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOADdrop();
                loadgrid();
            }
        }

        private void loadgrid()
        {
            try
            {
                string datatable = "select Accno,Description,Amount,Contribution,SharesCode from Defaults";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LOADdrop()
        {

            drpsharescode.Items.Clear();
            drpsharescode.Items.Add("");
            WARTECHCONNECTION.cConnect GLS = new WARTECHCONNECTION.cConnect();
            string GL = "select SharesCode from Defaults";
            dr = GLS.ReadDB(GL);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    drpsharescode.Items.Add("" + dr["SharesCode"].ToString() + "");
                }
            }
            dr.Close(); dr.Dispose(); dr = null; GLS.Dispose(); GLS = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtAccno.Text == "" || TxtRemarks.Text == "" || txtAmnt.Text == "" || txtContribution.Text == "" || drpsharescode.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Provide all Data to save");
            }
            else
            {
                try
                {
                    String save = "insert into Defaults(Accno,Description,Amount,Contribution,SharesCode)Values('" + txtAccno.Text + "','" + TxtRemarks.Text + "','" + txtAmnt.Text + "','" + txtContribution.Text + "','" + drpsharescode.Text + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(save);
                    Clear();
                    loadgrid();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Clear()
        {
            txtAccno.Text="";
            TxtRemarks.Text = ""; txtAmnt.Text = ""; txtContribution.Text = ""; drpsharescode.Text = "";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtAccno.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Select Item to Update");
            }
            else
            {
                string Update = "Update Defaults set Description='" + TxtRemarks.Text.Trim() + "',Amount='" + txtAmnt.Text + "',Contribution='" + txtContribution.Text + "',SharesCode='" + drpsharescode.Text + "' where Accno='"+txtAccno.Text+"'";
                new WARTECHCONNECTION.cConnect().WriteDB(Update);
                WARSOFT.WARMsgBox.Show("Record Updated");
                Clear();
                loadgrid();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtAccno.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Select Item to Delete");
            }
            else
            {
                try
                {
                    string delete = "Delete from Defaults where Accno='" + txtAccno.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(delete);
                    WARSOFT.WARMsgBox.Show("Record Deleted successfully");
                    Clear();
                    loadgrid();
                }
                catch(Exception ex)
                {
                    ex.Data.Clear();
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccno.Text=GridView1.SelectedRow.Cells[1].Text;
            TxtRemarks.Text=GridView1.SelectedRow.Cells[2].Text;
            txtAmnt.Text=GridView1.SelectedRow.Cells[3].Text; 
            txtContribution.Text=GridView1.SelectedRow.Cells[4].Text; 
            drpsharescode.Text=GridView1.SelectedRow.Cells[5].Text;
        }
    }
}