using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPATIENTALLEGIC : DataAccess
    {
        private static string _tblPATIENTALLEGIC = "VT_PATIENTALLEGIC";
        private static string _tblPATIENTALLEGICOTHER = "VT_PATIENTALLEGICOTHER";
        private static string _HN = "HN";
        private static string _allegicname = "allegicname";
        private static string _Reaction = "Reaction";
        private static string _Remark = "Remark";
        private static string _FromType = "FromType";
        private static string _LastUpdateDatetime = "LASTUPDATEDATETIME";
        private static string _MEMO = "MEMO";
        private static Boolean _blCheckAllergyType = false;

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAPATIENTALLEGIC() { }
        public DAPATIENTALLEGIC(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPATIENTALLEGIC(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPATIENTALLEGIC(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<PATIENTALLEGICVO> SearchByKey(PATIENTALLEGICVO _PATIENTALLEGICVO)
        {
            List<PATIENTALLEGICVO> retValue = new List<PATIENTALLEGICVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPATIENTALLEGIC);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_PATIENTALLEGICVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }                
                strQuery.Append(" order by " + _FromType + "," + _allegicname);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_PATIENTALLEGICVO.HN)));
                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);

                while (query.Read())
                {
                    
                    PATIENTALLEGICVO PATIENTALLEGICVO = new PATIENTALLEGICVO();
                    PATIENTALLEGICVO.HN = query[_HN].ToString();
                    PATIENTALLEGICVO.allegicname = query[_allegicname].ToString();
                    PATIENTALLEGICVO.Reaction = query[_Reaction].ToString();
                    PATIENTALLEGICVO.Remark = query[_Remark].ToString();
                    PATIENTALLEGICVO.FromType = query[_FromType].ToString();
                    retValue.Add(PATIENTALLEGICVO);
                    if (query[_FromType].ToString() == "1")
                    {
                        goto EndWhile;
                    }
                }

            EndWhile:
                _blCheckAllergyType = false;
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

        internal List<PATIENTALLEGICVO> SearchOtherByKey(PATIENTALLEGICVO _PATIENTALLEGICVO)
        {
            List<PATIENTALLEGICVO> retValue = new List<PATIENTALLEGICVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblPATIENTALLEGICOTHER);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_PATIENTALLEGICVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }
                strQuery.Append(" order by " + _LastUpdateDatetime);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_PATIENTALLEGICVO.HN)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    PATIENTALLEGICVO PATIENTALLEGICVO = new PATIENTALLEGICVO();
                    PATIENTALLEGICVO.HN = query[_HN].ToString();
                    PATIENTALLEGICVO.Memo = query[_MEMO].ToString();
                    retValue.Add(PATIENTALLEGICVO);
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
