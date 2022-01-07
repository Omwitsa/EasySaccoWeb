using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace USACBOSA.Setup
{
    public partial class SystemUser : System.Web.UI.Page
    {
        SqlDataReader dr, dr1;
        SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            binddropdownlist();
            loadUsers();
        }

        private void binddropdownlist()
        {
            string constr = ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand com = new SqlCommand("select GroupName from usergroups", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter();
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select distinct GroupName from usergroups");
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            cboUser.DataTextField = ds.Tables[0].Columns["GroupName"].ToString(); // text field name of table dispalyed in dropdown
            cboUser.DataSource = ds.Tables[0];
            cboUser.DataBind();
        }

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                this.txtName.Text = GridView.SelectedRow.Cells[1].Text;
                this.txtID.Text = GridView.SelectedRow.Cells[2].Text;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            loadUsers();
        }

        private void loadUsers()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT UserName,UserLoginID,PassExpire,DateCreated,AssignGl,branchcode FROM useraccounts");
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

        protected void chkIsTeller_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsTeller.Checked == true)
            {
                txtTellerGl.Enabled = false;
                lblglteller.Enabled = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect conne = new WARTECHCONNECTION.cConnect();
                string stryg = "select GroupName from usergroups";
                dr = conne.ReadDB(stryg);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        cboUser.Text = dr["GroupName"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null; conne.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string super = "";
                string status = "";
                if (txtID.MaxLength < 3)
                {
                    WARSOFT.WARMsgBox.Show("User ID should be at Least Three Charaters");
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Password is Invalid");
                    return;
                }

                if (txtPassword.Text != txtConfirm.Text)
                {
                    WARSOFT.WARMsgBox.Show("Confirmation password must be the same as the password.Please re-enter again");
                    txtConfirm.Text = "";
                    txtConfirm.Focus();
                    return;
                }
                if (cboStatus.Text == "Logged Out")
                {
                    status = "0";
                }
                else if (cboStatus.Text == "Locked")
                {
                    status = "2";
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("You can't update a user as Logged in");
                    return;
                }
                if (txtPassword.Text.Contains("'"))
                {
                    WARSOFT.WARMsgBox.Show("Your password should not contain an apostrophe (')");
                    txtPassword.Text = "";
                    txtConfirm.Text = "";
                    txtPassword.Focus();
                    return;
                }
                string paswd = Decryptor.Decript_String(txtPassword.Text.Replace("'", ""));
                string upsql = "set dateformat dmy update useraccounts set username  ='" + txtName.Text + "', password='" + paswd + "', groupId ='" + cboUser.Text + "',AssignGl='" + txtTellerGl.Text + "',lastdate='" + System.DateTime.Now + "',branchcode='" + cbobranch.Text + "',status='" + status + "' where userloginid ='" + txtID.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(upsql);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string super = "";
                string status = "";
                if (txtID.MaxLength < 3)
                {
                    WARSOFT.WARMsgBox.Show("User ID should be at Least Three Charaters");
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Password is Invalid");
                    return;
                }

                if (txtPassword.Text != txtConfirm.Text)
                {
                    WARSOFT.WARMsgBox.Show("Confirmation password must be the same as the password.Please re-enter again");
                    txtConfirm.Text = "";
                    txtConfirm.Focus();
                    return;
                }
                if (cboStatus.Text == "Logged Out")
                {
                    status = "0";
                }
                else if (cboStatus.Text == "Locked")
                {
                    status = "2";
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("You can't update a user as Logged in");
                    return;
                }
                if (txtPassword.Text.Contains("'"))
                {
                    WARSOFT.WARMsgBox.Show("Your password should not contain an apostrophe (')");
                    txtPassword.Text = "";
                    txtConfirm.Text = "";
                    txtPassword.Focus();
                    return;
                }
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from useraccounts where userloginid='" + txtID.Text + "'");
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The user already exist, Do you want to update the details ?");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
                string paswd = Decryptor.Decript_String(txtPassword.Text.Replace("'", ""));
                string instsql = "set dateformat dmy insert into useraccounts(username,userloginid,password,groupId,datecreated,lastdate,superuser,AssignGl,branchcode,signed) values( '" + txtName.Text + "','" + txtID.Text + "','" + paswd + "','" + cboUser.Text + "','" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + super + "','" + txtTellerGl.Text + "','" + cbobranch.Text + "',0)";
                new WARTECHCONNECTION.cConnect().WriteDB(instsql);

                loadUsers();
                WARSOFT.WARMsgBox.Show("User Record Added/Updated Successfully!");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView.Visible = false;
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTellerGl.Text = GridView2.SelectedRow.Cells[1].Text;
            this.lblglteller.Text = GridView2.SelectedRow.Cells[2].Text;
            GridView2.Visible = false;
            GridView.Visible = true;
            loadUsers();
        }
    }
}