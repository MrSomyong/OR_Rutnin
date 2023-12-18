using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORICD
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORICD() { }
        public BLPOSTORICD(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORICDVO> SearchByKey(POSTORICDVO POSTORICDVO)
        {
            DAPOSTORICD DAPOSTORICD = (dbInfo == null ? new DAPOSTORICD() : new DAPOSTORICD(dbInfo));
            return DAPOSTORICD.SearchByKey(POSTORICDVO);
        }

        public ReturnValue Insert(POSTORICDVO POSTORICDVO)
        {
            DAPOSTORICD DAPOSTORICD = (dbInfo == null ? new DAPOSTORICD() : new DAPOSTORICD(dbInfo));
            return DAPOSTORICD.Insert(POSTORICDVO);
        }

        public ReturnValue Update(POSTORICDVO POSTORICDVO)
        {
            DAPOSTORICD DAPOSTORICD = (dbInfo == null ? new DAPOSTORICD() : new DAPOSTORICD(dbInfo));
            return DAPOSTORICD.Update(POSTORICDVO);
        }

        public ReturnValue Delete(string ORID, string ID)
        {
            DAPOSTORICD DAPOSTORICD = (dbInfo == null ? new DAPOSTORICD() : new DAPOSTORICD(dbInfo));
            return DAPOSTORICD.Delete(ORID, ID);
        }

        public ReturnValue DeleteByID(string ID)
        {
            DAPOSTORICD DAPOSTORICD = (dbInfo == null ? new DAPOSTORICD() : new DAPOSTORICD(dbInfo));
            return DAPOSTORICD.DeleteByID(ID);
        }
    }
}
