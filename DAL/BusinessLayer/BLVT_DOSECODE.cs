using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_DOSECODE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_DOSECODE() { }
        public BLVT_DOSECODE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOSECODE> SearchAll()
        {
            DAVT_DOSECODE DAVT_DOSECODE = (dbInfo == null ? new DAVT_DOSECODE() : new DAVT_DOSECODE(dbInfo));
            return DAVT_DOSECODE.SearchAll();
        }

    }
}
