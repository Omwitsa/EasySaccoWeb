using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EasyMa
{
    public partial class login : System.Web.UI.MasterPage
    {
        System.Data.SqlClient.SqlDataReader DR;
        private string usrLoginID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Changepwd/Bosa.aspx", false);
        }
    }
}