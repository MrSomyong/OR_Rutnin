using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPGROUPMETHODMEDICINE
    {
        public int GroupMedthodMedID { get; set; }
        public int GroupMethodID { get; set; }
        public string GroupMethodCode { get; set; }
        public string MedicineCode { get; set; }
        public string MedicineName_TH { get; set; }
        public string MedicineName_EN { get; set; }
        public double QTY { get; set; }
        public double AMT { get; set; }
        public double UnitPrice { get; set; }
        public string DoseQTY { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string DoseTypeCode { get; set; }
        public string DoseUnitCode { get; set; }
        public string DoseCode { get; set; }
        public bool InActive { get; set; }
        public string ChargeCode { get; set; }
        public string AUXLABEL1 { get; set; }
        public string AUXLABEL2 { get; set; }
        public string AUXLABEL3 { get; set; }
        public string Remark { get; set; }
        public bool AutoTick { get; set; }
        public string STORE { get; set; }
        public string MedicinePriceType { get; set; }
        public string MedicineName {

            get {
                return string.IsNullOrEmpty(MedicineName_TH) ? (string.IsNullOrEmpty(MedicineName_EN) ? string.Empty : MedicineName_EN) : MedicineName_TH;
            }
        }
    }
}
