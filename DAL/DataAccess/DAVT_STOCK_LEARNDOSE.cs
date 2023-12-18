using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
using System.Data;
using System.Data.SqlClient;

namespace DAL 
{
    class DAVT_STOCK_LEARNDOSE : DataAccess
    {
        public DAVT_STOCK_LEARNDOSE() { }
        public DAVT_STOCK_LEARNDOSE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_STOCK_LEARNDOSE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_STOCK_LEARNDOSE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }




        internal VT_STOCK_LEARNDOSE GetStockDoseByKey(string stockCode)
        {
            VT_STOCK_LEARNDOSE VT_STOCK_LEARNDOSE = new VT_STOCK_LEARNDOSE();
            Boolean cLearn = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from dbo.VT_STOCK_LEARNDOSE ");
                strQuery.Append(" where StockCode = @StockCode ");
                strQuery.Append(" order by BinTaken desc");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("StockCode", IDbType.VarChar, DBNullConvert.From(stockCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    cLearn = true;
                    VT_STOCK_LEARNDOSE.StockCode = query["StockCode"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.DoseType = query["DoseType"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.DoseCode = query["DoseCode"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.DoseQtyCode = query["DoseQtyCode"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.DoseUnitCode = query["DoseUnitCode"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.NoDayDose = ADOUtil.GetIntFromQuery(query["NoDayDose"].ToString());
                    VT_STOCK_LEARNDOSE.UnitCode = query["UnitCode"].ToString().Trim();
                    VT_STOCK_LEARNDOSE.Qty = ADOUtil.GetDoubleFromQuery(query["qty"].ToString());
                    VT_STOCK_LEARNDOSE.BinTaken = ADOUtil.GetIntFromQuery(query["BinTaken"].ToString());
                    VT_STOCK_LEARNDOSE.MakeDateTime =  ADOUtil.GetDateFromQuery(query["MakeDateTime"].ToString());
                    VT_STOCK_LEARNDOSE.LastUpdateDateTime = ADOUtil.GetDateFromQuery(query["LastUpdateDateTime"].ToString());

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
               throw exc;
            }

            if (cLearn == false)
            {

                try
                {
                    StringBuilder strQuery = new StringBuilder();
                    strQuery.Append(" select top 1 * from dbo.VT_STOCK_MASTER_SETUP ");
                    strQuery.Append(" where StockCode = @StockCode ");
                    //strQuery.Append(" order by BinTaken desc");

                    ConnectDB();
                    List<IParameter> parameter = new List<IParameter>();
                    parameter.Add(new IParameter("StockCode", IDbType.VarChar, DBNullConvert.From(stockCode)));

                    command = GetCommand(strQuery.ToString(), parameter);
                    query = GetExecuteReader(command);
                    if (query.Read())
                    {
                        cLearn = true;
                        VT_STOCK_LEARNDOSE.StockCode = query["StockCode"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.DoseType = query["DoseType"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.DoseCode = query["DoseCode"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.DoseQtyCode = query["DoseQtyCode"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.DoseUnitCode = query["DoseUnitCode"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.NoDayDose = ADOUtil.GetIntFromQuery(query["NoDayDose"].ToString());
                        VT_STOCK_LEARNDOSE.UnitCode = query["UnitCode01"].ToString().Trim();
                        //VT_STOCK_LEARNDOSE.Qty = ADOUtil.GetDoubleFromQuery(query["qty"].ToString());
                        //VT_STOCK_LEARNDOSE.BinTaken = ADOUtil.GetIntFromQuery(query["BinTaken"].ToString());
                        //VT_STOCK_LEARNDOSE.MakeDateTime = ADOUtil.GetDateFromQuery(query["MakeDateTime"].ToString());
                        //VT_STOCK_LEARNDOSE.LastUpdateDateTime = ADOUtil.GetDateFromQuery(query["LastUpdateDateTime"].ToString());

                    }
                    query.Close();
                    command.Dispose();
                    DisconnectDB();
                }
                catch (Exception exc)
                {
                    throw exc;
                }

            }

            return VT_STOCK_LEARNDOSE;
        }
    }
}
