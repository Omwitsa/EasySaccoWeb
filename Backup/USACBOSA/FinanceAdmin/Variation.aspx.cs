using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.FinanceAdmin
{
    public partial class Variation : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3;
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
                LoadItems();
            }
        }

        private void LoadItems()
        {
            try
            {
                //dr= new WARTECHCONNECTION.cConnect().ReadDB("select m.memberno,m.surname,m.othernames,m.HomeAddr,m.companycode,c.companyname,m.applicdate  from members m inner join company c on m.companycode=c.companycode order by m.companycode,m.memberno")
                //Set rsVar = oSaccoMaster.GetRecordSet("select * from shrvar order by memberno,sharescode");
                da = new WARTECHCONNECTION.cConnect().ReadDB2("select st.sharescode [Share Code],st.sharestype [Share Type],st.minAmount [Default Value] from sharetype st order by priority");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ds.Dispose();
                da.Dispose();
                dtpShareVarDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member number is required");
                    txtMemberNo.Focus();
                    return;
                }
                txtShareCode.Text = "";
                txtShareType.Text = "";
                txtDefAmount.Text = "0.00";
                txtDefAmount.Text = GridView1.SelectedRow.Cells[4].Text;
                txtShareCode.Text = GridView1.SelectedRow.Cells[2].Text;
                txtShareType.Text = GridView1.SelectedRow.Cells[3].Text;
                getvariationdetails(txtMemberNo.Text);
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void cleartexts()
        {
            txtSubscribedAmount.Text = "0.00";
            txtRegDate.Text = DateTime.Today.ToString();
            txtDefAmount.Text = "0.00";
            txtCompany.Text = "";
        }

        private void getvariationdetails(string memberno)
        {
            txtSubscribedAmount.Text = "0.00";
            txtRegDate.Text = DateTime.Today.ToString();
            dtpShareVarDate.Text = DateTime.Today.ToString(); 
            dr = new WARTECHCONNECTION.cConnect().ReadDB("select OldContr,NewContr,VarDate,sharestype,SharesCode,Subscribed,M.ApplicDate from shrvar sv inner join members M ON M.memberNo=sv.memberNo where sv.memberno='" + memberno + "'");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    bool subsc = Convert.ToBoolean(dr["Subscribed"]);
                    txtRegDate.Text = dr["ApplicDate"].ToString();
                    dtpShareVarDate.Text = dr["vardate"].ToString();
                    if (subsc == true)
                    {
                        txtSubscribedAmount.Text = dr["NewContr"].ToString();
                    }
                    else
                    {
                        txtSubscribedAmount.Text = "0.00";
                    }
                }
            }
            else
            {
                txtSubscribedAmount.Text = "0.00";
                dtpShareVarDate.Text = DateTime.Today.ToString();
            }
            dr.Close(); dr.Dispose(); dr = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("Member number is required");
                    txtMemberNo.Focus();
                    return;
                }
                if (txtShareCode.Text.Trim() == "")
                {
                    WARSOFT.WARMsgBox.Show("Member Share Code is required");
                    txtShareCode.Focus();
                    return;
                }
                if (txtSubscribedAmount.Text == "")
                {
                    txtSubscribedAmount.Text ="0.00";
                }
                string subscribed = "";
                if (chkSubscribe.Checked==true)
                {
                    subscribed = "1";
                }
                else if (chkSubscribe.Checked==false)
                {
                    subscribed = "0";
                }
                    dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select * from shrvar where memberno='" + txtMemberNo.Text.Trim() + "' and sharescode='" + txtShareCode.Text.Trim() + "'");
                    if (dr1.HasRows)
                    {
                        new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy update shrvar set OldContr='" + txtDefAmount.Text.Trim() + "',NewContr='" + txtSubscribedAmount.Text.Trim() + "',VarDate='" + dtpShareVarDate.Text + "',AuditID='" + Session["mimi"].ToString().Trim() + "',AuditTime='" + System.DateTime.Now + "',sharestype='" + txtShareType.Text.Trim() + "',SharesCode='" + txtShareCode.Text.Trim() + "',Subscribed='" + subscribed + "' where memberno='" + txtMemberNo.Text.Trim() + "' and sharescode='" + txtShareCode.Text.Trim() + "'");
                    }
                    else
                    {
                        new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy insert into shrvar(MemberNo,OldContr,NewContr,VarDate,AuditID,AuditTime,sharestype,SharesCode,Subscribed)values('" + txtMemberNo.Text.Trim() + "','" + txtDefAmount.Text.Trim() + "','" + txtSubscribedAmount.Text.Trim() + "','" + dtpShareVarDate.Text + "','" + Session["mimi"].ToString().Trim() + "','" + System.DateTime.Now + "','" + txtShareType.Text.Trim() + "','" + txtShareCode.Text.Trim() + "','" + subscribed + "')");
                    }
                    dr1.Close(); dr1.Dispose(); dr1 = null;
                WARSOFT.WARMsgBox.Show("Member "+txtMemberNo.Text+"  subscription details posted sucessfully");
                return;
                
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            getvariationdetails(txtMemberNo.Text.Trim());
            subscbymember(txtMemberNo.Text.Trim());
        }

        private void subscbymember(string memberno)
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("select st.sharescode [Share Code],st.sharestype [Share Type],st.minAmount [Default Value],sv.subscribed from sharetype st inner join shrvar sv ON sv.sharescode=st.sharescode where MemberNo='" + memberno + "' order by priority");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
            dtpShareVarDate.Text = DateTime.Today.ToString();
        }
    }
}