using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_UNIT
    {
        DatabaseInfo dbInfo = null;
        public BLVT_UNIT() { }
        public BLVT_UNIT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<UNIT> SearchAll()
        {
            DAVT_UNIT DAVT_UNIT = (dbInfo == null ? new DAVT_UNIT() : new DAVT_UNIT(dbInfo));
            return DAVT_UNIT.SearchAll();
        }

    }
}
