using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPICDCM
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPICDCM() { }
        public BLSETUPICDCM(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPICDCMVO> SearchByKey(SETUPICDCMVO SETUPICDCMVO)
        {
            DASETUPICDCM DASETUPICDCM = (dbInfo == null ? new DASETUPICDCM() : new DASETUPICDCM(dbInfo));
            return DASETUPICDCM.SearchByKey(SETUPICDCMVO);
        }
        public List<SETUPICDCMVO> SearchLikeByKey(SETUPICDCMVO SETUPICDCMVO)
        {
            DASETUPICDCM DASETUPICDCM = (dbInfo == null ? new DASETUPICDCM() : new DASETUPICDCM(dbInfo));
            return DASETUPICDCM.SearchLikeByKey(SETUPICDCMVO);
        }
        public List<SETUPICDCMVO> SearchByCode(string Code)
        {
            DASETUPICDCM DASETUPICDCM = (dbInfo == null ? new DASETUPICDCM() : new DASETUPICDCM(dbInfo));
            return DASETUPICDCM.SearchByCode(Code);
        } 
        public ReturnValue Insert(SETUPICDCMVO SETUPICDCMVO)
        {
            DASETUPICDCM DASETUPICDCM = (dbInfo == null ? new DASETUPICDCM() : new DASETUPICDCM(dbInfo));
            return DASETUPICDCM.Insert(SETUPICDCMVO);
        }
    }
}
