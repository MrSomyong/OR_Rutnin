using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPATIENTALLEGIC
    {
        DatabaseInfo dbInfo = null;
        public BLPATIENTALLEGIC() { }
        public BLPATIENTALLEGIC(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<PATIENTALLEGICVO> SearchByKey(PATIENTALLEGICVO PATIENTALLEGICVO)
        {
            DAPATIENTALLEGIC DAPATIENTALLEGIC = (dbInfo == null ? new DAPATIENTALLEGIC() : new DAPATIENTALLEGIC(dbInfo));
            return DAPATIENTALLEGIC.SearchByKey(PATIENTALLEGICVO);
        }

        public List<PATIENTALLEGICVO> SearchOtherByKey(PATIENTALLEGICVO PATIENTALLEGICVO)
        {
            DAPATIENTALLEGIC DAPATIENTALLEGIC = (dbInfo == null ? new DAPATIENTALLEGIC() : new DAPATIENTALLEGIC(dbInfo));
            return DAPATIENTALLEGIC.SearchOtherByKey(PATIENTALLEGICVO);
        }
    }
}
