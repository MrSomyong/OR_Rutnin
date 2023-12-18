using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_TREATMENTMETHODCODE
    {

        public string MethodCode { get; set; }
        public string MethodName { get; set; }
        public int    SUFFIX { get; set; }
        public string TreatmentCode { get; set; }
        public string TreatmentName { get; set; }
        public double StdPrice1 { get; set; }

        public string TreatmentMethodName
        {
            get{
                return string.Format("[{0}] {1}", MethodCode, MethodName);
            }
        }

        public string TreatmentItemName
        {
            get
            {
                return string.Format("[{0}] {1}", TreatmentCode, TreatmentName);
            }
        }
       
    }
}
