using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA
{
    public partial class memberjuniorphoto : System.Web.UI.Page
    {
        database dbbj = new database();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id))
            {
                var sql = "Select Photo from JuniorMemberPhotos where JuniorNo ='" + id + "'";
                dbbj.Select(sql, "img");
                if (dbbj.Data.Tables["img"].Rows.Count > 0)
                {
                    byte[] imgdatajunior = (byte[])dbbj.Data.Tables["img"].Rows[0]["Photo"];
                    Response.BinaryWrite(imgdatajunior);
                }
            }
        }
    }
}