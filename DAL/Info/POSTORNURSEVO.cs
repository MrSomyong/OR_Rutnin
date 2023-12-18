using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class POSTORNURSEVO
    {
        public string ORID { get; set; }
        public int Suffix { get; set; }
        public int SuffixNext { get; set; }
        public int NurseType { get; set; }
        public string NurseTypeDesc { get; set; }
        public string Nurse { get; set; }
        public string NurseCode { get; set; }
        public string Remark { get; set; }

    }
}
