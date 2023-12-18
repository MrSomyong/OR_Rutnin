using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPOPERATIONSUB : DataAccess
    {
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";
        private static string _tblSETUPICDCM = "SETUPICDCM";
        private static string _tblSETUPORGANMAIN = "SETUPORGANMAIN";
        private static string _tblSETUPORGANSUB = "SETUPORGANSUB";

        private static string _SubCode = "SubCode";
        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _Remark = "Remark";
        private static string _SubName = "SubName";
        private static string _SubRemark = "SubRemark";
        private static string _Code = "Code";
        private static string _ICDCM = "ICDCM";
        private static string _ICDCMName = "ICDCMName";
        private static string _ORProcedureType = "ORProcedureType";
        private static string _ORProcedureTypeName = "ORProcedureTypeName";
        private static string _ORGANMAIN = "ORGANMAIN";
        private static string _ORGANMAINName = "ORGANMAINName";
        private static string _ORGANSUB = "ORGANSUB";
        private static string _ORGANSUBName = "ORGANSUBName";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPOPERATIONSUB() { }
        public DASETUPOPERATIONSUB(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPOPERATIONSUB(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPOPERATIONSUB(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPOPERATIONSUBVO> SearchAll()
        {
            List<SETUPOPERATIONSUBVO> retValue = new List<SETUPOPERATIONSUBVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPOPERATIONSUB);
                strQuery.Append(" Order by " + _SubName);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                    SETUPOPERATIONSUBVO.MainCode = query[_MainCode].ToString();
                    SETUPOPERATIONSUBVO.SubCode = query[_SubCode].ToString();
                    SETUPOPERATIONSUBVO.SubName = query[_SubName].ToString();
                    SETUPOPERATIONSUBVO.SubRemark = query[_SubRemark].ToString();

                    retValue.Add(SETUPOPERATIONSUBVO);
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

        internal List<SETUPOPERATIONSUBVO> SearchByKey(SETUPOPERATIONSUBVO _SETUPOPERATIONSUBVO)
        {
            List<SETUPOPERATIONSUBVO> retValue = new List<SETUPOPERATIONSUBVO>();
            try
            {

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Name + ", c." + _Name + " as " + _ICDCMName);
                strQuery.Append(", d." + _Name + " as " + _ORGANMAINName);
                strQuery.Append(", e." + _SubName + " as " + _ORGANSUBName);
                strQuery.Append(" from " + _tblSETUPOPERATIONSUB + " as a");
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " as b on a." + _MainCode + " = b." + _MainCode);
                strQuery.Append(" left join " + _tblSETUPICDCM + " as c on a." + _ICDCM + " = c." + _Code);
                strQuery.Append(" left join " + _tblSETUPORGANMAIN + " as d on a." + _ORGANMAIN + " = d." + _MainCode);
                strQuery.Append(" left join " + _tblSETUPORGANSUB + " as e on a." + _ORGANMAIN + " = e." + _MainCode + " and a."+ _ORGANSUB + " = e."+ _SubCode);
                strQuery.Append(" where 1 = 1");
                if (!string.IsNullOrEmpty(_SETUPOPERATIONSUBVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_SETUPOPERATIONSUBVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                if (!string.IsNullOrEmpty(_SETUPOPERATIONSUBVO.ICDCM))
                {
                    strQuery.Append(" and a." + _ICDCM + " = @" + _ICDCM);
                }
                if (_SETUPOPERATIONSUBVO.ORProcedureType > 0)
                {
                    strQuery.Append(" and a." + _ORProcedureType + " = @" + _ORProcedureType);
                }
                if (!string.IsNullOrEmpty(_SETUPOPERATIONSUBVO.ORGANMAIN))
                {
                    strQuery.Append(" and a." + _ORGANMAIN + " = @" + _ORGANMAIN);
                }
                if (!string.IsNullOrEmpty(_SETUPOPERATIONSUBVO.ORGANSUB))
                {
                    strQuery.Append(" and a." + _ORGANSUB + " = @" + _ORGANSUB);
                }
                strQuery.Append(" Order by a." + _SubName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubCode)));
                parameter.Add(new IParameter(_ICDCM, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ICDCM)));
                parameter.Add(new IParameter(_ORProcedureType, IDbType.Int, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORProcedureType, true)));
                parameter.Add(new IParameter(_ORGANMAIN, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANMAIN)));
                parameter.Add(new IParameter(_ORGANSUB, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANSUB)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                    SETUPOPERATIONSUBVO.MainCode = query[_MainCode].ToString();
                    SETUPOPERATIONSUBVO.Name = query[_Name].ToString();
                    SETUPOPERATIONSUBVO.SubCode = query[_SubCode].ToString();
                    SETUPOPERATIONSUBVO.SubName = query[_SubName].ToString();
                    SETUPOPERATIONSUBVO.SubRemark = query[_SubRemark].ToString();
                    SETUPOPERATIONSUBVO.ICDCM = query[_ICDCM].ToString();
                    SETUPOPERATIONSUBVO.ICDCMName = query[_ICDCMName].ToString();
                    SETUPOPERATIONSUBVO.ORProcedureType = ADOUtil.GetIntFromQuery(query[_ORProcedureType].ToString());
                    if (SETUPOPERATIONSUBVO.ORProcedureType > 0)
                        SETUPOPERATIONSUBVO.ORProcedureTypeName = ((EnumOR.ORProcedureType)ADOUtil.GetIntFromQuery(query[_ORProcedureType].ToString())).ToString();
                    SETUPOPERATIONSUBVO.ORGANMAIN = query[_ORGANMAIN].ToString();
                    SETUPOPERATIONSUBVO.ORGANMAINName = query[_ORGANMAINName].ToString();
                    SETUPOPERATIONSUBVO.ORGANSUB = query[_ORGANSUB].ToString();
                    SETUPOPERATIONSUBVO.ORGANSUBName = query[_ORGANSUBName].ToString();
                    retValue.Add(SETUPOPERATIONSUBVO);
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
                strQuery.Append(" select a.*, b." + _Name + " from " + _tblSETUPOPERATIONSUB + " as a");
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " as b on a." + _MainCode + " = b." + _MainCode);
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

        internal ReturnValue Insert(SETUPOPERATIONSUBVO _SETUPOPERATIONSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPOPERATIONSUB + " (");
                sbValue.Append(" VALUES(");


                sbInsert.Append(_MainCode);
                sbValue.Append("@" + _MainCode);

                sbInsert.Append("," + _SubCode);
                sbValue.Append(",@" + _SubCode);

                sbInsert.Append("," + _SubName);
                sbValue.Append(",@" + _SubName);

                sbInsert.Append("," + _SubRemark);
                sbValue.Append(",@" + _SubRemark);

                sbInsert.Append("," + _ICDCM);
                sbValue.Append(",@" + _ICDCM);

                sbInsert.Append("," + _ORProcedureType);
                sbValue.Append(",@" + _ORProcedureType);

                sbInsert.Append("," + _ORGANMAIN);
                sbValue.Append(",@" + _ORGANMAIN);

                sbInsert.Append("," + _ORGANSUB);
                sbValue.Append(",@" + _ORGANSUB);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubName)));
                parameter.Add(new IParameter(_SubRemark, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubRemark)));
                parameter.Add(new IParameter(_ICDCM, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ICDCM)));
                parameter.Add(new IParameter(_ORProcedureType, IDbType.Int, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORProcedureType, true)));
                parameter.Add(new IParameter(_ORGANMAIN, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANMAIN)));
                parameter.Add(new IParameter(_ORGANSUB, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANSUB)));

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

        internal ReturnValue Update(SETUPOPERATIONSUBVO _SETUPOPERATIONSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPOPERATIONSUB + " SET ");

                sbQuery.Append("" + _SubName + " = @" + _SubName);
                sbQuery.Append("," + _SubRemark + " = @" + _SubRemark);
                sbQuery.Append("," + _ICDCM + " = @" + _ICDCM);
                sbQuery.Append("," + _ORProcedureType + " = @" + _ORProcedureType);
                sbQuery.Append("," + _ORGANMAIN + " = @" + _ORGANMAIN);
                sbQuery.Append("," + _ORGANSUB + " = @" + _ORGANSUB);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);
                sbQuery.Append(" and " + _SubCode + " = @" + _SubCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubName)));
                parameter.Add(new IParameter(_SubRemark, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.SubRemark)));
                parameter.Add(new IParameter(_ICDCM, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ICDCM)));
                parameter.Add(new IParameter(_ORProcedureType, IDbType.Int, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORProcedureType, true)));
                parameter.Add(new IParameter(_ORGANMAIN, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANMAIN)));
                parameter.Add(new IParameter(_ORGANSUB, IDbType.VarChar, DBNullConvert.From(_SETUPOPERATIONSUBVO.ORGANSUB)));

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

        internal ReturnValue DeleteByMain(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPOPERATIONSUB);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(SETUPOPERATIONSUBVO.MainCode)));
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

        internal ReturnValue Delete(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPOPERATIONSUB);
                sbQuery.Append(" WHERE " + _MainCode + " = @" + _MainCode);
                sbQuery.Append(" and " + _SubCode + " = @" + _SubCode);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(SETUPOPERATIONSUBVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(SETUPOPERATIONSUBVO.SubCode)));
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
