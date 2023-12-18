using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPORGAN : DataAccess
    {
        private static string _tblDASETUPORGAN = "SETUPORGAN";
        private static string _ID = "ID";
        private static string _Name = "Name";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPORGAN() { }
        public DASETUPORGAN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPORGAN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPORGAN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPORGANVO> SearchByKey(SETUPORGANVO _SETUPORGANVO)
        {
            List<SETUPORGANVO> retValue = new List<SETUPORGANVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPORGAN);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPORGANVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPORGANVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORGANVO.ID)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORGANVO.Name)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORGANVO SETUPORGANVO = new SETUPORGANVO();
                    SETUPORGANVO.ID = query[_ID].ToString();
                    SETUPORGANVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPORGANVO);
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

        internal ReturnValue Insert(SETUPORGANVO _SETUPORGANVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblDASETUPORGAN + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("NEWID()");

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORGANVO.ID)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORGANVO.Name)));

                command = GetCommand(sbInsert.ToString(), parameter);

                effected = GetExecuteNonQuery(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }

            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }
    }
}
