using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class PATIENTALLEGICVO
    {
        public string HN { get; set; }
        public string allegicname { get; set; }
        public string Reaction { get; set; }
        public string Remark { get; set; }
        public string Memo { get; set; }
        public string FromType { get; set; }

    }
}
