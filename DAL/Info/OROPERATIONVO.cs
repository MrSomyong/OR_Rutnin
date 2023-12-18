using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class OROPERATIONVO
    {
        public string ID { get; set; }
        public string ORID { get; set; }
        public string MainCode { get; set; }
        public string SubCode { get; set; }
        public int Side { get; set; }
        public string strSide { get; set; }
        public string Reamrk { get; set; }
        public int OrderNum { get; set; }
        public string SubMark { get; set; }
        public string strSubMark { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public int Seq { get; set; }
        public int QTY { get; set; }

        public string Surgeon1 { get; set; }
        public string Surgeon2 { get; set; }
        public string Surgeon3 { get; set; }
        public string Complication { get; set; }
        public string ICD { get; set; }
        public string Organ { get; set; }
    }
}
