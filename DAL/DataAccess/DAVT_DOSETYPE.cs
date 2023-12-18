using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_DOSETYPE : DataAccess
    {
        public DAVT_DOSETYPE() { }
        public DAVT_DOSETYPE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_DOSETYPE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_DOSETYPE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOSETYPE> SearchAll()
        {
            List<DOSETYPE> retValue = new List<DOSETYPE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select Code, Name from VT_DOSETYPE");
                strQuery.Append(" Order by Name ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOSETYPE DOSETYPE = new DOSETYPE();
                    DOSETYPE.Code = query["Code"].ToString();
                    DOSETYPE.Name = query["Name"].ToString();
                    retValue.Add(DOSETYPE);
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

        internal DOSETYPE GetDoseTypeByKey(string code)
        {
            DOSETYPE VT_DOSETYPE = new DOSETYPE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_DOSETYPE ");
                strQuery.Append(" where Code = @Code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("Code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_DOSETYPE.Code = query["Code"].ToString();
                    VT_DOSETYPE.Name = query["Name"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_DOSETYPE;
        }

    }
}
