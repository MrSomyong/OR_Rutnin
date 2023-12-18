using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_ORDERMETHOD
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string METHODNAME {
            get{
                return string.Format("[{0}] {1}", CODE, NAME);
            }
        }
    }
}
