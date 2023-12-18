using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLPOSTORCOMPLICATION
    {
        DatabaseInfo dbInfo = null;
        public BLPOSTORCOMPLICATION() { }
        public BLPOSTORCOMPLICATION(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<POSTORCOMPLICATIONVO> SearchByKey(POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.SearchByKey(POSTORCOMPLICATIONVO);
        }
        public List<POSTORCOMPLICATIONVO> SearchByPrimary(string ORID, string ID)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.SearchByPrimary(ORID, ID);
        }
        
        //public int GetSuffixNext(string ORID)
        //{
        //    DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
        //    return DAPOSTORCOMPLICATION.GetSuffixNext(ORID);
        //}


        public ReturnValue Insert(POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.Insert(POSTORCOMPLICATIONVO);
        }

        public ReturnValue Update(POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.Update(POSTORCOMPLICATIONVO);
        }

        public ReturnValue Delete(string ORID)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.Delete(ORID);
        }

        public ReturnValue Delete(string ORID, string ID)
        {
            DAPOSTORCOMPLICATION DAPOSTORCOMPLICATION = (dbInfo == null ? new DAPOSTORCOMPLICATION() : new DAPOSTORCOMPLICATION(dbInfo));
            return DAPOSTORCOMPLICATION.Delete(ORID, ID);
        }
    }
}
