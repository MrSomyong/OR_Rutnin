using System;
using System.Collections.Generic;
using System.Text;
using DAL.Info;

namespace DAL
{
    [Serializable]
    public class VT_VNTREAT
    {
        public DateTime? VISITDATE { get; set; }
        public string VN { get; set; }
        public int SUFFIX { get; set; }
        public int SUBSUFFIX { get; set; }
        public string TREATMENTCODE { get; set; }
        public string TREATMENTNAME { get; set; }
        public string TREATMENTINFO {
            get {
                return string.Format("{0} : {1}", TREATMENTCODE, TREATMENTNAME);
            }
        }
        public string TREATMENTDOCTORDETAIL
        {
            get
            {
                return string.Format("{0}({1})", TREATMENTNAME, DOCTORNAME);
            }
        }
        public string CHARGECODE { get; set; }
        public string ActivityName { get; set; }
        public string DOCTOR { get; set; }
        public string DOCTORNAME { get; set; }
        public string CLINIC { get; set; }
        public string CLINICNAME { get; set; }
        public double AMT { get; set; }
        public double QTY { get; set; }
        public double COST { get; set; }
        public double PAIDAMT { get; set; }
        public int TYPEOFCHARGE { get; set; }
        public DateTime? MAKEDATETIME { get; set; }
        public int? TREATMENTENTRYSTYLE { get; set; }
        public string ENTRYBYUSERCODE { get; set; }
        public string ENTRYBYUSERNAME { get; set; }
        public string CXLBYUSERCODE { get; set; }
        public DateTime? CXLDATETIME { get; set; }
        public int REVERSE { get; set; }
        public string REMARKS { get; set; }
        public double Total
        {
            get
            {
                return (AMT * QTY);
            }
        }
        public string GROUPREQUESTCODE { get; set; }
        public int PAIDFLAG { get; set; }
        public double CHARGEAMT { get; set; }
        public SETUPGROUPMETHOD GroupMethodInfo { get; set; }
        public bool IsDeleted { get; set; }
        public int ZeroPrice { get; set; }
        public int? TIMETYPE { get; set; }
        public DateTime? TREATMENTDATETIMEFROM { get; set; }
        public DateTime? TREATMENTDATETIMETO { get; set; }

        public VT_TREATMENTCODE TreatmentCodeInfo { get; set; }
    }
}
