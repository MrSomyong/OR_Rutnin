using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_PATIENT_VNVO
    {
        public string HN { get; set; }
        public string VN { get; set; }
        public Nullable<DateTime> VisitDate { get; set; }
        public Nullable<DateTime> ORDateTime { get; set; }
    }
}
