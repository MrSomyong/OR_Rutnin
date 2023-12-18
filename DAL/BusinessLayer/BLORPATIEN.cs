using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORPATIENT
    {
        DatabaseInfo dbInfo = null;
        public BLORPATIENT() { }
        public BLORPATIENT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORPATIENTVO> SearchByKey(string strsearch)
        {
            DAORPATIENT DAORPATIENT = (dbInfo == null ? new DAORPATIENT() : new DAORPATIENT(dbInfo));
            return DAORPATIENT.SearchByKey(strsearch);
        }

        public ORPATIENTVO SearchByHN(string HN)
        {
            DAORPATIENT DAORPATIENT = (dbInfo == null ? new DAORPATIENT() : new DAORPATIENT(dbInfo));
            return DAORPATIENT.SearchByHN(HN);
        }

        public DataTable SearchByHN_DS(string HN)
        {
            DAORPATIENT DAORPATIENT = (dbInfo == null ? new DAORPATIENT() : new DAORPATIENT(dbInfo));
            return DAORPATIENT.SearchByHN_DS(HN);
        }
        
    }
}
