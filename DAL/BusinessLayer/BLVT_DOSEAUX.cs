using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLVT_DOSEAUX
    {
        DatabaseInfo dbInfo = null;
        public BLVT_DOSEAUX() { }
        public BLVT_DOSEAUX(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOSEAUX> SearchAll()
        {
            DAVT_DOSEAUX DAVT_DOSEAUX = (dbInfo == null ? new DAVT_DOSEAUX() : new DAVT_DOSEAUX(dbInfo));
            return DAVT_DOSEAUX.SearchAll();
        }

        public DOSEAUX GetDoseAUXByKey(string code)
        {
            DAVT_DOSEAUX DAVT_DOSEAUX = (dbInfo == null ? new DAVT_DOSEAUX() : new DAVT_DOSEAUX(dbInfo));
            return DAVT_DOSEAUX.GetDoseAUXByKey(code);
        }




    }

}
