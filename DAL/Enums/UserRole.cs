using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum UserRole
    {
        ADM = 1,
        RTC = 2,
        OCS = 3,
        Unkhown = -1
    }

    public class UserRoleClass
    {
        public UserRole GetUserRole(string userrole)
        {
            UserRole _Urole = UserRole.Unkhown;
            try
            {
                _Urole = (UserRole)Enum.Parse(typeof(UserRole), userrole);
            }
            catch { }
            return _Urole;
        }
    }
}