using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;


namespace DAL
{
    class DAVT_VNMEDICINE_TEMP : DataAccess
    {
        public DAVT_VNMEDICINE_TEMP() { }
        public DAVT_VNMEDICINE_TEMP(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_VNMEDICINE_TEMP(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_VNMEDICINE_TEMP(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        
        internal List<VT_VNMEDICINE_TEMP> SearchByKey(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            List<VT_VNMEDICINE_TEMP> retValue = new List<VT_VNMEDICINE_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select m.*,ac.*  from [dbo].VT_IOR_VNMEDICINE m left join dbo.VT_ACTIVITYCODE ac on m.CHARGECODE = ac.Activity ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and VISITDATE = @VISITDATE");
                strQuery.Append(" and SUFFIX = @SUFFIX");
                strQuery.Append(" and CXLDATETIME IS NULL");
                strQuery.Append(" order by SUBSUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));


                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNMEDICINE_TEMP VT_VNMEDICINE = new VT_VNMEDICINE_TEMP();
                    VT_VNMEDICINE.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNMEDICINE.VN = query["VN"].ToString();
                    VT_VNMEDICINE.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNMEDICINE.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    //VT_VNMEDICINE.STOCKCODE = query["STOCKCODE"].ToString();
                    //VT_VNMEDICINE.StockInfo = !string.IsNullOrEmpty(VT_VNMEDICINE.STOCKCODE) ? new DAVT_STOCK_MASTER(this.DbInfo).GetStockMasterByKey(VT_VNMEDICINE.STOCKCODE) : new Info.VT_STOCK_MASTER();
                    //VT_VNMEDICINE.ThaiName = query["ThaiName"].ToString();
                    //VT_VNMEDICINE.EngName = query["EngName"].ToString();
                    VT_VNMEDICINE.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNMEDICINE.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNMEDICINE.UNITPRICE = ADOUtil.GetDoubleFromQuery(query["UNITPRICE"].ToString());
                    VT_VNMEDICINE.UNITCODE =  query["UNITCODE"].ToString();
                    VT_VNMEDICINE.UNITNAME = !string.IsNullOrEmpty(VT_VNMEDICINE.UNITCODE) ? new DAVT_UNIT(this.DbInfo).SearchByKey(VT_VNMEDICINE.UNITCODE).Name : string.Empty;
                    VT_VNMEDICINE.STORE = query["STORE"].ToString();
                    VT_VNMEDICINE.STORENAME = !string.IsNullOrEmpty(VT_VNMEDICINE.STORE) ? new DAVT_STORE(this.DbInfo).GetStoreByKey(VT_VNMEDICINE.STORE).StoreName  : string.Empty;
                    VT_VNMEDICINE.MEDICINECODE = query["MEDICINECODE"].ToString();
                    VT_VNMEDICINE.MEDICINE_THAINAME = query["MEDICINE_THAINAME"].ToString();
                    VT_VNMEDICINE.MEDICINE_ENGLISHNAME = query["MEDICINE_ENGLISHNAME"].ToString();
                    VT_VNMEDICINE.MEDICINE_THAINAME = query["MEDICINE_THAINAME"].ToString();
                    VT_VNMEDICINE.UnitCode01 = query["UnitCode01"].ToString();
                    VT_VNMEDICINE.UnitName01 = query["UnitName01"].ToString();
                    VT_VNMEDICINE.UnitCode02 = query["UnitCode02"].ToString();
                    VT_VNMEDICINE.UnitName02 = query["UnitName02"].ToString();
                    VT_VNMEDICINE.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNMEDICINE.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNMEDICINE.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNMEDICINE.TYPEOFCHARGE = ADOUtil.GetIntFromQuery(query["TYPEOFCHARGE"].ToString());
                    VT_VNMEDICINE.REMARK = string.Empty;
                    VT_VNMEDICINE.GROUPCODE = string.Empty;
                    VT_VNMEDICINE.ActivityName = query["ActivityName"].ToString();


                    VT_VNMEDICINE.DOSETYPE = query["DOSETYPE"].ToString();
                    VT_VNMEDICINE.DOSETYPEINFO = !string.IsNullOrEmpty(VT_VNMEDICINE.DOSETYPE) ? new DAVT_DOSETYPE(DbInfo).GetDoseTypeByKey(VT_VNMEDICINE.DOSETYPE) : new Info.DOSETYPE();
                    VT_VNMEDICINE.DOSECODE = query["DOSECODE"].ToString();
                    VT_VNMEDICINE.DOSECODEINFO = !string.IsNullOrEmpty(VT_VNMEDICINE.DOSECODE) ? new DAVT_DOSECODE(DbInfo).GetDoseCodeByKey(VT_VNMEDICINE.DOSECODE) : new Info.DOSECODE();
                    VT_VNMEDICINE.DOSEQTYCODE = query["DOSEQTYCODE"].ToString();
                    VT_VNMEDICINE.DOSEQTYINFO = !string.IsNullOrEmpty(VT_VNMEDICINE.DOSEQTYCODE) ? new DAVT_DOSEQTY(DbInfo).GetDoseQtyByKey(VT_VNMEDICINE.DOSEQTYCODE) : new Info.DOSEQTY();
                    VT_VNMEDICINE.DOSEUNITCODE = query["DOSEUNITCODE"].ToString();
                    VT_VNMEDICINE.DOSEUNITINFO = !string.IsNullOrEmpty(VT_VNMEDICINE.DOSEUNITCODE) ? new DAVT_DOSEUNIT(DbInfo).GetDoseUnitByKey(VT_VNMEDICINE.DOSEUNITCODE) : new Info.DOSEUNIT();
                    VT_VNMEDICINE.CHARGECODE = query["CHARGECODE"].ToString();
                    VT_VNMEDICINE.ACTIVITYCODEINFO = !string.IsNullOrEmpty(VT_VNMEDICINE.CHARGECODE) ? new DAVT_ACTIVITYCODE(DbInfo).GetActivityCodeByKey(VT_VNMEDICINE.CHARGECODE) : new Info.ACTIVITYCODE();

                    VT_VNMEDICINE.AUXLABEL1 = query["AUXLABEL1"].ToString();
                    VT_VNMEDICINE.AUXLABEL1INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL1) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL1) : new Info.DOSEAUX();
                    VT_VNMEDICINE.AUXLABEL2 = query["AUXLABEL2"].ToString();
                    VT_VNMEDICINE.AUXLABEL2INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL2) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL2) : new Info.DOSEAUX();
                    VT_VNMEDICINE.AUXLABEL3 = query["AUXLABEL3"].ToString();
                    VT_VNMEDICINE.AUXLABEL3INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL3) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL3) : new Info.DOSEAUX();
                    VT_VNMEDICINE.DOSEMEMO = query["DOSEMEMO"].ToString();

