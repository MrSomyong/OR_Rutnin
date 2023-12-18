using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORHEADER : DataAccess
    {
        private static string _tblORHEADER = "ORHEADER";
        private static string _tblORPATIENT = "ORPATIENT";
        private static string _tblOROPERATION = "OROPERATION";
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";
        private static string _tblPOSTORICD = "POSTORICD";

        private static string _VT_PATIENTMASTER = "VT_PATIENTMASTER";
        private static string _VT_DOCTORMASTER = "VT_DOCTORMASTER";
        private static string _VT_ROOMTYPE = "VT_ROOMTYPE";
        private static string _VT_APPOINTMENTMASTER = "VT_APPOINTMENTMASTER";
        private static string _VT_ANESTHESIA = "VT_ANESTHESIA";

        private static string _ORID = "ORID";
        private static string _HN = "HN";
        private static string _PatientName = "PatientName";
        private static string _PatientInfection = "PatientInfection";
        private static string _PatientType1 = "PatientType1";
        private static string _PatientType2 = "PatientType2";
        private static string _PatientUP = "PatientUP";
        private static string _ORDate = "ORDate";
        private static string _ORDateFrom = "ORDateFrom";
        private static string _ORDateTo = "ORDateTo";
        private static string _ORTime = "ORTime";
        private static string _ArrivalTime = "ArrivalTime";
        private static string _ORTimeFollow = "ORTimeFollow";
        private static string _ORStatCase = "ORStatCase";
        private static string _ORCase = "ORCase";
        private static string _ORSpecificType = "ORSpecificType";
        private static string _ORStatus = "ORStatus";
        private static string _AdmitTimeType = "AdmitTimeType";
        private static string _RoomType = "RoomType";
        private static string _ORRoom = "ORRoom";
        private static string _AnesthesiaType1 = "AnesthesiaType1";
        private static string _AnesthesiaType2 = "AnesthesiaType2";
        private static string _AnesthesiaSign = "AnesthesiaSign";
        private static string _Surgeon1 = "Surgeon1";
        private static string _Surgeon2 = "Surgeon2";
        private static string _Surgeon3 = "Surgeon3";
        private static string _SurgeonMaster = "SurgeonMaster";
        private static string _AnesthesiaDoctor1 = "AnesthesiaDoctor1";
        private static string _AnesthesiaDoctor2 = "AnesthesiaDoctor2";
        private static string _AnesthesiaDoctor3 = "AnesthesiaDoctor3";
        private static string _AnesthesiaNurse1 = "AnesthesiaNurse1";
        private static string _AnesthesiaNurse2 = "AnesthesiaNurse2";
        private static string _AnesthesiaNurse3 = "AnesthesiaNurse3";
        private static string _Remark = "Remark";
        private static string _AppointmentNo = "AppointmentNo";

        private static string _Initial = "Initial";
        private static string _FirstName = "FirstName";
        private static string _LastName = "LastName";
        private static string _BirthDateTime = "BirthDateTime";
        //private static string _ageyear = "ageyear";
        //private static string _agemonth = "agemonth";
        private static string _Age = "Age";
        private static string _Sex = "Sex";
        private static string _Ref = "Ref";
        private static string _Nationality = "Nationality";

        private static string _ConfirmStatusType = "ConfirmStatusType";

        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _SubCode = "SubCode";
        private static string _SubName = "SubName";
        private static string _Side = "Side";
        private static string _Reamrk = "Reamrk";

        private static string _DOCTOR = "DOCTOR";
        private static string _DoctorName = "DoctorName";
        private static string _ChequeClientName = "CHEQUECLIENTNAME";
        private static string _SurgeonMasterName = "SurgeonMasterName";
        private static string _CreateDate = "CreateDate";
        private static string _CreateBy = "CreateBy";
        private static string _UpdateDate = "UpdateDate";
        private static string _UpdateBy = "UpdateBy";

        private static string _PatientName_IPPU = "PatientName_IPPU";
        private static string _strORDate = "strORDate";
        private static string _NSR = "NSR";
        private static string _strCreateDate = "strCreateDate";

        private static string _NAME = "NAME";
        private static string _RoomTypeName = "RoomTypeName";
        private static string _morning = "morning";
        private static string _evening = "evening";
        private static string _CODE = "CODE";
        private static string _Onmed = "Onmed";
        private static string _OnmedNote = "OnmedNote";
        //Cancel
        private static string _CxlDateTime = "CxlDateTime";
        private static string _CxlByUser = "CxlByUser";
        private static string _CxlReason = "CxlReason";

        private static string _CxlPostOR = "CxlPostOR";
        private static string _CxlPostORReason = "CxlPostORReason";
        private static string _CxlPostORReasonOther = "CxlPostORReasonOther";

        private static string _ElectiveCase = "ElectiveCase";
        private static string _UrgencyCase = "UrgencyCase";
        private static string _OperationTime = "OperationTime";
        private static string _StartORDateTime = "StartORDateTime";
        private static string _FinishORDateTime = "FinishORDateTime";
        private static string _AnesTime = "AnesTime";
        private static string _StartAnesDateTime = "StartAnesDateTime";
        private static string _FinishAnesDateTime = "FinishAnesDateTime";
        private static string _BlockTime = "BlockTime";
        private static string _StartBlockDateTime = "StartBlockDateTime";
        private static string _FinishBlockDateTime = "FinishBlockDateTime";
        private static string _RecoveryTime = "RecoveryTime";
        private static string _StartRecoveryDateTime = "StartRecoveryDateTime";
        private static string _FinishRecoveryDateTime = "FinishRecoveryDateTime";
        private static string _Prediag = "Prediag";
        private static string _RequestByUser = "RequestByUser";

        private static string _tblPOSTORDETAIL = "POSTORDETAIL";

        public DAORHEADER() {}
        public DAORHEADER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo;
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        }
        public DAORHEADER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORHEADER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORHEADERVO> SearchByKey(ORHEADERVO _ORHEADERVO)
        {
            
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*");
                //strQuery.Append(", c." + _BirthDateTime + ",c." + _Sex + ",c." + _Ref + ",c." + _Nationality);
                //strQuery.Append(", c." + _Initial + ", c." + _FirstName + ", c." + _LastName);
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblORHEADER + " as b ");
                //strQuery.Append(" left join " + _VT_PATIENTMASTER + " as c on b." + _HN + " = c." + _HN);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORHEADERVO.ArORRoom))
                {
                    strQuery.Append(" and b. " + _ORRoom + " in (" + _ORHEADERVO.ArORRoom + ")");
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.ORRoom))
                {
                    strQuery.Append(" and b. " + _ORRoom + " = @" + _ORRoom);
                }
                if (_ORHEADERVO.ORDate != null)
                {
                    strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.HN))
                {
                    strQuery.Append(" and b. " + _HN + " like @" + _HN);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.ORID))
                {
                    strQuery.Append(" and b. " + _ORID + " = @" + _ORID);
                }
                //strQuery.Append(" and b. "+ _ORStatus + " in (1,2) ");

                strQuery.Append(" order by b." + _ORTime + ",b." + _ORTimeFollow + ", " + _SurgeonMasterName + ",b." + _ORCase);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORRoom)));
                if (_ORHEADERVO.ORDate != null)
                {
                    parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                }
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From("%" + _ORHEADERVO.HN + "%")));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(DbInfo).SearchByHN(ORHEADERVO.HN);
                    ORHEADERVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORHEADERVO.ORPATIENTVO = ORPATIENTVO;

                    //ORHEADERVO.PatientName = query[_Initial].ToString() + " " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    ORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    ORHEADERVO.Onmed = query[_Onmed].ToString() == "True" ? true : false;
                    ORHEADERVO.OnmedNote = query[_OnmedNote].ToString();
                    string IPPU = string.Empty;
                    if (ORHEADERVO.PatientInfection)
                        IPPU += " (Infection)";
                    if (ORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (ORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (ORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    ORHEADERVO.PatientName_IPPU = ORHEADERVO.PatientName + IPPU;
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (ORHEADERVO.ORTimeFollow)
                    {
                        ORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        ORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    ORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    ORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    ORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORSpecificType))
                        ORHEADERVO.ORSpecificType = "0";
                    ORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORStatus))
                    {
                        ORHEADERVO.ORStatus = "0";
                        ORHEADERVO.strORStatus = "0";
                    }
                    else
                    {
                        ORHEADERVO.strORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    }
                    ORHEADERVO.AdmitTimeType = query[_AdmitTimeType].ToString();
                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    ORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    ORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    ORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    ORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    ORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    ORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    ORHEADERVO.Remark = query[_Remark].ToString();
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    //ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
                    //ORPATIENTVO.PatientName = ORHEADERVO.PatientName;
                    //ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    //ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    //ORPATIENTVO.Sex = query[_Sex].ToString();
                    //ORPATIENTVO.Ref = query[_Ref].ToString();
                    //ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    //ORPATIENTVO.Initial = query[_Initial].ToString();
                    //ORHEADERVO.ORPATIENTVO = ORPATIENTVO;
                    ORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    ORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    ORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    ORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    ORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    ORHEADERVO.CxlReason = query[_CxlReason].ToString();

                    ORHEADERVO.CxlPostOR = ADOUtil.GetBoolFromQuery(query[_CxlPostOR].ToString());
                    ORHEADERVO.CxlPostORReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostORReasonOther = query[_CxlPostORReasonOther].ToString();
                    ORHEADERVO.Prediag = query[_Prediag].ToString();
                    ORHEADERVO.SuggestByUser = query["SuggestByUser"].ToString();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    
                        
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchPostORByKey(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*");
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblORHEADER + " as b ");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(_ORHEADERVO.ArORRoom))
                {
                    strQuery.Append(" and b. " + _ORRoom + " in (" + _ORHEADERVO.ArORRoom + ")");
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.ORRoom))
                {
                    strQuery.Append(" and b. " + _ORRoom + " = @" + _ORRoom);
                }
                if (_ORHEADERVO.ORDate != null)
                {
                    strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                }
                if (_ORHEADERVO.ORDateFrom != null)
                {
                    strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) between CONVERT(date, @" + _ORDateFrom + ", 126)  and CONVERT(date, @" + _ORDateTo + ", 126) ");
                } else
                {
                    strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDateFrom + ", 126) ");
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.HN))
                {
                    strQuery.Append(" and b. " + _HN + " like @" + _HN);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.ORID))
                {
                    strQuery.Append(" and b. " + _ORID + " = @" + _ORID);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon))
                {
                    strQuery.Append(" and (b. " + _Surgeon1 + " = @" + _Surgeon1 + " or b." + _Surgeon2 + " = @" + _Surgeon2 + " or b." + _Surgeon3 + " = @" + _Surgeon3 + ")");
                }
                strQuery.Append(" and b. " + _CxlByUser + " IS NULL ");

                strQuery.Append(" order by b." + _ORTime + ",b." + _ORTimeFollow + ", " + _SurgeonMasterName + ",b." + _ORCase);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORRoom)));
                if (_ORHEADERVO.ORDate != null)
                {
                    parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                }
                if (_ORHEADERVO.ORDateFrom != null)
                {
                    parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                }
                if (_ORHEADERVO.ORDateTo != null)
                {
                    parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                }
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From("%" + _ORHEADERVO.HN + "%")));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(DbInfo).SearchByHN(ORHEADERVO.HN);
                    ORHEADERVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORHEADERVO.ORPATIENTVO = ORPATIENTVO;

                    //ORHEADERVO.PatientName = query[_Initial].ToString() + " " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    ORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    ORHEADERVO.Onmed = query[_Onmed].ToString() == "True" ? true : false;
                    ORHEADERVO.OnmedNote = query[_OnmedNote].ToString();
                    string IPPU = string.Empty;
                    if (ORHEADERVO.PatientInfection)
                        IPPU += " (Infection)";
                    if (ORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (ORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (ORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    ORHEADERVO.PatientName_IPPU = ORHEADERVO.PatientName + IPPU;
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (ORHEADERVO.ORTimeFollow)
                    {
                        ORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        ORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    ORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    ORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    ORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORSpecificType))
                        ORHEADERVO.ORSpecificType = "0";
                    ORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    //ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    //ORHEADERVO.strORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORStatus))
                    {
                        ORHEADERVO.ORStatus = "0";
                        ORHEADERVO.strORStatus = "0";
                    }
                    else
                    {
                        ORHEADERVO.strORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    }
                    ORHEADERVO.AdmitTimeType = query[_AdmitTimeType].ToString();
                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    ORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    ORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    ORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    ORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    ORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    ORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    ORHEADERVO.Remark = query[_Remark].ToString();
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    //ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
                    //ORPATIENTVO.PatientName = ORHEADERVO.PatientName;
                    //ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    //ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    //ORPATIENTVO.Sex = query[_Sex].ToString();
                    //ORPATIENTVO.Ref = query[_Ref].ToString();
                    //ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    //ORPATIENTVO.Initial = query[_Initial].ToString();
                    //ORHEADERVO.ORPATIENTVO = ORPATIENTVO;
                    ORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    ORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    ORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    ORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    ORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    ORHEADERVO.CxlReason = query[_CxlReason].ToString();

                    ORHEADERVO.CxlPostOR = ADOUtil.GetBoolFromQuery(query[_CxlPostOR].ToString());
                    ORHEADERVO.CxlPostORReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostORReasonOther = query[_CxlPostORReasonOther].ToString();
                    ORHEADERVO.Prediag = query[_Prediag].ToString();
                    ORHEADERVO.SuggestByUser = query["SuggestByUser"].ToString();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchByORID(string orid)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*");
                //strQuery.Append(" c." + _BirthDateTime + ",c." + _Sex + ",c." + _Ref + ",c." + _Nationality);
                //strQuery.Append(", c." + _Initial + ", c." + _FirstName + ", c." + _LastName);
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblORHEADER + " as b ");
                //strQuery.Append(" left join " + _VT_PATIENTMASTER + " as c on b." + _HN + " = c." + _HN);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where ");
                strQuery.Append(" b. " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(orid)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();

                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(DbInfo).SearchByHN(ORHEADERVO.HN);
                    ORHEADERVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORHEADERVO.ORPATIENTVO = ORPATIENTVO;

                    ORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    string IPPU = string.Empty;
                    if (ORHEADERVO.PatientInfection)
                        IPPU += " (Inf)";
                    if (ORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (ORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (ORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    ORHEADERVO.PatientName_IPPU = ORHEADERVO.PatientName + IPPU;
                    ORHEADERVO.Onmed = query[_Onmed].ToString() == "True" ? true : false;
                    ORHEADERVO.OnmedNote = query[_OnmedNote].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (ORHEADERVO.ORTimeFollow)
                    {
                        ORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        ORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    ORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    ORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    ORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORSpecificType))
                        ORHEADERVO.ORSpecificType = "0";
                    ORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    ORHEADERVO.AdmitTimeType = query[_AdmitTimeType].ToString();

                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    ORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    ORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    ORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    ORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    ORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    ORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    ORHEADERVO.Remark = query[_Remark].ToString();
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    //ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
                    //ORPATIENTVO.PatientName = ORHEADERVO.PatientName;
                    //ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    //ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    //ORPATIENTVO.Sex = query[_Sex].ToString();
                    //ORPATIENTVO.Ref = query[_Ref].ToString();
                    //ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    //ORPATIENTVO.Initial = query[_Initial].ToString();

                    //ORHEADERVO.ORPATIENTVO = ORPATIENTVO;

                    ORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    ORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    ORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    //OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    //OROPERATIONVO.MainCode = query[_MainCode].ToString();
                    //OROPERATIONVO.Reamrk = query[_Reamrk].ToString();
                    //OROPERATIONVO.Side = query[_Side].ToString();
                    //OROPERATIONVO.SubCode = query[_SubCode].ToString();

                    //ORHEADERVO.OROPERATIONVO = OROPERATIONVO;
                    ORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    ORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    ORHEADERVO.CxlReason = query[_CxlReason].ToString();

                    ORHEADERVO.CxlPostOR = ADOUtil.GetBoolFromQuery(query[_CxlPostOR].ToString());
                    ORHEADERVO.CxlPostORReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostORReasonOther = query[_CxlPostORReasonOther].ToString();
                    ORHEADERVO.Prediag = query[_Prediag].ToString();
                    ORHEADERVO.SuggestByUser = query["SuggestByUser"].ToString();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchByRoom(string orroom, DateTime ORDate)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*");
                //strQuery.Append(" c." + _BirthDateTime + ",c." + _Sex + ",c." + _Ref + ",c." + _Nationality);
                //strQuery.Append(", c." + _Initial + ", c." + _FirstName + ", c." + _LastName);
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblORHEADER + " as b ");
                //strQuery.Append(" left join " + _VT_PATIENTMASTER + " as c on b." + _HN + " = c." + _HN);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where ");
                strQuery.Append(" b. " + _ORRoom + " = @" + _ORRoom + " and ");
                strQuery.Append(" CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                //strQuery.Append(" and b. "+ _ORStatus + " in (1,2) ");

                strQuery.Append(" order by b." + _ORTime + ",b." + _ORTimeFollow + ", " + _SurgeonMasterName + ",b." + _ORCase);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(orroom)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));

                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(DbInfo).SearchByHN(ORHEADERVO.HN);
                    //ORHEADERVO.PatientName = query[_Initial].ToString() + " " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    ORHEADERVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORHEADERVO.ORPATIENTVO = ORPATIENTVO;

                    ORHEADERVO.Onmed = query[_Onmed].ToString() == "True" ? true : false;
                    ORHEADERVO.OnmedNote = query[_OnmedNote].ToString();

                    ORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    string IPPU = string.Empty;
                    if (ORHEADERVO.PatientInfection)
                        IPPU += " (Infection)";
                    if (ORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (ORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (ORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    ORHEADERVO.PatientName_IPPU = ORHEADERVO.PatientName + IPPU;
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");


                    ORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (ORHEADERVO.ORTimeFollow)
                    {
                        ORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        ORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    ORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    ORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    ORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORSpecificType))
                        ORHEADERVO.ORSpecificType = "0";
                    ORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORStatus))
                    { 
                        ORHEADERVO.ORStatus = "0";
                        ORHEADERVO.strORStatus = "0";
                    }
                    else
                    { 
                    ORHEADERVO.strORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    }
                    ORHEADERVO.AdmitTimeType = query[_AdmitTimeType].ToString();
                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    ORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    ORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    ORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    ORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    ORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    ORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    ORHEADERVO.Remark = query[_Remark].ToString();
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();



                    ORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    ORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    ORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    ORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    ORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    ORHEADERVO.CxlReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostOR = ADOUtil.GetBoolFromQuery(query[_CxlPostOR].ToString());
                    ORHEADERVO.CxlPostORReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostORReasonOther = query[_CxlPostORReasonOther].ToString();
                    ORHEADERVO.Prediag = query[_Prediag].ToString();
                    ORHEADERVO.SuggestByUser = query["SuggestByUser"].ToString();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchByIPD(DateTime ORDate)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*");
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblORHEADER + " as b ");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where ");
                strQuery.Append(" CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                strQuery.Append(" and b." + _ORStatus + " in (2,3,4)");

                strQuery.Append(" order by b." + _ORTime + ",b." + _ORTimeFollow + ", " + _SurgeonMasterName + ",b." + _ORCase);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();

                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(DbInfo).SearchByHN(ORHEADERVO.HN);
                    ORHEADERVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORHEADERVO.ORPATIENTVO = ORPATIENTVO;
                    ORHEADERVO.Onmed = query[_Onmed].ToString() == "True" ? true : false;
                    ORHEADERVO.OnmedNote = query[_OnmedNote].ToString();
                    ORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    ORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    string IPPU = string.Empty;
                    if (ORHEADERVO.PatientInfection)
                        IPPU += " (Infection)";
                    if (ORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (ORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (ORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    ORHEADERVO.PatientName_IPPU = ORHEADERVO.PatientName + IPPU;
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (ORHEADERVO.ORTimeFollow)
                    {
                        ORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        ORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    ORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    ORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    ORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORSpecificType))
                        ORHEADERVO.ORSpecificType = "0";
                    ORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    if (string.IsNullOrEmpty(ORHEADERVO.ORStatus))
                    {
                        ORHEADERVO.ORStatus = "0";
                        ORHEADERVO.strORStatus = "0";
                    }
                    else
                    {
                        ORHEADERVO.strORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    }
                    if (string.IsNullOrEmpty(query[_AdmitTimeType].ToString()))
                    {
                        ORHEADERVO.AdmitTimeType = "";
                    }
                    else
                    {
                        ORHEADERVO.AdmitTimeType = ((EnumOR.AdmitTimeType)int.Parse(query[_AdmitTimeType].ToString())).ToString();
                    }

                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    ORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    ORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    ORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    ORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    ORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    ORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    ORHEADERVO.Remark = query[_Remark].ToString();
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    ORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    ORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    ORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    ORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    ORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    ORHEADERVO.CxlReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostOR = ADOUtil.GetBoolFromQuery(query[_CxlPostOR].ToString());
                    ORHEADERVO.CxlPostORReason = query[_CxlReason].ToString();
                    ORHEADERVO.CxlPostORReasonOther = query[_CxlPostORReasonOther].ToString();
                    ORHEADERVO.Prediag = query[_Prediag].ToString();
                    ORHEADERVO.SuggestByUser = query["SuggestByUser"].ToString();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchBySurgeon(string Surgeonid, DateTime ORDate, string roomid)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select top 1 " + _ORTime + " from " + _tblORHEADER);
                strQuery.Append(" where " + _SurgeonMaster + " = @" + _SurgeonMaster);
                strQuery.Append(" and CONVERT(date, " + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                strQuery.Append(" and " + _ORRoom + " = @" + _ORRoom);
                strQuery.Append(" and " + _ORTimeFollow + " = 0");
                strQuery.Append(" and " + _ORCase + " <> 0");
                strQuery.Append(" order by " + _ORTime + " asc");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(Surgeonid)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(roomid)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchBySurgeonTF(string Surgeonid, DateTime ORDate, string roomid)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select " + _ORTime + ", " + _ORID + ", " + _AppointmentNo + " from " + _tblORHEADER);
                strQuery.Append(" where " + _SurgeonMaster + " = @" + _SurgeonMaster);
                strQuery.Append(" and CONVERT(date, " + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                strQuery.Append(" and " + _ORRoom + " = @" + _ORRoom);
                strQuery.Append(" and " + _ORTimeFollow + " = 1");
                strQuery.Append(" and " + _ORCase + " <> 0");
                strQuery.Append(" order by " + _ORTime + " asc");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(Surgeonid)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(roomid)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    ORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchByRequestbyuser(string orroom, DateTime ORDate)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" a.RequestByUser ");
                strQuery.Append(" from " + _tblORHEADER + " as a ");
                strQuery.Append(" where ");
                strQuery.Append(" a. " + _ORRoom + " = @" + _ORRoom + " and ");
                strQuery.Append(" CONVERT(date, a." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) and ");
                strQuery.Append(" a." + _RequestByUser + " is not null ");
                strQuery.Append(" Group by a." + _RequestByUser );
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(orroom)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));

                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.RequestByUser = query["RequestByUser"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchOperation(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _MainCode + ",c." + _Name + ", count(a." + _MainCode + ") as QTY");
                strQuery.Append(" FROM " + _tblOROPERATION + " a");
                strQuery.Append(" left join  " + _tblORHEADER + " b on a." + _ORID + " = b." + _ORID);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, b." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and b." + _Surgeon1 + " = @" + _Surgeon1);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.AnesthesiaType1))
                {
                    strQuery.Append(" and b." + _AnesthesiaType1 + " = @" + _AnesthesiaType1);
                }
                strQuery.Append(" group by a." + _MainCode + ",c." + _Name);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.OROPERATIONVO = new OROPERATIONVO();
                    ORHEADERVO.OROPERATIONVO.Name = query[_Name].ToString();
                    ORHEADERVO.OROPERATIONVO.QTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchOP(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName + ", count(a." + _MainCode + ") as QTY");
                strQuery.Append(" FROM " + _tblOROPERATION + " a");
                strQuery.Append(" left join  " + _tblORHEADER + " b on a." + _ORID + " = b." + _ORID);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " d on a." + _SubCode + " = d." + _SubCode);
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, b." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and b." + _Surgeon1 + " = @" + _Surgeon1);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.AnesthesiaType1))
                {
                    strQuery.Append(" and b." + _AnesthesiaType1 + " = @" + _AnesthesiaType1);
                }
                strQuery.Append(" group by a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.OROPERATIONVO = new OROPERATIONVO();
                    ORHEADERVO.OROPERATIONVO.Name = query[_Name].ToString();
                    ORHEADERVO.OROPERATIONVO.SubName = query[_SubName].ToString();
                    ORHEADERVO.OROPERATIONVO.QTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchOPD(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName + ", b." + _Surgeon1 + ", e." + _DoctorName + ", count(a." + _MainCode + ") as QTY");
                strQuery.Append(" FROM " + _tblOROPERATION + " a");
                strQuery.Append(" left join  " + _tblORHEADER + " b on a." + _ORID + " = b." + _ORID);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " d on a." + _SubCode + " = d." + _SubCode);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " e on b." + _Surgeon1 + " = e." + _DOCTOR);
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, b." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and b." + _Surgeon1 + " = @" + _Surgeon1);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.AnesthesiaType1))
                {
                    strQuery.Append(" and b." + _AnesthesiaType1 + " = @" + _AnesthesiaType1);
                }
                strQuery.Append(" group by a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName + ", b." + _Surgeon1 + ", e." + _DoctorName);
                strQuery.Append(" Order by e." + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.Surgeon1 = query[_DoctorName].ToString();
                    ORHEADERVO.OROPERATIONVO = new OROPERATIONVO();
                    ORHEADERVO.OROPERATIONVO.Name = query[_Name].ToString();
                    ORHEADERVO.OROPERATIONVO.SubName = query[_SubName].ToString();
                    ORHEADERVO.OROPERATIONVO.QTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchStatCase(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName + ", b." + _Surgeon1
                    + ", e." + _DoctorName + " , b.HN, b.PatientName, b.ORDate, b.ORTime, b.CreateDate"
                    + ", count(a." + _MainCode + ") as QTY");
                strQuery.Append(" FROM " + _tblOROPERATION + " a");
                strQuery.Append(" left join  " + _tblORHEADER + " b on a." + _ORID + " = b." + _ORID);
                strQuery.Append(" left join " + _tblSETUPOPERATIONMAIN + " c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" left join " + _tblSETUPOPERATIONSUB + " d on a." + _SubCode + " = d." + _SubCode);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " e on b." + _Surgeon1 + " = e." + _DOCTOR);
                strQuery.Append(" where ORStatCase = 1");
                strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, b." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and b." + _Surgeon1 + " = @" + _Surgeon1);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.MainCode))
                {
                    strQuery.Append(" and a." + _MainCode + " = @" + _MainCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.OROPERATIONVO.SubCode))
                {
                    strQuery.Append(" and a." + _SubCode + " = @" + _SubCode);
                }
                if (!string.IsNullOrEmpty(_ORHEADERVO.AnesthesiaType1))
                {
                    strQuery.Append(" and b." + _AnesthesiaType1 + " = @" + _AnesthesiaType1);
                }
                strQuery.Append(" group by a." + _MainCode + ",c." + _Name + ", a." + _SubCode + ", d." + _SubName + ", b." + _Surgeon1 + ", e." + _DoctorName
                    + ", b.HN, b.PatientName, b.ORDate, b.ORTime, b.CreateDate");
                strQuery.Append(" Order by e." + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OROPERATIONVO.SubCode)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.Surgeon1 = query[_DoctorName].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.PatientName = query[_PatientName].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy") + " " + query[_ORTime].ToString().Substring(0, 5);
                    ORHEADERVO.strCreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.OROPERATIONVO = new OROPERATIONVO();
                    ORHEADERVO.OROPERATIONVO.Name = query[_Name].ToString();
                    ORHEADERVO.OROPERATIONVO.SubName = query[_SubName].ToString();

                    ORHEADERVO.OROPERATIONVO.QTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchStatWard(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _ORStatus + ", count(a." + _ORStatus + ") as QTY");
                strQuery.Append(" FROM " + _tblORHEADER + " a");
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, a." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126)");
                strQuery.Append(" and CONVERT(date, a." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                strQuery.Append(" group by a." + _ORStatus);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORStatus = ((EnumOR.ORStatus)int.Parse(query[_ORStatus].ToString())).ToString();
                    ORHEADERVO.QTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchAnesthesiaType1(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _AnesthesiaType1 + ",b." + _Name + ", Count(a." + _AnesthesiaType1 + ") as QTY");
                strQuery.Append(" FROM " + _tblORHEADER + " a");
                strQuery.Append(" left join  " + _VT_ANESTHESIA + " b on a." + _AnesthesiaType1 + " = b." + _CODE);
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, a." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, a." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                strQuery.Append(" and a." + _AnesthesiaType1 + "  is not null ");
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and a." + _Surgeon1 + " = @" + _Surgeon1);
                }
                strQuery.Append(" group by a." + _AnesthesiaType1 + ", b." + _Name);
                strQuery.Append(" Order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    ORHEADERVO.AnesthesiaTypeName = query[_Name].ToString();
                    ORHEADERVO.AnesthesiaTypeQTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchAnesthesiaType2(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT a." + _AnesthesiaType2 + ",b." + _Name + ", Count(a." + _AnesthesiaType2 + ") as QTY");
                strQuery.Append(" FROM " + _tblORHEADER + " a");
                strQuery.Append(" left join  " + _VT_ANESTHESIA + " b on a." + _AnesthesiaType2 + " = b." + _CODE);
                strQuery.Append(" where 1 = 1");
                strQuery.Append(" and CONVERT(date, a." + _ORDate + ", 126) >= CONVERT(date, @" + _ORDateFrom + ", 126) and CONVERT(date, a." + _ORDate + ", 126) <= CONVERT(date, @" + _ORDateTo + ", 126)");
                strQuery.Append(" and a." + _AnesthesiaType2 + "  is not null ");
                strQuery.Append(" and a." + _AnesthesiaSign + " = @" + _AnesthesiaSign);
                if (!string.IsNullOrEmpty(_ORHEADERVO.Surgeon1))
                {
                    strQuery.Append(" and a." + _Surgeon1 + " = @" + _Surgeon1);
                }
                strQuery.Append(" group by a." + _AnesthesiaType2 + ", b." + _Name);
                strQuery.Append(" Order by b." + _Name);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDateFrom, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateFrom)));
                parameter.Add(new IParameter(_ORDateTo, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDateTo)));
                parameter.Add(new IParameter(_AnesthesiaSign, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaSign)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    ORHEADERVO.AnesthesiaTypeName = query[_Name].ToString();
                    ORHEADERVO.AnesthesiaTypeQTY = ADOUtil.GetIntFromQuery(query["QTY"].ToString());

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchRoomType(DateTime ORDate)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select " + _RoomType + ", b." + _NAME + " as " + _RoomTypeName);
                strQuery.Append(" , (select COUNT(" + _RoomType + ") from " + _tblORHEADER + " AA left join " + _VT_APPOINTMENTMASTER + " BB ON(AA." + _AppointmentNo + " = BB." + _AppointmentNo + ") where aa." + _AdmitTimeType + " = 1 and a." + _RoomType + " = aa." + _RoomType + " and BB." + _ConfirmStatusType + " <> '6' and aa." + _ORStatus + " in (2, 3, 4) and aa." + _RoomType + " is not null and CONVERT(date, aa. " + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ) as " + _morning);
                strQuery.Append(" , (select COUNT(" + _RoomType + ") from " + _tblORHEADER + " AA left join " + _VT_APPOINTMENTMASTER + " BB ON(AA." + _AppointmentNo + " = BB." + _AppointmentNo + ") where aa." + _AdmitTimeType + " = 2 and a." + _RoomType + " = aa." + _RoomType + " and aa." + _ORStatus + " in (2, 3, 4) and aa." + _RoomType + " is not null and BB." + _ConfirmStatusType + " <> '6' and CONVERT(date, aa. " + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ) as " + _evening);
                strQuery.Append(" from dbo." + _tblORHEADER + " a");
                strQuery.Append(" left join dbo." + _VT_ROOMTYPE + " b on (a." + _RoomType + " = b." + _CODE + ")");
                strQuery.Append(" left join dbo." + _VT_APPOINTMENTMASTER + " c on (a." + _AppointmentNo + " = c." + _AppointmentNo + ")");
                strQuery.Append(" where");
                strQuery.Append(" " + _ORStatus + " in (2,3,4) and a." + _RoomType + " is not null");
                strQuery.Append(" and CONVERT(date, a. " + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                strQuery.Append(" and c. " + _ConfirmStatusType + " <> '6'");
                strQuery.Append(" group by " + _RoomType + ", b." + _NAME);
                strQuery.Append(" order by " + _RoomType);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDate)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.RoomType = query[_RoomType].ToString();
                    ORHEADERVO.RoomTypeName = query[_RoomTypeName].ToString();
                    ORHEADERVO.morning = query[_morning].ToString();
                    ORHEADERVO.evening = query[_evening].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchByHN(string HN)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select " + _ORID + "," + _HN);
                strQuery.Append(" from dbo." + _tblORHEADER);
                strQuery.Append(" where " + _HN + " = @" + _HN);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(HN)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchUnderPatient(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select a.*, b." + _DoctorName + " as SurgeonName");
                strQuery.Append(" , c." + _Name + " as ORRoomName");
                strQuery.Append(" from " + _tblORHEADER + " as a");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as b on a." + _Surgeon1 + " = b." + _DOCTOR);
                strQuery.Append(" left join SETUPORROOM as c on a." + _ORRoom + " = c." + _CODE);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORHEADERVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }
                if (_ORHEADERVO.ORDate != null)
                {
                    strQuery.Append(" and CONVERT(date, " + _ORDate + ", 126) <> CONVERT(date, @" + _ORDate + ", 126) ");
                }
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.HN)));
                if (_ORHEADERVO.ORDate != null)
                {
                    parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                }
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    ORHEADERVO.SurgeonName = query["SurgeonName"].ToString();
                    ORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    ORHEADERVO.strORRoom = query["ORRoomName"].ToString();
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchPrevOR(ORHEADERVO _ORHEADERVO)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select TOP 1 * ");
                strQuery.Append(" from " + _tblORHEADER +" A");
                //strQuery.Append(" left join "+ _tblPOSTORDETAIL +" B ON(A.ORID = B.ORID) "); _tblOROPERATION
                strQuery.Append(" left join " + _tblOROPERATION + " B ON(A.ORID = B.ORID) "); 
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORHEADERVO.HN))
                {
                    strQuery.Append(" and A." + _HN + " = @" + _HN);
                }
                if (_ORHEADERVO.ORDate != null)
                {
                    strQuery.Append(" and CONVERT(date, A." + _ORDate + ", 126) < CONVERT(date, @" + _ORDate + ", 126) ");
                }
                strQuery.Append(" and A.CxlDateTime is null");
                strQuery.Append(" and B.ORID is not null");
                strQuery.Append(" ORDER BY A." + _ORDate + " DESC ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.HN)));
                if (_ORHEADERVO.ORDate != null)
                {
                    parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                }
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptSurgery(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag, string strCxlConfirmFlag, string strHN)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.ORDate,A.ORTime, A.ORID ");
                strQuery.Append(" , A.Surgeon1 ");
                strQuery.Append(" , A.PatientName ");
                strQuery.Append(" , C.DoctorName As SurgeonName ");
                strQuery.Append(" , A.AnesthesiaDoctor1 ");
                strQuery.Append(" , D.DoctorName As AnesDoctorName ");
                strQuery.Append(" , Case when B.ORCaseType = 1 then 'true' else 'false' end As  ElectiveCase ");
                strQuery.Append(" , Case when B.ORCaseType = 2 then 'true' else 'false' end As  UrgencyCase ");
                strQuery.Append(" , A.ORCase ");
                strQuery.Append(" , A.HN ");
                strQuery.Append(" , B.StartORDateTime ");
                strQuery.Append(" , B.FinishORDateTime ");
                strQuery.Append(" , DATEDIFF(Mi, B.StartORDateTime, B.FinishORDateTime) As OperationTime ");
                strQuery.Append(" , B.StartAnesDateTime ");
                strQuery.Append(" , B.FinishAnesDateTime ");
                strQuery.Append(" , DATEDIFF(Mi, B.StartAnesDateTime, B.FinishAnesDateTime) As AnesTime ");
                strQuery.Append(" , B.StartBlockDateTime ");
                strQuery.Append(" , B.FinishBlockDateTime ");
                strQuery.Append(" , DATEDIFF(Mi, B.StartBlockDateTime, B.FinishBlockDateTime) As BlockTime ");
                strQuery.Append(" , B.StartRecoveryDateTime ");
                strQuery.Append(" , B.FinishRecoveryDateTime ");
                strQuery.Append(" , DATEDIFF(Mi, B.StartRecoveryDateTime, B.FinishRecoveryDateTime) As RecoveryTime ");
                strQuery.Append(" , A.Remark ");
                strQuery.Append(" , E.Name As CxlReason ");
                strQuery.Append(" , F.Name As CxlPostORReason ");
                strQuery.Append(" , A.AppointmentNo ");
                strQuery.Append(" , G.Name As ORRoomName ");
                strQuery.Append(" from ");
                strQuery.Append(" dbo.ORHEADER A ");
                strQuery.Append(" left join dbo.POSTORDETAIL B On(A.ORID = B.ORID) ");
                strQuery.Append(" left join dbo.VT_DOCTORMASTER C on(A.Surgeon1 = C.DOCTOR) ");
                strQuery.Append(" left join dbo.VT_DOCTORMASTER D on(A.AnesthesiaDoctor1 = D.DOCTOR) ");
                strQuery.Append(" left join dbo.VT_REASON E on(A.CxlReason = E.CODE) ");
                strQuery.Append(" left join dbo.VT_REASON F on(A.CxlPostORReason = F.CODE) ");
                strQuery.Append(" left join dbo.SETUPORROOM G on(A.ORRoom = G.CODE) ");
                strQuery.Append(" where A.ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null ");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null ");
                    }
                    
                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                if (!string.IsNullOrEmpty(strHN))
                {
                    strQuery.Append(" and A.HN = '"+ strHN +"'");
                }
                strQuery.Append(" order by A.ORDate,A.Surgeon1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvORDate = string.Empty;
                string prvSurgeon1 = string.Empty;
                int xNo = 1;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    try { ORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5); } catch { }
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    ORHEADERVO.ElectiveCase = ADOUtil.GetBoolFromQuery(query[_ElectiveCase].ToString());
                    ORHEADERVO.UrgencyCase = ADOUtil.GetBoolFromQuery(query[_UrgencyCase].ToString());
                    ORHEADERVO.OperationTime = query[_OperationTime].ToString();
                    ORHEADERVO.StartORDateTime = ADOUtil.GetDateFromQuery(query[_StartORDateTime].ToString());
                    ORHEADERVO.FinishORDateTime = ADOUtil.GetDateFromQuery(query[_FinishORDateTime].ToString());
                    ORHEADERVO.AnesTime = query[_AnesTime].ToString();
                    ORHEADERVO.StartAnesDateTime = ADOUtil.GetDateFromQuery(query[_StartAnesDateTime].ToString());
                    ORHEADERVO.FinishAnesDateTime = ADOUtil.GetDateFromQuery(query[_FinishAnesDateTime].ToString());
                    ORHEADERVO.BlockTime = query[_BlockTime].ToString();
                    ORHEADERVO.StartBlockDateTime = ADOUtil.GetDateFromQuery(query[_StartBlockDateTime].ToString());
                    ORHEADERVO.FinishBlockDateTime = ADOUtil.GetDateFromQuery(query[_FinishBlockDateTime].ToString());
                    ORHEADERVO.RecoveryTime = query[_RecoveryTime].ToString();
                    ORHEADERVO.StartRecoveryDateTime = ADOUtil.GetDateFromQuery(query[_StartRecoveryDateTime].ToString());
                    ORHEADERVO.FinishRecoveryDateTime = ADOUtil.GetDateFromQuery(query[_FinishRecoveryDateTime].ToString());
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.PatientName = query["PatientName"].ToString();
                    ORHEADERVO.Remark = query["Remark"].ToString();
                    ORHEADERVO.SurgeonName = query["SurgeonName"].ToString();
                    ORHEADERVO.AnesDoctorName = query["AnesDoctorName"].ToString();
                    ORHEADERVO.CxlReason = query["CxlReason"].ToString();
                    ORHEADERVO.AppointmentNo = query["AppointmentNo"].ToString();
                    ORHEADERVO.CxlPostORReason = query["CxlPostORReason"].ToString();
                    ORHEADERVO.ORRoom = query["ORRoomName"].ToString();


                    if (ORHEADERVO.strORDate == prvORDate)
                    {
                       //  ORHEADERVO.strORDate = "\"";
                    }
                    else
                    {
                        prvORDate = ORHEADERVO.strORDate;
                    }

                    if (ORHEADERVO.Surgeon1 == prvSurgeon1)
                    {
                        //ORHEADERVO.SurgeonName = "\"";
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    else
                    {
                        prvSurgeon1 = ORHEADERVO.Surgeon1;
                        xNo = 1;
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptTop5(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" SELECT A.ICD,F.Name as ICDName,C.Name As MainOperation,D.SubName As SubOperation,COUNT(A.ICD) As 'Amount' ");
                strQuery.Append(" FROM dbo.POSTORICD A ");
                strQuery.Append(" left join SETUPICD F on(A.ICD = F.Code) ");
                strQuery.Append(" left join dbo.POSTOROPERATION B on(A.ORID = B.ORID) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONMAIN C on(B.MainCode = C.MainCode) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONSUB D on(B.MainCode = D.MainCode And B.SubCode = D.SubCode) ");
                strQuery.Append(" left join dbo.ORHEADER E On(A.ORID = E.ORID) ");
                strQuery.Append(" where E.ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and E.CxlDateTime is not null");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and E.CxlDateTime is null");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and E.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (E.CxlPostOR <> '1' or E.CxlPostOR is null) ");
                    }

                }
                strQuery.Append(" GROUP BY A.ICD,F.Name,C.Name,D.SubName ");
                strQuery.Append(" ORDER BY COUNT(A.ICD) DESC ");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvICDName = string.Empty;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ICD = query["ICD"].ToString();
                    ORHEADERVO.ICDName = query["ICDName"].ToString();
                    ORHEADERVO.MainOperation = query["MainOperation"].ToString();
                    ORHEADERVO.SubOperation = query["SubOperation"].ToString();
                    ORHEADERVO.Amount = ADOUtil.GetDoubleFromQuery(query["Amount"].ToString());
                    if (ORHEADERVO.ICDName == prvICDName)
                    {
                        if (string.IsNullOrEmpty(ORHEADERVO.ICDName))
                        { ORHEADERVO.ICDName = string.Empty; }
                        else
                        {
                            //ORHEADERVO.ICDName = "\"";
                        }
                    }
                    else
                    {
                        prvICDName = ORHEADERVO.ICDName;
                    }
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptSurgeryStatAth(DateTime ORDateF, DateTime ORDateT, string NurseType,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.ORDate,A.HN,A.PatientName,D.SubName As 'Procedure', E.Name as 'NurseName',A.AppointmentNo ");
                strQuery.Append(" from ");
                strQuery.Append(" dbo.ORHEADER A ");
                strQuery.Append(" left ");
                strQuery.Append(" join dbo.POSTOROPERATION B On(A.ORID = B.ORID) ");
                strQuery.Append(" left join dbo.POSTORNURSE C On(A.ORID = C.ORID) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONSUB D On(B.MainCode = D.MainCode And B.SubCode = D.SubCode) ");
                strQuery.Append(" left join dbo.VT_NURSEMASTER E On(C.NurseCode = E.Code) ");
                strQuery.Append(" WHERE A.ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                strQuery.Append(" and D.SubName is not null ");
                strQuery.Append(" and C.NurseType = '" + NurseType + "' ");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                strQuery.Append(" order by A.ORDate ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvORDate = string.Empty;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.PatientName = query[_PatientName].ToString();
                    ORHEADERVO.Procedure = query["Procedure"].ToString();
                    ORHEADERVO.NurseName = query["NurseName"].ToString();
                    ORHEADERVO.AppointmentNo = query["AppointmentNo"].ToString();
                    if (ORHEADERVO.strORDate == prvORDate)
                    {
                        ORHEADERVO.strORDate = "\"";
                    }
                    else
                    {
                        prvORDate = ORHEADERVO.strORDate;
                    }
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptSurgery48(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag,string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.ORID, A.ORDate");
                strQuery.Append(" , A.HN");
                strQuery.Append(" , A.PatientName");
                strQuery.Append(" ,B.StartORDateTime");
                strQuery.Append(" , B.FinishORDateTime");
                strQuery.Append(" , STUFF((select ',' + ICD from dbo.POSTORICD AA where AA.ORID = A.ORID FOR XML PATH('')), 1, 1, '') As Diagnosis");
                strQuery.Append(" , C.DoctorName As SurgeonName");
                strQuery.Append(" , D.Name As CxlReason ");
                strQuery.Append(" , E.Name As CxlPostORReason ");
                strQuery.Append(" , A.AppointmentNo ");
                strQuery.Append(" , B.ORWoundType1 As Reoperation");
                strQuery.Append(" , B.Indicator");
                strQuery.Append(" from");
                strQuery.Append(" dbo.ORHEADER A");
                strQuery.Append(" left join dbo.POSTORDETAIL B On(A.ORID = B.ORID)");
                strQuery.Append(" left join dbo.VT_DOCTORMASTER C on(A.Surgeon1 = C.DOCTOR)");
                strQuery.Append(" left join dbo.VT_REASON D on(A.CxlReason = D.CODE) ");
                strQuery.Append(" left join dbo.VT_REASON E on(A.CxlPostORReason = E.CODE) ");
                strQuery.Append(" where ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                strQuery.Append(" and B.ORWoundType1 = 1");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                strQuery.Append(" order by A.ORDate,A.HN");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvORDate = string.Empty;
                string prvSurgeon1 = string.Empty;
                int xNo = 1;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = query[_ORID].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.PatientName = query["PatientName"].ToString();
                    ORHEADERVO.StartORDateTime = ADOUtil.GetDateFromQuery(query[_StartORDateTime].ToString());
                    ORHEADERVO.FinishORDateTime = ADOUtil.GetDateFromQuery(query[_FinishORDateTime].ToString());
                    ORHEADERVO.CxlReason = query["CxlReason"].ToString();
                    ORHEADERVO.AppointmentNo = query["AppointmentNo"].ToString();
                    ORHEADERVO.CxlPostORReason = query["CxlPostORReason"].ToString();
                    ORHEADERVO.Indicator = query["Indicator"].ToString();
                    string strReoperation = query["Reoperation"].ToString();
                    if (query["Reoperation"].ToString() == "True")
                    { 
                    ORHEADERVO.Reoperation = true;
                    }
                    else
                    {
                        ORHEADERVO.Reoperation = false;
                    }

                    if (ORHEADERVO.StartORDateTime != null)
                    {
                        ORHEADERVO.strStartORDateTime = ADOUtil.GetDateFromQuery(query[_StartORDateTime].ToString()).Value.ToString("dd-MM-yyyy");
                    }
                    ORHEADERVO.Diagnosis = query["Diagnosis"].ToString();
                    ORHEADERVO.SurgeonName = query["SurgeonName"].ToString();

                    if (ORHEADERVO.strORDate == prvORDate)
                    {
                        ORHEADERVO.strORDate = "\"";
                    }
                    else
                    {
                        prvORDate = ORHEADERVO.strORDate;
                    }

                    if (ORHEADERVO.Surgeon1 == prvSurgeon1)
                    {
                        //ORHEADERVO.SurgeonName = "\"";
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    else
                    {
                        prvSurgeon1 = ORHEADERVO.Surgeon1;
                        xNo = 1;
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptSurgeryProcedrue(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag,string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.Surgeon1");
                strQuery.Append(" , C.DoctorName As SurgeonName");
                strQuery.Append(" , A.HN");
                strQuery.Append(" , Case when A.ORStatus = 1 then 'OPD' when A.ORStatus = 2 then 'IPD' else '' end As 'IPDOPD'");
                strQuery.Append(" , A.PatientName");
                strQuery.Append(" , D.BirthDateTime");
                strQuery.Append(" ,case when D.Sex = '1' then 'ชาย' when D.Sex = '2' then 'หญิง' else D.sex end As 'Gender'");
                strQuery.Append(" , F.NAME As 'AnesthesiaType'");
                strQuery.Append(" , A.ORDate As 'OperationDate'");
                strQuery.Append(" , H.Name as 'Operation'");
                strQuery.Append(" , case when I.SubName is null then E.SubName else I.SubName end as 'Procedrue'");
                strQuery.Append(" ,case when E.ORProcedureType = 1 then 'Major' when e.ORProcedureType = 2 then 'Minor' else '' end as 'OROperationType'");
                strQuery.Append(" , STUFF((select ',' + ICD from dbo.POSTORICD AA where AA.ORID = A.ORID FOR XML PATH('')), 1, 1, '') As 'Diagnosis'");
                strQuery.Append(" , J.Name As CxlReason ");
                strQuery.Append(" , K.Name As CxlPostORReason ");
                strQuery.Append(" , A.AppointmentNo ");
                strQuery.Append(" from");
                strQuery.Append(" dbo.ORHEADER A");
                strQuery.Append(" left");
                strQuery.Append(" join dbo.POSTORDETAIL B On(A.ORID = B.ORID)");
                strQuery.Append(" left join dbo.VT_DOCTORMASTER C on (A.Surgeon1 = C.DOCTOR)");
                strQuery.Append(" left");
                strQuery.Append(" join dbo.VT_PATIENTMASTER D ON(A.HN = D.HN)");
                strQuery.Append(" left join dbo.POSTOROPERATION E On(E.ORID = A.ORID)");
                strQuery.Append(" left join dbo.VT_ANESTHESIA F On(A.AnesthesiaType1 = F.CODE)");
                strQuery.Append(" left join dbo.VT_ANESTHESIA G On(A.AnesthesiaType2 = G.CODE)");
                strQuery.Append(" left join dbo.SETUPOPERATIONMAIN H On(E.MainCode = H.MainCode)");
                strQuery.Append(" left join dbo.SETUPOPERATIONSUB I On(E.MainCode = I.MainCode And E.SubCode = I.SubCode)");
                strQuery.Append(" left join dbo.VT_REASON J on(A.CxlReason = J.CODE) ");
                strQuery.Append(" left join dbo.VT_REASON K on(A.CxlPostORReason = K.CODE) ");
                strQuery.Append(" where ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                //strQuery.Append(" order by A.Surgeon1");
                strQuery.Append(" order by A.ORDate");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                int xNo = 1;
                string prvSurgeon1 = string.Empty;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    ORHEADERVO.SurgeonName = query["SurgeonName"].ToString();
                    ORHEADERVO.HN = query[_HN].ToString();
                    ORHEADERVO.IPDOPD = query["IPDOPD"].ToString();
                    ORHEADERVO.PatientName = query[_PatientName].ToString();
                    ORHEADERVO.CxlReason  = query["CxlReason"].ToString();
                    ORHEADERVO.AppointmentNo = query["AppointmentNo"].ToString();
                    ORHEADERVO.CxlPostORReason = query["CxlPostORReason"].ToString();

                    ORHEADERVO.BirthDateTime = ADOUtil.GetDateFromQuery(query["BirthDateTime"].ToString());
                    if (ORHEADERVO.BirthDateTime != null)
                    {
                        ORHEADERVO.Age = ORUtils.getAge(ORHEADERVO.BirthDateTime);
                        ORHEADERVO.strBirthDateTime = ORHEADERVO.BirthDateTime.Value.ToString("dd-MM-yyyy");
                    }
                    ORHEADERVO.Gender = query["Gender"].ToString();
                    ORHEADERVO.AnesthesiaType = query["AnesthesiaType"].ToString();
                    ORHEADERVO.OperationDate = ADOUtil.GetDateFromQuery(query["OperationDate"].ToString());
                    if (ORHEADERVO.OperationDate != null)
                    {
                        ORHEADERVO.strOperationDate = ORHEADERVO.OperationDate.Value.ToString("dd-MM-yyyy");
                    }
                    ORHEADERVO.Operation = query["Operation"].ToString();
                    ORHEADERVO.Procedrue = query["Procedrue"].ToString();
                    ORHEADERVO.OROperationType = query["OROperationType"].ToString();
                    ORHEADERVO.Diagnosis = query["Diagnosis"].ToString();                    

                    if (ORHEADERVO.Surgeon1 == prvSurgeon1)
                    {
                        //ORHEADERVO.SurgeonName = "\"";
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    else
                    {
                        prvSurgeon1 = ORHEADERVO.Surgeon1;
                        xNo = 1;
                        ORHEADERVO.No = xNo.ToString();
                        xNo++;
                    }
                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptSurgeryOrgan(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.ORDate,B.Organ,B1.Name As OrganName,B.OrganPosition,C.Name As Operation,D.SubName As 'Procedure',COUNT(D.SubName) As 'AmtSurgeon' from ");
                strQuery.Append(" dbo.ORHEADER A ");
                strQuery.Append(" left ");
                strQuery.Append(" join dbo.POSTOROPERATION B On(A.ORID = B.ORID) ");
                strQuery.Append(" left join dbo.SETUPORGANMAIN B1 on(B.Organ = B1.MainCode) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONMAIN C On(B.MainCode = C.MainCode) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONSUB D On(B.MainCode = D.MainCode And B.SubCode = D.SubCode) ");
                strQuery.Append(" where A.ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                strQuery.Append(" Group By A.ORDate,B.Organ,B1.Name,B.OrganPosition,C.Name,D.SubName ");
                strQuery.Append(" Having COUNT(D.SubName) > 0 ");
                strQuery.Append(" ORDER BY A.ORDate DESC");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvORDate = string.Empty;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.Organ = query["Organ"].ToString();
                    ORHEADERVO.OrganName = query["OrganName"].ToString();
                    ORHEADERVO.OrganPosition = query["OrganPosition"].ToString();
                    ORHEADERVO.Operation = query["Operation"].ToString();
                    ORHEADERVO.Procedure = query["Procedure"].ToString();
                    ORHEADERVO.AmtSurgeon = ADOUtil.GetIntFromQuery(query["AmtSurgeon"].ToString());

                    if (ORHEADERVO.strORDate == prvORDate)
                    {
                        ORHEADERVO.strORDate = "\"";
                    }
                    else
                    {
                        prvORDate = ORHEADERVO.strORDate;
                    }

                    retValue.Add(ORHEADERVO);
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

        internal List<ORHEADERVO> SearchrptIndicatorProcedure(DateTime ORDateF, DateTime ORDateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            List<ORHEADERVO> retValue = new List<ORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select A.Surgeon1 As 'Doctor' ");
                strQuery.Append(" , C.DoctorName As SurgeonName ");
                strQuery.Append(" , A.HN ");
                strQuery.Append(" , Case when A.ORStatus = 1 then 'OPD' when A.ORStatus = 2 then 'IPD' else '' end As 'IPD/OPD' ");
                strQuery.Append(" , A.PatientName ");
                strQuery.Append(" , D.BirthDateTime ");
                strQuery.Append(" ,case when D.Sex = '1' then 'ชาย' when D.Sex = '2' then 'หญิง' else D.sex end As 'Gender' ");
                strQuery.Append(" , F.NAME As 'AnesthesiaType' ");
                strQuery.Append(" , A.ORDate ");
                strQuery.Append(" , H.Name as 'Operation' ");
                strQuery.Append(" , I.SubName as 'Procedrue' ");
                strQuery.Append(" ,case when E.ORProcedureType = 1 then 'Major' when e.ORProcedureType = 2 then 'Minor' else '' end as 'OROperationType' ");
                strQuery.Append(" , STUFF((select ',' + ICD from dbo.POSTORICD AA where AA.ORID = A.ORID FOR XML PATH('')), 1, 1, '') As 'Diagnosis' ");
                strQuery.Append(" , j.Indicator, J.ORWoundType2,J.ORWoundType3,J.ORWoundType4,J.ChangOperation,J.HR48,J.Day30");
                strQuery.Append(" , K.NAME as 'CxlReason' ");
                strQuery.Append(" , L.Name As CxlPostORReason ");
                strQuery.Append(" , A.AppointmentNo ");
                strQuery.Append(" from dbo.ORHEADER A ");
                strQuery.Append(" left join dbo.POSTORDETAIL B On(A.ORID = B.ORID) ");
                strQuery.Append(" left join dbo.VT_DOCTORMASTER C on (A.Surgeon1 = C.DOCTOR) ");
                strQuery.Append("left join dbo.VT_PATIENTMASTER D ON(A.HN = D.HN) ");
                strQuery.Append(" left join dbo.POSTOROPERATION E On(E.ORID = A.ORID) ");
                strQuery.Append(" left join dbo.VT_ANESTHESIA F On(A.AnesthesiaType1 = F.CODE) ");
                strQuery.Append(" left join dbo.VT_ANESTHESIA G On(A.AnesthesiaType2 = G.CODE) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONMAIN H On(E.MainCode = H.MainCode) ");
                strQuery.Append(" left join dbo.SETUPOPERATIONSUB I On(E.MainCode = I.MainCode And E.SubCode = I.SubCode) ");
                strQuery.Append(" left join dbo.POSTORDETAIL J On(A.ORID = J.ORID)");
                strQuery.Append(" left join dbo.VT_REASON K on(A.CxlReason = K.CODE) ");
                strQuery.Append(" left join dbo.VT_REASON L on(A.CxlPostORReason = L.CODE) ");
                strQuery.Append(" where ORDate between '" + ORDateF.Year + "-" + ORDateF.Month + "-" + ORDateF.Day + "' and '" + ORDateT.Year + "-" + ORDateT.Month + "-" + ORDateT.Day + "'");
                if (!string.IsNullOrEmpty(strCxlCheckFlag))
                {
                    if (strCxlCheckFlag == "1")
                    {
                        strQuery.Append(" and A.CxlDateTime is not null ");
                    }
                    else if (strCxlCheckFlag == "2")
                    {
                        strQuery.Append(" and A.CxlDateTime is null ");
                    }

                }
                if (!string.IsNullOrEmpty(strCxlConfirmFlag))
                {
                    if (strCxlConfirmFlag == "1")
                    {
                        strQuery.Append(" and A.CxlPostOR = '1' ");
                    }
                    else if (strCxlConfirmFlag == "2")
                    {
                        strQuery.Append(" and (A.CxlPostOR <> '1' or A.CxlPostOR is null) ");
                    }

                }
                strQuery.Append(" Order by A.Surgeon1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateF)));
                //parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(ORDateT)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                string prvSurgeon = string.Empty;
                while (query.Read())
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.Surgeon = query["Doctor"].ToString();
                    ORHEADERVO.SurgeonName = query["SurgeonName"].ToString();
                    ORHEADERVO.HN = query["HN"].ToString();
                    ORHEADERVO.IPDOPD = query["IPD/OPD"].ToString();
                    ORHEADERVO.PatientName = query["PatientName"].ToString();
                    ORHEADERVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    if (ORHEADERVO.BirthDateTime != null)
                    {
                        ORHEADERVO.strBirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString()).Value.ToString("dd-MM-yyyy");
                    }
                    ORHEADERVO.Gender = query["Gender"].ToString();
                    ORHEADERVO.AnesthesiaType = query["AnesthesiaType"].ToString();
                    ORHEADERVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    if (ORHEADERVO.ORDate != null)
                    {
                        ORHEADERVO.strORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).Value.ToString("dd-MM-yyyy");
                    }
                    ORHEADERVO.Operation = query["Operation"].ToString();
                    ORHEADERVO.Procedrue = query["Procedrue"].ToString();
                    ORHEADERVO.OROperationType = query["OROperationType"].ToString();
                    ORHEADERVO.Diagnosis = query["Diagnosis"].ToString();

                    ORHEADERVO.Indicator = query["Indicator"].ToString();
                    ORHEADERVO.ORWoundType2 = ADOUtil.GetBoolFromQuery(query["ORWoundType2"].ToString());
                    ORHEADERVO.ORWoundType3 = ADOUtil.GetBoolFromQuery(query["ORWoundType3"].ToString());
                    ORHEADERVO.ORWoundType4 = ADOUtil.GetBoolFromQuery(query["ORWoundType4"].ToString());
                    ORHEADERVO.ChangOperation = ADOUtil.GetBoolFromQuery(query["ChangOperation"].ToString());
                    ORHEADERVO.HR48 = ADOUtil.GetBoolFromQuery(query["HR48"].ToString());
                    ORHEADERVO.Day30 = ADOUtil.GetBoolFromQuery(query["Day30"].ToString());
                    ORHEADERVO.CxlReason = query["CxlReason"].ToString();
                    ORHEADERVO.AppointmentNo = query["AppointmentNo"].ToString();
                    ORHEADERVO.CxlPostORReason = query["CxlPostORReason"].ToString();

                    if (ORHEADERVO.Surgeon == prvSurgeon)
                    {
                        //ORHEADERVO.SurgeonName = "\"";
                    }
                    else
                    {
                        prvSurgeon = ORHEADERVO.Surgeon;
                    }

                    retValue.Add(ORHEADERVO);
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

        internal bool SecrchCheckdup(string ORID)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblORHEADER);
                strQuery.Append(" where " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORID)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch
            {
                retVal = false;
            }
            return retVal;
        }

        internal ReturnValue CheckCountByAppointment(string AppointmentNo)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblORHEADER);
                strQuery.Append(" where " + _AppointmentNo + " = @" + _AppointmentNo);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AppointmentNo, IDbType.VarChar, DBNullConvert.From(AppointmentNo)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {
                retVal.Value = false;
                retVal.Exception = ex;
            }
            return retVal;
        }

        internal ReturnValue Insert(ORHEADERVO _ORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblORHEADER + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ORID);
                sbValue.Append("@" + _ORID);

                sbInsert.Append("," + _HN);
                sbValue.Append(",@" + _HN);

                sbInsert.Append("," + _PatientName);
                sbValue.Append(",@" + _PatientName);

                sbInsert.Append("," + _PatientInfection);
                sbValue.Append(",@" + _PatientInfection);

                sbInsert.Append("," + _PatientType1);
                sbValue.Append(",@" + _PatientType1);

                sbInsert.Append("," + _PatientType2);
                sbValue.Append(",@" + _PatientType2);

                sbInsert.Append("," + _PatientUP);
                sbValue.Append(",@" + _PatientUP);

                sbInsert.Append("," + _ORDate);
                sbValue.Append(",@" + _ORDate);

                sbInsert.Append("," + _ORTime);
                sbValue.Append(",@" + _ORTime);

                sbInsert.Append("," + _ArrivalTime);
                sbValue.Append(",@" + _ArrivalTime);

                sbInsert.Append("," + _ORTimeFollow);
                sbValue.Append(",@" + _ORTimeFollow);

                sbInsert.Append("," + _ORStatCase);
                sbValue.Append(",@" + _ORStatCase);

                sbInsert.Append("," + _ORCase);
                sbValue.Append(",@" + _ORCase);

                sbInsert.Append("," + _ORSpecificType);
                sbValue.Append(",@" + _ORSpecificType);

                sbInsert.Append("," + _ORStatus);
                sbValue.Append(",@" + _ORStatus);

                sbInsert.Append("," + _AdmitTimeType);
                sbValue.Append(",@" + _AdmitTimeType);

                sbInsert.Append("," + _RoomType);
                sbValue.Append(",@" + _RoomType);

                sbInsert.Append("," + _ORRoom);
                sbValue.Append(",@" + _ORRoom);

                sbInsert.Append("," + _AnesthesiaType1);
                sbValue.Append(",@" + _AnesthesiaType1);

                sbInsert.Append("," + _AnesthesiaType2);
                sbValue.Append(",@" + _AnesthesiaType2);

                sbInsert.Append("," + _AnesthesiaSign);
                sbValue.Append(",@" + _AnesthesiaSign);

                sbInsert.Append("," + _Surgeon1);
                sbValue.Append(",@" + _Surgeon1);

                sbInsert.Append("," + _Surgeon2);
                sbValue.Append(",@" + _Surgeon2);

                sbInsert.Append("," + _Surgeon3);
                sbValue.Append(",@" + _Surgeon3);

                sbInsert.Append("," + _SurgeonMaster);
                sbValue.Append(",@" + _SurgeonMaster);

                sbInsert.Append("," + _AnesthesiaDoctor1);
                sbValue.Append(",@" + _AnesthesiaDoctor1);

                sbInsert.Append("," + _AnesthesiaDoctor2);
                sbValue.Append(",@" + _AnesthesiaDoctor2);

                sbInsert.Append("," + _AnesthesiaDoctor3);
                sbValue.Append(",@" + _AnesthesiaDoctor3);

                sbInsert.Append("," + _AnesthesiaNurse1);
                sbValue.Append(",@" + _AnesthesiaNurse1);

                sbInsert.Append("," + _AnesthesiaNurse2);
                sbValue.Append(",@" + _AnesthesiaNurse2);

                sbInsert.Append("," + _AnesthesiaNurse3);
                sbValue.Append(",@" + _AnesthesiaNurse3);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);

                sbInsert.Append("," + _AppointmentNo);
                sbValue.Append(",@" + _AppointmentNo);

                sbInsert.Append("," + _CreateBy);
                sbValue.Append(",@" + _CreateBy);

                sbInsert.Append("," + _CreateDate);
                sbValue.Append(",@" + _CreateDate);

                sbInsert.Append("," + _Prediag);
                sbValue.Append(",@" + _Prediag);

                sbInsert.Append(",SuggestByUser");
                sbValue.Append(",@SuggestByUser");

                sbInsert.Append(",RequestByUser");
                sbValue.Append(",@RequestByUser");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.HN)));
                parameter.Add(new IParameter(_PatientName, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.PatientName)));
                parameter.Add(new IParameter(_PatientInfection, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientInfection)));
                parameter.Add(new IParameter(_PatientType1, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientType1)));
                parameter.Add(new IParameter(_PatientType2, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientType2)));
                parameter.Add(new IParameter(_PatientUP, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientUP)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                parameter.Add(new IParameter(_ORTime, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORTime)));
                parameter.Add(new IParameter(_ArrivalTime, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ArrivalTime)));
                parameter.Add(new IParameter(_ORTimeFollow, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.ORTimeFollow)));
                parameter.Add(new IParameter(_ORStatCase, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.ORStatCase)));
                parameter.Add(new IParameter(_ORCase, IDbType.Int, DBNullConvert.From(_ORHEADERVO.ORCase, false)));
                parameter.Add(new IParameter(_ORSpecificType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORSpecificType)));
                parameter.Add(new IParameter(_ORStatus, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORStatus)));
                parameter.Add(new IParameter(_AdmitTimeType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AdmitTimeType)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.RoomType)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORRoom)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));
                parameter.Add(new IParameter(_AnesthesiaType2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType2)));
                parameter.Add(new IParameter(_AnesthesiaSign, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaSign)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon3)));
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.SurgeonMaster)));
                parameter.Add(new IParameter(_AnesthesiaDoctor1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor1)));
                parameter.Add(new IParameter(_AnesthesiaDoctor2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor2)));
                parameter.Add(new IParameter(_AnesthesiaDoctor3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor3)));
                parameter.Add(new IParameter(_AnesthesiaNurse1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse1)));
                parameter.Add(new IParameter(_AnesthesiaNurse2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse2)));
                parameter.Add(new IParameter(_AnesthesiaNurse3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse3)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Remark)));
                parameter.Add(new IParameter(_AppointmentNo, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AppointmentNo)));
                parameter.Add(new IParameter(_CreateBy, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CreateBy)));
                parameter.Add(new IParameter(_CreateDate, IDbType.DateTime, DBNullConvert.From(_ORHEADERVO.CreateDate)));
                //parameter.Add(new IParameter(_UpdateBy, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.UpdateBy)));
                parameter.Add(new IParameter(_Prediag, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Prediag)));
                parameter.Add(new IParameter("SuggestByUser", IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.SuggestByUser)));
                parameter.Add(new IParameter("RequestByUser", IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.RequestByUser)));
                
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

        internal ReturnValue Update(ORHEADERVO _ORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblORHEADER + " SET ");

                sbQuery.Append("" + _HN + " = @" + _HN);
                sbQuery.Append("," + _PatientName + " = @" + _PatientName);
                sbQuery.Append("," + _PatientInfection + " = @" + _PatientInfection);
                sbQuery.Append("," + _PatientType1 + " = @" + _PatientType1);
                sbQuery.Append("," + _PatientType2 + " = @" + _PatientType2);
                sbQuery.Append("," + _PatientUP + " = @" + _PatientUP);
                sbQuery.Append("," + _Onmed + " = @" + _Onmed);
                sbQuery.Append("," + _OnmedNote + " = @" + _OnmedNote);
                sbQuery.Append("," + _ORDate + " = @" + _ORDate);
                sbQuery.Append("," + _ORTime + " = @" + _ORTime);
                sbQuery.Append("," + _ArrivalTime + " = @" + _ArrivalTime);
                sbQuery.Append("," + _ORTimeFollow + " = @" + _ORTimeFollow);
                sbQuery.Append("," + _ORStatCase + " = @" + _ORStatCase);
                sbQuery.Append("," + _ORCase + " = @" + _ORCase);
                sbQuery.Append("," + _ORSpecificType + " = @" + _ORSpecificType);
                sbQuery.Append("," + _ORStatus + " = @" + _ORStatus);
                sbQuery.Append("," + _AdmitTimeType + " = @" + _AdmitTimeType);
                sbQuery.Append("," + _RoomType + " = @" + _RoomType);
                sbQuery.Append("," + _ORRoom + " = @" + _ORRoom);
                sbQuery.Append("," + _AnesthesiaType1 + " = @" + _AnesthesiaType1);
                sbQuery.Append("," + _AnesthesiaType2 + " = @" + _AnesthesiaType2);
                sbQuery.Append("," + _AnesthesiaSign + " = @" + _AnesthesiaSign);
                sbQuery.Append("," + _Surgeon1 + " = @" + _Surgeon1);
                sbQuery.Append("," + _Surgeon2 + " = @" + _Surgeon2);
                sbQuery.Append("," + _Surgeon3 + " = @" + _Surgeon3);
                sbQuery.Append("," + _SurgeonMaster + " = @" + _SurgeonMaster);
                sbQuery.Append("," + _AnesthesiaDoctor1 + " = @" + _AnesthesiaDoctor1);
                sbQuery.Append("," + _AnesthesiaDoctor2 + " = @" + _AnesthesiaDoctor2);
                sbQuery.Append("," + _AnesthesiaDoctor3 + " = @" + _AnesthesiaDoctor3);
                sbQuery.Append("," + _AnesthesiaNurse1 + " = @" + _AnesthesiaNurse1);
                sbQuery.Append("," + _AnesthesiaNurse2 + " = @" + _AnesthesiaNurse2);
                sbQuery.Append("," + _AnesthesiaNurse3 + " = @" + _AnesthesiaNurse3);
                sbQuery.Append("," + _Remark + " = @" + _Remark);
                sbQuery.Append("," + _UpdateBy + " = @" + _UpdateBy);
                sbQuery.Append("," + _UpdateDate + " = GETDATE()");
                sbQuery.Append("," + _Prediag + " = @" + _Prediag);
                sbQuery.Append(",SuggestByUser = @SuggestByUser");
                sbQuery.Append(",RequestByUser = @RequestByUser");
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.HN)));
                parameter.Add(new IParameter(_PatientName, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.PatientName)));
                parameter.Add(new IParameter(_PatientInfection, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientInfection)));
                parameter.Add(new IParameter(_PatientType1, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientType1)));
                parameter.Add(new IParameter(_PatientType2, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientType2)));
                parameter.Add(new IParameter(_PatientUP, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.PatientUP)));
                parameter.Add(new IParameter(_Onmed, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.Onmed)));
                parameter.Add(new IParameter(_OnmedNote, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.OnmedNote)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_ORHEADERVO.ORDate)));
                parameter.Add(new IParameter(_ORTime, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORTime)));
                parameter.Add(new IParameter(_ArrivalTime, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ArrivalTime)));
                parameter.Add(new IParameter(_ORTimeFollow, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.ORTimeFollow)));
                parameter.Add(new IParameter(_ORStatCase, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.ORStatCase)));
                parameter.Add(new IParameter(_ORCase, IDbType.Int, DBNullConvert.From(_ORHEADERVO.ORCase, false)));
                parameter.Add(new IParameter(_ORSpecificType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORSpecificType)));
                parameter.Add(new IParameter(_ORStatus, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORStatus)));
                parameter.Add(new IParameter(_AdmitTimeType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AdmitTimeType)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.RoomType)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORRoom)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType1)));
                parameter.Add(new IParameter(_AnesthesiaType2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaType2)));
                parameter.Add(new IParameter(_AnesthesiaSign, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaSign)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Surgeon3)));
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.SurgeonMaster)));
                parameter.Add(new IParameter(_AnesthesiaDoctor1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor1)));
                parameter.Add(new IParameter(_AnesthesiaDoctor2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor2)));
                parameter.Add(new IParameter(_AnesthesiaDoctor3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaDoctor3)));
                parameter.Add(new IParameter(_AnesthesiaNurse1, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse1)));
                parameter.Add(new IParameter(_AnesthesiaNurse2, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse2)));
                parameter.Add(new IParameter(_AnesthesiaNurse3, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.AnesthesiaNurse3)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Remark)));
                //parameter.Add(new IParameter(_CreateDate, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CreateDate)));
                //parameter.Add(new IParameter(_CreateBy, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CreateBy)));
                //parameter.Add(new IParameter(_UpdateDate, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.UpdateDate)));
                parameter.Add(new IParameter(_UpdateBy, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.UpdateBy)));
                parameter.Add(new IParameter(_Prediag, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.Prediag)));
                parameter.Add(new IParameter("SuggestByUser", IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.SuggestByUser)));
                parameter.Add(new IParameter("RequestByUser", IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.RequestByUser)));
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

        internal ReturnValue CancelApp(ORHEADERVO _ORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblORHEADER + " SET ");

                sbQuery.Append("" + _CxlDateTime + " = GETDATE()");
                sbQuery.Append("," + _CxlByUser + " = @" + _CxlByUser);
                sbQuery.Append("," + _CxlReason + " = @" + _CxlReason);

                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_CxlByUser, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CxlByUser)));
                parameter.Add(new IParameter(_CxlReason, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CxlReason)));
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

        internal ReturnValue CancelPostOR(ORHEADERVO _ORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblORHEADER + " SET ");
                sbQuery.Append("" + _CxlPostOR + " = @" + _CxlPostOR);
                sbQuery.Append("," + _CxlPostORReason + " = @" + _CxlPostORReason);
                sbQuery.Append("," + _CxlPostORReasonOther + " = @" + _CxlPostORReasonOther);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_CxlPostOR, IDbType.Bit, DBNullConvert.From(_ORHEADERVO.CxlPostOR)));
                parameter.Add(new IParameter(_CxlPostORReason, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CxlPostORReason)));
                parameter.Add(new IParameter(_CxlPostORReasonOther, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.CxlPostORReasonOther)));
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

        internal ReturnValue UpdateTimeTF(ORHEADERVO _ORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblORHEADER + " SET ");

                sbQuery.Append("" + _ORTime + " = @" + _ORTime);
                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORID)));
                parameter.Add(new IParameter(_ORTime, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.ORTime)));

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
                sbQuery.Append("DELETE FROM " + _tblORHEADER);
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
    }
}
