using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
using System.Data;
using System.Data.SqlClient;

namespace DAL 
{
    class DAVT_STOCK_MASTER : DataAccess
    {
        public DAVT_STOCK_MASTER() { }
        public DAVT_STOCK_MASTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_STOCK_MASTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_STOCK_MASTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_STOCK_MASTER> SearchAll()
        {
            List<VT_STOCK_MASTER> retValue = new List<VT_STOCK_MASTER>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 100 * from dbo.VT_STOCK_MASTER");
                strQuery.Append(" Order by case when ThaiName <> '' then ThaiName else EngName end ");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                command.CommandTimeout = 0;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_STOCK_MASTER stockMaster = new VT_STOCK_MASTER();
                    stockMaster.StockCode = query["StockCode"].ToString().Trim();
                    stockMaster.ThaiName = query["ThaiName"].ToString().Trim();
                    stockMaster.EngName = query["EngName"].ToString().Trim();
                    stockMaster.UnitCode01 = query["UnitCode01"].ToString().Trim();
                    stockMaster.UnitName01 = query["UnitName01"].ToString().Trim();
                    stockMaster.UnitCode02 = query["UnitCode02"].ToString().Trim();
                    stockMaster.UnitName02 = query["UnitName02"].ToString().Trim();
                    retValue.Add(stockMaster);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return retValue;
        }

        public Tuple<List<VT_STOCK_MASTER>, int, int> SearchStockMaster(string storeCode , string textSearch, int pStartIndex, int pageSize)
        {
            int limit = pageSize;
            int total = default(int);
            int totalPages = default(int);
            string sql = string.Empty;
            string querySearch = string.Empty;
            //int startIndex = (pageSize * (pStartIndex - 1) + 1);
            int startIndex = pStartIndex;
            List<VT_STOCK_MASTER> lists = new List<VT_STOCK_MASTER>();
            try
            {

                using (SqlConnection cn = new SqlConnection(DbInfo.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_IOR_SearchStockMaster", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pStartIndex", System.Data.SqlDbType.Int).Value = startIndex;
                    cmd.Parameters.Add("@pPageSize", SqlDbType.Int).Value = pageSize;
                    cmd.Parameters.Add("@pTextSearch", SqlDbType.VarChar).Value = textSearch;
                    cmd.Parameters.Add("@pStoreCode", SqlDbType.VarChar).Value = storeCode;
                    cmd.Parameters.Add("@pTotal", SqlDbType.Int).Value = 0;
                    cmd.Parameters["@pTotal"].Direction = ParameterDirection.Output;
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        VT_STOCK_MASTER stockMaster = new VT_STOCK_MASTER();
                        stockMaster.StockCode = reader["StockCode"].ToString().Trim();
                        stockMaster.ThaiName = reader["ThaiName"].ToString().Trim();
                        stockMaster.EngName = reader["EngName"].ToString().Trim();
                        stockMaster.UnitCode01 = reader["UnitCode01"].ToString().Trim();
                        stockMaster.UnitName01 = reader["UnitName01"].ToString().Trim();
                        stockMaster.UnitCode02 = reader["UnitCode02"].ToString().Trim();
                        stockMaster.UnitName02 = reader["UnitName02"].ToString().Trim();
                        stockMaster.ChargeCode = reader["CHARGECODE"].ToString().Trim();
                        stockMaster.STORE = reader["STORE"].ToString().Trim();

                        stockMaster.STOREINFO = new DAVT_STORE(DbInfo).GetStoreByKey(stockMaster.STORE); //DAVT_STOCK_LEARNDOSE(DbInfo).GetStockDoseByKey(VT_STOCK_MASTER.StockCode);

                        lists.Add(stockMaster);
                    }
                    reader.Close();
                    total = (int)cmd.Parameters["@pTotal"].Value; ;
                    totalPages = total % limit > 0 ? (total / limit) + 1 : total / limit;
                    
                }

               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            return new Tuple<List<VT_STOCK_MASTER>, int, int>(lists, total, totalPages);
        }

