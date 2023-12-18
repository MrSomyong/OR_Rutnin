using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace DAL
{
    
    public class BLVT_VNMEDICINE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_VNMEDICINE() { }
        public BLVT_VNMEDICINE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VT_VNMEDICINE> SearchByKey(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.SearchByKey(VT_VNMEDICINE);
        }


        public VT_VNMEDICINE GetVT_VNMEDICINEByKey(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.GetVT_VNMEDICINEByKey(VT_VNMEDICINE);
        }


        public ReturnValue CXLVNMEDICINE(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.CXLVNMEDICINE(VT_VNMEDICINE);
        }

        public bool CheckDup(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.CheckDup(VT_VNMEDICINE);
        }

        public ReturnValue Insert(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.Insert(VT_VNMEDICINE);
        }

        public ReturnValue Update(VT_VNMEDICINE VT_VNMEDICINE)
        {
            DAVT_VNMEDICINE DAVT_VNMEDICINE = (dbInfo == null ? new DAVT_VNMEDICINE() : new DAVT_VNMEDICINE(dbInfo));
            return DAVT_VNMEDICINE.UpdateVNMEDICINE(VT_VNMEDICINE);
        }


        //public List<VT_VNTREAT> SearchVT_VNTreatByKey(VT_VNTREAT VT_VNTREAT,bool isDF)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.SearchByKey(VT_VNTREAT , isDF);
        //}

        //public ReturnValue CXLVNTREAT(VT_VNTREAT VT_VNTREAT)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.CXLVNTREAT(VT_VNTREAT);
        //}

        //public ReturnValue Insert(VT_VNTREAT VT_VNTREAT)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.Insert(VT_VNTREAT);
        //}

        //public bool CheckDup(VT_VNTREAT VT_VNTREAT)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.CheckDup(VT_VNTREAT);
        //}



        //public VT_VNTREAT GetVT_VNTREATByKey(VT_VNTREAT VT_VNTREAT)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.GetVT_VNTREATByKey(VT_VNTREAT);
        //}

        //public ReturnValue UpdateVNTREAT(VT_VNTREAT VT_VNTREAT)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.UpdateVNTREAT(VT_VNTREAT);
        //}

        //public double GetTotalTreatmentPrice(VT_VNTREAT VT_VNTREAT,int statusType)
        //{
        //    DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
        //    return DAVT_VNTREAT.GetTotalTreatmentPrice(VT_VNTREAT, statusType);
        //}


    }

}
