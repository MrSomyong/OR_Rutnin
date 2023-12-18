using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORHEADER
    {
        DatabaseInfo dbInfo = null;
        public BLORHEADER() { }
        public BLORHEADER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORHEADERVO> SearchByKey(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByKey(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchPostORByKey(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchPostORByKey(ORHEADERVO);
        }
        
        public List<ORHEADERVO> SearchByORID(string orid)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByORID(orid);
        }
        public List<ORHEADERVO> SearchByRoom(string orroom, DateTime ordate)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByRoom(orroom, ordate);
        }

        public List<ORHEADERVO> SearchByRequestbyuser(string orroom, DateTime ordate)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByRequestbyuser(orroom, ordate);
        }

        public List<ORHEADERVO> SearchPrevOR(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchPrevOR(ORHEADERVO);
        }
        
        public List<ORHEADERVO> SearchByIPD(DateTime ordate)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByIPD(ordate);
        }

        public List<ORHEADERVO> SearchRoomType(DateTime ordate)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchRoomType(ordate);
        }        

        public List<ORHEADERVO> SearchBySurgeon(string surgeonid, DateTime ordate,string roomid)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchBySurgeon(surgeonid, ordate, roomid);
        }

        public List<ORHEADERVO> SearchBySurgeonTF(string surgeonid, DateTime ordate, string roomid)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchBySurgeonTF(surgeonid, ordate, roomid);
        }

        public List<ORHEADERVO> SearchByHN(string orid)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchByHN(orid);
        }

        public List<ORHEADERVO> SearchUnderPatient(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchUnderPatient(ORHEADERVO);
        }
        

        public bool SecrchCheckdup(string ORID)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SecrchCheckdup(ORID);
        }

        public ReturnValue CheckCountByAppointment(string AppointmentNo)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.CheckCountByAppointment(AppointmentNo);
        }

        public List<ORHEADERVO> SearchOperation(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchOperation(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchOP(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchOP(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchOPD(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchOPD(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchAnesthesiaType1(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchAnesthesiaType1(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchAnesthesiaType2(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchAnesthesiaType2(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchStatCase(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchStatCase(ORHEADERVO);
        }

        public List<ORHEADERVO> SearchStatWard(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchStatWard(ORHEADERVO);
        }
        public List<ORHEADERVO> SearchrptSurgery(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag ,string strHN)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptSurgery(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag, strHN);
        }
        public List<ORHEADERVO> SearchrptTop5(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptTop5(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag);
        }

        public List<ORHEADERVO> SearchrptSurgeryStatAth(DateTime ordateF, DateTime ordateT, string NurseType,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptSurgeryStatAth(ordateF, ordateT, NurseType, strCxlCheckFlag, strCxlConfirmFlag);
        }
        public List<ORHEADERVO> SearchrptSurgery48(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptSurgery48(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag);
        }

        public List<ORHEADERVO> SearchrptSurgeryProcedrue(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptSurgeryProcedrue(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag);
        }
        
        public List<ORHEADERVO> SearchrptSurgeryOrgan(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptSurgeryOrgan(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag);
        }

        public List<ORHEADERVO> SearchrptIndicatorProcedure(DateTime ordateF, DateTime ordateT,string strCxlCheckFlag, string strCxlConfirmFlag)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.SearchrptIndicatorProcedure(ordateF, ordateT, strCxlCheckFlag, strCxlConfirmFlag);
        }        

        public ReturnValue Insert(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.Insert(ORHEADERVO);
        }

        public ReturnValue Update(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.Update(ORHEADERVO);
        }
        
        public ReturnValue CancelApp(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.CancelApp(ORHEADERVO);
        }

        public ReturnValue CancelPostOR(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.CancelPostOR(ORHEADERVO);
        }

        public ReturnValue UpdateTimeTF(ORHEADERVO ORHEADERVO)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.UpdateTimeTF(ORHEADERVO);
        }        

        public ReturnValue Delete(string ORID)
        {
            DAORHEADER DAORHEADER = (dbInfo == null ? new DAORHEADER() : new DAORHEADER(dbInfo));
            return DAORHEADER.Delete(ORID);
        }
    }
}
