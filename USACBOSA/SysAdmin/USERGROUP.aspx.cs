using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Easyma.SetUp
{
    public partial class USERGROUP : System.Web.UI.Page
    {
        public static int select;
        System.Data.SqlClient.SqlDataReader DR;
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
                Loadgrid();
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/easymain.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/easymain.aspx");
        }

        private void Loadgrid()
        {
            da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT id,menu,alias,enabled,regdate from tbl_menus");
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
            //throw new NotImplementedException();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Double intper = 0;
            Double N1 = 0;
            Double N2 = 0;
            String postdataurl = "";
            Double N3 = 0;
            Double adbal = 0;
            Double appamount = 0;
            double Amount = 0;
            Double totaldr = 0;
            Double totalcr = 0;
            double Amount1 = 0;
            Double avgamt = 0;
            string intgl = "";
            string ATYPE = "";
            string cheeck = "";
            string netpay = "";
            try
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
                    if (GridView1.Rows.Count > 0)
                    {
                        //chkRow.Checked = true;
                        if (chkRow.Checked == true)
                        {

                            string id = GridView1.Rows[i].Cells[1].Text;
                            string menu = GridView1.Rows[i].Cells[2].Text;
                            string alias = GridView1.Rows[i].Cells[3].Text;
                            string enabled = GridView1.Rows[i].Cells[4].Text;
                            string regdate = GridView1.Rows[i].Cells[5].Text;

                            string checkk = "set dateformat dmy select * from tbl_roles where id_menu='" + id + "'";
                            DR = new WARTECHCONNECTION.cConnect().ReadDB(checkk);
                            if (DR.HasRows)
                            {
                                //String inDB = "set dateformat dmy update d_payrollcopy set status3=1,user3='" + Session["mimi"].ToString() + "' where id='" + id + "'";
                                //new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                            }
                            else
                            {

                                string inDB = "set datefromat dmy INSERT INTO tbl_Roles (Groupid,groupname,menu,alias,enabled,regdate,id_menu) values ('" + TextBox2.Text + "','" + TextBox1.Text + "','" + menu + "','" + alias + "','" + enabled + "','" + regdate + "','" + id + "')";
                                new WARTECHCONNECTION.cConnect().WriteDB(inDB);
                                WARSOFT.WARMsgBox.Show("Data saved");
                                select++;
                            }
                            DR.Dispose(); DR.Close(); DR = null;
                        }
                        else
                        {
                            if (GridView1.Rows.Count > 0)
                            {
                            }
                            else
                            {
                                WARSOFT.WARMsgBox.Show("Please check one checkbox records");
                                return;
                            }
                        }
                    }
                    else
                    {
                        WARSOFT.WARMsgBox.Show("No records");
                        return;
                    }
                }
                //Thread.Sleep(5000);
                //GridView1.Columns.Clear();
                WARSOFT.WARMsgBox.Show("Record Updated successfully.");
                Loadgrid();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
    }
}