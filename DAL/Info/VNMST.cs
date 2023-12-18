using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VNMST
    {
        public DateTime VISITDATE { get; set; }
        public string VN { get; set; }
        public string HN { get; set; }
        public DateTime? VISITINDATETIME { get; set; }
        public DateTime? VISITOUTDATETIME { get; set; }
    }
}
