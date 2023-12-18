using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class HNUnderlyVO
    {
        public string HN { get; set; }
        public string Underlyingtext { get; set; }
        public Nullable<DateTime> CreateDateTime { get; set; }
        public string CreateUser { get; set; }
        public Nullable<DateTime> UpdateDateTime { get; set; }
        public string UpdateUser { get; set; }

    }
}
