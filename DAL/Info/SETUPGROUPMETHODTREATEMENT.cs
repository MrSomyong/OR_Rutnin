using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPGROUPMETHODTREATMENT
    {
        public int GroupMethodID { get; set; }
        public string GroupMethodCode { get; set; }
        public string TreatmentCode { get; set; }
        public string TreatmentName { get; set; }
        public bool InActive { get; set; }
        public string CHARGECODE { get; set; }
        public double AMT { get; set; }
        public double QTY { get; set; }
        public int TREATMENTENTRYSTYLE { get; set; }
        public string REMARKS { get; set; }
        public bool AutoTick { get; set; }
    }
}
