using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
//using StationaryDAL;

namespace DAL
{
    public class DBConnect
    {
        string StrConn = "";
        private SqlCommand sqlComm;
        private SqlConnection conn;
        public DBConnect()
        { }
        public DBConnect(string _strConn)
        {
            try
            {
                //StrConn = WebConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                ConstDB.StrConn = _strConn;
                //conn = new SqlConnection(StrConn);
                //if (conn.State.ToString() == "Open")
                //{
                //    conn.Close();
                //}
                //conn.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public SqlConnection openConnect()
        {
            try
            {
                //StrConn = WebConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                conn = new SqlConnection(ConstDB.StrConn);
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
                conn.Open();
                return conn;
            }
            catch (SqlException e)
            {                
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public void CloseConnect()
        {
            try
            {
                conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
