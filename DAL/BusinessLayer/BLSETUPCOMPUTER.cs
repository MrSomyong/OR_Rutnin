using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLSETUPCOMPUTER
    {
        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;
        public BLSETUPCOMPUTER() { }
        public BLSETUPCOMPUTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLSETUPCOMPUTER(DatabaseInfo appConnDBInfo, DatabaseInfo extConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }

        public ReturnValue Insert(SETUPCOMPUTER SETUPCOMPUTER)
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
            return DASETUPCOMPUTER.Insert(SETUPCOMPUTER);
        }

        public ReturnValue Delete(SETUPCOMPUTER SETUPCOMPUTER)
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
            return DASETUPCOMPUTER.Delete(SETUPCOMPUTER);
        }

        public ReturnValue Update(SETUPCOMPUTER SETUPCOMPUTER)
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
            return DASETUPCOMPUTER.Update(SETUPCOMPUTER);
        }
        public List<SETUPCOMPUTER> SearchAll()
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = new DASETUPCOMPUTER(appConnDBInfo , extConnDBInfo);
            return DASETUPCOMPUTER.SearchAll();
        }
        public SETUPCOMPUTER SearchByKey(string computerCode)
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
            return DASETUPCOMPUTER.SearchByKey(computerCode);
        }
        

        public ReturnValue CheckDup (SETUPCOMPUTER SETUPCOMPUTER)
        {
            DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
            return DASETUPCOMPUTER.CheckDup(SETUPCOMPUTER);
        }



    }

}
