using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace USACBOSA.SysAdmin
{
    public partial class Locations : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Session["mimi"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
                if (!IsPostBack)
                {
                    LoadGridview();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLocationCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Location code is required");
                    return;
                }
                if (txtLocationName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Location name is required");
                    return;
                }
                else
                {
                    dr = new WARTECHCONNECTION.cConnect().ReadDB("select LocationCode,LocationName from Locations where LocationCode='" + txtLocationCode.Text + "'");
                    if (dr.HasRows)
                    {
                        WARSOFT.WARMsgBox.Show("Location already exist");
                        clearTexts();
                        return;
                    }
                    else
                    {
                        string inserdb = "insert into Locations(Ltype,LocationCode,LocationName,CreatedBy)values('" + cboLocationtype.Text + "','" + txtLocationCode.Text + "','" + txtLocationName.Text + "','')";
                        new WARTECHCONNECTION.cConnect().WriteDB(inserdb);
                        WARSOFT.WARMsgBox.Show("Location save sucessfully");
                        clearTexts();
                        LoadGridview();
                        return;
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadGridview()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT LocationCode [Location Code],LocationName [Location Name],LType [Location Type],CreatedBy [Created By] from Locations");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        private void clearTexts()
        {
            cboLocationtype.Text = "";
            txtLocationName.Text = "";
            txtLocationCode.Text = "";
            txtLocationId.Text = "";
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtLocationCode.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
            this.txtLocationName.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
            this.cboLocationtype.SelectedValue = GridView1.SelectedRow.Cells[3].Text.Trim();
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select LocationId from Locations where LocationCode='" + txtLocationCode.Text.Trim() + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtLocationId.Text = dr["LocationId"].ToString();
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        protected void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            try
            {
                string uppdddate = "update Locations set LType='" + cboLocationtype.Text + "',LocationCode='" + txtLocationCode.Text + "',LocationName='" + txtLocationName.Text + "' where LocationId='" + txtLocationId.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(uppdddate);
                LoadGridview();
                WARSOFT.WARMsgBox.Show("Location updated sucessfully");
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string dellet = "Delete from Locations where LocationId='" + txtLocationId.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(dellet);
                LoadGridview();
                WARSOFT.WARMsgBox.Show("Location deleted sucessfully");
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
    }
}