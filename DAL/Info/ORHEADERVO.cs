using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORHEADERVO
    {
        public string AppointmentNo { get; set; }
        public string ORID { get; set; }
        public string HN { get; set; }
        public string VisitID { get; set; }
        public string PatientName { get; set; }
        public bool PatientInfection { get; set; }
        public bool PatientType1 { get; set; }
        public bool PatientType2 { get; set; }
        public bool PatientUP { get; set; }
        public bool Onmed { get; set; }
        public string OnmedNote { get; set; }        
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
        public string strORRoom { get; set; }
        public string ArORRoom { get; set; }
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

        public string strSurgeon1 { get; set; }
        public string strSurgeon2 { get; set; }
        public string strSurgeon3 { get; set; }

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

        //public List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO { get; set; }
        public string POSTOROPERATION { get; set; }


        public Nullable<Boolean> CxlPostOR { get; set; }
        public string CxlPostORReason { get; set; }
        public string CxlPostORReasonOther { get; set; }
        public List<POSTORICDVO> lstPOSTORICDVO { get; set; }

        public string SurgeonName { get; set; }
        public string AnesDoctorName { get; set; }        
        public Nullable<Boolean> ElectiveCase { get; set; }
        public Nullable<Boolean> UrgencyCase { get; set; }
        public string OperationTime { get; set; }
        public Nullable<DateTime> StartORDateTime { get; set; }
        public string strStartORDateTime { get; set; }
        public Nullable<DateTime> FinishORDateTime { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }

        public string AnesTime { get; set; }
        public Nullable<DateTime> StartAnesDateTime { get; set; }
        public string strStartAnesDateTime { get; set; }
        public Nullable<DateTime> FinishAnesDateTime { get; set; }
        public string StartAnesTime { get; set; }
        public string FinishAnesTime { get; set; }

        public string BlockTime { get; set; }
        public Nullable<DateTime> StartBlockDateTime { get; set; }
        public string strStartBlockDateTime { get; set; }
        public Nullable<DateTime> FinishBlockDateTime { get; set; }
        public string StartBlockTime { get; set; }
        public string FinishBlockTime { get; set; }

        public string RecoveryTime { get; set; }
        public Nullable<DateTime> StartRecoveryDateTime { get; set; }
        public string strStartRecoveryDateTime { get; set; }
        public Nullable<DateTime> FinishRecoveryDateTime { get; set; }
        public string StartRecoveryTime { get; set; }
        public string FinishRecoveryTime { get; set; }

        public string No { get; set; }

        public string ICD { get; set; }
        public string ICDName { get; set; }
        public string MainOperation { get; set; }
        public string SubOperation { get; set; }
        public double Amount { get; set; }

        public string Procedure { get; set; }
        public string Diagnosis { get; set; }
        public string IPDOPD { get; set; }

        public Nullable<DateTime> BirthDateTime { get; set; }
        public string strBirthDateTime { get; set; }
        public string Gender { get; set; }

        public Nullable<DateTime> OperationDate { get; set; }
        public string strOperationDate { get; set; }

        public string Operation { get; set; }
        public string Procedrue { get; set; }
        public string OROperationType { get; set; }
        public string Age { get; set; }

        public string Organ { get; set; }
        public string OrganName { get; set; }
        public string OrganPosition { get; set; }
        public Nullable<int> AmtSurgeon { get; set; }

        public string Prediag { get; set; }
        public string NurseName { get; set; }
        public Nullable<bool> Reoperation { get; set; }

        public string Indicator { get; set; }
        public Nullable<bool> ORWoundType2 { get; set; }
        public Nullable<bool> ORWoundType3 { get; set; }
        public Nullable<bool> ORWoundType4 { get; set; }
        public Nullable<bool> ChangOperation { get; set; }
        public Nullable<bool> HR48 { get; set; }
        public Nullable<bool> Day30 { get; set; }

        public string SuggestByUser { get; set; }
        public string RequestByUser { get; set; }
        public string RequestByUserName { get; set; }

        public string strAnesNurse { get; set; }
        public string strScrubNurse { get; set; }
        public string strCriNurse { get; set; }

        public string strSide { get; set; }
        public string Note { get; set; }
        public int iSeq { get; set; }
    }
}
