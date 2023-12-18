using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORDETAIL
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORDETAIL() { }
        public BLPOSTORDETAIL(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORDETAILVO> SearchByKey(POSTORDETAILVO POSTORDETAILVO)
        {
            DAPOSTORDETAIL DAPOSTORDETAIL = (dbInfo == null ? new DAPOSTORDETAIL() : new DAPOSTORDETAIL(dbInfo));
            return DAPOSTORDETAIL.SearchByKey(POSTORDETAILVO);
        }

        public bool Checkdup(string orid)
        {
            DAPOSTORDETAIL DAPOSTORDETAIL = (dbInfo == null ? new DAPOSTORDETAIL() : new DAPOSTORDETAIL(dbInfo));
            return DAPOSTORDETAIL.Checkdup(orid);
        }

        public ReturnValue Insert(POSTORDETAILVO POSTORDETAILVO)
        {
            DAPOSTORDETAIL DAPOSTORDETAIL = (dbInfo == null ? new DAPOSTORDETAIL() : new DAPOSTORDETAIL(dbInfo));
            return DAPOSTORDETAIL.Insert(POSTORDETAILVO);
        }

        public ReturnValue Update(POSTORDETAILVO POSTORDETAILVO)
        {
            DAPOSTORDETAIL DAPOSTORDETAIL = (dbInfo == null ? new DAPOSTORDETAIL() : new DAPOSTORDETAIL(dbInfo));
            return DAPOSTORDETAIL.Update(POSTORDETAILVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORDETAIL DAPOSTORDETAIL = (dbInfo == null ? new DAPOSTORDETAIL() : new DAPOSTORDETAIL(dbInfo));
            return DAPOSTORDETAIL.Delete(ORID);
        }
    }
}
