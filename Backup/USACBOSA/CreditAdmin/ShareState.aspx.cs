﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.CreditAdmin
{
    public partial class ShareState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack == false)
            {
                Response.Redirect("~/Reports/ShareStatementReport.aspx");
            }
        }
    }
}