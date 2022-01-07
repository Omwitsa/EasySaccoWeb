using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.CustomServAdmin
{
    public partial class GroupMembers : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCompanyCode.Text = Session["bcode"].ToString();
            txtCompanyName.Text = Session["bname"].ToString();
            LoadGroupMembers();
        }

        protected void btnSave_Click(object sender, EventArgs e)
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
                    WARSOFT.WARMsgBox.Show("Company/Business Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                if (txtnames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member Names");
                    txtnames.Focus();
                    return;
                }
                if (txtidno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member ID Number");
                    txtidno.Focus();
                    return;
                }
                if (txtmobileno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member Mobile Number");
                    txtmobileno.Focus();
                    return;
                }
                string insadat = "set dateformat dmy insert into GroupMembers(CompanyCode,CompanyName,MemberNo,MemberNames,IdNO,MobileNo,PostalAddress,EmailAddress)values('"+txtCompanyCode.Text+"','"+txtCompanyName.Text+"','"+txtmemberno.Text+"','"+txtnames.Text+"','"+txtidno.Text+"','"+txtmobileno.Text+"','"+txtAddress.Text+"','"+txtEmailAddress.Text+"')";
                new WARTECHCONNECTION.cConnect().WriteDB(insadat);
                LoadGroupMembers();
                clearTexts();
                WARSOFT.WARMsgBox.Show("Group Member Details saved sucessfully");
                return;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
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
                    WARSOFT.WARMsgBox.Show("Company/Business Name is required");
                    txtCompanyName.Focus();
                    return;
                }
                if (txtnames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member Names");
                    txtnames.Focus();
                    return;
                }
                if (txtidno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member ID Number");
                    txtidno.Focus();
                    return;
                }
                if (txtmobileno.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Please enter the Member Mobile Number");
                    txtmobileno.Focus();
                    return;
                }
                string insadat = "set dateformat dmy update GroupMembers set MemberNo='"+txtmemberno.Text.Trim()+"',MemberNames='"+txtnames.Text.Trim()+"',IdNO='"+txtidno.Text.Trim()+"',MobileNo='"+txtmobileno.Text.Trim()+"',PostalAddress='"+txtAddress.Text.Trim()+"',EmailAddress='"+txtEmailAddress.Text.Trim()+"'";
                new WARTECHCONNECTION.cConnect().WriteDB(insadat);
                LoadGroupMembers();
                clearTexts();
              
                WARSOFT.WARMsgBox.Show("Group Member Details updated sucessfully");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void clearTexts()
        {
            txtEmailAddress.Text = "";
            txtidno.Text = "";
            txtmemberno.Text = "";
            txtmobileno.Text = "";
            txtnames.Text = "";
            txtAddress.Text = "";
        }

        private void LoadGroupMembers()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT MemberNo [Member No],MemberNames [Member Names],IdNO [ID Number],MobileNo [Mobile No],PostalAddress [Postal Address],EmailAddress [Email Address] FROM GroupMembers where CompanyCode='" + txtCompanyCode.Text + "' order by id desc");
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnFindMember_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtmemberno.Text = GridView1.SelectedRow.Cells[1].Text;
                this.txtnames.Text = GridView1.SelectedRow.Cells[2].Text;
                this.txtidno.Text = GridView1.SelectedRow.Cells[3].Text;
                this.txtmobileno.Text = GridView1.SelectedRow.Cells[4].Text;
                this.txtAddress.Text = GridView1.SelectedRow.Cells[5].Text;
                this.txtEmailAddress.Text = GridView1.SelectedRow.Cells[6].Text;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}