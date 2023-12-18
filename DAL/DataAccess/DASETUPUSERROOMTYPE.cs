using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPUSERROOMTYPE : DataAccess
    {
        private static string _tblSETUPORROOMTYPE = "SETUPORROOMTYPE";
        private static string _tblSETUPUSERROOMTYPE = "SETUPUSERROOMTYPE";
        private static string _ID = "ID";
        private static string _UserID = "UserID";
        private static string _RoomType = "RoomType"; 
        private static string _RoomTypeName = "RoomTypeName";
        private static string _CodeType = "CodeType";
        private static string _Name = "Name";
        private static string _ProcedureCode = "ProcedureCode";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPUSERROOMTYPE() { }
        public DASETUPUSERROOMTYPE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPUSERROOMTYPE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPUSERROOMTYPE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPUSERROOMTYPEVO> SearchByUser(SETUPUSERROOMTYPEVO _SETUPUSERROOMTYPEVO)
        {
            List<SETUPUSERROOMTYPEVO> retValue = new List<SETUPUSERROOMTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name from " + _tblSETUPUSERROOMTYPE + " a");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a."+_RoomType+" = b."+_ID);
                strQuery.Append(" where a."+_UserID +" = @" +_UserID);
                strQuery.Append(" order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.UserID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
                    SETUPUSERROOMTYPEVO.UserID = query[_UserID].ToString();
                    SETUPUSERROOMTYPEVO.RoomType = query[_RoomType].ToString();
                    SETUPUSERROOMTYPEVO.RoomTypeName = query[_Name].ToString();
                    retValue.Add(SETUPUSERROOMTYPEVO);
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

        internal List<SETUPUSERROOMTYPEVO> CheckDup(SETUPUSERROOMTYPEVO _SETUPUSERROOMTYPEVO)
        {
            List<SETUPUSERROOMTYPEVO> retValue = new List<SETUPUSERROOMTYPEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.Name from " + _tblSETUPUSERROOMTYPE + " a");
                strQuery.Append(" left join " + _tblSETUPORROOMTYPE + " b on a." + _RoomType + " = b." + _ID);
                strQuery.Append(" where a." + _UserID + " = @" + _UserID);
                strQuery.Append(" AND a." + _RoomType + " = @" + _RoomType);
                strQuery.Append(" order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.UserID)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.RoomType)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
                    SETUPUSERROOMTYPEVO.UserID = query[_UserID].ToString();
                    SETUPUSERROOMTYPEVO.RoomType = query[_RoomType].ToString();
                    SETUPUSERROOMTYPEVO.RoomTypeName = query[_Name].ToString();
                    retValue.Add(SETUPUSERROOMTYPEVO);
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

        internal ReturnValue Insert(SETUPUSERROOMTYPEVO _SETUPUSERROOMTYPEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPUSERROOMTYPE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_UserID);
                sbValue.Append("@" + _UserID);

                sbInsert.Append("," + _RoomType);
                sbValue.Append(",@" + _RoomType);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.UserID)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.RoomType)));

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

        internal ReturnValue Delete(SETUPUSERROOMTYPEVO _SETUPUSERROOMTYPEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPUSERROOMTYPE);
                sbQuery.Append(" WHERE " + _UserID + " = @" + _UserID);
                sbQuery.Append(" AND " + _RoomType + " = @" + _RoomType);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.UserID)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_SETUPUSERROOMTYPEVO.RoomType)));
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
