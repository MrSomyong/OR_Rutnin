using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPGROUPMETHODDOCTOR
    {
        public int GroupMedthodDocID { get; set; }
        public int GroupMethodID { get; set; }
        public string GroupMethodCode { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public bool IsActive { get; set; }
      
    }
}
