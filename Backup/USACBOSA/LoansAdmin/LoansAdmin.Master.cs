using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.LoansAdmin
{
    public partial class LoansAdmin : System.Web.UI.MasterPage
    {
        System.Data.SqlClient.SqlDataReader DR;
        private string usrLoginID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mimi"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                usrLoginID = Session["mimi"].ToString();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
            if (!IsPostBack)
            {
                try
                {
                    DR = new WARTECHCONNECTION.cConnect().ReadDB("select UserName,usergroup from Useraccounts1 where UserLoginid='" + usrLoginID + "'");
                    if (DR.HasRows)
                        while (DR.Read())
                        {
                            this.Label2.Text = DR["UserName"].ToString();
                            this.Label4.Text = DR["usergroup"].ToString();
                        }
                    DR.Close();
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                    WARSOFT.WARMsgBox.Show(ex.Message);
                    return;
                }
            }
        }

        protected void NavigationMenu_MenuItemClick2(object sender, MenuEventArgs e)
        {

        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Changepwd/Passchange.aspx");
        }
    }
}