using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class APPOINTMENTVO
    {
        public string HN { get; set; }
        public string PatientName { get; set; }
        public string AppointmentNo { get; set; }
        public Nullable<DateTime> AppointmentDateTime { get; set; }
        public string strAppointmentDateTime { get; set; }
        public string ProcedureCode { get; set; }
        public string ArProcedureCode { get; set; }
        public string ProcedureName { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public string RemarksMemo { get; set; }
        public Nullable<DateTime> MakeDateTime { get; set; }
        public int ConfirmStatusType { get; set; }
    }
}
