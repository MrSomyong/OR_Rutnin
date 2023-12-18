using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPICD : DataAccess
    {
        private static string _tblDASETUPICD = "SETUPICD";
        private static string _Code = "Code";
        private static string _Name = "Name";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPICD() { }
        public DASETUPICD(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPICD(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPICD(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPICDVO> SearchByKey(SETUPICDVO _SETUPICDVO)
        {
            List<SETUPICDVO> retValue = new List<SETUPICDVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICD);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPICDVO.Code))
                {
                    strQuery.Append(" and " + _Code + " = @" + _Code);
                }
                if (!string.IsNullOrEmpty(_SETUPICDVO.Name))
                {
                    strQuery.Append(" and " + _Name + " = @" + _Name);
                }
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_SETUPICDVO.Code)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPICDVO.Name)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDVO SETUPICDVO = new SETUPICDVO();
                    SETUPICDVO.Code = query[_Code].ToString();
                    SETUPICDVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPICDVO);
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

        internal List<SETUPICDVO> SearchLikeByKey(SETUPICDVO _SETUPICDVO)
        {
            List<SETUPICDVO> retValue = new List<SETUPICDVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICD);
                strQuery.Append(" where ");
                strQuery.Append( _Code + " Like @" + _Code);
                strQuery.Append(" or " + _Name + " Like @" + _Name);
                
                strQuery.Append(" order by " + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From("%" + _SETUPICDVO.Code + "%")));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From("%" + _SETUPICDVO.Name + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPICDVO SETUPICDVO = new SETUPICDVO();
                    SETUPICDVO.Code = query[_Code].ToString();
                    SETUPICDVO.Name = query[_Name].ToString();
                    SETUPICDVO.CodeName = query[_Code].ToString() +" : "+ query[_Name].ToString();
                    retValue.Add(SETUPICDVO);
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

        internal List<SETUPICDVO> SearchByCode(string Code)
        {
            List<SETUPICDVO> retValue = new List<SETUPICDVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDASETUPICD);
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
                    SETUPICDVO SETUPICDVO = new SETUPICDVO();
                    SETUPICDVO.Code = query[_Code].ToString();
                    SETUPICDVO.Name = query[_Name].ToString();
                    retValue.Add(SETUPICDVO);
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

        internal ReturnValue Insert(SETUPICDVO _SETUPICDVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblDASETUPICD + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_Code);
                sbValue.Append("@"+ _Code);

                sbInsert.Append("," + _Name);
                sbValue.Append(",@" + _Name);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Code, IDbType.VarChar, DBNullConvert.From(_SETUPICDVO.Code)));
                parameter.Add(new IParameter(_Name, IDbType.VarChar, DBNullConvert.From(_SETUPICDVO.Name)));

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
