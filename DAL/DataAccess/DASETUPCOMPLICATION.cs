using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPCOMPLICATION : DataAccess
    {
        private static string _tblSETUPCOMPLICATION = "SETUPCOMPLICATION";
        private static string _ID = "ID";
        private static string _ComplicationHeader = "ComplicationHeader";
        private static string _ComplicationDetail = "ComplicationDetail";
        private static string _CreateDateTime = "CreateDateTime";
        private static string _UpdateDateTime = "UpdateDateTime";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DASETUPCOMPLICATION() { }
        public DASETUPCOMPLICATION(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPCOMPLICATION(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPCOMPLICATION(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPCOMPLICATIONVO> SearchByKey(SETUPCOMPLICATIONVO _SETUPCOMPLICATIONVO)
        {
            List<SETUPCOMPLICATIONVO> retValue = new List<SETUPCOMPLICATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPCOMPLICATION + " where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPCOMPLICATIONVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                strQuery.Append(" order by " + _CreateDateTime + " DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPCOMPLICATIONVO.ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                    SETUPCOMPLICATIONVO.ID = query[_ID].ToString();
                    SETUPCOMPLICATIONVO.ComplicationHeader = query[_ComplicationHeader].ToString();
                    SETUPCOMPLICATIONVO.ComplicationDetail = query[_ComplicationDetail].ToString();
                    SETUPCOMPLICATIONVO.CreateDateTime = ADOUtil.GetDateFromQuery(query[_CreateDateTime].ToString());
                    SETUPCOMPLICATIONVO.UpdateDateTime = ADOUtil.GetDateFromQuery(query[_UpdateDateTime].ToString());
                    retValue.Add(SETUPCOMPLICATIONVO);
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

        internal List<SETUPCOMPLICATIONVO> SearchByPrimary(string ID)
        {
            List<SETUPCOMPLICATIONVO> retValue = new List<SETUPCOMPLICATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPCOMPLICATION + " where 1=1 ");
                strQuery.Append(" and " + _ID + " = @" + _ID);
                strQuery.Append(" order by " + _CreateDateTime + " DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                    SETUPCOMPLICATIONVO.ID = query[_ID].ToString();
                    SETUPCOMPLICATIONVO.ComplicationHeader = query[_ComplicationHeader].ToString();
                    SETUPCOMPLICATIONVO.ComplicationDetail = query[_ComplicationDetail].ToString();
                    SETUPCOMPLICATIONVO.CreateDateTime = ADOUtil.GetDateFromQuery(query[_CreateDateTime].ToString());
                    SETUPCOMPLICATIONVO.UpdateDateTime = ADOUtil.GetDateFromQuery(query[_UpdateDateTime].ToString());
                    retValue.Add(SETUPCOMPLICATIONVO);
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

        //internal int GetSuffixNext(string orid)
        //{
        //    int SuffixNext = 1;
        //    try
        //    {
        //        StringBuilder strQuery = new StringBuilder();
        //        strQuery.Append(" SELECT MAX(Suffix) AS SuffixMax FROM " + _tblPOSTORCOMPLICATION);
        //        strQuery.Append(" where " + _ORID + " = @" + _ORID);
        //        ConnectDB();
        //        List<IParameter> parameter = new List<IParameter>();
        //        parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(orid)));

        //        command = GetCommand(strQuery.ToString(), parameter);
        //        query = GetExecuteReader(command);
        //        while (query.Read())
        //        {
        //            SuffixNext = ADOUtil.GetIntFromQuery(query["SuffixMax"].ToString()) + 1;
        //        }
        //        query.Close();
        //        command.Dispose();
        //        DisconnectDB();
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    return SuffixNext;
        //}

        internal ReturnValue Insert(SETUPCOMPLICATIONVO _SETUPCOMPLICATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPCOMPLICATION + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _ComplicationHeader);
                sbValue.Append(",@" + _ComplicationHeader);

                sbInsert.Append("," + _ComplicationDetail);
                sbValue.Append(",@" + _ComplicationDetail);

                sbInsert.Append("," + _CreateDateTime);
                sbValue.Append(", GETDATE()");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPCOMPLICATIONVO.ID)));
                parameter.Add(new IParameter(_ComplicationHeader, IDbType.VarChar, DBNullConvert.From(_SETUPCOMPLICATIONVO.ComplicationHeader)));
                parameter.Add(new IParameter(_ComplicationDetail, IDbType.Text, DBNullConvert.From(_SETUPCOMPLICATIONVO.ComplicationDetail)));
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

        internal ReturnValue Update(SETUPCOMPLICATIONVO _SETUPCOMPLICATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPCOMPLICATION + " SET ");
                sbQuery.Append("" + _ComplicationHeader + " = @" + _ComplicationHeader);
                sbQuery.Append("," + _ComplicationDetail + " = @" + _ComplicationDetail);
                sbQuery.Append("," + _UpdateDateTime + " = GETDATE()");
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_SETUPCOMPLICATIONVO.ID)));
                parameter.Add(new IParameter(_ComplicationHeader, IDbType.VarChar, DBNullConvert.From(_SETUPCOMPLICATIONVO.ComplicationHeader)));
                parameter.Add(new IParameter(_ComplicationDetail, IDbType.Text, DBNullConvert.From(_SETUPCOMPLICATIONVO.ComplicationDetail)));

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

        internal ReturnValue Delete(string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPCOMPLICATION);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
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
