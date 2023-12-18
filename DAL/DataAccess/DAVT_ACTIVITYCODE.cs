using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_ACTIVITYCODE : DataAccess
    {
        public DAVT_ACTIVITYCODE() { }
        public DAVT_ACTIVITYCODE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_ACTIVITYCODE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_ACTIVITYCODE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

       

        internal ACTIVITYCODE GetActivityCodeByKey(string code)
        {
            ACTIVITYCODE VT_ACTIVITYCODE = new ACTIVITYCODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_ACTIVITYCODE ");
                strQuery.Append(" where Activity = @code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_ACTIVITYCODE.Activity = query["Activity"].ToString();
                    VT_ACTIVITYCODE.ActivityName = query["ActivityName"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_ACTIVITYCODE;
        }
    }
}
