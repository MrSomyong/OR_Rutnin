using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORNURSE
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORNURSE() { }
        public BLPOSTORNURSE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORNURSEVO> SearchByKey(POSTORNURSEVO POSTORNURSEVO)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.SearchByKey(POSTORNURSEVO);
        }

        public int GetSuffixNext(string ORID)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.GetSuffixNext(ORID);
        }

        public ReturnValue Insert(POSTORNURSEVO POSTORNURSEVO)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.Insert(POSTORNURSEVO);
        }

        public ReturnValue Update(POSTORNURSEVO POSTORNURSEVO)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.Update(POSTORNURSEVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.Delete(ORID);
        }

        public ReturnValue Delete(string ORID, int Suffix)
        {
            DAPOSTORNURSE DAPOSTORNURSE = (dbInfo == null ? new DAPOSTORNURSE() : new DAPOSTORNURSE(dbInfo));
            return DAPOSTORNURSE.Delete(ORID, Suffix);
        }
    }
}
