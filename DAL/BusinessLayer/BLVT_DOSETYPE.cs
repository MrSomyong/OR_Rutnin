using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_DOSETYPE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_DOSETYPE() { }
        public BLVT_DOSETYPE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOSETYPE> SearchAll()
        {
            DAVT_DOSETYPE DAVT_DOSETYPE = (dbInfo == null ? new DAVT_DOSETYPE() : new DAVT_DOSETYPE(dbInfo));
            return DAVT_DOSETYPE.SearchAll();
        }

    }
}
