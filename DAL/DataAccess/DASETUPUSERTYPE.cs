using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPUSERTYPE : DataAccess
    {
        private static string _tblUSERTYPE = "USERTYPE";
        private static string _ID = "ID";
        private static string _Description = "Description";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPUSERTYPE() { }
        public DASETUPUSERTYPE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPUSERTYPE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPUSERTYPE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPUSERTYPEVO> SearchByKey(SETUPUSERTYPEVO _SETUPUSERTYPEVO)
        {
            List<SETUPUSERTYPEVO> retValue = new List<SETUPUSERTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblUSERTYPE + " where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPUSERTYPEVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPUSERTYPEVO.Description))
                {
                    strQuery.Append(" and " + _Description + " = @" + _Description);
                }
                strQuery.Append(" order by " + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPUSERTYPEVO.ID)));
                parameter.Add(new IParameter(_Description, IDbType.VarChar, DBNullConvert.From(_SETUPUSERTYPEVO.Description)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPUSERTYPEVO SETUPUSERTYPEVO = new SETUPUSERTYPEVO();
                    SETUPUSERTYPEVO.ID = query[_ID].ToString();
                    SETUPUSERTYPEVO.Description = query[_Description].ToString();
                    retValue.Add(SETUPUSERTYPEVO);
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
