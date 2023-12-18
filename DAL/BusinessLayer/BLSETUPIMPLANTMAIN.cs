using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPIMPLANTMAIN
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPIMPLANTMAIN() { }
        public BLSETUPIMPLANTMAIN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPIMPLANTMAINVO> SearchAll()
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.SearchAll();
        }

        public List<SETUPIMPLANTMAINVO> SearchByKey(SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.SearchByKey(SETUPIMPLANTMAINVO);
        }

        public SETUPIMPLANTMAINVO SearchByCode(string MainCode)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.SearchByCode(MainCode);
        }

        public DataSet SearchByCode_DS(string MainCode)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.SearchByCode_DS(MainCode);
        }

        public ReturnValue Insert(SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.Insert(SETUPIMPLANTMAINVO);
        }

        public ReturnValue Update(SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.Update(SETUPIMPLANTMAINVO);
        }

        public ReturnValue Delete(string MainCode)
        {
            DASETUPIMPLANTMAIN DASETUPIMPLANTMAIN = (dbInfo == null ? new DASETUPIMPLANTMAIN() : new DASETUPIMPLANTMAIN(dbInfo));
            return DASETUPIMPLANTMAIN.Delete(MainCode);
        }
    }
}
