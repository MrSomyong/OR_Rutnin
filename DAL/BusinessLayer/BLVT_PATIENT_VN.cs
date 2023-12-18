using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_PATIENT_VN
    {
        DatabaseInfo dbInfo = null;
        public BLVT_PATIENT_VN() { }
        public BLVT_PATIENT_VN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<VT_PATIENT_VNVO> SearchByKey(VT_PATIENT_VNVO VT_PATIENT_VNVO)
        {
            DAVT_PATIENT_VN DAVT_PATIENT_VN = (dbInfo == null ? new DAVT_PATIENT_VN() : new DAVT_PATIENT_VN(dbInfo));
            return DAVT_PATIENT_VN.SearchByKey(VT_PATIENT_VNVO);
        }
        public List<VT_PATIENT_VNVO> SearchVN(VT_PATIENT_VNVO VT_PATIENT_VNVO)
        {
            DAVT_PATIENT_VN DAVT_PATIENT_VN = (dbInfo == null ? new DAVT_PATIENT_VN() : new DAVT_PATIENT_VN(dbInfo));
            return DAVT_PATIENT_VN.SearchVN(VT_PATIENT_VNVO);
        }
    }
}
