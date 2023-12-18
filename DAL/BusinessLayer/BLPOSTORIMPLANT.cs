using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORIMPLANT
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORIMPLANT() { }
        public BLPOSTORIMPLANT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORIMPLANTVO> SearchByKey(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.SearchByKey(POSTORIMPLANTVO);
        }

        public ReturnValue Insert(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.Insert(POSTORIMPLANTVO);
        }

        public ReturnValue UpdateImage(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.UpdateImage(POSTORIMPLANTVO);
        }

        public ReturnValue UpdateUsed(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.UpdateUsed(POSTORIMPLANTVO);
        }

        public ReturnValue UpdateRemark(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.UpdateRemark(POSTORIMPLANTVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.Delete(ORID);
        }

        public ReturnValue Delete(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            DAPOSTORIMPLANT DAPOSTORIMPLANT = (dbInfo == null ? new DAPOSTORIMPLANT() : new DAPOSTORIMPLANT(dbInfo));
            return DAPOSTORIMPLANT.Delete(POSTORIMPLANTVO);
        }
    }
}
