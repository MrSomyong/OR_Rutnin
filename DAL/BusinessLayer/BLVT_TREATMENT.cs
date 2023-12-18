using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_TREATMENT
    {
        DatabaseInfo dbInfo = null;
        public BLVT_TREATMENT() { }
        public BLVT_TREATMENT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VT_TREATMENT> SearchAll()
        {
            DAVT_TREATMENT DAVT_TREATMENT = (dbInfo == null ? new DAVT_TREATMENT() : new DAVT_TREATMENT(dbInfo));
            return DAVT_TREATMENT.SearchAll();
        }
        
    }
}
