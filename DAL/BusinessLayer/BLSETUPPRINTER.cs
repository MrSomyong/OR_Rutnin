using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLSETUPPRINTER
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPPRINTER() { }
        public BLSETUPPRINTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPPRINTERVO> SearchAll()
        {
            DASETUPPRINTER DASETUPPRINTER = (dbInfo == null ? new DASETUPPRINTER() : new DASETUPPRINTER(dbInfo));
            return DASETUPPRINTER.SearchAll();
        }
        public List<SETUPPRINTERVO> SearchByKey(SETUPPRINTERVO SETUPPRINTERVO)
        {
            DASETUPPRINTER DASETUPPRINTER = (dbInfo == null ? new DASETUPPRINTER() : new DASETUPPRINTER(dbInfo));
            return DASETUPPRINTER.SearchByKey(SETUPPRINTERVO);
        }

        public ReturnValue Insert(SETUPPRINTERVO SETUPPRINTERVO)
        {
            DASETUPPRINTER DASETUPPRINTER = (dbInfo == null ? new DASETUPPRINTER() : new DASETUPPRINTER(dbInfo));
            return DASETUPPRINTER.Insert(SETUPPRINTERVO);
        }

        public ReturnValue Update(SETUPPRINTERVO SETUPPRINTERVO)
        {
            DASETUPPRINTER DASETUPPRINTER = (dbInfo == null ? new DASETUPPRINTER() : new DASETUPPRINTER(dbInfo));
            return DASETUPPRINTER.Update(SETUPPRINTERVO);
        }

        public ReturnValue Delete(string ID)
        {
            DASETUPPRINTER DASETUPPRINTER = (dbInfo == null ? new DASETUPPRINTER() : new DASETUPPRINTER(dbInfo));
            return DASETUPPRINTER.Delete(ID);
        }
    }
}
