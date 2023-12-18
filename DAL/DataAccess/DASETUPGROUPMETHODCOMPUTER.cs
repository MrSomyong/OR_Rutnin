using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL
{
    class DASETUPGROUPMETHODCOMPUTER : DataAccess
    {
        public DASETUPGROUPMETHODCOMPUTER() { }
        public DASETUPGROUPMETHODCOMPUTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHODCOMPUTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHODCOMPUTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal List<SETUPGROUPMETHODCOMPUTER> GetSETUPGROUPMETHODCOMPUTERByKey(int groupMethodID)
        {
            List<SETUPGROUPMETHODCOMPUTER> retValue = new List<SETUPGROUPMETHODCOMPUTER>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPGROUPMETHODCOMPUTER]");
                strQuery.Append(" where IsActive = 1 and  GroupMethodID = @GroupMethodID");
                strQuery.Append(" Order by ComputerCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(groupMethodID, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER = new SETUPGROUPMETHODCOMPUTER();
                    _SETUPGROUPMETHODCOMPUTER.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODCOMPUTER.ComputerCode = query["ComputerCode"].ToString();
                    _SETUPGROUPMETHODCOMPUTER.ComputerName = query["ComputerName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODCOMPUTER);
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

        
        internal List<SETUPGROUPMETHODCOMPUTER> GetSETUPGROUPMETHODCOMPUTERByGroupMethodCode(string  groupMethodCode)
        {
            List<SETUPGROUPMETHODCOMPUTER> retValue = new List<SETUPGROUPMETHODCOMPUTER>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* , b.GroupMethodCode  from [dbo].[SETUPGROUPMETHODCOMPUTER]  a left join SETUPGROUPMETHOD b on a.GroupMethodID = b.GroupMethodID ");
                strQuery.Append(" where a.IsActive = 1 and  b.GroupMethodCode = @groupMethodCode");
                strQuery.Append(" Order by a.ComputerCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER = new SETUPGROUPMETHODCOMPUTER();
                    _SETUPGROUPMETHODCOMPUTER.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODCOMPUTER.GroupMethodCode = query["GroupMethodCode"].ToString();
                    _SETUPGROUPMETHODCOMPUTER.ComputerCode = query["ComputerCode"].ToString();
                    _SETUPGROUPMETHODCOMPUTER.ComputerName = query["ComputerName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODCOMPUTER);
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


        internal bool CheckDup(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT count(*) as num FROM [dbo].[SETUPGROUPMETHODCOMPUTER] ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND ComputerCode = @ComputerCode");
                strQuery.Append(" AND IsActive = 1");
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.GroupMethodID,false)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerCode)));
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

        internal ReturnValue Insert(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append(" INSERT INTO  [dbo].[SETUPGROUPMETHODCOMPUTER] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodID");
                sbValue.Append("@GroupMethodID");

                sbInsert.Append(", ComputerCode");
                sbValue.Append(",@ComputerCode");

                sbInsert.Append(", ComputerName");
                sbValue.Append(",@ComputerName");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.GroupMethodID, false)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerCode)));
                parameter.Add(new IParameter("ComputerName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerName)));
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


        internal ReturnValue InActiveSETUPGROUPMETHODCOMPUTER(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODCOMPUTER] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND ComputerCode = @ComputerCode");
                sbQuery.Append(" AND IsActive = 1");
                

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.GroupMethodID, false)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerCode)));
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

        internal SETUPGROUPMETHODCOMPUTER GetSETUPGROUPMETHODCOMPUTERByKey(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            SETUPGROUPMETHODCOMPUTER SETUPGROUPMETHODCOMPUTER = new SETUPGROUPMETHODCOMPUTER();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  t.*  from [dbo].[SETUPGROUPMETHODCOMPUTER] t ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND ComputerCode = @ComputerCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.GroupMethodID, false)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    SETUPGROUPMETHODCOMPUTER.GroupMethodID = ADOUtil.GetIntFromQuery(query["GroupMethodID"].ToString());
                    SETUPGROUPMETHODCOMPUTER.ComputerCode = query["ComputerCode"].ToString();
                    SETUPGROUPMETHODCOMPUTER.ComputerName = query["ComputerName"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHODCOMPUTER;
        }

        internal ReturnValue Update(SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODCOMPUTER] SET ");

                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND ComputerCode = @ComputerCode");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.GroupMethodID, false)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCOMPUTER.ComputerCode)));
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
