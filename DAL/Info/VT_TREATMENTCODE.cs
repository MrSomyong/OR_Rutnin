using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class VT_TREATMENTCODE
    {
        public string CODE { get; set; }
        public string Name { get; set; }
        public double StdPrice1 { get; set; }
        public double StdPrice2 { get; set; }
        public double StdPrice3 { get; set; }
        public double StdPrice4 { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Activity { get; set; }
        public string ActivityName { get; set; }
        public int TreatmentStyle { get; set; }
        public int IsOff { get; set; }
        public string TreatmentCategory { get; set; }
        public int DF { get; set; }
        public string TREATMENTNAME {
            get {
                return string.Format("[{0}] {1}", CODE, Name);
            }
        }
        public int FixRate { get; set; }
        private double stdPrice = 0;
        public double StdPrice
        { 
            get {
                if (stdPrice == 0)
                {
                    switch (FixRate)
                    {
                        case 1:
                            stdPrice = StdPrice1;
                            break;
                        case 2:
                            stdPrice = StdPrice2;
                            break;
                        case 3:
                            stdPrice = StdPrice3;
                            break;
                        case 4:
                            stdPrice = StdPrice4;
                            break;
                        default:
                            stdPrice = StdPrice1;
                            break;
                    }
                }
                return stdPrice;
            }
            set{
                stdPrice = value;
            }
        }
        public int ZeroPrice { get; set; }
        public int TimeType { get; set; }
        public double Time01 { get; set; }
        public double Time02 { get; set; }
        public double Time03 { get; set; }
        public double Time04 { get; set; }
        public double Time05 { get; set; }
        public double Time06 { get; set; }
    }
}
