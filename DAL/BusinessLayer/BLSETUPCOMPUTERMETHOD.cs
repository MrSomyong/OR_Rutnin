using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLSETUPCOMPUTERMETHOD
    {
        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;
        public BLSETUPCOMPUTERMETHOD() { }
        public BLSETUPCOMPUTERMETHOD(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLSETUPCOMPUTERMETHOD(DatabaseInfo appConnDBInfo, DatabaseInfo extConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }

        public ReturnValue Insert(SETUPCOMPUTERMETHOD SETUPCOMPUTERMETHOD)
        {
            DASETUPCOMPUTERMETHOD DASETUPCOMPUTERMETHOD = (dbInfo == null ? new DASETUPCOMPUTERMETHOD() : new DASETUPCOMPUTERMETHOD(dbInfo));
            return DASETUPCOMPUTERMETHOD.Insert(SETUPCOMPUTERMETHOD);
        }

        public ReturnValue Delete(SETUPCOMPUTERMETHOD SETUPCOMPUTERMETHOD)
        {
            DASETUPCOMPUTERMETHOD DASETUPCOMPUTERMETHOD = (dbInfo == null ? new DASETUPCOMPUTERMETHOD() : new DASETUPCOMPUTERMETHOD(dbInfo));
            return DASETUPCOMPUTERMETHOD.Delete(SETUPCOMPUTERMETHOD);
        }

        
    
        public List<SETUPCOMPUTERMETHOD> GetAllByKey(string computerCode)
        {
            DASETUPCOMPUTERMETHOD DASETUPCOMPUTERMETHOD = (dbInfo == null ? new DASETUPCOMPUTERMETHOD() : new DASETUPCOMPUTERMETHOD(dbInfo));
            return DASETUPCOMPUTERMETHOD.GetAllByKey(computerCode);
        }
        

        public ReturnValue CheckDup (SETUPCOMPUTERMETHOD _SETUPCOMPUTERMETHOD)
        {
            DASETUPCOMPUTERMETHOD DASETUPCOMPUTERMETHOD = (dbInfo == null ? new DASETUPCOMPUTERMETHOD() : new DASETUPCOMPUTERMETHOD(dbInfo));
            return DASETUPCOMPUTERMETHOD.CheckDup(_SETUPCOMPUTERMETHOD);
        }



    }

}
