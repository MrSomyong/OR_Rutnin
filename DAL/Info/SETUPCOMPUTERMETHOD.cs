using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPCOMPUTERMETHOD
    {
        public int ComputerMethodID { get; set; }
        public string ComputerCode { get; set; }
        public string MethodCode { get; set; }
        public string MethodName { get; set; }
        public bool IsActive { get; set; }
        public VT_TREATMENTCODE TreatmentCodeInfo { get; set; }

    }
}
