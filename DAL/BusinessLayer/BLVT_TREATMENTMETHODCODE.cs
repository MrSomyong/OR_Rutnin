using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_TREATMENTMETHODCODE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_TREATMENTMETHODCODE() { }
        public BLVT_TREATMENTMETHODCODE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VT_TREATMENTMETHODCODE> GetTreatmentMethodAll()
        {
            DAVT_TREATMENTMETHODCODE DAVT_TREATMENTMETHODCODE = (dbInfo == null ? new DAVT_TREATMENTMETHODCODE() : new DAVT_TREATMENTMETHODCODE(dbInfo));
            return DAVT_TREATMENTMETHODCODE.GetTreatmentMethodAll();
        }
 
        public List<VT_TREATMENTMETHODCODE> GetTreatmentCodeByMethodCode(string methodCode)
        {
            DAVT_TREATMENTMETHODCODE DAVT_TREATMENTMETHODCODE = (dbInfo == null ? new DAVT_TREATMENTMETHODCODE() : new DAVT_TREATMENTMETHODCODE(dbInfo));
            return DAVT_TREATMENTMETHODCODE.GetTreatmentCodeByMethodCode(methodCode);
        }
        public VT_TREATMENTMETHODCODE GetTreatmentMethodByKey(string methodCode)
        {
            DAVT_TREATMENTMETHODCODE DAVT_TREATMENTMETHODCODE = (dbInfo == null ? new DAVT_TREATMENTMETHODCODE() : new DAVT_TREATMENTMETHODCODE(dbInfo));
            return DAVT_TREATMENTMETHODCODE.GetTreatmentMethodByKey(methodCode);
        }
        


    }
}
