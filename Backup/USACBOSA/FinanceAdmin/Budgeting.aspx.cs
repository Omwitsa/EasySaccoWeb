using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.FinanceAdmin
{
    public partial class Budgeting : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtBudgetYear.Text = DateTime.Today.Year.ToString();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void optSpread_CheckedChanged(object sender, EventArgs e)
        {
            if (optSpread.Checked == true)
            {
                optFixed.Checked = false;
            }
        }

        protected void optFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (optFixed.Checked == true)
            {
                optSpread.Checked = false;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LBLGLCONTRA.Text = GridView2.SelectedRow.Cells[1].Text;
                this.glName1.Text = GridView2.SelectedRow.Cells[2].Text;
                Get_Budget();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Get_Budget()
        {
            try
            {
                string sehhh = "Select mmonth Period,yyear [End Date],Budgetted [Budgetted Amount] From BUDGETS where AccNo='" + LBLGLCONTRA.Text + "' and yYear=" + txtBudgetYear.Text + " order by mMonth";
                da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();
                GridView1.Visible = false;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string sehhh = "Select AccNo,GLAccName,GLAccGroup,GLAccGroup From GLSETUP";
            da = new WARTECHCONNECTION.cConnect().ReadDB2(sehhh);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            ds.Dispose();
            da.Dispose();
        }
    }
}


        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
               
//                Save_My_Budget();
//                WARSOFT.WARMsgBox.Show("Budget Updated Successfully");
//                LBLGLCONTRA.Text = "";
//                glName1.Text = "";
//                txtbudgettedamount.Text = "0";
//                 lvwBudget.ListItems.Clear
//                glName1.Focus();
//                 SendKeys "{Home}+{End}"
//                Exit Sub

//                '//get the last items
//                dr = new WARTECHCONNECTION.cConnect().ReadDB("select accno from budgets where accno='" + LBLGLCONTRA + "' and yyear=" + cboyear + "");
//                if (dr.HasRows)
//                {
//                    new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy insert into budgets(Accno, mmonth, yyear, Actual, Budgetted, Variance) values('" + LBLGLCONTRA + "'," + month(TransDate) + "," + cboyear + "," + lblactuals + "," + txtbudgettedamount + "," + CCur(CCur(txtbudgettedamount) - CCur(lblactuals)) + ")");
//                    WARSOFT.WARMsgBox.Show("You have successfully added the records");
//                    return;
//                }
//                else
//                {
//                    new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy UPDATE budgets  SET actual=" + lblactuals + " ,variance=" + CCur(lblactuals) - txtbudgettedamount + " where accno='" + LBLGLCONTRA + "' and yyear=" + cboyear + "");
//                    WARSOFT.WARMsgBox.Show("You have successfully updated the account");
//                    return;
//                }
//                dr.Close(); dr.Dispose(); dr = null;
//            }
//            catch (Exception ex) { }
//        }

//        private void Save_My_Budget()
//        {
//            try
//            {
//    int i=0; 
//    if(LBLGLCONTRA.Text.Trim() == "" )
//    {
//         WARSOFT.WARMsgBox.Show( "Please supply the Account No");
//        LBLGLCONTRA.Focus();
//        return;
//    }
//    dr = new WARTECHCONNECTION.cConnect().ReadDB("Select AccNo From GLSETUP where AccNo='" + LBLGLCONTRA + "'")
//   if(dr.HasRows)
//   {
//       while(dr.Read())
//       {
//                new WARTECHCONNECTION.cConnect().WriteDB("Delete From BUDGETS where AccNo='" + LBLGLCONTRA + "' and yYear=" + Year(dtpBudgetYear));
//                for( i = 1; i <= 12)
//                {
//                    Set li = lvwBudget.ListItems(1)
//                     Save_The_Budget(LBLGLCONTRA.Text, i, Year(dtpBudgetYear),CDbl(li.SubItems(2)), "") ;
                        
//       }
//   }
//                else
//                {
//                 WARSOFT.WARMsgBox.Show("Account No " + LBLGLCONTRA + " not found in the Chart Of Accounts");
//                LBLGLCONTRA.Focus();
//                return;
//   }
//            End If
//        End If
//    End With
//    Exit Sub
//        */
//            }
//            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
//        }
//    }
//}