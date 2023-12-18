using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPOPERATIONMAIN
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPOPERATIONMAIN() { }
        public BLSETUPOPERATIONMAIN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPOPERATIONMAINVO> SearchAll()
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.SearchAll();
        }

        public List<SETUPOPERATIONMAINVO> SearchByKey(SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.SearchByKey(SETUPOPERATIONMAINVO);
        }

        public SETUPOPERATIONMAINVO SearchByCode(string MainCode)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.SearchByCode(MainCode);
        }

        public DataSet SearchByCode_DS(string MainCode)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.SearchByCode_DS(MainCode);
        }

        public ReturnValue Insert(SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.Insert(SETUPOPERATIONMAINVO);
        }

        public ReturnValue Update(SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.Update(SETUPOPERATIONMAINVO);
        }

        public ReturnValue Delete(string MainCode)
        {
            DASETUPOPERATIONMAIN DASETUPOPERATIONMAIN = (dbInfo == null ? new DASETUPOPERATIONMAIN() : new DASETUPOPERATIONMAIN(dbInfo));
            return DASETUPOPERATIONMAIN.Delete(MainCode);
        }
    }
}
