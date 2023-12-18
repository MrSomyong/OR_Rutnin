using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_TREATMENTCODE_SPLITDF
    {
        public string TreatmentCode { get; set; }
        public string TreatmentName { get; set; }
        public string DFTreatmentCode { get; set; }
        public string DFTreatmentName { get; set; }
        public double StdPrice1 { get; set; }
       
        public string TreatmentDesc
        {
            get{
                return string.Format("[{0}] {1}", TreatmentCode, TreatmentName);
            }
        }
       
    }
}
