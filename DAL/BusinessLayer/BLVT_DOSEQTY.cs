using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_DOSEQTY
    {
        DatabaseInfo dbInfo = null;
        public BLVT_DOSEQTY() { }
        public BLVT_DOSEQTY(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOSEQTY> SearchAll()
        {
            DAVT_DOSEQTY DAVT_DOSEQTY = (dbInfo == null ? new DAVT_DOSEQTY() : new DAVT_DOSEQTY(dbInfo));
            return DAVT_DOSEQTY.SearchAll();
        }

    }
}
