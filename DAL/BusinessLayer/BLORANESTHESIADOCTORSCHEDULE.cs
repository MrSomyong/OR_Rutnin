﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORANESTHESIADOCTORSCHEDULE
    {
        DatabaseInfo dbInfo = null;
        public BLORANESTHESIADOCTORSCHEDULE() { }
        public BLORANESTHESIADOCTORSCHEDULE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORANESTHESIADOCTORSCHEDULEVO> SearchByKey(ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO)
        {
            DAORANESTHESIADOCTORSCHEDULE DAORANESTHESIADOCTORSCHEDULE  = (dbInfo == null ? new DAORANESTHESIADOCTORSCHEDULE() : new DAORANESTHESIADOCTORSCHEDULE(dbInfo));
            return DAORANESTHESIADOCTORSCHEDULE.SearchByKey(ORANESTHESIADOCTORSCHEDULEVO);
        }

        public DataTable SearchByDate_DS(DateTime StartAnesthesiaDateTime)
        {
            DAORANESTHESIADOCTORSCHEDULE DAORANESTHESIADOCTORSCHEDULE = (dbInfo == null ? new DAORANESTHESIADOCTORSCHEDULE() : new DAORANESTHESIADOCTORSCHEDULE(dbInfo));
            return DAORANESTHESIADOCTORSCHEDULE.SearchByDate_DS(StartAnesthesiaDateTime);
        }

        public ReturnValue Insert(ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO)
        {
            DAORANESTHESIADOCTORSCHEDULE DAORANESTHESIADOCTORSCHEDULE = (dbInfo == null ? new DAORANESTHESIADOCTORSCHEDULE() : new DAORANESTHESIADOCTORSCHEDULE(dbInfo));
            return DAORANESTHESIADOCTORSCHEDULE.Insert(ORANESTHESIADOCTORSCHEDULEVO);
        }
        public ReturnValue Delete(string ID)
        {
            DAORANESTHESIADOCTORSCHEDULE DAORANESTHESIADOCTORSCHEDULE = (dbInfo == null ? new DAORANESTHESIADOCTORSCHEDULE() : new DAORANESTHESIADOCTORSCHEDULE(dbInfo));
            return DAORANESTHESIADOCTORSCHEDULE.Delete(ID);
        }
    }
}