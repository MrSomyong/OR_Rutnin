using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLSETUPHYPERLINK
    {
        DatabaseInfo dbInfo = null;
        DatabaseInfo appConnDBInfo = null;
        DatabaseInfo extConnDBInfo = null;
        public BLSETUPHYPERLINK() { }
        public BLSETUPHYPERLINK(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public BLSETUPHYPERLINK(DatabaseInfo appConnDBInfo, DatabaseInfo extConnDBInfo) { this.appConnDBInfo = appConnDBInfo; this.extConnDBInfo = extConnDBInfo; }

        public ReturnValue Insert(SETUPHYPERLINK SETUPHYPERLINK)
        {
            DASETUPHYPERLINK DASETUPHYPERLINK = (dbInfo == null ? new DASETUPHYPERLINK() : new DASETUPHYPERLINK(dbInfo));
            return DASETUPHYPERLINK.Insert(SETUPHYPERLINK);
        }

        public ReturnValue Delete(SETUPHYPERLINK SETUPHYPERLINK)
        {
            DASETUPHYPERLINK DASETUPHYPERLINK = (dbInfo == null ? new DASETUPHYPERLINK() : new DASETUPHYPERLINK(dbInfo));
            return DASETUPHYPERLINK.Delete(SETUPHYPERLINK);
        }

        public ReturnValue Update(SETUPHYPERLINK SETUPHYPERLINK,string oldLinkCode)
        {
            DASETUPHYPERLINK DASETUPHYPERLINK = (dbInfo == null ? new DASETUPHYPERLINK() : new DASETUPHYPERLINK(dbInfo));
            return DASETUPHYPERLINK.Update(SETUPHYPERLINK, oldLinkCode);
        }
        public List<SETUPHYPERLINK> SearchAll()
        {
            DASETUPHYPERLINK DASETUPHYPERLINK = (dbInfo == null ? new DASETUPHYPERLINK() : new DASETUPHYPERLINK(dbInfo));
            return DASETUPHYPERLINK.SearchAll();
        }

        //public SETUPCOMPUTER SearchByKey(string computerCode)
        //{
        //    DASETUPCOMPUTER DASETUPCOMPUTER = (dbInfo == null ? new DASETUPCOMPUTER() : new DASETUPCOMPUTER(dbInfo));
        //    return DASETUPCOMPUTER.SearchByKey(computerCode);
        //}
        

        public ReturnValue CheckDup (SETUPHYPERLINK SETUPHYPERLINK)
        {
            DASETUPHYPERLINK DASETUPHYPERLINK = (dbInfo == null ? new DASETUPHYPERLINK() : new DASETUPHYPERLINK(dbInfo));
            return DASETUPHYPERLINK.CheckDup(SETUPHYPERLINK);
        }



    }

}
