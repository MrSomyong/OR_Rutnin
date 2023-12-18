//using System;
//using System.Collections.Generic;
//using System.Text;
//using ADOUtils;
//using System.Data;

//namespace DAL
//{
//    class DAUnderPatient : DataAccess
//    {
//        private static string _tblORHEADER = "ORHEADER";
//        private static string _tblOROPERATION = "OROPERATION";

//        private static string _ORID = "ORID";
//        private static string _HN = "HN";
//        private static string _PatientName = "PatientName";
//        private static string _ORDate = "ORDate";
//        private static string _ORTime = "ORTime";
//        private static string _Operation = "Operation";

//        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("th-TH");

//        public DAUnderPatient() { }
//        public DAUnderPatient(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
//        public DAUnderPatient(System.Data.IDbTransaction useTransaction)
//        {
//            this.useTransaction = true; this.transaction = useTransaction;
//            this.connection = useTransaction.Connection;
//        }
//        public DAUnderPatient(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
//        {
//            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
//            this.connection = useTransaction.Connection;
//        }

//        internal List<UnderPatientVO> SearchByKey(UnderPatientVO _UnderPatientVO)
//        {
//            List<UnderPatientVO> retValue = new List<UnderPatientVO>();
//            try
//            {
//                StringBuilder strQuery = new StringBuilder();
//                strQuery.Append(" select a.*, b." + _NAME + " from " + _tblPOSTORNURSE + " as a ");
//                strQuery.Append(" left join " + _VT_NURSEMASTER + " as b on a." + _NurseCode + " = b." + _CODE);
//                strQuery.Append(" where 1=1 ");
//                if (!string.IsNullOrEmpty(_POSTORNURSEVO.ORID))
//                {
//                    strQuery.Append(" and " + _ORID + " = @" + _ORID);
//                }
//                if (_POSTORNURSEVO.Suffix > 0)
//                {
//                    strQuery.Append(" and " + _Suffix + " = @" + _Suffix);
//                }
//                strQuery.Append(" order by " + _ORID);
//                ConnectDB();
//                List<IParameter> parameter = new List<IParameter>();
//                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORNURSEVO.ORID)));
//                parameter.Add(new IParameter(_Suffix, IDbType.Int, DBNullConvert.From(_POSTORNURSEVO.Suffix, false)));

//                command = GetCommand(strQuery.ToString(), parameter);
//                query = GetExecuteReader(command);
//                while (query.Read())
//                {
//                    POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
//                    POSTORNURSEVO.ORID = query[_ORID].ToString();
//                    POSTORNURSEVO.Suffix = ADOUtil.GetIntFromQuery(query[_Suffix].ToString());
//                    POSTORNURSEVO.NurseType = ADOUtil.GetIntFromQuery(query[_NurseType].ToString());
//                    POSTORNURSEVO.NurseTypeDesc = ((EnumOR.NurseType)POSTORNURSEVO.NurseType).ToString();
//                    POSTORNURSEVO.NurseCode = query[_NurseCode].ToString();
//                    POSTORNURSEVO.Nurse = query[_NAME].ToString();
//                    POSTORNURSEVO.Remark = query[_Remark].ToString();
//                    retValue.Add(POSTORNURSEVO);
//                }
//                query.Close();
//                command.Dispose();
//                DisconnectDB();
//            }
//            catch (Exception exc)
//            {
//                throw exc;
//            }
//            return retValue;
//        }
//    }
//}
