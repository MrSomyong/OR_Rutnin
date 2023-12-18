using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLAPPOINTMENT
    {
        DatabaseInfo dbInfo = null;
        public BLAPPOINTMENT() { }
        public BLAPPOINTMENT(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<APPOINTMENTVO> SearchByKey(APPOINTMENTVO APPOINTMENTVO)
        {
            DAAPPOINTMENT DAAPPOINTMENT = (dbInfo == null ? new DAAPPOINTMENT() : new DAAPPOINTMENT(dbInfo));
            return DAAPPOINTMENT.SearchByKey(APPOINTMENTVO);
        }

    }
}
