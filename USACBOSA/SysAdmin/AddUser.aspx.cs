using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace USACBOSA.SysAdmin
{
    public partial class AddUser : System.Web.UI.Page
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
                    loadgrid();
                    LoadBrances();
                    Loadcompany();
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
            }
        }

        private void LoadBrances()
        {
            try
            {
                DropDownList1.Items.Add("");
                DropDownList1.Items.Clear();
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
        private void Loadcompany()
        {
            try
            {
                DropDownList2.Items.Add("");
                DropDownList2.Items.Clear();
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select companycode from company order by companycode");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList2.Items.Add(dr["companycode"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int passlength = 5;
            int passAge = 90;
            int PassHist = 1;
            int pcomplexity = 1;
            // string reg = "^.{2,}(?<=\\d.*)(?<=[^a-zA-Z0-9].*)$";
            try
            {
                WARTECHCONNECTION.cConnect cnt4 = new WARTECHCONNECTION.cConnect();
                DR4 = cnt4.ReadDB("select top 1 ID,passlength,passexpire,enforcepasshistory,pcomplexity from Smis_policies order by id desc");
                if (DR4.HasRows)
                    while (DR4.Read())
                    {
                        passlength = Convert.ToInt32(DR4["passlength"]);
                        passAge = Convert.ToInt32(DR4["passexpire"]);
                        PassHist = Convert.ToInt32(DR4["enforcepasshistory"]);
                        pcomplexity = Convert.ToInt32(DR4["pcomplexity"]);
                    }
                DR4.Close(); DR4.Dispose(); DR4 = null; cnt4.Dispose(); cnt4 = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
            try
            {
                string loginID, usrName, usrPsswd, conpsswd, expDate, Branch, UserGroup, maze1;
                string dateCreated, expiryDate, branchcode;

                maze1 = "";
                branchcode = "";
                string Password = "";
                Password = Decryptor.Decript_String(txtPassword2.Text);
                string mimi = Session["mimi"].ToString();
                loginID = this.txtUserLoginId.Text;
                usrName = this.txtRUserName.Text;
                usrPsswd = this.txtPassword1.Text;
                conpsswd = this.txtPassword2.Text;
                expDate = this.TextBox1.Text;
                Branch = this.DropDownList1.Text;
                UserGroup = this.DropDownUserTypes.Text;

                WARTECHCONNECTION.cConnect cnt = new WARTECHCONNECTION.cConnect();
                DR3 = cnt.ReadDB("select DepCode from department where Name='" + DropDownList1.Text + "'");
                if (DR3.HasRows)
                {
                    DR3.Read();
                    branchcode = DR3["DepCode"].ToString();
                }
                DR3.Close(); DR3.Dispose(); DR3 = null; cnt.Dispose(); cnt = null;
                if (UserGroup == "Administrator")
                {
                    maze1 = "1";
                }
                if (UserGroup == "Agrovet")
                {
                    maze1 = "2";
                }
                if (UserGroup == "Accounts")
                {
                    maze1 = "3";
                }
                if (UserGroup == "Reception")
                {
                    maze1 = "4";
                }
                dateCreated = System.DateTime.Now.ToString();
                expiryDate = System.DateTime.Now.AddDays(Convert.ToInt32(expDate)).ToString();

                if (txtPassword1.Text == this.txtPassword2.Text)
                {
                    WARTECHCONNECTION.cConnect cnt2 = new WARTECHCONNECTION.cConnect();
                    DR = cnt2.ReadDB("select UserLoginID from UserAccounts1 where UserLoginID='" + this.txtUserLoginId.Text + "'");
                    if (DR.HasRows)
                    {
                        DR.Read();
                        WARSOFT.WARMsgBox.Show("User account exists...");
                    }
                    else
                    {
                        if (this.DropDownUserTypes.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user type...");
                            DropDownUserTypes.Focus();
                            return;
                        }

                        if (this.txtUserLoginId.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user login type...");
                            txtUserLoginId.Focus();
                            return;
                        }
                        if (this.txtRUserName.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user name...");
                            txtRUserName.Focus();
                            return;
                        }
                        if (this.txtPassword1.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter your password...");
                            txtPassword1.Focus();
                            return;
                        }

                        if (txtPassword1.Text.Length < passlength)
                        {
                            WARSOFT.WARMsgBox.Show("Password length does not meet the mininimum password length...");
                            return;
                        }

                        if (this.txtPassword2.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please confirm your password..");
                            txtPassword2.Focus();
                            return;
                        }

                        if (this.txtPassword1.Text != this.txtPassword2.Text)
                        {
                            WARSOFT.WARMsgBox.Show("Password confirmation should be the same as password...");
                            return;
                        }

                        else
                        {
                            if (pcomplexity == 0)
                            {
                                string inDB = "Set dateformat dmy Insert into UserAccounts1 (UserLoginID,UserName,PassExpire,UserGroup,DepCode,DateCreated,expiryDate,password,superuser,Department,euser,companycode)VALUES('" + loginID + "','" + usrName + "','" + expDate + "','" + UserGroup + "','" + branchcode + "','" + dateCreated + "','" + expiryDate + "','" + Password + "','" + maze1 + "','" + Branch + "','" + mimi + "','" + DropDownList2.Text + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                                WARSOFT.WARMsgBox.Show("User account has been created...");
                                //ClearText();
                                //loaddrop2();
                                DropDownList1.Enabled = true;
                                //loadMytext();
                            }

                            if (pcomplexity == 1)
                            {
                                string inDB = "Set dateformat dmy Insert into UserAccounts1 (UserLoginID,UserName,PassExpire,UserGroup,DepCode,DateCreated,expiryDate,password,superuser,Department,euser,companycode) Values ('" + loginID + "','" + usrName + "','" + expDate + "','" + UserGroup + "','" + branchcode + "','" + dateCreated + "','" + expiryDate + "','" + Password + "','" + maze1 + "','" + Branch + "','" + mimi + "','"+DropDownList2.Text+"')";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                                WARSOFT.WARMsgBox.Show("User account has been created...");
                                DropDownList1.Enabled = true;
                            }
                            else if (pcomplexity == 3)
                            {
                                string inDB = "Set dateformat dmy Insert into UserAccounts1 (UserLoginID,UserName,PassExpire,UserGroup,DepCode,DateCreated,expiryDate,password,superuser,Department,euser,companycode) Values ('" + loginID + "','" + usrName + "','" + expDate + "','" + UserGroup + "','" + branchcode + "','" + dateCreated + "','" + expiryDate + "','" + Password + "','" + maze1 + "','" + Branch + "','" + mimi + "','" + DropDownList2.Text + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                                WARSOFT.WARMsgBox.Show("User account has been created...");
                                DropDownList1.Enabled = true;
                            }
                        }
                        DR.Close(); DR.Dispose(); DR = null; cnt2.Dispose(); cnt2 = null;
                    }
                    if (txtPassword1.Text != this.txtPassword2.Text)
                    {
                        WARSOFT.WARMsgBox.Show("Password confirmation failed!");
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Clear();
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void loadgrid()
        {

            da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT UserName [User Full Names],UserLoginID [Login ID],UserGroup [User Group],Department [Branch],DateCreated [Date Created] from UserAccounts1 order by DateCreated desc");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }
        private void loadusertype()
        {
            DropDownUserTypes.Items.Clear();
            try
            {
                DropDownUserTypes.Items.Add("");
                DR = new WARTECHCONNECTION.cConnect().ReadDB("select distinct UserGroup from UserAccounts1");
                if (DR.HasRows)
                    while (DR.Read())
                    {
                        this.DropDownUserTypes.Items.Add(DR["UserGroup"].ToString());
                    }
                DR.Close(); DR.Dispose(); DR = null;
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void Loaddepart()
        {

            try
            {
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("");
                DR = new WARTECHCONNECTION.cConnect().ReadDB("select Name from department");
                if (DR.HasRows)
                    while (DR.Read())
                    {
                        this.DropDownList1.Items.Add(DR["Name"].ToString());
                    }
                DR.Close(); DR.Dispose(); DR = null;
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRUserName.Text = GridView1.SelectedRow.Cells[1].Text;
            txtUserLoginId.Text = GridView1.SelectedRow.Cells[2].Text;
            //DropDownUserTypes.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
            //DropDownList1.SelectedValue = GridView1.SelectedRow.Cells[4].Text;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            int passlength = 5;
            int passAge = 90;
            int PassHist = 1;
            int pcomplexity = 1;
            try
            {
                WARTECHCONNECTION.cConnect cnt4 = new WARTECHCONNECTION.cConnect();
                DR4 = cnt4.ReadDB("select top 1 passlength,passexpire,enforcepasshistory,pcomplexity from Smis_policies ");
                if (DR4.HasRows)
                    while (DR4.Read())
                    {
                        passlength = Convert.ToInt32(DR4["passlength"]);
                        passAge = Convert.ToInt32(DR4["passexpire"]);
                        PassHist = Convert.ToInt32(DR4["enforcepasshistory"]);
                        pcomplexity = Convert.ToInt32(DR4["pcomplexity"]);
                    }
                DR4.Close(); DR4.Dispose(); DR4 = null; cnt4.Dispose(); cnt4 = null;
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
            try
            {
                string loginID, usrName, usrPsswd, conpsswd, expDate, Branch, UserGroup, maze1;
                string dateCreated, expiryDate, branchcode;

                maze1 = "";
                branchcode = "";
                string Password = "";
                Password = Decryptor.Decript_String(txtPassword2.Text);
                string mimi = Session["mimi"].ToString();
                loginID = this.txtUserLoginId.Text;
                usrName = this.txtRUserName.Text;
                usrPsswd = this.txtPassword1.Text;
                conpsswd = this.txtPassword2.Text;
                expDate = this.TextBox1.Text;
                Branch = this.DropDownList1.Text;
                UserGroup = this.DropDownUserTypes.Text;

                branchcode = DropDownList1.Text.Trim();

                if (UserGroup == "System Administrator")
                {
                    maze1 = "1";
                }
                if (UserGroup == "Finance Officer")
                {
                    maze1 = "2";
                }
                if (UserGroup == "Loans Officer")
                {
                    maze1 = "3";
                }

                if (UserGroup == "Teller/Customer Service")
                {
                    maze1 = "4";
                }
                if (UserGroup == "Management")
                {
                    maze1 = "5";
                }
                if (UserGroup == "Human Resource")
                {
                    maze1 = "6";
                }
                if (UserGroup == "Credit Officer")
                {
                    maze1 = "7";
                }
                string ENABLED = "";
                if (chkEnable.Checked == true)
                {
                    ENABLED = "ENABLE";
                }
                else
                {
                    ENABLED = "DISABLED";
                }
                dateCreated = System.DateTime.Now.ToString();
                expiryDate = System.DateTime.Now.AddDays(Convert.ToInt32(expDate)).ToString();

                if (txtPassword1.Text == this.txtPassword2.Text)
                {
                    WARTECHCONNECTION.cConnect cnt2 = new WARTECHCONNECTION.cConnect();
                    DR = cnt2.ReadDB("select UserLoginID from UserAccounts1 where UserLoginID='" + this.txtUserLoginId.Text + "'");
                    if (DR.HasRows)
                    {
                        DR.Read();
                        WARSOFT.WARMsgBox.Show("User account exists...");
                    }
                    else
                    {
                        if (this.DropDownUserTypes.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user type...");
                            DropDownUserTypes.Focus();
                            return;
                        }

                        if (this.txtUserLoginId.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user login type...");
                            txtUserLoginId.Focus();
                            return;
                        }
                        if (this.txtRUserName.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter user name...");
                            txtRUserName.Focus();
                            return;
                        }
                        if (this.txtPassword1.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please enter your password...");
                            txtPassword1.Focus();
                            return;
                        }

                        if (txtPassword1.Text.Length < passlength)
                        {
                            WARSOFT.WARMsgBox.Show("Password length does not meet the mininimum password length...");
                            return;
                        }

                        if (this.txtPassword2.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("Please confirm your password..");
                            txtPassword2.Focus();
                            return;
                        }

                        if (this.txtPassword1.Text != this.txtPassword2.Text)
                        {
                            WARSOFT.WARMsgBox.Show("Password confirmation should be the same as password...");
                            return;
                        }
                        else
                        {
                            string inDB = "Set dateformat dmy Insert into UserAccounts1 (UserLoginID,UserName,PassExpire,UserGroup,DepCode,DateCreated,expiryDate,password,superuser,Department,euser,count,passwordStatus,userstatus,vendor_ID,companycode)VALUES('" + loginID + "','" + usrName + "','" + expDate + "','" + UserGroup + "','" + branchcode + "','" + dateCreated + "','" + expiryDate + "','" + Password + "','" + maze1 + "','" + Branch + "','" + mimi + "','0','CHANGE','" + ENABLED + "','0','" + DropDownList2.Text + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                            WARSOFT.WARMsgBox.Show("User account has been created..");
                            DropDownList1.Enabled = true;
                            loadgrid();
                        }
                    }
                    DR.Close(); DR.Dispose(); DR = null; cnt2.Dispose(); cnt2 = null;
                }
                if (txtPassword1.Text != this.txtPassword2.Text)
                {
                    WARSOFT.WARMsgBox.Show("Password confirmation failed!");
                    txtPassword2.Focus();
                    return;
                }
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void DropDownUserTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                string loginID, usrName, usrPsswd, conpsswd, expDate, Branch, UserGroup, maze1;
                string dateCreated, expiryDate, branchcode;

                maze1 = "";
                branchcode = "";
                string Password = "";
                Password = Decryptor.Decript_String(txtPassword2.Text);
                string mimi = Session["mimi"].ToString();
                loginID = this.txtUserLoginId.Text;
                usrName = this.txtRUserName.Text;
                usrPsswd = this.txtPassword1.Text;
                conpsswd = this.txtPassword2.Text;
                expDate = this.TextBox1.Text;
                Branch = this.DropDownList1.Text;
                UserGroup = this.DropDownUserTypes.Text;

                branchcode = DropDownList1.Text.Trim();

                if (UserGroup == "System Administrator")
                {
                    maze1 = "1";
                }
                if (UserGroup == "Finance Officer")
                {
                    maze1 = "2";
                }
                if (UserGroup == "Loans Officer")
                {
                    maze1 = "3";
                }

                if (UserGroup == "Teller/Customer Service")
                {
                    maze1 = "4";
                }
                if (UserGroup == "Management")
                {
                    maze1 = "5";
                }
                if (UserGroup == "Human Resource")
                {
                    maze1 = "6";
                }
                if (UserGroup == "Credit Officer")
                {
                    maze1 = "7";
                }
                else if (maze1 == "")
                {
                    WARSOFT.WARMsgBox.Show("User type is not specified");
                    return;
                }
                string userstatus = "";
                if (chkEnable.Checked == true)
                {
                    userstatus = "ENABLED";
                }
                else if (chkEnable.Checked == false)
                {
                    userstatus = "DISABLED";
                }
                string update = "Set dateformat dmy update UserAccounts1 set userstatus='" + userstatus + "', UserLoginID='" + loginID + "',UserName='" + usrName + "',UserGroup='" + UserGroup + "',DepCode='" + branchcode + "',superuser='" + maze1 + "',Department='" + Branch + "',euser='" + mimi + "',companycode='" + DropDownList2.Text + "' where UserLoginID='" + txtUserLoginId.Text.Trim() + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(update);
                WARSOFT.WARMsgBox.Show("User Details updated sucessfully");
                return;
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            if (txtUserLoginId.Text == "" || txtRUserName.Text == "")
            {
                WARSOFT.WARMsgBox.Show("");
            }
            else
            {
                String sql = "set dateformat ymd update UserAccounts1 set Password='?<372',expirydate='" + System.DateTime.Today.ToString("yyyy/MM/dd") + "' where UserLoginID='" + txtUserLoginId.Text + "' and UserName='" + txtRUserName.Text + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(sql);
                WARSOFT.WARMsgBox.Show("Password for " + txtRUserName.Text + " has been Successfully Reset!");
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                string loginID, usrName, usrPsswd, conpsswd, expDate, Branch, UserGroup, maze1;
                string dateCreated, expiryDate, branchcode;

                maze1 = "";
                branchcode = "";
                string Password = "";
                Password = Decryptor.Decript_String(txtPassword2.Text);
                string mimi = Session["mimi"].ToString();
                loginID = this.txtUserLoginId.Text;
                usrName = this.txtRUserName.Text;
                usrPsswd = this.txtPassword1.Text;
                conpsswd = this.txtPassword2.Text;
                expDate = this.TextBox1.Text;
                Branch = this.DropDownList1.Text;
                UserGroup = this.DropDownUserTypes.Text;

                branchcode = DropDownList1.Text.Trim();

                if (UserGroup == "System Administrator")
                {
                    maze1 = "1";
                }
                if (UserGroup == "Finance Officer")
                {
                    maze1 = "2";
                }
                if (UserGroup == "Loans Officer")
                {
                    maze1 = "3";
                }

                if (UserGroup == "Teller/Customer Service")
                {
                    maze1 = "4";
                }
                if (UserGroup == "Management")
                {
                    maze1 = "5";
                }
                if (UserGroup == "Human Resource")
                {
                    maze1 = "6";
                }
                if (UserGroup == "Credit Officer")
                {
                    maze1 = "7";
                }
                else if (maze1 == "")
                {
                    WARSOFT.WARMsgBox.Show("User type is not specified");
                    return;
                }
                string userstatus = "";
                if (chkEnable.Checked == true)
                {
                    userstatus = "ENABLED";
                }
                else if (chkEnable.Checked == false)
                {
                    userstatus = "DISABLED";
                }
                string update = "Set dateformat dmy delete from UserAccounts1  where UserLoginID='" + txtUserLoginId.Text.Trim() + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(update);
                WARSOFT.WARMsgBox.Show("User Deleted sucessfully");
                return;
            }
            catch (Exception ex) { ex.Data.Clear(); WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void chkEnable_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
    