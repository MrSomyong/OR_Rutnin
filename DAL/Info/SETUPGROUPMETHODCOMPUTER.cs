using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPGROUPMETHODCOMPUTER
    {
        public int GroupMedthodDocID { get; set; }
        public int GroupMethodID { get; set; }
        public string GroupMethodCode { get; set; }
        public string ComputerCode { get; set; }
        public string ComputerName { get; set; }
        public bool IsActive { get; set; }
      
    }
}
