using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPOPERATIONMAIN : DataAccess
    {
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";

        private static string _SubCode = "SubCode";
        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _Remark = "Remark";
        private static string _SubName = "SubName";
        private static string _SubRemark = "SubRemark";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPOPERATIONMAIN() { }
        public DASETUPOPERATIONMAIN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPOPERATIONMAIN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPOPERATIONMAIN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPOPERATIONMAINVO> SearchAll()
        {
            List<SETUPOPERATIONMAINVO> retValue = new List<SETUPOPERATIONMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPOPERATIONMAIN);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new SETUPOPERATIONMAINVO();
                    SETUPOPERATIONMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPOPERATIONMAINVO.Name = query[_Name].ToString();
                    SETUPOPERATIONMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPOPERATIONMAINVO);
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

        internal List<SETUPOPERATIONMAINVO> SearchByKey(SETUPOPERATIONMAINVO _SETUPOPERATIONMAINVO)
        {
            List<SETUPOPERATIONMAINVO> retValue = new List<SETUPOPERATIONMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPOPERATIONMAIN);
                strQuery.Append(" where 1 = 1 ");
                if (!string.IsNullOrEmpty(_SETUPOPERATIONMAINVO.MainCode))
                {
                    strQuery.Append(" and " + _MainCode + " = @" + _MainCode);
                }
                strQuery.Append(" Order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new SETUPOPERATIONMAINVO();
                    SETUPOPERATIONMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPOPERATIONMAINVO.Name = query[_Name].ToString();
                    SETUPOPERATIONMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPOPERATIONMAINVO);
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

        internal SETUPOPERATIONMAINVO SearchByCode(string MainCode)
        {
            SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new SETUPOPERATIONMAINVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPOPERATIONMAIN);
                strQuery.Append(" where " + _MainCode + " = @" + _MainCode);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {

                    SETUPOPERATIONMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPOPERATIONMAINVO.Name = query[_Name].ToString();
                    SETUPOPERATIONMAINVO.Remark = query[_Remark].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPOPERATIONMAINVO;
        }

        internal DataSet SearchByCode_DS(string MainCode)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPOPERATIONMAIN);
                strQuery.Append(" where " + _MainCode + " = @" + _MainCode);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                table.Load(GetExecuteReader(command));
                ds.Tables.Add(table);
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return ds;
        }

        internal ReturnValue Insert(SETUPOPERATIONMAINVO _SETUPOPERATIONMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPOPERATIONMAIN + " (");
                sbValue.Append(" VALUES(");


                sbInsert.Append( _MainCode);
                sbValue.Append("@" + _MainCode);

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);
                
                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.Remark)));

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

        internal ReturnValue Update(SETUPOPERATIONMAINVO _SETUPOPERATIONMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPOPERATIONMAIN + " SET ");

                sbQuery.Append("" + _Name + " = @" + _Name);
                sbQuery.Append("," + _Remark + " = @" + _Remark);

                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONMAINVO.Remark)));

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

        internal ReturnValue Delete(string MainCode)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPOPERATIONMAIN);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));
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
