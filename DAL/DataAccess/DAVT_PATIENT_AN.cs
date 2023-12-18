using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAVT_PATIENT_AN : DataAccess
    {
        private static string _VT_PATIENT_AN = "VT_PATIENT_AN";
        private static string _HN = "HN";
        private static string _AN = "AN";
        private static string _AdmitDateTime = "AdmitDateTime";
        private static string _DischargeDateTime = "DischargeDateTime";
        private static string _ORDateTime = "ORDateTime";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAVT_PATIENT_AN() { }
        public DAVT_PATIENT_AN(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_PATIENT_AN(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_PATIENT_AN(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_PATIENT_ANVO> SearchByKey(VT_PATIENT_ANVO _VT_PATIENT_ANVO)
        {
            List<VT_PATIENT_ANVO> retValue = new List<VT_PATIENT_ANVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_PATIENT_AN + " where 1=1 ");
                if (!string.IsNullOrEmpty(_VT_PATIENT_ANVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }
                if (!string.IsNullOrEmpty(_VT_PATIENT_ANVO.AN))
                {
                    strQuery.Append(" and " + _AN + " = @" + _AN);
                }
                strQuery.Append(" order by " + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_ANVO.HN)));
                parameter.Add(new IParameter(_AN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_ANVO.AN)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_PATIENT_ANVO VT_PATIENT_ANVO = new VT_PATIENT_ANVO();
                    VT_PATIENT_ANVO.HN  = query[_HN].ToString();
                    VT_PATIENT_ANVO.AN  = query[_AN].ToString();
                    VT_PATIENT_ANVO.AdmitDateTime  = ADOUtil.GetDateFromQuery(query[_AdmitDateTime].ToString());
                    VT_PATIENT_ANVO.DischargeDateTime  = ADOUtil.GetDateFromQuery(query[_DischargeDateTime].ToString());
                    retValue.Add(VT_PATIENT_ANVO);
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

        internal List<VT_PATIENT_ANVO> SearchAN(VT_PATIENT_ANVO _VT_PATIENT_ANVO)
        {
            List<VT_PATIENT_ANVO> retValue = new List<VT_PATIENT_ANVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from " + _VT_PATIENT_AN + " where 1=1 ");
                strQuery.Append(" and " + _HN + " = @" + _HN);
                strQuery.Append(" and " + _AdmitDateTime + " >= @" + _ORDateTime); //แบงค์ขอแก้ไข เนื่องจากเปิด AN ก่อน OR time           
                strQuery.Append(" order by " + _HN + " , " + _AdmitDateTime + " DESC");

                //strQuery.Append(" and " + _AdmitDateTime + " >= @" + _ORDateTime); //แบงค์ขอแก้ไข เนื่องจากเปิด AN ก่อน OR time
                //strQuery.Append(" and CONVERT(date," + _AdmitDateTime + ", 126) >= CONVERT(date, @" + _ORDateTime + ", 126) "); //แก้ไขเป็นตอนนี้
                //strQuery.Append(" and " + _AdmitDateTime + " <= @" + _ORDateTime);
                //strQuery.Append(" and " + _DischargeDateTime + " is null ");
                //strQuery.Append(" and " + _AdmitDateTime + " => @" + _ORDateTime); //แบงค์ขอแก้ไข เนื่องจากเปิด AN ก่อน OR time
                //strQuery.Append(" and " + _DischargeDateTime + " is null ");
                //strQuery.Append(" and (" + _DischargeDateTime + " >= @" + _ORDateTime + " OR " + _DischargeDateTime + " is null )");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_VT_PATIENT_ANVO.HN)));
                parameter.Add(new IParameter(_ORDateTime, IDbType.DateTime, DBNullConvert.From(_VT_PATIENT_ANVO.ORDateTime)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_PATIENT_ANVO VT_PATIENT_ANVO = new VT_PATIENT_ANVO();
                    VT_PATIENT_ANVO.HN = query[_HN].ToString();
                    VT_PATIENT_ANVO.AN = query[_AN].ToString();
                    VT_PATIENT_ANVO.AdmitDateTime = ADOUtil.GetDateFromQuery(query[_AdmitDateTime].ToString());
                    //VT_PATIENT_ANVO.DischargeDateTime = ADOUtil.GetDateFromQuery(query[_DischargeDateTime].ToString());
                    retValue.Add(VT_PATIENT_ANVO);
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
