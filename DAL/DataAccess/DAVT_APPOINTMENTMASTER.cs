using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAVT_APPOINTMENTMASTER : DataAccess
    {
        private static string _VT_APPOINTMENTMASTER = "VT_APPOINTMENTMASTER";
        private static string _V_DOCTOR_ORDAYWEEK = "V_DOCTOR_ORDAYWEEK";
        
        private string _HN = "HN";
        private string _PatientName = "PatientName";
        private string _AppointmentNo = "AppointmentNo";
        private string _AppointmentDateTime = "AppointmentDateTime";
        private string _Doctor = "Doctor";
        private string _DoctorName = "DoctorName";
        private string _MakeDateTime = "MakeDateTime";
        private string _ORDAYWEEK = "ORDAYWEEK";
        private string _DOCTORCODE = "DOCTORCODE";
        private string _PROCEDURECODE = "ProcedureCode";
        private string _PROCEDURENAME = "ProcedureName";
        private string _ORROOM = "ORRoom";
        private string _ORROOMNAME = "Name";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAVT_APPOINTMENTMASTER() { }
        public DAVT_APPOINTMENTMASTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_APPOINTMENTMASTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_APPOINTMENTMASTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        internal List<VT_APPOINTMENTMASTERVO> SearchByKey(VT_APPOINTMENTMASTERVO _VT_APPOINTMENTMASTERVO)
        {
            List<VT_APPOINTMENTMASTERVO> retValue = new List<VT_APPOINTMENTMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT A.HN,A.PatientName,A.AppointmentNo,A.AppointmentDateTime,A.Doctor,case when A.CHEQUECLIENTNAME is not null then A.CHEQUECLIENTNAME when A.CHEQUECLIENTNAME <> '' then A.CHEQUECLIENTNAME when A.Doctor is null then 'ไม่ระบุแพทย์' else A.DoctorName end  As DoctorName,A.MakeDateTime,A.ORDAYWEEK,B.DOCTORCODE,A.ProcedureCode,A.ProcedureName,C.ORRoom,D.Name");
                strQuery.Append(" FROM dbo.VT_APPOINTMENTMASTER A");
                strQuery.Append(" LEFT JOIN dbo.V_DOCTOR_ORDAYWEEK B");
                strQuery.Append(" ON(A.Doctor = B.DOCTORCODE AND A.ORDAYWEEK = B.ORDAYWEEK)");
                strQuery.Append(" LEFT JOIN dbo.ORHEADER C");
                strQuery.Append(" ON(A.HN = C.HN AND A.AppointmentNo = C.AppointmentNo)");
                strQuery.Append(" LEFT JOIN dbo.SETUPORROOM D");
                strQuery.Append(" ON(C.ORRoom = D.CODE)");
                strQuery.Append(" WHERE A.ConfirmStatusType <> 7");
                strQuery.Append(" AND A.AppointmentDateTime Between @datefrom AND @dateto");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("datefrom", IDbType.Date, DBNullConvert.From(_VT_APPOINTMENTMASTERVO.AppointmentDateFrom)));
                parameter.Add(new IParameter("dateto", IDbType.Date, DBNullConvert.From(_VT_APPOINTMENTMASTERVO.AppointmentDateTo)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_APPOINTMENTMASTERVO VT_APPOINTMENTMASTERVO = new VT_APPOINTMENTMASTERVO();
                    VT_APPOINTMENTMASTERVO.HN = query[_HN].ToString();
                    VT_APPOINTMENTMASTERVO.PatientName = query[_PatientName].ToString();
                    VT_APPOINTMENTMASTERVO.AppointmentNo = query[_AppointmentNo].ToString();
                    VT_APPOINTMENTMASTERVO.Doctor = query[_Doctor].ToString();
                    VT_APPOINTMENTMASTERVO.DoctorName = query[_DoctorName].ToString();
                    VT_APPOINTMENTMASTERVO.ORDAYWEEK = query[_ORDAYWEEK].ToString();
                    VT_APPOINTMENTMASTERVO.AppointmentDateTime = ADOUtil.GetDateFromQuery(query[_AppointmentDateTime].ToString());
                    VT_APPOINTMENTMASTERVO.MakeDateTime = ADOUtil.GetDateFromQuery(query[_MakeDateTime].ToString());
                    VT_APPOINTMENTMASTERVO.DOCTORCODE = query[_DOCTORCODE].ToString();
                    VT_APPOINTMENTMASTERVO.PRCEDURECODE = query[_PROCEDURECODE].ToString();
                    VT_APPOINTMENTMASTERVO.PRCEDURENAME = query[_PROCEDURENAME].ToString();
                    VT_APPOINTMENTMASTERVO.ORROOM = query[_ORROOM].ToString();
                    VT_APPOINTMENTMASTERVO.ORROOMNAME = query[_ORROOMNAME].ToString();

                    retValue.Add(VT_APPOINTMENTMASTERVO);
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
