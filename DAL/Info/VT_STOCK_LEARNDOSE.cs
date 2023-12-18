using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class VT_STOCK_LEARNDOSE
    {

        public string StockCode { get; set; }
        public string DoseType { get; set; }
        public string DoseCode { get; set; }
        public string DoseQtyCode { get; set; }
        public string DoseUnitCode { get; set; }
        public int NoDayDose { get; set; }
        public string UnitCode { get; set; }
        private double qty = 0;
        public double Qty {
            get
            {
                return  qty == 0 ? 1 : qty; 
            }
            set
            {
                qty = value;
            }
        }
        public int BinTaken { get; set; }
        public DateTime? MakeDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}