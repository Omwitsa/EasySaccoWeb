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
                Loadgridview();
                generateBusinessNo();
            }
        }

        private void generateBusinessNo()
        {
            try
            {
                string MMMMMM1 = "";
                int memmebno1 = 0;
                string suffix1 = "";
                string jmember = "select max(SUBSTRING(companycode,2,10)) as companycode from Company";
                dr1 = new WARTECHCONNECTION.cConnect().ReadDB(jmember);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        MMMMMM1 = dr1["companycode"].ToString();
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
                        txtCompanyCode.Text ="G"+ suffix1.ToString();
                        break;
                    }
                }
                dr1.Close(); dr1.Dispose(); dr1 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Loadgridview()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT CompanyCode,CompanyName,Telephone,Address,AccountNo FROM company");
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
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select CompanyCode,CompanyName,Email,Contactperson,Telephone,Address,AccountNo,NoYears,NoEmployees,Location,type,AuditID,AuditTime from company where CompanyCode='" + txtCompanyCode.Text.Trim() + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox1.Text = dr["Location"].ToString().Trim();
                        txtNoUsers.Text = dr["NoEmployees"].ToString().Trim();
                        txtNoYears.Text = dr["NoYears"].ToString().Trim();
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
                    WARSOFT.WARMsgBox.Show("Company/Business code is required");
                    txtCompanyCode.Focus();
                    return;
                }
                if (txtCompanyName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Business Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                else
                {
                    dr = new WARTECHCONNECTION.cConnect().ReadDB("select * from company where CompanyCode='"+txtCompanyCode.Text.Trim()+"'");
                    if (dr.HasRows)
                    {
                        WARSOFT.WARMsgBox.Show("The group with same code already exist.");
                        return;
                    }
                    String type = cboBusinesstatus.Text;
                   if(type=="")
                    {
                        WARSOFT.WARMsgBox.Show("Please select the business status");
                        cboBusinesstatus.Focus();
                        return;
                    }
                   string indatadb = "set dateformat dmy insert into company(CompanyCode,CompanyName,Email,Contactperson,type,NoYears,Location,Telephone,Address,AuditID)values('" + txtCompanyCode.Text + "','" + txtCompanyName.Text + "','" + txtEmailAddress.Text + "','" + TextBox3.Text + "','" + type + "','" + txtNoYears.Text + "','" + TextBox1.Text + "','" + txtTelephone.Text + "','" + txtAddress.Text + "','" + Session["mimi"].ToString() + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(indatadb);
                    cleartxts();
                    Loadgridview();
                    WARSOFT.WARMsgBox.Show("Details saved sucessfully");
                    return;
                }
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
                txtNoYears.Text = "";
                txtEmailAddress.Text = "";
                TextBox3.Text = "";
                txtTelephone.Text = "";
                TextBox1.Text = "";
                txtAddress.Text = "";
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cboBusinesstatus.Text;
              
                if(type=="")
                {
                    WARSOFT.WARMsgBox.Show("Please select the business status");
                    cboBusinesstatus.Focus();
                    return;
                }
                if (txtCompanyCode.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Company/Business code is required");
                    txtCompanyCode.Focus();
                    return;
                }
                if (txtCompanyName.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Company/Business Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                else
                {
                    string update = "set dateformat dmy update company set CompanyName='" + txtCompanyName.Text + "',NoEmployees='" + txtNoUsers.Text + "',NoYears='" + txtNoYears.Text + "',Email='" + txtEmailAddress.Text + "',Contactperson='" + TextBox3.Text + "',type='" + type + "',Telephone='" + txtTelephone.Text + "',Location='" + TextBox1.Text + "',Address='" + txtAddress.Text + "',AuditID='" + Session["mimi"].ToString() + "'where CompanyCode='" + txtCompanyCode.Text + "'";
                    new WARTECHCONNECTION.cConnect().WriteDB(update);
                    cleartxts();
                    Loadgridview();
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
                    WARSOFT.WARMsgBox.Show("Company/Business code is required");
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
    }
}

  