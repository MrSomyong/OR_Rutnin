using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPORROOMTYPE
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPORROOMTYPE() { }
        public BLSETUPORROOMTYPE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPORROOMTYPEVO> SearchByKey(SETUPORROOMTYPEVO SETUPORROOMTYPEVO)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.SearchByKey(SETUPORROOMTYPEVO);
        }

        public List<SETUPORROOMTYPEVO> SearchByUser(string xUserID)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.SearchByUser(xUserID);
        }        

        public List<SETUPORROOMTYPEVO> CheckDupProcedureCode(SETUPORROOMTYPEVO SETUPORROOMTYPEVO)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.CheckDupProcedureCode(SETUPORROOMTYPEVO);
        }
        
        public ReturnValue Insert(SETUPORROOMTYPEVO SETUPORROOMTYPEVO)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.Insert(SETUPORROOMTYPEVO);
        }
        public ReturnValue Update(SETUPORROOMTYPEVO SETUPORROOMTYPEVO)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.Update(SETUPORROOMTYPEVO);
        }
        public ReturnValue Delete(SETUPORROOMTYPEVO SETUPORROOMTYPEVO)
        {
            DASETUPORROOMTYPE DASETUPORROOMTYPE = (dbInfo == null ? new DASETUPORROOMTYPE() : new DASETUPORROOMTYPE(dbInfo));
            return DASETUPORROOMTYPE.Delete(SETUPORROOMTYPEVO);
        }
    }
}
