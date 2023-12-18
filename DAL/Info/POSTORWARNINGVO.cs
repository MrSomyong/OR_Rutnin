using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class POSTORWARNINGVO
    {
        public string ORID { get; set; }
        public string ID { get; set; }
        public string Warning { get; set; }
        public Nullable<DateTime> CreateDateTime { get; set; }
        public Nullable<DateTime> UpdateDateTime { get; set; }
    }
}
