using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class PATIENTDIAGVO
    {
        public string HN { get; set; }
        public string icdname { get; set; }
        public string diagname { get; set; }
        public string Remark { get; set; }

    }
}
