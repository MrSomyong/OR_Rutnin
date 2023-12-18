using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPORGANSUB : DataAccess
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
        public DASETUPORGANSUB() { }
        public DASETUPORGANSUB(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPORGANSUB(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPORGANSUB(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPORGANSUBVO> SearchAll()
        {
            List<SETUPORGANSUBVO> retValue = new List<SETUPORGANSUBVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPORGANSUB);
                strQuery.Append(" Order by " + _SubName);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                    SETUPORGANSUBVO.MainCode = query[_MainCode].ToString();
                    SETUPORGANSUBVO.SubCode = query[_SubCode].ToString();
                    SETUPORGANSUBVO.SubName = query[_SubName].ToString();
                    SETUPORGANSUBVO.SubRemark = query[_SubRemark].ToString();

                    retValue.Add(SETUPORGANSUBVO);
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

        internal List<SETUPORGANSUBVO> SearchByKey(SETUPORGANSUBVO _SETUPORGANSUBVO)
        {
            List<SETUPORGANSUBVO> retValue = new List<SETUPORGANSUBVO>();
            try
            {

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Name + " from " + _tblSETUPORGANSUB + " as a");
                strQuery.Append(" left join " + _tblSETUPORGANMAIN + " as b on a." + _MainCode + " = b." + _MainCode);
                strQuery.Append(" where 1 = 1");
                if (!string.IsNullOrEmpty(_SETUPORGANSUBVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_SETUPORGANSUBVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                strQuery.Append(" Order by a." + _SubName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                    SETUPORGANSUBVO.MainCode = query[_MainCode].ToString();
                    SETUPORGANSUBVO.Name = query[_Name].ToString();
                    SETUPORGANSUBVO.SubCode = query[_SubCode].ToString();
                    SETUPORGANSUBVO.SubName = query[_SubName].ToString();
                    SETUPORGANSUBVO.SubRemark = query[_SubRemark].ToString();

                    retValue.Add(SETUPORGANSUBVO);
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

        internal DataSet SearchByKey_DS(string MainCode, string SubCode)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Name + " from " + _tblSETUPORGANSUB + " as a");
                strQuery.Append(" left join " + _tblSETUPORGANMAIN + " as b on a." + _MainCode + " = b." + _MainCode);
                strQuery.Append(" where 1 = 1");
                if (!string.IsNullOrEmpty(MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                strQuery.Append(" Order by a." + _SubName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(SubCode)));

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

        internal ReturnValue Insert(SETUPORGANSUBVO _SETUPORGANSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPORGANSUB + " (");
                sbValue.Append(" VALUES(");


                sbInsert.Append( _MainCode);
                sbValue.Append("@" + _MainCode);

                sbInsert.Append("," + _SubCode);
                sbValue.Append(",@" + _SubCode);

                sbInsert.Append("," + _SubName);
                sbValue.Append(",@" + _SubName);

                sbInsert.Append("," + _SubRemark);
                sbValue.Append(",@" + _SubRemark);
                
                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubName)));
                parameter.Add(new IParameter(_SubRemark, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubRemark)));

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

        internal ReturnValue Update(SETUPORGANSUBVO _SETUPORGANSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPORGANSUB + " SET ");

                sbQuery.Append("" + _SubName + " = @" + _SubName);
                sbQuery.Append("," + _SubRemark + " = @" + _SubRemark);

                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);
                sbQuery.Append(" and " + _SubCode + " = @" + _SubCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubName)));
                parameter.Add(new IParameter(_SubRemark, IDbType.VarChar, DBNullConvert.From(_SETUPORGANSUBVO.SubRemark)));

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

        internal ReturnValue DeleteByMain(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPORGANSUB);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(SETUPORGANSUBVO.MainCode)));
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

        internal ReturnValue Delete(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPORGANSUB);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);
                sbQuery.Append(" and " + _SubCode + " = @" + _SubCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(SETUPORGANSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(SETUPORGANSUBVO.SubCode)));
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
