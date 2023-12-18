using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_PATIENT_AN
    {
        DatabaseInfo dbInfo = null;
        public BLVT_PATIENT_AN() { }
        public BLVT_PATIENT_AN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<VT_PATIENT_ANVO> SearchByKey(VT_PATIENT_ANVO VT_PATIENT_ANVO)
        {
            DAVT_PATIENT_AN DAVT_PATIENT_AN = (dbInfo == null ? new DAVT_PATIENT_AN() : new DAVT_PATIENT_AN(dbInfo));
            return DAVT_PATIENT_AN.SearchByKey(VT_PATIENT_ANVO);
        }
        public List<VT_PATIENT_ANVO> SearchAN(VT_PATIENT_ANVO VT_PATIENT_ANVO)
        {
            DAVT_PATIENT_AN DAVT_PATIENT_AN = (dbInfo == null ? new DAVT_PATIENT_AN() : new DAVT_PATIENT_AN(dbInfo));
            return DAVT_PATIENT_AN.SearchAN(VT_PATIENT_ANVO);
        }
    }
}
