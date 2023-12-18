using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPOPERATIONSUB
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPOPERATIONSUB() { }
        public BLSETUPOPERATIONSUB(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPOPERATIONSUBVO> SearchAll()
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.SearchAll();
        }

        public List<SETUPOPERATIONSUBVO> SearchByKey(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.SearchByKey(SETUPOPERATIONSUBVO);
        }

        public DataSet SearchByKey_DS(string MainCode, string SubCode)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.SearchByKey_DS(MainCode, SubCode);
        }

        public ReturnValue Insert(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.Insert(SETUPOPERATIONSUBVO);
        }

        public ReturnValue Update(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.Update(SETUPOPERATIONSUBVO);
        }

        public ReturnValue Delete(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.Delete(SETUPOPERATIONSUBVO);
        }
        public ReturnValue DeleteByMain(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            DASETUPOPERATIONSUB DASETUPOPERATIONSUB = (dbInfo == null ? new DASETUPOPERATIONSUB() : new DASETUPOPERATIONSUB(dbInfo));
            return DASETUPOPERATIONSUB.DeleteByMain(SETUPOPERATIONSUBVO);
        }
    }
}
