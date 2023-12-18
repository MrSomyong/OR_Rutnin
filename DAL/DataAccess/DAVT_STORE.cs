using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_STORE : DataAccess
    {
        public DAVT_STORE() { }
        public DAVT_STORE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_STORE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_STORE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_STORE> SearchAll()
        {
            List<VT_STORE> retValue = new List<VT_STORE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from VT_STORE");
                strQuery.Append(" Order by StoreName");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_STORE store = new VT_STORE();
                    store.StoreCode = query["StoreCode"].ToString();
                    store.StoreName = query["StoreName"].ToString();
                    store.ChargeCode = query["ChargeCode"].ToString();
                    retValue.Add(store);
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

        internal VT_STORE GetStoreByKey(string storeCode)
        {
            VT_STORE VT_STORE = new VT_STORE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_STORE ");
                strQuery.Append(" where StoreCode = @StoreCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("StoreCode", IDbType.VarChar, DBNullConvert.From(storeCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_STORE.StoreCode = query["StoreCode"].ToString();
                    VT_STORE.StoreName = query["StoreName"].ToString();
                    VT_STORE.ChargeCode = query["ChargeCode"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_STORE;
        }
    }
}
