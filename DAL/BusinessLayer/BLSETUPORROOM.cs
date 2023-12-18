using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPORROOM
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPORROOM() { }
        public BLSETUPORROOM(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPORROOMVO> SearchByKey(SETUPORROOMVO SETUPORROOMVO)
        {
            DASETUPORROOM DASETUPORROOM = (dbInfo == null ? new DASETUPORROOM() : new DASETUPORROOM(dbInfo));
            return DASETUPORROOM.SearchByKey(SETUPORROOMVO);
        }      
        public ReturnValue Insert(SETUPORROOMVO SETUPORROOMVO)
        {
            DASETUPORROOM DASETUPORROOM = (dbInfo == null ? new DASETUPORROOM() : new DASETUPORROOM(dbInfo));
            return DASETUPORROOM.Insert(SETUPORROOMVO);
        }
        public ReturnValue Update(SETUPORROOMVO SETUPORROOMVO)
        {
            DASETUPORROOM DASETUPORROOM = (dbInfo == null ? new DASETUPORROOM() : new DASETUPORROOM(dbInfo));
            return DASETUPORROOM.Update(SETUPORROOMVO);
        }
        public ReturnValue Delete(SETUPORROOMVO SETUPORROOMVO)
        {
            DASETUPORROOM DASETUPORROOM = (dbInfo == null ? new DASETUPORROOM() : new DASETUPORROOM(dbInfo));
            return DASETUPORROOM.Delete(SETUPORROOMVO);
        }
    }
}
