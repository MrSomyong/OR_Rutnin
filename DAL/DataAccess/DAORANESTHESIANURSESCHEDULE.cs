using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORANESTHESIANURSESCHEDULE : DataAccess
    {
        private static string _tblORANESTHESIANURSESCHEDULE = "ORANESTHESIANURSESCHEDULE";
        private static string _VT_NURSEMASTER = "VT_NURSEMASTER";

        private static string _ID = "ID";
        private static string _CODE = "CODE";
        private static string _NURSE = "NURSE";
        private static string _Name = "Name";
        private static string _Reamrk = "Reamrk";
        private static string _StartAnesthesiaDateTime = "StartAnesthesiaDateTime";
        private static string _strStartAnesthesiaDateTime = "strStartAnesthesiaDateTime";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAORANESTHESIANURSESCHEDULE() { }
        public DAORANESTHESIANURSESCHEDULE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORANESTHESIANURSESCHEDULE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORANESTHESIANURSESCHEDULE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORANESTHESIANURSESCHEDULEVO> SearchByKey(ORANESTHESIANURSESCHEDULEVO _ORANESTHESIANURSESCHEDULEVO)
        {
            List<ORANESTHESIANURSESCHEDULEVO> retValue = new List<ORANESTHESIANURSESCHEDULEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Name + " from " + _tblORANESTHESIANURSESCHEDULE + " as a ");
                strQuery.Append(" left join " + _VT_NURSEMASTER + " as b on a." + _NURSE + " = b." + _CODE);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORANESTHESIANURSESCHEDULEVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime.ToString()))
                {
                    strQuery.Append(" and CONVERT(date, a. " + _StartAnesthesiaDateTime + ", 126) = CONVERT(date, @"+ _StartAnesthesiaDateTime + ", 126) ");
                }

                strQuery.Append(" order by a." + _StartAnesthesiaDateTime + ", b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORANESTHESIANURSESCHEDULEVO.ID)));
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(_ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
                    ORANESTHESIANURSESCHEDULEVO.ID = query[_ID].ToString();
                    ORANESTHESIANURSESCHEDULEVO.NURSE = query[_NURSE].ToString();
                    ORANESTHESIANURSESCHEDULEVO.Name = query[_Name].ToString();
                    ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = ADOUtil.GetDateFromQuery(query[_StartAnesthesiaDateTime].ToString());
                    ORANESTHESIANURSESCHEDULEVO.strStartAnesthesiaDateTime = ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime.Value.ToString("dd-MM-yyyy HH:mm");
                    ORANESTHESIANURSESCHEDULEVO.Reamrk = query[_Reamrk].ToString();
                    retValue.Add(ORANESTHESIANURSESCHEDULEVO);
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

        internal DataTable SearchByDate_DS(DateTime StartAnesthesiaDateTime)
        {
            DataTable table = new DataTable();
            table.Columns.Add(_ID, typeof(String));
            table.Columns.Add(_NURSE, typeof(String));
            table.Columns.Add(_Name, typeof(String));
            table.Columns.Add(_StartAnesthesiaDateTime, typeof(DateTime));
            table.Columns.Add(_strStartAnesthesiaDateTime, typeof(String));
            table.Columns.Add(_Reamrk, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Name + " from " + _tblORANESTHESIANURSESCHEDULE + " as a ");
                strQuery.Append(" left join " + _VT_NURSEMASTER + " as b on a." + _NURSE + " = b." + _CODE);
                strQuery.Append(" where 1 = 1 ");
                if (!string.IsNullOrEmpty(StartAnesthesiaDateTime.ToString()))
                {
                    strQuery.Append(" and CONVERT(date, a. " + _StartAnesthesiaDateTime + ", 126) = CONVERT(date, @" + _StartAnesthesiaDateTime + ", 126) ");
                }

                strQuery.Append(" order by a." + _StartAnesthesiaDateTime + ", b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(StartAnesthesiaDateTime)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DataRow row = table.NewRow();
                    row[_ID] = query[_ID].ToString();
                    row[_NURSE] = query[_NURSE].ToString();
                    row[_Name] = query[_Name].ToString();
                    row[_StartAnesthesiaDateTime] = query[_StartAnesthesiaDateTime].ToString();
                    row[_strStartAnesthesiaDateTime] = ADOUtil.GetDateFromQuery(query[_StartAnesthesiaDateTime].ToString()).Value.ToString("dd-MM-yyyy HH:mm");
                    row[_Reamrk] = query[_Reamrk].ToString();

                    table.Rows.Add(row);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return table;
        }

        internal ReturnValue Checkdup(ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblORANESTHESIANURSESCHEDULE);
                strQuery.Append(" where " + _NURSE + " = @" + _NURSE);
                strQuery.Append(" and " + _StartAnesthesiaDateTime + " = @" + _StartAnesthesiaDateTime);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_NURSE, IDbType.VarChar, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.NURSE)));
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch(Exception ex)
            {
                retVal.Exception = ex;
                retVal.Value = false;
            }
            return retVal;
        }

        internal ReturnValue Insert(ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblORANESTHESIANURSESCHEDULE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _NURSE);
                sbValue.Append(",@" + _NURSE);

                sbInsert.Append("," + _StartAnesthesiaDateTime);
                sbValue.Append(",@" + _StartAnesthesiaDateTime);

                sbInsert.Append("," + _Reamrk);
                sbValue.Append(",@" + _Reamrk);              

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.ID)));
                parameter.Add(new IParameter(_NURSE, IDbType.VarChar, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.NURSE)));
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime)));
                parameter.Add(new IParameter(_Reamrk, IDbType.VarChar, DBNullConvert.From(ORANESTHESIANURSESCHEDULEVO.Reamrk)));

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

        internal ReturnValue Delete(string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblORANESTHESIANURSESCHEDULE);
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
