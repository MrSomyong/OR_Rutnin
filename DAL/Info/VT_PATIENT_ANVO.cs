using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_PATIENT_ANVO
    {
        public string HN { get; set; }
        public string AN { get; set; }
        public Nullable<DateTime> ORDateTime { get; set; }
        public Nullable<DateTime> AdmitDateTime { get; set; }
        public Nullable<DateTime> DischargeDateTime { get; set; }
    }
}
