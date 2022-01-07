using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USACBOSA.Setup
{
    public partial class MembershipDefaults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccNames.Text == "")
                {
                    WARSOFT.WARMsgBox.Show("The Account has not been given");
                    return;
                }

                if (Convert.ToDouble(txtValue.Text) <= 0)
                {
                    WARSOFT.WARMsgBox.Show("The value of this Default item must be given");
                    return;
                }
                string Contrib = "0";
                if (chkIsContribution.Checked == true)
                {
                    Contrib = "1";
                }
                new WARTECHCONNECTION.cConnect().WriteDB("Insert into defaults(ACCNO,DESCRIPTION,AMOUNT,Contribution,sharescode) VALUES('" + cboAccno.Text + "','" + txtAccNames.Text.Trim() + "'," + txtValue.Text.Trim() + "," + Contrib + ",'" + cboSharesCode.Text.Trim() + "')");
                LoadDefaults();
                WARSOFT.WARMsgBox.Show("Defaults Saved sucessfully");
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void LoadDefaults()
        {
            
        }

        protected void chkIsContribution_CheckedChanged(object sender, EventArgs e)
        {
            LoadSharetype();
        }

        private void LoadSharetype()
        {
            
        }

        protected void cboAccno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}