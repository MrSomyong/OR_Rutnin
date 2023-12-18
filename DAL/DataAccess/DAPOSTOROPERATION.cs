using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTOROPERATION : DataAccess
    {
        private static string _tblPOSTOROPERATION = "POSTOROPERATION";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";
        private static string _tblORHEADER = "ORHEADER";

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

        private static string _Surgeon1 = "Surgeon1";
        private static string _Surgeon2 = "Surgeon2";
        private static string _Surgeon3 = "Surgeon3";
        private static string _Complication = "Complication";
        private static string _ICD = "ICD";
        private static string _Organ = "Organ";
        private static string _OrganPosition = "OrganPosition";
        private static string _ORProcedureType = "ORProcedureType";

        private static string _strComplication = "strComplication";
        private static string _ORDate = "ORDate";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAPOSTOROPERATION() { }
        public DAPOSTOROPERATION(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTOROPERATION(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTOROPERATION(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTOROPERATIONVO> SearchAll()
        {
            List<POSTOROPERATIONVO> retValue = new List<POSTOROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPOSTOROPERATION); 

                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                    POSTOROPERATIONVO.ID = query[_ID].ToString();
                    POSTOROPERATIONVO.ORID = query[_ORID].ToString();
                    POSTOROPERATIONVO.MainCode = query[_MainCode].ToString();
                    POSTOROPERATIONVO.SubCode = query[_SubCode].ToString();
                    POSTOROPERATIONVO.SubName = query[_SubName].ToString();
                    POSTOROPERATIONVO.Side = int.Parse(query[_Side].ToString());
                    POSTOROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    POSTOROPERATIONVO.SubMark = query[_SubMark].ToString();
                    POSTOROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    POSTOROPERATIONVO.Surgeon1 = query[_Surgeon1].ToString();
                    POSTOROPERATIONVO.Surgeon2 = query[_Surgeon2].ToString();
                    POSTOROPERATIONVO.Surgeon3 = query[_Surgeon3].ToString();
                    POSTOROPERATIONVO.Complication = query[_Complication].ToString();
                    POSTOROPERATIONVO.strComplication = query[_strComplication].ToString();
                    retValue.Add(POSTOROPERATIONVO);
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

        internal List<POSTOROPERATIONVO> SearchByKey(POSTOROPERATIONVO _POSTOROPERATIONVO)
        {
            List<POSTOROPERATIONVO> retValue = new List<POSTOROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a." + _ID + ", a." + _ORID + ", a." + _ORID + ", a." + _MainCode + ", a." + _SubCode + ", a." + _SubName
                                   + " as " + _SubNameFt + ", a." + _Reamrk + ", a." + _Seq
                                   + ", a." + _Surgeon1 + ", a." + _Surgeon2 + ", a." + _Surgeon3);
                strQuery.Append(" ,a." + _Side + ", a." + _SubMark + ", b." + _SubName + ", c." + _Name);
                strQuery.Append(" , d." + _ORDate + ", e.NAME as AnesthesiaTypeName");
                strQuery.Append(" from " + _tblPOSTOROPERATION +" a");
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " as b on a." + _SubCode + " = b." + _SubCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " as c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" left join " + _tblORHEADER + " as d on a." + _ORID + " = d." + _ORID);
                strQuery.Append(" left join VT_ANESTHESIA as e on e.CODE = d.AnesthesiaType1");
                strQuery.Append(" where 1=1");
                if (!string.IsNullOrEmpty(_POSTOROPERATIONVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_POSTOROPERATIONVO.ORID))
                {
                    strQuery.Append(" and a." + _ORID + " = @" + _ORID);
                }
                strQuery.Append(" order by a." + _Side + ", a." + _Seq);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ORID)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                    POSTOROPERATIONVO.ID = query[_ID].ToString();
                    POSTOROPERATIONVO.ORID = query[_ORID].ToString();
                    POSTOROPERATIONVO.MainCode = query[_MainCode].ToString();
                    POSTOROPERATIONVO.SubCode = query[_SubCode].ToString();
                    POSTOROPERATIONVO.SubName = query[_SubName].ToString();
                    POSTOROPERATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    POSTOROPERATIONVO.strSide = ((EnumOR.ORSide)POSTOROPERATIONVO.Side).ToString();
                    POSTOROPERATIONVO.Name = query[_Name].ToString();
                    POSTOROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    POSTOROPERATIONVO.SubMark = query[_SubMark].ToString();
                    if (string.IsNullOrEmpty(POSTOROPERATIONVO.SubName))
                    {
                        POSTOROPERATIONVO.SubName = query[_SubNameFt].ToString();
                    }
                    if (POSTOROPERATIONVO.SubMark == "0")
                        POSTOROPERATIONVO.strSubMark = "";
                    else if (POSTOROPERATIONVO.SubMark == "1")
                        POSTOROPERATIONVO.strSubMark = "+";
                    else if (POSTOROPERATIONVO.SubMark == "2")
                        POSTOROPERATIONVO.strSubMark = "+-";
                    else if (POSTOROPERATIONVO.SubMark == "3")
                        POSTOROPERATIONVO.strSubMark = "/";
                    POSTOROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    POSTOROPERATIONVO.Surgeon1 = query[_Surgeon1].ToString();
                    POSTOROPERATIONVO.Surgeon2 = query[_Surgeon2].ToString();
                    POSTOROPERATIONVO.Surgeon3 = query[_Surgeon3].ToString();
                    POSTOROPERATIONVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    POSTOROPERATIONVO.strORDate = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).ToString()), YearType.English).ToString("dd MMM yyyy");
                    POSTOROPERATIONVO.AnesthesiaTypeName = query["AnesthesiaTypeName"].ToString();
                    retValue.Add(POSTOROPERATIONVO);
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

        internal List<POSTOROPERATIONVO> SearchByORID(string orid)
        {
            List<POSTOROPERATIONVO> retValue = new List<POSTOROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select a." + _ID + ", a." + _ORID + ", a." + _ORID + ", a." + _MainCode + ", a." + _SubCode + ", a." + _SubName
                                   + " as " + _SubNameFt + ", a." + _Reamrk + ", a." + _Seq
                                   + ", a." + _Surgeon1 + ", a." + _Surgeon2 + ", a." + _Surgeon3);
                strQuery.Append(" ,a." + _Side + ", a." + _SubMark + ", b." + _SubName + ", c." + _Name);
                strQuery.Append(" from "+_tblPOSTOROPERATION+" as a");
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
                    POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                    POSTOROPERATIONVO.ID = query[_ID].ToString();
                    POSTOROPERATIONVO.ORID = query[_ORID].ToString();
                    POSTOROPERATIONVO.MainCode = query[_MainCode].ToString();
                    POSTOROPERATIONVO.SubCode = query[_SubCode].ToString();
                    POSTOROPERATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    POSTOROPERATIONVO.strSide = ((EnumOR.ORSide)int.Parse(query[_Side].ToString())).ToString();

                    POSTOROPERATIONVO.SubMark = query[_SubMark].ToString();
                    if (POSTOROPERATIONVO.SubMark == "0")
                        POSTOROPERATIONVO.strSubMark = "";
                    else if (POSTOROPERATIONVO.SubMark == "1")
                        POSTOROPERATIONVO.strSubMark = "+";
                    else if (POSTOROPERATIONVO.SubMark == "2")
                        POSTOROPERATIONVO.strSubMark = "+-";
                    else if (POSTOROPERATIONVO.SubMark == "3")
                        POSTOROPERATIONVO.strSubMark = "/";
                    POSTOROPERATIONVO.SubName = query[_SubName].ToString();
                    if (string.IsNullOrEmpty(POSTOROPERATIONVO.SubName))
                    {
                        POSTOROPERATIONVO.SubName = query[_SubNameFt].ToString();
                    }
                    POSTOROPERATIONVO.Name = query[_Name].ToString();
                    POSTOROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    POSTOROPERATIONVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    POSTOROPERATIONVO.Surgeon1 = query[_Surgeon1].ToString();
                    POSTOROPERATIONVO.Surgeon2 = query[_Surgeon2].ToString();
                    POSTOROPERATIONVO.Surgeon3 = query[_Surgeon3].ToString();

                    retValue.Add(POSTOROPERATIONVO);
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

        internal List<POSTOROPERATIONVO> SearchByID(string id)
        {
            List<POSTOROPERATIONVO> retValue = new List<POSTOROPERATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select * from " + _tblPOSTOROPERATION);
                strQuery.Append(" where " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(id)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                    POSTOROPERATIONVO.ID = query[_ID].ToString();
                    POSTOROPERATIONVO.Surgeon1 = query[_Surgeon1].ToString();
                    POSTOROPERATIONVO.Surgeon2 = query[_Surgeon2].ToString();
                    POSTOROPERATIONVO.Surgeon3 = query[_Surgeon3].ToString();
                    POSTOROPERATIONVO.Complication = query[_Complication].ToString();
                    POSTOROPERATIONVO.ICD = query[_ICD].ToString();
                    POSTOROPERATIONVO.Organ = query[_Organ].ToString();
                    POSTOROPERATIONVO.OrganPosition = query[_OrganPosition].ToString();
                    POSTOROPERATIONVO.ORProcedureType = query[_ORProcedureType].ToString();
                    POSTOROPERATIONVO.strComplication = query[_strComplication].ToString();
                    retValue.Add(POSTOROPERATIONVO);
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

        internal ReturnValue Insert(POSTOROPERATIONVO _POSTOROPERATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTOROPERATION + " (");
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

                sbInsert.Append("," + _Surgeon1);
                sbValue.Append(",@" + _Surgeon1);

                sbInsert.Append("," + _Surgeon2);
                sbValue.Append(",@" + _Surgeon2);

                sbInsert.Append("," + _Surgeon3);
                sbValue.Append(",@" + _Surgeon3);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ORID)));                
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.SubName)));
                parameter.Add(new IParameter(_Side, IDbType.Int, DBNullConvert.From(_POSTOROPERATIONVO.Side,false)));
                parameter.Add(new IParameter(_Reamrk, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Reamrk)));
                parameter.Add(new IParameter(_SubMark, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.SubMark)));
                parameter.Add(new IParameter(_Seq, IDbType.Int, DBNullConvert.From(_POSTOROPERATIONVO.Seq, false)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon3)));
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

        internal ReturnValue UpdateSetupProcedure(POSTOROPERATIONVO _POSTOROPERATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTOROPERATION + " SET ");
                sbQuery.Append("" + _Surgeon1 + " = @" + _Surgeon1);
                sbQuery.Append("," + _Surgeon2 + " = @" + _Surgeon2);
                sbQuery.Append("," + _Surgeon3 + " = @" + _Surgeon3);
                sbQuery.Append("," + _Complication + " = @" + _Complication);
                sbQuery.Append("," + _strComplication + " = @" + _strComplication);
                sbQuery.Append("," + _ICD + " = @" + _ICD);
                sbQuery.Append("," + _Organ + " = @" + _Organ);
                sbQuery.Append("," + _OrganPosition + " = @" + _OrganPosition);
                sbQuery.Append("," + _ORProcedureType + " = @" + _ORProcedureType);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ID)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Surgeon3)));
                parameter.Add(new IParameter(_Complication, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Complication)));
                parameter.Add(new IParameter(_strComplication, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.strComplication)));
                parameter.Add(new IParameter(_ICD, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ICD)));
                parameter.Add(new IParameter(_Organ, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.Organ)));
                parameter.Add(new IParameter(_OrganPosition, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.OrganPosition)));
                parameter.Add(new IParameter(_ORProcedureType, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ORProcedureType)));

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

        internal ReturnValue DeleteByID(POSTOROPERATIONVO _POSTOROPERATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTOROPERATION);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTOROPERATIONVO.ID)));
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
                sbQuery.Append("DELETE FROM " + _tblPOSTOROPERATION);
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
