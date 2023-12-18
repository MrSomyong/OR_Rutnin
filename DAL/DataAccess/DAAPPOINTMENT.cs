using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAAPPOINTMENT : DataAccess
    {

        private static string _VT_APPOINTMENTMASTER = "VT_APPOINTMENTMASTER";
        private static string _VT_PATIENTMASTER = "VT_PATIENTMASTER";

        private static string _AppointmentNo = "AppointmentNo";
        private static string _HN = "HN";
        private static string _PatientName = "PatientName";
        private static string _AppointmentDateTime = "AppointmentDateTime";
        private static string _ProcedureCode = "ProcedureCode";
        private static string _ProcedureName = "ProcedureName";
        private static string _Doctor = "Doctor";
        private static string _DoctorName = "DoctorName";
        private static string _RemarksMemo = "RemarksMemo";
        private static string _MakeDateTime = "MakeDateTime";
        private static string _ConfirmStatusType = "ConfirmStatusType";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAAPPOINTMENT() { }
        public DAAPPOINTMENT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAAPPOINTMENT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAAPPOINTMENT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<APPOINTMENTVO> SearchByKey(APPOINTMENTVO _APPOINTMENTVO)
        {
            List<APPOINTMENTVO> lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*");
                strQuery.Append(" from " + _VT_APPOINTMENTMASTER + " as a");
                strQuery.Append(" where 1 = 1");

                if (!string.IsNullOrEmpty(_APPOINTMENTVO.ArProcedureCode))
                {
                    strQuery.Append(" and a." + _ProcedureCode + " in (" + _APPOINTMENTVO.ArProcedureCode + ") ");
                }
                if (!string.IsNullOrEmpty(_APPOINTMENTVO.AppointmentDateTime.ToString()))
                {
                    strQuery.Append(" and CONVERT(date, a." + _AppointmentDateTime + ", 126) = CONVERT(date, @" + _AppointmentDateTime + ", 126)");
                }
                if (!string.IsNullOrEmpty(_APPOINTMENTVO.AppointmentNo))
                {
                    strQuery.Append(" and " + _AppointmentNo + " = @" + _AppointmentNo);
                }

                if (!string.IsNullOrEmpty(_APPOINTMENTVO.HN))
                {
                    strQuery.Append(" and " + _HN + " = @" + _HN);
                }
                //if (!string.IsNullOrEmpty(_APPOINTMENTVO.ConfirmStatusType.ToString()) && _APPOINTMENTVO.ConfirmStatusType == 6)
                //{
                //    strQuery.Append(" and " + _ConfirmStatusType + " <> @" + _ConfirmStatusType);
                //}
                strQuery.Append(" order by a." + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AppointmentDateTime, IDbType.DateTime, DBNullConvert.From(_APPOINTMENTVO.AppointmentDateTime)));
                parameter.Add(new IParameter(_AppointmentNo, IDbType.VarChar, DBNullConvert.From(_APPOINTMENTVO.AppointmentNo)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_APPOINTMENTVO.HN)));
                //parameter.Add(new IParameter(_ConfirmStatusType, IDbType.VarChar, DBNullConvert.From(_APPOINTMENTVO.ConfirmStatusType.ToString())));
                //parameter.Add(new IParameter(_ProcedureCode, IDbType.VarChar, DBNullConvert.From(_APPOINTMENTVO.ArProcedureCode))); 
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    APPOINTMENTVO.AppointmentNo = query[_AppointmentNo].ToString();
                    APPOINTMENTVO.HN = query[_HN].ToString();
                    APPOINTMENTVO.PatientName = query[_PatientName].ToString();
                    APPOINTMENTVO.ProcedureCode = query[_ProcedureCode].ToString();
                    APPOINTMENTVO.ProcedureName = query[_ProcedureName].ToString();
                    APPOINTMENTVO.Doctor = query[_Doctor].ToString();
                    APPOINTMENTVO.DoctorName = query[_DoctorName].ToString();
                    APPOINTMENTVO.RemarksMemo = query[_RemarksMemo].ToString();
                    APPOINTMENTVO.AppointmentDateTime = ADOUtil.GetDateFromQuery(query[_AppointmentDateTime].ToString());
                    APPOINTMENTVO.strAppointmentDateTime = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_AppointmentDateTime].ToString()).ToString()), YearType.English).ToString("dd-MM-yyyy");
                    APPOINTMENTVO.MakeDateTime = ADOUtil.GetDateFromQuery(query[_MakeDateTime].ToString());
                    APPOINTMENTVO.ConfirmStatusType = ADOUtil.GetIntFromQuery(query[_ConfirmStatusType].ToString());
                    lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lstAPPOINTMENTVO;
        }
    }
}
