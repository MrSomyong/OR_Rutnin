using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class VT_DEFAULTSTORE
    {
        public string StockCode { get; set; }
        public string ThaiName { get; set; }
        public string EngName { get; set; }
        public string UnitCode01 { get; set; }
        public string UnitName01 { get; set; }
        public string UnitCode02 { get; set; }
        public string UnitName02 { get; set; }

        public string MedicineName
        {
            get
            {
                return string.Format("[{0}] {1}", StockCode, !String.IsNullOrEmpty(ThaiName)? ThaiName : EngName);
            }
        }
    }
}