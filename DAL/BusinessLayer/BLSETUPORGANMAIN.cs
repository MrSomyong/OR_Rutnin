using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPORGANMAIN
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPORGANMAIN() { }
        public BLSETUPORGANMAIN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPORGANMAINVO> SearchAll()
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.SearchAll();
        }

        public List<SETUPORGANMAINVO> SearchByKey(SETUPORGANMAINVO SETUPORGANMAINVO)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.SearchByKey(SETUPORGANMAINVO);
        }

        public SETUPORGANMAINVO SearchByCode(string MainCode)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.SearchByCode(MainCode);
        }

        public DataSet SearchByCode_DS(string MainCode)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.SearchByCode_DS(MainCode);
        }

        public ReturnValue Insert(SETUPORGANMAINVO SETUPORGANMAINVO)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.Insert(SETUPORGANMAINVO);
        }

        public ReturnValue Update(SETUPORGANMAINVO SETUPORGANMAINVO)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.Update(SETUPORGANMAINVO);
        }

        public ReturnValue Delete(string MainCode)
        {
            DASETUPORGANMAIN DASETUPORGANMAIN = (dbInfo == null ? new DASETUPORGANMAIN() : new DASETUPORGANMAIN(dbInfo));
            return DASETUPORGANMAIN.Delete(MainCode);
        }
    }
}
