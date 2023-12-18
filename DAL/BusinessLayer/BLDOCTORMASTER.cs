using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLDOCTORMASTER
    {
        DatabaseInfo dbInfo = null;
        public BLDOCTORMASTER() { }
        public BLDOCTORMASTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<DOCTORMASTERVO> SearchAll()
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchAll();
        }

        public DOCTORMASTERVO SearchByCode(string Code)
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchByCode(Code);
        }

        public List<DOCTORMASTERVO> SearchByKey(DOCTORMASTERVO DOCTORMASTERVO)
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchByKey(DOCTORMASTERVO);
        }

        public List<DOCTORMASTERVO> SearchDDL(DOCTORMASTERVO DOCTORMASTERVO)
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchDDL(DOCTORMASTERVO);
        }
        
        public DataTable SearchByCode_DS(string[] Code)
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchByCode_DS(Code);
        }
        
        public List<DOCTORMASTERVO> SearchByKeyTreatment(DOCTORMASTERVO DOCTORMASTERVO)
        {
            DADOCTORMASTER DADOCTORMASTER = (dbInfo == null ? new DADOCTORMASTER() : new DADOCTORMASTER(dbInfo));
            return DADOCTORMASTER.SearchByKeyTreatment(DOCTORMASTERVO);
        }

    }
}
