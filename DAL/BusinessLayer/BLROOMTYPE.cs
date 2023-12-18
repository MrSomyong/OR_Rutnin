using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLROOMTYPE
    {
        DatabaseInfo dbInfo = null;
        public BLROOMTYPE() { }
        public BLROOMTYPE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ROOMTYPEVO> SearchAll()
        {
            DAROOMTYPE DAROOMTYPE = (dbInfo == null ? new DAROOMTYPE() : new DAROOMTYPE(dbInfo));
            return DAROOMTYPE.SearchAll();
        }

        public ROOMTYPEVO SearchByCode(string Code)
        {
            DAROOMTYPE DAROOMTYPE = (dbInfo == null ? new DAROOMTYPE() : new DAROOMTYPE(dbInfo));
            return DAROOMTYPE.SearchByCode(Code);
        }
        public DataTable SearchByCode_DS(string[] Code)
        {
            DAROOMTYPE DAROOMTYPE = (dbInfo == null ? new DAROOMTYPE() : new DAROOMTYPE(dbInfo));
            return DAROOMTYPE.SearchByCode_DS(Code);
        }
    }
}
