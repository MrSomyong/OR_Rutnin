using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPOPERATIONSUBVO
    {
        public string MainCode { get; set; }
        public string Name { get; set; }
        public string SubCode { get; set; }
        public string SubName { get; set; }
        public string SubRemark { get; set; }
        public string ICDCM { get; set; }
        public string ICDCMName { get; set; }
        public Nullable<int> ORProcedureType { get; set; }
        public string ORProcedureTypeName { get; set; }
        public string ORGANMAIN { get; set; }
        public string ORGANMAINName { get; set; }
        public string ORGANSUB { get; set; }
        public string ORGANSUBName { get; set; }
    }
}
