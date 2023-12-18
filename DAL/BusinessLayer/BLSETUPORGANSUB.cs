using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPORGANSUB
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPORGANSUB() { }
        public BLSETUPORGANSUB(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPORGANSUBVO> SearchAll()
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.SearchAll();
        }

        public List<SETUPORGANSUBVO> SearchByKey(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.SearchByKey(SETUPORGANSUBVO);
        }

        public DataSet SearchByKey_DS(string MainCode, string SubCode)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.SearchByKey_DS(MainCode, SubCode);
        }

        public ReturnValue Insert(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.Insert(SETUPORGANSUBVO);
        }

        public ReturnValue Update(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.Update(SETUPORGANSUBVO);
        }

        public ReturnValue Delete(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.Delete(SETUPORGANSUBVO);
        }
        public ReturnValue DeleteByMain(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            DASETUPORGANSUB DASETUPORGANSUB = (dbInfo == null ? new DASETUPORGANSUB() : new DASETUPORGANSUB(dbInfo));
            return DASETUPORGANSUB.DeleteByMain(SETUPORGANSUBVO);
        }
    }
}
