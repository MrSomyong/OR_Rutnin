using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPATIENTDIAG : DataAccess
    {
        private static string _tblPATIENTDIAG = "VT_PATIENTDIAG";
        private static string _HN = "HN";
        private static string _icdname = "icdname";
        private static string _diagname = "diagname";
        private static string _Remark = "Remark";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAPATIENTDIAG() { }
        public DAPATIENTDIAG(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPATIENTDIAG(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPATIENTDIAG(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<PATIENTDIAGVO> SearchByKey(PATIENTDIAGVO _PATIENTDIAGVO)
        {
            List<PATIENTDIAGVO> retValue = new List<PATIENTDIAGVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPATIENTDIAG);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_PATIENTDIAGVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }                
                strQuery.Append(" order by " + _diagname);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_PATIENTDIAGVO.HN)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    PATIENTDIAGVO PATIENTDIAGVO = new PATIENTDIAGVO();
                    PATIENTDIAGVO.HN = query[_HN].ToString();
                    PATIENTDIAGVO.icdname = query[_icdname].ToString();
                    PATIENTDIAGVO.diagname = query[_diagname].ToString();
                    PATIENTDIAGVO.Remark = query[_Remark].ToString();
                    retValue.Add(PATIENTDIAGVO);
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
