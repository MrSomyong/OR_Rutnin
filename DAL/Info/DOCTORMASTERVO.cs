using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class DOCTORMASTERVO
    {
        public string DOCTOR { get; set; }
        public string DoctorName { get; set; }
        public string EDUCATIONSTANDARD { get; set; }
        public string CHEQUECLIENTNAME { get; set; }
        public string DoctorType { get; set; }
    }
}
