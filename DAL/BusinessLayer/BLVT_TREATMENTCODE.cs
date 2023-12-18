using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_TREATMENTCODE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_TREATMENTCODE() { }
        public BLVT_TREATMENTCODE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<VT_TREATMENTCODE> SearchAllByKey(int isDF)
        {
            DAVT_TREATMENTCODE DAVT_TREATMENTCODE = (dbInfo == null ? new DAVT_TREATMENTCODE() : new DAVT_TREATMENTCODE(dbInfo));
            return DAVT_TREATMENTCODE.SearchAllByKey(isDF);
        }

        public VT_TREATMENTCODE GetTreatmentCodeByKey(string _CODE)
        {
            int fixRate = 0;
            DAVT_TREATMENTCODE DAVT_TREATMENTCODE = (dbInfo == null ? new DAVT_TREATMENTCODE() : new DAVT_TREATMENTCODE(dbInfo));
            return DAVT_TREATMENTCODE.GetTreatmentCodeByKey(_CODE, fixRate);
        }
        public VT_TREATMENTCODE GetTreatmentCodeByKey(string _CODE, int fixRate)
        {
            DAVT_TREATMENTCODE DAVT_TREATMENTCODE = (dbInfo == null ? new DAVT_TREATMENTCODE() : new DAVT_TREATMENTCODE(dbInfo));
            return DAVT_TREATMENTCODE.GetTreatmentCodeByKey(_CODE , fixRate);
        }


        public VT_TREATMENTCODE GetTreatmentCodeByKey(string _MethodCode, string _CODE)
        {
            DAVT_TREATMENTCODE DAVT_TREATMENTCODE = (dbInfo == null ? new DAVT_TREATMENTCODE() : new DAVT_TREATMENTCODE(dbInfo));
            return DAVT_TREATMENTCODE.GetTreatmentCodeByKey(_MethodCode , _CODE);
        }

    }
}
