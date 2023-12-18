using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace DAL
{
    
    public class BLVT_VNMASTER
    {
        DatabaseInfo dbInfo = null;
        public BLVT_VNMASTER() { }
        public BLVT_VNMASTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<VT_VNMASTER> SearchVT_VNMASTERByKey(VT_VNMASTER VT_VNMASTER)
        {
            DAVT_VNMASTER DAVT_VNMASTER = (dbInfo == null ? new DAVT_VNMASTER() : new DAVT_VNMASTER(dbInfo));
            return DAVT_VNMASTER.SearchByKey(VT_VNMASTER);
        }
        public VT_VNMASTER GetVNDetailByKey(VT_VNMASTER VT_VNMASTER)
        {
            DAVT_VNMASTER DAVT_VNMASTER = (dbInfo == null ? new DAVT_VNMASTER() : new DAVT_VNMASTER(dbInfo));
            return DAVT_VNMASTER.GetVNDetailByKey(VT_VNMASTER);
        }
    }
    
}
