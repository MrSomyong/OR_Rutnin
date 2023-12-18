using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class VT_STOCK_MASTER
    {
        public string StockCode { get; set; }
        public string ThaiName { get; set; }
        public string EngName { get; set; }
        public string UnitCode01 { get; set; }
        public string UnitName01 { get; set; }
        public string UnitCode02 { get; set; }
        public string UnitName02 { get; set; }
        public double Price { get; set; }
        public string ChargeCode { get; set; }
        public string AUXLABEL1 { get; set; }
        public string AUXLABEL2 { get; set; }
        public string AUXLABEL3 { get; set; }
        public string MEMO { get; set; }
        public string STORE { get; set; }
        public string MedicinePriceType { get; set; }

        //Backup MedicineName
        //public string MedicineName
        //{
        //    get
        //    {
        //        return string.Format("[{2}-{0}] {1}", StockCode, !String.IsNullOrEmpty(ThaiName)? ThaiName : EngName, STOREINFO.StoreName);
        //    }
        //}

        public string MedicineName
        {
            get
            {
                return string.Format("[{0}] {1}", StockCode, !String.IsNullOrEmpty(ThaiName) ? ThaiName : EngName);
            }
        }
        public VT_STORE STOREINFO { get; set; }
        public VT_STOCK_LEARNDOSE STOCK_LEARNDOSEINFO { get; set; }


    }
}