using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPORROOM : DataAccess
    {
        private static string _tblSETUPORROOM = "SETUPORROOM";
        private static string _tblSETUPORROOMTYPE = "SETUPORROOMTYPE";
        private static string _ID = "ID";
        private static string _CODE = "CODE";
        private static string _CodeType = "CodeType";
        private static string _Name = "Name";
        private static string _CodeTypeName = "CodeTypeName";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPORROOM() { }
        public DASETUPORROOM(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPORROOM(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPORROOM(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPORROOMVO> SearchByKey(SETUPORROOMVO _SETUPORROOMVO)
        {
            List<SETUPORROOMVO> retValue = new List<SETUPORROOMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name as " + _CodeTypeName + " from " + _tblSETUPORROOM + " a ");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a." + _CodeType + " = b." + _ID);
                strQuery.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(_SETUPORROOMVO.ArCodeType))
                {
                    strQuery.Append(" and a." + _CodeType + " in (" + _SETUPORROOMVO.ArCodeType + ")");
                }

                if (!string.IsNullOrEmpty(_SETUPORROOMVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.CODE))
                {
                    strQuery.Append(" and a." + _CODE + " = @" + _CODE);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.CodeType))
                {
                    strQuery.Append(" and a." + _CodeType + " = @" + _CodeType);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.Name))
                {
                    strQuery.Append(" and a." + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by a." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.ID)));
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CODE)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.Name)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.ID = query[_ID].ToString();
                    SETUPORROOMVO.CODE = query[_CODE].ToString();
                    SETUPORROOMVO.CodeType = query[_CodeType].ToString();
                    SETUPORROOMVO.Name = query[_Name].ToString();
                    SETUPORROOMVO.CodeTypeName = query[_CodeTypeName].ToString();
                    retValue.Add(SETUPORROOMVO);
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

        internal List<SETUPORROOMVO> SearchByUser(SETUPORROOMVO _SETUPORROOMVO)
        {
            List<SETUPORROOMVO> retValue = new List<SETUPORROOMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name as " + _CodeTypeName + " from " + _tblSETUPORROOM + " a ");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a." + _CodeType + " = b." + _ID);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.CODE))
                {
                    strQuery.Append(" and a." + _CODE + " = @" + _CODE);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.CodeType))
                {
                    strQuery.Append(" and a." + _CodeType + " = @" + _CodeType);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMVO.Name))
                {
                    strQuery.Append(" and a." + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by a." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.ID)));
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CODE)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.Name)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.ID = query[_ID].ToString();
                    SETUPORROOMVO.CODE = query[_CODE].ToString();
                    SETUPORROOMVO.CodeType = query[_CodeType].ToString();
                    SETUPORROOMVO.Name = query[_Name].ToString();
                    SETUPORROOMVO.CodeTypeName = query[_CodeTypeName].ToString();
                    retValue.Add(SETUPORROOMVO);
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

        internal ReturnValue Insert(SETUPORROOMVO _SETUPORROOMVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPORROOM + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _CODE);
                sbValue.Append(",@" + _CODE);

                sbInsert.Append("," + _CodeType);
                sbValue.Append(",@" + _CodeType);

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.ID)));
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CODE)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.Name)));

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

        internal ReturnValue Update(SETUPORROOMVO _SETUPORROOMVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPORROOM + " SET ");
                sbQuery.Append("" + _CodeType + " = @" + _CodeType);
                sbQuery.Append("," + _Name + " = @" + _Name);

                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.ID)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.Name)));

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

        internal ReturnValue Delete(SETUPORROOMVO _SETUPORROOMVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPORROOM);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMVO.ID)));
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
