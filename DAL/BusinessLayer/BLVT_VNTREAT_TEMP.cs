using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace DAL
{
    
    public class BLVT_VNTREAT_TEMP
    {

        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;
        public BLVT_VNTREAT_TEMP() { }
        public BLVT_VNTREAT_TEMP(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLVT_VNTREAT_TEMP(DatabaseInfo extConnDBInfo, DatabaseInfo appConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }
        public List<VT_VNTREAT_TEMP> SearchVT_VNTreatByKey(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.SearchByKey(VT_VNTREAT);
        }

        public List<VT_VNTREAT_TEMP> SearchVT_VNTreatByKey(VT_VNTREAT_TEMP VT_VNTREAT, bool isDF)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.SearchByKey(VT_VNTREAT, isDF);
        }
        public List<VT_VNTREAT_TEMP> DFTreatmentChargeAll(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.DFTreatmentChargeAll(VT_VNTREAT);
        }
        
        public List<VT_VNTREAT_TEMP> GetAllVNTreatByKey(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT,true);
        }

        public List<VT_VNTREAT_TEMP> GetAllVNTreatByKey(VT_VNTREAT_TEMP VT_VNTREAT, bool isDeleted)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT, isDeleted);
        }

        public List<VT_VNTREAT_TEMP> GetAllVNTreatByKey(VT_VNTREAT_TEMP VT_VNTREAT, bool isDeleted, bool isDF)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = new DAVT_VNTREAT_TEMP(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT, isDeleted, isDF);
        }



        public ReturnValue CXLVNTREAT(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.CXLVNTREAT(VT_VNTREAT);
        }

        public ReturnValue DELVNTREAT(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.DELVNTREAT(VT_VNTREAT);
        }

        public ReturnValue Insert(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.Insert(VT_VNTREAT);
        }
        
        public bool CheckDup(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.CheckDup(VT_VNTREAT);
        }

      
        
        public VT_VNTREAT_TEMP GetVT_VNTREATByKey(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.GetVT_VNTREATByKey(VT_VNTREAT);
        }

        public ReturnValue UpdateVNTREAT(VT_VNTREAT_TEMP VT_VNTREAT)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.UpdateVNTREAT(VT_VNTREAT);
        }

        public double GetTotalTreatmentPrice(VT_VNTREAT_TEMP VT_VNTREAT,int statusType)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.GetTotalTreatmentPrice(VT_VNTREAT, statusType);
        }

        public double GetTotalTreatmentPriceAll(VT_VNTREAT_TEMP VT_VNTREAT, int statusType)
        {
            DAVT_VNTREAT_TEMP DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT_TEMP() : new DAVT_VNTREAT_TEMP(dbInfo));
            return DAVT_VNTREAT.GetTotalTreatmentPriceAll(VT_VNTREAT, statusType);
        }


    }

}
