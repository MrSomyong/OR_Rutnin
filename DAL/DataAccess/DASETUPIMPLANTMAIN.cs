using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPIMPLANTMAIN : DataAccess
    {
        private static string _tblSETUPIMPLANTMAIN = "SETUPIMPLANTMAIN";
        private static string _tblSETUPIMPLANTSUB = "SETUPIMPLANTSUB";

        private static string _SubCode = "SubCode";
        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _Remark = "Remark";
        private static string _SubName = "SubName";
        private static string _SubRemark = "SubRemark";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPIMPLANTMAIN() { }
        public DASETUPIMPLANTMAIN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPIMPLANTMAIN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPIMPLANTMAIN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPIMPLANTMAINVO> SearchAll()
        {
            List<SETUPIMPLANTMAINVO> retValue = new List<SETUPIMPLANTMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPIMPLANTMAIN);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO = new SETUPIMPLANTMAINVO();
                    SETUPIMPLANTMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPIMPLANTMAINVO.Name = query[_Name].ToString();
                    SETUPIMPLANTMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPIMPLANTMAINVO);
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

        internal List<SETUPIMPLANTMAINVO> SearchByKey(SETUPIMPLANTMAINVO _SETUPIMPLANTMAINVO)
        {
            List<SETUPIMPLANTMAINVO> retValue = new List<SETUPIMPLANTMAINVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPIMPLANTMAIN);
                strQuery.Append(" where 1 = 1 ");
                if (!string.IsNullOrEmpty(_SETUPIMPLANTMAINVO.MainCode))
                {
                    strQuery.Append(" and " + _MainCode + " = @" + _MainCode);
                }
                strQuery.Append(" Order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO = new SETUPIMPLANTMAINVO();
                    SETUPIMPLANTMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPIMPLANTMAINVO.Name = query[_Name].ToString();
                    SETUPIMPLANTMAINVO.Remark = query[_Remark].ToString();

                    retValue.Add(SETUPIMPLANTMAINVO);
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

        internal SETUPIMPLANTMAINVO SearchByCode(string MainCode)
        {
            SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO = new SETUPIMPLANTMAINVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPIMPLANTMAIN);
                strQuery.Append(" where " + _MainCode + " = @" + _MainCode);
                strQuery.Append(" Order by " + _MainCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(MainCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {

                    SETUPIMPLANTMAINVO.MainCode = query[_MainCode].ToString();
                    SETUPIMPLANTMAINVO.Name = query[_Name].ToString();
                    SETUPIMPLANTMAINVO.Remark = query[_Remark].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPIMPLANTMAINVO;
        }

        internal DataSet SearchByCode_DS(string MainCode)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPIMPLANTMAIN);
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

        internal ReturnValue Insert(SETUPIMPLANTMAINVO _SETUPIMPLANTMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPIMPLANTMAIN + " (");
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

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.Remark)));

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

        internal ReturnValue Update(SETUPIMPLANTMAINVO _SETUPIMPLANTMAINVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPIMPLANTMAIN + " SET ");

                sbQuery.Append("" + _Name + " = @" + _Name);
                sbQuery.Append("," + _Remark + " = @" + _Remark);

                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.MainCode)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.Name)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPIMPLANTMAINVO.Remark)));

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
                sbQuery.Append("DELETE FROM " + _tblSETUPIMPLANTMAIN);
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
