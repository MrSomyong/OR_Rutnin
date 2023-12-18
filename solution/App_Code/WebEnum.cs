using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Web_Admin
{
    public enum UserType
    {
        None = 0,
        User = 1,
        Manager = 2,
        Admin = 3,
        Dev = 99,
    }

    public enum WebLogType
    {
        None = 0,
        NewEntry = 1,
        Edit = 2,        
    }
            
}
