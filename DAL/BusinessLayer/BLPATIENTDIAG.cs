using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPATIENTDIAG
    {
        DatabaseInfo dbInfo = null;
        public BLPATIENTDIAG() { }
        public BLPATIENTDIAG(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<PATIENTDIAGVO> SearchByKey(PATIENTDIAGVO PATIENTDIAGVO)
        {
            DAPATIENTDIAG DAPATIENTDIAG = (dbInfo == null ? new DAPATIENTDIAG() : new DAPATIENTDIAG(dbInfo));
            return DAPATIENTDIAG.SearchByKey(PATIENTDIAGVO);
        }
    }
}
