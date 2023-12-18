﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class BLORANESTHESIANURSESCHEDULE
    {
        DatabaseInfo dbInfo = null;
        public BLORANESTHESIANURSESCHEDULE() { }
        public BLORANESTHESIANURSESCHEDULE(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<ORANESTHESIANURSESCHEDULEVO> SearchByKey(ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO)
        {
            DAORANESTHESIANURSESCHEDULE DAORANESTHESIANURSESCHEDULE  = (dbInfo == null ? new DAORANESTHESIANURSESCHEDULE() : new DAORANESTHESIANURSESCHEDULE(dbInfo));
            return DAORANESTHESIANURSESCHEDULE.SearchByKey(ORANESTHESIANURSESCHEDULEVO);
        }

        public DataTable SearchByDate_DS(DateTime StartAnesthesiaDateTime)
        {
            DAORANESTHESIANURSESCHEDULE DAORANESTHESIANURSESCHEDULE = (dbInfo == null ? new DAORANESTHESIANURSESCHEDULE() : new DAORANESTHESIANURSESCHEDULE(dbInfo));
            return DAORANESTHESIANURSESCHEDULE.SearchByDate_DS(StartAnesthesiaDateTime);
        }

        public ReturnValue Checkdup(ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO)
        {
            DAORANESTHESIANURSESCHEDULE DAORANESTHESIANURSESCHEDULE = (dbInfo == null ? new DAORANESTHESIANURSESCHEDULE() : new DAORANESTHESIANURSESCHEDULE(dbInfo));
            return DAORANESTHESIANURSESCHEDULE.Checkdup(ORANESTHESIANURSESCHEDULEVO);
        }
        public ReturnValue Insert(ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO)
        {
            DAORANESTHESIANURSESCHEDULE DAORANESTHESIANURSESCHEDULE = (dbInfo == null ? new DAORANESTHESIANURSESCHEDULE() : new DAORANESTHESIANURSESCHEDULE(dbInfo));
            return DAORANESTHESIANURSESCHEDULE.Insert(ORANESTHESIANURSESCHEDULEVO);
        }
        public ReturnValue Delete(string ID)
        {
            DAORANESTHESIANURSESCHEDULE DAORANESTHESIANURSESCHEDULE = (dbInfo == null ? new DAORANESTHESIANURSESCHEDULE() : new DAORANESTHESIANURSESCHEDULE(dbInfo));
            return DAORANESTHESIANURSESCHEDULE.Delete(ID);
        }
    }
}
