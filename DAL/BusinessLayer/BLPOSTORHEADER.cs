using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORHEADER
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORHEADER() { }
        public BLPOSTORHEADER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORHEADERVO> SearchByKey(POSTORHEADERVO POSTORHEADERVO)
        {
            DAPOSTORHEADER DAPOSTORHEADER = (dbInfo == null ? new DAPOSTORHEADER() : new DAPOSTORHEADER(dbInfo));
            return DAPOSTORHEADER.SearchByKey(POSTORHEADERVO);
        }
        public bool Checkdup(string orid)
        {
            DAPOSTORHEADER DAPOSTORHEADER = (dbInfo == null ? new DAPOSTORHEADER() : new DAPOSTORHEADER(dbInfo));
            return DAPOSTORHEADER.Checkdup(orid);
        }
        
        public ReturnValue Insert(POSTORHEADERVO POSTORHEADERVO)
        {
            DAPOSTORHEADER DAPOSTORHEADER = (dbInfo == null ? new DAPOSTORHEADER() : new DAPOSTORHEADER(dbInfo));
            return DAPOSTORHEADER.Insert(POSTORHEADERVO);
        }

        public ReturnValue Update(POSTORHEADERVO POSTORHEADERVO)
        {
            DAPOSTORHEADER DAPOSTORHEADER = (dbInfo == null ? new DAPOSTORHEADER() : new DAPOSTORHEADER(dbInfo));
            return DAPOSTORHEADER.Update(POSTORHEADERVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORHEADER DAPOSTORHEADER = (dbInfo == null ? new DAPOSTORHEADER() : new DAPOSTORHEADER(dbInfo));
            return DAPOSTORHEADER.Delete(ORID);
        }

    }
}
