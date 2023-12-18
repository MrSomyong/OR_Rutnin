using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_DOSEQTY : DataAccess
    {
        public DAVT_DOSEQTY() { }
        public DAVT_DOSEQTY(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_DOSEQTY(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_DOSEQTY(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOSEQTY> SearchAll()
        {
            List<DOSEQTY> retValue = new List<DOSEQTY>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select Code, Name from VT_DOSEQTY");
                strQuery.Append(" Order by Name ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOSEQTY DOSEQTY = new DOSEQTY();
                    DOSEQTY.Code = query["Code"].ToString();
                    DOSEQTY.Name = query["Name"].ToString();
                    retValue.Add(DOSEQTY);
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

        internal DOSEQTY GetDoseQtyByKey(string code)
        {
            DOSEQTY VT_DOSEQTY = new DOSEQTY();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_DOSEQTY ");
                strQuery.Append(" where Code = @Code");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("Code", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_DOSEQTY.Code = query["Code"].ToString();
                    VT_DOSEQTY.Name = query["Name"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_DOSEQTY;
        }

    }
}
