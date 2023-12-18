using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPICD
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPICD() { }
        public BLSETUPICD(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPICDVO> SearchByKey(SETUPICDVO SETUPICDVO)
        {
            DASETUPICD DASETUPICD = (dbInfo == null ? new DASETUPICD() : new DASETUPICD(dbInfo));
            return DASETUPICD.SearchByKey(SETUPICDVO);
        }
        public List<SETUPICDVO> SearchLikeByKey(SETUPICDVO SETUPICDVO)
        {
            DASETUPICD DASETUPICD = (dbInfo == null ? new DASETUPICD() : new DASETUPICD(dbInfo));
            return DASETUPICD.SearchLikeByKey(SETUPICDVO);
        }
        
        public List<SETUPICDVO> SearchByCode(string Code)
        {
            DASETUPICD DASETUPICD = (dbInfo == null ? new DASETUPICD() : new DASETUPICD(dbInfo));
            return DASETUPICD.SearchByCode(Code);
        }

        public ReturnValue Insert(SETUPICDVO SETUPICDVO)
        {
            DASETUPICD DASETUPICD = (dbInfo == null ? new DASETUPICD() : new DASETUPICD(dbInfo));
            return DASETUPICD.Insert(SETUPICDVO);
        }
    }
}
