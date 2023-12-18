using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAOROPERATION : DataAccess
    {
        private static string _tblOROPERATION = "OROPERATION";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";

        private static string _ID = "ID";
        private static string _ORID = "ORID";
        private static string _MainCode = "MainCode";
        private static string _SubCode = "SubCode";
        private static string _SubNameFt = "SubNameFt";
        private static string _Side = "Side";
        private static string _strSide = "strSide";
        private static string _Reamrk = "Reamrk";
        private static string _SubMark = "SubMark";
        private static string _strSubMark = "strSubMark";
        private static string _Name = "Name";
        private static string _SubName = "SubName";
        private static string _Seq = "Seq";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAOROPERATION() { }
        public DAOROPERATION(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAOROPERATION(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAOROPERATION(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<OROPERATIONVO> SearchAll()
        {
            List<OROPERATIONVO> retValue = new List<OROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblOROPERATION);

                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    OROPERATIONVO.ID = query[_ID].ToString();
                    OROPERATIONVO.ORID = query[_ORID].ToString();
                    OROPERATIONVO.MainCode = query[_MainCode].ToString();
                    OROPERATIONVO.SubCode = query[_SubCode].ToString();
                    OROPERATIONVO.SubName = query[_SubName].ToString();
                    OROPERATIONVO.Side = int.Parse(query[_Side].ToString());
                    OROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    OROPERATIONVO.SubMark = query[_SubMark].ToString();
                    OROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    retValue.Add(OROPERATIONVO);
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

        internal List<OROPERATIONVO> SearchByKey(OROPERATIONVO _OROPERATIONVO)
        {
            List<OROPERATIONVO> retValue = new List<OROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblOROPERATION + " where 1=1 ");
                if (!string.IsNullOrEmpty(_OROPERATIONVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_OROPERATIONVO.ORID))
                {
                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
                }
                strQuery.Append(" order by " + _Side + ", " + _Seq);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.ORID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    OROPERATIONVO.ID = query[_ID].ToString();
                    OROPERATIONVO.ORID = query[_ORID].ToString();
                    OROPERATIONVO.MainCode = query[_MainCode].ToString();
                    OROPERATIONVO.SubCode = query[_SubCode].ToString();
                    OROPERATIONVO.SubName = query[_SubName].ToString();
                    OROPERATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    OROPERATIONVO.strSide = ((EnumOR.ORSide)OROPERATIONVO.Side).ToString();
                    OROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    OROPERATIONVO.SubMark = query[_SubMark].ToString();
                    OROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    retValue.Add(OROPERATIONVO);
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

        internal List<OROPERATIONVO> SearchByORID(string orid)
        {
            List<OROPERATIONVO> retValue = new List<OROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select a." + _ID + ", a." + _ORID + ", a." + _ORID + ", a." + _MainCode + ", a." + _SubCode + ", a." + _SubName
                                   + " as " + _SubNameFt + ", a." + _Reamrk + ", a." + _Seq);
                strQuery.Append(" ,a." + _Side + ", a." + _SubMark + ", b." + _SubName + ", c." + _Name);
                strQuery.Append(" from " + _tblOROPERATION + " as a");
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " as b on a." + _SubCode + " = b." + _SubCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " as c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" where a." + _ORID + " = @" + _ORID + " Order By a." + _Side + ", a." + _Seq);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(orid)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    OROPERATIONVO.ID = query[_ID].ToString();
                    OROPERATIONVO.ORID = query[_ORID].ToString();
                    OROPERATIONVO.MainCode = query[_MainCode].ToString();
                    OROPERATIONVO.SubCode = query[_SubCode].ToString();
                    OROPERATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    OROPERATIONVO.strSide = ((EnumOR.ORSide)OROPERATIONVO.Side).ToString();
                    OROPERATIONVO.SubMark = query[_SubMark].ToString();
                    if (OROPERATIONVO.SubMark == "0")
                        OROPERATIONVO.strSubMark = "";
                    else if (OROPERATIONVO.SubMark == "1")
                        OROPERATIONVO.strSubMark = "+";
                    else if (OROPERATIONVO.SubMark == "2")
                        OROPERATIONVO.strSubMark = "+-";
                    else if (OROPERATIONVO.SubMark == "3")
                        OROPERATIONVO.strSubMark = "/";
                    OROPERATIONVO.SubName = query[_SubName].ToString();
                    if (string.IsNullOrEmpty(OROPERATIONVO.SubName))
                    {
                        OROPERATIONVO.SubName = query[_SubNameFt].ToString();
                    }
                    OROPERATIONVO.Name = query[_Name].ToString();
                    OROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    OROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());

                    retValue.Add(OROPERATIONVO);
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

        internal List<OROPERATIONVO> SearchInORID(List<ORHEADERVO> _lstORHEADERVO)
        {
            List<OROPERATIONVO> retValue = new List<OROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select a." + _ID + ", a." + _ORID + ", a." + _ORID + ", a." + _MainCode + ", a." + _SubCode + ", a." + _SubName
                                   + " as " + _SubNameFt + ", a." + _Reamrk + ", a." + _Seq);
                strQuery.Append(" ,a." + _Side + ", a." + _SubMark + ", b." + _SubName + ", c." + _Name);
                strQuery.Append(" from " + _tblOROPERATION + " as a");
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " as b on a." + _SubCode + " = b." + _SubCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " as c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" where a." + _ORID + " in (");
                int i = 1;
                foreach (ORHEADERVO ORHEADERVO in _lstORHEADERVO)
                {
                    strQuery.Append(" " + ORHEADERVO.ORID);
                    if (i < _lstORHEADERVO.Count)
                    {
                        strQuery.Append(",");
                    }
                    i++;
                }
                strQuery.Append(")");
                strQuery.Append(" Order By a." + _Side + ", a." + _Seq);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                i = 1;
                foreach (ORHEADERVO ORHEADERVO in _lstORHEADERVO)
                {
                    parameter.Add(new IParameter(_ORID + i, IDbType.VarChar, DBNullConvert.From(ORHEADERVO.ORID)));
                    i++;
                }

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    OROPERATIONVO.ID = query[_ID].ToString();
                    OROPERATIONVO.ORID = query[_ORID].ToString();
                    OROPERATIONVO.MainCode = query[_MainCode].ToString();
                    OROPERATIONVO.SubCode = query[_SubCode].ToString();
                    OROPERATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    OROPERATIONVO.strSide = ((EnumOR.ORSide)OROPERATIONVO.Side).ToString();
                    OROPERATIONVO.SubMark = query[_SubMark].ToString();
                    if (OROPERATIONVO.SubMark == "0")
                        OROPERATIONVO.strSubMark = "";
                    else if (OROPERATIONVO.SubMark == "1")
                        OROPERATIONVO.strSubMark = "+";
                    else if (OROPERATIONVO.SubMark == "2")
                        OROPERATIONVO.strSubMark = "+-";
                    else if (OROPERATIONVO.SubMark == "3")
                        OROPERATIONVO.strSubMark = "/";
                    OROPERATIONVO.SubName = query[_SubName].ToString();
                    if (string.IsNullOrEmpty(OROPERATIONVO.SubName))
                    {
                        OROPERATIONVO.SubName = query[_SubNameFt].ToString();
                    }
                    OROPERATIONVO.Name = query[_Name].ToString();
                    OROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    OROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());

                    retValue.Add(OROPERATIONVO);
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

        internal ReturnValue Insert(OROPERATIONVO _OROPERATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblOROPERATION + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _ORID);
                sbValue.Append(",@" + _ORID);

                sbInsert.Append("," + _MainCode);
                sbValue.Append(",@" + _MainCode);

                sbInsert.Append("," + _SubCode);
                sbValue.Append(",@" + _SubCode);

                sbInsert.Append("," + _SubName);
                sbValue.Append(",@" + _SubName);

                sbInsert.Append("," + _Side);
                sbValue.Append(",@" + _Side);

                sbInsert.Append("," + _Reamrk);
                sbValue.Append(",@" + _Reamrk);

                sbInsert.Append("," + _SubMark);
                sbValue.Append(",@" + _SubMark);

                sbInsert.Append("," + _Seq);
                sbValue.Append(",@" + _Seq);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.ORID)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.SubName)));
                parameter.Add(new IParameter(_Side, IDbType.Int, DBNullConvert.From(_OROPERATIONVO.Side, false)));
                parameter.Add(new IParameter(_Reamrk, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.Reamrk)));
                parameter.Add(new IParameter(_SubMark, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.SubMark)));
                parameter.Add(new IParameter(_Seq, IDbType.Int, DBNullConvert.From(_OROPERATIONVO.Seq, false)));
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

        internal ReturnValue DeleteByID(OROPERATIONVO _OROPERATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblOROPERATION);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_OROPERATIONVO.ID)));
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

        internal ReturnValue DeleteByORID(string orid)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblOROPERATION);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(orid)));
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
