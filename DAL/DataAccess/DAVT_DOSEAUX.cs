using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_DOSEAUX : DataAccess
    {
        public DAVT_DOSEAUX() { }
        public DAVT_DOSEAUX(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_DOSEAUX(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_DOSEAUX(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOSEAUX> SearchAll()
        {
            List<DOSEAUX> retValue = new List<DOSEAUX>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from VT_DOSEAUX");
                strQuery.Append(" Order by name");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOSEAUX aux = new DOSEAUX();
                    aux.Code = query["code"].ToString();
                    aux.Name = query["Name"].ToString();
                    retValue.Add(aux);
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

        internal DOSEAUX GetDoseAUXByKey(string code)
        {
            DOSEAUX VT_DOSEAUX = new DOSEAUX();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_DOSEAUX ");
                strQuery.Append(" where code = @code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_DOSEAUX.Code = query["code"].ToString();
                    VT_DOSEAUX.Name = query["Name"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_DOSEAUX;
        }
    }
}
