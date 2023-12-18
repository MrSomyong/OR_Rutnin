using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_UNIT : DataAccess
    {
        public DAVT_UNIT() { }
        public DAVT_UNIT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_UNIT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_UNIT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<UNIT> SearchAll()
        {
            List<UNIT> retValue = new List<UNIT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select code ,  Name from [dbo].[VT_UNIT]");
                strQuery.Append(" Order by Name ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    UNIT UNIT = new UNIT();
                    UNIT.Code = query["code"].ToString();
                    UNIT.Name = query["Name"].ToString();
                    retValue.Add(UNIT);
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


        internal UNIT SearchByKey(string code)
        {
            UNIT unit = new UNIT();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select code ,  Name from [dbo].[VT_UNIT] ");
                strQuery.Append(" where code = @code");
             
                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("code", IDbType.VarChar, DBNullConvert.From(code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    unit.Code = query["code"].ToString();
                    unit.Name = query["Name"].ToString();
                    
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
                 
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return unit;
        }

    }
}
