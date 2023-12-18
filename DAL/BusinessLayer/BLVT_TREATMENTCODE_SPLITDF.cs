using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_TREATMENTCODE_SPLITDF
    {
        DatabaseInfo dbInfo = null;
        public BLVT_TREATMENTCODE_SPLITDF() { }
        public BLVT_TREATMENTCODE_SPLITDF(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

      
        public VT_TREATMENTCODE_SPLITDF GetTreatmentCodeSplitDFByKey(string treatmentCode)
        {
            DAVT_TREATMENTCODE_SPLITDF DAVT_TREATMENTCODE_SPLITDF = (dbInfo == null ? new DAVT_TREATMENTCODE_SPLITDF() : new DAVT_TREATMENTCODE_SPLITDF(dbInfo));
            return DAVT_TREATMENTCODE_SPLITDF.GetTreatmentCodeSplitDFByKey(treatmentCode);
        }
        public VT_TREATMENTCODE_SPLITDF GetTreatmentCodeSplitDFByAllKey(string treatmentCode)
        {
            DAVT_TREATMENTCODE_SPLITDF DAVT_TREATMENTCODE_SPLITDF = (dbInfo == null ? new DAVT_TREATMENTCODE_SPLITDF() : new DAVT_TREATMENTCODE_SPLITDF(dbInfo));
            return DAVT_TREATMENTCODE_SPLITDF.GetTreatmentCodeSplitDFByAllKey(treatmentCode);
        }

    }
}
