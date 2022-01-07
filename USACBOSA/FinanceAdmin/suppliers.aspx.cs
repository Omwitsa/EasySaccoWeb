using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA.FinanceAdmin
{
    public partial class suppliers : System.Web.UI.Page
    {
        public static System.Data.SqlClient.SqlDataReader dr, DR, Dr, dr1, dr4, dr2, dr6, dr7;
        System.Data.SqlClient.SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox2.Text = "";
                txtChequeNo.Text = "";
                txtDistributedAmount.Text = "";
                txtGlAccountName.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                string readdataLoan = "select count(supplierid) as id  FROM ag_Supplier1";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(readdataLoan);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        double suppliercode = Convert.ToDouble(dr[0]);
                        //suppliercode++;
                        txtMemberNo.Text = Convert.ToString(suppliercode+1);
                    }
                else
                {
                    txtMemberNo.Text = "1";
                }
                dr.Close(); dr.Dispose(); dr = null;
            }
            btnAdd.Enabled = false;
            Button1.Enabled = false;
            btnRemove.Enabled = true;
            LoadBanks();
            LoadBanks2();
            LoadsupplierAccounts();

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            Button1.Enabled = false;
           
            TextBox2.Text = "";
            txtChequeNo.Text = "";
            txtDistributedAmount.Text = "";
            txtGlAccountName.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            string readdataLoan = "select count(supplierid) as supplierid FROM ag_Supplier1";
            dr = new WARTECHCONNECTION.cConnect().ReadDB(readdataLoan);
            if (dr.HasRows)
                while (dr.Read())
                {
                    double suppliercode = Convert.ToDouble(dr[0]);
                    //suppliercode++;
                    txtMemberNo.Text = Convert.ToString(suppliercode+1);
                }
            else
            {
                txtMemberNo.Text = "1";
            }
            dr.Close(); dr.Dispose(); dr = null;
            btnRemove.Enabled = true;
        }
        private void LoadBanks()
        {
            cboBankAC.Items.Add("");
            string sql = "select Accno from GLSETUP";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    cboBankAC.Items.Add("" + dr["Accno"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }
        private void LoadBanks2()
        {
            DropDownList2.Items.Add("");
            string sql = "select Accno from GLSETUP";
            WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
            dr = oSaccoMaster.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    DropDownList2.Items.Add("" + dr["Accno"].ToString() + "");
                }
            dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
        }
        private void LoadsupplierAccounts()
        {
            try
            {
                da = new WARTECHCONNECTION.cConnect().ReadDB2("SELECT     SupplierId, CompanyName, SupplierAccno, DebitAccno, SupplierAccName, DebitAccName, narration FROM         ag_supplier1 order by supplierid");
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (txtChequeNo.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (txtDistributedAmount.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (txtGlAccountName.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (TextBox4.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (txtnarration.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            if (TextBox5.Text == "")
            {
                WARSOFT.WARMsgBox.Show("fill the blank spaces");
                return;
            }
            else
            {
                string TransNo = "SELECT  SupplierID, CompanyName, ContactPerson, ContactTitle,Address,Email,Phone,Fax,narration FROM ag_Supplier1 where supplierid='" + txtMemberNo + "'";
                WARTECHCONNECTION.cConnect TNo = new WARTECHCONNECTION.cConnect();
                dr1 = TNo.ReadDB(TransNo);
                if (!dr1.HasRows)                   
                    {
                        string insert = "set dateformat dmy INSERT INTO   ag_Supplier1(SupplierID, CompanyName, ContactPerson, ContactTitle,supplieraccno,debitaccno,SupplierAccName,DebitAccName, Address, Email, Phone, Fax,narration) values('" + txtMemberNo.Text + "','" + TextBox2.Text + "','" + txtChequeNo.Text + "','" + TextBox4.Text + "','" + cboBankAC.Text + "','" + DropDownList2.Text + "','"+txtBankAC.Text+"','"+txtDRacc.Text+"','" + txtGlAccountName.Text + "','" + TextBox3.Text + "','" + txtDistributedAmount.Text + "','" + TextBox5.Text + "','" + txtnarration.Text + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(insert);
                        WARSOFT.WARMsgBox.Show("Supplier Details Saved successfully");
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; TNo.Dispose(); TNo = null;
            }
            btnAdd.Enabled =true;
            Button1.Enabled =true;
            btnRemove.Enabled =false;
        }
        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            string TransNo = "SELECT SupplierID, CompanyName, ContactPerson, ContactTitle,supplieraccno,debitaccno,,SupplierAccName,DebitAccName, Address, Email, Phone, Fax,narration FROM  ag_Supplier1 where SupplierID = '" + txtMemberNo.Text + "'";
            WARTECHCONNECTION.cConnect TNoo = new WARTECHCONNECTION.cConnect();
            dr2 = TNoo.ReadDB(TransNo);
            if (dr2.HasRows)
             while(dr2.Read())
             {
                 TextBox2.Text = dr2["CompanyName"].ToString();
                 TextBox4.Text = dr2["ContactTitle"].ToString();
                 txtChequeNo.Text = dr2["ContactPerson"].ToString();
                 txtGlAccountName.Text = dr2["Address"].ToString();
                 TextBox3.Text = dr2["Email"].ToString();
                 txtDistributedAmount.Text = dr2["Phone"].ToString();
                 TextBox5.Text = dr2["Fax"].ToString();
                 txtnarration.Text = dr2["narration"].ToString();
                 DropDownList2.Text = dr2["debitaccno"].ToString();
                 cboBankAC.Text = dr2["supplieraccno"].ToString();
                 txtBankAC.Text = dr2["SupplierAccName"].ToString();
                 txtDRacc.Text = dr2["DebitAccName"].ToString();
             }
            dr2.Close(); dr2.Dispose(); dr2 = null; TNoo.Dispose(); TNoo = null;
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            string insert = "Update ag_Supplier1  SET CompanyName = '" + TextBox2.Text + "', ContactPerson = '" + txtChequeNo.Text + "',narration='"+txtnarration.Text+"' ContactTitle = '" + TextBox4.Text + "', Address = '" + txtGlAccountName.Text + "', Email = '" + TextBox3.Text + "', Phone = '" + txtDistributedAmount.Text + "', Fax = '" + TextBox5.Text + "'  WHERE SupplierID = '" + txtMemberNo.Text + "'";
            new WARTECHCONNECTION.cConnect().WriteDB(insert);
            WARSOFT.WARMsgBox.Show("Supplier Details updated successfully");
        }

        protected void cboBankAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Glaccname from GLSETUP where Accno='" + cboBankAC.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtBankAC.Text = dr["Glaccname"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Glaccname from GLSETUP where Accno='" + DropDownList2.Text + "'";
                WARTECHCONNECTION.cConnect oSaccoMaster = new WARTECHCONNECTION.cConnect();
                dr = oSaccoMaster.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        txtDRacc.Text = dr["Glaccname"].ToString();

                    }
                dr.Close(); dr.Dispose(); dr = null; oSaccoMaster.Dispose(); oSaccoMaster = null;
            }

            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void btnFindBank_Click(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadsuplliers();
        }
        private void loadsuplliers()
        {
            this.txtMemberNo.Text = GridView2.SelectedRow.Cells[1].Text;
            string TransNo = "SELECT SupplierID, CompanyName, ContactPerson, ContactTitle,supplieraccno,debitaccno,SupplierAccName,DebitAccName, Address, Email, Phone, Fax,narration FROM  ag_Supplier1 where SupplierID = '" + txtMemberNo.Text + "'";
            WARTECHCONNECTION.cConnect TNoo = new WARTECHCONNECTION.cConnect();
            dr2 = TNoo.ReadDB(TransNo);
            if (dr2.HasRows)
                while (dr2.Read())
                {
                    TextBox2.Text = dr2["CompanyName"].ToString().Replace("'"," ");
                    TextBox4.Text = dr2["ContactTitle"].ToString().Replace("'", " ");
                    txtChequeNo.Text = dr2["ContactPerson"].ToString().Replace("'", " ");
                    txtGlAccountName.Text = dr2["Address"].ToString().Replace("'"," ");
                    TextBox3.Text = dr2["Email"].ToString().Replace("'", " ");
                    txtDistributedAmount.Text = dr2["Phone"].ToString().Replace("'", " ");
                    TextBox5.Text = dr2["Fax"].ToString().Replace("'", " ");
                    txtnarration.Text = dr2["narration"].ToString().Replace("'", " ");
                    DropDownList2.Text = dr2["debitaccno"].ToString().Replace("'", " ");
                    cboBankAC.Text = dr2["supplieraccno"].ToString().Replace("'", " ");
                    txtBankAC.Text = dr2["SupplierAccName"].ToString().Replace("'", " ");
                    txtDRacc.Text = dr2["DebitAccName"].ToString().Replace("'", " ");
                }
            dr2.Close(); dr2.Dispose(); dr2 = null; TNoo.Dispose(); TNoo = null;
        }
    }
}