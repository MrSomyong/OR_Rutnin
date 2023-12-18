using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL
{
    class DASETUPGROUPMETHODMEDICINE : DataAccess
    {
        public DASETUPGROUPMETHODMEDICINE() { }
        public DASETUPGROUPMETHODMEDICINE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHODMEDICINE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHODMEDICINE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal List<SETUPGROUPMETHODMEDICINE> GetSETUPGROUPMETHODMEDICINEListByKey(int groupMethodID)
        {
            List<SETUPGROUPMETHODMEDICINE> retValue = new List<SETUPGROUPMETHODMEDICINE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select A.*,b.[Name] as UnitName from [dbo].[SETUPGROUPMETHODMEDICINE] A left join dbo.VT_UNIT B ON(a.unitcode = b.[code])");
                strQuery.Append(" where A.IsActive = 1 and  A.GroupMethodID = @GroupMethodID");
                strQuery.Append(" Order by A.MedicineCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(groupMethodID, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                    SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    SETUPGROUPMETHODMEDICINE.MedicineCode = query["MedicineCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_TH = query["MedicineName_TH"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_EN = query["MedicineName_EN"].ToString();
                    SETUPGROUPMETHODMEDICINE.UnitName = query["UnitName"].ToString();
                    SETUPGROUPMETHODMEDICINE.AutoTick = (bool)query["AutoTick"];
                    retValue.Add(SETUPGROUPMETHODMEDICINE);
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

        internal List<SETUPGROUPMETHODMEDICINE> GetSETUPGROUPMETHODMEDICINEListByGroupMethodCode(string groupMethodCode)
        {
            List<SETUPGROUPMETHODMEDICINE> retValue = new List<SETUPGROUPMETHODMEDICINE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* , b.GroupMethodCode,C.[Name] as UnitName from [dbo].[SETUPGROUPMETHODMEDICINE] a left join SETUPGROUPMETHOD b on a.GroupMethodID = b.GroupMethodID ");
                strQuery.Append(" left join dbo.VT_UNIT C ON(a.unitcode = C.[code])");
                strQuery.Append(" where a.IsActive = 1 and  b.GroupMethodCode = @groupMethodCode");
                strQuery.Append(" Order by a.MedicineCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                    SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    SETUPGROUPMETHODMEDICINE.GroupMethodCode = query["GroupMethodCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineCode = query["MedicineCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_TH = query["MedicineName_TH"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_EN = query["MedicineName_EN"].ToString();
                    SETUPGROUPMETHODMEDICINE.UnitName = query["UnitName"].ToString();
                    SETUPGROUPMETHODMEDICINE.AutoTick = (bool)query["AutoTick"];
                    retValue.Add(SETUPGROUPMETHODMEDICINE);
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
        internal bool CheckDup(SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPGROUPMETHODMEDICINE] ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND MedicineCode = @MedicineCode");
                strQuery.Append(" AND IsActive = 1");
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.GroupMethodID, false)));
                parameter.Add(new IParameter("MedicineCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.MedicineCode)));
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

        internal ReturnValue Insert(SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append(" INSERT INTO [dbo].[SETUPGROUPMETHODMEDICINE] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodID");
                sbValue.Append("@GroupMethodID");

                sbInsert.Append(", MedicineCode");
                sbValue.Append(",@MedicineCode");

                sbInsert.Append(", MedicineName_TH");
                sbValue.Append(",@MedicineName_TH");

                sbInsert.Append(", MedicineName_EN");
                sbValue.Append(",@MedicineName_EN");

                sbInsert.Append(", QTY");
                sbValue.Append(",@QTY");

                sbInsert.Append(", AMT");
                sbValue.Append(",@AMT");

                sbInsert.Append(", UnitPrice");
                sbValue.Append(",@UnitPrice");

                sbInsert.Append(", DoseQTY");
                sbValue.Append(",@DoseQTY");

                sbInsert.Append(", DoseTypeCode");
                sbValue.Append(",@DoseTypeCode");

                sbInsert.Append(", UnitCode");
                sbValue.Append(",@UnitCode");

                sbInsert.Append(", DoseUnitCode");
                sbValue.Append(",@DoseUnitCode");

                sbInsert.Append(", DoseCode");
                sbValue.Append(",@DoseCode");

                sbInsert.Append(", AUXLABEL1");
                sbValue.Append(",@AUXLABEL1");

                sbInsert.Append(", AUXLABEL2");
                sbValue.Append(",@AUXLABEL2");

                sbInsert.Append(", AUXLABEL3");
                sbValue.Append(",@AUXLABEL3");

                sbInsert.Append(", Remark");
                sbValue.Append(",@Remark");

                sbInsert.Append(", AutoTick");
                sbValue.Append(",@AutoTick");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.GroupMethodID, false)));
                parameter.Add(new IParameter("MedicineCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.MedicineCode)));
                parameter.Add(new IParameter("MedicineName_TH", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.MedicineName_TH)));
                parameter.Add(new IParameter("MedicineName_EN", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.MedicineName_EN)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.QTY)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.AMT)));
                parameter.Add(new IParameter("UnitPrice", IDbType.Float, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.UnitPrice)));
                parameter.Add(new IParameter("DoseQTY", IDbType.Float, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.DoseQTY)));
                parameter.Add(new IParameter("DoseTypeCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.DoseTypeCode)));
                parameter.Add(new IParameter("UnitCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.UnitCode)));
                parameter.Add(new IParameter("DoseUnitCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.DoseUnitCode)));
                parameter.Add(new IParameter("DoseCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.DoseCode)));
                parameter.Add(new IParameter("AUXLABEL1", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.AUXLABEL1)));
                parameter.Add(new IParameter("AUXLABEL2", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.AUXLABEL2)));
                parameter.Add(new IParameter("AUXLABEL3", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.AUXLABEL3)));
                parameter.Add(new IParameter("Remark", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.Remark)));
                parameter.Add(new IParameter("AutoTick", IDbType.Bit, SETUPGROUPMETHODMEDICINE.AutoTick));
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


        internal ReturnValue InActiveSETUPGROUPMETHODMEDICINE(SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODMEDICINE] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND MedicineCode = @MedicineCode");
                sbQuery.Append(" AND IsActive = 1");
                

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.GroupMethodID, false)));
                parameter.Add(new IParameter("MedicineCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHODMEDICINE.MedicineCode)));
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

        internal SETUPGROUPMETHODMEDICINE GetSETUPGROUPMETHODMEDICINEByKey(SETUPGROUPMETHODMEDICINE _SETUPGROUPMETHODMEDICINE)
        {
            SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  m.*  from [dbo].[SETUPGROUPMETHODMEDICINE] m ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND MedicineCode = @MedicineCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.GroupMethodID, false)));
                parameter.Add(new IParameter("MedicineCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.MedicineCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    SETUPGROUPMETHODMEDICINE.GroupMethodID = ADOUtil.GetIntFromQuery(query["GroupMethodID"].ToString());
                    SETUPGROUPMETHODMEDICINE.MedicineCode = query["MedicineCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_TH = query["MedicineName_TH"].ToString();
                    SETUPGROUPMETHODMEDICINE.MedicineName_EN = query["MedicineName_EN"].ToString();
                    SETUPGROUPMETHODMEDICINE.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    SETUPGROUPMETHODMEDICINE.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    SETUPGROUPMETHODMEDICINE.UnitPrice = ADOUtil.GetDoubleFromQuery(query["UnitPrice"].ToString());
                    SETUPGROUPMETHODMEDICINE.DoseQTY = query["DoseQTY"].ToString();
                    SETUPGROUPMETHODMEDICINE.DoseTypeCode = query["DoseTypeCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.UnitCode = query["UnitCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.DoseUnitCode = query["DoseUnitCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.DoseCode = query["DoseCode"].ToString();
                    SETUPGROUPMETHODMEDICINE.AUXLABEL1 = query["AUXLABEL1"].ToString();
                    SETUPGROUPMETHODMEDICINE.AUXLABEL2 = query["AUXLABEL2"].ToString();
                    SETUPGROUPMETHODMEDICINE.AUXLABEL3 = query["AUXLABEL3"].ToString();
                    SETUPGROUPMETHODMEDICINE.Remark = query["Remark"].ToString();
                    SETUPGROUPMETHODMEDICINE.AutoTick = (bool)query["AutoTick"];
                    

                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHODMEDICINE;
        }

        internal ReturnValue Update(SETUPGROUPMETHODMEDICINE _SETUPGROUPMETHODMEDICINE)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODMEDICINE] SET ");
                sbQuery.Append("  MedicineName_TH = @MedicineName_TH");
                sbQuery.Append(" ,MedicineName_EN = @MedicineName_EN");
                sbQuery.Append(" ,QTY = @QTY");
                sbQuery.Append(" ,AMT = @AMT");
                sbQuery.Append(" ,UnitPrice = @UnitPrice");
                sbQuery.Append(" ,DoseQTY = @DoseQTY");
                sbQuery.Append(" ,UnitCode = @UnitCode");
                sbQuery.Append(" ,DoseTypeCode = @DoseTypeCode");
                sbQuery.Append(" ,DoseUnitCode = @DoseUnitCode");
                sbQuery.Append(" ,DoseCode = @DoseCode");
                sbQuery.Append(" ,AUXLABEL1 = @AUXLABEL1");
                sbQuery.Append(" ,AUXLABEL2 = @AUXLABEL2");
                sbQuery.Append(" ,AUXLABEL3 = @AUXLABEL3");
                sbQuery.Append(" ,Remark = @Remark");
                sbQuery.Append(" ,AutoTick = @AutoTick");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND MedicineCode = @MedicineCode");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.GroupMethodID, false)));
                parameter.Add(new IParameter("MedicineCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.MedicineCode)));
                parameter.Add(new IParameter("MedicineName_TH", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.MedicineName_TH)));
                parameter.Add(new IParameter("MedicineName_EN", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.MedicineName_EN)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.QTY)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.AMT)));
                parameter.Add(new IParameter("UnitPrice", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.UnitPrice)));
                parameter.Add(new IParameter("DoseQTY", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.DoseQTY)));
                parameter.Add(new IParameter("DoseTypeCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.DoseTypeCode)));
                parameter.Add(new IParameter("UnitCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.UnitCode)));
                parameter.Add(new IParameter("DoseUnitCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.DoseUnitCode)));
                parameter.Add(new IParameter("DoseCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.DoseCode)));
                parameter.Add(new IParameter("AUXLABEL1", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.AUXLABEL1)));
                parameter.Add(new IParameter("AUXLABEL2", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.AUXLABEL2)));
                parameter.Add(new IParameter("AUXLABEL3", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.AUXLABEL3)));
                parameter.Add(new IParameter("Remark", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODMEDICINE.Remark)));
                parameter.Add(new IParameter("AutoTick", IDbType.Bit, _SETUPGROUPMETHODMEDICINE.AutoTick));
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



    }
}
