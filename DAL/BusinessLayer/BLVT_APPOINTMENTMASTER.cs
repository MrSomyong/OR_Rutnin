using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLVT_APPOINTMENTMASTER
    {
        DatabaseInfo dbInfo = null;
        public BLVT_APPOINTMENTMASTER() { }
        public BLVT_APPOINTMENTMASTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VT_APPOINTMENTMASTERVO> SearchByKey(VT_APPOINTMENTMASTERVO v)
        {
            DAVT_APPOINTMENTMASTER da = (dbInfo == null ? new DAVT_APPOINTMENTMASTER() : new DAVT_APPOINTMENTMASTER(dbInfo));
            return da.SearchByKey(v);
        }
    }
}
