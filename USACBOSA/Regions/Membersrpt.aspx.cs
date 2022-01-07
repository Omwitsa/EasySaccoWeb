using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace USACBOSA.Regions
{
    public partial class Membersrpt : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadregions();
                //if (ScriptManager.GetCurrent(Page) == null)
                //{
                //    Page.Form.Controls.AddAt(0, new ScriptManager());
                //}
            }

        }

        private void loadregions()
        {
            try
            {
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("");
                dr = new WARTECHCONNECTION.cConnect().ReadDB("select branchname from branches order by branchname");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList1.Items.Add(dr["branchname"].ToString());
                    }
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Choose your Region");
            }
            if (TextBox1.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Provide start Date");
            }
            if (TextBox2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("Provide end Date");
            }
            else
            {
                SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(" set dateformat dmy SELECT MemberNo as MEMBERNO,StaffNo as STAFFNO,IDNo as IDNUMBER,Surname +' '+OtherNames as MEMBERNAMES,Sex as GENDER,District as REGION,MobileNo as PHONENO,ApplicDate as REGISTRATIONDATE from MEMBERS where District='" + DropDownList1.Text + "' and ApplicDate between '" + TextBox1.Text + "' and '" + TextBox2.Text + "'ORDER BY MemberNo desc", Connection);

                try
                {
                    adapter.Fill(ds);
                    ExportToExcel(ds);
                }
                catch (Exception ex)
                {
                    Connection.Close();
                }
            }
        }
        private void ExportToExcel(System.ComponentModel.MarshalByValueComponent DataSource)
        {
            try
            {
                System.IO.StringWriter objStringWriter = new System.IO.StringWriter();
                System.Web.UI.WebControls.DataGrid tempDataGrid = new System.Web.UI.WebControls.DataGrid();
                System.Web.UI.HtmlTextWriter objHtmlTextWriter = new System.Web.UI.HtmlTextWriter(objStringWriter);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=FEPSACCOMEMBERS.xls");
                tempDataGrid.DataSource = DataSource;
                tempDataGrid.DataBind();
                tempDataGrid.HeaderStyle.Font.Bold = true;
                tempDataGrid.RenderControl(objHtmlTextWriter);
                DataSource.Dispose();
                HttpContext.Current.Response.Write(objStringWriter.ToString());
                HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}