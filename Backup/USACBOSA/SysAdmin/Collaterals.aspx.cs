using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class Collaterals : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_Collaterals();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the collateral code.");
                    txtCode.Focus();
                    return;
                }

                if (txtDescription.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the collateral description.");
                    txtDescription.Focus();
                    return;
                }
                if (txtPercent.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Percentage.");
                    txtPercent.Focus();
                    return;
                }

                SAVE_COLLATERAL(txtCode.Text, txtDescription.Text, Convert.ToDouble(txtPercent.Text), "User");

                WARSOFT.WARMsgBox.Show("Collateral Created Successfully.");

                Load_Collaterals();
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtPercent.Text = "";
            TextBox1.Text = "";
        }

        private void Load_Collaterals()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("Select ColCode,Coldescription,Percentage From COLLATERALS Order By ColCode");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView.Visible = true;
                GridView.DataSource = ds;
                GridView.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SAVE_COLLATERAL(string ColCode, string ColDescription, double Percentage, string auditid)
        {
            try
            {
                string todb = "Exec SAVE_COLLATERAL '" + ColCode + "','" + ColDescription + "'," + Percentage + "";
                new WARTECHCONNECTION.cConnect().WriteDB(todb);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the collateral code.");
                    txtCode.Focus();
                    return;
                }

                if (txtDescription.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the collateral description.");
                    txtDescription.Focus();
                    return;
                }
                if (txtPercent.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Percentage.");
                    txtPercent.Focus();
                    return;
                }
                if (TextBox1.Text != "")
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("Update COLLATERALS Set COLCODE='"+txtCode.Text.Trim()+"', ColDescription='" + txtDescription.Text + "',Percentage='" + txtPercent.Text + "' Where id='" + TextBox1.Text + "'");
                }
                else
                {
                    new WARTECHCONNECTION.cConnect().WriteDB("Update COLLATERALS Set ColDescription='" + txtDescription.Text + "',Percentage='" + txtPercent.Text + "' Where COLCODE='" + txtCode.Text + "'");
                }
                WARSOFT.WARMsgBox.Show("Collateral Updated Successfully.");
                clearTexts();
                Load_Collaterals();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string del = "Delete from COLLATERALS where ColCode='" + txtCode.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(del);
                WARSOFT.WARMsgBox.Show("Collateral deleted Successfully.");
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtCode.Text = GridView.SelectedRow.Cells[1].Text;
                txtDescription.Text = GridView.SelectedRow.Cells[2].Text;
                txtPercent.Text = GridView.SelectedRow.Cells[3].Text;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select id from COLLATERALS where ColCode='" + txtCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox1.Text=dr["id"].ToString().Trim();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
    }
}