using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BLHNUnderly
    {
        DatabaseInfo dbInfo = null;
        public BLHNUnderly() { }
        public BLHNUnderly(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<HNUnderlyVO> SearchByHN(HNUnderlyVO HNUnderlyVO)
        {
            DAHNUnderly DAHNUnderly = (dbInfo == null ? new DAHNUnderly() : new DAHNUnderly(dbInfo));
            return DAHNUnderly.SearchByHN(HNUnderlyVO);
        }
        
        public ReturnValue Insert(HNUnderlyVO HNUnderlyVO)
        {
            DAHNUnderly DAHNUnderly = (dbInfo == null ? new DAHNUnderly() : new DAHNUnderly(dbInfo));
            return DAHNUnderly.Insert(HNUnderlyVO);
        }

        public ReturnValue Update(HNUnderlyVO HNUnderlyVO)
        {
            DAHNUnderly DAHNUnderly = (dbInfo == null ? new DAHNUnderly() : new DAHNUnderly(dbInfo));
            return DAHNUnderly.Update(HNUnderlyVO);
        }  
        
    }
}
