﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace DAL
{
    
    public class BLVT_VNTREAT
    {

        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;
        public BLVT_VNTREAT() { }
        public BLVT_VNTREAT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLVT_VNTREAT(DatabaseInfo extConnDBInfo, DatabaseInfo appConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }
        public List<VT_VNTREAT> SearchVT_VNTreatByKey(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.SearchByKey(VT_VNTREAT);
        }

        public List<VT_VNTREAT> SearchVT_VNTreatByKey(VT_VNTREAT VT_VNTREAT, bool isDF)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.SearchByKey(VT_VNTREAT, isDF);
        }
        public List<VT_VNTREAT> DFTreatmentChargeAll(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.DFTreatmentChargeAll(VT_VNTREAT);
        }
        
        public List<VT_VNTREAT> GetAllVNTreatByKey(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT,true);
        }

        public List<VT_VNTREAT> GetAllVNTreatByKey(VT_VNTREAT VT_VNTREAT, bool isDeleted)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT, isDeleted);
        }

        public List<VT_VNTREAT> GetAllVNTreatByKey(VT_VNTREAT VT_VNTREAT, bool isDeleted, bool isDF)
        {
            DAVT_VNTREAT DAVT_VNTREAT = new DAVT_VNTREAT(extConnDBInfo, appConnDBInfo);
            return DAVT_VNTREAT.GetAllVNTreatByKey(VT_VNTREAT, isDeleted, isDF);
        }



        public ReturnValue CXLVNTREAT(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.CXLVNTREAT(VT_VNTREAT);
        }

        public ReturnValue Insert(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.Insert(VT_VNTREAT);
        }
        
        public bool CheckDup(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.CheckDup(VT_VNTREAT);
        }

      
        
        public VT_VNTREAT GetVT_VNTREATByKey(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.GetVT_VNTREATByKey(VT_VNTREAT);
        }

        public ReturnValue UpdateVNTREAT(VT_VNTREAT VT_VNTREAT)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.UpdateVNTREAT(VT_VNTREAT);
        }

        public double GetTotalTreatmentPrice(VT_VNTREAT VT_VNTREAT,int statusType)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.GetTotalTreatmentPrice(VT_VNTREAT, statusType);
        }

        public double GetTotalTreatmentPriceAll(VT_VNTREAT VT_VNTREAT, int statusType)
        {
            DAVT_VNTREAT DAVT_VNTREAT = (dbInfo == null ? new DAVT_VNTREAT() : new DAVT_VNTREAT(dbInfo));
            return DAVT_VNTREAT.GetTotalTreatmentPriceAll(VT_VNTREAT, statusType);
        }


    }

}
