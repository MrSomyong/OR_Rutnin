using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_APPOINTMENTMASTERVO
    {
        public string HN { get; set; }
        public string PatientName { get; set; }
        public string AppointmentNo { get; set; }
        public Nullable<DateTime> AppointmentDateTime { get; set; }
        public Nullable<DateTime> AppointmentDateFrom { get; set; }
        public Nullable<DateTime> AppointmentDateTo { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public Nullable<DateTime> MakeDateTime { get; set; }
        public string ORDAYWEEK { get; set; }
        public string DOCTORCODE { get; set; }
        public string PRCEDURECODE { get; set; }
        public string PRCEDURENAME { get; set; }
        public string ORROOM { get; set; }
        public string ORROOMNAME { get; set; }
    }
}