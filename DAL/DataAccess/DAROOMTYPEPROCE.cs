﻿using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAROOMTYPEPROCE : DataAccess
    {
        private static string _tblSETUPORROOMTYPE = "SETUPORROOMTYPE";
        private static string _tblROOMTYPEPROCE = "ROOMTYPEPROCE";
        private static string _ID = "ID";
        private static string _RoomType = "RoomType"; 
        private static string _RoomTypeName = "RoomTypeName";
        private static string _CodeType = "CodeType";
        private static string _Name = "Name";
        private static string _ProcedureCode = "ProcedureCode";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAROOMTYPEPROCE() { }
        public DAROOMTYPEPROCE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAROOMTYPEPROCE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAROOMTYPEPROCE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ROOMTYPEPROCEVO> SearchByUser(ROOMTYPEPROCEVO _ROOMTYPEPROCEVO)
        {
            List<ROOMTYPEPROCEVO> retValue = new List<ROOMTYPEPROCEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name from " + _tblROOMTYPEPROCE + " a");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a."+_RoomType+" = b."+_ID);
                strQuery.Append(" where a."+ _RoomType + " = @" + _RoomType);
                strQuery.Append(" order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.RoomType)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
                    ROOMTYPEPROCEVO.ProcedureCode = query[_ProcedureCode].ToString();
                    ROOMTYPEPROCEVO.RoomType = query[_RoomType].ToString();
                    ROOMTYPEPROCEVO.RoomTypeName = query[_Name].ToString();
                    retValue.Add(ROOMTYPEPROCEVO);
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

        internal List<ROOMTYPEPROCEVO> CheckDup(ROOMTYPEPROCEVO _ROOMTYPEPROCEVO)
        {
            List<ROOMTYPEPROCEVO> retValue = new List<ROOMTYPEPROCEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name from " + _tblROOMTYPEPROCE + " a");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a." + _RoomType + " = b." + _ID);
                strQuery.Append(" where a." + _RoomType + " = @" + _RoomType);
                strQuery.Append(" AND a." + _ProcedureCode + " = @" + _ProcedureCode);
                strQuery.Append(" order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.RoomType)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.ProcedureCode)));
                
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
                    ROOMTYPEPROCEVO.ProcedureCode = query[_ProcedureCode].ToString();
                    ROOMTYPEPROCEVO.RoomType = query[_RoomType].ToString();
                    ROOMTYPEPROCEVO.RoomTypeName = query[_Name].ToString();
                    retValue.Add(ROOMTYPEPROCEVO);
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

        internal ReturnValue Insert(ROOMTYPEPROCEVO _ROOMTYPEPROCEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblROOMTYPEPROCE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_RoomType);
                sbValue.Append("@" + _RoomType);

                sbInsert.Append("," + _ProcedureCode);
                sbValue.Append(",@" + _ProcedureCode);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.RoomType)));
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.ProcedureCode)));

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

        internal ReturnValue Delete(ROOMTYPEPROCEVO _ROOMTYPEPROCEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblROOMTYPEPROCE);
                sbQuery.Append(" WHERE " + _ProcedureCode + " = @" + _ProcedureCode);
                sbQuery.Append(" AND " + _RoomType + " = @" + _RoomType);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.ProcedureCode)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ROOMTYPEPROCEVO.RoomType)));
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