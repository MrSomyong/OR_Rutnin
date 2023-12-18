using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLDOCUMENT_ITEM
    {
        DatabaseInfo dbInfo = null;
        public BLDOCUMENT_ITEM() { }
        public BLDOCUMENT_ITEM(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOCUMENT_ITEMVO> SearchByKey(DOCUMENT_ITEMVO DOCUMENT_ITEMVO)
        {
            DADOCUMENT_ITEM DADOCUMENT_ITEM = (dbInfo == null ? new DADOCUMENT_ITEM() : new DADOCUMENT_ITEM(dbInfo));
            return DADOCUMENT_ITEM.SearchByKey(DOCUMENT_ITEMVO);
        }

        public byte [] SearchByHN(string HN)
        {
            DADOCUMENT_ITEM DADOCUMENT_ITEM = (dbInfo == null ? new DADOCUMENT_ITEM() : new DADOCUMENT_ITEM(dbInfo));
            return DADOCUMENT_ITEM.SearchByHN(HN);
        }

        public Boolean SearchByURL(string strURL)
        {
            DADOCUMENT_ITEM DADOCUMENT_ITEM = (dbInfo == null ? new DADOCUMENT_ITEM() : new DADOCUMENT_ITEM(dbInfo));
            return DADOCUMENT_ITEM.SearchByURL(strURL);
        }
    }
}
