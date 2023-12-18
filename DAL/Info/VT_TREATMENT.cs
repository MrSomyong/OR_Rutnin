using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_TREATMENT
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string TREATMENTNAME {
            get{
                return string.Format("[{0}] {1}", CODE, NAME);
            }
        }
    }
}
