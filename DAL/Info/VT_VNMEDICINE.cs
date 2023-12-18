using System;
using System.Collections.Generic;
using System.Text;
using DAL.Info;

namespace DAL
{
    [Serializable]
    public class VT_VNMEDICINE
    {
        public DateTime? VISITDATE { get; set; }
        public string VN { get; set; }
        public int SUFFIX { get; set; }
        public int SUBSUFFIX { get; set; }
        public DateTime? MAKEDATETIME { get; set; }
        public string CXLBYUSERCODE { get; set; }
        public DateTime? CXLDATETIME { get; set; }
        public string STORE { get; set; }
        public string STORENAME { get; set; }
        public string STOCKCODE { get; set; }
        //public VT_STOCK_MASTER StockInfo { get; set; }
        //public string ThaiName { get; set; }
        //public string EngName { get; set; }
        //public string MedicineName
        //{
        //    get
        //    {
        //        return string.Format("[{0}] {1}", STOCKCODE, !String.IsNullOrEmpty(StockInfo.ThaiName) ? StockInfo.ThaiName : StockInfo.EngName);
        //    }
        //}
        public string DOSETYPE { get; set; }
        public string DOSECODE { get; set; }
        public string DOSEQTYCODE { get; set; }
        public string DOSEUNITCODE { get; set; }
        public string UNITCODE { get; set; }
        public string UNITNAME { get; set; }
        public string ENTRYBYUSERCODE { get; set; }
        public string ENTRYBYUSERNAME { get; set; }
        public string CHARGECODE { get; set; }
        public string ActivityName { get; set; }
        public string DOSEMEMO { get; set; }
        public double UNITPRICE { get; set; }
        public double AMT { get; set; }
        public double PAIDAMT { get; set; }
        public double? OTHERAMT { get; set; }
        public double? PAIDOTHERAMT { get; set; }
        public double COST { get; set; }
        public double QTY { get; set; }
        public int TYPEOFCHARGE { get; set; }
        public int PRINTLABELBYQTY { get; set; }
        public int PRINTLABEL { get; set; }
        public int LABELISSUED { get; set; }
        public int REVERSE { get; set; }
        public int USEDIPDPRICE { get; set; }
        public int ALREADYPRINTTODRUGSTORE { get; set; }
        public int ALTERCHARGECODE { get; set; }
        public int DRUGINTERACTION { get; set; }
        public int QTYOFPRINTLABEL { get; set; }
        public int ENTRYSHORTAGE { get; set; }
        public string RIGHTCODE { get; set; }
        public int OUTOFRIGHTLIST { get; set; }
 

        public string MEDICINECODE { get; set; }
        public string MEDICINE_THAINAME { get; set; }
        public string MEDICINE_ENGLISHNAME { get; set; }
        public string MEDICINENAME
        {
            get
            {
                return string.Format("{0}", !String.IsNullOrEmpty(MEDICINE_THAINAME) ? MEDICINE_THAINAME : MEDICINE_ENGLISHNAME);
            }
        }
        public string MEDICINENAMEDETAIL
        {
            get
            {
                return string.Format("[{0}] {1}", MEDICINECODE, !String.IsNullOrEmpty(MEDICINE_THAINAME) ? MEDICINE_THAINAME : MEDICINE_ENGLISHNAME);
            }
        }
        public string UnitCode01 { get; set; }
        public string UnitName01 { get; set; }
        public string UnitCode02 { get; set; }
        public string UnitName02 { get; set; }

        public string TYPEOFCHARGENAME
        {
            get
            {
                string typeOfChangeName = string.Empty;
                switch (TYPEOFCHARGE)
                {
                    case 0:
                        typeOfChangeName = string.Format("None");
                        break;
                    case 1:
                        typeOfChangeName = string.Format("Free");
                        break;
                    case 2:
                        typeOfChangeName = string.Format("Refund");
                        break;
                    default:
                        typeOfChangeName = string.Format("None");
                        break;
                }
                return typeOfChangeName;
            }
        }

        public string REMARK { get; set; }
        public string GROUPCODE { get; set; }
        //public string UnitName 
        //{
        //    get
        //    {
        //        return string.Format("{0}",!String.IsNullOrEmpty(UnitName01) ? UnitName01 : !String.IsNullOrEmpty(UnitName02) ? UnitName02 : string.Format("-") );
        //    }
        //}

        public int PAIDFLAG { get; set; }


        public DOSETYPE DOSETYPEINFO { get; set; }
        public DOSECODE DOSECODEINFO { get; set; }
        public DOSEQTY  DOSEQTYINFO { get; set; }
        public DOSEUNIT DOSEUNITINFO { get; set; }
        public ACTIVITYCODE ACTIVITYCODEINFO { get; set; }

        public string AUXLABEL1 { get; set; }
        public string AUXLABEL2 { get; set; }
        public string AUXLABEL3 { get; set; }
        public DOSEAUX AUXLABEL1INFO { get; set; }
        public DOSEAUX AUXLABEL2INFO { get; set; }
        public DOSEAUX AUXLABEL3INFO { get; set; }
    }
}
