using System;
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

namespace USACBOSA.SysAdmin
{
    public partial class MemberRegistration : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, drU;
        System.Data.SqlClient.SqlDataAdapter da;
        int memmebno, memmebno1; string MMMMMM, MMMMMM1;
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
                LoadMembers();
                DateTime ddate = DateTime.Today;
                // txtRegDate.Text = Convert.ToDateTime(ddate).ToString("dd-MM-yyyy");
                AutogenerateMemberNo();
                loadAgents();
                LoadBrances();
            }
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

        private void LoadMembers()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy SELECT MemberNo [Member No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth] from members order by memberno desc");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
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
                else
                {
                    imageupload();

                    string savedata = "set dateformat dmy insert into members(CompanyCode,MemberNo,StaffNo,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,PIN,Photo,ShareCap,BankCode,Bname,AuditID,AuditTime,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Rank,Age) values('C123','" + txtMemberNo.Text.Trim() + "','" + txtMemberNo.Text.Trim() + "','" + txtIdNo.Text + "','" + txtSurname.Text + "','" + txtOtherNames.Text + "','" + DropDownList2.Text.Trim() + "','" + txtDob.Text + "','Default Company','','','" + txtPostalAddress.Text.Trim() + "','" + txtLandLine.Text + "','','','','','" + txtRegDate.Text + "','','" + System.DateTime.Today + "','','','','','" + withdrawn + "','','','" + cboStation.Text + "','" + txtPinNo.Text + "','','','','','" + Session["mimi"].ToString() + "','','','','','','','','','','','','','','" + txtEmail.Text.Trim() + "','','" + txtPhoneNo.Text.Trim() + "','" + cboAgentId.Text.Trim() + "','" + txtPhoneNo.Text + "','True','" + withdrawn + "','" + mstatus + "','" + cboType.Text + "','" + txtAge.Text.Trim() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(savedata);
                    string online = "set dateformat ymd insert into UserAccounts2 (UserName, UserLoginID, Password, GroupId, PassExpire, DateCreated, SUPERUSER, AssignGl, branchcode, levels, Authorize, Status, Branch,signed, LoginBal, Lastdate, passwordstatus) Values('" + txtSurname.Text + "'" + "'" + txtOtherNames.Text + "','" + txtMemberNo.Text.Trim() + "','?<372','ADMIN','30','" + System.DateTime.Today.ToString("yyyy-MM-dd") + "','0','','" +cboType.Text + "','','','','','false','','','CHANGE')";
                    new WARTECHCONNECTION.cConnect().WriteDB(online);
                    
                    LoadMembers();
                    ClearTexts();
                    WARSOFT.WARMsgBox.Show("Membership details sucessfully saved");
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void AutogenerateMemberNo()
        {
            string suffix1 = "";
            string jmember = "select max(MemberNo) as MemberNo from members";
            dr1 = new WARTECHCONNECTION.cConnect().ReadDB(jmember);
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    MMMMMM1 = dr1["MemberNo"].ToString();
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
                    txtMemberNo.Text = suffix1.ToString();
                    break;
                }
            }
            dr1.Close(); dr1.Dispose(); dr1 = null;
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
                    drU.Close(); drU.Dispose();
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

        private void ClearTexts()
        {
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
            txtAge.Text = "";
            cboAgentId.Text = "";
            cboMaritalStatus.SelectedValue = "";
            cboType.SelectedValue = "";
            cboStatus.SelectedValue = "";
            DropDownList2.SelectedValue = "";
            txtAgentNames.Text = "";
            AutogenerateMemberNo();
        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            populateTexts();
            imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
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
                        imageupload();
                        memberno = dr["memberno"].ToString();
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

                        string savedata = "set dateformat dmy update members set MemberNo='" + txtMemberNo.Text.Trim() + "',StaffNo='" + txtMemberNo.Text.Trim() + "',IDNo='" + txtIdNo.Text.Trim() + "',Surname='" + txtSurname.Text.Trim() + "',OtherNames='" + txtOtherNames.Text.Trim() + "',Sex='" + DropDownList2.Text.Trim() + "',DOB='" + txtDob.Text + "',PresentAddr='" + txtPostalAddress.Text.Trim() + "',OfficeTelNo='" + txtLandLine.Text.Trim() + "',AsAtDate='" + txtRegDate.Text.Trim() + "',Withdrawn='" + withdrawn + "',Province='',District='" + cboStation.Text.Trim() + "',Station='" + cboStation.Text.Trim() + "',PIN='" + txtPinNo.Text.Trim() + "',AuditID='" + Session["mimi"].ToString() + "',AuditTime='" + System.DateTime.Now + "',EmailAddress='" + txtEmail.Text + "',MobileNo='" + mobileno + "',AgentId='" + cboAgentId.Text.Trim() + "',PhoneNo='" + mobileno + "',status='" + withdrawn + "',mstatus='" + mstatus + "',Rank='',Age='" + txtAge.Text.Trim() + "' where memberno='" + memberno + "'";
                        new WARTECHCONNECTION.cConnect().WriteDB(savedata);

                        string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditTime,AuditID)values('members','Updated member memberno " + txtMemberNo.Text + "','" + System.DateTime.Today + "','0','" + System.DateTime.Now.ToString("hh:mm") + "','" + Session["mimi"].ToString() + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                        WARSOFT.WARMsgBox.Show("Membership details updated sucessfully");
                        LoadMembers();
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; chhhek.Dispose(); chhhek = null;
                imgPhoto.ImageUrl = "../memberphoto.aspx?id=" + txtMemberNo.Text;

                ClearTexts();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void cboSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void populateTexts()
        {
            try
            {
                WARTECHCONNECTION.cConnect cooon = new WARTECHCONNECTION.cConnect();
                string memebr = "Select MemberNo,StaffNo,Station,IDNo,Surname,OtherNames,Sex,DOB,Employer,Dept,Terms,PresentAddr,OfficeTelNo,HomeAddr,HomeTelNo,RegFee,InitShares,AsAtDate,MonthlyContr,ApplicDate,EffectDate,Signed,Accepted,Archived,Withdrawn,Province,District,Station,CompanyCode,PIN,Photo,ShareCap,BankCode,Bname,AuditID,AuditTime,E_DATE,posted,initsharesTransfered,Transferdate,LoanBalance,InterestBalance,FormFilled,EmailAddress,accno,memberwitrawaldate,Dormant,MemberDescription,email,TransactionNo,MobileNo,AgentId,PhoneNo,Entrance,status,mstatus,Age FROM MEMBERS WHERE MemberNo='" + txtMemberNo.Text + "'";
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

                        txtOtherNames.Text = dr["OtherNames"].ToString().Trim();
                        txtPhoneNo.Text = dr["MobileNo"].ToString().Trim();

                        txtPinNo.Text = dr["PIN"].ToString();
                        txtPostalAddress.Text = dr["PresentAddr"].ToString();
                        txtRegDate.Text = dr["AsAtDate"].ToString();

                        txtSurname.Text = dr["Surname"].ToString();
                        txtAge.Text = dr["Age"].ToString();
                        string mstatus = dr["mstatus"].ToString();
                        if (mstatus == "0")
                        {
                            cboMaritalStatus.Text = "Single";
                        }
                        if (mstatus == "1")
                        {
                            cboMaritalStatus.Text = "Married";
                        }
                        if (mstatus == "2")
                        {
                            cboMaritalStatus.Text = "Others";
                        }
                        else
                        {
                            cboMaritalStatus.Text = "";
                        }

                        string withdrawn = dr["Withdrawn"].ToString();
                      
                        if (withdrawn == "0")
                        {
                            cboStatus.Text = "Active";
                        }
                        if (withdrawn == "1")
                        {
                            cboStatus.Text = "Withdrawn";
                        }
                        if (withdrawn == "2")
                        {
                            cboStatus.Text = "Deceased";
                        }
                        if (withdrawn == "3")
                        {
                            cboStatus.Text = "Dormant";
                        }
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

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtfindsearch.Text == "A")
                {
                    txtAgentNames.Text = GridView2.SelectedRow.Cells[1].Text;
                    cboAgentId.SelectedValue = GridView2.SelectedRow.Cells[4].Text;
                    txtfindsearch.Text = "";
                }
                else if (txtfindsearch.Text == "B")
                {
                    ClearTexts();
                    txtIdNo.Text = GridView2.SelectedRow.Cells[1].Text;
                    string surnam = GridView2.SelectedRow.Cells[2].Text;
                   string[] surname2= surnam.Split(' ');
                    txtSurname.Text=surname2[0].ToString();
                    txtOtherNames.Text = GridView2.SelectedRow.Cells[2].Text;
                    txtPhoneNo.Text = GridView2.SelectedRow.Cells[3].Text;
                    txtPostalAddress.Text = GridView2.SelectedRow.Cells[4].Text;
                    txtEmail.Text = GridView2.SelectedRow.Cells[5].Text;
                    txtfindsearch.Text = "";
                    LoadMembers();
                }
                else
                {
                    ClearTexts();
                    txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
                    txtSurname.Text = GridView2.SelectedRow.Cells[3].Text;
                    txtOtherNames.Text = GridView2.SelectedRow.Cells[4].Text;

                    populateTexts();
                    txtfindsearch.Text = "";
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtIdNo_TextChanged(object sender, EventArgs e)
        {

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

        protected void btnBeneficiary_Click1(object sender, EventArgs e)
        {

        }

        protected void btnIncomeDetails_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtfindsearch.Text == "A")
                {
                    if (cboSearch.Text == "Names")
                    {
                        string datatable = "select Names,Gender,StaffCode [Staff Code],IdNo [Id Number],Occupation,MobileNo,Branchname Station,HomeAddress [Home Address],Town from Agents where Names like '%" + txtSearch.Text + "%'";
                        da = new WARTECHCONNECTION.cConnect().ReadDB2(datatable);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                }
                else
                {
                    if (cboSearch.Text == "Member No")
                    {
                        da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT  MemberNo [Member No\\Staff No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth],Employer from members where memberno like'%" + txtSearch.Text + "%'");
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView2.Visible = true;
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                        ds.Dispose();
                        da.Dispose();
                    }
                    else if (cboSearch.Text == "Names")
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
                    else if (cboSearch.Text == "ID Number")
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
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnFindAgent_Click(object sender, EventArgs e)
        {
            txtfindsearch.Text = "A";
        }

        protected void btnFindGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboType.Text == "Grouped")
                {
                    txtfindsearch.Text = "B";

                    da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT CompanyCode [Group Code],CompanyName [Group Name],Telephone [Phone No.],Address,Email FROM company");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (txtfindsearch.Text != "B" && txtfindsearch.Text != "A")
                {
                    GridView2.PageIndex = e.NewPageIndex;
                    GridView2.DataBind();
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy SELECT MemberNo [Member No],IDNo [ID Number],Surname,OtherNames [Other Names],Sex [Gender],DOB [Date Of Birth] from members order by memberno desc");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else if (txtfindsearch.Text == "A")
                {
                    GridView2.PageIndex = e.NewPageIndex;
                    GridView2.DataBind();
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy select Names,Gender,StaffCode [Staff Code],IdNo [Id Number],Occupation,MobileNo,Branchname Station,HomeAddress [Home Address],Town from Agents where Names like '%" + txtSearch.Text + "%'");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
                else if (txtfindsearch.Text == "B")
                {
                    GridView2.PageIndex = e.NewPageIndex;
                    GridView2.DataBind();
                    da = new WARTECHCONNECTION.cConnect().ReadDB2("set dateformat dmy SELECT CompanyCode [Group Code],CompanyName [Group Name],Telephone [Phone No.],Address,Email FROM company");
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView2.Visible = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    ds.Dispose();
                    da.Dispose();
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}