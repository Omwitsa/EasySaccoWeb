using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.Changepwd
{
    public partial class Passchange : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader DR, DR2, DR3;
        protected void Page_Load(object sender, EventArgs e)
        {
            string smimi = Session["mimi"].ToString();
            this.Label14.Text = smimi;
            DR = new WARTECHCONNECTION.cConnect().ReadDB("select username from useraccounts1 where userloginid='"+smimi+"'");
            if (DR.HasRows)
            {
                while(DR.Read())
                {
                    this.Label14.Text = DR["username"].ToString();
                }
            }
            DR.Close(); DR.Dispose(); DR = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string expiryDate = System.DateTime.Now.AddDays(Convert.ToInt32(30)).ToString();
            string smimi = Session["mimi"].ToString();
            string Password1 = Decryptor.Decript_String(TextBox1.Text);
            string Password = Decryptor.Decript_String(TextBox2.Text);
            string Password2 = Decryptor.Decript_String(TextBox3.Text);
            string myDate = System.DateTime.Now.ToString();
            // string reg = "^.{2,}(?<=\\d.*)(?<=[^a-zA-Z0-9].*)$";
            string psword = "";
            int passlength = 5;
            int passAge = 90;
            int PassHist = 1;
            int numPassrem = 8;
            int usrPassrem = 0;
            int pcomplexity = 1;
            try
            {
                WARTECHCONNECTION.cConnect cnt = new WARTECHCONNECTION.cConnect();
                DR = cnt.ReadDB("select top 1 passlength,passexpire,enforcepasshistory,ephnum,pcomplexity from Smis_policies ");
                if (DR.HasRows)
                    while (DR.Read())
                    {
                        passlength = Convert.ToInt32(DR["passlength"]);
                        passAge = Convert.ToInt32(DR["passexpire"]);
                        PassHist = Convert.ToInt32(DR["enforcepasshistory"]);
                        numPassrem = Convert.ToInt32(DR["ephnum"]);
                        pcomplexity = Convert.ToInt32(DR["pcomplexity"]);
                    }
                DR.Close(); DR.Dispose(); DR = null; cnt.Dispose(); cnt = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                WARSOFT.WARMsgBox.Show(ex.Message);
            }

            try
            {
                WARTECHCONNECTION.cConnect cnt = new WARTECHCONNECTION.cConnect();
                string sgl = "select userid,password,ephnum from passhistory where userid='" + smimi + "' and Password='" + Password2 + "'";
                DR2 = cnt.ReadDB(sgl);
                if (DR2.HasRows)
                    while (DR2.Read())
                    {
                        usrPassrem = Convert.ToInt32(DR2["ephnum"]);
                        if (usrPassrem < numPassrem)
                        {
                            WARSOFT.WARMsgBox.Show("The password you entered has been used before");
                            return;
                        }

                        if (usrPassrem >= numPassrem)
                        {
                            if (pcomplexity == 0)
                            {
                                string inDb = "set dateformat dmy Update UserAccounts1 set Password='" + Password2 + "',expirydate='" + expiryDate + "',passwordStatus='CHANGED',userstatus='ENABLED' where userloginID='" + smimi + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDb);
                                string upPassHist = "update passhistory set ephnum=1 where userid='" + smimi + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(upPassHist);
                                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('UserAccounts1','Password Changed " + Label14.Text.Trim() + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                                Session["mimi"] = "";
                                WARSOFT.WARMsgBox.Show("Password changed successfully");
                                Response.Redirect("~/Default.aspx", false);
                            }

                            if (pcomplexity == 1)
                            {
                                string inDb = "set dateformat dmy Update UserAccounts1 set Password='" + Password2 + "',expirydate='" + expiryDate + "',passwordStatus='CHANGED',userstatus='ENABLED' where userloginID='" + smimi + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDb);
                                string upPassHist = "update passhistory set ephnum=ephnum+1 where userid='" + smimi + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(upPassHist);
                                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID,AuditTime)values('UserAccounts1','Password Changed " + Label14.Text.Trim() + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "','" + System.DateTime.Now.ToString("hh:mm:ss") + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                                Session["mimi"] = "";
                                WARSOFT.WARMsgBox.Show("Password change done");
                                Response.Redirect("~/Default.aspx", false);
                            }

                        }
                    }
                else
                {
                    WARTECHCONNECTION.cConnect cnt2 = new WARTECHCONNECTION.cConnect();
                    DR = cnt2.ReadDB("select Password from UserAccounts1 where UserLoginID='" + smimi + "'");
                    if (DR.HasRows)
                        while (DR.Read())
                        {

                            psword = DR["Password"].ToString();

                            if (this.TextBox1.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("Enter Old Password");
                                TextBox1.Focus();
                                return;
                            }
                            if (this.TextBox2.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("Enter New Password");
                                TextBox2.Focus();
                                return;
                            }

                            if (this.TextBox3.Text == "")
                            {
                                WARSOFT.WARMsgBox.Show("Confirm Your New Password");
                                TextBox3.Focus();
                                return;
                            }
                            if (Password1 != psword)
                            {
                                WARSOFT.WARMsgBox.Show("Confirm your old password");
                                return;
                            }

                            if (this.TextBox1.Text == this.TextBox2.Text)
                            {
                                WARSOFT.WARMsgBox.Show("Old password should not be the same as new password");
                                return;
                            }

                            if (this.TextBox2.Text != this.TextBox3.Text)
                            {
                                WARSOFT.WARMsgBox.Show("Confirm your new password");
                                return;
                            }
                            if (TextBox2.Text.Length < passlength)
                            {
                                WARSOFT.WARMsgBox.Show("Password length short");
                                return;
                            }

                            else
                            {
                                if (pcomplexity == 0)
                                {
                                    string inDb = "set dateformat dmy Update UserAccounts1 set Password='" + Password2 + "',expirydate='" + expiryDate + "',passwordStatus='CHANGED',userstatus='ENABLED' where userloginID='" + smimi + "'";
                                    new WARTECHCONNECTION.cConnect().WriteDB(inDb);
                                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID,AuditTime)values('UserAccounts1','Password Changed " + Label14.Text.Trim() + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "','"+System.DateTime.Now.ToString("hh:mm:ss")+"')";
                                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                                    WARTECHCONNECTION.cConnect cnt8 = new WARTECHCONNECTION.cConnect();
                                    DR3 = cnt8.ReadDB("select userid from passhistory where userid='" + smimi + "'");
                                    if (DR3.HasRows)
                                        while (DR3.Read())
                                        {
                                            string upPassHist = "update passhistory set ephnum=ephnum+1 where userid='" + smimi + "'";
                                            new WARTECHCONNECTION.cConnect().WriteDB(upPassHist);
                                        }
                                    else
                                    {
                                        string inDB2 = "set dateformat dmy Insert into passhistory (userID,password,TransDate) values ('" + smimi + "','" + Password2 + "','" + myDate + "')";
                                        new WARTECHCONNECTION.cConnect().WriteDB(inDB2);
                                    }

                                    DR3.Close(); DR3.Dispose(); DR3 = null; cnt8.Dispose(); cnt8 = null;
                                    string trunDB;
                                    trunDB = "Delete from Session1 where session_ID='" + smimi + "'";
                                    new WARTECHCONNECTION.cConnect().WriteDB(trunDB);
                                    Session["mimi"] = "";
                                    WARSOFT.WARMsgBox.Show("Password changed");
                                    Response.Redirect("~/Default.aspx", false);
                                }

                                if (pcomplexity == 1)
                                {
                                    //if (Regex.IsMatch(this.TextBox2.Text, reg))
                                    // {
                                    string inDb = "Update UserAccounts1 set Password='" + Password2 + "',expirydate='" + expiryDate + "',passwordStatus='CHANGED',userstatus='ENABLED' where userloginID='" + smimi + "'";
                                    new WARTECHCONNECTION.cConnect().WriteDB(inDb);
                                    WARTECHCONNECTION.cConnect cnt8 = new WARTECHCONNECTION.cConnect();
                                    DR3 = cnt8.ReadDB("select userid from passhistory where userid='" + smimi + "'");
                                    if (DR3.HasRows)
                                        while (DR3.Read())
                                        {
                                            string upPassHist = "update passhistory set ephnum=ephnum+1 where userid='" + smimi + "'";
                                            new WARTECHCONNECTION.cConnect().WriteDB(upPassHist);
                                        }
                                    else
                                    {
                                        string inDB2 = "set dateformat dmy Insert into passhistory (userID,password,TransDate) values ('" + smimi + "','" + Password2 + "','" + myDate + "')";
                                        new WARTECHCONNECTION.cConnect().WriteDB(inDB2);
                                    }

                                    DR3.Close(); DR3.Dispose(); DR3 = null; cnt8.Dispose(); cnt8 = null;
                                    string trunDB;
                                    trunDB = "Delete from Session1 where session_ID='" + smimi + "'";
                                    new WARTECHCONNECTION.cConnect().WriteDB(trunDB);
                                    WARSOFT.WARMsgBox.Show("Password Change Done Successfully");
                                    Session["mimi"] = "";
                                    Response.Redirect("~/Default.aspx", false);
                                }
                            }
                        }
                    DR.Close(); DR.Dispose(); DR = null; cnt2.Dispose(); cnt2 = null;

                }
                DR2.Close(); DR2.Dispose(); DR2 = null; cnt.Dispose(); cnt = null;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }
    }
}