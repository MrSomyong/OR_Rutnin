using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class VT_CLINIC
    {
        public string CLINICCODE { get; set; }
        public string CLINICNAME { get; set; }
        public string CLINIC_CODE { get; set; }
        public string CLINIC_NAME_TH { get; set; }
        public string CLINIC_NAME_ENG { get; set; }
        public string CLINIC_NAMEDESC {
            get { return string.Format("[{0}] {1}", CLINIC_CODE , string.IsNullOrEmpty(CLINIC_NAME_TH) ? CLINIC_NAME_TH : CLINIC_NAME_ENG); }
        }
        public int I_ENABLED { get; set; }
    }
}