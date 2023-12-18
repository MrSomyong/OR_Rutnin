using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORNURSE : DataAccess
    {
        private static string _tblPOSTORNURSE = "POSTORNURSE";
        private static string _VT_NURSEMASTER = "VT_NURSEMASTER";
        private static string _CODE = "CODE";
        private static string _NAME = "NAME";

        private static string _ORID = "ORID";
        private static string _Suffix = "Suffix";
        private static string _NurseType = "NurseType";
        private static string _NurseCode = "NurseCode";
        private static string _Remark = "Remark";


        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORNURSE() { }
        public DAPOSTORNURSE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORNURSE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORNURSE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORNURSEVO> SearchByKey(POSTORNURSEVO _POSTORNURSEVO)
        {
            List<POSTORNURSEVO> retValue = new List<POSTORNURSEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _NAME + " from " + _tblPOSTORNURSE + " as a ");
                strQuery.Append(" left join " + _VT_NURSEMASTER + " as b on a." + _NurseCode + " = b." + _CODE);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORNURSEVO.ORID))
                {
                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
                }
                if (_POSTORNURSEVO.Suffix > 0)
                {
                    strQuery.Append(" and " + _Suffix + " = @" + _Suffix);
                }
                strQuery.Append(" order by " + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.ORID)));
                parameter.Add(new IParameter(_Suffix, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.Suffix, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
                    POSTORNURSEVO.ORID = query[_ORID].ToString();
                    POSTORNURSEVO.Suffix = ADOUtil.GetIntFromQuery(query[_Suffix].ToString());
                    POSTORNURSEVO.NurseType = ADOUtil.GetIntFromQuery(query[_NurseType].ToString());
                    POSTORNURSEVO.NurseTypeDesc = ((EnumOR.NurseType)POSTORNURSEVO.NurseType).ToString();
                    POSTORNURSEVO.NurseCode = query[_NurseCode].ToString();
                    POSTORNURSEVO.Nurse = query[_NAME].ToString();
                    POSTORNURSEVO.Remark = query[_Remark].ToString();
                    retValue.Add(POSTORNURSEVO);
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

        internal int GetSuffixNext(string orid)
        {
            int SuffixNext = 1;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT MAX(Suffix) AS SuffixMax FROM " + _tblPOSTORNURSE);
                strQuery.Append(" where " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(orid)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SuffixNext = ADOUtil.GetIntFromQuery(query["SuffixMax"].ToString()) + 1;
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SuffixNext;
        }

        internal ReturnValue Insert(POSTORNURSEVO _POSTORNURSEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORNURSE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ORID);
                sbValue.Append("@" + _ORID);

                sbInsert.Append("," + _Suffix);
                sbValue.Append(",@" + _Suffix);

                sbInsert.Append("," + _NurseType);
                sbValue.Append(",@" + _NurseType);

                sbInsert.Append("," + _NurseCode);
                sbValue.Append(",@" + _NurseCode);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.ORID)));
                parameter.Add(new IParameter(_Suffix, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.Suffix, false)));
                parameter.Add(new IParameter(_NurseType, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.NurseType, false)));
                parameter.Add(new IParameter(_NurseCode, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.NurseCode)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.Remark)));
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

        internal ReturnValue Update(POSTORNURSEVO _POSTORNURSEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORNURSE + " SET ");
                sbQuery.Append("" + _NurseType + " = @" + _NurseType);
                sbQuery.Append("," + _NurseCode + " = @" + _NurseCode);
                sbQuery.Append("," + _Remark + " = @" + _Remark);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);
                sbQuery.Append(" AND " + _Suffix + " = @" + _Suffix);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.ORID)));
                parameter.Add(new IParameter(_Suffix, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.Suffix, false)));
                parameter.Add(new IParameter(_NurseType, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.NurseType, false)));
                parameter.Add(new IParameter(_NurseCode, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.NurseCode)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.Remark)));

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
                sbQuery.Append("DELETE FROM " + _tblPOSTORNURSE);
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

        internal ReturnValue Delete(string ORID, int Suffix)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORNURSE);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);
                sbQuery.Append(" AND " + _Suffix + " = @" + _Suffix);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
                parameter.Add(new IParameter(_Suffix, IDbType.Int, DBNullConvert.From(Suffix, false)));
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
