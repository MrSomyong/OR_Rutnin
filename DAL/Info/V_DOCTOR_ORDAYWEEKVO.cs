using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class V_DOCTOR_ORDAYWEEKVO
    {
        public string ORDAYWEEK { get; set; }
        public string DOCTORCODE { get; set; }
        public string DOCTOR { get; set; }
        public string DoctorName { get; set; }
        public string EDUCATIONSTANDARD { get; set; }
        public string CHEQUECLIENTNAME { get; set; }
    }
}