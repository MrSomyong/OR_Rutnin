using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL
{
    class DASETUPGROUPMETHODTREATMENT : DataAccess
    {
        public DASETUPGROUPMETHODTREATMENT() { }
        public DASETUPGROUPMETHODTREATMENT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHODTREATMENT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHODTREATMENT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal List<SETUPGROUPMETHODTREATMENT> GetSETUPGROUPMETHODTREATMENTByKey(int groupMethodID)
        {
            List<SETUPGROUPMETHODTREATMENT> retValue = new List<SETUPGROUPMETHODTREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPGROUPMETHODTREATMENT]");
                strQuery.Append(" where IsActive = 1 and  GroupMethodID = @GroupMethodID");
                strQuery.Append(" Order by TreatmentCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(groupMethodID, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
                    _SETUPGROUPMETHODTREATMENT.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODTREATMENT.TreatmentCode = query["TreatmentCode"].ToString();
                    _SETUPGROUPMETHODTREATMENT.TreatmentName = query["TreatmentName"].ToString();
                    _SETUPGROUPMETHODTREATMENT.AutoTick = (bool)query["AutoTick"];
                    retValue.Add(_SETUPGROUPMETHODTREATMENT);
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

        
        internal List<SETUPGROUPMETHODTREATMENT> GetSETUPGROUPMETHODTREATMENTByGroupMethodCode(string  groupMethodCode)
        {
            List<SETUPGROUPMETHODTREATMENT> retValue = new List<SETUPGROUPMETHODTREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* , b.GroupMethodCode  from [dbo].[SETUPGROUPMETHODTREATMENT]  a left join SETUPGROUPMETHOD b on a.GroupMethodID = b.GroupMethodID ");
                strQuery.Append(" where a.IsActive = 1 and  b.GroupMethodCode = @groupMethodCode");
                strQuery.Append(" Order by a.TreatmentCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
                    _SETUPGROUPMETHODTREATMENT.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODTREATMENT.GroupMethodCode = query["GroupMethodCode"].ToString();
                    _SETUPGROUPMETHODTREATMENT.TreatmentCode = query["TreatmentCode"].ToString();
                    _SETUPGROUPMETHODTREATMENT.TreatmentName = query["TreatmentName"].ToString();
                    _SETUPGROUPMETHODTREATMENT.AutoTick = (bool)query["AutoTick"];
                    retValue.Add(_SETUPGROUPMETHODTREATMENT);
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


        internal bool CheckDup(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPGROUPMETHODTREATMENT] ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND TreatmentCode = @TreatmentCode");
                strQuery.Append(" AND IsActive = 1");
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.GroupMethodID,false)));
                parameter.Add(new IParameter("TreatmentCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentCode)));
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

        internal ReturnValue Insert(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append(" INSERT INTO  [dbo].[SETUPGROUPMETHODTREATMENT] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodID");
                sbValue.Append("@GroupMethodID");

                sbInsert.Append(", TreatmentCode");
                sbValue.Append(",@TreatmentCode");

                sbInsert.Append(", TreatmentName");
                sbValue.Append(",@TreatmentName");

                sbInsert.Append(", CHARGECODE");
                sbValue.Append(",@CHARGECODE");

                sbInsert.Append(", AMT");
                sbValue.Append(",@AMT");

                sbInsert.Append(", QTY");
                sbValue.Append(",@QTY");

                sbInsert.Append(", TREATMENTENTRYSTYLE");
                sbValue.Append(",@TREATMENTENTRYSTYLE");

                sbInsert.Append(", REMARKS");
                sbValue.Append(",@REMARKS");

                sbInsert.Append(", AutoTick");
                sbValue.Append(",@AutoTick");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.GroupMethodID, false)));
                parameter.Add(new IParameter("TreatmentCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentCode)));
                parameter.Add(new IParameter("TreatmentName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentName)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.CHARGECODE)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.QTY)));
                parameter.Add(new IParameter("TREATMENTENTRYSTYLE", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE,false)));
                parameter.Add(new IParameter("REMARKS", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.REMARKS)));
                parameter.Add(new IParameter("AutoTick", IDbType.Bit, _SETUPGROUPMETHODTREATMENT.AutoTick));
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


        internal ReturnValue InActiveSETUPGROUPMETHODTREATMENT(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODTREATMENT] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND TreatmentCode = @TreatmentCode");
                sbQuery.Append(" AND IsActive = 1");
                

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.GroupMethodID, false)));
                parameter.Add(new IParameter("TreatmentCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentCode)));
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

        internal SETUPGROUPMETHODTREATMENT GetSETUPGROUPMETHODTREATMENTByKey(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {
            SETUPGROUPMETHODTREATMENT SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  t.*  from [dbo].[SETUPGROUPMETHODTREATMENT] t ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND TreatmentCode = @TreatmentCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.GroupMethodID, false)));
                parameter.Add(new IParameter("TreatmentCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    SETUPGROUPMETHODTREATMENT.GroupMethodID = ADOUtil.GetIntFromQuery(query["GroupMethodID"].ToString());
                    SETUPGROUPMETHODTREATMENT.TreatmentCode = query["TreatmentCode"].ToString();
                    SETUPGROUPMETHODTREATMENT.TreatmentName = query["TreatmentName"].ToString();
                    SETUPGROUPMETHODTREATMENT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    SETUPGROUPMETHODTREATMENT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    SETUPGROUPMETHODTREATMENT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    SETUPGROUPMETHODTREATMENT.REMARKS = query["REMARKS"].ToString();
                    SETUPGROUPMETHODTREATMENT.AutoTick = (bool)query["AutoTick"];

                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHODTREATMENT;
        }

        internal ReturnValue Update(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODTREATMENT] SET ");
                sbQuery.Append("  AMT = @AMT");
                sbQuery.Append(" ,QTY = @QTY");
                sbQuery.Append(" ,REMARKS = @REMARKS");
                sbQuery.Append(" ,AutoTick = @AutoTick");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND TreatmentCode = @TreatmentCode");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.GroupMethodID, false)));
                parameter.Add(new IParameter("TreatmentCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.TreatmentCode)));
                parameter.Add(new IParameter("REMARKS", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.REMARKS)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_SETUPGROUPMETHODTREATMENT.QTY)));
                parameter.Add(new IParameter("AutoTick", IDbType.Bit, _SETUPGROUPMETHODTREATMENT.AutoTick));
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
