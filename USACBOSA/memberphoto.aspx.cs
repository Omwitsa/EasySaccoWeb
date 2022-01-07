using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA
{
    public partial class memberphoto : System.Web.UI.Page
    {
        
        database db = new database();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id))
            {
                var sql = "Select Photo from MemberPhotos where MemberNo ='" + id + "'";
                db.Select(sql, "img");
                if (db.Data.Tables["img"].Rows.Count > 0)
                {
                    byte[] imgdata = (byte[])db.Data.Tables["img"].Rows[0]["Photo"];
                    Response.BinaryWrite(imgdata);
                }
            }
        }
    }
}