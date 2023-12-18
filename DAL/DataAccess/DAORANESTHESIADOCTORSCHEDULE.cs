using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORANESTHESIADOCTORSCHEDULE : DataAccess
    {
        private static string _tblORANESTHESIADOCTORSCHEDULE = "ORANESTHESIADOCTORSCHEDULE";
        private static string _VT_DOCTORMASTER = "VT_DOCTORMASTER";

        private static string _ID = "ID";
        private static string _Doctor = "Doctor";
        private static string _DoctorName = "DoctorName";
        private static string _Reamrk = "Reamrk";
        private static string _StartAnesthesiaDateTime = "StartAnesthesiaDateTime";
        private static string _strStartAnesthesiaDateTime = "strStartAnesthesiaDateTime";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAORANESTHESIADOCTORSCHEDULE() { }
        public DAORANESTHESIADOCTORSCHEDULE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORANESTHESIADOCTORSCHEDULE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORANESTHESIADOCTORSCHEDULE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORANESTHESIADOCTORSCHEDULEVO> SearchByKey(ORANESTHESIADOCTORSCHEDULEVO _ORANESTHESIADOCTORSCHEDULEVO)
        {
            List<ORANESTHESIADOCTORSCHEDULEVO> retValue = new List<ORANESTHESIADOCTORSCHEDULEVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _DoctorName + " from " + _tblORANESTHESIADOCTORSCHEDULE + " as a ");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as b on a." + _Doctor + " = b." + _Doctor);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORANESTHESIADOCTORSCHEDULEVO.ID))
                {
                    strQuery.Append(" and " + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime.ToString()))
                {
                    strQuery.Append(" and CONVERT(date, a. " + _StartAnesthesiaDateTime + ", 126) = CONVERT(date, '"+ _ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime.Value.ToString("yyyy-MM-dd") + "', 126) ");
                }
                
                strQuery.Append(" order by a." + _StartAnesthesiaDateTime + ", b." + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORANESTHESIADOCTORSCHEDULEVO.ID)));
                // parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(_ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime.Value.ToString("yyyy-MM-dd"))));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
                    ORANESTHESIADOCTORSCHEDULEVO.ID = query[_ID].ToString();
                    ORANESTHESIADOCTORSCHEDULEVO.Doctor = query[_Doctor].ToString();
                    ORANESTHESIADOCTORSCHEDULEVO.DoctorName = query[_DoctorName].ToString();
                    ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = ADOUtil.GetDateFromQuery(query[_StartAnesthesiaDateTime].ToString());
                    ORANESTHESIADOCTORSCHEDULEVO.strStartAnesthesiaDateTime = ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime.Value.ToString("dd-MM-yyyy HH:mm");
                    ORANESTHESIADOCTORSCHEDULEVO.Reamrk = query[_Reamrk].ToString();
                    retValue.Add(ORANESTHESIADOCTORSCHEDULEVO);
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
            table.Columns.Add(_Doctor, typeof(String));
            table.Columns.Add(_DoctorName, typeof(String));
            table.Columns.Add(_StartAnesthesiaDateTime, typeof(DateTime));
            table.Columns.Add(_strStartAnesthesiaDateTime, typeof(String));
            table.Columns.Add(_Reamrk, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _DoctorName + " from " + _tblORANESTHESIADOCTORSCHEDULE + " as a ");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as b on a." + _Doctor + " = b." + _Doctor);
                strQuery.Append(" where 1 = 1 ");
                strQuery.Append(" and CONVERT(date, a. " + _StartAnesthesiaDateTime + ", 126) = CONVERT(date, @" + _StartAnesthesiaDateTime + ", 126) ");

                strQuery.Append(" order by a." + _StartAnesthesiaDateTime + ", b." + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(StartAnesthesiaDateTime)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DataRow row = table.NewRow();
                    row[_ID] = query[_ID].ToString();
                    row[_Doctor] = query[_Doctor].ToString();
                    row[_DoctorName] = query[_DoctorName].ToString();
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

        internal ReturnValue Insert(ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblORANESTHESIADOCTORSCHEDULE + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _Doctor);
                sbValue.Append(",@" + _Doctor);

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
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ORANESTHESIADOCTORSCHEDULEVO.ID)));
                parameter.Add(new IParameter(_Doctor, IDbType.VarChar, DBNullConvert.From(ORANESTHESIADOCTORSCHEDULEVO.Doctor)));
                parameter.Add(new IParameter(_StartAnesthesiaDateTime, IDbType.DateTime, DBNullConvert.From(ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime)));
                parameter.Add(new IParameter(_Reamrk, IDbType.VarChar, DBNullConvert.From(ORANESTHESIADOCTORSCHEDULEVO.Reamrk)));

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
                sbQuery.Append("DELETE FROM " + _tblORANESTHESIADOCTORSCHEDULE);
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
