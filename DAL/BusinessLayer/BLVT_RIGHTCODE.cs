using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_RIGHTCODE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_RIGHTCODE() { }
        public BLVT_RIGHTCODE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public VT_RIGHTCODE GetRightCodeByKey(string code)
        {
            DAVT_RIGHTCODE DAVT_RIGHTCODE = (dbInfo == null ? new DAVT_RIGHTCODE() : new DAVT_RIGHTCODE(dbInfo));
            return DAVT_RIGHTCODE.GetRightCodeByKey(code);
        }

    }
}
