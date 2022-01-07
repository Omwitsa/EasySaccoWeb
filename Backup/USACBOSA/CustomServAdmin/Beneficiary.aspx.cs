using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.CustomServAdmin
{
    public partial class Beneficiary : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1;
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

                LoadKin();
                txtSignDate.Text = Convert.ToString(System.DateTime.Today.ToString("dd-MM-yyyy"));
            }
            //txtMemberNo.Text = Session["memberno"].ToString().Trim();
        }

        private void LoadKin()
        {
            try
            {
                string sehhh = "select MemberNo,KinNo,KinNames,Address,Relationship from kin where MemberNo ='" + txtMemberNo.Text.Trim() + "'order by MemberNo";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
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

        protected void GridView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                txtMemberNo.Text = GridView1.SelectedRow.Cells[1].Text;
                txtKinNo.Text = GridView1.SelectedRow.Cells[2].Text;
                txtKinNames.Text = GridView1.SelectedRow.Cells[3].Text;
                txtAddress.Text = GridView1.SelectedRow.Cells[4].Text;
                cboRelationship.SelectedValue = GridView1.SelectedRow.Cells[4].Text;
                getKinDetails(txtKinNo.Text);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void getKinDetails(string kinno)
        {
            dr = new WARTECHCONNECTION.cConnect().ReadDB("Select K.MemberNo,K.KinNames,K.KinNo,K.Address,K.IDNo,K.Relationship,K.HomeTelNo,K.Witness,K.Percentage,K.OfficeTelNo,K.SignDate,K.KinSigned,K.AuditID,K.AuditTime,K.Comments,M.SURNAME + M.OTHERNAMES AS NAMES from kin K INNER JOIN MEMBERS M ON K.MEMBERNO=M.MEMBERNO where KinNo='" + kinno + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtAddress.Text = dr["Address"].ToString().Trim();
                    txtComments.Text = dr["Comments"].ToString().Trim();
                    txtHomeTel.Text = dr["HomeTelNo"].ToString().Trim();
                    txtIdNumber.Text = dr["IDNo"].ToString().Trim();
                    txtKinNames.Text = dr["KinNames"].ToString().Trim();
                    txtKinNo.Text = dr["KinNo"].ToString().Trim();
                    txtMemberNo.Text = dr["MemberNo"].ToString().Trim();
                    txtNames.Text = dr["NAMES"].ToString().Trim();
                    txtOfficeTel.Text = dr["OfficeTelNo"].ToString().Trim();
                    txtPercentage.Text = dr["Percentage"].ToString().Trim();
                    txtSignDate.Text = dr["SignDate"].ToString().Trim();
                    txtWitness.Text = dr["Witness"].ToString().Trim();
                    cboRelationship.SelectedValue = dr["Relationship"].ToString();
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Loadgridview();
        }

        private void Loadgridview()
        {
            try
            {
                string sehhh = "select MemberNo,KinNo,KinNames,Address,Relationship,Percentage from kin where MemberNo ='" + txtMemberNo.Text + "'";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
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
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Loadgridview();
            LoadNames();
        }

        private void LoadNames()
        {
            try
            {
                WARTECHCONNECTION.cConnect oNames = new WARTECHCONNECTION.cConnect();
                dr = oNames.ReadDB("select surname,othernames from MEMBERS where memberno='" + txtMemberNo.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string naming = dr["surname"].ToString();
                        string oname = dr["othernames"].ToString();
                        txtNames.Text = naming + ' ' + oname;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null; oNames.Dispose(); oNames = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Number is required before adding the next of Kin");
                    txtMemberNo.Focus();
                    return;
                }
                if (txtKinNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter kin number");
                    txtKinNo.Focus();
                    return;
                }
                if (txtKinNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter kin names");
                    txtKinNames.Focus();
                    return;
                }

                if (cboRelationship.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Select relationship");
                    cboRelationship.Focus();
                    return;
                }
                if (txtAddress.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter address");
                    txtAddress.Focus();
                    return;
                }
                if (txtPercentage.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter Percentage, Disregard This?");
                    txtPercentage.Focus();
                    return;
                }
                if (txtSignDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter signdate");
                    txtSignDate.Focus();
                    return;
                }
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from kin where kinno='" + txtKinNo + "'");
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("" + txtKinNo + " already exist");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;

                string memberno = txtMemberNo.Text;
                string kinno = txtKinNo.Text;
                string KinNames = txtKinNames.Text;
                string Relationship = cboRelationship.Text;
                string address = txtAddress.Text;
                string auditid = Session["mimi"].ToString();
                string audittime = DateTime.Now.ToString();
                string idno = txtIdNumber.Text;
                string HomeTelNo = txtHomeTel.Text;
                string OfficeTelNo = txtOfficeTel.Text;
                string SignDate = txtSignDate.Text;
                if (txtSignDate.Text == "")
                {
                    SignDate = txtSignDate.Text;
                }
                string Witness = txtWitness.Text;
                string Percentage = "";
                if (txtPercentage.Text == "")
                {
                    Percentage = "0";
                }
                else
                {
                    Percentage = txtPercentage.Text;
                }
                string kinsigned = "yes";
                string Comments = txtComments.Text;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from kin where kinno='" + txtKinNo.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    WARSOFT.WARMsgBox.Show("The Kin number " + txtKinNo.Text.Trim() + " already exist");
                    return;
                }
                dr.Close(); dr.Dispose(); dr = null;
                Double SumOfPercentage = 0; Double SumOfPercentage1 = 0;
                WARTECHCONNECTION.cConnect MemExist = new WARTECHCONNECTION.cConnect();
                dr1 = MemExist.ReadDB("SELECT * from KIN Where MemberNo='" + txtMemberNo.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        WARTECHCONNECTION.cConnect GetKin = new WARTECHCONNECTION.cConnect();
                        string KinPercent = "SELECT Sum(KIN.Percentage) AS SumOfPercentage FROM KIN WHERE KIN.MEMBERNO= '" + txtMemberNo.Text + "'";
                        dr = GetKin.ReadDB(KinPercent);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string SumPerrcs = dr["SumOfPercentage"].ToString();
                                SumOfPercentage1 = Convert.ToDouble(dr["SumOfPercentage"]);//.ToString());
                                SumOfPercentage = Math.Round(SumOfPercentage1, 1);
                            }
                        }
                        dr.Close(); dr.Dispose(); dr = null; GetKin.Dispose(); GetKin = null;
                        SumOfPercentage = SumOfPercentage + Convert.ToInt32(txtPercentage.Text.Trim());
                        if (SumOfPercentage > 100)
                        {
                            WARSOFT.WARMsgBox.Show("Adjust percentages of kin to accomodate new kin,the percentage sum should not exceed 100.");
                            return;
                        }
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; MemExist.Dispose(); MemExist = null;
                string todata = "set dateformat dmy insert into kin(MemberNo,KinNames,KinNo,Address,IDNo,Relationship,HomeTelNo,Witness,Percentage,OfficeTelNo,SignDate,KinSigned,AuditID,Comments)values('" + memberno + "','" + KinNames + "','" + kinno + "','" + address + "','" + idno + "','" + Relationship + "','" + HomeTelNo + "','" + Witness + "','" + Percentage + "','" + OfficeTelNo + "','" + SignDate + "','" + kinsigned + "','" + auditid + "','" + Comments + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(todata);

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('kin','Added kin kinno " + kinno + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                Loadgridview();
                WARSOFT.WARMsgBox.Show("" + KinNames + "kin details saved sucessfully");
                clearTexts();
                txtMemberNo.Focus();
                // return;
                // Load_Records();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        private void clearTexts()
        {
            txtHomeTel.Text = "";
            txtOfficeTel.Text = "";
            txtSignDate.Text = "";
            txtIdNumber.Text = "";
            txtPercentage.Text = "";
            txtWitness.Text = "";
            txtComments.Text = "";
            txtAddress.Text = "";
            txtKinNames.Text = "";
            txtKinNo.Text = "";
            cboRelationship.SelectedValue = "";
            txtMemberNo.Text = "";
            txtNames.Text = "";
        }

        protected void btnAddKin_Click(object sender, EventArgs e)
        {
            try
            {
                cboRelationship.Visible = true;
                imgSignDate.Visible = true;
                // MultiView1.ActiveViewIndex = 1;
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Number is required before adding the next of Kin");
                    return;
                }
                int SumOfPercentage = 0;
                WARTECHCONNECTION.cConnect MemExist = new WARTECHCONNECTION.cConnect();
                dr1 = MemExist.ReadDB("SELECT * from KIN Where MemberNo='" + txtMemberNo.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        WARTECHCONNECTION.cConnect GetKin = new WARTECHCONNECTION.cConnect();
                        string KinPercent = "SELECT Sum(KIN.Percentage) AS SumOfPercentage FROM KIN WHERE KIN.MEMBERNO= '" + txtMemberNo.Text + "'";
                        dr = GetKin.ReadDB(KinPercent);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string SumPerrcs = dr["SumOfPercentage"].ToString();
                                SumOfPercentage = Convert.ToInt32(dr["SumOfPercentage"].ToString());
                            }
                        }
                        dr.Close(); dr.Dispose(); dr = null; GetKin.Dispose(); GetKin = null;

                        if (SumOfPercentage >= 100)
                        {
                            WARSOFT.WARMsgBox.Show("Adjust percentages of other kin to accomodate new kin ,100% ,already allocated");
                            return;
                        }
                        else
                        {
                            dr = new WARTECHCONNECTION.cConnect().ReadDB("Select count(*)+1 as countt from kin where memberno='" + txtMemberNo.Text.Trim() + "'");
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    int str1 = 0;
                                    string str2 = "";

                                    str1 = Convert.ToInt32(dr["countt"].ToString());
                                    str2 = String.Format("{0:00}", str1);
                                    //str1.ToString().PadLeft(2,'0');
                                    txtKinNo.Text = "K" + txtMemberNo.Text + "-" + str1;
                                }
                            }
                            dr.Close(); dr.Dispose(); dr = null;
                        }
                    }
                }
                else
                {
                    dr = new WARTECHCONNECTION.cConnect().ReadDB("Select count(*)+1 as countt from kin where memberno='" + txtMemberNo.Text.Trim() + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int str1 = 0;
                            string str2 = "";

                            str1 = Convert.ToInt32(dr["countt"].ToString());
                            str2 = String.Format("{0:00}", str1);
                            //str1.ToString().PadLeft(2,'0');
                            txtKinNo.Text = "K" + txtMemberNo.Text + "-" + str1;
                        }
                    }
                    dr.Close(); dr.Dispose(); dr = null;
                }
                dr1.Close(); dr1.Dispose(); dr1 = null; MemExist.Dispose(); MemExist = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void imgSearchMember_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //LoadMembers();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadMembers()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("Exec Get_All_Members_Like '%" + txtMemberNo.Text + "%'");
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Number is required before adding the next of Kin");
                    return;
                }
                if (txtKinNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter kin number");
                    txtKinNo.Focus();
                }
                if (txtKinNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter kin names");
                    txtKinNames.Focus();
                    return;
                }

                if (cboRelationship.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Select relationship");
                    cboRelationship.Focus();
                }
                if (txtAddress.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter address");
                    txtAddress.Focus();
                }
                if (txtPercentage.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter Percentage, Disregard This?");
                    txtPercentage.Focus();
                }
                if (txtSignDate.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Enter signdate");
                    txtSignDate.Focus();
                }
                //dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from kin where kinno='" + txtKinNo + "'");
                //if (dr.HasRows)
                //{
                //    WARSOFT.WARMsgBox.Show("" + txtKinNo + " already exist");
                //    return;
                //}
                //dr.Close(); dr.Dispose(); dr = null;

                string memberno = txtMemberNo.Text;
                string kinno = txtKinNo.Text;
                string KinNames = txtKinNames.Text;
                string Relationship = cboRelationship.Text;
                string address = txtAddress.Text;
                string auditid = Session["mimi"].ToString();
                string audittime = DateTime.Now.ToString();
                string idno = txtIdNumber.Text;
                string HomeTelNo = txtHomeTel.Text;
                string OfficeTelNo = txtOfficeTel.Text;
                string SignDate = txtSignDate.Text;
                if (txtSignDate.Text == "")
                {
                    SignDate = System.DateTime.Today.ToString("yyyy-MM-dd");
                }
                string Witness = txtWitness.Text;
                string Percentage = "";
                if (txtPercentage.Text == "")
                {
                    Percentage = "0";
                }
                else
                {
                    Percentage = txtPercentage.Text;
                }
                string kinsigned = "yes";
                string Comments = txtComments.Text;
                int SumOfPercentage = 0;
                WARTECHCONNECTION.cConnect MemExist = new WARTECHCONNECTION.cConnect();
                dr1 = MemExist.ReadDB("SELECT * from KIN Where MemberNo='" + txtMemberNo.Text + "'");
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        WARTECHCONNECTION.cConnect GetKin = new WARTECHCONNECTION.cConnect();
                        string KinPercent = "SELECT Sum(KIN.Percentage) AS SumOfPercentage FROM KIN WHERE KIN.MEMBERNO= '" + txtMemberNo.Text + "'";
                        dr = GetKin.ReadDB(KinPercent);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string SumPerrcs = dr["SumOfPercentage"].ToString();
                                SumOfPercentage = Convert.ToInt32(dr["SumOfPercentage"].ToString());
                            }
                        }
                        dr.Close(); dr.Dispose(); dr = null; GetKin.Dispose(); GetKin = null;
                        if (SumOfPercentage > 100)
                        {
                            WARSOFT.WARMsgBox.Show("Adjust percentages of kin to accomodate new kin,the percentage sum should not exceed 100.");
                            // return;
                        }
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
                string todata = "set dateformat dmy update kin set MemberNo='" + memberno + "',KinNames='" + KinNames + "',KinNo='" + kinno + "',Address='" + address + "',IDNo='" + idno + "',Relationship='" + Relationship + "',HomeTelNo='" + HomeTelNo + "',Witness='" + Witness + "',Percentage='" + Percentage + "',OfficeTelNo='" + OfficeTelNo + "',SignDate='" + SignDate + "',KinSigned='" + kinsigned + "',AuditID='" + auditid + "',AuditTime='" + audittime + "',Comments='" + Comments + "' WHERE KinNo='" + kinno + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(todata);

                string audittrans = "set dateformat dmy insert into AUDITTRANS(TransTable,TransDescription,TransDate,Amount,AuditID)values('kin','Updated kin kinno " + kinno + "','" + System.DateTime.Today + "','0','" + Session["mimi"].ToString() + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(audittrans);

                Loadgridview();
                WARSOFT.WARMsgBox.Show("" + KinNames + " kin details Updated sucessfully");
                clearTexts();
                // return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMemberNo.Text = GridView1.SelectedRow.Cells[1].Text;
            txtKinNo.Text = GridView1.SelectedRow.Cells[2].Text;
            txtKinNames.Text = GridView1.SelectedRow.Cells[3].Text;
            txtAddress.Text = GridView1.SelectedRow.Cells[4].Text;
            this.cboRelationship.SelectedValue = GridView1.SelectedRow.Cells[5].Text;
            dr = new WARTECHCONNECTION.cConnect().ReadDB("Select MemberNo,KinNames,KinNo,Address,IDNo,Relationship,HomeTelNo,Witness,Percentage,OfficeTelNo,SignDate,KinSigned,AuditID,AuditTime,Comments FROM KIN WHERE KinNo='" + txtKinNo.Text.Trim() + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtHomeTel.Text = dr["HomeTelNo"].ToString().Trim();
                    txtOfficeTel.Text = dr["OfficeTelNo"].ToString().Trim();
                    txtSignDate.Text = dr["SignDate"].ToString().Trim();
                    txtIdNumber.Text = dr["IDNo"].ToString().Trim();
                    txtPercentage.Text = dr["Percentage"].ToString().Trim();
                    txtWitness.Text = dr["Witness"].ToString().Trim();
                    txtComments.Text = dr["Comments"].ToString().Trim();
                }
            }
            dr.Close(); dr.Dispose(); dr = null;
        }
    }
}