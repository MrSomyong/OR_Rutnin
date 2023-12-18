using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLSETUPGROUPMETHOD
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPGROUPMETHOD() { }
        public BLSETUPGROUPMETHOD(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public ReturnValue Insert(SETUPGROUPMETHOD SETUPGROUPMETHOD)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.Insert(SETUPGROUPMETHOD);
        }

        public ReturnValue InActive(SETUPGROUPMETHOD SETUPGROUPMETHOD)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.InActive(SETUPGROUPMETHOD);
        }

        public ReturnValue Update(SETUPGROUPMETHOD SETUPGROUPMETHOD)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.Update(SETUPGROUPMETHOD);
        }
        public List<SETUPGROUPMETHOD> SearchAll()
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.SearchAll();
        }

        public List<SETUPGROUPMETHOD> SearchMedicineOnly()
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.SearchMedicineOnly();
        }

        public List<SETUPGROUPMETHOD> SearchAllFilterBy(string doctorCode, string clinicCode, string computerCode)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.SearchAllFilterBy(doctorCode, clinicCode, computerCode);
        }

        public ReturnValue CheckDup (SETUPGROUPMETHOD SETUPGROUPMETHOD)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.CheckDup(SETUPGROUPMETHOD);
        }
        public SETUPGROUPMETHOD GetSETUPGROUPMETHODByKey(string groupMethodCode)
        {
            DASETUPGROUPMETHOD DASETUPGROUPMETHOD = (dbInfo == null ? new DASETUPGROUPMETHOD() : new DASETUPGROUPMETHOD(dbInfo));
            return DASETUPGROUPMETHOD.GetSETUPGROUPMETHODByKey(groupMethodCode);
        }
        

    }

}
