using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLORUSER
    {
        DatabaseInfo dbInfo = null;
        public BLORUSER() { }
        public BLORUSER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORUSERVO> SearchByKey(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.SearchByKey(ORUSERVO);
        }

        public ReturnValue Checkdup(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.CheckDup(ORUSERVO);
        }

        public ORUSERVO CheckLogin(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.CheckLogin(ORUSERVO);
        }

        public ORUSERVO GetUSERByid(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.GetUSERByid(ORUSERVO);
        }

        public ReturnValue Insert(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.Insert(ORUSERVO);
        }

        public ReturnValue UpdateProfiles(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.UpdateProfiles(ORUSERVO);
        }

        public ReturnValue ChangePassword(ORUSERVO ORUSERVO)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.ChangePassword(ORUSERVO);
        }

        public ReturnValue Delete(string USER_ID)
        {
            DAORUSER DAORUSER = (dbInfo == null ? new DAORUSER() : new DAORUSER(dbInfo));
            return DAORUSER.Delete(USER_ID);
        }
    }
}
