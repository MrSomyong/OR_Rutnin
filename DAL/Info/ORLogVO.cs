using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORLogVO
    {
        public string ID { get; set; }
        public string ORID { get; set; }
        public string HN { get; set; }
        public string PatientName { get; set; }
        public string Detail { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> UpdateDateF { get; set; }
        public Nullable<DateTime> UpdateDateT { get; set; }
        public string UpdateBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UpdateName { get; set; }
    }
}
