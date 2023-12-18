using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTOROPERATION
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTOROPERATION() { }
        public BLPOSTOROPERATION(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTOROPERATIONVO> SearchByKey(POSTOROPERATIONVO POSTOROPERATIONVO)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.SearchByKey(POSTOROPERATIONVO);
        }

        public List<POSTOROPERATIONVO> SearchByID(string id)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.SearchByID(id);
        }

        public List<POSTOROPERATIONVO> SearchByORID(string orid)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.SearchByORID(orid);
        }       

        public ReturnValue Insert(POSTOROPERATIONVO POSTOROPERATIONVO)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.Insert(POSTOROPERATIONVO);
        }

        public ReturnValue UpdateSetupProcedure(POSTOROPERATIONVO POSTOROPERATIONVO)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.UpdateSetupProcedure(POSTOROPERATIONVO);
        }

        public ReturnValue DeleteByID(POSTOROPERATIONVO POSTOROPERATIONVO)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.DeleteByID(POSTOROPERATIONVO);
        }

        public ReturnValue DeleteByORID(string ORID)
        {
            DAPOSTOROPERATION DAPOSTOROPERATION = (dbInfo == null ? new DAPOSTOROPERATION() : new DAPOSTOROPERATION(dbInfo));
            return DAPOSTOROPERATION.DeleteByORID(ORID);
        }
    }
}
