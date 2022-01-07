using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA
{
    public partial class Site : System.Web.UI.MasterPage
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
            {   usrLoginID = Session["mimi"].ToString();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
        }
    }
}