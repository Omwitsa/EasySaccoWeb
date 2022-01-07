using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace USACBOSA
{
    public partial class sharetoloanoffsettin : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlDataReader dr, dr1, dr2, dr3, dr4, dr5, DR, DR1, DR2, DR3, DR4, DR5, DR6, Dr1, Dr2, Dr3, Dr4, Dr5, Dr6, Dr7, Dr8, Dr9;
        System.Data.SqlClient.SqlDataAdapter da;

        int select = 0;
        bool AutoCal = false;
        double RepaidInterest = 0;
        double RepaidPrincipal = 0;
         double Principal = 0;
         double mrepayment = 0;
         double IntrOwed = 0;
         double totalrepayable = 0;
         double RepayableInterest = 0;
         string transactionNo = "";
        double loanbalance = 0;
         double intTotal = 0;
         int ActionOnInteretDefaulted = 0;
         string mMemberno = "";
         double IntBalalance = 0;
         DateTime FirstDate = DateTime.Today;  DateTime nextdate = DateTime.Today;
         double interest = 0;
         int RepayMode = 0;
         double Penalty = 0;
         double intOwed = 0;
         double PrincAmount = 0;
         DateTime duedate = DateTime.Today;

         string rmethod = "";
         int mdtei = 0;
         DateTime lastrepay;
         DateTime Dateissued;
         double Amount = 0;
         string penaltyAcc = "";
         string PremiumAcc = "";
         int PaymentNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Session["mimi"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                catch (Exception ex) { Response.Redirect("~/Default.aspx"); return; }
                //dtpTransDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                //Charges0.Text = "3";
                //Amont0.Text = "0";
                //mnt0.Text = "0";
                //MultiView1.ActiveViewIndex = 0;
                //MultiView2.ActiveViewIndex = -1;
                //MultiView3.ActiveViewIndex = -1;
                //MultiView4.ActiveViewIndex = -1;
                //if (!IsPostBack)
                //{
                //    BindDropsharecode();
                //    BindDropdown();
                //    BindDev();
                //}
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        private void BindDev()
        {
            Dev0.Items.Clear();
            try
            {
                Dev0.Items.Clear();
                Dev0.Items.Add("");
                DR6 = new WARTECHCONNECTION.cConnect().ReadDB("select LoanCode,LoanType from LOANTYPE");
                if (DR6.HasRows)
                    while (DR6.Read())
                    {
                        this.Dev0.Items.Add(DR6["LoanCode"].ToString());

                    }
                DR6.Close(); DR6.Dispose(); DR6 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }

        private void BindDropdown()
        {
            DropDownList2.Items.Clear();
            try
            {
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add("");
                DR4 = new WARTECHCONNECTION.cConnect().ReadDB("Select accno from glsetup order by accno asc");
                if (DR4.HasRows)
                    while (DR4.Read())
                    {
                        this.DropDownList2.Items.Add(DR4["accno"].ToString());

                    }
                DR4.Close(); DR4.Dispose(); DR4 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }
        private void BindDropsharecode()
        {
            Dropsharecode.Items.Clear();
            try
            {
                Dropsharecode.Items.Clear();
                Dropsharecode.Items.Add("");
                DR3 = new WARTECHCONNECTION.cConnect().ReadDB("select SharesCode,SharesType from ShareType");
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        this.Dropsharecode.Items.Add(DR3["SharesCode"].ToString());

                    }
                DR3.Close(); DR3.Dispose(); DR3 = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message); return;
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView3.ActiveViewIndex = 0;
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {
                        String Loanno = GridView1.Rows[i].Cells[1].Text;
                        da = new WARTECHCONNECTION.cConnect().ReadDB2("select guarMemberno,Guarnames,GuarAmount [Guanteed Amount],GuarBalance,loanbalance  from vwloanguarantors  where loanno= '" + Loanno + "' and GuarBalance>5");
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView5.Visible = true;
                        GridView5.DataSource = ds;
                        GridView5.DataBind();
                        ds.Dispose();
                        da.Dispose();
                        select++;
                    }
                    //else
                    //{
                    //    WARSOFT.WARMsgBox.Show("Ensure that the checkbox is Cheked");
                    //}
                }

                if (select == 0)
                {
                    Page.RegisterStartupScript("Alert Message", "<script language='javascript'>alert('Please check one checkbox records');</script>");
                    return;
                }
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView2.ActiveViewIndex = 0;
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {
                        String Loanno1 = GridView1.Rows[i].Cells[1].Text;
                        String lcode = Loanno1.Substring(0, 3);
                        WARTECHCONNECTION.cConnect bert = new WARTECHCONNECTION.cConnect();
                        string albert = "Select loancode from loantype where isMain = 1 order by loancode";
                        Dr6 = bert.ReadDB(albert);
                        if (Dr6.HasRows)
                        {
                            while (Dr6.Read())
                            {
                                Dev0.Text = Dr6["LoanCode"].ToString();
                                // TxtDevloan0.Text = Dr6["LoanType"].ToString();
                                DR = new WARTECHCONNECTION.cConnect().ReadDB("Select loantype, interest,repaymethod,repayPeriod from loantype  where loancode='" + Dev0.Text + "'");
                                if (DR.HasRows)
                                {
                                    while (DR.Read())
                                    {
                                        TxtDevloan0.Text = DR["loantype"].ToString();
                                        Rmethod.Text = DR["repaymethod"].ToString();
                                        Rperiod0.Text = DR["repayPeriod"].ToString();

                                        Rate0.Text = DR["interest"].ToString();
                                        if (TxtMemberNo.Text == "")
                                        {
                                            Newloan0.Text = "";
                                        }
                                    }
                                }
                                DR.Close(); DR.Dispose(); DR = null;
                                //    Else
                                //        txtLoanNo.Text = getLoanno(cboLoanCode(0), txtmemberno)
                                //    End If
                                //ELSE
                                //    TxtDevloan0.Text = "";
                                //    txtRMethod(Index).Text = ""
                                //    txtRate(Index).Text = 0
                                //    txtLoanNo.Text = ""
                                //End If
                            }
                        }
                        Dr6.Close(); Dr6.Dispose(); Dr6 = null;
                        String Balance = GridView1.Rows[i].Cells[4].Text;
                        String intrest = GridView1.Rows[i].Cells[5].Text;
                        Double getintrest = Convert.ToDouble(Balance) - Convert.ToDouble(intrest);
                        Double intrestss = Convert.ToDouble(intrest);
                        Double lBalance = Convert.ToDouble(Balance);
                        Double totals = getintrest + lBalance;
                        Double intrate = Convert.ToDouble(Charges0.Text);
                        Double chargess = intrate / 100 * totals;
                        Amont0.Text = chargess.ToString();
                        //
                        /*nnnnn*/
                    }
                    else if (AtmSelector.Checked == false)
                    {
                        Page.RegisterStartupScript("Alert Message", "<script language='javascript'>alert('Please check atleast one checkbox records');</script>");
                    }
                }
                WARTECHCONNECTION.cConnect berte = new WARTECHCONNECTION.cConnect();
                string alberto = "select COUNT(MemberNo)MemberNo from LOANS where MemberNo='" + TxtMemberNo.Text + "'";
                Dr7 = berte.ReadDB(alberto);
                if (Dr7.HasRows)
                {
                    while (Dr7.Read())
                    {
                        int a = Convert.ToInt32(Dr7["MemberNo"]);
                        int b = a + 1;
                        Newloan0.Text = Dev0.Text + "" + TxtMemberNo.Text + "-" + b;
                    }
                }
                Dr7.Dispose(); Dr7.Dispose(); Dr7 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView4.ActiveViewIndex = 0;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Offsett_Click(object sender, EventArgs e)
        {
            try
            {
                double allocatedShare = 0;
                double OffSetAmount = 0;
                string Remark = "";
                double totalOffsets = 0;
                double ticked = 0;
                //sql = "This Process Offsets the existing loans from the member Shares/Deposits" & vbNewLine _
                //& "The Process is started assuming that the member is withdrawing or is declared Defaulter" & vbNewLine _
                //& "His/Her shares are Prorated on the existing Loans " & vbNewLine _
                //& vbNewLine & "CONTINUE?"

                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    CheckBox AtmSelector = (CheckBox)row.FindControl("AtmSelector");
                    if (AtmSelector.Checked == true)
                    {

                        // 'else: Begin Transaction


                        NewTransaction(Convert.ToDouble(TxtBalance.Text), Convert.ToDateTime(dtpTransDate.Text), "Share-Loan Offsetting - MemberNo -" + TxtMemberNo.Text + "");


                        allocatedShare = 0;
                        string Loanno = GridView1.Rows[i].Cells[1].Text;
                        //// allocatedShare = if(Convert.ToDouble((GridView1.Rows[i].Cells[3].Text) + Convert.ToDouble(GridView1.Rows[i].Cells[4].Text) > Convert.ToDouble(txtTotalShares), Convert.ToDouble(txtTotalShares), (Convert.ToDouble(.ListItems(i).ListSubItems(3)) + Convert.ToDouble(GridView1.Rows[i].Cells[4].Text;)));
                        //// OffSetAmount = Math.Round(OffSetAmount + allocatedShare, 1);

                        //'pay the loan now
                        //'But first, Get the shares account which becomes the contra acc
                        string shareAcc = "";
                        DR = new WARTECHCONNECTION.cConnect().ReadDB("Select SharesAcc from sharetype where sharescode='" + Dropsharecode.Text + "'");
                        if (DR.HasRows)
                        {
                            while (DR.Read())
                            {
                                shareAcc = DR["SharesAcc"].ToString();
                            }
                        }
                        DR.Close(); DR.Dispose(); DR = null;
                        SaveRepay(Loanno, Convert.ToDateTime(dtpTransDate.Text), Math.Round(allocatedShare, 1), shareAcc, "Share-Loan Offsetting", "0", "1", "Offseting Loanno - " + Loanno, Session["mimi"].ToString(), Session["mimi"].ToString(), transactionNo, false, true);
                        if (Remark == "")
                        {
                            Remark = Loanno;
                        }
                        else
                        {
                            Remark = Remark + ' ' + Loanno;
                        }

                        //  'Less His/Shares now here ----- AMOS
                        SaveContrib(TxtMemberNo.Text, Convert.ToDateTime(dtpTransDate.Text), Dropsharecode.Text, OffSetAmount * (-1), "Non-Cash", "Share-Loan Offsetting", "Non-Cash", Session["mimi"].ToString(), "Offseting Loans - " + Remark, transactionNo);


                        //  'Offsetting Fees

                        if (Convert.ToDouble(txtOffsettingfee.Text) > 0)
                        {

                            //'                 If Not SaveContrib(txtMemberNo, dtpTransDate, cboSharesType, CDbl(txtOffsettingFee.Text) * (-1), cboAccno(1).Text, "Share-Loan Offsetting", "JV", User, "Charges on Offseting Loans - " & Remark, transactionNo) Then
                            //'                     GoTo TransError
                            //'                 End If
                        }
                    }
                }
                //  txtmemberno_Change();
                WARSOFT.WARMsgBox.Show("Offsetting Completed Successfully");
                return;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void SaveContrib(string memberno, DateTime ContrDate, string SHARECode, double contrAmount, string BankAcc, string Receiptno, string chequeno, string transby, string remarks, string transactionNo)
        {

        }

        private void SaveRepay(string Loanno, DateTime DateDeposited, double Amountt, string BankAcc, string Receiptno, string Locked, string Posted, string remarks, string auditid, string transby, string transactionNo, bool penalise, bool charge)
        {
            try
            {


                double mPrincipal = 0; double mInterest = 0;
                double intCharged = 0;
                double intPaid = 0;
                double owedPaid = 0;
                string TempAcc = "";
                DateTime dreceived = new DateTime();
                double overpadAmnt = 0;
                string interestAcc = ""; string ContraAcc = ""; string LoanAcc = "";
                // Today = Get_Server_Date;
                string csql = "select max(IsNull(paymentno,0))PaymentNo from repay where loanno='" + Loanno + "' ";
                dr = new WARTECHCONNECTION.cConnect().ReadDB(csql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        PaymentNo = Convert.ToInt32(dr["PaymentNo"]) + 1;
                    }
                else
                {
                    PaymentNo = 1;
                }
                dr.Close(); dr.Dispose(); dr = null;
                dreceived = DateDeposited;
                Calculate_Loan_Repayment(Loanno);
                DateDeposited = dreceived;

                intCharged = interest;
                if (RepayMode == 1)
                {
                    mdtei = mdtei;
                }
                else
                {
                    mdtei = 0;
                }

                if (lastrepay.Month == DateDeposited.Month)
                {
                    interest = 0;
                    intCharged = 0;
                }
                if (DateDeposited.Day <= mdtei && Amountt > 0)    // ' Means he should not this time be charged interest
                {
                    intCharged = 0;
                    interest = 0;
                }
                if (Amountt >= Penalty)
                {
                    Amountt = Amountt - Penalty;
                }
                if (Amountt > 0)
                {
                    if (rmethod == "STL")
                    {
                        while (IntBalalance > 0 && Amountt > 0)
                        {
                            if (Amountt > interest)
                            {
                                mInterest = mInterest + interest;
                                Amountt = Amountt - interest;
                                if (Amountt >= Principal)
                                {
                                    if (mPrincipal + Principal > loanbalance)
                                    {
                                        Principal = loanbalance - mPrincipal;
                                    }
                                    mPrincipal = mPrincipal + Principal;
                                    Amountt = Amountt - Principal;
                                }
                                else
                                {
                                    if (mPrincipal + Principal > loanbalance)
                                    {
                                        Principal = loanbalance - mPrincipal;
                                    }
                                    mPrincipal = mPrincipal + Principal;
                                    Amountt = Amountt - Principal;
                                }
                            }
                            else
                            {
                                mInterest = mInterest + Amountt;
                                Amountt = 0;
                            }
                            IntBalalance = IntBalalance - interest;
                        }
                        if (Amountt > 0)
                        {
                            mPrincipal = mPrincipal + Amountt;
                            Amountt = 0;
                        }
                        Principal = mPrincipal;
                        interest = mInterest;
                    }
                    else
                    {
                        if (Amountt > intOwed)
                        {
                            owedPaid = intOwed;
                            Amountt = Amountt - owedPaid;
                            if (Amountt > interest)
                            {
                                interest = Math.Round(interest, 0);
                                Amountt = Amountt - interest;
                                interest = interest + intOwed;
                                intOwed = 0;
                            }
                            // 'check if clearing the loan eg refinancing etc
                            if (Math.Round(Amountt, 0) > Math.Round(loanbalance, 0))
                            {
                                if (Amountt == loanbalance)
                                {
                                    Principal = Principal;
                                }
                                else
                                {
                                    Principal = loanbalance;
                                    goto loansrepay;
                                    overpadAmnt = Amountt - loanbalance;

                                    //  'remaing money to vover recovery
                                    if (overpadAmnt > 0)
                                    {
                                        // 'GL Transactions
                                        dr1 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.PremiumAcc,IsNull(lt.penaltyAcc,'')PenaltyAcc,lt.ReceivableAcc from loantype lt  inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                                        if (dr1.HasRows)
                                            while (dr1.Read())
                                            {
                                                PremiumAcc = dr1["PremiumAcc"].ToString();
                                                LoanAcc = dr1["LoanAcc"].ToString();
                                                interestAcc = dr1["InterestAcc"].ToString();
                                                penaltyAcc = dr1["PenaltyAcc"].ToString();
                                            }
                                        else
                                        {
                                            WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                                            return;
                                        }
                                        dr1.Close(); dr1.Dispose(); dr1 = null;
                                        if (LoanAcc == "" || interestAcc == "" || (Penalty > 0 && penaltyAcc == ""))
                                        {
                                            WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                                        }
                                        ContraAcc = BankAcc;
                                        Save_GLTRANSACTION(dreceived, overpadAmnt, ContraAcc, PremiumAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                                    }

                                }
                            }
                            else
                            {
                                if (Amountt == loanbalance)
                                {
                                    Principal = Amountt;
                                }
                                else
                                {
                                    // 'remaing money to cover recovery
                                    Amountt = Amountt;
                                    if (Amountt > Principal && loanbalance >= Amountt)
                                    {
                                        Principal = Amountt;
                                    }

                                }
                            }

                        }

                        else
                        {
                            interest = Amountt + owedPaid;
                            Amountt = 0;
                            Principal = 0;
                        }
                        intPaid = interest;
                    }
                }
                if (Amountt < 0)// 'REVERSAL!
                {
                    intPaid = 0;
                    intOwed = 0;
                    owedPaid = 0;
                    Principal = Amountt;
                    Amountt = 0;
                }
                else if (Amountt == 0)
                {
                    intOwed = intOwed + intCharged;
                    intPaid = 0;
                    owedPaid = 0;
                    interest = 0;
                    Principal = 0;
                }

            loansrepay:
                loanbalance = Math.Round(loanbalance - Principal, 0);

                FirstDate = DateDeposited;

                string sssql = "set dateformat dmy Insert into Repay(Loanno,Datereceived,Paymentno,Amount,Principal,Interest,intrCharged,IntrOwed,Penalty,intbalance,Loanbalance,Receiptno,TransBy,Remarks,auditid,TransactionNo) Values('" + Loanno + "','" + DateDeposited + "'," + PaymentNo + "," + (Principal + interest) + "," + Principal + "," + interest + "," + intCharged + "," + intOwed + "," + Penalty + "," + IntBalalance + "," + loanbalance + ",'" + Receiptno + "','" + transby + "','" + remarks + "','" + auditid + "','" + transactionNo + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(sssql);
                string upsql = "set dateformat dmy UPDATE loanbal set balance=" + loanbalance + ",intrOwed=" + intOwed + " ,intBalance=" + IntBalalance + " ,lastdate='" + DateDeposited + "',duedate='" + duedate + "',penalty=" + Penalty + " where loanno='" + Loanno + "'";
                new WARTECHCONNECTION.cConnect().WriteDB(upsql);

                //'GL Transactions

                dr2 = new WARTECHCONNECTION.cConnect().ReadDB("select lt.interestAcc,lt.loanAcc,IsNull(lt.penaltyAcc,'')penaltyAcc,lt.ReceivableAcc from loantype lt inner join loanbal l on lt.loancode=l.loancode where l.loanno='" + Loanno + "'");
                if (dr2.HasRows)
                    while (dr2.Read())
                    {
                        LoanAcc = dr2["LoanAcc"].ToString();
                        interestAcc = dr2["InterestAcc"].ToString();
                        penaltyAcc = dr2["PenaltyAcc"].ToString();
                        //  'receivableAcc = IIf(IsNull(rst("ReceivableAcc")), "", rst("ReceivableAcc"))
                    }
                else
                {
                    WARSOFT.WARMsgBox.Show("Can't get the Gl Accounts Required");
                    return;
                }

                if (LoanAcc == "" || interestAcc == "" || (Penalty > 0) && penaltyAcc == "")
                {
                    WARSOFT.WARMsgBox.Show("Either the Loan or Interest or penalty Gl Control Accounts have not been set. Do that to proceed!");
                }

                ContraAcc = BankAcc;

                if (interest > 0)
                {
                    Save_GLTRANSACTION(dreceived, interest, ContraAcc, interestAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                }

                if (Penalty > 0)
                {
                    Save_GLTRANSACTION(dreceived, Penalty, ContraAcc, penaltyAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);

                }

                if (Principal > 0)
                {
                    Save_GLTRANSACTION(dreceived, Principal, ContraAcc, LoanAcc, Receiptno, mMemberno, "User", remarks, 1, 1, Receiptno, transactionNo);
                }


            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void Save_GLTRANSACTION(DateTime TransDate, double Amount, string DRaccno, string Craccno, string DocumentNo, string Source, string auditid, string TransDescription, int CashBook, int doc_posted, string chequeno, string transactionNo)
        {
            new WARTECHCONNECTION.cConnect().WriteDB("Set DateFormat DMY Exec Save_GLTRANSACTION '" + TransDate + "'," + Amount + ",'" + DRaccno + "','" + Craccno + "','" + DocumentNo + "','" + Source + "','" + auditid + "','" + TransDescription + "'," + CashBook + "," + doc_posted + ",'" + chequeno + "','" + transactionNo + "','Bosa'");
        }

        private void Calculate_Loan_Repayment(string Loanno)
        {
            try
            {
                WARTECHCONNECTION.cConnect mconn = new WARTECHCONNECTION.cConnect();
                string sql = "SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + Loanno + "'";
                dr = mconn.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr["TotalInterest"].ToString());
                        RepaidPrincipal = Convert.ToDouble(dr["TotaRepaid"].ToString());
                    }
                dr.Close(); dr.Dispose(); dr = null; mconn.Dispose(); mconn = null;

                WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
                string mysql = "SELECT (select top 1 IntrOwed  from REPAY  where LoanNo =lb.loanno order by PaymentNo  desc) as lastintowed,  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE(LB.loanno='" + Loanno + "')";
                dr1 = ConSql.ReadDB(mysql);
                if (dr1.HasRows)
                    while (dr1.Read())
                    {
                        string mMemberno = dr1["memberno"].ToString();
                        string rmethod = dr1["RepayMethod"].ToString();
                        int rperiod = Convert.ToInt32(dr1["RPeriod"].ToString());
                        double rrate = Convert.ToDouble(dr1["interest"].ToString());
                        double initialAmount = Convert.ToDouble(dr1["InitialAmount"].ToString());
                        double intOwed = Convert.ToDouble(dr1["intrOwed"].ToString());
                        double LBalance = Convert.ToDouble(dr1["Balance"].ToString());
                        DateTime lastrepay = Convert.ToDateTime(dr1["LastDate"].ToString());
                        DateTime Dateissued = Convert.ToDateTime(dr1["dateissued"].ToString());
                        DateTime duedate = Convert.ToDateTime(dr1["duedate"].ToString());
                        string LoanCode = dr1["LoanCode"].ToString();
                        int mdtei = Convert.ToInt32(dr1["mdtei"].ToString());
                        double repayrate = Convert.ToDouble(dr1["RepRate"].ToString());
                        string intRecovery = dr1["intRecovery"].ToString();
                        //bool DefaultedAmount = Convert.ToBoolean(dr1["DefAmount"].ToString());//].ToString(); = True, 0, dr1["DefAmount"].ToString();].ToString();
                        double penaltyOwed = Convert.ToDouble(dr1["PenaltyOwed"].ToString());
                        double loanbalance = Convert.ToDouble(LBalance);
                        AutoCal = Convert.ToBoolean(dr1["AutoCalc"].ToString());
                        string RepayMode = dr1["RepayMode"].ToString();
                        bool wePenalize = Convert.ToBoolean(dr1["Penalty"].ToString());
                        double IntBalalance = Convert.ToDouble(dr1["intBalance"].ToString());

                        if (rmethod == "AMRT")
                        {
                            totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);

                            if (AutoCal == true)
                            {
                                mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                            }
                            else
                            {
                                mrepayment = repayrate;
                                if (mrepayment <= 0)
                                {
                                    getRepayRate(Convert.ToDouble(initialAmount), rrate, rmethod, rperiod, Convert.ToDouble(initialAmount));
                                    mrepayment = Principal + interest;
                                }
                            }

                            if (ActionOnInteretDefaulted == 2)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                            }
                            else
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is Accrued
                            }

                            Principal = Math.Round((mrepayment - interest), 2);
                            RepayableInterest = 0;
                        }
                        if (rmethod == "STL")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 100 / rperiod));
                            RepayableInterest = interest * rperiod;
                            mrepayment = Principal + interest;
                            if (interest >= IntBalalance)
                            {
                                if (IntBalalance < 0)
                                {
                                    interest = 0;
                                }
                                else
                                {
                                    interest = IntBalalance;
                                }
                            }
                        }
                        if (rmethod == "RBAL")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                            Principal = initialAmount / rperiod;
                            if (ActionOnInteretDefaulted == 1)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0); //'Interest owed is loaded
                            }
                            else //'Accrue
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is accrued
                            }
                            if (AutoCal == false)
                            {
                                mrepayment = repayrate;
                                Principal = mrepayment - interest;
                            }

                            RepayableInterest = 0;//'unpredictable
                        }
                        if (rmethod == "RSPECIAL")
                        {
                            double intTotal = 0;
                            double actualInterest = Math.Round(rrate / 12 / 100 * (LBalance), 0);
                            double PrincAmount = 0;
                            LBalance = initialAmount;
                            for (int i = 1; i <= rperiod; i++)
                            {
                                Principal = initialAmount / rperiod;
                                interest = (rrate / 12 / 100) * LBalance;
                                intTotal = intTotal + interest;
                                LBalance = LBalance - Principal;
                            }
                            interest = intTotal / rperiod;
                            RepayableInterest = 0;
                            LBalance = loanbalance;// 'to continue with the previous flow
                        }
                        if (rmethod == "RSTL")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                            Principal = initialAmount / rperiod;
                            if (ActionOnInteretDefaulted == 1)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0);// 'Interest owed is loaded
                            }
                            else // 'Accrue
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0); //'Interest owed is accrued
                            }

                            RepayableInterest = 0; //'unpredictable
                        }
                        if (rmethod == "ADV")
                        {
                            totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                            Principal = initialAmount / rperiod;
                            interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                            RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                        }

                        if (Principal > LBalance)
                        {
                            Principal = LBalance;
                        }
                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;

                //return CurrentTotalDeductions;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void getRepayRate(double initialAmount, double rrate, string rmethod, int rperiod, double LBalance)
        {
            double pesaYote = 0;
            if (rmethod == "AMRT")
            {
                totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                double interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                interest = Math.Ceiling(interest);
                Principal = Math.Round((mrepayment - interest), 2);
                Amount = Principal + interest + IntrOwed;
                RepayableInterest = 0;
            }
            if (rmethod == "STL")
            {
                totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                Principal = initialAmount / rperiod;
                interest = (initialAmount * (rrate / 12 / 100));
                Amount = Principal + interest;
                RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
            }
            if (rmethod == "RBAL")
            {
                totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                Principal = initialAmount / rperiod;
                interest = (rrate / 12 / 100) * LBalance;
                Amount = Principal + interest;
                // 'intrcharged = LoanAmount * (rRate / 200 * (RPeriod + 1)) / RPeriod
                RepayableInterest = 0;// 'unpredictable
            }
            if (rmethod == "RSPECIAL")
            {
                Double intTotal = 0;
                Double PrincAmount = 0;
                LBalance = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = initialAmount / rperiod;
                    // 'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                interest = intTotal / rperiod;
                Amount = Principal + interest;
                RepayableInterest = 0;
                LBalance = loanbalance;// 'to continue with the previous flow
            }
            if (rmethod == "RSTL")
            {
                pesaYote = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = pesaYote / rperiod;
                    // 'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                Amount = Principal + interest;
                IntBalalance = intTotal;
                RepayableInterest = 0;// 'unpredictable
            }
            if (rmethod == "ADV")
            {
                totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                Principal = initialAmount / rperiod;
                interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                Amount = Principal + interest;
            }
            if (rmethod == "RSPECIAL")
            {
                PrincAmount = 0;
                LBalance = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = initialAmount / rperiod;
                    //'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                interest = intTotal / rperiod;
                Amount = Principal + interest;
            }
        }
        private void NewTransaction(double AmountPaid, DateTime TransDate, string Description)
        {
            DateTime TimeNow = DateTime.Now;
            transactionNo = Convert.ToString(TimeNow);
            transactionNo = transactionNo.Replace("/", "").Replace(" ", "").Replace(":", "").TrimStart().TrimEnd();
            string sql = "set dateformat dmy Insert into transactions(transactionno,amount,auditid,TransDate,transDescription,status) Values('" + transactionNo + "'," + AmountPaid + ",'" + Session["mimi"].ToString() + "',Convert(Varchar(10), '" + TransDate + "', 101),'" + Description + "','Posted')";
            new WARTECHCONNECTION.cConnect().WriteDB(sql);
        }

        protected void guaoffset_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void TxtMemberNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView4.ActiveViewIndex = -1;

                DR = new WARTECHCONNECTION.cConnect().ReadDB("Select m.surname + ' ' + m.othernames as membernames from members m  where m.memberno='" + TxtMemberNo.Text + "' ");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        TxtNames.Text = DR["membernames"].ToString();
                        cboSharesType_Change();
                    }
                }
                else
                {
                    TxtNames.Text = "";
                    TxtTotShares.Text = "0";
                }
                DR.Close(); DR.Dispose(); DR = null;

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void cboSharesType_Change()
        {/*
            double AvailableShares=0;
    double mLoanBalance =0;
    
   // 'get his loan balance
    DR1 = new WARTECHCONNECTION.cConnect().ReadDB("select isnull(balance,0) Balance from loanbal where memberno='" + TxtMemberNo.Text + "' and balance>10");
    if(DR1.HasRows) 
    {
        while(DR1.Read())
        {
            
        TxtBalance.Text = DR1["balance"].ToString();
        }
    }
    else
    {
        TxtBalance.Text = "0";
    }
    string sql="";
    if(TxtMemberNo.Text == "" )
    {
        sql = "Select sharestype,0,Withdrawable,sharesAcc from sharetype where sharescode='" + Dropsharecode.Text + "'";
    }
    else
    {
        sql = " Select st.sharestype,st.withdrawable,st.sharesacc,isnull(sum(s.amount),0) TotalShares from sharetype st right outer join contrib s on s.sharescode=st.sharescode where st.sharescode='" + Dropsharecode.Text + "' and s.memberno='" + TxtMemberNo.Text + "' group by st.sharestype,st.withdrawable,st.sharesacc";
    }
    DR3 = new WARTECHCONNECTION.cConnect().ReadDB(sql);
    if(DR3.HasRows)
    {
        while(DR3.Read())
        {
            TextShareType.Text = DR3["sharestype"].ToString();
            TxtTotShares.Text= DR3["TotalShares"].ToString();
        netShares = IIf(netShares < 0, 0, netShares);
        txtTotalShares = IIf(txtTotalShares.Text < 0, 0, txtTotalShares.Text)
'        txtOffsettingFee.Text = IIf(txtTotalShares.Text <= 0, 0, txtOffsettingFee.Text)
'        txtOffsettingFee = IIf(CDbl(txtOffsettingFee.Text) > CDbl(txtTotalShares), txtTotalShares, txtOffsettingFee)
        
        isWithdrawable = IIf(Rst("Withdrawable") = True, True, False)
        shareAcc = Rst("SharesAcc")
    Else
        txtsharetype.Text = ""
        txtTotalShares.Text = 0
        isWithdrawable = False
        shareAcc = ""
        Exit Sub
    End If
    Dim RsIntowed As New ADODB.Recordset
    
    'His Pending Loans
    sql = "select lb.loanno,lt.loantype,c.amount,lb.balance,lb.intBalance,lb.intrOwed,lt.isMain,isnull(lb.repayperiod,0) rperiod,c.dateissued" _
    & " from loanbal lb inner join loantype lt on lb.loancode=lt.loancode inner join cheques c on lb.loanno=c.loanno" _
    & " where lb.memberno='" & txtMemberNo & "' and lb.balance>10"
    Set Rst = oSaccoMaster.GetRecordSet(sql)
    With Rst
        If .EOF Then
            lvwMemberLoans.ListItems.Clear
            txtlbalance.Text = 0
        Else
            mLoanBalance = 0
            While Not .EOF
                Calculate_Loan_Repayment (!Loanno)
                mLoanBalance = mLoanBalance + !balance + interest + intOwed
            .MoveNext
            Wend
            If mLoanBalance < CDbl(txtTotalShares) Then
                AvailableShares = mLoanBalance
            Else
                AvailableShares = CDbl(txtTotalShares)
            End If
'            txtlbalance.Text = Format(mLoanBalance, Cfmt)
            .MoveFirst
            lvwMemberLoans.ListItems.Clear
            While Not .EOF
                Set li = lvwMemberLoans.ListItems.Add(, , !Loanno)
                Calculate_Loan_Repayment (!Loanno)
                
                If Day(dtpTransDate) < mdtei Then
                    interest = 0
                End If
                If IntBalalance <= 0 Then
                    Set RsIntowed = oSaccoMaster.GetRecordSet("select top 1 IntrOwed  from repay   where loanno ='" & !Loanno & "' order by paymentNo desc")
                    If Not RsIntowed.EOF Then
                        intOwed = IIf(IsNull(RsIntowed.Fields!IntrOwed), 0, RsIntowed.Fields!IntrOwed)
                        oSaccoMaster.Execute ("update loanbal set intrOwed =" & intOwed & "  where Loanno ='" & !Loanno & "' ")
                        
                        IntBalalance = intOwed
                    End If
                    
                End If
                
                
                
                li.ListSubItems.Add , , !LoanType
                li.ListSubItems.Add , , !Amount
                li.ListSubItems.Add , , Format(!balance, Cfmt)
                If intRecovery <> "All" Then
                    li.ListSubItems.Add , , intOwed + interest
                Else
                    li.ListSubItems.Add , , IntBalalance
                End If
                If !balance <= 0 Then
                    li.ListSubItems.Add , , 0
                Else
                    If isWithdrawable = True Then
                        li.ListSubItems.Add , , Format(netShares, Cfmt)
                    Else
                        li.ListSubItems.Add , , Format(((!balance + interest + intOwed) / mLoanBalance) * netShares, Cfmt)
                    End If
                End If
                li.ListSubItems.Add , , Round((!balance / !Amount), 1) * 100
                li.ListSubItems.Add , , IIf(!isMain = True, 1, 0)
                li.ListSubItems.Add , , !rperiod - DateDiff("M", !Dateissued, dtpTransDate)
            .MoveNext
            Wend
            
            
        End If
        txtAmount = 0
        txtRPeriod(0) = 0
        txtCharges = 0
    End With
        */
        }

        protected void Dropsharecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //  MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                MultiView4.ActiveViewIndex = -1;
                WARTECHCONNECTION.cConnect ll = new WARTECHCONNECTION.cConnect();
                string fnme = "Select sharestype,withdrawable,SharesAcc from sharetype where sharescode='" + Dropsharecode.Text + "' ";
                DR1 = ll.ReadDB(fnme);
                if (DR1.HasRows)
                {
                    while (DR1.Read())
                    {
                        TextShareType.Text = DR1["SharesType"].ToString();
                        loadtotshares();
                    }
                }
                DR1.Close(); DR1.Dispose(); DR1 = null;
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void loadtotshares()
        {
            try
            {
                WARTECHCONNECTION.cConnect lla = new WARTECHCONNECTION.cConnect();
                string fnme = "select isnull(balance,0) Balance from loanbal where memberno='" + TxtMemberNo.Text + "' and balance>1";
                DR2 = lla.ReadDB(fnme);
                if (DR2.HasRows)
                {
                    while (DR2.Read())
                    {
                        TxtBalance.Text = DR2["balance"].ToString();
                    }
                }
                DR2.Close(); DR2.Dispose(); DR2 = null;
                calculateintrest();
                WARTECHCONNECTION.cConnect llac = new WARTECHCONNECTION.cConnect();
                string like = "select isnull(sum(s.amount),0) TotalShares,st.usedtoguarantee from contrib S inner join sharetype st ON S.sharescode=st.sharescode where S.memberno='" + TxtMemberNo.Text + "' and S.sharescode='" + Dropsharecode.Text + "' group by s.MEMBERNO,st.usedtoguarantee ";
                DR3 = llac.ReadDB(like);
                if (DR3.HasRows)
                    while (DR3.Read())
                    {
                        TxtTotShares.Text = DR3["TotalShares"].ToString();
                        try
                        {
                            da = new WARTECHCONNECTION.cConnect().ReadDB2("select lb.loanno,lt.loantype,c.amount,lb.balance,lb.intBalance,lb.intrOwed,lt.isMain,isnull(lb.repayperiod,0) rperiod,c.dateissued from loanbal lb inner join loantype lt on lb.loancode=lt.loancode inner join cheques c on lb.loanno=c.loanno where memberno='" + TxtMemberNo.Text + "' and lb.balance>10");
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            GridView1.Visible = true;
                            GridView1.DataSource = ds;
                            GridView1.DataBind();
                            ds.Dispose();
                            da.Dispose();
                        }
                        catch (Exception ex)
                        {
                            WARSOFT.WARMsgBox.Show(ex.Message); return;
                        }
                    }
                DR3.Close(); DR3.Dispose();
            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        private void calculateintrest()
        {
            /*
            DateTime DateDeposited = Convert.ToDateTime(txtDateDeposited.Text);
            DateTime lastrepay = Convert.ToDateTime(lastrepaydate);
            int datediff = ((DateDeposited.Year - lastrepay.Year) * 12) + DateDeposited.Month - lastrepay.Month;

            Double pesaYote, totalrepayable = 0;
            double interest_B = 0;
            double interest_A = 0;
            string valret = "";
            Double Amount = 0;
            int m = datediff;
            if (rmethod == "AMRT")
            {

                if (m > 1 && chkAcruedInterest.Checked == false)
                {
                    WARSOFT.WARMsgBox.Show("You have not been paying " + vote + " for the last " + m + " months");
                    return;
                }
                for (m = 1; m < (datediff); m++)
                {
                    totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                    double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                    //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                    interest_A = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                    interest_A = Math.Ceiling(interest_A);
                    interest_B = interest_B + interest_A;
                    interest = interest_B;
                    Principal = Math.Round((mrepayment - interest_A), 2);
                    Amount = Principal + interest + interest_A + IntrOwed;
                    RepayableInterest = 0;//'baadaye
                    valret = "ok";
                }
                if (valret == "ok")
                {
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','0','" + (interest + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
                if (valret != "ok")
                {
                    totalrepayable = rperiod * Pmt(rrate, rperiod, initialAmount, 0);
                    double mrepayment = Math.Round(Pmt(rrate, rperiod, initialAmount, 0), 2);
                    //'mrepayment = repayrate ' I did this after alot of discussions with lucy
                    double interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 2);// 'Interest owed is loaded
                    interest = Math.Ceiling(interest);
                    Principal = Math.Round((mrepayment - interest), 2);
                    Amount = Principal + interest + IntrOwed;
                    RepayableInterest = 0;//'baadaye
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','0','" + (interest_A + IntrOwed) + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
            }
            if (rmethod == "STL")
            {
                if (IntBalance > 0)
                {

                    if (m > 1 && chkAcruedInterest.Checked == false)
                    {
                        WARSOFT.WARMsgBox.Show("You have not been paying " + vote + " for the last " + m + " months");
                        return;
                    }
                    for (m = 1; m < (datediff); m++)
                    {
                        totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                        Principal = initialAmount / rperiod;
                        //  interest = (initialAmount * (rrate / 12 / 100));
                        interest_A = Math.Round((initialAmount * (rrate / 12 / 100)), 2);// 'Interest owed is loaded
                        interest_A = Math.Ceiling(interest_A);
                        interest_B = interest_B + interest_A;
                        interest = interest_B;

                        RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                        if ((interest + interest_A) > IntBalance)
                        {
                            interest = IntBalance;
                            IntrOwed = IntBalance;
                        }

                        valret = "ok";
                    }
                    Amount = Principal + interest + interest_A;
                    if (valret == "ok")
                    {
                        string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest_A + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    }
                    if (valret != "ok")
                    {
                        totalrepayable = initialAmount + (initialAmount * (rrate / 12 / 100) * rperiod);
                        Principal = initialAmount / rperiod;
                        interest = (initialAmount * (rrate / 12 / 100));
                        Amount = Principal + interest;
                        RepayableInterest = (initialAmount * (rrate / 12 / 100) * rperiod);
                        string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "')";
                        new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                    }
                }
                else
                {
                    Principal = initialAmount / rperiod;
                    interest = 0;
                    RepayableInterest = 0; IntrOwed = 0;
                    Amount = Principal + interest;
                    string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,IntBalance,IntOwed,MemberNo,MemberNames,Type,TransType,RMethod)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + RepayableInterest + "','" + IntrOwed + "','" + memberno + "','" + MemberNames + "','" + Type + "','LoanRepayment','" + rmethod + "')";
                    new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
                }
            }
            if (rmethod == "RBAL")
            {
                totalrepayable = initialAmount + (initialAmount * (rrate + 1) / 200);
                Principal = initialAmount / rperiod;
                interest = (rrate / 12 / 100) * LBalance;
                Amount = Principal + interest;
                // 'intrcharged = LoanAmount * (rRate / 200 * (RPeriod + 1)) / RPeriod
                RepayableInterest = 0;// 'unpredictable
                string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
            }
            if (rmethod == "RSPECIAL")
            {
                Double intTotal = 0;
                Double PrincAmount = 0;
                LBalance = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = initialAmount / rperiod;
                    // 'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                interest = intTotal / rperiod;
                Amount = Principal + interest;
                RepayableInterest = 0;
                LBalance = loanbalance;// 'to continue with the previous flow
                string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
            }
            if (rmethod == "RSTL")
            {
                pesaYote = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = pesaYote / rperiod;
                    // 'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                Amount = Principal + interest;
                IntBalalance = intTotal;
                RepayableInterest = 0;// 'unpredictable
                string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
            }
            if (rmethod == "ADV")
            {
                totalrepayable = initialAmount + (initialAmount * (rrate / 200 * (rperiod + 1)));
                Principal = initialAmount / rperiod;
                interest = (initialAmount * (rrate / 200 * (rperiod + 1))) / rperiod;
                RepayableInterest = (initialAmount * (rrate / 200 * (rperiod + 1)));
                Amount = Principal + interest;
                string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
            }
            if (rmethod == "RSPECIAL")
            {
                PrincAmount = 0;
                LBalance = initialAmount;
                for (int i = 1; i <= rperiod; i++)
                {
                    Principal = initialAmount / rperiod;
                    //'PrincAmount = PrincAmount + Principal
                    interest = (rrate / 12 / 100) * LBalance;
                    intTotal = intTotal + interest;
                    LBalance = LBalance - Principal;
                }
                interest = intTotal / rperiod;
                Amount = Principal + interest;
                string ISERDATA = "insert  into MembersAccount(Vote,TransDescription,Amount,Principal,Interest,MemberNo,MemberNames,Type)values('" + vote + "','" + TransDescription + "','" + Amount + "','" + Principal + "','" + interest + "','" + memberno + "','" + MemberNames + "','" + Type + "')";
                new WARTECHCONNECTION.cConnect().WriteDB(ISERDATA);
            }*/
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;
            WARTECHCONNECTION.cConnect lln = new WARTECHCONNECTION.cConnect();
            string fnme1 = "Select accno,Glaccname from glsetup where accno='" + DropDownList2.Text + "' ";
            DR5 = lln.ReadDB(fnme1);
            if (DR5.HasRows)
                while (DR5.Read())
                {
                    TextBox3.Text = DR5["Glaccname"].ToString();
                }
            DR5.Close(); DR5.Dispose(); lln = null;
        }

        protected void Dev0_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;
            WARTECHCONNECTION.cConnect lanz = new WARTECHCONNECTION.cConnect();
            string form = "Select LoanCode, LoanType, Interest,RepayPeriod,Repaymethod from LOANTYPE where LoanCode='" + Dev0.Text + "' ";
            Dr1 = lanz.ReadDB(form);
            if (Dr1.HasRows)
                while (Dr1.Read())
                {
                    TxtDevloan0.Text = Dr1["LoanType"].ToString();
                    Rate0.Text = Dr1["Interest"].ToString();
                    Rperiod0.Text = Dr1["RepayPeriod"].ToString();
                    Rmethod.Text = Dr1["Repaymethod"].ToString();
                }
            Dr1.Close(); Dr1.Dispose(); lanz = null;
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            MultiView3.ActiveViewIndex = 0;
            MultiView4.ActiveViewIndex = -1;
            // String Loanno = GridView4.SelectedRow.Cells[1].Text;
            //da = new WARTECHCONNECTION.cConnect().ReadDB2("select * from LOANGUAR where LoanNo= '" + Loanno + "'");
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //GridView5.Visible = true;
            //GridView5.DataSource = ds;
            //GridView5.DataBind();
            //ds.Dispose();
            //da.Dispose();
            //        Double inRates = Convert.ToDouble(Charges0.Text);
            //        Double Principle = Convert.ToDouble(TxtBalance.Text);
            //        Double Intrest = (inRates /100)*Principle;
            //        Double Totlon = Intrest + Principle;
            //        Amont0.Text = Intrest.ToString();
            //        mnt0.Text = Totlon.ToString();
            //        WARTECHCONNECTION.cConnect minx = new WARTECHCONNECTION.cConnect();
            //        string fume = "select count(LoanNo)LoanNo from LOANS where MemberNo='" + TxtMemberNo.Text + "' and LoanCode='" + Dev0.Text + "'";
            //        Dr3 = minx.ReadDB(fume);
            //if (Dr3.HasRows)
            //    while (Dr3.Read())
            //    {
            //        countloan = Convert.ToInt32(Dr3["LoanNo"]);//.ToString();
            //        int a = 1;
            //        loancount = countloan + a;
            //        Newloan0.Text = (Loanno + "-" + loancount).ToString();
            //    }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                //double GuarAmount = 0;
                //if (GridView1.Rows.Count < 1)
                //{
                //    WARSOFT.WARMsgBox.Show("Please enter the new guarantors");
                //    return;
                //}

                //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                //{
                //    mMemberno = GridView1.Rows[i].Cells[1].Text;
                //    GuarAmount = GridView1.Rows[i].Cells[2].Text;


                //    DR = "select * from loanguar where loanno='" + lblloanno.Caption + "' and memberno='" + mMemberno + "'";
                //    if (DR.HasRows)
                //    {
                //        while (DR.Read())
                //        {
                //            new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy Update LOANGUAR set MemberNo='" + mMemberno + "',AMOUNT=" + GuarAmount + ",BALANCE=" + GuarAmount + " where LoanNo='" + lblloanno.Caption + "' and MemberNo='" + mMemberno + "'");
                //        }
                //    }
                //    else
                //    {
                //        new WARTECHCONNECTION.cConnect().WriteDB("set dateformat dmy INSERT INTO LOANGUAR(MemberNo, LoanNo, Amount, Balance, AuditID, AuditTime, Transfered)VALUES('" + mMemberno + "','" + lblloanno + "'," + GuarAmount + "," + GuarAmount + ",'" + Session["mimi"].ToString() + "','" + System.DateTime.Now + "',0)");
                //    }
                //    DR.Close(); DR.Dispose(); DR = null;
                //}

                ////'update the old memberno as replaced
                //new WARTECHCONNECTION.cConnect().WriteDB("Update loanGuar set transfered=1 where loanno='" + lblloanno + "' and memberno='" + txtDGMemberno.Text + "'");

                //txtDGMemberno_Change();
                //WARSOFT.WARMsgBox.Show("Guarantor Replaced!");

            }
            catch (Exception ex) { WARSOFT.WARMsgBox.Show(ex.Message); return; }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}