using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPIMPLANTSUB
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPIMPLANTSUB() { }
        public BLSETUPIMPLANTSUB(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPIMPLANTSUBVO> SearchAll()
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.SearchAll();
        }

        public List<SETUPIMPLANTSUBVO> SearchByKey(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.SearchByKey(SETUPIMPLANTSUBVO);
        }

        public DataSet SearchByKey_DS(string MainCode, string SubCode)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.SearchByKey_DS(MainCode, SubCode);
        }

        public ReturnValue Insert(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.Insert(SETUPIMPLANTSUBVO);
        }

        public ReturnValue Update(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.Update(SETUPIMPLANTSUBVO);
        }

        public ReturnValue Delete(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.Delete(SETUPIMPLANTSUBVO);
        }
        public ReturnValue DeleteByMain(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            DASETUPIMPLANTSUB DASETUPIMPLANTSUB = (dbInfo == null ? new DASETUPIMPLANTSUB() : new DASETUPIMPLANTSUB(dbInfo));
            return DASETUPIMPLANTSUB.DeleteByMain(SETUPIMPLANTSUBVO);
        }
    }
}
