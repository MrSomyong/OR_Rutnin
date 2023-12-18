using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORHEADER : DataAccess
    {
        private static string _tblPOSTORHEADER = "POSTORHEADER";
        private static string _tblORPATIENT = "ORPATIENT";
        private static string _tblOROPERATION = "OROPERATION";
        private static string _tblSETUPOPERATIONMAIN = "SETUPOPERATIONMAIN";
        private static string _tblSETUPOPERATIONSUB = "SETUPOPERATIONSUB";

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

        //Cancel
        private static string _CxlDateTime = "CxlDateTime";
        private static string _CxlByUser = "CxlByUser";
        private static string _CxlReason = "CxlReason";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORHEADER() { }
        public DAPOSTORHEADER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORHEADER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORHEADER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORHEADERVO> SearchByKey(POSTORHEADERVO _POSTORHEADERVO)
        {
            List<POSTORHEADERVO> retValue = new List<POSTORHEADERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select ");
                strQuery.Append(" b.*,");
                strQuery.Append(" c." + _BirthDateTime + ",c." + _Sex + ",c." + _Ref + ",c." + _Nationality);
                strQuery.Append(", c." + _Initial + ", c." + _FirstName + ", c." + _LastName);
                strQuery.Append(",d." + _DoctorName + " as " + _SurgeonMasterName);
                strQuery.Append(" from " + _tblPOSTORHEADER + " as b ");
                strQuery.Append(" left join " + _VT_PATIENTMASTER + " as c on b." + _HN + " = c." + _HN);
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as d on b." + _SurgeonMaster + " = d." + _DOCTOR);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_POSTORHEADERVO.ORRoom))
                {
                    strQuery.Append(" and b. " + _ORRoom + " = @" + _ORRoom);
                }
                if (_POSTORHEADERVO.ORDate != null)
                {
                    strQuery.Append(" and CONVERT(date, b." + _ORDate + ", 126) = CONVERT(date, @" + _ORDate + ", 126) ");
                }
                if (!string.IsNullOrEmpty(_POSTORHEADERVO.HN))
                {
                    strQuery.Append(" and b. " + _HN + " like @" + _HN);
                }
                if (!string.IsNullOrEmpty(_POSTORHEADERVO.ORID))
                {
                    strQuery.Append(" and b. " + _ORID + " = @" + _ORID);
                }
                //strQuery.Append(" and b. "+ _ORStatus + " in (1,2) ");

                strQuery.Append(" order by b." + _ORTime + ",b." + _ORTimeFollow + ", " + _SurgeonMasterName + ",b." + _ORCase);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORRoom)));
                if (_POSTORHEADERVO.ORDate != null)
                {
                    parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_POSTORHEADERVO.ORDate)));
                }
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From("%" + _POSTORHEADERVO.HN + "%")));
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORID)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 300;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORHEADERVO POSTORHEADERVO = new POSTORHEADERVO();
                    POSTORHEADERVO.ORID = query[_ORID].ToString();
                    POSTORHEADERVO.HN = query[_HN].ToString();
                    POSTORHEADERVO.PatientName = query[_Initial].ToString() + " " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    POSTORHEADERVO.PatientInfection = query[_PatientInfection].ToString() == "True" ? true : false;
                    POSTORHEADERVO.PatientType1 = query[_PatientType1].ToString() == "True" ? true : false;
                    POSTORHEADERVO.PatientType2 = query[_PatientType2].ToString() == "True" ? true : false;
                    POSTORHEADERVO.PatientUP = query[_PatientUP].ToString() == "True" ? true : false;
                    string IPPU = string.Empty;
                    if (POSTORHEADERVO.PatientInfection)
                        IPPU += " (Infection)";
                    if (POSTORHEADERVO.PatientType1)
                        IPPU += " (**)";
                    if (POSTORHEADERVO.PatientType2)
                        IPPU += " (***)";
                    if (POSTORHEADERVO.PatientUP)
                        IPPU += " (UP)";
                    POSTORHEADERVO.PatientName_IPPU = POSTORHEADERVO.PatientName + IPPU;
                    POSTORHEADERVO.ORDate = DateTime.Parse(ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).ToString());
                    POSTORHEADERVO.strORDate = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).ToString()), YearType.Thai).ToString("dd MMM yyyy");
                    POSTORHEADERVO.ORTimeFollow = query[_ORTimeFollow].ToString() == "True" ? true : false;
                    if (POSTORHEADERVO.ORTimeFollow)
                    {
                        POSTORHEADERVO.ORTime = "TF";
                    }
                    else
                    {
                        POSTORHEADERVO.ORTime = query[_ORTime].ToString().Substring(0, 5);
                    }
                    if (query[_ArrivalTime].ToString().Length > 0)
                        POSTORHEADERVO.ArrivalTime = query[_ArrivalTime].ToString().Substring(0, 5);
                    POSTORHEADERVO.ORStatCase = query[_ORStatCase].ToString() == "True" ? true : false;
                    POSTORHEADERVO.ORCase = ADOUtil.GetIntFromQuery(query[_ORCase].ToString());
                    POSTORHEADERVO.ORSpecificType = query[_ORSpecificType].ToString();
                    if (string.IsNullOrEmpty(POSTORHEADERVO.ORSpecificType))
                        POSTORHEADERVO.ORSpecificType = "0";
                    POSTORHEADERVO.NSR = ((EnumOR.ORSpecificType)int.Parse(POSTORHEADERVO.ORSpecificType)).ToString().Substring(0, 1).ToUpper();
                    //'ORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    POSTORHEADERVO.ORStatus = query[_ORStatus].ToString();
                    POSTORHEADERVO.AdmitTimeType = query[_AdmitTimeType].ToString();
                    POSTORHEADERVO.RoomType = query[_RoomType].ToString();
                    POSTORHEADERVO.ORRoom = query[_ORRoom].ToString();
                    POSTORHEADERVO.AnesthesiaType1 = query[_AnesthesiaType1].ToString();
                    POSTORHEADERVO.AnesthesiaType2 = query[_AnesthesiaType2].ToString();
                    POSTORHEADERVO.AnesthesiaSign = query[_AnesthesiaSign].ToString();
                    POSTORHEADERVO.Surgeon1 = query[_Surgeon1].ToString();
                    POSTORHEADERVO.Surgeon2 = query[_Surgeon2].ToString();
                    POSTORHEADERVO.Surgeon3 = query[_Surgeon3].ToString();
                    POSTORHEADERVO.SurgeonMaster = query[_SurgeonMaster].ToString();
                    POSTORHEADERVO.AnesthesiaDoctor1 = query[_AnesthesiaDoctor1].ToString();
                    POSTORHEADERVO.AnesthesiaDoctor2 = query[_AnesthesiaDoctor2].ToString();
                    POSTORHEADERVO.AnesthesiaDoctor3 = query[_AnesthesiaDoctor3].ToString();
                    POSTORHEADERVO.AnesthesiaNurse1 = query[_AnesthesiaNurse1].ToString();
                    POSTORHEADERVO.AnesthesiaNurse2 = query[_AnesthesiaNurse2].ToString();
                    POSTORHEADERVO.AnesthesiaNurse3 = query[_AnesthesiaNurse3].ToString();

                    POSTORHEADERVO.Remark = query[_Remark].ToString();
                    POSTORHEADERVO.AppointmentNo = query[_AppointmentNo].ToString();

                    ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
                    ORPATIENTVO.PatientName = POSTORHEADERVO.PatientName;
                    ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    ORPATIENTVO.Sex = query[_Sex].ToString();
                    ORPATIENTVO.Ref = query[_Ref].ToString();
                    ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    ORPATIENTVO.Initial = query[_Initial].ToString();
                    POSTORHEADERVO.ORPATIENTVO = ORPATIENTVO;
                    POSTORHEADERVO.CreateBy = query[_CreateBy].ToString();
                    POSTORHEADERVO.CreateDate = ADOUtil.GetDateFromQuery(query[_CreateDate].ToString());
                    POSTORHEADERVO.strCreateDate = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_CreateDate].ToString()).ToString()), YearType.Thai).ToString("dd MMM yyyy");
                    POSTORHEADERVO.UpdateBy = query[_UpdateBy].ToString();
                    POSTORHEADERVO.UpdateDate = ADOUtil.GetDateFromQuery(query[_UpdateDate].ToString());
                    POSTORHEADERVO.CxlByUser = query[_CxlByUser].ToString();
                    POSTORHEADERVO.CxlDateTime = ADOUtil.GetDateFromQuery(query[_CxlDateTime].ToString());
                    POSTORHEADERVO.CxlReason = query[_CxlReason].ToString();

                    retValue.Add(POSTORHEADERVO);
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

        internal bool Checkdup(string ORID)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblPOSTORHEADER);
                strQuery.Append(" where " + _ORID + " = @" + _ORID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(ORID)));
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

        internal ReturnValue Insert(POSTORHEADERVO _POSTORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORHEADER + " (");
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
                sbValue.Append(", GETDATE()");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.HN)));
                parameter.Add(new IParameter(_PatientName, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.PatientName)));
                parameter.Add(new IParameter(_PatientInfection, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientInfection)));
                parameter.Add(new IParameter(_PatientType1, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientType1)));
                parameter.Add(new IParameter(_PatientType2, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientType2)));
                parameter.Add(new IParameter(_PatientUP, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientUP)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_POSTORHEADERVO.ORDate)));
                parameter.Add(new IParameter(_ORTime, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORTime)));
                parameter.Add(new IParameter(_ArrivalTime, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ArrivalTime)));
                parameter.Add(new IParameter(_ORTimeFollow, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.ORTimeFollow)));
                parameter.Add(new IParameter(_ORStatCase, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.ORStatCase)));
                parameter.Add(new IParameter(_ORCase, IDbType.Int, DBNullConvert.From(_POSTORHEADERVO.ORCase, false)));
                parameter.Add(new IParameter(_ORSpecificType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORSpecificType)));
                parameter.Add(new IParameter(_ORStatus, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORStatus)));
                parameter.Add(new IParameter(_AdmitTimeType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AdmitTimeType)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.RoomType)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORRoom)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaType1)));
                parameter.Add(new IParameter(_AnesthesiaType2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaType2)));
                parameter.Add(new IParameter(_AnesthesiaSign, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaSign)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon3)));
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.SurgeonMaster)));
                parameter.Add(new IParameter(_AnesthesiaDoctor1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor1)));
                parameter.Add(new IParameter(_AnesthesiaDoctor2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor2)));
                parameter.Add(new IParameter(_AnesthesiaDoctor3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor3)));
                parameter.Add(new IParameter(_AnesthesiaNurse1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse1)));
                parameter.Add(new IParameter(_AnesthesiaNurse2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse2)));
                parameter.Add(new IParameter(_AnesthesiaNurse3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse3)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Remark)));
                parameter.Add(new IParameter(_AppointmentNo, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AppointmentNo)));
                parameter.Add(new IParameter(_CreateBy, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.CreateBy)));
                //parameter.Add(new IParameter(_CreateDate, IDbType.DateTime, DBNullConvert.From(_POSTORHEADERVO.CreateDate)));
                //parameter.Add(new IParameter(_UpdateBy, IDbType.VarChar, DBNullConvert.From(_ORHEADERVO.UpdateBy)));

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

        internal ReturnValue Update(POSTORHEADERVO _POSTORHEADERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORHEADER + " SET ");

                sbQuery.Append("" + _HN + " = @" + _HN);
                sbQuery.Append("," + _PatientName + " = @" + _PatientName);
                sbQuery.Append("," + _PatientInfection + " = @" + _PatientInfection);
                sbQuery.Append("," + _PatientType1 + " = @" + _PatientType1);
                sbQuery.Append("," + _PatientType2 + " = @" + _PatientType2);
                sbQuery.Append("," + _PatientUP + " = @" + _PatientUP);
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

                sbQuery.Append(" WHERE " + _ORID + " = @" + _ORID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ORID, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.HN)));
                parameter.Add(new IParameter(_PatientName, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.PatientName)));
                parameter.Add(new IParameter(_PatientInfection, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientInfection)));
                parameter.Add(new IParameter(_PatientType1, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientType1)));
                parameter.Add(new IParameter(_PatientType2, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientType2)));
                parameter.Add(new IParameter(_PatientUP, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.PatientUP)));
                parameter.Add(new IParameter(_ORDate, IDbType.Date, DBNullConvert.From(_POSTORHEADERVO.ORDate)));
                parameter.Add(new IParameter(_ORTime, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORTime)));
                parameter.Add(new IParameter(_ArrivalTime, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ArrivalTime)));
                parameter.Add(new IParameter(_ORTimeFollow, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.ORTimeFollow)));
                parameter.Add(new IParameter(_ORStatCase, IDbType.Bit, DBNullConvert.From(_POSTORHEADERVO.ORStatCase)));
                parameter.Add(new IParameter(_ORCase, IDbType.Int, DBNullConvert.From(_POSTORHEADERVO.ORCase, false)));
                parameter.Add(new IParameter(_ORSpecificType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORSpecificType)));
                parameter.Add(new IParameter(_ORStatus, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORStatus)));
                parameter.Add(new IParameter(_AdmitTimeType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AdmitTimeType)));
                parameter.Add(new IParameter(_RoomType, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.RoomType)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.ORRoom)));
                parameter.Add(new IParameter(_AnesthesiaType1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaType1)));
                parameter.Add(new IParameter(_AnesthesiaType2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaType2)));
                parameter.Add(new IParameter(_AnesthesiaSign, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaSign)));
                parameter.Add(new IParameter(_Surgeon1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon1)));
                parameter.Add(new IParameter(_Surgeon2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon2)));
                parameter.Add(new IParameter(_Surgeon3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Surgeon3)));
                parameter.Add(new IParameter(_SurgeonMaster, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.SurgeonMaster)));
                parameter.Add(new IParameter(_AnesthesiaDoctor1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor1)));
                parameter.Add(new IParameter(_AnesthesiaDoctor2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor2)));
                parameter.Add(new IParameter(_AnesthesiaDoctor3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaDoctor3)));
                parameter.Add(new IParameter(_AnesthesiaNurse1, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse1)));
                parameter.Add(new IParameter(_AnesthesiaNurse2, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse2)));
                parameter.Add(new IParameter(_AnesthesiaNurse3, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.AnesthesiaNurse3)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.Remark)));
                //parameter.Add(new IParameter(_UpdateDate, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.UpdateDate)));
                parameter.Add(new IParameter(_UpdateBy, IDbType.VarChar, DBNullConvert.From(_POSTORHEADERVO.UpdateBy)));

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
                sbQuery.Append("DELETE FROM " + _tblPOSTORHEADER);
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
