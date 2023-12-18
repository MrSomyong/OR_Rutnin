using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    [Serializable]
    public class SETUPGROUPMETHOD
    {
        public int GroupMethodID { get; set; }
        public string GroupMethodCode{get; set;}
        public string GroupMethodName{get; set;}
        public bool IsActive { get; set; }
        public string NameInfo
        {
            get
            {
                return string.Format("[{0}] {1}", GroupMethodCode, GroupMethodName);
            }
        }
    }
}
