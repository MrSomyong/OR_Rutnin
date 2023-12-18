using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORDETAIL : DataAccess
    {
        private static string _tblPOSTORDETAIL = "POSTORDETAIL";

        private static string _ORID = "ORID";
        private static string _StartORDateTime = "StartORDateTime";
        private static string _FinishORDateTime = "FinishORDateTime";
        private static string _StartAnesDateTime = "StartAnesDateTime";
        private static string _FinishAnesDateTime = "FinishAnesDateTime";

        private static string _StartBlockDateTime = "StartBlockDateTime";
        private static string _FinishBlockDateTime = "FinishBlockDateTime";

        private static string _StartRecoveryDateTime = "StartRecoveryDateTime";
        private static string _FinishRecoveryDateTime = "FinishRecoveryDateTime";

        private static string _ORCaseType = "ORCaseType";
        private static string _ORWrongCase = "ORWrongCase";
        private static string _ORWoundType = "ORWoundType";
        private static string _ORUnplantType = "ORUnplantType";

        private static string _ORWoundType1 = "ORWoundType1";
        private static string _ORWoundType2 = "ORWoundType2";
        private static string _ORWoundType3 = "ORWoundType3";
        private static string _ORWoundType4 = "ORWoundType4";

        private static string _External = "xExternal"; 
        private static string _Anterior = "Anterior";
        private static string _Posterior = "Posterior";

        private static string _ChangOperation = "ChangOperation";
        private static string _HR48 = "HR48";
        private static string _Day30 = "Day30";
        private static string _Indicator = "Indicator";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORDETAIL() { }
        public DAPOSTORDETAIL(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORDETAIL(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORDETAIL(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORDETAILVO> SearchByKey(POSTORDETAILVO _POSTORDETAILVO)
        {
            List<POSTORDETAILVO> retValue = new List<POSTORDETAILVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPOSTORDETAIL + " where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORDETAILVO.ORID))
                {
                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
                }
                strQuery.Append(" order by " + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORDETAILVO.ORID)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORDETAILVO POSTORDETAILVO = new POSTORDETAILVO();
                    POSTORDETAILVO.ORID = query[_ORID].ToString();
                    POSTORDETAILVO.StartORDateTime = ADOUtil.GetDateFromQuery(query[_StartORDateTime].ToString());
                    POSTORDETAILVO.FinishORDateTime = ADOUtil.GetDateFromQuery(query[_FinishORDateTime].ToString());
                    POSTORDETAILVO.StartAnesDateTime = ADOUtil.GetDateFromQuery(query[_StartAnesDateTime].ToString());
                    POSTORDETAILVO.FinishAnesDateTime = ADOUtil.GetDateFromQuery(query[_FinishAnesDateTime].ToString());

                    POSTORDETAILVO.StartBlockDateTime = ADOUtil.GetDateFromQuery(query[_StartBlockDateTime].ToString());
                    POSTORDETAILVO.FinishBlockDateTime = ADOUtil.GetDateFromQuery(query[_FinishBlockDateTime].ToString());

                    POSTORDETAILVO.StartRecoveryDateTime = ADOUtil.GetDateFromQuery(query[_StartRecoveryDateTime].ToString());
                    POSTORDETAILVO.FinishRecoveryDateTime = ADOUtil.GetDateFromQuery(query[_FinishRecoveryDateTime].ToString());

                    POSTORDETAILVO.ORCaseType = ADOUtil.GetIntFromQuery(query[_ORCaseType].ToString());
                    POSTORDETAILVO.ORWrongCase = ADOUtil.GetIntFromQuery(query[_ORWrongCase].ToString());
                    POSTORDETAILVO.ORWoundType = ADOUtil.GetIntFromQuery(query[_ORWoundType].ToString());
                    POSTORDETAILVO.ORUnplantType = ADOUtil.GetIntFromQuery(query[_ORUnplantType].ToString());

                    POSTORDETAILVO.ORWoundType1 = ADOUtil.GetBoolFromQuery(query[_ORWoundType1].ToString());
                    POSTORDETAILVO.ORWoundType2 = ADOUtil.GetBoolFromQuery(query[_ORWoundType2].ToString());
                    POSTORDETAILVO.ORWoundType3 = ADOUtil.GetBoolFromQuery(query[_ORWoundType3].ToString());
                    POSTORDETAILVO.ORWoundType4 = ADOUtil.GetBoolFromQuery(query[_ORWoundType4].ToString());

                    POSTORDETAILVO.External = ADOUtil.GetBoolFromQuery(query[_External].ToString());
                    POSTORDETAILVO.Anterior = ADOUtil.GetBoolFromQuery(query[_Anterior].ToString());
                    POSTORDETAILVO.Posterior = ADOUtil.GetBoolFromQuery(query[_Posterior].ToString());

                    POSTORDETAILVO.ChangOperation = ADOUtil.GetBoolFromQuery(query[_ChangOperation].ToString());
                    POSTORDETAILVO.HR48 = ADOUtil.GetBoolFromQuery(query[_HR48].ToString());
                    POSTORDETAILVO.Day30 = ADOUtil.GetBoolFromQuery(query[_Day30].ToString());

                    POSTORDETAILVO.Indicator = query[_Indicator].ToString();

                    retValue.Add(POSTORDETAILVO);
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

        internal bool Checkdup(string ORID)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblPOSTORDETAIL);
                strQuery.Append(" where " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch
            {
                retVal = false;
            }
            return retVal;
        }

        internal ReturnValue Insert(POSTORDETAILVO _POSTORDETAILVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORDETAIL + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ORID);
                sbValue.Append("@" + _ORID);

                sbInsert.Append("," + _StartORDateTime);
                sbValue.Append(",@" + _StartORDateTime);

                sbInsert.Append("," + _FinishORDateTime);
                sbValue.Append(",@" + _FinishORDateTime);

                sbInsert.Append("," + _StartAnesDateTime);
                sbValue.Append(",@" + _StartAnesDateTime);

                sbInsert.Append("," + _FinishAnesDateTime);
                sbValue.Append(",@" + _FinishAnesDateTime);

                sbInsert.Append("," + _StartBlockDateTime);
                sbValue.Append(",@" + _StartBlockDateTime);

                sbInsert.Append("," + _FinishBlockDateTime);
                sbValue.Append(",@" + _FinishBlockDateTime);

                sbInsert.Append("," + _StartRecoveryDateTime);
                sbValue.Append(",@" + _StartRecoveryDateTime);

                sbInsert.Append("," + _FinishRecoveryDateTime);
                sbValue.Append(",@" + _FinishRecoveryDateTime);

                sbInsert.Append("," + _ORCaseType);
                sbValue.Append(",@" + _ORCaseType);

                sbInsert.Append("," + _ORWrongCase);
                sbValue.Append(",@" + _ORWrongCase);

                sbInsert.Append("," + _ORWoundType);
                sbValue.Append(",@" + _ORWoundType);

                sbInsert.Append("," + _ORUnplantType);
                sbValue.Append(",@" + _ORUnplantType);

                sbInsert.Append("," + _ORWoundType1);
                sbValue.Append(",@" + _ORWoundType1);

                sbInsert.Append("," + _ORWoundType2);
                sbValue.Append(",@" + _ORWoundType2);

                sbInsert.Append("," + _ORWoundType3);
                sbValue.Append(",@" + _ORWoundType3);

                sbInsert.Append("," + _ORWoundType4);
                sbValue.Append(",@" + _ORWoundType4);

                sbInsert.Append("," + _External);
                sbValue.Append(",@" + _External);

                sbInsert.Append("," + _Anterior);
                sbValue.Append(",@" + _Anterior);

                sbInsert.Append("," + _Posterior);
                sbValue.Append(",@" + _Posterior);

                sbInsert.Append("," + _ChangOperation);
                sbValue.Append(",@" + _ChangOperation);

                sbInsert.Append("," + _HR48);
                sbValue.Append(",@" + _HR48);

                sbInsert.Append("," + _Day30);
                sbValue.Append(",@" + _Day30);

                sbInsert.Append("," + _Indicator);
                sbValue.Append(",@" + _Indicator);


                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORDETAILVO.ORID)));
                parameter.Add(new IParameter(_StartORDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartORDateTime)));
                parameter.Add(new IParameter(_FinishORDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishORDateTime)));
                parameter.Add(new IParameter(_StartAnesDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartAnesDateTime)));
                parameter.Add(new IParameter(_FinishAnesDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishAnesDateTime)));

                parameter.Add(new IParameter(_StartBlockDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartBlockDateTime)));
                parameter.Add(new IParameter(_FinishBlockDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishBlockDateTime)));

                parameter.Add(new IParameter(_StartRecoveryDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartRecoveryDateTime)));
                parameter.Add(new IParameter(_FinishRecoveryDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishRecoveryDateTime)));

                parameter.Add(new IParameter(_ORCaseType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORCaseType, false)));
                parameter.Add(new IParameter(_ORWrongCase, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORWrongCase, false)));
                parameter.Add(new IParameter(_ORWoundType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORWoundType, false)));
                parameter.Add(new IParameter(_ORUnplantType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORUnplantType, false)));

                parameter.Add(new IParameter(_ORWoundType1, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType1)));
                parameter.Add(new IParameter(_ORWoundType2, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType2)));
                parameter.Add(new IParameter(_ORWoundType3, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType3)));
                parameter.Add(new IParameter(_ORWoundType4, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType4)));

                parameter.Add(new IParameter(_External, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.External)));
                parameter.Add(new IParameter(_Anterior, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Anterior)));
                parameter.Add(new IParameter(_Posterior, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Posterior)));

                parameter.Add(new IParameter(_ChangOperation, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ChangOperation)));
                parameter.Add(new IParameter(_HR48, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.HR48)));
                parameter.Add(new IParameter(_Day30, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Day30)));
                parameter.Add(new IParameter(_Indicator, IDbType.VarChar, DBNullConvert.From(_POSTORDETAILVO.Indicator)));

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

        internal ReturnValue Update(POSTORDETAILVO _POSTORDETAILVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORDETAIL + " SET ");
                sbQuery.Append("" + _StartORDateTime + " = @" + _StartORDateTime);
                sbQuery.Append("," + _FinishORDateTime + " = @" + _FinishORDateTime);
                sbQuery.Append("," + _StartAnesDateTime + " = @" + _StartAnesDateTime);
                sbQuery.Append("," + _FinishAnesDateTime + " = @" + _FinishAnesDateTime);

                sbQuery.Append("," + _StartBlockDateTime + " = @" + _StartBlockDateTime);
                sbQuery.Append("," + _FinishBlockDateTime + " = @" + _FinishBlockDateTime);

                sbQuery.Append("," + _StartRecoveryDateTime + " = @" + _StartRecoveryDateTime);
                sbQuery.Append("," + _FinishRecoveryDateTime + " = @" + _FinishRecoveryDateTime);

                sbQuery.Append("," + _ORCaseType + " = @" + _ORCaseType);
                sbQuery.Append("," + _ORWrongCase + " = @" + _ORWrongCase);
                sbQuery.Append("," + _ORWoundType + " = @" + _ORWoundType);
                sbQuery.Append("," + _ORUnplantType + " = @" + _ORUnplantType);

                sbQuery.Append("," + _ORWoundType1 + " = @" + _ORWoundType1);
                sbQuery.Append("," + _ORWoundType2 + " = @" + _ORWoundType2);
                sbQuery.Append("," + _ORWoundType3 + " = @" + _ORWoundType3);
                sbQuery.Append("," + _ORWoundType4 + " = @" + _ORWoundType4);

                sbQuery.Append("," + _External + " = @" + _External);
                sbQuery.Append("," + _Anterior + " = @" + _Anterior);
                sbQuery.Append("," + _Posterior + " = @" + _Posterior);

                sbQuery.Append("," + _ChangOperation + " = @" + _ChangOperation);
                sbQuery.Append("," + _HR48 + " = @" + _HR48);
                sbQuery.Append("," + _Day30 + " = @" + _Day30);
                sbQuery.Append("," + _Indicator + " = @" + _Indicator);

                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORDETAILVO.ORID)));
                parameter.Add(new IParameter(_StartORDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartORDateTime)));
                parameter.Add(new IParameter(_FinishORDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishORDateTime)));
                parameter.Add(new IParameter(_StartAnesDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartAnesDateTime)));
                parameter.Add(new IParameter(_FinishAnesDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishAnesDateTime)));

                parameter.Add(new IParameter(_StartBlockDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartBlockDateTime)));
                parameter.Add(new IParameter(_FinishBlockDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishBlockDateTime)));

                parameter.Add(new IParameter(_StartRecoveryDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.StartRecoveryDateTime)));
                parameter.Add(new IParameter(_FinishRecoveryDateTime, IDbType.DateTime, DBNullConvert.From(_POSTORDETAILVO.FinishRecoveryDateTime)));

                parameter.Add(new IParameter(_ORCaseType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORCaseType, false)));
                parameter.Add(new IParameter(_ORWrongCase, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORWrongCase, false)));
                parameter.Add(new IParameter(_ORWoundType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORWoundType, false)));
                parameter.Add(new IParameter(_ORUnplantType, IDbType.Int, DBNullConvert.From(_POSTORDETAILVO.ORUnplantType, false)));

                parameter.Add(new IParameter(_ORWoundType1, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType1)));
                parameter.Add(new IParameter(_ORWoundType2, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType2)));
                parameter.Add(new IParameter(_ORWoundType3, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType3)));
                parameter.Add(new IParameter(_ORWoundType4, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ORWoundType4)));

                parameter.Add(new IParameter(_External, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.External)));
                parameter.Add(new IParameter(_Anterior, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Anterior)));
                parameter.Add(new IParameter(_Posterior, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Posterior)));

                parameter.Add(new IParameter(_ChangOperation, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.ChangOperation)));
                parameter.Add(new IParameter(_HR48, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.HR48)));
                parameter.Add(new IParameter(_Day30, IDbType.Bit, DBNullConvert.From(_POSTORDETAILVO.Day30)));
                parameter.Add(new IParameter(_Indicator, IDbType.VarChar, DBNullConvert.From(_POSTORDETAILVO.Indicator)));

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

        internal ReturnValue Delete(string ORID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORDETAIL);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
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
