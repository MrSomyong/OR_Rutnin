using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class POSTORHEADERVO
    {
        public string AppointmentNo { get; set; }
        public string ORID { get; set; }
        public string HN { get; set; }
        public string PatientName { get; set; }
        public bool PatientInfection { get; set; }
        public bool PatientType1 { get; set; }
        public bool PatientType2 { get; set; }
        public bool PatientUP { get; set; }
        public string PatientName_IPPU { get; set; }
        public Nullable<DateTime> ORDate { get; set; }
        public Nullable<DateTime> ORDateFrom { get; set; }
        public Nullable<DateTime> ORDateTo { get; set; }
        public string strORDate { get; set; }
        public string ORTime { get; set; }
        public string ArrivalTime { get; set; }
        public bool ORTimeFollow { get; set; }
        public bool ORStatCase { get; set; }
        public int ORCase { get; set; }
        public string strORCase { get; set; }
        public string ORSpecificType { get; set; }
        public string NSR { get; set; }
        public string ORStatus { get; set; }
        public string strORStatus { get; set; }
        public string AdmitTimeType { get; set; }
        public string RoomType { get; set; }
        public string ORRoom { get; set; }
        public string AnesthesiaType { get; set; }
        public string AnesthesiaType1 { get; set; }
        public string AnesthesiaType2 { get; set; }
        public string AnesthesiaSign { get; set; }
        public string AnesthesiaTypeName { get; set; }
        public int AnesthesiaTypeQTY { get; set; }
        public string Surgeon { get; set; }
        public string Surgeon1 { get; set; }
        public string Surgeon2 { get; set; }
        public string Surgeon3 { get; set; }
        public string SurgeonMaster { get; set; }
        public string AnesthesiaDoctor1 { get; set; }
        public string AnesthesiaDoctor2 { get; set; }
        public string AnesthesiaDoctor3 { get; set; }
        public string AnesthesiaNurse1 { get; set; }
        public string AnesthesiaNurse2 { get; set; }
        public string AnesthesiaNurse3 { get; set; }
        public string Remark { get; set; }

        public Nullable<DateTime> CreateDate { get; set; }
        public string strCreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public ORPATIENTVO ORPATIENTVO { get; set; }
        public OROPERATIONVO OROPERATIONVO { get; set; }

        public string RoomTypeName { get; set; }
        public string morning { get; set; }
        public string evening { get; set; }
        public Nullable<DateTime> CxlDateTime { get; set; }
        public string CxlByUser { get; set; }
        public string CxlReason { get; set; }
        public int QTY { get; set; }

    }
}
