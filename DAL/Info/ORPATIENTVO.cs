using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORPATIENTVO
    {
        public string HN { get; set; }



        public string PatientName { get; set; }
        public string Gender { get; set; }
        public Nullable<DateTime> BirthDateTime { get; set; }
        public string IDCARD { get; set; }
        public string Nationality { get; set; }
        public string Age { get; set; }

        public string Initial { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HNName { get; set; }
        
        //public string ageyear { get; set; }
        //public string agemonth { get; set; }
        public string Sex { get; set; }
        public string Ref { get; set; }

        public string PictureFileName { get; set; }
        public string PatientType { get; set; }


    }
}
