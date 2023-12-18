using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;
namespace DAL
{
    
    public class BLTREATMENT
    {
        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;


        public BLTREATMENT() { }
        public BLTREATMENT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLTREATMENT(DatabaseInfo extConnDBInfo , DatabaseInfo appConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }
        public List<TREATMENT> GetTREATMENTByGroupMethodCode(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = new DATREATMENT(extConnDBInfo , appConnDBInfo);
            return DATREATMENT.GetTREATMENTByGroupMethodCode(TREATMENT);
        }
        public List<TREATMENT> GetAllTREATMENT(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = new DATREATMENT(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllTREATMENT(TREATMENT, true);
        }

        public List<TREATMENT> GetAllTREATMENT(TREATMENT TREATMENT,bool isDeleted)
        {
            DATREATMENT DATREATMENT = new DATREATMENT(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllTREATMENT(TREATMENT, isDeleted);
        }

        public List<TREATMENT> GetAllItemCharge(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = new DATREATMENT(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllItemCharge(TREATMENT);
        }
        public List<TREATMENT> GetAllItemChargeAll(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = new DATREATMENT(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllItemChargeAll(TREATMENT);
        }
        public double GetTotalTreatmentPrice(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = (dbInfo == null ? new DATREATMENT() : new DATREATMENT(dbInfo));
            return DATREATMENT.GetTotalTreatmentPrice(TREATMENT);
        }

        public double GetTotalTreatmentPriceAll(TREATMENT TREATMENT)
        {
            DATREATMENT DATREATMENT = (dbInfo == null ? new DATREATMENT() : new DATREATMENT(dbInfo));
            return DATREATMENT.GetTotalTreatmentPriceAll(TREATMENT);
        }


    }

}
