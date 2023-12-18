using System;
using System.Collections.Generic;
using System.Text;
namespace DAL.Info
{
    [Serializable]
    public class TREATMENT
    {
        public DateTime? VISITDATE { get; set; }
        public string VN { get; set; }
        public int SUFFIX { get; set; }
        public int SUBSUFFIX { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string ITEMDETAIL
        {
            get
            {
                return string.Format("{0} : {1}", ITEMCODE, ITEMNAME);
            }
        }
        public double UNITPRICE { get; set; }
        public double AMT { get; set; }
        public double QTY { get; set; }
        public string CHARGECODE { get; set; }
        public string ActivityName { get; set; }
        public string GroupType { get; set; }
        public string GroupMethodCode { get; set; }
        public SETUPGROUPMETHOD GroupMethodInfo { get; set; }
        public double Total
        {
            get
            {
                return (AMT);
            }
        }

        public bool IsDeleted { get; set; }
        //public string ActivityName { get; set; }


    }
}
