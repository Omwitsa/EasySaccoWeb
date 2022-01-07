using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace USACBOSA.Regions
{
    public partial class RgnAgents : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR3;
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
                TextBox2.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
                LoadAgents();
            }
        }

        private void LoadAgents()
        {
            try
            {
                string datatable = "select Names,Gender,IdNo,LandPhone,MobileNo,HomeAddress,Town,Recruitdate from Agents order by Recruitdate desc";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "" || TextBox4.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Input the required Information");
                    return;
                }
                else
                {
                    string save = " set dateformat dmy Insert into Agents(Names,Gender,IdNo,LandPhone,MobileNo,HomeAddress,Town,Recruitdate,AuditId,AuditTime) Values('" + TextBox1.Text + "','" + DropDownList1.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox2.Text + "','" + Session["mimi"].ToString() + "','" + System.DateTime.Now.ToString("dd/MM/yyyy") + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(save);

                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Agents','Added Agent " + TextBox1.Text + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                    LoadAgents();
                    WARSOFT.WARMsgBox.Show("Agent details saved sucessfully");
                    clearTexts();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void clearTexts()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            DropDownList1.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox4.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the ID Number");
                }
                else
                {
                    String update = "set dateformat dmy Update Agents  set Names='" + TextBox1.Text + "',Gender='" + DropDownList1.Text + "',LandPhone='" + TextBox5.Text + "',MobileNo='" + TextBox6.Text + "',HomeAddress='" + TextBox7.Text + "',Town='" + TextBox8.Text + "' where IdNo='" + TextBox4.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(update);

                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Agents','Updated Agent " + TextBox1.Text + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                    LoadAgents();
                    WARSOFT.WARMsgBox.Show("Agent Updated Successfully");
                    clearTexts();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox4.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter the ID Number of item to delete");
                    TextBox4.Focus();
                    return;
                }
                else
                {
                    String Delete = "set dateformat dmy Delete from Agents where IdNo='" + TextBox4.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(Delete);

                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('Agents','Deleted Agent " + TextBox1.Text + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                    LoadAgents();
                    WARSOFT.WARMsgBox.Show("" + TextBox1.Text + " Details Deleted Successfully");
                    clearTexts();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect nono = new WARTECHCONNECTION.cConnect();
                String mimi = ("Select  * from Agents where IdNo='" + TextBox4.Text + "'");
                DR3 = nono.ReadDB(mimi);
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        TextBox1.Text = DR3["Names"].ToString();
                        TextBox5.Text = DR3["LandPhone"].ToString();
                        TextBox6.Text = DR3["MobileNo"].ToString();
                        TextBox7.Text = DR3["HomeAddress"].ToString();
                        TextBox8.Text = DR3["Town"].ToString();
                        TextBox4.Text = DR3["IdNo"].ToString();
                    }
                DR3.Close(); DR3.Dispose(); DR3 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox1.Text = GridView1.SelectedRow.Cells[1].Text;
                TextBox2.Text = GridView1.SelectedRow.Cells[8].Text;
                TextBox4.Text = GridView1.SelectedRow.Cells[3].Text;
                TextBox5.Text = GridView1.SelectedRow.Cells[4].Text;
                TextBox6.Text = GridView1.SelectedRow.Cells[5].Text;
                TextBox7.Text = GridView1.SelectedRow.Cells[6].Text;
                TextBox8.Text = GridView1.SelectedRow.Cells[7].Text;
                string gendr = GridView1.SelectedRow.Cells[2].Text;
                if (gendr == "Male")
                {
                    DropDownList1.SelectedValue = "Male";
                }
                if (gendr == "Female")
                {
                    DropDownList1.SelectedValue = "Female";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }
    }
}