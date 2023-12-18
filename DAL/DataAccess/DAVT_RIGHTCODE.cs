using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_RIGHTCODE : DataAccess
    {
        public DAVT_RIGHTCODE() { }
        public DAVT_RIGHTCODE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_RIGHTCODE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_RIGHTCODE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal VT_RIGHTCODE GetRightCodeByKey(string code)
        {
            VT_RIGHTCODE VT_RIGHTCODE = new VT_RIGHTCODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_RIGHTCODE ");
                strQuery.Append(" where CODE = @CODE");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("CODE", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_RIGHTCODE.CODE = query["CODE"].ToString();
                    VT_RIGHTCODE.NAME = query["NAME"].ToString();
                    VT_RIGHTCODE.FixRate = Convert.ToInt32(query["FixRate"]);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_RIGHTCODE;
        }
    }
}
