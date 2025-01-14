﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLSETUPGROUPMETHODCOMPUTER
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPGROUPMETHODCOMPUTER() { }
        public BLSETUPGROUPMETHODCOMPUTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPGROUPMETHODCOMPUTER> GetSETUPGROUPMETHODCOMPUTERByKey(int groupMethodID)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.GetSETUPGROUPMETHODCOMPUTERByKey(groupMethodID);
        }

        public List<SETUPGROUPMETHODCOMPUTER> GetSETUPGROUPMETHODCOMPUTERByGroupMethodCode(string groupMethodCode)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.GetSETUPGROUPMETHODCOMPUTERByGroupMethodCode(groupMethodCode);
        }

        public bool CheckDup(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            DASETUPGROUPMETHODCOMPUTER _DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return _DASETUPGROUPMETHODCOMPUTER.CheckDup(_SETUPGROUPMETHODCOMPUTER);
        }

        public ReturnValue Insert(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.Insert(_SETUPGROUPMETHODCOMPUTER);
        }

        public ReturnValue Update(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.Update(_SETUPGROUPMETHODCOMPUTER);
        }

        public ReturnValue InActive(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.InActiveSETUPGROUPMETHODCOMPUTER(_SETUPGROUPMETHODCOMPUTER);
        }
        public SETUPGROUPMETHODCOMPUTER GetSETUPGROUPMETHODCOMPUTERByKey(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            DASETUPGROUPMETHODCOMPUTER DASETUPGROUPMETHODCOMPUTER = (dbInfo == null ? new DASETUPGROUPMETHODCOMPUTER() : new DASETUPGROUPMETHODCOMPUTER(dbInfo));
            return DASETUPGROUPMETHODCOMPUTER.GetSETUPGROUPMETHODCOMPUTERByKey(_SETUPGROUPMETHODCOMPUTER);
        }
        

    }
}
