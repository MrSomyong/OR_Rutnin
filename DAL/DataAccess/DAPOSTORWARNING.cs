using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORWARNING : DataAccess
    {
        private static string _tblPOSTORWARNING = "POSTORWARNING";
        private static string _ORID = "ORID";
        private static string _ID = "ID";
        private static string _Warning = "Warning";
        private static string _CreateDateTime = "CreateDateTime";
        private static string _UpdateDateTime = "UpdateDateTime";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORWARNING() { }
        public DAPOSTORWARNING(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORWARNING(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORWARNING(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORWARNINGVO> SearchByKey(POSTORWARNINGVO _POSTORWARNINGVO)
        {
            List<POSTORWARNINGVO> retValue = new List<POSTORWARNINGVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPOSTORWARNING + " where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORWARNINGVO.ORID))
                {
                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
                }
                if (!string.IsNullOrEmpty(_POSTORWARNINGVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                strQuery.Append(" order by " + _CreateDateTime + " DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORWARNINGVO POSTORWARNINGVO = new POSTORWARNINGVO();
                    POSTORWARNINGVO.ORID = query[_ORID].ToString();
                    POSTORWARNINGVO.ID = query[_ID].ToString();
                    POSTORWARNINGVO.Warning = query[_Warning].ToString();
                    POSTORWARNINGVO.CreateDateTime = ADOUtil.GetDateFromQuery(query[_CreateDateTime].ToString());
                    POSTORWARNINGVO.UpdateDateTime = ADOUtil.GetDateFromQuery(query[_UpdateDateTime].ToString());
                    retValue.Add(POSTORWARNINGVO);
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

        internal List<POSTORWARNINGVO> SearchByPrimary(string ORID, string ID)
        {
            List<POSTORWARNINGVO> retValue = new List<POSTORWARNINGVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPOSTORWARNING + " where 1=1 ");
                strQuery.Append(" and " + _ORID + " = @" + _ORID);
                strQuery.Append(" and " + _ID + " = @" + _ID);
                strQuery.Append(" order by " + _CreateDateTime + " DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORWARNINGVO POSTORWARNINGVO = new POSTORWARNINGVO();
                    POSTORWARNINGVO.ORID = query[_ORID].ToString();
                    POSTORWARNINGVO.ID = query[_ID].ToString();
                    POSTORWARNINGVO.Warning = query[_Warning].ToString();
                    POSTORWARNINGVO.CreateDateTime = ADOUtil.GetDateFromQuery(query[_CreateDateTime].ToString());
                    POSTORWARNINGVO.UpdateDateTime = ADOUtil.GetDateFromQuery(query[_UpdateDateTime].ToString());
                    retValue.Add(POSTORWARNINGVO);
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

        internal ReturnValue Insert(POSTORWARNINGVO _POSTORWARNINGVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORWARNING + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ORID);
                sbValue.Append("@" + _ORID);

                sbInsert.Append("," + _ID);
                sbValue.Append(",@" + _ID);

                sbInsert.Append("," + _Warning);
                sbValue.Append(",@" + _Warning);

                sbInsert.Append("," + _CreateDateTime);
                sbValue.Append(", GETDATE()");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ID)));
                parameter.Add(new IParameter(_Warning, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.Warning)));
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

        internal ReturnValue Update(POSTORWARNINGVO _POSTORWARNINGVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORWARNING + " SET ");
                sbQuery.Append("" + _Warning + " = @" + _Warning);
                sbQuery.Append("," + _UpdateDateTime + " = GETDATE()");
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID + " AND " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.ID)));
                parameter.Add(new IParameter(_Warning, IDbType.VarChar, DBNullConvert.From(_POSTORWARNINGVO.Warning)));

                command = GetCommand(sbQuery.ToString(), parameter);
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

        internal ReturnValue Delete(string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORWARNING);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(sbQuery.ToString(), parameter);
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

        internal ReturnValue Delete(string ORID, string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORWARNING);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);
                sbQuery.Append(" AND " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(sbQuery.ToString(), parameter);
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
