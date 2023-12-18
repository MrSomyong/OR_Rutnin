using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;
namespace DAL
{
    
    public class BLTREATMENT_TEMP
    {
        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;


        public BLTREATMENT_TEMP() { }
        public BLTREATMENT_TEMP(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLTREATMENT_TEMP(DatabaseInfo extConnDBInfo , DatabaseInfo appConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }
        public List<TREATMENT> GetTREATMENTByGroupMethodCode(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = new DATREATMENT_TEMP(extConnDBInfo , appConnDBInfo);
            return DATREATMENT.GetTREATMENTByGroupMethodCode(TREATMENT);
        }
        public List<TREATMENT> GetAllTREATMENT(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = new DATREATMENT_TEMP(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllTREATMENT(TREATMENT, true);
        }

        public List<TREATMENT> GetAllTREATMENT(TREATMENT TREATMENT,bool isDeleted)
        {
            DATREATMENT_TEMP DATREATMENT = new DATREATMENT_TEMP(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllTREATMENT(TREATMENT, isDeleted);
        }

        public List<TREATMENT> GetAllItemCharge(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = new DATREATMENT_TEMP(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllItemCharge(TREATMENT);
        }
        public List<TREATMENT> GetAllItemChargeAll(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = new DATREATMENT_TEMP(extConnDBInfo, appConnDBInfo);
            return DATREATMENT.GetAllItemChargeAll(TREATMENT);
        }
        public double GetTotalTreatmentPrice(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = (dbInfo == null ? new DATREATMENT_TEMP() : new DATREATMENT_TEMP(dbInfo));
            return DATREATMENT.GetTotalTreatmentPrice(TREATMENT);
        }

        public double GetTotalTreatmentPriceAll(TREATMENT TREATMENT)
        {
            DATREATMENT_TEMP DATREATMENT = (dbInfo == null ? new DATREATMENT_TEMP() : new DATREATMENT_TEMP(dbInfo));
            return DATREATMENT.GetTotalTreatmentPriceAll(TREATMENT);
        }


    }

}
