using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class AccountCodes : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlDataReader DR1, DR2, DR3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddrop2();
                binddrop1();
                LoadAccCodes();
            }
        }

        private void LoadAccCodes()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select  Categoryid,AccGroup AccGroup,Description,MainAccount from category");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        private void binddrop2()
        {
            DropDownList2.Items.Clear();
            try
            {
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add("");
                DR2 = new WARTECHCONNECTION.cConnect().ReadDB("select Distinct MainAccount from category");
                if (DR2.HasRows)
                    while (DR2.Read())
                    {
                        this.DropDownList2.Items.Add(DR2["MainAccount"].ToString());

                    }
                DR2.Close(); DR2.Dispose(); DR2 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void binddrop1()
        {
            DropDownList1.Items.Clear();
            try
            {
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("");
                DR1 = new WARTECHCONNECTION.cConnect().ReadDB("select Distinct AccGroup from category");
                if (DR1.HasRows)
                    while (DR1.Read())
                    {
                        this.DropDownList1.Items.Add(DR1["AccGroup"].ToString());

                    }
                DR1.Close(); DR1.Dispose(); DR1 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter Details");
            }
            else
            {
                String sql = "Insert into category(Categoryid,Description,AccGroup,MainAccount) Values('"+TextBox1.Text+"','"+TextBox2.Text+"','"+DropDownList1.Text+"','"+DropDownList2.Text+"')";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
                LoadAccCodes();
                WARSOFT.WARMsgBox.Show("Saved Successfully");
                clearTexts();
                return;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter CategoryID To Proceed!");
            }
            else
            {
                String Update = "Update category set Categoryid='" + TextBox1.Text + "',Description='" + TextBox2.Text + "',AccGroup='" + DropDownList1.Text + "',MainAccount='" + DropDownList2.Text + "'where CategoryID='" + TextBox1.Text + "' ";
                new WARTECHCONNECTION.cConnect().WriteDB(Update);
                LoadAccCodes();
                WARSOFT.WARMsgBox.Show("Record Updated Successfully");
                clearTexts();
                return;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            WARTECHCONNECTION.cConnect nono = new WARTECHCONNECTION.cConnect();
            String mimi = ("Select  * from category where Categoryid='" + TextBox1.Text + "'");
                DR3 = nono.ReadDB(mimi);
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        TextBox1.Text = DR3["Categoryid"].ToString();
                        TextBox2.Text = DR3["Description"].ToString();
                        DropDownList1.Text = DR3["AccGroup"].ToString();
                        DropDownList2.Text = DR3["MainAccount"].ToString();
                    }
                DR3.Close(); DR3.Dispose(); DR3 = null;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Enter CategoryID");
                return;
            }
            else
            {
                String delete = "Delete from category where Categoryid='" + TextBox1.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(delete);
                clearTexts();
                LoadAccCodes();
                WARSOFT.WARMsgBox.Show("Record Deleted Successfully");
                return;
            }

        }

        private void clearTexts()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            DropDownList2.Text = "";
            DropDownList1.Text = "";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
             try
            {
                TextBox1.Text = GridView1.SelectedRow.Cells[1].Text;
                TextBox2.Text = GridView1.SelectedRow.Cells[3].Text;
               DropDownList1.Text = GridView1.SelectedRow.Cells[2].Text;
              DropDownList2.Text = GridView1.SelectedRow.Cells[4].Text;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        
    }
}