using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_DOSECODE : DataAccess
    {
        public DAVT_DOSECODE() { }
        public DAVT_DOSECODE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_DOSECODE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_DOSECODE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOSECODE> SearchAll()
        {
            List<DOSECODE> retValue = new List<DOSECODE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select Code, Name from VT_DOSECODE");
                strQuery.Append(" Order by Name ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOSECODE DOSECODE = new DOSECODE();
                    DOSECODE.Code = query["Code"].ToString();
                    DOSECODE.Name = query["Name"].ToString();
                    retValue.Add(DOSECODE);
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

        internal DOSECODE GetDoseCodeByKey(string code)
        {
            DOSECODE VT_DOSECODE = new DOSECODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_DOSECODE ");
                strQuery.Append(" where Code = @Code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("Code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_DOSECODE.Code = query["Code"].ToString();
                    VT_DOSECODE.Name = query["Name"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_DOSECODE;
        }

    }
}
