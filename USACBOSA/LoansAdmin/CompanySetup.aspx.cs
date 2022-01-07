using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.LoansAdmin
{
    public partial class CompanySetup : System.Web.UI.Page
    {

        System.Data.SqlClient.SqlDataReader dr, dr1, dr2;
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
                Loadgridview();
                generateBusinessNo();
                Loadsubcounty();
                LoadWard();
                Loadvillage();
                LoadBrances();
            }
        }
        //private void loadgl()
        //{
        //    try
        //    { 
        //        DropDownList1.Items.Add("");
        //        DropDownList1.Items.Clear();
        //        dr = new WARTECHCONNECTION.cConnect().ReadDB("select accno from glsetup order by accno");
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                DropDownList1.Items.Add(dr["accno"].ToString());
        //            }
        //        }
        //        dr.Close(); dr.Dispose(); dr = null;
        //    }
        //    catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        //}

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DropDownList1.Text == "")
        //        {
        //            WARSOFT.WARMsgBox.Show("Enter AccountNo!");
        //            return;
        //        }
        //        else
        //        {
        //            dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select Glaccname from GLSETUP where accno='" + DropDownList1.Text + "'");
        //            if (dr2.HasRows)
        //            {
        //                while (dr2.Read())
        //                {
        //                    TextBox4.Text = dr2["Glaccname"].ToString();
        //                }
        //            }
        //            dr2.Close(); dr2.Dispose(); dr2 = null;
        //        }
        //    }
        //    catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }

        //}
        private void generateBusinessNo()
        {
            try
            {
                string ssessionuser = "";
                string myddd444 = "select userid from useraccounts1  where userloginid='" + Session["mimi"].ToString() + "'";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(myddd444);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ssessionuser = dr["userid"].ToString();
                    }
                dr.Close(); dr.Dispose(); dr = null;
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                string myddd = "select isnull(MAX(RIGHT(companycode,3)),0)+1 ccount from company  where companycode like '%CIG%' and year(audittime)='" + (System.DateTime.Today).Year + "' and month(audittime)='" + (System.DateTime.Today).Month + "' and day(audittime)= '" + (System.DateTime.Today).Day + "'";
                dr = oSaccoMaster.ReadDB(myddd);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        int maxno = Convert.ToInt32(dr[0].ToString());
                        int nextno = maxno;
                        if (ssessionuser == "1" || ssessionuser == "2" || ssessionuser == "3" || ssessionuser == "4" || ssessionuser == "5" || ssessionuser == "6" || ssessionuser == "7" || ssessionuser == "8" || ssessionuser == "9")
                        {
                            ssessionuser = 0 + ssessionuser;
                        }
                        txtCompanyCode.Text = "CIG" + ssessionuser + "-" + ((nextno).ToString()).PadLeft(3, '0');
                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
                //string MMMMMM1 = "";
                //int memmebno1 = 0;
                //string suffix1 = "";
                //string jmember = "select max(SUBSTRING(companycode,7,2)) as companycode from Company";
                //dr1 = new WARTECHCONNECTION.cConnect().ReadDB(jmember);
                //if (dr1.HasRows)
                //{
                //    while (dr1.Read())
                //    {
                //        MMMMMM1 = dr1["companycode"].ToString();
                //        if (MMMMMM1 == "")
                //        {
                //            MMMMMM1 = "000";
                //            memmebno1 = Convert.ToInt32(MMMMMM1);
                //            int Intsuffix1 = memmebno1 + 1;
                //            suffix1 = Convert.ToString(Intsuffix1);
                //        }
                //        else
                //        {
                //            memmebno1 = Convert.ToInt32(MMMMMM1);
                //            int Intsuffix1 = memmebno1 + 1;
                //            suffix1 = Convert.ToString(Intsuffix1);
                //        }
                //        suffix1 = suffix1.PadLeft(5, '0');
                //        txtCompanyCode.Text = "CIG-000" + suffix1.ToString();
                //        break;
                //    }
                //}
                //dr1.Close(); dr1.Dispose(); dr1 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loadgridview()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT CompanyCode,CompanyName,Telephone,Address,county,subcounty,ward,village FROM company");
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
                this.txtCompanyCode.Text = GridView.SelectedRow.Cells[1].Text;
                this.txtCompanyName.Text = GridView.SelectedRow.Cells[2].Text;
                this.txtTelephone.Text = GridView.SelectedRow.Cells[3].Text;
                this.txtAddress.Text = GridView.SelectedRow.Cells[4].Text;
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyCode,CompanyName,Email,Contactperson,Telephone,Address,AccountNo,NoYears,NoEmployees,Location,type,AuditID,county,subcounty,ward,village from company where CompanyCode='" + txtCompanyCode.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList2.Text = dr["county"].ToString().Trim();
                        DropDownList3.Text = dr["ward"].ToString().Trim();
                        DropDownList4.Text = dr["village"].ToString().Trim();
                        DropDownList5.Text = dr["subcounty"].ToString().Trim();
                        txtNoUsers.Text = dr["NoEmployees"].ToString().Trim();
                        //txtNoYears.Text = dr["NoYears"].ToString().Trim();
                        TextBox3.Text = dr["Contactperson"].ToString().Trim();
                        txtEmailAddress.Text = dr["Email"].ToString().Trim();
                        string bstatus = dr["type"].ToString().Trim();
                        cboBusinesstatus.SelectedValue = bstatus;
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Group code is required");
                    txtCompanyCode.Focus();
                    return;
                }
                if (txtCompanyName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Group Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                else
                {
                    dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from company where CompanyCode='" + txtCompanyCode.Text.Trim() + "'");
                    if (dr.HasRows)
                    {
                        WARSOFT.WARMsgBox.Show("The group with same code already exist.");
                        return;
                    }
                    String type = cboBusinesstatus.Text;
                    if (type == "")
                    {
                        WARSOFT.WARMsgBox.Show("Please select the business status");
                        cboBusinesstatus.Focus();
                        return;
                    }
                    string indatadb = "set dateformat dmy insert into company(CompanyCode,county,subcounty,ward,village,CompanyName,Email,Contactperson,type,Telephone,Address,AuditID)values('" + txtCompanyCode.Text + "','" + DropDownList2.Text + "','" + DropDownList5.Text + "','" + DropDownList3.Text + "','" + DropDownList4.Text + "','" + txtCompanyName.Text + "','" + txtEmailAddress.Text + "','" + TextBox3.Text + "','" + type + "','" + txtTelephone.Text + "','" + txtAddress.Text + "','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(indatadb);
                    cleartxts();
                    Loadgridview();
                    generateBusinessNo();
                    WARSOFT.WARMsgBox.Show("Details saved sucessfully");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadBrances()
        {
            try
            {
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches order by branchname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList2.Items.Add(dr["branchname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Loadsubcounty()
        {
            try
            {
                DropDownList5.Items.Clear();
                DropDownList5.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select name from subcounty order by name");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList5.Items.Add(dr["name"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void LoadWard()
        {
            try
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select name from ward order by name");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList3.Items.Add(dr["name"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void Loadvillage()
        {
            try
            {
                DropDownList4.Items.Clear();
                DropDownList4.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select name from village order by name");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList4.Items.Add(dr["name"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private void cleartxts()
        {
            try
            {
                txtCompanyCode.Text = "";
                txtCompanyName.Text = "";
                txtNoUsers.Text = "";
                //txtNoYears.Text = "";
                txtEmailAddress.Text = "";
                TextBox3.Text = "";
                txtTelephone.Text = "";
                //TextBox1.Text = "";
                txtAddress.Text = "";
                DropDownList2.Text = "";
                DropDownList3.Text = "";
                DropDownList4.Text = "";
                DropDownList5.Text = "";
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cboBusinesstatus.Text;

                if (type == "")
                {
                    WARSOFT.WARMsgBox.Show("Please select the business status");
                    cboBusinesstatus.Focus();
                    return;
                }
                if (txtCompanyCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Group code is required");
                    txtCompanyCode.Focus();
                    return;
                }
                if (txtCompanyName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Group Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                else
                {
                    string update = "set dateformat dmy update company set CompanyName='" + txtCompanyName.Text + "',NoEmployees='" + txtNoUsers.Text + "',county='" + DropDownList2.Text + "',subcounty='" + DropDownList5.Text + "',ward='" + DropDownList3.Text + "',village='" + DropDownList4.Text + "',Email='" + txtEmailAddress.Text + "',Contactperson='" + TextBox3.Text + "',type='" + type + "',Telephone='" + txtTelephone.Text + "',Address='" + txtAddress.Text + "',AuditID='" + Session["mimi"].ToString() + "' where CompanyCode='" + txtCompanyCode.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(update);
                    cleartxts();
                    Loadgridview();
                    generateBusinessNo();
                    WARSOFT.WARMsgBox.Show("Updated Successfully");
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Group code is required");
                    txtCompanyCode.Focus();
                    return;
                }
                else
                {
                    string del = "set dateformat ymd delete from company Where CompanyCode='" + txtCompanyCode.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(del);
                    cleartxts();
                    Loadgridview();
                    WARSOFT.WARMsgBox.Show("Deleted Successfully");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnAddMembers_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyCode.Text != "")
                {
                    Session["bcode"] = txtCompanyCode.Text;
                    Session["bname"] = txtCompanyName.Text;
                    Response.Redirect("~/SysAdmin/GroupMembers.aspx", false);
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        private static string Left(string strFld13, int p)
        {
            string S = strFld13.Substring(0, 8);
            //return the result of the operation
            return S;
        }

        private static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        private static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        private static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

    }
}

