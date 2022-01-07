using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace USACBOSA
{
    public class database
    {
        private SqlConnection con;
        private SqlDataAdapter adpter;
        private SqlCommand com;
        private DataSet data;
        private SqlDataReader reader;
        public DataSet Data
        {
            get { return data; }
            set { data = value; }
        }
        public database()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["bosaConnectionString"].ConnectionString);
            if(con.State ==ConnectionState.Closed) con.Open();
            adpter = new SqlDataAdapter("", con);
            com = new SqlCommand("", con);
            data = new DataSet();
        }
        public DataTable Select(string sql,string table)
        {
            adpter.SelectCommand.CommandText = sql;
            if (data.Tables.Contains(table))
            {
                data.Tables[table].Clear();
            }
            adpter.Fill(data, table);
            return data.Tables[table];
        }
        public void Execute(string sql)
        {
            com.CommandTimeout = 600000;
            com.CommandText = sql;
            com.Connection = con;
            com.ExecuteNonQuery();
        }
        public string ExecuteScalar(string sql)
        {
            string result = "";
            com.CommandTimeout = 600000;
            com.CommandText = sql;
            com.Connection = con;
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                result = reader[0].ToString();
            }
            reader.Close();
            return result;
        }
        public int ExecuteScalarWithReturn(string sql)
        {
            int result ;
            com.CommandTimeout = 600000;
            com.CommandText = sql;
            com.Connection = con;
            //reader = com.ExecuteReader();
            result = Convert.ToInt32(com.ExecuteScalar());
            return result;
        }
        
    }
}