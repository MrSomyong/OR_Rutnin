using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAHNUnderly : DataAccess
    {
        private static string _tblDAHNUnderly = "HNUnderly";

        private static string _HN = "HN";
        private static string _Underlyingtext = "Underlyingtext";
        private static string _CreateDateTime = "CreateDateTime";
        private static string _CreateUser = "CreateUser";
        private static string _UpdateDateTime = "UpdateDateTime";
        private static string _UpdateUser = "UpdateUser";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAHNUnderly() { }
        public DAHNUnderly(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAHNUnderly(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAHNUnderly(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<HNUnderlyVO> SearchByHN(HNUnderlyVO _HNUnderlyVO)
        {
            List<HNUnderlyVO> retValue = new List<HNUnderlyVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDAHNUnderly);
                strQuery.Append(" where 1 = 1 ");
                strQuery.Append(" and " + _HN + " = @" + _HN);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.HN)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    HNUnderlyVO HNUnderlyVO = new HNUnderlyVO();
                    HNUnderlyVO.HN = query[_HN].ToString();
                    HNUnderlyVO.Underlyingtext = query[_Underlyingtext].ToString();
                    HNUnderlyVO.CreateDateTime = ADOUtil.GetDateFromQuery(query[_CreateDateTime].ToString());
                    HNUnderlyVO.CreateUser = query[_CreateUser].ToString();
                    HNUnderlyVO.UpdateDateTime = ADOUtil.GetDateFromQuery(query[_UpdateDateTime].ToString());
                    HNUnderlyVO.UpdateUser = query[_UpdateUser].ToString();
                    retValue.Add(HNUnderlyVO);
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

        internal ReturnValue Insert(HNUnderlyVO _HNUnderlyVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblDAHNUnderly + " (");
                sbValue.Append(" VALUES(");


                sbInsert.Append( _HN);
                sbValue.Append("@" + _HN);

                sbInsert.Append("," + _Underlyingtext);
                sbValue.Append(",@" + _Underlyingtext);

                sbInsert.Append("," + _CreateDateTime);
                sbValue.Append(", GETDATE()");

                sbInsert.Append("," + _CreateUser);
                sbValue.Append(",@" + _CreateUser);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.HN)));
                parameter.Add(new IParameter(_Underlyingtext, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.Underlyingtext)));
                parameter.Add(new IParameter(_CreateUser, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.CreateUser)));

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

        internal ReturnValue Update(HNUnderlyVO _HNUnderlyVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblDAHNUnderly + " SET ");

                sbQuery.Append("" + _Underlyingtext + " = @" + _Underlyingtext);
                sbQuery.Append("," + _UpdateDateTime + " = GETDATE()");
                sbQuery.Append("," + _UpdateUser + " = @" + _UpdateUser);
                sbQuery.Append(" WHERE " + _HN + " = @" + _HN);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.HN)));
                parameter.Add(new IParameter(_Underlyingtext, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.Underlyingtext)));
                parameter.Add(new IParameter(_UpdateUser, IDbType.VarChar, DBNullConvert.From(_HNUnderlyVO.UpdateUser)));


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
