using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORLog
    {
        DatabaseInfo dbInfo = null;
        public BLORLog() { }
        public BLORLog(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORLogVO> SearchByKey(ORLogVO ORLogVO)
        {
            DAORLog DAORLog = (dbInfo == null ? new DAORLog() : new DAORLog(dbInfo));
            return DAORLog.SearchByKey(ORLogVO);
        }
        public ReturnValue Insert(ORLogVO ORLogVO)
        {
            DAORLog DAORLog = (dbInfo == null ? new DAORLog() : new DAORLog(dbInfo));
            return DAORLog.Insert(ORLogVO);
        }
    }
}
