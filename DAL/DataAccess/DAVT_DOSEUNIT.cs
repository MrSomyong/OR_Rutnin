using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_DOSEUNIT : DataAccess
    {
        public DAVT_DOSEUNIT() { }
        public DAVT_DOSEUNIT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_DOSEUNIT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_DOSEUNIT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOSEUNIT> SearchAll()
        {
            List<DOSEUNIT> retValue = new List<DOSEUNIT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select Code, Name from VT_DOSEUNIT");
                strQuery.Append(" Order by Name ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOSEUNIT DOSEUNIT = new DOSEUNIT();
                    DOSEUNIT.Code = query["Code"].ToString();
                    DOSEUNIT.Name = query["Name"].ToString();
                    retValue.Add(DOSEUNIT);
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

        internal DOSEUNIT GetDoseUnitByKey(string code)
        {
            DOSEUNIT VT_DOSEUNIT = new DOSEUNIT();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_DOSEUNIT ");
                strQuery.Append(" where Code = @Code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("Code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_DOSEUNIT.Code = query["Code"].ToString();
                    VT_DOSEUNIT.Name = query["Name"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_DOSEUNIT;
        }

    }
}
