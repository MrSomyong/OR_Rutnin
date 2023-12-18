using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLSETUPORGAN
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPORGAN() { }
        public BLSETUPORGAN(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPORGANVO> SearchByKey(SETUPORGANVO SETUPORGANVO)
        {
            DASETUPORGAN DASETUPORGAN = (dbInfo == null ? new DASETUPORGAN() : new DASETUPORGAN(dbInfo));
            return DASETUPORGAN.SearchByKey(SETUPORGANVO);
        }
        public ReturnValue Insert(SETUPORGANVO SETUPORGANVO)
        {
            DASETUPORGAN DASETUPORGAN = (dbInfo == null ? new DASETUPORGAN() : new DASETUPORGAN(dbInfo));
            return DASETUPORGAN.Insert(SETUPORGANVO);
        }
    }
}
