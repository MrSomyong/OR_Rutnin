using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORLog : DataAccess
    {
        private static string _tblORLog = "ORLog";
        private static string _ID = "ID";
        private static string _ORID = "ORID";
        private static string _HN = "HN";
        private static string _PatientName = "PatientName";

        private static string _Detail = "Detail";
        private static string _UpdateDate = "UpdateDate";
        private static string _UpdateDateF = "UpdateDateF";
        private static string _UpdateDateT = "UpdateDateT";
        private static string _UpdateBy = "UpdateBy";

        private static string _UserID = "UserID";
        private static string _FirstName = "FirstName";
        private static string _LastName = "LastName";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAORLog() { }
        public DAORLog(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORLog(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORLog(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORLogVO> SearchByKey(ORLogVO _ORLogVO)
        {
            List<ORLogVO> retValue = new List<ORLogVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _FirstName + ", b." + _LastName + " from " + _tblORLog + " a ");
                strQuery.Append(" left join SETUPLOGON b on a." + _UpdateBy + " = b." + _UserID);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORLogVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_ORLogVO.ORID))
                {
                    strQuery.Append(" and a." + _ORID + " = @" + _ORID);
                }
                if (_ORLogVO.UpdateDateF != null)
                {
                    strQuery.Append(" and a." + _UpdateDate + " BETWEEN (@" + _UpdateDateF + " AND @" + _UpdateDateT + ")");
                }
                strQuery.Append(" order by a." + _UpdateDate +" DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORLogVO.ID)));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORLogVO.ORID)));
                if (_ORLogVO.UpdateDateF != null)
                {
                    parameter.Add(new IParameter(_UpdateDateF, IDbType.DateTime, DBNullConvert.From(_ORLogVO.UpdateDateF.Value.ToString("yyyy-MM-dd 00:00:00"))));
                    parameter.Add(new IParameter(_UpdateDateT, IDbType.DateTime, DBNullConvert.From(_ORLogVO.UpdateDateT.Value.ToString("yyyy-MM-dd 23:59:59"))));
                }
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORLogVO ORLogVO = new ORLogVO();
                    ORLogVO.ID = query[_ID].ToString();
                    ORLogVO.ORID = query[_ORID].ToString();
                    ORLogVO.HN = query[_HN].ToString();
                    ORLogVO.PatientName = query[_PatientName].ToString();
                    ORLogVO.Detail = query[_Detail].ToString();
                    ORLogVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    ORLogVO.UpdateBy = query[_UpdateBy].ToString();
                    ORLogVO.FirstName = query[_FirstName].ToString();
                    ORLogVO.LastName = query[_LastName].ToString();
                    ORLogVO.UpdateName = query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    retValue.Add(ORLogVO);
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

        internal ReturnValue Insert(ORLogVO _ORLogVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblORLog + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("NEWID()");

                sbInsert.Append("," + _ORID);
                sbValue.Append(",@" + _ORID);

                sbInsert.Append("," + _HN);
                sbValue.Append(",@" + _HN);

                sbInsert.Append("," + _PatientName);
                sbValue.Append(",@" + _PatientName);

                sbInsert.Append("," + _Detail);
                sbValue.Append(",@" + _Detail);

                sbInsert.Append("," + _UpdateBy);
                sbValue.Append(",@" + _UpdateBy);

                sbInsert.Append("," + _UpdateDate);
                sbValue.Append(", GETDATE()");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORLogVO.ORID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORLogVO.HN)));
                parameter.Add(new IParameter(_PatientName, IDbType.VarChar, DBNullConvert.From(_ORLogVO.PatientName)));
                parameter.Add(new IParameter(_Detail, IDbType.VarChar, DBNullConvert.From(_ORLogVO.Detail)));
                parameter.Add(new IParameter(_UpdateBy, IDbType.VarChar, DBNullConvert.From(_ORLogVO.UpdateBy)));

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
    }
}
