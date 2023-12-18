using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLSETUPLOGON
    {
        DatabaseInfo dbInfo = null;
        public BLSETUPLOGON() { }
        public BLSETUPLOGON(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<SETUPLOGONVO> SearchByKey(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.SearchByKey(SETUPLOGONVO);
        }

        public List<SETUPLOGONVO> SearchDDL(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.SearchDDL(SETUPLOGONVO);
        }

        public SETUPLOGONVO SearchLogin(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.SearchLogin(SETUPLOGONVO);
        }

        public ReturnValue Checkdup(string Username)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.Checkdup(Username);
        }        

        public SETUPLOGONVO CheckLogin(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.CheckLogin(SETUPLOGONVO);
        }

        public ReturnValue Insert(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.Insert(SETUPLOGONVO);
        }

        public ReturnValue Update(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.Update(SETUPLOGONVO);
        }

        public ReturnValue ChangePassword(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.ChangePassword(SETUPLOGONVO);
        }

        public ReturnValue Delete(string USER_ID)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.Delete(USER_ID);
        }

        public List<SETUPLOGONVO> SearchByKeyAccessMenuCode(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.SearchByKeyAccessMenuCode(SETUPLOGONVO);
        }

        public ReturnValue CheckdupAccessMenuCode(string AccessCode)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.CheckdupAccessMenuCode(AccessCode);
        }

        public ReturnValue InsertAccessMenuCode(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.InsertAccessMenuCode(SETUPLOGONVO);
        }

        public ReturnValue UpdateAccessMenuCode(SETUPLOGONVO SETUPLOGONVO)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.UpdateAccessMenuCode(SETUPLOGONVO);
        }

        public ReturnValue DeleteAccessMenuCode(string USER_ID)
        {
            DASETUPLOGON DASETUPLOGON = (dbInfo == null ? new DASETUPLOGON() : new DASETUPLOGON(dbInfo));
            return DASETUPLOGON.DeleteAccessMenuCode(USER_ID);
        }

    }
}
