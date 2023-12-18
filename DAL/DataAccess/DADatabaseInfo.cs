using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    internal class DADatabaseInfo : DataAccess
    {
        public DADatabaseInfo() { }
        public DADatabaseInfo(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }

        public ReturnValue ConnectDatabase()
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                ConnectDB();
                retVal.Value = true;
                DisconnectDB();
            }
            catch (Exception exc) { retVal.Value = false; retVal.Exception = exc; }
            return retVal;
        }
    }
}