using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAVT_PATIENT_VN : DataAccess
    {
        private static string _VT_PATIENT_AN = "VT_PATIENT_VN";
        private static string _HN = "HN";
        private static string _VN = "VN";
        private static string _VisitDate = "VisitDate";
        private static string _ORDateTime = "ORDateTime";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAVT_PATIENT_VN() { }
        public DAVT_PATIENT_VN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_PATIENT_VN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_PATIENT_VN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_PATIENT_VNVO> SearchByKey(VT_PATIENT_VNVO _VT_PATIENT_VNVO)
        {
            List<VT_PATIENT_VNVO> retValue = new List<VT_PATIENT_VNVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_PATIENT_AN + " where 1=1 ");
                if (!string.IsNullOrEmpty(_VT_PATIENT_VNVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }
                if (!string.IsNullOrEmpty(_VT_PATIENT_VNVO.VN))
                {
                    strQuery.Append(" and " + _VN + " = @" + _VN);
                }
                strQuery.Append(" order by " + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_VNVO.HN)));
                parameter.Add(new IParameter(_VN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_VNVO.VN)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_PATIENT_VNVO VT_PATIENT_VNVO = new VT_PATIENT_VNVO();
                    VT_PATIENT_VNVO.HN  = query[_HN].ToString();
                    VT_PATIENT_VNVO.VN  = query[_VN].ToString();
                    VT_PATIENT_VNVO.VisitDate  = ADOUtil.GetDateFromQuery(query[_VisitDate].ToString());
                    retValue.Add(VT_PATIENT_VNVO);
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

        internal List<VT_PATIENT_VNVO> SearchVN(VT_PATIENT_VNVO _VT_PATIENT_VNVO)
        {
            List<VT_PATIENT_VNVO> retValue = new List<VT_PATIENT_VNVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_PATIENT_AN + " where 1=1 ");
                strQuery.Append(" and " + _HN + " = @" + _HN);
                strQuery.Append(" and CONVERT(date," + _VisitDate + ", 126) = CONVERT(date, @" + _ORDateTime + ", 126) ");
                strQuery.Append(" order by " + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_VNVO.HN)));
                parameter.Add(new IParameter(_ORDateTime, IDbType.DateTime, DBNullConvert.From(_VT_PATIENT_VNVO.ORDateTime)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_PATIENT_VNVO VT_PATIENT_VNVO = new VT_PATIENT_VNVO();
                    VT_PATIENT_VNVO.HN = query[_HN].ToString();
                    VT_PATIENT_VNVO.VN = query[_VN].ToString();
                    VT_PATIENT_VNVO.VisitDate = ADOUtil.GetDateFromQuery(query[_VisitDate].ToString());
                    retValue.Add(VT_PATIENT_VNVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                DisconnectDB();
            }
            return retValue;
        }
    }
}
