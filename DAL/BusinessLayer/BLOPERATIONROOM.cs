using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLOPERATIONROOM
    {
        DatabaseInfo dbInfo = null;
        public BLOPERATIONROOM() { }
        public BLOPERATIONROOM(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<OPERATIONROOMVO> SearchAll()
        {
            DAOPERATIONROOM DAOPERATIONROOM = (dbInfo == null ? new DAOPERATIONROOM() : new DAOPERATIONROOM(dbInfo));
            return DAOPERATIONROOM.SearchAll();
        }
        public OPERATIONROOMVO SearchByCode(string Code)
        {
            DAOPERATIONROOM DAOPERATIONROOM = (dbInfo == null ? new DAOPERATIONROOM() : new DAOPERATIONROOM(dbInfo));
            return DAOPERATIONROOM.SearchByCode(Code);
        }
        public OPERATIONROOMVO SearchByCodeNotLike(string Code)
        {
            DAOPERATIONROOM DAOPERATIONROOM = (dbInfo == null ? new DAOPERATIONROOM() : new DAOPERATIONROOM(dbInfo));
            return DAOPERATIONROOM.SearchByCodeNotLike(Code);
        }
        
        public DataTable SearchByCode_DS(string[] Code)
        {
            DAOPERATIONROOM DAOPERATIONROOM = (dbInfo == null ? new DAOPERATIONROOM() : new DAOPERATIONROOM(dbInfo));
            return DAOPERATIONROOM.SearchByCode_DS(Code);
        }
    }
}
