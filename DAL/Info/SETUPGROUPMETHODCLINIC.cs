using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPGROUPMETHODCLINIC
    {
        public int GroupMedthodDocID { get; set; }
        public int GroupMethodID { get; set; }
        public string GroupMethodCode { get; set; }
        public string ClinicCode { get; set; }
        public string ClinicName { get; set; }
        public bool IsActive { get; set; }
      
    }
}
