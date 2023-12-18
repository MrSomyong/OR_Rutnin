using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLDatabaseInfo
    {
        DatabaseInfo dbInfo = null;
        public BLDatabaseInfo() { }
        public BLDatabaseInfo(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public ReturnValue TestConnection()
        {
            DADatabaseInfo dadbInfo = (dbInfo == null ? new DADatabaseInfo() : new DADatabaseInfo(dbInfo));
            return dadbInfo.ConnectDatabase();
        }
    }
}