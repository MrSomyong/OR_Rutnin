﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLSETUPGROUPMETHODDOCTOR
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPGROUPMETHODDOCTOR() { }
        public BLSETUPGROUPMETHODDOCTOR(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPGROUPMETHODDOCTOR> GetSETUPGROUPMETHODDOCTORByKey(int groupMethodID)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.GetSETUPGROUPMETHODDOCTORByKey(groupMethodID);
        }

        public List<SETUPGROUPMETHODDOCTOR> GetSETUPGROUPMETHODDOCTORByGroupMethodCode(string groupMethodCode)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.GetSETUPGROUPMETHODDOCTORByGroupMethodCode(groupMethodCode);
        }

        public bool CheckDup(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            DASETUPGROUPMETHODDOCTOR _DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return _DASETUPGROUPMETHODDOCTOR.CheckDup(_SETUPGROUPMETHODDOCTOR);
        }

        public ReturnValue Insert(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.Insert(_SETUPGROUPMETHODDOCTOR);
        }

        public ReturnValue Update(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.Update(_SETUPGROUPMETHODDOCTOR);
        }

        public ReturnValue InActive(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.InActiveSETUPGROUPMETHODDOCTOR(_SETUPGROUPMETHODDOCTOR);
        }
        public SETUPGROUPMETHODDOCTOR GetSETUPGROUPMETHODDOCTORByKey(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            DASETUPGROUPMETHODDOCTOR DASETUPGROUPMETHODDOCTOR = (dbInfo == null ? new DASETUPGROUPMETHODDOCTOR() : new DASETUPGROUPMETHODDOCTOR(dbInfo));
            return DASETUPGROUPMETHODDOCTOR.GetSETUPGROUPMETHODDOCTORByKey(_SETUPGROUPMETHODDOCTOR);
        }
        

    }
}
