using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORANESTHESIANURSESCHEDULEVO
    {
        public string ID { get; set; }
        public string NURSE { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> StartAnesthesiaDateTime { get; set; }
        public string Reamrk { get; set; }
        public string strStartAnesthesiaDateTime { get; set; }
    }
}
