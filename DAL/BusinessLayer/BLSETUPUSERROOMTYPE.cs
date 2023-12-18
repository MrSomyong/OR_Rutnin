using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPUSERROOMTYPE
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPUSERROOMTYPE() { }
        public BLSETUPUSERROOMTYPE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPUSERROOMTYPEVO> SearchByKey(SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO)
        {
            DASETUPUSERROOMTYPE DASETUPUSERROOMTYPE = (dbInfo == null ? new DASETUPUSERROOMTYPE() : new DASETUPUSERROOMTYPE(dbInfo));
            return DASETUPUSERROOMTYPE.SearchByUser(SETUPUSERROOMTYPEVO);
        }
        public List<SETUPUSERROOMTYPEVO> CheckDup(SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO)
        {
            DASETUPUSERROOMTYPE DASETUPUSERROOMTYPE = (dbInfo == null ? new DASETUPUSERROOMTYPE() : new DASETUPUSERROOMTYPE(dbInfo));
            return DASETUPUSERROOMTYPE.CheckDup(SETUPUSERROOMTYPEVO);
        }
        public ReturnValue Insert(SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO)
        {
            DASETUPUSERROOMTYPE DASETUPUSERROOMTYPE = (dbInfo == null ? new DASETUPUSERROOMTYPE() : new DASETUPUSERROOMTYPE(dbInfo));
            return DASETUPUSERROOMTYPE.Insert(SETUPUSERROOMTYPEVO);
        }
        public ReturnValue Delete(SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO)
        {
            DASETUPUSERROOMTYPE DASETUPUSERROOMTYPE = (dbInfo == null ? new DASETUPUSERROOMTYPE() : new DASETUPUSERROOMTYPE(dbInfo));
            return DASETUPUSERROOMTYPE.Delete(SETUPUSERROOMTYPEVO);
        }
    }
}
