using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{
    
    public class BLVT_CLINIC
    {
        DatabaseInfo dbInfo = null;
        public BLVT_CLINIC() { }
        public BLVT_CLINIC(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
      
        public List<VT_CLINIC> SearchAll()
        {
            DAVT_CLINIC DAVT_CLINIC = (dbInfo == null ? new DAVT_CLINIC() : new DAVT_CLINIC(dbInfo));
            return DAVT_CLINIC.SearchAll();
        }

        public VT_CLINIC GetClinicByKey(string clinicCode)
        {
            DAVT_CLINIC DAVT_CLINIC = (dbInfo == null ? new DAVT_CLINIC() : new DAVT_CLINIC(dbInfo));
            return DAVT_CLINIC.GetClinicByKey(clinicCode);
        }




    }

}
