using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORMIGRATION
    {
        DatabaseInfo dbInfo = null;
        public BLORMIGRATION() { }
        public BLORMIGRATION(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORMIGRATIONVO> SearchByKey(ORMIGRATIONVO ORMIGRATIONVO)
        {
            DAORMIGRATION DAORMIGRATION = (dbInfo == null ? new DAORMIGRATION() : new DAORMIGRATION(dbInfo));
            return DAORMIGRATION.SearchByKey(ORMIGRATIONVO);
        }

        public ReturnValue Insert(ORMIGRATIONVO ORMIGRATIONVO)
        {
            DAORMIGRATION DAORMIGRATION = (dbInfo == null ? new DAORMIGRATION() : new DAORMIGRATION(dbInfo));
            return DAORMIGRATION.Insert(ORMIGRATIONVO);
        }

        public ReturnValue Update(ORMIGRATIONVO ORMIGRATIONVO)
        {
            DAORMIGRATION DAORMIGRATION = (dbInfo == null ? new DAORMIGRATION() : new DAORMIGRATION(dbInfo));
            return DAORMIGRATION.Update(ORMIGRATIONVO);
        }

        public ReturnValue Delete(string ID)
        {
            DAORMIGRATION DAORMIGRATION = (dbInfo == null ? new DAORMIGRATION() : new DAORMIGRATION(dbInfo));
            return DAORMIGRATION.Delete(ID);
        }
    }
}
