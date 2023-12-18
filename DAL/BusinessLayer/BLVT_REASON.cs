using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_REASON
    {
        DatabaseInfo dbInfo = null;
        public BLVT_REASON() { }
        public BLVT_REASON(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<VT_REASONVO> SearchByKey(VT_REASONVO VT_REASONVO)
        {
            DAVT_REASON DAVT_REASON = (dbInfo == null ? new DAVT_REASON() : new DAVT_REASON(dbInfo));
            return DAVT_REASON.SearchByKey(VT_REASONVO);
        }        
    }
}
