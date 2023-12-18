using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Info;
using System.Data;
using System.Net;
using Newtonsoft.Json;


namespace solution.EnquirePostMedicine
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected static DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string SearchMedicine(string storeCode , string textSearch, int startPage, int per_page)
        {

            VT_STOCK_MASTER VT_STOCKMASTER = new VT_STOCK_MASTER();
            var result = new BLVT_STOCK_MASTER(extConnDBInfo).SearchStockMaster(storeCode , textSearch, startPage, per_page);
            var json = JsonConvert.SerializeObject(result);
            return json;

        }


        [System.Web.Services.WebMethod]
        public static string GetStockMasterByKey(string stockCode,string store = "",string medicinePriceType = "1")
        {

            SETUPGROUPMETHODMEDICINE methodMed = new SETUPGROUPMETHODMEDICINE();
            if (!string.IsNullOrEmpty(stockCode))
            {

                VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(stockCode, store , medicinePriceType);
                methodMed.MedicineCode = VT_STOCK_MASTER.StockCode;
                methodMed.MedicineName_TH = VT_STOCK_MASTER.ThaiName;
                methodMed.MedicineName_EN = VT_STOCK_MASTER.EngName;
                methodMed.UnitCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.UnitCode;
                 //VT_STOCK_MASTER.UnitCode01.Trim();
                methodMed.QTY = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.Qty;
                methodMed.UnitPrice = VT_STOCK_MASTER.Price;
                methodMed.AMT = methodMed.UnitPrice * methodMed.QTY;
                methodMed.ChargeCode = VT_STOCK_MASTER.ChargeCode;

                methodMed.DoseTypeCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseType;
                methodMed.DoseQTY = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseQtyCode;
                methodMed.DoseUnitCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseUnitCode;
                methodMed.DoseCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseCode;

                methodMed.AUXLABEL1 = VT_STOCK_MASTER.AUXLABEL1;
                methodMed.AUXLABEL2 = VT_STOCK_MASTER.AUXLABEL2;
                methodMed.AUXLABEL3 = VT_STOCK_MASTER.AUXLABEL3;

                methodMed.STORE = VT_STOCK_MASTER.STORE;
            }
            var json = JsonConvert.SerializeObject(methodMed);
            return json;
        }


    }
}