using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class UnderPatientVO
    {
        public Nullable<DateTime> ORDate { get; set; }
        public string ORTime { get; set; }
        public string Operation { get; set; }

    }
}
