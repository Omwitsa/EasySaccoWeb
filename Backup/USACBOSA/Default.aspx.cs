using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace USACBOSA
{
    public partial class _Default : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, DR2, DR3, DR5, DR6, DR7;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Write("The Page Loaded at: " + DateTime.Now.ToLongTimeString());
                btnLogin.Focus();
                txtUserName.Focus();
            }
        }
        protected bool Authenticate(string username, string password)
        {
            DateTime now = DateTime.Now;
            bool b = false;
            WARTECHCONNECTION.cConnect cnt = new WARTECHCONNECTION.cConnect();
            DR = cnt.ReadDB("SELECT UserloginID, Password FROM UserAccounts1 WHERE UserLoginID='" + username + "' AND Password='" + password + "'");
            b = DR.HasRows;

            return b;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Password = "";
            Password = Decryptor.Decript_String(txtPassword.Text);
            string myExpDate;
            myExpDate = "";
            DateTime expDate, currDate;
            try
            {
                string t, dDate, InsertIntoDB;
                int seconds;
                string sessionStatus = "";

                int mycount = 0;
                DateTime dt = DateTime.Now;
                seconds = dt.Second;
                t = dt.ToString("T");
                string Username = this.txtUserName.Text;
                currDate = System.DateTime.Now;

                WARTECHCONNECTION.cConnect cnt = new WARTECHCONNECTION.cConnect();
                DR6 = cnt.ReadDB("select Password,passwordStatus,expirydate from UserAccounts1 where userLoginID='" + this.txtUserName.Text + "' and passwordStatus='CHANGE'");
                if (DR6.HasRows)
                {
                    DR6.Read();
                    if (this.Authenticate(this.txtUserName.Text, Password))
                    {
                        Session["mimi"] = this.txtUserName.Text;
                        Response.Redirect("~/Changepwd/Passchange.aspx", false);
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("Wrong password or username");
                        return;
                    }
                }
                else
                {
                    WARTECHCONNECTION.cConnect cnt2 = new WARTECHCONNECTION.cConnect();
                    DR7 = cnt2.ReadDB("select expirydate,count from UserAccounts1 where userLoginID='" + this.txtUserName.Text + "'");
                    if (DR7.HasRows)
                        while (DR7.Read())
                        {
                            myExpDate = DR7["expiryDate"].ToString();
                            mycount = Convert.ToInt32(DR7["count"]);
                        }
                    DR7.Close(); DR7.Dispose(); DR7 = null; cnt2.Dispose(); cnt2 = null;
                    expDate = System.DateTime.Now.AddDays(50);//Convert.ToDateTime(myExpDate);

                    if (mycount >= 10000)
                    {
                        WARSOFT.WARMsgBox.Show(" Account blocked, contact administrator...");
                        return;
                    }

                    else
                    {
                        if (currDate < expDate)
                        {
                            if (this.Authenticate(this.txtUserName.Text, Password))
                            {
                                WARTECHCONNECTION.cConnect cnt3 = new WARTECHCONNECTION.cConnect();
                                DR3 = cnt3.ReadDB("select top 1 Session_ID,Status from Session1 where Session_ID='" + Username + "' order by id desc");
                                if (DR3.HasRows)
                                {
                                    DR3.Read();
                                    sessionStatus = DR3["Status"].ToString();
                                    if (sessionStatus == "OUT")
                                    {
                                        WARTECHCONNECTION.cConnect cnt4 = new WARTECHCONNECTION.cConnect();
                                        DR5 = cnt4.ReadDB("select UserStatus from UserAccounts1 WHERE UserLoginID='" + Username + "'");
                                        if (DR5.HasRows)
                                            while (DR5.Read())
                                            {
                                                string usrStatus = DR5["UserStatus"].ToString();
                                                if (usrStatus == "ENABLED")
                                                {
                                                    WARTECHCONNECTION.cConnect cnt5 = new WARTECHCONNECTION.cConnect();
                                                    DR2 = cnt5.ReadDB("select getDate()");
                                                    WARTECHCONNECTION.cConnect cnt6 = new WARTECHCONNECTION.cConnect();
                                                    DR = cnt6.ReadDB("SELECT SUPERUSER,branchcode FROM UserAccounts1 WHERE UserLoginID='" + Username + "'");

                                                    if (DR2.HasRows)
                                                        while (DR2.Read())
                                                        {
                                                            dDate = DR2[0].ToString();
                                                            //t = DR2[0].ToString("T");
                                                            if (DR.HasRows)

                                                                while (DR.Read())
                                                                {
                                                                    int A;

                                                                    A = Convert.ToInt32(DR["SUPERUSER"]);
                                                                    if (A == 1)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);                                                              
                                                                        Response.Redirect("~/SysAdmin/SystemAdmin.aspx", false);
                                                                    }
                                                                    if (A == 2)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/FinanceAdmin/FinanceAdmin.aspx", false);
                                                                    }
                                                                    if (A == 3)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/LoansAdmin/LoansAdmin.aspx", false);
                                                                    }

                                                                    if (A == 4)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/CustomServAdmin/CustomServAdmin.aspx", false);

                                                                    }
                                                                    if (A == 5)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/ManagementAdmin/ManagementAdmin.aspx", false);
                                                                    }
                                                                    if (A == 6)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/HR_Admin/HR_Admin.aspx", false);
                                                                    }
                                                                    if (A == 7)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/CreditAdmin/CreditAdmin.aspx", false);
                                                                    }
                                                                    if (A == 8)
                                                                    {
                                                                        Session["mimi"] = this.txtUserName.Text;
                                                                        Session["Branch"] = DR["branchcode"].ToString();
                                                                        InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                        new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                        Response.Redirect("~/ClientAdmin/ClientAdmin.aspx", false);
                                                                    }
                                                                    string upmyussracc = "update UserAccounts1 set count=count+1 where userloginID='" + txtUserName.Text + "'";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(upmyussracc);
                                                                }
                                                            DR.Close(); DR.Dispose(); DR = null; cnt6.Dispose(); cnt6 = null;

                                                        }
                                                    DR2.Close(); DR2.Dispose(); DR2 = null; cnt5.Dispose(); cnt5 = null;
                                                }

                                                if (usrStatus == "DISABLED")
                                                {
                                                    WARSOFT.WARMsgBox.Show("Account is currently Disabled, Contact the Systems Administrator for assistance"); return;
                                                }
                                            }// mwisho ni hapa
                                        DR5.Close(); DR5.Dispose(); DR5 = null; cnt4.Dispose(); cnt4 = null;
                                    }
                                    if (sessionStatus == "IN")
                                    {
                                        WARSOFT.WARMsgBox.Show("You are already logged in");
                                        this.txtUserName.Text = "";
                                        this.txtPassword.Text = "";
                                    }
                                }
                                else
                                {

                                    WARTECHCONNECTION.cConnect cnt7 = new WARTECHCONNECTION.cConnect();
                                    DR5 = cnt7.ReadDB("select UserStatus from UserAccounts1 WHERE UserLoginID='" + Username + "'");
                                    if (DR5.HasRows)
                                        while (DR5.Read())
                                        {
                                            string usrStatus = DR5["UserStatus"].ToString();
                                            if (usrStatus == "ENABLED")
                                            {
                                                WARTECHCONNECTION.cConnect cnt8 = new WARTECHCONNECTION.cConnect();
                                                DR2 = cnt8.ReadDB("select getDate()");
                                                WARTECHCONNECTION.cConnect cnt9 = new WARTECHCONNECTION.cConnect();
                                                DR = cnt9.ReadDB("SELECT SUPERUSER,branchcode FROM UserAccounts1 WHERE UserLoginID='" + Username + "'");


                                                if (DR2.HasRows)
                                                    while (DR2.Read())
                                                    {
                                                        dDate = DR2[0].ToString();
                                                        if (DR.HasRows)

                                                            while (DR.Read())
                                                            {
                                                                int A;

                                                                A = Convert.ToInt32(DR["SUPERUSER"]);
                                                                if (A == 1)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,Status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);                                                                   
                                                                    Response.Redirect("~/SysAdmin/SystemAdmin.aspx", false);
                                                                }
                                                                if (A == 2)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,Status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/FinanceAdmin/FinanceAdmin.aspx", false);
                                                                }
                                                                if (A == 3)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,Status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/LoansAdmin/LoansAdmin.aspx", false);
                                                                }
                                                                if (A == 4)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,Status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/CustomServAdmin/CustomServAdmin.aspx", false);
                                                                }
                                                                if (A == 5)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    string updateDB = "update Session1 set status='IN' where UserLoginID='" + Username + "'";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(updateDB);
                                                                    Response.Redirect("~/ManagementAdmin/ManagementAdmin.aspx", false);
                                                                }
                                                                if (A == 6)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/HR_Admin/HR_Admin.aspx", false);
                                                                }
                                                                if (A == 7)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/CreditAdmin/CreditAdmin.aspx", false);
                                                                }
                                                                if (A == 8)
                                                                {
                                                                    Session["mimi"] = this.txtUserName.Text;
                                                                    Session["Branch"] = DR["branchcode"].ToString();
                                                                    InsertIntoDB = "set dateformat dmy Insert into Session1 (session_ID,dTime,dDate,status) VALUES ('" + Username + "','" + t + "','" + dDate + "','OUT')";
                                                                    new WARTECHCONNECTION.cConnect().WriteDB(InsertIntoDB);
                                                                    Response.Redirect("~/ClientAdmin/ClientAdmin.aspx", false);
                                                                }
                                                                string upmyussracc = "update UserAccounts1 set count=0 where userloginID='" + txtUserName.Text + "'";
                                                                new WARTECHCONNECTION.cConnect().WriteDB(upmyussracc);
                                                            }
                                                        DR.Close(); DR.Dispose(); DR = null; cnt9.Dispose(); cnt9 = null;
                                                    }
                                                DR2.Close(); DR2.Dispose(); DR2 = null; cnt8.Dispose(); cnt8 = null;
                                            }
                                            if (usrStatus == "DISABLED")
                                            {
                                                WARSOFT.WARMsgBox.Show("Account is currently Disabled, Contact the Systems Administrator for assistance"); return;
                                            }
                                        }
                                    DR5.Close(); DR5.Dispose(); DR5 = null; cnt7.Dispose(); cnt7 = null;
                                }
                                DR3.Close(); DR3.Dispose(); DR = null; cnt3.Dispose(); cnt3 = null;
                            }

                            else
                            {
                                string upmyussracc = "update UserAccounts1 set count=count+1 where userloginID='" + txtUserName.Text + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(upmyussracc);
                                WARSOFT.WARMsgBox.Show("Wrong password or username");
                                return;
                            }

                        }
                        if (currDate >= expDate)
                        {
                            WARSOFT.WARMsgBox.Show("Password expired please change your password");
                            Session["mimi"] = this.txtUserName.Text;
                            Response.Redirect("~/Changepwd/Passchange.aspx", false);

                        }
                    }
                }
                DR6.Close(); DR6.Dispose(); DR6 = null; cnt.Dispose(); cnt = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}