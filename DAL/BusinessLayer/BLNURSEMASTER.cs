using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLNURSEMASTER
    {
        DatabaseInfo dbInfo = null;
        public BLNURSEMASTER() { }
        public BLNURSEMASTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<NURSEMASTERVO> SearchAll()
        {
            DANURSEMASTER DANURSEMASTER = (dbInfo == null ? new DANURSEMASTER() : new DANURSEMASTER(dbInfo));
            return DANURSEMASTER.SearchAll();
        }

        public NURSEMASTERVO SearchByCode(string Code)
        {
            DANURSEMASTER DANURSEMASTER = (dbInfo == null ? new DANURSEMASTER() : new DANURSEMASTER(dbInfo));
            return DANURSEMASTER.SearchByCode(Code);
        }

        public List<NURSEMASTERVO> SearchByKey(NURSEMASTERVO NURSEMASTERVO)
        {
            DANURSEMASTER DANURSEMASTER = (dbInfo == null ? new DANURSEMASTER() : new DANURSEMASTER(dbInfo));
            return DANURSEMASTER.SearchByKey(NURSEMASTERVO);
        }

        public DataTable SearchByCode_DS(string Code)
        {
            DANURSEMASTER DANURSEMASTER = (dbInfo == null ? new DANURSEMASTER() : new DANURSEMASTER(dbInfo));
            return DANURSEMASTER.SearchByCode_DS(Code);
        }

    }
}
