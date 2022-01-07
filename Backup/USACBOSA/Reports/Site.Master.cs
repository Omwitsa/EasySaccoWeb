using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.Reports
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        System.Data.SqlClient.SqlDataReader DR, DR2;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("The Page Loaded at: " + DateTime.Now.ToLongTimeString());
            try
            {
                if (Session["mimi"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (Session["mimi"].Equals(""))
                {
                    Response.Redirect("~/Default.aspx");
                }
                string UserLoginID = Session["mimi"].ToString();
                string branchcode = "";
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                if (!IsPostBack)
                {
                    try
                    {
                        DR = new WARTECHCONNECTION.cConnect().ReadDB("select * from UserAccounts1 where UserLoginID='" + UserLoginID + "' ");
                        if (DR.HasRows)
                            while (DR.Read())
                            {
                                this.Label1.Text = DR["UserName"].ToString();
                            }
                        DR.Close();

                        DR2 = new WARTECHCONNECTION.cConnect().ReadDB("select Department from UserAccounts1 where UserLoginID='" + UserLoginID + "'");
                        if (DR2.HasRows)
                            while (DR2.Read())
                            {
                                this.Label2.Text = DR2["Department"].ToString();
                            }
                        DR2.Close();
                    }
                    catch (Exception ex)
                    {
                        WARSOFT.WARMsgBox.Show(ex.Message); return;
                    }
                }
            }
            catch(Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
    

    }
}

