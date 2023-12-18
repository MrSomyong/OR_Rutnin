using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLVT_STORE
    {
        DatabaseInfo dbInfo = null;
        public BLVT_STORE() { }
        public BLVT_STORE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
      
        public List<VT_STORE> SearchAll()
        {
            DAVT_STORE DAVT_STORE = (dbInfo == null ? new DAVT_STORE() : new DAVT_STORE(dbInfo));
            return DAVT_STORE.SearchAll();
        }

        public VT_STORE GetStoreByKey(string stockCode)
        {
            DAVT_STORE DAVT_STORE = (dbInfo == null ? new DAVT_STORE() : new DAVT_STORE(dbInfo));
            return DAVT_STORE.GetStoreByKey(stockCode);
        }




    }

}
