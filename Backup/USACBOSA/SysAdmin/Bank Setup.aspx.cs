using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.SysAdmin
{
    public partial class Bank_Setup : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader  DR1, DR2, DR3, DR4;
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
                bindDrop1();
                loadBanks();
            }
        }

        private void loadBanks()
        {
            try
            {
                string sehhh = "Select BankCode,BankName,AccNo,BankAccno,BranchName From BANKS";
                    DR1 = new WARTECHCONNECTION.cConnect().ReadDB(sehhh);
                    if (DR1.HasRows)
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView1.Visible = true;
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                    DR1.Close(); DR1.Dispose(); DR1 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void bindDrop1()
        {
           // DropDownList2.Items.Clear();
            try
            {
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add("");
                DR3 = new WARTECHCONNECTION.cConnect().ReadDB("SELECT accno FROM GLSETUP");
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        this.DropDownList2.Items.Add(DR3["accno"].ToString());
                    }
                DR3.Close(); DR3.Dispose(); DR3 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" || TextBox6.Text == "" || TextBox7.Text == "" || DropDownList3.Text == "" || DropDownList2.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter all the Bank Details required"); return;
                }
                else
                {
                    WARTECHCONNECTION.cConnect NN = new WARTECHCONNECTION.cConnect();
                    String mm = ("Select  * from Banks where BankCode='" + TextBox1.Text + "'");
                    DR1 = NN.ReadDB(mm);
                    if (DR1.HasRows)
                    {

                        while (DR1.Read())
                        {
                            WARSOFT.WARMsgBox.Show("Bank Already Exists!");
                            return;
                        }
                    }
                    else
                    {
                        String sql = "set dateformat ymd Insert into Banks(BankCode,BankName,BranchName,Address,Telephone,AuditID,AuditTime,AccNo,AccType,BankAccno) Values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox4.Text + "','" + TextBox6.Text + "','" + TextBox5.Text + "','" + Session["mimi"].ToString() + "','" + System.DateTime.Now.ToString("yyyy/MM/dd") + "','" + TextBox3.Text + "','" + DropDownList3.Text + "','" + DropDownList2.Text + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(sql);
                        clearTexts();
                        WARSOFT.WARMsgBox.Show("Record Saved Successfully");
                        
                    }
                    DR1.Close(); DR1.Dispose(); DR1 = null;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            DropDownList3.Text = "";
            DropDownList2.Text = "";
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect NANA = new WARTECHCONNECTION.cConnect();
                String mma = ("Select  * from Banks where BankCode='" + TextBox1.Text + "'");
                DR2 = NANA.ReadDB(mma);
                if (DR2.HasRows)
                    while (DR2.Read())
                    {
                        TextBox1.Text = DR2["BankCode"].ToString();
                        TextBox2.Text = DR2["BankName"].ToString();
                        TextBox3.Text = DR2["BankAccno"].ToString();
                        TextBox4.Text = DR2["BranchName"].ToString();
                        TextBox5.Text = DR2["Telephone"].ToString();
                        TextBox6.Text = DR2["Address"].ToString();
                        // TextBox7.Text = DR2["BankAccno"].ToString();
                        //DropDownList1.Text = DR2["AccType"].ToString();
                        //DropDownList2.Text = DR2["AccNo"].ToString();
                    }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGLAccNo(DropDownList2.Text.Trim());
        }

        private void loadGLAccNo(string Accnumber)
        {
            try
            {
                WARTECHCONNECTION.cConnect NANok = new WARTECHCONNECTION.cConnect();
                String mama = ("Select accno,glaccname from glsetup where accno='" + Accnumber + "'");
                DR3 = NANok.ReadDB(mama);
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        TextBox7.Text = DR3["glaccname"].ToString();
                    }
                DR3.Close(); DR3.Dispose(); DR3 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please the Enter Bank Code");
                }
                else
                {
                    String updait = "update Banks set BankCode='" + TextBox1.Text + "', BankName='" + TextBox2.Text + "',BranchName='" + TextBox4.Text + "',Address='" + TextBox6.Text + "',Telephone='" + TextBox5.Text + "',AccNo='" + DropDownList2.Text + "',AccType='" + DropDownList3.Text + "',BankAccno='" + TextBox3.Text + "' where BankCode='" + TextBox1.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(updait);
                    WARSOFT.WARMsgBox.Show("Bank Details updated sucessfully");
                    loadBanks();
                    clearTexts();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Provide BankCode");
                }
                else
                {
                    WARTECHCONNECTION.cConnect nono = new WARTECHCONNECTION.cConnect();
                    String mimi = ("Select  * from Banks where BankCode='" + TextBox1.Text + "'");
                    DR4 = nono.ReadDB(mimi);
                    if (DR4.HasRows)
                        while (DR4.Read())
                        {
                            TextBox1.Text = DR4["BankCode"].ToString();
                            TextBox2.Text = DR4["BankName"].ToString();
                            TextBox3.Text = DR4["BankAccno"].ToString();
                            TextBox4.Text = DR4["BranchName"].ToString();
                            TextBox5.Text = DR4["Telephone"].ToString();
                            TextBox6.Text = DR4["Address"].ToString();
                            // TextBox7.Text = DR2["BankAccno"].ToString();
                            //DropDownList1.Text = DR2["AccType"].ToString();
                            //DropDownList2.Text = DR2["AccNo"].ToString();
                        }
                    DR4.Close(); DR4.Dispose(); DR4 = null;
                    String delete = "Delete from Banks where BankCode='" + TextBox1.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(delete);
                    WARSOFT.WARMsgBox.Show("Record Deleted sucessfully");
                    clearTexts();
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox1.Text = GridView1.SelectedRow.Cells[1].Text;
                TextBox2.Text = GridView1.SelectedRow.Cells[2].Text;
               // DropDownList2.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
                //TextBox3.Text = GridView1.SelectedRow.Cells[4].Text;
                //TextBox4.Text = GridView1.SelectedRow.Cells[5].Text;
                getBankdetails(TextBox1.Text.Trim());
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getBankdetails(string bankcode)
        {
            DR1 = new WARTECHCONNECTION.cConnect().ReadDB("select BankCode,BankName,BranchName,Address,Telephone,AuditID,AuditTime,AccNo,AccType,BankAccno from banks where bankcode='" + bankcode + "'");
            if (DR1.HasRows)
            {
                while (DR1.Read())
                {
                    TextBox2.Text = DR1["BankName"].ToString().Trim();
                    TextBox3.Text = DR1["BankAccno"].ToString().Trim();
                    TextBox4.Text = DR1["BranchName"].ToString().Trim();
                    TextBox5.Text = DR1["Telephone"].ToString().Trim();
                    TextBox6.Text = DR1["Address"].ToString().Trim();
                    DropDownList3.SelectedValue = DR1["AccType"].ToString().Trim();
                    DropDownList2.SelectedValue = DR1["AccNo"].ToString().Trim();
                    loadGLAccNo(DropDownList2.Text.Trim());
                }
            }
            DR1.Close(); DR1.Dispose(); DR1 = null;
        }
    }
}
    