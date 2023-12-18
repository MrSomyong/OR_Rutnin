using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPCOMPLICATION
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPCOMPLICATION() { }
        public BLSETUPCOMPLICATION(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPCOMPLICATIONVO> SearchByKey(SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO)
        {
            DASETUPCOMPLICATION DASETUPCOMPLICATION = (dbInfo == null ? new DASETUPCOMPLICATION() : new DASETUPCOMPLICATION(dbInfo));
            return DASETUPCOMPLICATION.SearchByKey(SETUPCOMPLICATIONVO);
        }
        public List<SETUPCOMPLICATIONVO> SearchByPrimary(string ID)
        {
            DASETUPCOMPLICATION DASETUPCOMPLICATION = (dbInfo == null ? new DASETUPCOMPLICATION() : new DASETUPCOMPLICATION(dbInfo));
            return DASETUPCOMPLICATION.SearchByPrimary(ID);
        }
        
        public ReturnValue Insert(SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO)
        {
            DASETUPCOMPLICATION DASETUPCOMPLICATION = (dbInfo == null ? new DASETUPCOMPLICATION() : new DASETUPCOMPLICATION(dbInfo));
            return DASETUPCOMPLICATION.Insert(SETUPCOMPLICATIONVO);
        }

        public ReturnValue Update(SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO)
        {
            DASETUPCOMPLICATION DASETUPCOMPLICATION = (dbInfo == null ? new DASETUPCOMPLICATION() : new DASETUPCOMPLICATION(dbInfo));
            return DASETUPCOMPLICATION.Update(SETUPCOMPLICATIONVO);
        }

        public ReturnValue Delete(string ID)
        {
            DASETUPCOMPLICATION DASETUPCOMPLICATION = (dbInfo == null ? new DASETUPCOMPLICATION() : new DASETUPCOMPLICATION(dbInfo));
            return DASETUPCOMPLICATION.Delete(ID);
        }

    }
}
