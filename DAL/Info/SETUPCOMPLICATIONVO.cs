using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPCOMPLICATIONVO
    {
        public string ID { get; set; }
        public string ComplicationHeader { get; set; }
        public string ComplicationDetail { get; set; }
        public Nullable<DateTime> CreateDateTime { get; set; }
        public Nullable<DateTime> UpdateDateTime { get; set; }
    }
}
