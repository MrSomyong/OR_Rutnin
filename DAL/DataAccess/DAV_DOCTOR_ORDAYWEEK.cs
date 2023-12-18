using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAV_DOCTOR_ORDAYWEEK : DataAccess
    {
        private static string _V_DOCTOR_ORDAYWEEK = "V_DOCTOR_ORDAYWEEK";
        private static string _VT_DOCTORMASTER = "VT_DOCTORMASTER";
        
        private string _ORDAYWEEK = "ORDAYWEEK";
        private string _DOCTORCODE = "DOCTORCODE";
        private string _DOCTOR = "DOCTOR";
        //private string _DoctorName = "DoctorName";
        private string _DoctorName = "CHEQUECLIENTNAME";
        private string _EDUCATIONSTANDARD = "EDUCATIONSTANDARD";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAV_DOCTOR_ORDAYWEEK() { }
        public DAV_DOCTOR_ORDAYWEEK(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAV_DOCTOR_ORDAYWEEK(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAV_DOCTOR_ORDAYWEEK(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<V_DOCTOR_ORDAYWEEKVO> SearchAll()
        {
            List<V_DOCTOR_ORDAYWEEKVO> retValue = new List<V_DOCTOR_ORDAYWEEKVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.* from " + _V_DOCTOR_ORDAYWEEK + " a");
                strQuery.Append(" LEFT JOIN " + _VT_DOCTORMASTER + " b");
                strQuery.Append(" ON( a." + _DOCTORCODE + " = b." + _DOCTOR + ")");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    V_DOCTOR_ORDAYWEEKVO V_DOCTOR_ORDAYWEEKVO = new V_DOCTOR_ORDAYWEEKVO();
                    V_DOCTOR_ORDAYWEEKVO.ORDAYWEEK = query[_ORDAYWEEK].ToString();
                    V_DOCTOR_ORDAYWEEKVO.DOCTORCODE = query[_DOCTORCODE].ToString();
                    V_DOCTOR_ORDAYWEEKVO.DOCTOR = query[_DOCTOR].ToString();
                    V_DOCTOR_ORDAYWEEKVO.DoctorName = query[_DoctorName].ToString();
                    V_DOCTOR_ORDAYWEEKVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();

                    retValue.Add(V_DOCTOR_ORDAYWEEKVO);
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
    }
}
