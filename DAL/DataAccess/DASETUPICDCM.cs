using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPICDCM : DataAccess
    {
        private static string _tblDASETUPICDCM = "SETUPICDCM";
        private static string _Code = "Code";
        private static string _Name = "Name";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPICDCM() { }
        public DASETUPICDCM(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPICDCM(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPICDCM(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPICDCMVO> SearchByKey(SETUPICDCMVO _SETUPICDCMVO)
        {
            List<SETUPICDCMVO> retValue = new List<SETUPICDCMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICDCM);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPICDCMVO.Code))
                {
                    strQuery.Append(" and " + _Code + " = @" + _Code);
                }
                if (!string.IsNullOrEmpty(_SETUPICDCMVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_SETUPICDCMVO.Code)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPICDCMVO.Name)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
                    SETUPICDCMVO.Code = query[_Code].ToString();
                    SETUPICDCMVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPICDCMVO);
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

        internal List<SETUPICDCMVO> SearchByCode(SETUPICDCMVO _SETUPICDCMVO)
        {
            List<SETUPICDCMVO> retValue = new List<SETUPICDCMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICDCM);
                strQuery.Append(" where 1=1 ");
                strQuery.Append(" and " + _Code + " = @" + _Code);
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_SETUPICDCMVO.Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
                    SETUPICDCMVO.Code = query[_Code].ToString();
                    SETUPICDCMVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPICDCMVO);
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

        internal List<SETUPICDCMVO> SearchLikeByKey(SETUPICDCMVO _SETUPICDCMVO)
        {
            List<SETUPICDCMVO> retValue = new List<SETUPICDCMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICDCM);
                strQuery.Append(" where ");
                strQuery.Append(_Code + " like @" + _Code);
                strQuery.Append(" or " + _Name + " like @" + _Name);
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From("%" + _SETUPICDCMVO.Code + "%")));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From("%" + _SETUPICDCMVO.Name + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
                    SETUPICDCMVO.Code = query[_Code].ToString();
                    SETUPICDCMVO.Name = query[_Name].ToString();
                    SETUPICDCMVO.CodeName = query[_Code].ToString() +" : "+ query[_Name].ToString();
                    retValue.Add(SETUPICDCMVO);
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


        internal List<SETUPICDCMVO> SearchByCode(string Code)
        {
            List<SETUPICDCMVO> retValue = new List<SETUPICDCMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICDCM);
                strQuery.Append(" where 1=1 ");
                strQuery.Append(" and " + _Code + " = @" + _Code);
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
                    SETUPICDCMVO.Code = query[_Code].ToString();
                    SETUPICDCMVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPICDCMVO);
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

        internal ReturnValue Insert(SETUPICDCMVO _SETUPICDCMVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblDASETUPICDCM + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_Code);
                sbValue.Append("@" + _Code);

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_SETUPICDCMVO.Code)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPICDCMVO.Name)));

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
    }
}
