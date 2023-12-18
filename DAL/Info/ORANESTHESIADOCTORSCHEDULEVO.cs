using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORANESTHESIADOCTORSCHEDULEVO
    {
        public string ID { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public Nullable<DateTime> StartAnesthesiaDateTime { get; set; }
        public string Reamrk { get; set; }
        public string strStartAnesthesiaDateTime { get; set; }
    }
}
