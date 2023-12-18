using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_DOSEUNIT
    {
        DatabaseInfo dbInfo = null;
        public BLVT_DOSEUNIT() { }
        public BLVT_DOSEUNIT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOSEUNIT> SearchAll()
        {
            DAVT_DOSEUNIT DAVT_DOSEUNIT = (dbInfo == null ? new DAVT_DOSEUNIT() : new DAVT_DOSEUNIT(dbInfo));
            return DAVT_DOSEUNIT.SearchAll();
        }

    }
}
