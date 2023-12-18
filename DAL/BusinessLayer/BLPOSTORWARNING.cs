using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORWARNING
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORWARNING() { }
        public BLPOSTORWARNING(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORWARNINGVO> SearchByKey(POSTORWARNINGVO POSTORWARNINGVO)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.SearchByKey(POSTORWARNINGVO);
        }
        public List<POSTORWARNINGVO> SearchByPrimary(string ORID, string ID)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.SearchByPrimary(ORID, ID);
        }

        public ReturnValue Insert(POSTORWARNINGVO POSTORWARNINGVO)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.Insert(POSTORWARNINGVO);
        }

        public ReturnValue Update(POSTORWARNINGVO POSTORWARNINGVO)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.Update(POSTORWARNINGVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.Delete(ORID);
        }

        public ReturnValue Delete(string ORID, string ID)
        {
            DAPOSTORWARNING DAPOSTORWARNING = (dbInfo == null ? new DAPOSTORWARNING() : new DAPOSTORWARNING(dbInfo));
            return DAPOSTORWARNING.Delete(ORID, ID);
        }
    }
}
