using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPORROOMTYPE : DataAccess
    {
        private static string _tblSETUPORROOMTYPE = "SETUPORROOMTYPE";
        private static string _tblSETUPUSERROOMTYPE = "SETUPUSERROOMTYPE";
        private static string _ID = "ID";
        private static string _CodeType = "CodeType";
        private static string _Name = "Name";
        private static string _ProcedureCode = "ProcedureCode";

        private static string _UserID = "UserID";
        private static string _RoomType= "RoomType";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DASETUPORROOMTYPE() { }
        public DASETUPORROOMTYPE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPORROOMTYPE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPORROOMTYPE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPORROOMTYPEVO> SearchByKey(SETUPORROOMTYPEVO _SETUPORROOMTYPEVO)
        {
            List<SETUPORROOMTYPEVO> retValue = new List<SETUPORROOMTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORROOMTYPE);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPORROOMTYPEVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMTYPEVO.CodeType))
                {
                    strQuery.Append(" and " + _CodeType + " = @" + _CodeType);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMTYPEVO.ProcedureCode))
                {
                    strQuery.Append(" and " + _ProcedureCode + " = @" + _ProcedureCode);
                }
                if (!string.IsNullOrEmpty(_SETUPORROOMTYPEVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ID)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ProcedureCode)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.Name)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                    SETUPORROOMTYPEVO.ID = query[_ID].ToString();
                    SETUPORROOMTYPEVO.ProcedureCode = query[_ProcedureCode].ToString();
                    SETUPORROOMTYPEVO.CodeType = query[_CodeType].ToString();
                    SETUPORROOMTYPEVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPORROOMTYPEVO);
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

        internal List<SETUPORROOMTYPEVO> SearchByUser(string xUserID)
        {
            List<SETUPORROOMTYPEVO> retValue = new List<SETUPORROOMTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* from " + _tblSETUPORROOMTYPE + " a");
                strQuery.Append(" left join " + _tblSETUPUSERROOMTYPE + " b on a." + _ID + " = b." + _RoomType);
                strQuery.Append(" where b." + _UserID + " = @" + _UserID);
                strQuery.Append(" order by a." + _ProcedureCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(xUserID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                    SETUPORROOMTYPEVO.ID = query[_ID].ToString();
                    SETUPORROOMTYPEVO.CodeType = query[_CodeType].ToString();
                    SETUPORROOMTYPEVO.Name = query[_Name].ToString();
                    SETUPORROOMTYPEVO.ProcedureCode = query[_ProcedureCode].ToString();
                    retValue.Add(SETUPORROOMTYPEVO);
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

        internal List<SETUPORROOMTYPEVO> CheckDupProcedureCode(SETUPORROOMTYPEVO _SETUPORROOMTYPEVO)
        {
            List<SETUPORROOMTYPEVO> retValue = new List<SETUPORROOMTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORROOMTYPE);
                strQuery.Append(" where 1=1 ");
                strQuery.Append(" and " + _ID + " <> @" + _ID);
                strQuery.Append(" and " + _ProcedureCode + " = @" + _ProcedureCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ID)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ProcedureCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                    SETUPORROOMTYPEVO.ID = query[_ID].ToString();
                    SETUPORROOMTYPEVO.ProcedureCode = query[_ProcedureCode].ToString();
                    SETUPORROOMTYPEVO.CodeType = query[_CodeType].ToString();
                    SETUPORROOMTYPEVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPORROOMTYPEVO);
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

        internal ReturnValue Insert(SETUPORROOMTYPEVO _SETUPORROOMTYPEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPORROOMTYPE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@"+ _ID);

                sbInsert.Append("," + _ProcedureCode);
                sbValue.Append(",@" + _ProcedureCode);

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
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ID)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ProcedureCode)));
                parameter.Add(new IParameter(_CodeType, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.CodeType)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.Name)));

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

        internal ReturnValue Update(SETUPORROOMTYPEVO _SETUPORROOMTYPEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPORROOMTYPE + " SET ");
                sbQuery.Append("" + _ProcedureCode + " = @" + _ProcedureCode);
                sbQuery.Append("," + _Name + " = @" + _Name);
                
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ID)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ProcedureCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.Name)));

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

        internal ReturnValue Delete(SETUPORROOMTYPEVO _SETUPORROOMTYPEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPORROOMTYPE);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPORROOMTYPEVO.ID)));
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