                    retValue.Add(VT_VNMEDICINE);
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


        internal VT_VNMEDICINE_TEMP GetVT_VNMEDICINEByKey(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            VT_VNMEDICINE_TEMP VT_VNMEDICINE = new VT_VNMEDICINE_TEMP();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  a.*  from [dbo].VT_IOR_VNMEDICINE a ");
                strQuery.Append(" where a.VN = @VN");
                strQuery.Append(" and a.VISITDATE = @VISITDATE");
                strQuery.Append(" and a.SUFFIX = @SUFFIX");
                strQuery.Append(" and a.SUBSUFFIX = @SUBSUFFIX");
                strQuery.Append(" and CXLDATETIME IS NULL");

                ConnectDB();
                
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUBSUFFIX, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_VNMEDICINE.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNMEDICINE.VN = query["VN"].ToString();
                    VT_VNMEDICINE.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNMEDICINE.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNMEDICINE.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNMEDICINE.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNMEDICINE.DOSETYPE = query["DOSETYPE"].ToString();
                    VT_VNMEDICINE.DOSEQTYCODE = query["DOSEQTYCODE"].ToString();
                    VT_VNMEDICINE.DOSEUNITCODE = query["DOSEUNITCODE"].ToString();
                    VT_VNMEDICINE.DOSECODE = query["DOSECODE"].ToString();
                    VT_VNMEDICINE.UNITPRICE = ADOUtil.GetDoubleFromQuery(query["UNITPRICE"].ToString());
                    VT_VNMEDICINE.UNITCODE = query["UNITCODE"].ToString();
                    VT_VNMEDICINE.UNITNAME = !string.IsNullOrEmpty(VT_VNMEDICINE.UNITCODE) ? new DAVT_UNIT(this.DbInfo).SearchByKey(VT_VNMEDICINE.UNITCODE).Name : string.Empty;
                    VT_VNMEDICINE.STORE = query["STORE"].ToString();
                    VT_VNMEDICINE.STORENAME = !string.IsNullOrEmpty(VT_VNMEDICINE.STORE) ? new DAVT_STORE(this.DbInfo).GetStoreByKey(VT_VNMEDICINE.STORE).StoreName : string.Empty;
                    VT_VNMEDICINE.MEDICINECODE = query["MEDICINECODE"].ToString();
                    VT_VNMEDICINE.MEDICINE_THAINAME = query["MEDICINE_THAINAME"].ToString();
                    VT_VNMEDICINE.MEDICINE_ENGLISHNAME = query["MEDICINE_ENGLISHNAME"].ToString();
                    VT_VNMEDICINE.MEDICINE_THAINAME = query["MEDICINE_THAINAME"].ToString();
                    VT_VNMEDICINE.UnitCode01 = query["UnitCode01"].ToString();
                    VT_VNMEDICINE.UnitName01 = query["UnitName01"].ToString();
                    VT_VNMEDICINE.UnitCode02 = query["UnitCode02"].ToString();
                    VT_VNMEDICINE.UnitName02 = query["UnitName02"].ToString();
                    VT_VNMEDICINE.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNMEDICINE.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNMEDICINE.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNMEDICINE.TYPEOFCHARGE = ADOUtil.GetIntFromQuery(query["TYPEOFCHARGE"].ToString());
                    VT_VNMEDICINE.REMARK = string.Empty;
                    VT_VNMEDICINE.GROUPCODE = string.Empty;
                    VT_VNMEDICINE.STORE = query["STORE"].ToString();
                    VT_VNMEDICINE.AUXLABEL1 = query["AUXLABEL1"].ToString();
                    VT_VNMEDICINE.AUXLABEL1INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL1) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL1) : new Info.DOSEAUX();
                    VT_VNMEDICINE.AUXLABEL2 = query["AUXLABEL2"].ToString();
                    VT_VNMEDICINE.AUXLABEL2INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL2) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL2) : new Info.DOSEAUX();
                    VT_VNMEDICINE.AUXLABEL3 = query["AUXLABEL3"].ToString();
                    VT_VNMEDICINE.AUXLABEL3INFO = !string.IsNullOrEmpty(VT_VNMEDICINE.AUXLABEL3) ? new DAVT_DOSEAUX(DbInfo).GetDoseAUXByKey(VT_VNMEDICINE.AUXLABEL3) : new Info.DOSEAUX();
                    VT_VNMEDICINE.DOSEMEMO = query["DOSEMEMO"].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
               
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_VNMEDICINE;
        }

        internal ReturnValue CXLVNMEDICINE(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[IOR_VNMEDICINE] SET ");
                sbQuery.Append(" CXLDATETIME = GETDATE()");
                sbQuery.Append(" ,CXLBYUSERCODE = @CXLBYUSERCODE");
                sbQuery.Append(" ,REVERSE = 1 ");
                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");
                sbQuery.Append(" AND SUBSUFFIX = @SUBSUFFIX");
                sbQuery.Append(" AND CXLDATETIME IS NULL");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("CXLBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.CXLBYUSERCODE)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUBSUFFIX, false)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue DELVNMEDICINE(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE [dbo].[IOR_VNMEDICINE] ");
                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : true);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal bool CheckDup(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[IOR_VNMEDICINE] ");
                strQuery.Append(" WHERE VISITDATE = @VISITDATE");
                strQuery.Append(" AND VN = @VN");
                strQuery.Append(" AND SUFFIX = @SUFFIX");
                strQuery.Append(" AND STOCKCODE = @MEDICINECODE");
                strQuery.Append(" AND CXLDATETIME IS NULL");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));
                parameter.Add(new IParameter("MEDICINECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.MEDICINECODE)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch
            {
                retVal = false;
            }
            return retVal;
        }


        internal ReturnValue Insert(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                sbInsert.Append("Declare @i int; ");

                sbInsert.Append("Select @i = coalesce(MAX(SUBSUFFIX), 0) + 1 FROM [dbo].[IOR_VNMEDICINE] ");
                sbInsert.Append("where VISITDATE = @VISITDATE and VN = @VN and SUFFIX = @SUFFIX ; ");
                sbInsert.Append(" INSERT INTO [dbo].[IOR_VNMEDICINE] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("VISITDATE");
                sbValue.Append("@VISITDATE");

                sbInsert.Append(", VN");
                sbValue.Append(",@VN");

                sbInsert.Append(", SUFFIX");
                sbValue.Append(",@SUFFIX");

                sbInsert.Append(", SUBSUFFIX" );
                sbValue.Append(",@i" );

                sbInsert.Append(", STOCKCODE");
                sbValue.Append(",@STOCKCODE");

                sbInsert.Append(", AMT");
                sbValue.Append(",@AMT");

                sbInsert.Append(", QTY");
                sbValue.Append(",@QTY");

                sbInsert.Append(", UNITPRICE");
                sbValue.Append(",@UNITPRICE");

                sbInsert.Append(", UNITCODE");
                sbValue.Append(",@UNITCODE");

                sbInsert.Append(", DOSETYPE");
                sbValue.Append(",@DOSETYPE");

                sbInsert.Append(", DOSEQTYCODE");
                sbValue.Append(",@DOSEQTYCODE");

                sbInsert.Append(", DOSEUNITCODE");
                sbValue.Append(",@DOSEUNITCODE");

                sbInsert.Append(", DOSECODE");
                sbValue.Append(",@DOSECODE");

                sbInsert.Append(", TYPEOFCHARGE");
                sbValue.Append(",@TYPEOFCHARGE");

                sbInsert.Append(", MAKEDATETIME");
                sbValue.Append(", GETDATE() ");

                sbInsert.Append(", ENTRYBYUSERCODE");
                sbValue.Append(", @ENTRYBYUSERCODE");
               

                sbInsert.Append(", REVERSE");
                sbValue.Append(",@REVERSE");

                sbInsert.Append(", STORE");
                sbValue.Append(",@STORE");

                sbInsert.Append(", PAIDFLAG");
                sbValue.Append(",@PAIDFLAG");

                sbInsert.Append(", CHARGECODE");
                sbValue.Append(",@CHARGECODE");

                sbInsert.Append(", AUXLABEL1");
                sbValue.Append(",@AUXLABEL1");

                sbInsert.Append(", AUXLABEL2");
                sbValue.Append(",@AUXLABEL2");

                sbInsert.Append(", AUXLABEL3");
                sbValue.Append(",@AUXLABEL3");

                sbInsert.Append(", DOSEMEMO");
                sbValue.Append(",@DOSEMEMO");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX,false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUBSUFFIX,false)));
                parameter.Add(new IParameter("STOCKCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.MEDICINECODE)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.QTY)));
                parameter.Add(new IParameter("UNITPRICE", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.UNITPRICE)));
                parameter.Add(new IParameter("UNITCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.UNITCODE)));
                parameter.Add(new IParameter("DOSETYPE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSETYPE)));
                parameter.Add(new IParameter("DOSEQTYCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEQTYCODE)));
                parameter.Add(new IParameter("DOSEUNITCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEUNITCODE)));
                parameter.Add(new IParameter("DOSECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSECODE)));
                parameter.Add(new IParameter("TYPEOFCHARGE", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.TYPEOFCHARGE, false)));
                parameter.Add(new IParameter("MAKEDATETIME", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.MAKEDATETIME)));
                parameter.Add(new IParameter("ENTRYBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.ENTRYBYUSERCODE)));
                parameter.Add(new IParameter("REVERSE", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.REVERSE, false)));
                parameter.Add(new IParameter("STORE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.STORE)));
                parameter.Add(new IParameter("PAIDFLAG", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.PAIDFLAG, false)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.CHARGECODE)));
                parameter.Add(new IParameter("AUXLABEL1", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL1)));
                parameter.Add(new IParameter("AUXLABEL2", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL2)));
                parameter.Add(new IParameter("AUXLABEL3", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL3)));
                parameter.Add(new IParameter("DOSEMEMO", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEMEMO)));
                command = GetCommand(sbInsert.ToString(), parameter);



                effected = GetExecuteNonQuery(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }

            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }


        internal ReturnValue UpdateVNMEDICINE(VT_VNMEDICINE_TEMP _VT_VNMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[IOR_VNMEDICINE] SET ");
                sbQuery.Append("  AMT = @AMT");
                sbQuery.Append(" ,QTY = @QTY");
                sbQuery.Append(" ,UNITPRICE = @UNITPRICE");
                sbQuery.Append(" ,UNITCODE = @UNITCODE");
                sbQuery.Append(" ,DOSETYPE = @DOSETYPE");
                sbQuery.Append(" ,DOSEQTYCODE = @DOSEQTYCODE");
                sbQuery.Append(" ,DOSEUNITCODE = @DOSEUNITCODE");
                sbQuery.Append(" ,DOSECODE = @DOSECODE");
                sbQuery.Append(" ,STORE = @STORE");
                sbQuery.Append(" ,TYPEOFCHARGE = @TYPEOFCHARGE");
                sbQuery.Append(" ,REVERSE = @REVERSE");
                sbQuery.Append(" ,AUXLABEL1 = @AUXLABEL1");
                sbQuery.Append(" ,AUXLABEL2 = @AUXLABEL2");
                sbQuery.Append(" ,AUXLABEL3 = @AUXLABEL3");
                sbQuery.Append(" ,DOSEMEMO = @DOSEMEMO");

                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");
                sbQuery.Append(" AND SUBSUFFIX = @SUBSUFFIX");
                sbQuery.Append(" AND STOCKCODE = @STOCKCODE");
                sbQuery.Append(" AND CXLDATETIME IS NULL");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMEDICINE.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.SUBSUFFIX, false)));
                parameter.Add(new IParameter("STOCKCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.MEDICINECODE)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.QTY)));
                parameter.Add(new IParameter("UNITPRICE", IDbType.Float, DBNullConvert.From(_VT_VNMEDICINE.UNITPRICE)));
                parameter.Add(new IParameter("UNITCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.UNITCODE)));
                parameter.Add(new IParameter("DOSETYPE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSETYPE)));
                parameter.Add(new IParameter("DOSEQTYCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEQTYCODE)));
                parameter.Add(new IParameter("DOSEUNITCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEUNITCODE)));
                parameter.Add(new IParameter("DOSECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSECODE)));
                parameter.Add(new IParameter("TYPEOFCHARGE", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.TYPEOFCHARGE, false)));
                parameter.Add(new IParameter("ENTRYBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.ENTRYBYUSERCODE)));
                parameter.Add(new IParameter("REVERSE", IDbType.Int, DBNullConvert.From(_VT_VNMEDICINE.REVERSE, false)));
                parameter.Add(new IParameter("STORE", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.STORE)));
                parameter.Add(new IParameter("AUXLABEL1", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL1)));
                parameter.Add(new IParameter("AUXLABEL2", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL2)));
                parameter.Add(new IParameter("AUXLABEL3", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.AUXLABEL3)));
                parameter.Add(new IParameter("DOSEMEMO", IDbType.VarChar, DBNullConvert.From(_VT_VNMEDICINE.DOSEMEMO)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }


        internal double GetTotalTreatmentPrice(VT_VNTREAT_TEMP _VT_VNTREAT, int statusType)
        {
            double retVal = default(double);
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT SUM(t.AMT * t.QTY) as Total ");
                strQuery.Append(" FROM [dbo].[IOR_VNTREAT]  t ");
                strQuery.Append(" LEFT JOIN VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" WHERE t.VISITDATE = @VISITDATE");
                strQuery.Append(" AND t.VN = @VN");
                strQuery.Append(" AND t.SUFFIX = @SUFFIX");
                strQuery.Append(" AND t.CXLDATETIME IS NULL");
                switch (statusType)
                {
                    case 0:
                        strQuery.Append(" AND tc.DF = 0 ");
                        break;
                    case 1:
                        strQuery.Append(" AND tc.DF = 1 ");
                        break;
                    default:
                        strQuery.Append(" AND tc.DF = tc.DF ");
                        break;
                }

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                retVal = Convert.ToDouble(command.ExecuteScalar());
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {

            }
            return retVal;
        }

    }
}
