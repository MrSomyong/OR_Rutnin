using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class POSTORIMPLANTVO
    {
        public string ID { get; set; }
        public string PostOperation_ID { get; set; }
        public string MainCode { get; set; }
        public string Name { get; set; }
        public string SubCode { get; set; }
        public string SubName { get; set; }
        public int Seq { get; set; }
        public byte[] Img1 { get; set; }
        public byte[] Img2 { get; set; }
        public byte[] Img3 { get; set; }
        public byte[] Img4 { get; set; }
        public byte[] Img5 { get; set; }

        public bool delimg1 { get; set; }
        public bool delimg2 { get; set; }
        public bool delimg3 { get; set; }
        public bool delimg4 { get; set; }
        public bool delimg5 { get; set; }

        public bool Used { get; set; }
        public string Remark { get; set; }
    }
}
