using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ADOUtils;

namespace DAL
{
     class DAVNMST : DataAccess
    {
        public DAVNMST() { }
        public DAVNMST(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVNMST(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVNMST(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VNMST> SearchByKey(VNMST _VNMST)
        {
            List<VNMST> retValue = new List<VNMST>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*  from dbo.VNMST a ");
                //strQuery.Append(" left join " + _tblSETUPUSERTYPE + " b on a." + _UseType + " = b." + _ID);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_VNMST.HN))
                {
                    strQuery.Append(" and a.HN = @HN");
                }
                //if (!string.IsNullOrEmpty(_VNMST.VISITDATE.ToString()))
                //{
                //    strQuery.Append(" and a.VISITDATE = @VISITDATE");
                //}
                if (!string.IsNullOrEmpty(_VNMST.VN))
                {
                    strQuery.Append(" and a.VN = @VN");
                }
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("HN", IDbType.VarChar, DBNullConvert.From(_VNMST.HN)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VNMST.VN)));
                //parameter.Add(new IParameter("@VISITDATE", IDbType.DateTime, DBNullConvert.From(_VNMST.VISITDATE)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VNMST VNMST = new VNMST();
                    VNMST.VN = query["VN"].ToString();
                    VNMST.HN = query["HN"].ToString();
                    VNMST.VISITINDATETIME = ADOUtil.GetDateFromQuery(query["VISITINDATETIME"].ToString());
                    
                    retValue.Add(VNMST);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return retValue;
        }




        //public List<VNMST> SearchVNMSTByKey(VNMST)
        //{
        //    string sql = @"Select [VISITDATE]
        //                  ,[VN]
        //                  ,[HN]
        //                  ,[VISITINDATETIME]
        //                  ,[VISITOUTDATETIME] 
        //                  from VNMST where IsDelete = 0 Order by Code";
        //    using (SqlConnection cn = new SqlConnection(SQLHelper.connDBCheckUpDB))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, cn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {

        //            List<VNMST> vnmsts = new List<VNMST>();
        //            while (reader.Read())
        //            {
        //                VNMST vnmst = new VNMST();
        //                vnmst.VISITDATE = (DateTime)reader["VISITDATE"]; //reader["VISITDATE"] != DBNull.Value ? (DateTime)reader["VISITDATE"] : vnmst.VISITDATE = null;
        //                vnmst.VN = reader["VN"].ToString();
        //                vnmst.HN = reader["HN"].ToString();
        //                vnmst.VISITINDATETIME = reader["VISITINDATETIME"] != DBNull.Value ? (DateTime)reader["VISITINDATETIME"] : vnmst.VISITINDATETIME = null;
        //                vnmst.VISITOUTDATETIME = reader["VISITOUTDATETIME"] != DBNull.Value ? (DateTime)reader["VISITOUTDATETIME"] : vnmst.VISITOUTDATETIME = null;
        //                vnmsts.Add(vnmst);
        //            }
        //            return vnmsts;
        //        }
        //        else
        //            return new List<VNMST>();
        //    }



        //}
    }
}
