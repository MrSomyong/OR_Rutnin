using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLROOMTYPEPROCE
    {
        DatabaseInfo dbInfo = null;
        public BLROOMTYPEPROCE() { }
        public BLROOMTYPEPROCE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ROOMTYPEPROCEVO> SearchByKey(ROOMTYPEPROCEVO ROOMTYPEPROCEVO)
        {
            DAROOMTYPEPROCE DAROOMTYPEPROCE = (dbInfo == null ? new DAROOMTYPEPROCE() : new DAROOMTYPEPROCE(dbInfo));
            return DAROOMTYPEPROCE.SearchByUser(ROOMTYPEPROCEVO);
        }
        public List<ROOMTYPEPROCEVO> CheckDup(ROOMTYPEPROCEVO ROOMTYPEPROCEVO)
        {
            DAROOMTYPEPROCE DAROOMTYPEPROCE = (dbInfo == null ? new DAROOMTYPEPROCE() : new DAROOMTYPEPROCE(dbInfo));
            return DAROOMTYPEPROCE.CheckDup(ROOMTYPEPROCEVO);
        }
        public ReturnValue Insert(ROOMTYPEPROCEVO ROOMTYPEPROCEVO)
        {
            DAROOMTYPEPROCE DAROOMTYPEPROCE = (dbInfo == null ? new DAROOMTYPEPROCE() : new DAROOMTYPEPROCE(dbInfo));
            return DAROOMTYPEPROCE.Insert(ROOMTYPEPROCEVO);
        }
        public ReturnValue Delete(ROOMTYPEPROCEVO ROOMTYPEPROCEVO)
        {
            DAROOMTYPEPROCE DAROOMTYPEPROCE = (dbInfo == null ? new DAROOMTYPEPROCE() : new DAROOMTYPEPROCE(dbInfo));
            return DAROOMTYPEPROCE.Delete(ROOMTYPEPROCEVO);
        }
    }
}
