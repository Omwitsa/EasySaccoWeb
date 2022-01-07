using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USACBOSA
{

    public class Calculate_Loan_Repayment
    {

        System.Data.SqlClient.SqlDataReader dr, dr1;
        System.Data.SqlClient.SqlDataAdapter da;
        bool AutoCal = false;
        double DefaultedAmount = 0;
        double RepaidInterest, RepaidPrincipal = 0;
        double totalrepayable = 0;
        double mrepayment = 0;
        double RepayableInterest = 0;
        double Principal = 0;
        double interest = 0;
        int ActionOnInteretDefaulted = 0;
        double CurrentTotalDeductions = 0;
        double TotalPrinciple = 0;
        double totalinterest = 0;
        public double Pmt(double rrate, int rperiod, double initialAmount, int p_3)
        {
            var rate = (double)rrate / 100 / 12;
            var denominator = Math.Pow((1 + rate), rperiod) - 1;
            return (rate + (rate / denominator)) * initialAmount;
        }
        public double getCalculatedLoanRepayment(string LoanNo)
        {
            WARTECHCONNECTION.cConnect mconn = new WARTECHCONNECTION.cConnect();
            string sql = "SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + LoanNo + "'";
            dr = mconn.ReadDB(sql);
            if (dr.HasRows)
                while (dr.Read())
                {
                    RepaidInterest = Convert.ToDouble(dr["TotalInterest"].ToString());
                    RepaidPrincipal = Convert.ToDouble(dr["TotaRepaid"].ToString());
                }
            dr.Close(); dr.Dispose(); dr = null; mconn.Dispose(); mconn = null;

            WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
            string mysql = "SELECT  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE (LB.loanno='" + LoanNo + "')";
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
                            interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0); //'Interest owed is loaded
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
                    CurrentTotalDeductions = Principal + interest;
                    TotalPrinciple = TotalPrinciple + Principal;
                    totalinterest = totalinterest + interest;
                    // getCurrentTotalDeductions(CurrentTotalDeductions);
                    getTotalPrinciple(TotalPrinciple);
                    gettotalinterest(totalinterest);

                }
            dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;

            return CurrentTotalDeductions;

        }
        public double gettotalinterest(string LoanNo)
        {
                WARTECHCONNECTION.cConnect mconn = new WARTECHCONNECTION.cConnect();
                string sql = "SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + LoanNo + "'";
                dr = mconn.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr["TotalInterest"].ToString());
                        RepaidPrincipal = Convert.ToDouble(dr["TotaRepaid"].ToString());
                    }
                dr.Close(); dr.Dispose(); dr = null; mconn.Dispose(); mconn = null;

                WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
                string mysql = "SELECT  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE (LB.loanno='" + LoanNo + "')";
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
                            totalrepayable = rperiod * Pmt(rrate / 12 / 100, rperiod, -initialAmount, 0);

                            if (AutoCal == true)
                            {
                                mrepayment = Pmt(rrate / 12 / 100, rperiod, -initialAmount, 0);
                            }
                            else
                            {
                                mrepayment = repayrate;
                            }

                            if (ActionOnInteretDefaulted == 2)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0);// 'Interest owed is loaded
                            }
                            else
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is Accrued
                            }

                            Principal = Math.Round((mrepayment - interest), 0);
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
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0); //'Interest owed is loaded
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
                        CurrentTotalDeductions = Principal + interest;
                        TotalPrinciple = TotalPrinciple + Principal;
                        totalinterest = totalinterest + interest;
                        // getCurrentTotalDeductions(CurrentTotalDeductions);
                        getTotalPrinciple(TotalPrinciple);
                        gettotalinterest(totalinterest);

                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;

                return totalinterest;
            
            // return TotalPrinciple;
        }
        public double getTotalPrinciple(string LoanNo)
        {
            try
            {
                WARTECHCONNECTION.cConnect mconn = new WARTECHCONNECTION.cConnect();
                string sql = "SELECT CASE WHEN SUM(interest) IS NULL THEN 0 ELSE SUM(interest) END AS TotalInterest, CASE WHEN SUM(principal) IS NULL  THEN 0 ELSE SUM(principal) END AS TotaRepaid from repay where loanno='" + LoanNo + "'";
                dr = mconn.ReadDB(sql);
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        RepaidInterest = Convert.ToDouble(dr["TotalInterest"].ToString());
                        RepaidPrincipal = Convert.ToDouble(dr["TotaRepaid"].ToString());
                    }
                dr.Close(); dr.Dispose(); dr = null; mconn.Dispose(); mconn = null;

                WARTECHCONNECTION.cConnect ConSql = new WARTECHCONNECTION.cConnect();
                string mysql = "SELECT  C.LoanNo,ISNULL(LB.penalty,0) as PenaltyOwed,LB.DueDate,VD.Arrears DefAmount,C.DateIssued, C.Amount AS InitialAmount, LB.Balance,LB.loancode,LB.RepayRate,LB.lastdate,Lt.penalty,LB.MEMBERNO,LB.introwed,LB.intbalance, ISNULL(LB.RepayRate,0) AS RepRate,LB.RepayMethod,LB.RepayMode,LB.AutoCalc, case when (isnull(LB.RepayPeriod,0))=0 THEN LOANS.REPAYPERIOD ELSE LB.REPAYPERIOD END  AS RPERIOD, LB.Interest,LT.MDTEI,lt.intRecovery FROM loantype lt inner join LOANBAL LB on LB.loancode=lt.loancode INNER JOIN  CHEQUES C ON LB.LoanNo = C.LoanNo LEFT OUTER JOIN vwDefauters vd on vd.Loanno=C.Loanno LEFT OUTER JOIN LOANS ON LB.LOANNO=LOANS.LOANNO WHERE (LB.loanno='" + LoanNo + "')";
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
                            totalrepayable = rperiod * Pmt(rrate / 12 / 100, rperiod, -initialAmount, 0);

                            if (AutoCal == true)
                            {
                                mrepayment = Pmt(rrate / 12 / 100, rperiod, -initialAmount, 0);
                            }
                            else
                            {
                                mrepayment = repayrate;
                            }

                            if (ActionOnInteretDefaulted == 2)
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0);// 'Interest owed is loaded
                            }
                            else
                            {
                                interest = Math.Round(rrate / 12 / 100 * (LBalance), 0);// 'Interest owed is Accrued
                            }

                            Principal = Math.Round((mrepayment - interest), 0);
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
                                interest = Math.Round(rrate / 12 / 100 * (LBalance + intOwed), 0); //'Interest owed is loaded
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
                             //PrincAmount = 0;
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
                        CurrentTotalDeductions = Principal + interest;
                        TotalPrinciple = TotalPrinciple + Principal;
                        totalinterest = totalinterest + interest;
                        // getCurrentTotalDeductions(CurrentTotalDeductions);
                        getTotalPrinciple(TotalPrinciple);
                        gettotalinterest(totalinterest);

                    }
                dr1.Close(); dr1.Dispose(); dr1 = null; ConSql.Dispose(); ConSql = null;
            }
            catch (Exception ex)
            {
                WARSOFT.WARMsgBox.Show(ex.Message);
            }
            return TotalPrinciple;
            // return TotalPrinciple;
        }
        public double gettotalinterest(double totalinterest)
        {
            return totalinterest;
        }

        public double getTotalPrinciple(double TotalPrinciple)
        {
            return TotalPrinciple;
        }

        public double getCurrentTotalDeductions(double CurrentTotalDeductions)
        {
            return CurrentTotalDeductions;
        }

    }
}