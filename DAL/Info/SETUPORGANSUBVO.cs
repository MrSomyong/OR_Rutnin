using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPORGANSUBVO
    {
        public string MainCode { get; set; }
        public string Name { get; set; }
        public string SubCode { get; set; }
        public string SubName { get; set; }
        public string SubRemark { get; set; }
    }
}
