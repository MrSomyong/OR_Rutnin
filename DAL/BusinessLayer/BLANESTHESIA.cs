using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLANESTHESIA
    {
        DatabaseInfo dbInfo = null;
        public BLANESTHESIA() { }
        public BLANESTHESIA(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ANESTHESIAVO> SearchAll()
        {
            DAANESTHESIA DAANESTHESIA = (dbInfo == null ? new DAANESTHESIA() : new DAANESTHESIA(dbInfo));
            return DAANESTHESIA.SearchAll();
        }
        public List<ANESTHESIAVO> SearchByKey(ANESTHESIAVO ANESTHESIAVO)
        {
            DAANESTHESIA DAANESTHESIA = (dbInfo == null ? new DAANESTHESIA() : new DAANESTHESIA(dbInfo));
            return DAANESTHESIA.SearchByKey(ANESTHESIAVO);
        }
        public ANESTHESIAVO SearchByCode(string Code)
        {
            DAANESTHESIA DAANESTHESIA = (dbInfo == null ? new DAANESTHESIA() : new DAANESTHESIA(dbInfo));
            return DAANESTHESIA.SearchByCode(Code);
        }
        public DataTable SearchByCode_DS(string[] Code)
        {
            DAANESTHESIA DAANESTHESIA = (dbInfo == null ? new DAANESTHESIA() : new DAANESTHESIA(dbInfo));
            return DAANESTHESIA.SearchByCode_DS(Code);
        }
    }
}
