using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string session_ID = Session["mimi"].ToString();
            string deleteDATA = "update session1 set status='OUT' where session_ID='" + Session["mimi"].ToString() + "'";
            new WARTECHCONNECTION.cConnect().WriteDB(deleteDATA);           
            Session.Remove("mimi");
            Session.Abandon();

            Response.Redirect("Default.aspx");           
        }
    }
}