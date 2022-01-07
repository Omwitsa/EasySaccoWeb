using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.SysAdmin
{
    public partial class Ward : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (name.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Ward is required");
                    return;
                }
                else
                {
                    dr = new WARTECHCONNECTION.cConnect().ReadDB("SELECT * FROM Ward WHERE Name = '" + name.Text + "'");
                    if (dr.HasRows)
                    {
                        WARSOFT.WARMsgBox.Show("Ward already exist");
                        clearTexts();
                        return;
                    }
                    else
                    {
                        string inserdb = "INSERT INTO Ward(Name, CreatedOn, CreatedBy) VALUES ('" + name.Text + "', GETDATE(), '')";
                        new WARTECHCONNECTION.cConnect().WriteDB(inserdb);
                        WARSOFT.WARMsgBox.Show("Ward save sucessfully");
                        clearTexts();
                        LoadGridview();
                        return;
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            name.Text = "";
        }

        private void LoadGridview()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select Name, CreatedOn, CreatedBy from Ward");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.name.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
            dr = new WARTECHCONNECTION.cConnect().ReadDB("SELECT * FROM Ward WHERE Name = '" + name.Text + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    name.Text = dr["Name"].ToString();
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }
    }
}