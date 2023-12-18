using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLOROPERATION
    {
        DatabaseInfo dbInfo = null;
        public BLOROPERATION() { }
        public BLOROPERATION(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<OROPERATIONVO> SearchAll()
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.SearchAll();
        }

        public List<OROPERATIONVO> SearchByKey(OROPERATIONVO OROPERATIONVO)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.SearchByKey(OROPERATIONVO);
        }

        public List<OROPERATIONVO> SearchByORID(string orid)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.SearchByORID(orid);
        }
        public List<OROPERATIONVO> SearchInORID(List<ORHEADERVO> lstORHEADERVO)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.SearchInORID(lstORHEADERVO);
        }
        public ReturnValue Insert(OROPERATIONVO OROPERATIONVO)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.Insert(OROPERATIONVO);
        }

        public ReturnValue DeleteByID(OROPERATIONVO OROPERATIONVO)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.DeleteByID(OROPERATIONVO);
        }

        public ReturnValue DeleteByORID(string ORID)
        {
            DAOROPERATION DAOROPERATION = (dbInfo == null ? new DAOROPERATION() : new DAOROPERATION(dbInfo));
            return DAOROPERATION.DeleteByORID(ORID);
        }
    }
}
