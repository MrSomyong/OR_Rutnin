using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPPRINTER : DataAccess
    {

        private static string _tblSETUPPRINTER = "SETUPPRINTER";


        private static string _ID = "ID";
        private static string _Name = "Name";
        private static string _Path = "Path";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPPRINTER() { }
        public DASETUPPRINTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPPRINTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPPRINTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPPRINTERVO> SearchAll()
        {
            List<SETUPPRINTERVO> retValue = new List<SETUPPRINTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPPRINTER);
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPPRINTERVO SETUPPRINTERVO = new SETUPPRINTERVO();
                    SETUPPRINTERVO.ID = query[_ID].ToString();
                    SETUPPRINTERVO.Name = query[_Name].ToString();
                    SETUPPRINTERVO.Path = query[_Path].ToString();

                    retValue.Add(SETUPPRINTERVO);
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
        internal List<SETUPPRINTERVO> SearchByKey(SETUPPRINTERVO _SETUPPRINTERVO)
        {
            List<SETUPPRINTERVO> LstSETUPPRINTERVO = new List<SETUPPRINTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPPRINTER);
                strQuery.Append(" where 1 = 1");
                if (!string.IsNullOrEmpty(_SETUPPRINTERVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_SETUPPRINTERVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                if (!string.IsNullOrEmpty(_SETUPPRINTERVO.Path))
                {
                    strQuery.Append(" and " + _Path + " = @" + _Path);
                }
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.ID)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Name)));
                parameter.Add(new IParameter(_Path, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Path)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPPRINTERVO SETUPPRINTERVO = new SETUPPRINTERVO();
                    SETUPPRINTERVO.ID = query[_ID].ToString();
                    SETUPPRINTERVO.Name = query[_Name].ToString();
                    SETUPPRINTERVO.Path = query[_Path].ToString();
                    LstSETUPPRINTERVO.Add(SETUPPRINTERVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return LstSETUPPRINTERVO;
        }

        internal ReturnValue Insert(SETUPPRINTERVO _SETUPPRINTERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPPRINTER + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("NEWID()");

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append("," + _Path);
                sbValue.Append(",@" + _Path);               

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.ID)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Name)));
                parameter.Add(new IParameter(_Path, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Path)));

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

        internal ReturnValue Update(SETUPPRINTERVO _SETUPPRINTERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPPRINTER + " SET ");
                sbQuery.Append("" + _Name + " = @" + _Name);
                sbQuery.Append("," + _Path + " = @" + _Path);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.ID)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Name)));
                parameter.Add(new IParameter(_Path, IDbType.VarChar, DBNullConvert.From(_SETUPPRINTERVO.Path)));

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

        internal ReturnValue Delete(string id)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPPRINTER);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(id)));
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
