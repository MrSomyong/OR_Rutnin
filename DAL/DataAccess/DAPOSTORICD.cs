using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORICD : DataAccess
    {
        private static string _tblPOSTORICD = "POSTORICD";
        private static string _tblSETUPICD = "SETUPICD";
        private static string _tblSETUPICDCM = "SETUPICDCM";
        private static string _ID = "ID";
        private static string _ORID = "ORID";
        private static string _ICD = "ICD";
        private static string _ICDCM1 = "ICDCM1";
        private static string _ICDCM2 = "ICDCM2";
        private static string _ICDCM3 = "ICDCM3";
        private static string _ICDCM4 = "ICDCM4";
        private static string _ICDCM5 = "ICDCM5";

        private static string _ICDCM1DOC1 = "ICDCM1DOC1";
        private static string _ICDCM1DOC2 = "ICDCM1DOC2";
        private static string _ICDCM1DOC3 = "ICDCM1DOC3";
        private static string _ICDCM1DOC4 = "ICDCM1DOC4";

        private static string _ICDCM2DOC1 = "ICDCM2DOC1";
        private static string _ICDCM2DOC2 = "ICDCM2DOC2";
        private static string _ICDCM2DOC3 = "ICDCM2DOC3";
        private static string _ICDCM2DOC4 = "ICDCM2DOC4";

        private static string _ICDCM3DOC1 = "ICDCM3DOC1";
        private static string _ICDCM3DOC2 = "ICDCM3DOC2";
        private static string _ICDCM3DOC3 = "ICDCM3DOC3";
        private static string _ICDCM3DOC4 = "ICDCM3DOC4";

        private static string _ICDCM4DOC1 = "ICDCM4DOC1";
        private static string _ICDCM4DOC2 = "ICDCM4DOC2";
        private static string _ICDCM4DOC3 = "ICDCM4DOC3";
        private static string _ICDCM4DOC4 = "ICDCM4DOC4";

        private static string _ICDCM5DOC1 = "ICDCM5DOC1";
        private static string _ICDCM5DOC2 = "ICDCM5DOC2";
        private static string _ICDCM5DOC3 = "ICDCM5DOC3";
        private static string _ICDCM5DOC4 = "ICDCM5DOC4";

        private static string _Code = "Code";
        private static string _Name = "Name";

        private static string _Remark = "Remark";
        private static string _MakeDatetime = "MakeDatetime";
        private static string _LastUpdateDatetime = "LastUpdateDatetime";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORICD() { }
        public DAPOSTORICD(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORICD(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORICD(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORICDVO> SearchByKey(POSTORICDVO _POSTORICDVO)
        {
            List<POSTORICDVO> retValue = new List<POSTORICDVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPOSTORICD);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORICDVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_POSTORICDVO.ORID))
                {
                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
                }
                strQuery.Append(" order by " + _ORID + "," + _ICD);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ORID)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORICDVO POSTORICDVO = new POSTORICDVO();
                    POSTORICDVO.ID = query[_ID].ToString();
                    POSTORICDVO.ORID = query[_ORID].ToString();
                    POSTORICDVO.ICD = query[_ICD].ToString();
                    POSTORICDVO.ICDCM1 = query[_ICDCM1].ToString();
                    POSTORICDVO.ICDCM2 = query[_ICDCM2].ToString();
                    POSTORICDVO.ICDCM3 = query[_ICDCM3].ToString();
                    POSTORICDVO.ICDCM4 = query[_ICDCM4].ToString();
                    POSTORICDVO.ICDCM5 = query[_ICDCM5].ToString();

                    POSTORICDVO.ICDCM1DOC1 = query[_ICDCM1DOC1].ToString();
                    POSTORICDVO.ICDCM1DOC2 = query[_ICDCM1DOC2].ToString();
                    POSTORICDVO.ICDCM1DOC3 = query[_ICDCM1DOC3].ToString();
                    POSTORICDVO.ICDCM1DOC4 = query[_ICDCM1DOC4].ToString();

                    POSTORICDVO.ICDCM2DOC1 = query[_ICDCM2DOC1].ToString();
                    POSTORICDVO.ICDCM2DOC2 = query[_ICDCM2DOC2].ToString();
                    POSTORICDVO.ICDCM2DOC3 = query[_ICDCM2DOC3].ToString();
                    POSTORICDVO.ICDCM2DOC4 = query[_ICDCM2DOC4].ToString();

                    POSTORICDVO.ICDCM3DOC1 = query[_ICDCM3DOC1].ToString();
                    POSTORICDVO.ICDCM3DOC2 = query[_ICDCM3DOC2].ToString();
                    POSTORICDVO.ICDCM3DOC3 = query[_ICDCM3DOC3].ToString();
                    POSTORICDVO.ICDCM3DOC4 = query[_ICDCM3DOC4].ToString();

                    POSTORICDVO.ICDCM4DOC1 = query[_ICDCM4DOC1].ToString();
                    POSTORICDVO.ICDCM4DOC2 = query[_ICDCM4DOC2].ToString();
                    POSTORICDVO.ICDCM4DOC3 = query[_ICDCM4DOC3].ToString();
                    POSTORICDVO.ICDCM4DOC4 = query[_ICDCM4DOC4].ToString();

                    POSTORICDVO.ICDCM5DOC1 = query[_ICDCM5DOC1].ToString();
                    POSTORICDVO.ICDCM5DOC2 = query[_ICDCM5DOC2].ToString();
                    POSTORICDVO.ICDCM5DOC3 = query[_ICDCM5DOC3].ToString();
                    POSTORICDVO.ICDCM5DOC4 = query[_ICDCM5DOC4].ToString();

                    POSTORICDVO.Remark = query[_Remark].ToString();
                    if (query[_MakeDatetime].ToString() != "")
                    {
                        POSTORICDVO.MakeDatetime = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_MakeDatetime].ToString()).ToString()), YearType.English).ToString("dd-MM-yyyy HH:ss");
                        //query[_MakeDatetime].ToString();
                    }
                    else
                    {
                        POSTORICDVO.MakeDatetime = "";
                    }
                    retValue.Add(POSTORICDVO);
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

        internal ReturnValue Insert(POSTORICDVO _POSTORICDVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORICD + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _ORID);
                sbValue.Append(",@" + _ORID);

                sbInsert.Append("," + _ICD);
                sbValue.Append(",@" + _ICD);

                sbInsert.Append("," + _ICDCM1);
                sbValue.Append(",@" + _ICDCM1);

                sbInsert.Append("," + _ICDCM2);
                sbValue.Append(",@" + _ICDCM2);

                sbInsert.Append("," + _ICDCM3);
                sbValue.Append(",@" + _ICDCM3);

                sbInsert.Append("," + _ICDCM4);
                sbValue.Append(",@" + _ICDCM4);

                sbInsert.Append("," + _ICDCM5);
                sbValue.Append(",@" + _ICDCM5);

                sbInsert.Append("," + _ICDCM1DOC1);
                sbValue.Append(",@" + _ICDCM1DOC1);
                sbInsert.Append("," + _ICDCM1DOC2);
                sbValue.Append(",@" + _ICDCM1DOC2);
                sbInsert.Append("," + _ICDCM1DOC3);
                sbValue.Append(",@" + _ICDCM1DOC3);
                sbInsert.Append("," + _ICDCM1DOC4);
                sbValue.Append(",@" + _ICDCM1DOC4);

                sbInsert.Append("," + _ICDCM2DOC1);
                sbValue.Append(",@" + _ICDCM2DOC1);
                sbInsert.Append("," + _ICDCM2DOC2);
                sbValue.Append(",@" + _ICDCM2DOC2);
                sbInsert.Append("," + _ICDCM2DOC3);
                sbValue.Append(",@" + _ICDCM2DOC3);
                sbInsert.Append("," + _ICDCM2DOC4);
                sbValue.Append(",@" + _ICDCM2DOC4);

                sbInsert.Append("," + _ICDCM3DOC1);
                sbValue.Append(",@" + _ICDCM3DOC1);
                sbInsert.Append("," + _ICDCM3DOC2);
                sbValue.Append(",@" + _ICDCM3DOC2);
                sbInsert.Append("," + _ICDCM3DOC3);
                sbValue.Append(",@" + _ICDCM3DOC3);
                sbInsert.Append("," + _ICDCM3DOC4);
                sbValue.Append(",@" + _ICDCM3DOC4);

                sbInsert.Append("," + _ICDCM4DOC1);
                sbValue.Append(",@" + _ICDCM4DOC1);
                sbInsert.Append("," + _ICDCM4DOC2);
                sbValue.Append(",@" + _ICDCM4DOC2);
                sbInsert.Append("," + _ICDCM4DOC3);
                sbValue.Append(",@" + _ICDCM4DOC3);
                sbInsert.Append("," + _ICDCM4DOC4);
                sbValue.Append(",@" + _ICDCM4DOC4);

                sbInsert.Append("," + _ICDCM5DOC1);
                sbValue.Append(",@" + _ICDCM5DOC1);
                sbInsert.Append("," + _ICDCM5DOC2);
                sbValue.Append(",@" + _ICDCM5DOC2);
                sbInsert.Append("," + _ICDCM5DOC3);
                sbValue.Append(",@" + _ICDCM5DOC3);
                sbInsert.Append("," + _ICDCM5DOC4);
                sbValue.Append(",@" + _ICDCM5DOC4);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);

                sbInsert.Append("," + _MakeDatetime);
                sbValue.Append(",CURRENT_TIMESTAMP");

                sbInsert.Append("," + _LastUpdateDatetime);
                sbValue.Append(",CURRENT_TIMESTAMP");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ORID)));
                parameter.Add(new IParameter(_ICD, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICD)));
                parameter.Add(new IParameter(_ICDCM1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1)));
                parameter.Add(new IParameter(_ICDCM2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2)));
                parameter.Add(new IParameter(_ICDCM3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3)));
                parameter.Add(new IParameter(_ICDCM4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4)));
                parameter.Add(new IParameter(_ICDCM5, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5)));

                parameter.Add(new IParameter(_ICDCM1DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC1)));
                parameter.Add(new IParameter(_ICDCM1DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC2)));
                parameter.Add(new IParameter(_ICDCM1DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC3)));
                parameter.Add(new IParameter(_ICDCM1DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC4)));

                parameter.Add(new IParameter(_ICDCM2DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC1)));
                parameter.Add(new IParameter(_ICDCM2DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC2)));
                parameter.Add(new IParameter(_ICDCM2DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC3)));
                parameter.Add(new IParameter(_ICDCM2DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC4)));

                parameter.Add(new IParameter(_ICDCM3DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC1)));
                parameter.Add(new IParameter(_ICDCM3DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC2)));
                parameter.Add(new IParameter(_ICDCM3DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC3)));
                parameter.Add(new IParameter(_ICDCM3DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC4)));

                parameter.Add(new IParameter(_ICDCM4DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC1)));
                parameter.Add(new IParameter(_ICDCM4DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC2)));
                parameter.Add(new IParameter(_ICDCM4DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC3)));
                parameter.Add(new IParameter(_ICDCM4DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC4)));

                parameter.Add(new IParameter(_ICDCM5DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC1)));
                parameter.Add(new IParameter(_ICDCM5DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC2)));
                parameter.Add(new IParameter(_ICDCM5DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC3)));
                parameter.Add(new IParameter(_ICDCM5DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC4)));

                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.Remark)));
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

        internal ReturnValue Update(POSTORICDVO _POSTORICDVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORICD + " SET ");
                sbQuery.Append("" + _ICD + " = @" + _ICD);
                sbQuery.Append("," + _ICDCM1 + " = @" + _ICDCM1);
                sbQuery.Append("," + _ICDCM2 + " = @" + _ICDCM2);
                sbQuery.Append("," + _ICDCM3 + " = @" + _ICDCM3);
                sbQuery.Append("," + _ICDCM4 + " = @" + _ICDCM4);
                sbQuery.Append("," + _ICDCM5 + " = @" + _ICDCM5);

                sbQuery.Append("," + _ICDCM1DOC1 + " = @" + _ICDCM1DOC1);
                sbQuery.Append("," + _ICDCM1DOC2 + " = @" + _ICDCM1DOC2);
                sbQuery.Append("," + _ICDCM1DOC3 + " = @" + _ICDCM1DOC3);
                sbQuery.Append("," + _ICDCM1DOC4 + " = @" + _ICDCM1DOC4);

                sbQuery.Append("," + _ICDCM2DOC1 + " = @" + _ICDCM2DOC1);
                sbQuery.Append("," + _ICDCM2DOC2 + " = @" + _ICDCM2DOC2);
                sbQuery.Append("," + _ICDCM2DOC3 + " = @" + _ICDCM2DOC3);
                sbQuery.Append("," + _ICDCM2DOC4 + " = @" + _ICDCM2DOC4);

                sbQuery.Append("," + _ICDCM3DOC1 + " = @" + _ICDCM3DOC1);
                sbQuery.Append("," + _ICDCM3DOC2 + " = @" + _ICDCM3DOC2);
                sbQuery.Append("," + _ICDCM3DOC3 + " = @" + _ICDCM3DOC3);
                sbQuery.Append("," + _ICDCM3DOC4 + " = @" + _ICDCM3DOC4);

                sbQuery.Append("," + _ICDCM4DOC1 + " = @" + _ICDCM4DOC1);
                sbQuery.Append("," + _ICDCM4DOC2 + " = @" + _ICDCM4DOC2);
                sbQuery.Append("," + _ICDCM4DOC3 + " = @" + _ICDCM4DOC3);
                sbQuery.Append("," + _ICDCM4DOC4 + " = @" + _ICDCM4DOC4);

                sbQuery.Append("," + _ICDCM5DOC1 + " = @" + _ICDCM5DOC1);
                sbQuery.Append("," + _ICDCM5DOC2 + " = @" + _ICDCM5DOC2);
                sbQuery.Append("," + _ICDCM5DOC3 + " = @" + _ICDCM5DOC3);
                sbQuery.Append("," + _ICDCM5DOC4 + " = @" + _ICDCM5DOC4);

                sbQuery.Append("," + _Remark + " = @" + _Remark);
                sbQuery.Append("," + _LastUpdateDatetime + " = CURRENT_TIMESTAMP");

                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                sbQuery.Append(" AND " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ORID)));
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ID)));
                parameter.Add(new IParameter(_ICD, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICD)));
                parameter.Add(new IParameter(_ICDCM1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1)));
                parameter.Add(new IParameter(_ICDCM2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2)));
                parameter.Add(new IParameter(_ICDCM3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3)));
                parameter.Add(new IParameter(_ICDCM4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4)));
                parameter.Add(new IParameter(_ICDCM5, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.Remark)));

                parameter.Add(new IParameter(_ICDCM1DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC1)));
                parameter.Add(new IParameter(_ICDCM1DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC2)));
                parameter.Add(new IParameter(_ICDCM1DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC3)));
                parameter.Add(new IParameter(_ICDCM1DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM1DOC4)));

                parameter.Add(new IParameter(_ICDCM2DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC1)));
                parameter.Add(new IParameter(_ICDCM2DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC2)));
                parameter.Add(new IParameter(_ICDCM2DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC3)));
                parameter.Add(new IParameter(_ICDCM2DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM2DOC4)));

                parameter.Add(new IParameter(_ICDCM3DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC1)));
                parameter.Add(new IParameter(_ICDCM3DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC2)));
                parameter.Add(new IParameter(_ICDCM3DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC3)));
                parameter.Add(new IParameter(_ICDCM3DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM3DOC4)));

                parameter.Add(new IParameter(_ICDCM4DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC1)));
                parameter.Add(new IParameter(_ICDCM4DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC2)));
                parameter.Add(new IParameter(_ICDCM4DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC3)));
                parameter.Add(new IParameter(_ICDCM4DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM4DOC4)));

                parameter.Add(new IParameter(_ICDCM5DOC1, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC1)));
                parameter.Add(new IParameter(_ICDCM5DOC2, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC2)));
                parameter.Add(new IParameter(_ICDCM5DOC3, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC3)));
                parameter.Add(new IParameter(_ICDCM5DOC4, IDbType.VarChar, DBNullConvert.From(_POSTORICDVO.ICDCM5DOC4)));

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
                sbQuery.Append("DELETE FROM " + _tblPOSTORICD);
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

        internal ReturnValue DeleteByID(string _ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORICD);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ID)));
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
