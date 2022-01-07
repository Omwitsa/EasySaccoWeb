﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace USACBOSA.LoansAdmin
{
    public partial class MembershipRegistration : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, drU;
        System.Data.SqlClient.SqlDataAdapter da;
        int memmebno, memmebno1; string MMMMMM, MMMMMM1;


        protected void Page_Load(object sender, EventArgs e)
        {
            // txtRegDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
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
                LoadMembers();
                DateTime ddate = DateTime.Today;
                // txtRegDate.Text = Convert.ToDateTime(ddate).ToString("dd-MM-yyyy");
                LoadCompany();
                loadAgents();
                LoadBrances();
                GridView1.DataBind();
                GridView2.Visible = false;
            }
        }

        private void LoadBrances()
        {
            try
            {
                cboStation.Items.Clear();
                cboStation.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches order by branchname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboStation.Items.Add(dr["branchname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void loadAgents()
        {
            try
            {
                cboAgentId.Items.Clear();
                cboAgentId.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select idno,names from agents order by idno desc");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboAgentId.Items.Add(dr["idno"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadCompany()
        {
            try
            {
                cboCompanyCode.Items.Clear();
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyCode from Company order by CompanyCode desc");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboCompanyCode.Items.Add(dr["CompanyCode"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadMembers()
        {
            GridView2.Visible = false;
            da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy SELECT MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members order by applicdate desc");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Member Number is required");
                    txtMemberNo.Focus();
                    return;
                }
                if (txtSurname.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Surname of the member is required");
                    txtSurname.Focus();
                    return;
                }
                if (txtOtherNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Other Names of the member are required");
                    txtOtherNames.Focus();
                    return;
                }
                if (txtDob.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The date of birth of the member is required");
                    txtDob.Focus();
                    return;
                }
                if (txtIdNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The ID Number of the member is required");
                    txtIdNo.Focus();
                    return;
                }
                if (txtPhoneNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Phone Number of the member is required");
                    txtPhoneNo.Focus();
                    return;
                }
                if (txtRegDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Registration Date is required");
                    txtRegDate.Focus();
                    return;
                }
                string mstatus = "";
                if (cboMaritalStatus.Text == "Married")
                {
                    mstatus = "1";
                }
                else if (cboMaritalStatus.Text == "Single")
                {
                    mstatus = "0";
                }
                else if (cboMaritalStatus.Text == "Others")
                {
                    mstatus = "2";
                }
                string withdrawn = "";
                if (cboStatus.Text == "Active")
                {
                    withdrawn = "0";
                }
                if (cboStatus.Text == "Withdrawn")
                {
                    withdrawn = "1";
                }
                if (cboStatus.Text == "Deceased")
                {
                    withdrawn = "2";
                }
                if (cboStatus.Text == "Dormant")
                {
                    withdrawn = "3";
                }
                // end of validation of details required above
                // data is inserted/ saved/update into the database here below
                string memberno = "";
                WARTECHCONNECTION.cConnect chhhek = new WARTECHCONNECTION.cConnect();
                string existss = "select memberno from members where memberno='" + txtMemberNo.Text + "'";
                dr = chhhek.ReadDB(existss);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("Member Number " + txtMemberNo.Text + " already exist");
                        return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; chhhek.Dispose(); chhhek = null;
                string existss2 = "select memberno from members where memberno='" + txtMemberNo.Text.TrimStart('J') + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(existss2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("Member Number " + txtMemberNo.Text + " already exist");
                        return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                string exists2 = "select idno from members where idno='" + txtIdNo.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(exists2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WARSOFT.WARMsgBox.Show("id Number " + txtIdNo.Text + " already exist");
                        return;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
                string existss22 = "select max(substring(MemberNo,4,5)) as MemberNo from members where memberno not like 'J%' and rank='County'";
                dr4 = new WARTECHCONNECTION.cConnect().ReadDB(existss22);
                if (dr4.HasRows)
                    while (dr4.Read())
                    {
                        MMMMMM = dr4["MemberNo"].ToString();
                        int membern = Convert.ToInt32(MMMMMM);
                        string suff = txtMemberNo.Text.ToString();
                        string sub = suff.Substring(3, 5);
                        if (MMMMMM == sub)
                        {
                            WARSOFT.WARMsgBox.Show("Member Number autogenerated  " + txtMemberNo.Text + " already exist, generate another one");
                            return;
                        }
                        else
                        {
                            string CompanyCode = cboCompanyCode.Text.Trim();
                            if (txtCompany.Text == "")
                            {
                                CompanyCode = "";
                            }
                            if (cboMemberType.Text == "Normal Member")
                            {
                                imageupload();

                                string savedata = "set dateformat dmy insert into members(CompanyCode,MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,PIN,Photo,ShareCap,BankCode,Bname,AuditID,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Rank,Age) values('" + CompanyCode + "','" + txtMemberNo.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + txtIdNo.Text + "','" + txtSurname.Text.Replace("'", " ") + "','" + txtOtherNames.Text.Replace("'", " ") + "','" + DropDownList2.Text.Trim() + "','" + txtDob.Text + "','" + txtCompany.Text.Trim() + "','','','" + txtPostalAddress.Text.Trim() + "','" + txtLandLine.Text + "','','','','','" + txtRegDate.Text + "','','" + System.DateTime.Today + "','','','','','" + withdrawn + "','" + cboStation.Text + "','" + cboStation.Text + "','" + cboStation.Text + "','" + txtPinNo.Text + "','','','','','" + Session["mimi"].ToString() + "','','','','','','','','','','','','','" + txtEmail.Text.Trim() + "','','" + txtPhoneNo.Text.Trim() + "','" + cboAgentId.Text.Trim() + "','" + txtPhoneNo.Text + "','True','" + withdrawn + "','" + mstatus + "','" + DropDownList1.Text + "','" + txtAge.Text.Trim() + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(savedata);
                                string online = "set dateformat ymd insert into UserAccounts2 (UserName, UserLoginID, Password, GroupId, PassExpire, DateCreated, SUPERUSER, AssignGl, branchcode, levels, Authorize, Status, Branch,signed, LoginBal, Lastdate, passwordstatus) Values('" + txtSurname.Text.Replace("'", " ") + "'" + "'" + txtOtherNames.Text.Replace("'", " ") + "','" + txtMemberNo.Text.Trim() + "','?<372','ADMIN','30','" + System.DateTime.Today.ToString("yyyy-MM-dd") + "','0','','" + DropDownList3.Text + "','','','','','false','','','CHANGE')";
                                new WARTECHCONNECTION.cConnect().WriteDB(online);
                            }
                            if (cboMemberType.Text == "Junior Member")
                            {
                                imageuploadJunior();
                                string savedata = "set dateformat dmy insert into members(CompanyCode,MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,PIN,Photo,ShareCap,BankCode,Bname,AuditID,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Rank,Age) values('" + cboCompanyCode.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + txtMemberNumber.Text.Trim() + "','" + txtIdNo.Text + "','" + txtSurname.Text.Replace("'", " ") + "','" + txtOtherNames.Text.Replace("'", " ") + "','" + DropDownList2.Text.Trim() + "','" + txtDob.Text + "','" + txtCompany.Text.Trim() + "','','','" + txtPostalAddress.Text.Trim() + "','" + txtLandLine.Text + "','','','','','" + txtRegDate.Text + "','','" + txtRegDate.Text + "','','','','','" + withdrawn + "','" + cboStation.Text + "','" + cboStation.Text + "','" + cboStation.Text + "','" + txtPinNo.Text + "','','','','','" + Session["mimi"].ToString() + "','','','','','','','','','','','','','" + txtEmail.Text.Trim() + "','','" + txtPhoneNo.Text.Trim() + "','" + cboAgentId.Text.Trim() + "','" + txtPhoneNo.Text + "','True','" + withdrawn + "','" + mstatus + "','" + DropDownList1.Text + "','" + txtAge.Text.Trim() + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(savedata);
                                string online1 = "set dateformat ymd insert into UserAccounts2 (UserName, UserLoginID, Password, GroupId, PassExpire, DateCreated, SUPERUSER, AssignGl, branchcode, levels, Authorize, Status, Branch,signed, LoginBal, Lastdate, passwordstatus) Values('" + txtSurname.Text.Replace("'", " ") + "'" + "'" + txtOtherNames.Text.Replace("'", " ") + "','" + txtMemberNo.Text.Trim() + "','?<372','ADMIN','30','" + System.DateTime.Today.ToString("yyyy-MM-dd") + "','0','','" + DropDownList3.Text + "','','','','','false','','','CHANGE')";
                                new WARTECHCONNECTION.cConnect().WriteDB(online1);
                            }
                            string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('members','Added member memberno " + txtMemberNo.Text + "','" + System.DateTime.Today + "','0','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                            new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                            LoadMembers();
                            ClearTexts();
                            WARSOFT.WARMsgBox.Show("Membership details sucessfully saved");
                        }
                    }
                else
                {
                    string CompanyCode = cboCompanyCode.Text.Trim();
                    if (txtCompany.Text == "")
                    {
                        CompanyCode = "";
                    }
                    if (cboMemberType.Text == "Normal Member")
                    {
                        imageupload();

                        string savedata = "set dateformat dmy insert into members(CompanyCode,MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,PIN,Photo,ShareCap,BankCode,Bname,AuditID,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Rank,Age) values('" + CompanyCode + "','" + txtMemberNo.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + txtIdNo.Text + "','" + txtSurname.Text.Replace("'", " ") + "','" + txtOtherNames.Text.Replace("'", " ") + "','" + DropDownList2.Text.Trim() + "','" + txtDob.Text + "','" + txtCompany.Text.Trim() + "','','','" + txtPostalAddress.Text.Trim() + "','" + txtLandLine.Text + "','','','','','" + txtRegDate.Text + "','','" + System.DateTime.Today + "','','','','','" + withdrawn + "','" + cboStation.Text + "','" + cboStation.Text + "','" + cboStation.Text + "','" + txtPinNo.Text + "','','','','','" + Session["mimi"].ToString() + "','','','','','','','','','','','','','" + txtEmail.Text.Trim() + "','','" + txtPhoneNo.Text.Trim() + "','" + cboAgentId.Text.Trim() + "','" + txtPhoneNo.Text + "','True','" + withdrawn + "','" + mstatus + "','" + DropDownList1.Text + "','" + txtAge.Text.Trim() + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(savedata);
                        string online = "set dateformat ymd insert into UserAccounts2 (UserName, UserLoginID, Password, GroupId, PassExpire, DateCreated, SUPERUSER, AssignGl, branchcode, levels, Authorize, Status, Branch,signed, LoginBal, Lastdate, passwordstatus) Values('" + txtSurname.Text.Replace("'", " ") + "'" + "'" + txtOtherNames.Text.Replace("'", " ") + "','" + txtMemberNo.Text.Trim() + "','?<372','ADMIN','30','" + System.DateTime.Today.ToString("yyyy-MM-dd") + "','0','','" + DropDownList3.Text + "','','','','','false','','','CHANGE')";
                        new WARTECHCONNECTION.cConnect().WriteDB(online);
                    }
                    if (cboMemberType.Text == "Junior Member")
                    {
                        imageuploadJunior();
                        string savedata = "set dateformat dmy insert into members(CompanyCode,MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,PIN,Photo,ShareCap,BankCode,Bname,AuditID,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Rank,Age) values('" + cboCompanyCode.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + txtMemberNumber.Text.Trim() + "','" + txtIdNo.Text + "','" + txtSurname.Text.Replace("'", " ") + "','" + txtOtherNames.Text.Replace("'", " ") + "','" + DropDownList2.Text.Trim() + "','" + txtDob.Text + "','" + txtCompany.Text.Trim() + "','','','" + txtPostalAddress.Text.Trim() + "','" + txtLandLine.Text + "','','','','','" + txtRegDate.Text + "','','" + txtRegDate.Text + "','','','','','" + withdrawn + "','" + cboStation.Text + "','" + cboStation.Text + "','" + cboStation.Text + "','" + txtPinNo.Text + "','','','','','" + Session["mimi"].ToString() + "','','','','','','','','','','','','','" + txtEmail.Text.Trim() + "','','" + txtPhoneNo.Text.Trim() + "','" + cboAgentId.Text.Trim() + "','" + txtPhoneNo.Text + "','True','" + withdrawn + "','" + mstatus + "','" + DropDownList1.Text + "','" + txtAge.Text.Trim() + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(savedata);
                        string online1 = "set dateformat ymd insert into UserAccounts2 (UserName, UserLoginID, Password, GroupId, PassExpire, DateCreated, SUPERUSER, AssignGl, branchcode, levels, Authorize, Status, Branch,signed, LoginBal, Lastdate, passwordstatus) Values('" + txtSurname.Text.Replace("'", " ") + "'" + "'" + txtOtherNames.Text.Replace("'", " ") + "','" + txtMemberNo.Text.Trim() + "','?<372','ADMIN','30','" + System.DateTime.Today.ToString("yyyy-MM-dd") + "','0','','" + DropDownList3.Text + "','','','','','false','','','CHANGE')";
                        new WARTECHCONNECTION.cConnect().WriteDB(online1);
                    }
                    string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('members','Added member memberno " + txtMemberNo.Text + "','" + System.DateTime.Today + "','0','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(audittrans);
                    LoadMembers();
                    ClearTexts();
                    WARSOFT.WARMsgBox.Show("Membership details sucessfully saved");
                }
                dr4.Close(); dr4.Dispose(); dr4 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void imageuploadJunior()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
                    con.Open();

                    int imagefilelenth = FileUpload1.PostedFile.ContentLength;
                    byte[] imgarray = new byte[imagefilelenth];
                    HttpPostedFile image = FileUpload1.PostedFile;
                    image.InputStream.Read(imgarray, 0, imagefilelenth);
                    string query = "";
                    string exiist = "select JuniorNo from JuniorMemberPhotos where JuniorNo='" + txtMemberNo.Text + "'";
                    drU = new WARTECHCONNECTION.cConnect().ReadDB(exiist);
                    if (drU.HasRows)
                    {
                        while (drU.Read())
                        {
                            query = " Delete from JuniorMemberPhotos  where JuniorNo ='" + txtMemberNo.Text + "' ";
                            SqlCommand com = new SqlCommand(query, con);
                            com.Parameters.AddWithValue("@JuniorNo", SqlDbType.VarChar).Value = txtMemberNo.Text;
                            com.Parameters.AddWithValue("@Photo", SqlDbType.Image).Value = imgarray;
                            com.ExecuteNonQuery();
                            break;
                        }
                    }
                    drU.Close(); drU.Dispose(); drU = null;
                    query = "select JuniorNo,Photo from JuniorMemberPhotos where JuniorNo='" + txtMemberNo.Text + "'";
                    query = "Insert into JuniorMemberPhotos (JuniorNo,Photo) values (@JuniorNo,@Photo)";
                    SqlCommand comm = new SqlCommand(query, con);
                    comm.Parameters.AddWithValue("@JuniorNo", SqlDbType.VarChar).Value = txtMemberNo.Text;
                    comm.Parameters.AddWithValue("@Photo", SqlDbType.Image).Value = imgarray;
                    comm.ExecuteNonQuery();
                    imgPhoto.ImageUrl = "../memberjuniorphoto.aspx?id=" + txtMemberNo.Text;
                    WARSOFT.WARMsgBox.Show("Junior Member photo uploaded successfully");
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void imageupload()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
                    con.Open();

                    int imagefilelenth = FileUpload1.PostedFile.ContentLength;
                    byte[] imgarray = new byte[imagefilelenth];
                    HttpPostedFile image = FileUpload1.PostedFile;
                    image.InputStream.Read(imgarray, 0, imagefilelenth);
                    string query = "";
                    string exiist = "select MemberNo from MemberPhotos where MemberNo='" + txtMemberNo.Text + "'";
                    drU = new WARTECHCONNECTION.cConnect().ReadDB(exiist);
                    if (drU.HasRows)
                    {
                        while (drU.Read())
                        {
                            query = " Delete from MemberPhotos  where MemberNo ='" + txtMemberNo.Text + "' ";
                            SqlCommand com = new SqlCommand(query, con);
                            com.Parameters.AddWithValue("@MemberNo", SqlDbType.VarChar).Value = txtMemberNo.Text;
                            com.Parameters.AddWithValue("@Photo", SqlDbType.Image).Value = imgarray;
                            com.ExecuteNonQuery();
                            break;
                        }
                    }
                    drU.Close(); drU.Dispose(); drU = null;
                    query = "select MemberNo,Photo from MemberPhotos where MemberNo='" + txtMemberNo.Text + "'";
                    query = "Insert into MemberPhotos (MemberNo,Photo) values (@MemberNo,@Photo)";
                    SqlCommand comm = new SqlCommand(query, con);
                    comm.Parameters.AddWithValue("@MemberNo", SqlDbType.VarChar).Value = txtMemberNo.Text;
                    comm.Parameters.AddWithValue("@Photo", SqlDbType.Image).Value = imgarray;
                    comm.ExecuteNonQuery();
                    imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
                    //WARSOFT.WARMsgBox.Show("Member photo uploaded successfully");
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            // ClearTexts();
            populateTexts();
            imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
        }

        private void populateTexts()
        {
            try
            {
                WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                string memebr = "Select SUBSTRING(MemberNo,1,3)memtype,MemberNo,StaffNo,Station,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,CompanyCode,PIN,Photo,ShareCap,BankCode,Bname,AuditID,AuditTime,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Age FROM MEMBERS WHERE MemberNo='" + txtMemberNo.Text + "'";
                dr = cooon.ReadDB(memebr);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtDob.Text = dr["DOB"].ToString().Trim();
                        txtEmail.Text = dr["EmailAddress"].ToString().Trim();
                        txtIdNo.Text = dr["IDNo"].ToString().Trim();
                        txtLandLine.Text = dr["OfficeTelNo"].ToString().Trim();
                        txtMemberNo.Text = dr["MemberNo"].ToString().Trim();
                        string memtype = dr["memtype"].ToString().Trim();

                        txtOtherNames.Text = dr["OtherNames"].ToString().Trim().Replace("'", " ");
                        txtPhoneNo.Text = dr["MobileNo"].ToString().Trim().Replace("'", " ");

                        txtPinNo.Text = dr["PIN"].ToString().Replace("'", " ");
                        txtPostalAddress.Text = dr["PresentAddr"].ToString().Replace("'", " ");
                        txtRegDate.Text = dr["AsAtDate"].ToString().Replace("'", " ");

                        txtSurname.Text = dr["Surname"].ToString().Replace("'", " ");
                        txtAge.Text = dr["Age"].ToString();
                        cboMaritalStatus.SelectedValue = dr["mstatus"].ToString();

                        bool withdrawn = Convert.ToBoolean(dr["Withdrawn"].ToString());

                        string Station = dr["Station"].ToString();
                        if (Station != "")
                        {
                            cboStation.SelectedValue = Station;
                        }
                        string sex = dr["Sex"].ToString();
                        if (sex != "")
                        {
                            DropDownList2.SelectedValue = sex;
                        }
                        string companycode = dr["CompanyCode"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyName from Company where CompanyCode='" + companycode + "'");
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                cboCompanyCode.SelectedValue = companycode;
                                txtCompany.Text = dr1["CompanyName"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        string Agentid = dr["AgentId"].ToString().Trim();
                        if (Agentid != "")
                        {

                            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select idno,names from agents where idno='" + Agentid + "'");
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    cboAgentId.SelectedValue = Agentid.Trim();
                                    txtAgentNames.Text = dr2["names"].ToString();
                                }
                            }
                            dr2.Close(); dr2.Dispose(); dr2 = null;
                        }
                        if (memtype.Contains('J'))
                        {

                        }
                        dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select LType,LocationName from Locations where LocationCode='" + memtype + "'");
                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                string ltyp = dr3["LType"].ToString();
                                string lname = dr3["LocationName"].ToString();
                                if (ltyp == "County")
                                {
                                    DropDownList1.SelectedValue = "County";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                                if (ltyp == "Diaspora")
                                {
                                    DropDownList1.SelectedValue = "Diaspora";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                                if (ltyp == "Staff")
                                {
                                    DropDownList1.SelectedValue = "Staff";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                            }
                        }
                        dr3.Close(); dr3.Dispose(); dr3 = null;
                        ////if (txtMemberNo.Text.Contains('J'))
                        ////{
                        ////    imageuploadJunior();
                        ////}
                        ////else
                        ////{
                        ////    imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
                        ////}

                        //imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + GridView1.SelectedRow.Cells[1].Text.Trim();
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The member can not be found, Enter the correct member number and try again");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null; cooon.Dispose(); cooon = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTexts();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void ClearTexts()
        {
            try
            {
                // cboCompanyCode.Text = "";
                txtDob.Text = "";
                txtEmail.Text = "";
                txtIdNo.Text = "";
                txtLandLine.Text = "";
                txtMemberNo.Text = "";
                txtOtherNames.Text = "";
                txtPhoneNo.Text = "";
                txtPinNo.Text = "";
                txtPostalAddress.Text = "";
                txtRegDate.Text = "";
                cboStation.Text = "";
                txtSurname.Text = "";
                DropDownList1.Text = "";
                DropDownList2.Text = "";
                txtAge.Text = "";
                cboAgentId.SelectedValue = "";
                txtAgentNames.Text = "";
                cboStatus.Text = "";
                cboMaritalStatus.Text = "";
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtMemberNo_DataBinding(object sender, EventArgs e)
        {
            populateTexts();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                membertrypechange();
                ClearTexts();
                txtMemberNo.Text = GridView1.SelectedRow.Cells[1].Text;
                txtIdNo.Text = GridView1.SelectedRow.Cells[2].Text;
                txtSurname.Text = GridView1.SelectedRow.Cells[3].Text;
                txtOtherNames.Text = GridView1.SelectedRow.Cells[4].Text;

                populateTexts();

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.Visible = false;
            try
            {

                if (DropDownList5.Text == "Member No")
                {
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where memberno ='" + txtSearch.Text + "'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else if (DropDownList5.Text == "Names")
                {
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where surname like'%" + txtSearch.Text + "%' or OtherNames like'%" + txtSearch.Text + "%'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else if (DropDownList5.Text == "ID Number")
                {

                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where IDNo ='" + txtSearch.Text + "'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("No Details to Show");
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "County")
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add("");
                string locationss = "SELECT LocationCode,LocationName from Locations where LType='" + DropDownList1.Text + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(locationss);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        DropDownList3.Items.Add(dr["LocationName"].ToString().Trim());
                    }
                dr.Close(); dr.Dispose(); dr = null;
            }
            if (DropDownList1.Text == "Staff")
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add("");
                WARTECHCONNECTION.cConnect diasp = new WARTECHCONNECTION.cConnect();
                string ssellect = "select LType from Locations where LType='" + DropDownList1.Text + "'";
                dr = diasp.ReadDB(ssellect);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList3.Items.Add(dr["LType"].ToString().Trim());
                        break;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; diasp.Dispose(); diasp = null;
            }
            if (DropDownList1.Text == "Diaspora")
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add("");
                WARTECHCONNECTION.cConnect diasp = new WARTECHCONNECTION.cConnect();
                string ssellect = "select LType from Locations where LType='" + DropDownList1.Text + "'";
                dr = diasp.ReadDB(ssellect);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList3.Items.Add(dr["LType"].ToString().Trim());
                        break;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; diasp.Dispose(); diasp = null;
            }
        }

        protected void DropDownList3_TextChanged(object sender, EventArgs e)
        {
            // Autogenerate member number required here based on the goup allocated for the member below
            if (DropDownList3.Text != "")
            {
                string LocationCode = "";
                string locationss = "SELECT LocationCode,LocationName from Locations";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(locationss);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        LocationCode = dr["LocationCode"].ToString().Trim();
                    }
                dr.Close(); dr.Dispose(); dr = null;
            }
            else
            {
                WARSOFT.WARMsgBox.Show("Select the Location mapping for the member");
                return;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMemberType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Specify the member type first");
                    cboMemberType.Focus();
                    return;
                }
                LoadMemberCategory();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadMemberCategory()
        {
            if (DropDownList1.Text == "Staff")
            {
                DropDownList3.SelectedValue = "Staff";
            }
            if (DropDownList1.Text == "Diaspora")
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add("");
                WARTECHCONNECTION.cConnect diasp = new WARTECHCONNECTION.cConnect();
                string ssellect = "select LType from Locations where LType='" + DropDownList1.Text + "'";
                dr4 = diasp.ReadDB(ssellect);
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        DropDownList3.Items.Add(dr4["LType"].ToString().Trim());
                        break;
                    }
                }
                dr4.Close(); dr4.Dispose(); dr4 = null; diasp.Dispose(); diasp = null;
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                AutogenerateMemberNo();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void AutogenerateMemberNo()
        {
            try
            {
                if (cboMemberType.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please select the member type to continue.");
                    return;
                }
                if (cboMemberType.Text == "Normal Member")
                {
                    if (DropDownList3.Text == "Diaspora")
                    {
                        string suffix = "";
                        string membbber = "select max(substring(MemberNo,4,5)) as MemberNo from members where   memberno like 'D49%' and memberno<>'D4913888' and memberno<>'D4913887' and memberno<>'D4913886' and memberno<>'D4913885' and memberno<>'D4913884' and memberno<>'D4913883' and memberno<>'D4913882' and memberno<>'D4913881' and memberno<>'D4913880' and memberno<>'D4913879' and memberno<>'D4913878' and memberno<>'D4913877' and memberno<>'D4913889' and memberno<>'D4913890' and memberno<>'D4913891' and rank='" + DropDownList1.Text.Trim() + "'"; //and
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                MMMMMM = dr["MemberNo"].ToString();
                                if (MMMMMM == "")
                                {
                                    MMMMMM = "000";
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                else
                                {
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                suffix = suffix.PadLeft(5, '0');
                                break;
                            }
                        dr.Close(); dr.Dispose(); dr = null;
                        string prefix = "";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        string generate = "select LocationCode from Locations where LocationName='" + DropDownList3.Text + "'";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(generate);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                prefix = (dr["LocationCode"].ToString());
                            }
                        dr.Close(); dr.Dispose(); dr = null;

                        string MNo = prefix + suffix;
                        txtMemberNo.Text = MNo.ToString();
                    }
                    if (DropDownList3.Text == "Staff")
                    {
                        string suffix = "";
                        string membbber = "select max(substring(MemberNo,4,5)) as MemberNo from members where   memberno not like 'J%' and rank='" + DropDownList1.Text.Trim() + "'"; //and
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                MMMMMM = dr["MemberNo"].ToString();
                                if (MMMMMM == "")
                                {
                                    MMMMMM = "000";
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                else
                                {
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                suffix = suffix.PadLeft(5, '0');
                                break;
                            }
                        dr.Close(); dr.Dispose(); dr = null;
                        string prefix = "";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        string generate = "select LocationCode from Locations where LocationName='" + DropDownList3.Text + "'";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(generate);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                prefix = (dr["LocationCode"].ToString());
                            }
                        dr.Close(); dr.Dispose(); dr = null;

                        string MNo = prefix + suffix;
                        txtMemberNo.Text = MNo.ToString();
                    }
                    if (DropDownList3.Text != "Diaspora" & DropDownList3.Text != "Staff")
                    {
                        string suffix1 = "";
                        string jmember = "select max(substring(MemberNo,5,5)) as MemberNo from members where   memberno like 'J%'";// rank='County' and
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(jmember);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                MMMMMM1 = dr["MemberNo"].ToString();
                                if (MMMMMM1 == "")
                                {
                                    MMMMMM1 = "000";
                                    memmebno1 = Convert.ToInt32(MMMMMM1);
                                    int Intsuffix1 = memmebno1 + 1;
                                    suffix1 = Convert.ToString(Intsuffix1);
                                }
                                else
                                {
                                    memmebno1 = Convert.ToInt32(MMMMMM1);
                                    int Intsuffix1 = memmebno1 + 1;
                                    suffix1 = Convert.ToString(Intsuffix1);
                                }
                                suffix1 = suffix1.PadLeft(5, '0');
                                break;
                            }
                        }
                        dr.Close(); dr.Dispose(); dr = null;

                        string suffix = "";
                        string membbber = "select max(substring(MemberNo,4,5)) as MemberNo from members where   memberno not like 'J%' and rank='" + DropDownList1.Text.Trim() + "'"; //and
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                MMMMMM = dr["MemberNo"].ToString();
                                if (MMMMMM == "")
                                {
                                    MMMMMM = "000";
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                else
                                {
                                    memmebno = Convert.ToInt32(MMMMMM);
                                    int Intsuffix = memmebno + 1;
                                    suffix = Convert.ToString(Intsuffix);
                                }
                                suffix = suffix.PadLeft(5, '0');
                                break;
                            }
                        dr.Close(); dr.Dispose(); dr = null;
                        string prefix = "";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                        string generate = "select LocationCode from Locations where LocationName='" + DropDownList3.Text + "'";
                        dr = new WARTECHCONNECTION.cConnect().ReadDB(generate);
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                prefix = (dr["LocationCode"].ToString());
                            }
                        dr.Close(); dr.Dispose(); dr = null;

                        if (Convert.ToInt32(suffix1) > Convert.ToInt32(suffix))
                        {
                            string MNo = prefix + suffix1;
                            txtMemberNo.Text = MNo.ToString();
                        }
                        else if (Convert.ToInt32(suffix1) < Convert.ToInt32(suffix))
                        {
                            string MNo = prefix + suffix;
                            txtMemberNo.Text = MNo.ToString();
                        }
                        else
                        {
                            string MNo = prefix + suffix;
                            txtMemberNo.Text = MNo.ToString();
                        }
                    }
                }
                else if (cboMemberType.Text == "Junior Member")
                {
                    string suffix1 = "";
                    string jmember = "select max(substring(MemberNo,5,5)) as MemberNo from members where  memberno like 'J%'";//rank='County' and
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(jmember);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            MMMMMM1 = dr["MemberNo"].ToString();
                            if (MMMMMM1 == "")
                            {
                                MMMMMM1 = "000";
                                memmebno1 = Convert.ToInt32(MMMMMM1);
                                int Intsuffix1 = memmebno1 + 1;
                                suffix1 = Convert.ToString(Intsuffix1);
                            }
                            else
                            {
                                memmebno1 = Convert.ToInt32(MMMMMM1);
                                int Intsuffix1 = memmebno1 + 1;
                                suffix1 = Convert.ToString(Intsuffix1);
                            }
                            suffix1 = suffix1.PadLeft(5, '0');
                            break;
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null;

                    string suffix = "";
                    string membbber = "select max(substring(MemberNo,4,5)) as MemberNo from members where  memberno not like 'J%'";//rank='County' and
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            MMMMMM = dr["MemberNo"].ToString();
                            if (MMMMMM == "")
                            {
                                MMMMMM = "000";
                                memmebno = Convert.ToInt32(MMMMMM);
                                int Intsuffix = memmebno + 1;
                                suffix = Convert.ToString(Intsuffix);
                            }
                            else
                            {
                                memmebno = Convert.ToInt32(MMMMMM);
                                int Intsuffix = memmebno + 1;
                                suffix = Convert.ToString(Intsuffix);
                            }
                            suffix = suffix.PadLeft(5, '0');
                            break;
                        }
                    dr.Close(); dr.Dispose(); dr = null;
                    string prefix = "";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(membbber);
                    string generate = "select LocationCode from Locations where LocationName='" + DropDownList3.Text + "'";
                    dr = new WARTECHCONNECTION.cConnect().ReadDB(generate);
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            prefix = (dr["LocationCode"].ToString());
                        }
                    dr.Close(); dr.Dispose(); dr = null;

                    if (Convert.ToInt32(suffix1) > Convert.ToInt32(suffix))
                    {
                        string MNo = prefix + suffix1;
                        txtMemberNo.Text = 'J' + MNo.ToString();
                    }
                    else if (Convert.ToInt32(suffix1) < Convert.ToInt32(suffix))
                    {
                        string MNo = prefix + suffix;
                        txtMemberNo.Text = 'J' + MNo.ToString();
                    }
                    else
                    {
                        string MNo = prefix + suffix;
                        txtMemberNo.Text = 'J' + MNo.ToString();
                    }
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnBeneficiary_Click1(object sender, EventArgs e)
        {
            Session["memberno"] = txtMemberNo.Text.Trim();
            Response.Redirect("/SysAdmin/Beneficiary.aspx", false);
        }

        protected void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyName from Company where CompanyCode='" + cboCompanyCode.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtCompany.Text = dr["CompanyName"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                //GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
                //BindData();
                da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy SELECT MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members order by applicdate desc");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            try
            {
                WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                string memebr = "Select * FROM MEMBERS WHERE MemberNo='" + txtMemberNo.Text + "'";
                dr = cooon.ReadDB(memebr);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (txtMemberNo.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The Member Number is required");
                            txtMemberNo.Focus();
                            return;
                        }
                        if (txtSurname.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The Surname of the member is required");
                            txtSurname.Focus();
                            return;
                        }
                        if (txtOtherNames.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The Other Names of the member are required");
                            txtOtherNames.Focus();
                            return;
                        }
                        if (txtDob.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The date of birth of the member is required");
                            txtDob.Focus();
                            return;
                        }
                        if (txtIdNo.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The ID Number of the member is required");
                            txtIdNo.Focus();
                            return;
                        }

                        if (txtPhoneNo.Text == "")
                        {
                            WARSOFT.WARMsgBox.Show("The Phone Number of the member is required");
                            txtPhoneNo.Focus();
                            return;
                        }
                        string mstatus = "";
                        if (cboMaritalStatus.Text == "Married")
                        {
                            mstatus = "1";
                        }
                        else if (cboMaritalStatus.Text == "Single")
                        {
                            mstatus = "0";
                        }
                        else if (cboMaritalStatus.Text == "Others")
                        {
                            mstatus = "2";
                        }
                        string withdrawn = "";
                        if (cboStatus.Text == "Active")
                        {
                            withdrawn = "0";
                        }
                        if (cboStatus.Text == "Withdrawn")
                        {
                            withdrawn = "1";
                        }
                        if (cboStatus.Text == "Deceased")
                        {
                            withdrawn = "2";
                        }
                        if (cboStatus.Text == "Dormant")
                        {
                            withdrawn = "3";
                        }
                        // end of validation of details required above
                        // data is inserted/ saved/update into the database here below
                        string memberno = "";
                        string existss = "select memberno from members where memberno='" + txtMemberNo.Text + "'";
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB(existss);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                imageupload();
                                memberno = dr1["memberno"].ToString();
                                string mobileno = "";
                                if (txtPhoneNo.Text.Contains('-'))
                                {
                                    string[] mycontact = txtPhoneNo.Text.Split('-');
                                    string mmobil = mycontact[0];
                                    string mbile = mycontact[1];
                                    mobileno = (mmobil + mbile).Replace(" ", "").Trim();
                                }
                                else
                                {
                                    mobileno = txtPhoneNo.Text.Trim();
                                }
                                string CompanyCode = cboCompanyCode.Text.Trim();
                                if (txtCompany.Text == "")
                                {
                                    CompanyCode = "";
                                }
                                string savedata = "set dateformat dmy update members set CompanyCode= '" + CompanyCode + "',MemberNo='" + txtMemberNo.Text.Trim() + "',StaffNo='" + txtMemberNo.Text.Trim() + "',IDNo='" + txtIdNo.Text.Trim() + "',Surname='" + txtSurname.Text.Trim().Replace("'", " ") + "',OtherNames='" + txtOtherNames.Text.Trim().Replace("'", " ") + "',Sex='" + DropDownList2.Text.Trim() + "',DOB='" + txtDob.Text + "',Employer='" + txtCompany.Text.Trim() + "',Dept='',Terms='',PresentAddr='" + txtPostalAddress.Text.Trim() + "',OfficeTelNo='" + txtLandLine.Text.Trim() + "',HomeAddr='',HomeTelNo='',RegFee='',InitShares='',AsAtDate='" + txtRegDate.Text.Trim() + "',MonthlyContr='',EffectDate='',Signed='',Accepted='',Archived='',Withdrawn='" + withdrawn + "',Province='" + cboStation.Text.Trim() + "',District='" + cboStation.Text.Trim() + "',Station='" + cboStation.Text.Trim() + "',PIN='" + txtPinNo.Text.Trim() + "',Photo='',ShareCap='',BankCode='',Bname='',AuditID='" + Session["mimi"].ToString() + "',AuditTime='" + System.DateTime.Now + "',E_DATE='',posted='',initsharesTransfered='',Transferdate='',LoanBalance='',InterestBalance='',FormFilled='',EmailAddress='" + txtEmail.Text + "',accno='',memberwitrawaldate='',Dormant='',MemberDescription='',email='',TransactionNo='',MobileNo='" + mobileno + "',AgentId='" + cboAgentId.Text.Trim() + "',PhoneNo='" + mobileno + "',Entrance='',status='" + withdrawn + "',mstatus='" + mstatus + "',Rank='" + DropDownList1.Text + "',Age='" + txtAge.Text.Trim() + "' where memberno='" + memberno + "'";
                                new WARTECHCONNECTION.cConnect().WriteDB(savedata);

                                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('members','Updated member memberno " + txtMemberNo.Text + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                                WARSOFT.WARMsgBox.Show("Membership details updated sucessfully");
                                ClearTexts();
                                LoadMembers();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The member can not be found, Enter the correct member number and try again");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboAgentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("Select idno,names from agents where idno='" + cboAgentId.Text + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtAgentNames.Text = dr["names"].ToString();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtDob_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date1 = System.DateTime.Today;
                DateTime date2 = Convert.ToDateTime(txtDob.Text);

                int DateDiff = (date1.Year - date2.Year);
                txtAge.Text = DateDiff.ToString();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboMemberType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                membertrypechange();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void membertrypechange()
        {
            if (cboMemberType.Text == "Normal Member")
            {
                Label10.Text = "ID Number";
                DropDownList1.Visible = true;
                txtMemberNumber.Visible = false;
                Label29.Text = "Category:";
                Label7.Text = "Member No.";
            }
            if (cboMemberType.Text == "Junior Member")
            {
                Label10.Text = "Birth Cert. No";
                DropDownList1.Visible = false;
                txtMemberNumber.Visible = true;
                Label29.Text = "Member No.";
                Label7.Text = "Junior No.";
            }
        }

        protected void txtMemberNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                populateTexts2();
                //DropDownList1.Text = "County";
                string locationss = "SELECT LocationCode,LocationName from Locations where LType='County'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(locationss);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        DropDownList3.Items.Add(dr["LocationName"].ToString().Trim());
                    }
                dr.Close(); dr.Dispose(); dr = null;
                ClearTexts();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void populateTexts2()
        {
            try
            {
                WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                string memebr = "Select SUBSTRING(MemberNo,1,4)memtype,MemberNo,StaffNo,Station,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,CompanyCode,PIN,Photo,ShareCap,BankCode,Bname,AuditID,AuditTime,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Age FROM MEMBERS WHERE MemberNo='" + txtMemberNumber.Text + "'";
                dr = cooon.ReadDB(memebr);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtDob.Text = dr["DOB"].ToString().Trim();
                        txtEmail.Text = dr["EmailAddress"].ToString().Trim();
                        txtIdNo.Text = dr["IDNo"].ToString().Trim();
                        txtLandLine.Text = dr["OfficeTelNo"].ToString().Trim();
                        //txtMemberNo.Text = dr["MemberNo"].ToString().Trim();
                        string memtype = dr["memtype"].ToString().Trim();

                        // txtOtherNames.Text = dr["OtherNames"].ToString().Trim();
                        txtPhoneNo.Text = dr["MobileNo"].ToString().Trim();

                        txtPinNo.Text = dr["PIN"].ToString();
                        txtPostalAddress.Text = dr["PresentAddr"].ToString();
                        txtRegDate.Text = dr["AsAtDate"].ToString();
                        cboStation.SelectedValue = dr["Station"].ToString();
                        // txtSurname.Text = dr["Surname"].ToString();
                        DropDownList2.SelectedValue = dr["Sex"].ToString();
                        txtAge.Text = dr["Age"].ToString();
                        bool mstatus = Convert.ToBoolean(dr["mstatus"].ToString());

                        bool withdrawn = Convert.ToBoolean(dr["Withdrawn"].ToString());
                        if (withdrawn == true)
                        {
                            WARSOFT.WARMsgBox.Show("The member has withdrawn from the sacco membership");
                            return;
                        }
                        cboCompanyCode.SelectedValue = dr["CompanyCode"].ToString();
                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyName from Company where CompanyCode='" + cboCompanyCode.Text.Trim() + "'");
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                txtCompany.Text = dr1["CompanyName"].ToString();
                            }
                        }
                        dr1.Close(); dr1.Dispose(); dr1 = null;
                        string Agentid = dr["AgentId"].ToString().Trim();
                        if (Agentid != "")
                        {
                            cboAgentId.SelectedValue = Agentid.Trim();
                            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("Select idno,names from agents where idno='" + cboAgentId.Text.Trim() + "'");
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    txtAgentNames.Text = dr2["names"].ToString();
                                }
                            }
                            dr2.Close(); dr2.Dispose(); dr2 = null;
                        }
                        dr3 = new WARTECHCONNECTION.cConnect().ReadDB("select LType,LocationName from Locations where LocationCode='" + memtype + "'");
                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                string ltyp = dr3["LType"].ToString();
                                string lname = dr3["LocationName"].ToString();
                                if (ltyp == "County")
                                {
                                    DropDownList1.SelectedValue = "County";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                                if (ltyp == "Diaspora")
                                {
                                    DropDownList1.SelectedValue = "Diaspora";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                                if (ltyp == "Staff")
                                {
                                    DropDownList1.SelectedValue = "Staff";
                                    LoadMemberCategory();
                                    // DropDownList3.Text = lname;
                                }
                            }
                        }
                        dr3.Close(); dr3.Dispose(); dr3 = null;
                        imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
                    }
                }
                else
                {
                    WARSOFT.WARMsgBox.Show("The member can not be found, Enter the correct member number and try again");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null; cooon.Dispose(); cooon = null;
                imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select memberno,station from members");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string mno = dr["memberno"].ToString();
                        string stion = dr["station"].ToString().TrimEnd().TrimStart();
                        new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy update members set station='" + stion.Trim() + "' where memberno='" + mno + "'");
                    }
                    WARSOFT.WARMsgBox.Show("STATIONS UPDATED SUCESSFULLY");
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                membertrypechange();
                ClearTexts();
                txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
                txtIdNo.Text = GridView2.SelectedRow.Cells[2].Text;
                txtSurname.Text = GridView2.SelectedRow.Cells[3].Text;
                txtOtherNames.Text = GridView2.SelectedRow.Cells[4].Text;

                populateTexts();
                imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String userphoneno = txtPhoneNo.Text;
                if (IsValidPhone(userphoneno))
                {

                    Label30.Text = "";
                    Label30.Visible = true;
                }
                else
                {

                    Label30.Text = "not correct format!";
                    Label30.Visible = true;
                    txtPhoneNo.Text = "";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected bool IsValidPhone(string phone)
        {
            //Remove anything that is not a number
            string strphone = Regex.Replace(phone, @"[^\d]", String.Empty);
            txtPhoneNo.Text = strphone;
            //check for exactly 12 numbers left over

            return (strphone.Length == 12) || (strphone.Length == 10);
        }
    }
}




