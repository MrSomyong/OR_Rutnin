using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLV_DOCTOR_ORDAYWEEK
    {
        DatabaseInfo dbInfo = null;
        public BLV_DOCTOR_ORDAYWEEK() { }
        public BLV_DOCTOR_ORDAYWEEK(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }
        public List<V_DOCTOR_ORDAYWEEKVO> SearchAll()
        {
            DAV_DOCTOR_ORDAYWEEK da = (dbInfo == null ? new DAV_DOCTOR_ORDAYWEEK() : new DAV_DOCTOR_ORDAYWEEK(dbInfo));
            return da.SearchAll();
        }
    }
}