        public Tuple<List<VT_STOCK_MASTER>, int, int> SearchStockMasterSetup(string textSearch, int pStartIndex, int pageSize)
        {
            int limit = pageSize;
            int total = default(int);
            int totalPages = default(int);
            string sql = string.Empty;
            string querySearch = string.Empty;
            int startIndex = pStartIndex;
            List<VT_STOCK_MASTER> lists = new List<VT_STOCK_MASTER>();
            try
            {

                using (SqlConnection cn = new SqlConnection(DbInfo.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_IOR_SearchStockMasterSetup", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pStartIndex", System.Data.SqlDbType.Int).Value = startIndex;
                    cmd.Parameters.Add("@pPageSize", SqlDbType.Int).Value = pageSize;
                    cmd.Parameters.Add("@pTextSearch", SqlDbType.VarChar).Value = textSearch;
                    cmd.Parameters.Add("@pTotal", SqlDbType.Int).Value = 0;
                    cmd.Parameters["@pTotal"].Direction = ParameterDirection.Output;
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        VT_STOCK_MASTER stockMaster = new VT_STOCK_MASTER();
                        stockMaster.StockCode = reader["StockCode"].ToString().Trim();
                        stockMaster.ThaiName = reader["ThaiName"].ToString().Trim();
                        stockMaster.EngName = reader["EngName"].ToString().Trim();
                        stockMaster.UnitCode01 = reader["UnitCode01"].ToString().Trim();
                        stockMaster.UnitName01 = reader["UnitName01"].ToString().Trim();
                        stockMaster.UnitCode02 = reader["UnitCode02"].ToString().Trim();
                        stockMaster.UnitName02 = reader["UnitName02"].ToString().Trim();
                        stockMaster.ChargeCode = reader["CHARGECODE"].ToString().Trim();
                        stockMaster.STORE = reader["STORE"].ToString().Trim();

                        //stockMaster.STOREINFO = new DAVT_STORE(DbInfo).GetStoreByKey(stockMaster.STORE); //DAVT_STOCK_LEARNDOSE(DbInfo).GetStockDoseByKey(VT_STOCK_MASTER.StockCode);

                        lists.Add(stockMaster);
                    }
                    reader.Close();
                    total = (int)cmd.Parameters["@pTotal"].Value; ;
                    totalPages = total % limit > 0 ? (total / limit) + 1 : total / limit;

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return new Tuple<List<VT_STOCK_MASTER>, int, int>(lists, total, totalPages);
        }

        internal VT_STOCK_MASTER GetStockMasterByKey(string stockCode, string store, string MedicinePriceType)
        { 
            VT_STOCK_MASTER VT_STOCK_MASTER = new VT_STOCK_MASTER();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from dbo.VT_STOCK_MASTER ");
                strQuery.Append(" where StockCode = @StockCode and STORE = (case when @StoreCode = '' then [STORE] else @StoreCode end) ");
               

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("StockCode", IDbType.VarChar, stockCode));
                parameter.Add(new IParameter("StoreCode", IDbType.VarChar, store));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    VT_STOCK_MASTER.StockCode = query["StockCode"].ToString().Trim();
                    VT_STOCK_MASTER.ThaiName = query["ThaiName"].ToString().Trim();
                    VT_STOCK_MASTER.EngName = query["EngName"].ToString().Trim();
                    VT_STOCK_MASTER.UnitCode01 = query["UnitCode01"].ToString().Trim();
                    VT_STOCK_MASTER.UnitName01 = query["UnitName01"].ToString().Trim();
                    VT_STOCK_MASTER.UnitCode02 = query["UnitCode02"].ToString().Trim();
                    VT_STOCK_MASTER.UnitName02 = query["UnitName02"].ToString().Trim();
                    if (MedicinePriceType == "3")
                    {
                        VT_STOCK_MASTER.Price = ADOUtil.GetDoubleFromQuery(query["PriceInter"].ToString());
                    }
                    else
                    {
                        VT_STOCK_MASTER.Price = ADOUtil.GetDoubleFromQuery(query["Price"].ToString());
                    }
                    VT_STOCK_MASTER.ChargeCode = query["CHARGECODE"].ToString().Trim(); 
                    VT_STOCK_MASTER.AUXLABEL1 = query["AUXLABEL1"].ToString().Trim();
                    VT_STOCK_MASTER.AUXLABEL2 = query["AUXLABEL2"].ToString().Trim();
                    VT_STOCK_MASTER.AUXLABEL3 = query["AUXLABEL3"].ToString().Trim();
                    VT_STOCK_MASTER.MEMO = query["MEMO"].ToString().Trim();
                    VT_STOCK_MASTER.STORE = query["STORE"].ToString().Trim();

                    VT_STOCK_MASTER.STOREINFO = new DAVT_STORE(DbInfo).GetStoreByKey(VT_STOCK_MASTER.STORE);
                    VT_STOCK_MASTER.STOCK_LEARNDOSEINFO = new DAVT_STOCK_LEARNDOSE(DbInfo).GetStockDoseByKey(VT_STOCK_MASTER.StockCode);
                    
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
               throw exc;
            }
            return VT_STOCK_MASTER;
        }       

    }
}
