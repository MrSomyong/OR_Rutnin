using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace DAL
{
    
    public class BLVNMST {
        DatabaseInfo dbInfo = null;
        public BLVNMST() { }
        public BLVNMST(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VNMST> SearchVNMSTByKey(VNMST VNMST)
        {
            DAVNMST DAVNMST = (dbInfo == null ? new DAVNMST() : new DAVNMST(dbInfo));
            return DAVNMST.SearchByKey(VNMST);
        }
    }
    
}
