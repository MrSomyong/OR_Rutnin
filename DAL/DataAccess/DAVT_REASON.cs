using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAVT_REASON : DataAccess
    {
        private static string _VT_REASON = "VT_REASON";
        private static string _Code = "Code";
        private static string _Name = "Name";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAVT_REASON() { }
        public DAVT_REASON(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_REASON(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_REASON(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_REASONVO> SearchByKey(VT_REASONVO _VT_REASONVO)
        {
            List<VT_REASONVO> retValue = new List<VT_REASONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_REASON + " where 1=1 ");
                if (!string.IsNullOrEmpty(_VT_REASONVO.Code))
                {
                    strQuery.Append(" and " + _Code + " = @" + _Code);
                }
                if (!string.IsNullOrEmpty(_VT_REASONVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by " + _Code);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_VT_REASONVO.Code)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_VT_REASONVO.Name)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_REASONVO VT_REASONVO = new VT_REASONVO();
                    VT_REASONVO.Code = query[_Code].ToString();
                    VT_REASONVO.Name = query[_Name].ToString();
                    retValue.Add(VT_REASONVO);
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
