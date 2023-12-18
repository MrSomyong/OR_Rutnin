using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPORGANMAIN : DataAccess
    {
        private static string _tblSETUPORGANMAIN = "SETUPORGANMAIN";
        private static string _tblSETUPORGANSUB = "SETUPORGANSUB";

        private static string _SubCode = "SubCode";
        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _Remark = "Remark";
        private static string _SubName = "SubName";
        private static string _SubRemark = "SubRemark";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPORGANMAIN() { }
        public DASETUPORGANMAIN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPORGANMAIN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPORGANMAIN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPORGANMAINVO> SearchAll()
        {
            List<SETUPORGANMAINVO> retValue = new List<SETUPORGANMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORGANMAIN);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
                    SETUPORGANMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPORGANMAINVO.Name = query[_Name].ToString();
                    SETUPORGANMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPORGANMAINVO);
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

        internal List<SETUPORGANMAINVO> SearchByKey(SETUPORGANMAINVO _SETUPORGANMAINVO)
        {
            List<SETUPORGANMAINVO> retValue = new List<SETUPORGANMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORGANMAIN);
                strQuery.Append(" where 1 = 1 ");
                if (!string.IsNullOrEmpty(_SETUPORGANMAINVO.MainCode))
                {
                    strQuery.Append(" and " + _MainCode + " = @" + _MainCode);
                }
                strQuery.Append(" Order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
                    SETUPORGANMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPORGANMAINVO.Name = query[_Name].ToString();
                    SETUPORGANMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPORGANMAINVO);
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

        internal SETUPORGANMAINVO SearchByCode(string MainCode)
        {
            SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORGANMAIN);
                strQuery.Append(" where " + _MainCode + " = @" + _MainCode);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {

                    SETUPORGANMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPORGANMAINVO.Name = query[_Name].ToString();
                    SETUPORGANMAINVO.Remark = query[_Remark].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPORGANMAINVO;
        }

        internal DataSet SearchByCode_DS(string MainCode)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORGANMAIN);
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

        internal ReturnValue Insert(SETUPORGANMAINVO _SETUPORGANMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPORGANMAIN + " (");
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

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.Remark)));

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

        internal ReturnValue Update(SETUPORGANMAINVO _SETUPORGANMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPORGANMAIN + " SET ");

                sbQuery.Append("" + _Name + " = @" + _Name);
                sbQuery.Append("," + _Remark + " = @" + _Remark);

                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPORGANMAINVO.Remark)));

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
                sbQuery.Append("DELETE FROM " + _tblSETUPORGANMAIN);
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
