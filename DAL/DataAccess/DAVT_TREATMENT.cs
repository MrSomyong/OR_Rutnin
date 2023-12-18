using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class DAVT_TREATMENT : DataAccess
    {
        public DAVT_TREATMENT() { }
        public DAVT_TREATMENT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_TREATMENT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_TREATMENT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_TREATMENT> SearchAll()
        {
            List<VT_TREATMENT> retValue = new List<VT_TREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from VT_TREATMENT" );
                strQuery.Append(" order by CODE");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_TREATMENT VT_TREATMENT = new VT_TREATMENT();
                    VT_TREATMENT.CODE = query["CODE"].ToString();
                    VT_TREATMENT.NAME = query["NAME"].ToString();
                    retValue.Add(VT_TREATMENT);
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
    }
}
