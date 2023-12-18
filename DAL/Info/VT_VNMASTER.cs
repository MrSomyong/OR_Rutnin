using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_VNMASTER
    {
        public DateTime? VISITDATE { get; set; }
        public string VN { get; set; }
        public string HN { get; set; }
        public string Initial { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return Initial + " " + FirstName + " " + LastName; }
        }
        public DateTime? VISITINDATETIME { get; set; }
        public DateTime? VISITOUTDATETIME { get; set; }
        public int SUFFIX { get; set; }
        public string DOCTOR { get; set; }
        public string DoctorName { get; set; }
        public string CLINIC { get; set; }
        public string CLINICNAME { get; set; }
        public string CLOSEVISITTYPE { get; set; }
        
        public string RIGHTCODE { get; set; }
        public string RIGHTNAME { get; set; }
        public bool? OutFlag { get; set; }
        public bool? CloseVisitFlag { get; set; }
        public bool HoldBill { get; set; }
        public string HoldBillDesc { get { return HoldBill == true ? "Hold bill" : ""; } }
        public string TreatmentPriceType { get; set; }
        public string MedicinePriceType { get; set; }
        public string Close { get; set; }
        //{
        //    get { return CloseVisitFlag.HasValue ? "Y" : "N"; }
        //    //get { return CloseVisitFlag.HasValue || CloseVisitFlag == true ? "Y" : "N"; }
        //    //get { return CLOSEVISITTYPE != null ? "Y" : "N"; }
        //}

    }
}
