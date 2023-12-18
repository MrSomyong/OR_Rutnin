using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DAL.Info;

namespace DAL
{

    public class BLVT_STOCK_MASTER
    {
        DatabaseInfo dbInfo = null;
        public BLVT_STOCK_MASTER() { }
        public BLVT_STOCK_MASTER(DatabaseInfo dbInfo) { this.dbInfo = dbInfo; }

        public List<VT_STOCK_MASTER> SearchAll()
        {
            DAVT_STOCK_MASTER DAVT_STOCKMASTER = (dbInfo == null ? new DAVT_STOCK_MASTER() : new DAVT_STOCK_MASTER(dbInfo));
            return DAVT_STOCKMASTER.SearchAll();
        }

        public Tuple<List<VT_STOCK_MASTER>, int, int> SearchStockMasterSetup(string textSearch, int pStartIndex, int pageSize)
        {
            DAVT_STOCK_MASTER DAVT_STOCKMASTER = (dbInfo == null ? new DAVT_STOCK_MASTER() : new DAVT_STOCK_MASTER(dbInfo));
            return DAVT_STOCKMASTER.SearchStockMasterSetup(textSearch, pStartIndex, pageSize);
        }

        public Tuple<List<VT_STOCK_MASTER>, int, int> SearchStockMaster( string textSearch, int pStartIndex, int pageSize)
        {
            DAVT_STOCK_MASTER DAVT_STOCKMASTER = (dbInfo == null ? new DAVT_STOCK_MASTER() : new DAVT_STOCK_MASTER(dbInfo));
            return DAVT_STOCKMASTER.SearchStockMaster(string.Empty, textSearch, pStartIndex, pageSize);
        }
        public Tuple<List<VT_STOCK_MASTER>, int, int> SearchStockMaster(string storeCode, string textSearch, int pStartIndex, int pageSize)
        {
            DAVT_STOCK_MASTER DAVT_STOCKMASTER = (dbInfo == null ? new DAVT_STOCK_MASTER() : new DAVT_STOCK_MASTER(dbInfo));
            return DAVT_STOCKMASTER.SearchStockMaster(storeCode, textSearch, pStartIndex, pageSize);
        }
        public VT_STOCK_MASTER GetStockMasterByKey(string stockCode,string store = "", string MedicinePriceType="")
        {
            DAVT_STOCK_MASTER DAVT_STOCK_MASTER = (dbInfo == null ? new DAVT_STOCK_MASTER() : new DAVT_STOCK_MASTER(dbInfo));
            return DAVT_STOCK_MASTER.GetStockMasterByKey(stockCode, store, MedicinePriceType);
        }

        

    }
}
