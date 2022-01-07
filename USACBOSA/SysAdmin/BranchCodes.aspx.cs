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
    public partial class BranchCodes : System.Web.UI.Page
    {
        SqlDataReader dr, dr1,dr2;
        SqlDataAdapter da;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBranches();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBranch.Text == "" || txtCode.Text == "" || txtName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Enter All The Required Details ");
                    return;
                }
               
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from  Branches WHERE  branchCODE='" + txtCode.Text + "' and branchname= '" + txtName.Text + "'");
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The Bank Code Already Exist");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
                string isntart = "insert into  Branches(branchCode,branchName,branchphysicaladdress,branchaddress,branchtelephoneno,email)select'" + txtCode.Text + "','" + txtName.Text + "','" + txtphysicaladdress.Text + "','" + txtaddress.Text + "','" + txtTelephone.Text + "','" + txtBranch.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(isntart);
                LoadBranches();
                WARSOFT.WARMsgBox.Show("Record has been successfully saved");
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadBranches()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT branchcode,branchname,branchphysicaladdress,branchaddress,branchtelephoneno,email,status FROM branches");
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

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
               

                this.txtCode.Text = GridView.SelectedRow.Cells[1].Text;
                this.txtName.Text = GridView.SelectedRow.Cells[2].Text;
                this.txtphysicaladdress.Text = GridView.SelectedRow.Cells[3].Text;
                this.txtaddress.Text = GridView.SelectedRow.Cells[4].Text;
                this.txtTelephone.Text = GridView.SelectedRow.Cells[5].Text;
                this.txtBranch.Text = GridView.SelectedRow.Cells[6].Text;
                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select * from branches where Branchcode='" + txtCode.Text + "'");
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        TextBox1.Text = dr2["bid"].ToString();
                    }
                }
                else
                {
                    return;
                }
                

                
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtBranch.Text == "" || txtCode.Text == "" || txtName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please Enter All The Required Details ");
                    return;
                }
               
                string ppsql = "update  branches set Branchcode ='" + txtCode.Text + "',branchname= '" + txtName.Text + "',branchphysicaladdress= '" + txtphysicaladdress.Text + "', branchaddress = '" + txtaddress.Text + "',branchtelephoneno='" + txtTelephone.Text + "', email='" + txtBranch.Text + "' where bid ='" + TextBox1.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(ppsql);
                LoadBranches();
                WARSOFT.WARMsgBox.Show("Record has been successfully updated");
                clearTexts();
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtaddress.Text = "";
            txtBranch.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtphysicaladdress.Text = "";
            txtTelephone.Text = "";
            
        }
    }
}