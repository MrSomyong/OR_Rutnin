using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORCOMPLICATION : DataAccess
    {
        private static string _tblPOSTORCOMPLICATION = "POSTORCOMPLICATION";
        private static string _tblSETUPCOMPLICATION = "SETUPCOMPLICATION";
        private static string _ORID = "ORID";
        private static string _ID = "ID";
        private static string _ComplicationHeader = "ComplicationHeader";
        private static string _ComplicationDetail = "ComplicationDetail";
        private static string _CreateDateTime = "CreateDateTime";
        private static string _UpdateDateTime = "UpdateDateTime";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORCOMPLICATION() { }
        public DAPOSTORCOMPLICATION(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORCOMPLICATION(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORCOMPLICATION(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORCOMPLICATIONVO> SearchByKey(POSTORCOMPLICATIONVO _POSTORCOMPLICATIONVO)
        {
            List<POSTORCOMPLICATIONVO> retValue = new List<POSTORCOMPLICATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _ComplicationHeader + " from " + _tblPOSTORCOMPLICATION + " a ");
                strQuery.Append(" left join " + _tblSETUPCOMPLICATION + " b on a." + _ID + " = b." + _ID);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORCOMPLICATIONVO.ORID))
                {
                    strQuery.Append(" and a." + _ORID + " = @" + _ORID);
                }
                if (!string.IsNullOrEmpty(_POSTORCOMPLICATIONVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                strQuery.Append(" order by b." + _ComplicationHeader);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORCOMPLICATIONVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORCOMPLICATIONVO.ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO = new POSTORCOMPLICATIONVO();
                    POSTORCOMPLICATIONVO.ORID = query[_ORID].ToString();
                    POSTORCOMPLICATIONVO.ID = query[_ID].ToString();
                    POSTORCOMPLICATIONVO.ComplicationHeader = query[_ComplicationHeader].ToString();
                    POSTORCOMPLICATIONVO.ComplicationDetail = query[_ComplicationDetail].ToString();
                    retValue.Add(POSTORCOMPLICATIONVO);
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

        internal List<POSTORCOMPLICATIONVO> SearchByPrimary(string ORID, string ID)
        {
            List<POSTORCOMPLICATIONVO> retValue = new List<POSTORCOMPLICATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _ComplicationHeader + " from " + _tblPOSTORCOMPLICATION + " a ");
                strQuery.Append(" left join " + _tblSETUPCOMPLICATION + " b on a." + _ID + " = b." + _ID);
                strQuery.Append(" where 1=1 ");
                strQuery.Append(" and a." + _ORID + " = @" + _ORID);
                strQuery.Append(" and a." + _ID + " = @" + _ID);
                strQuery.Append(" order by b." + _ComplicationHeader);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO = new POSTORCOMPLICATIONVO();
                    POSTORCOMPLICATIONVO.ORID = query[_ORID].ToString();
                    POSTORCOMPLICATIONVO.ID = query[_ID].ToString();
                    POSTORCOMPLICATIONVO.ComplicationHeader = query[_ComplicationHeader].ToString();
                    POSTORCOMPLICATIONVO.ComplicationDetail = query[_ComplicationDetail].ToString();
                    retValue.Add(POSTORCOMPLICATIONVO);
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

        internal ReturnValue Insert(POSTORCOMPLICATIONVO _POSTORCOMPLICATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORCOMPLICATION + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ORID);
                sbValue.Append("@" + _ORID);

                sbInsert.Append("," + _ID);
                sbValue.Append(",@" + _ID);

                sbInsert.Append("," + _ComplicationDetail);
                sbValue.Append(",@" + _ComplicationDetail);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORCOMPLICATIONVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORCOMPLICATIONVO.ID)));
                parameter.Add(new IParameter(_ComplicationDetail, IDbType.Text, DBNullConvert.From(_POSTORCOMPLICATIONVO.ComplicationDetail))); 
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

        internal ReturnValue Update(POSTORCOMPLICATIONVO _POSTORCOMPLICATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORCOMPLICATION + " SET ");
                sbQuery.Append("" + _ComplicationDetail + " = @" + _ComplicationDetail);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORCOMPLICATIONVO.ID)));
                parameter.Add(new IParameter(_ComplicationDetail, IDbType.Text, DBNullConvert.From(_POSTORCOMPLICATIONVO.ComplicationDetail)));

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
                sbQuery.Append("DELETE FROM " + _tblPOSTORCOMPLICATION);
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

        internal ReturnValue Delete(string ORID, string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORCOMPLICATION);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);
                sbQuery.Append(" AND " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
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
