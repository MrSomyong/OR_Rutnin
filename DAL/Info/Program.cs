using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class Program
    {
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDesc { get; set; }
        public Nullable<bool> Active { get; set; }
        public string status { get; set; } // temp
        public Nullable<DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
       
    }
    [Serializable]
    public class PermissionCls : Program
    {
        public bool CanView { get; set; }
        public bool CanInsert { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public bool CanProcess { get; set; }
        public bool CanPrint { get; set; }
    }
}
