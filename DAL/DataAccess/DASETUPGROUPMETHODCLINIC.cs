using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL
{
    class DASETUPGROUPMETHODCLINIC : DataAccess
    {
        public DASETUPGROUPMETHODCLINIC() { }
        public DASETUPGROUPMETHODCLINIC(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHODCLINIC(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHODCLINIC(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal List<SETUPGROUPMETHODCLINIC> GetSETUPGROUPMETHODCLINICByKey(int groupMethodID)
        {
            List<SETUPGROUPMETHODCLINIC> retValue = new List<SETUPGROUPMETHODCLINIC>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPGROUPMETHODCLINIC]");
                strQuery.Append(" where IsActive = 1 and  GroupMethodID = @GroupMethodID");
                strQuery.Append(" Order by ClinicCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(groupMethodID, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC = new SETUPGROUPMETHODCLINIC();
                    _SETUPGROUPMETHODCLINIC.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODCLINIC.ClinicCode = query["ClinicCode"].ToString();
                    _SETUPGROUPMETHODCLINIC.ClinicName = query["ClinicName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODCLINIC);
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

        
        internal List<SETUPGROUPMETHODCLINIC> GetSETUPGROUPMETHODCLINICByGroupMethodCode(string  groupMethodCode)
        {
            List<SETUPGROUPMETHODCLINIC> retValue = new List<SETUPGROUPMETHODCLINIC>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* , b.GroupMethodCode  from [dbo].[SETUPGROUPMETHODCLINIC]  a left join SETUPGROUPMETHOD b on a.GroupMethodID = b.GroupMethodID ");
                strQuery.Append(" where a.IsActive = 1 and  b.GroupMethodCode = @groupMethodCode");
                strQuery.Append(" Order by a.ComputerCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC = new SETUPGROUPMETHODCLINIC();
                    _SETUPGROUPMETHODCLINIC.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODCLINIC.GroupMethodCode = query["GroupMethodCode"].ToString();
                    _SETUPGROUPMETHODCLINIC.ClinicCode = query["ClinicCode"].ToString();
                    _SETUPGROUPMETHODCLINIC.ClinicName = query["ClinicName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODCLINIC);
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


        internal bool CheckDup(SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT count(*) as num FROM [dbo].[SETUPGROUPMETHODCLINIC] ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND ClinicCode = @ClinicCode");
                strQuery.Append(" AND IsActive = 1");
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.GroupMethodID,false)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicCode)));
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

        internal ReturnValue Insert(SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append(" INSERT INTO  [dbo].[SETUPGROUPMETHODCLINIC] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodID");
                sbValue.Append("@GroupMethodID");

                sbInsert.Append(", ClinicCode");
                sbValue.Append(",@ClinicCode");

                sbInsert.Append(", ClinicName");
                sbValue.Append(",@ClinicName");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.GroupMethodID, false)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicCode)));
                parameter.Add(new IParameter("ClinicName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicName)));
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


        internal ReturnValue InActiveSETUPGROUPMETHODCLINIC(SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODCLINIC] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND ClinicCode = @ClinicCode");
                sbQuery.Append(" AND IsActive = 1");
                

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.GroupMethodID, false)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicCode)));
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

        internal SETUPGROUPMETHODCLINIC GetSETUPGROUPMETHODCLINICByKey(SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC)
        {
            SETUPGROUPMETHODCLINIC SETUPGROUPMETHODCLINIC = new SETUPGROUPMETHODCLINIC();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  t.*  from [dbo].[SETUPGROUPMETHODCLINIC] t ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND ClinicCode = @ClinicCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.GroupMethodID, false)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    SETUPGROUPMETHODCLINIC.GroupMethodID = ADOUtil.GetIntFromQuery(query["GroupMethodID"].ToString());
                    SETUPGROUPMETHODCLINIC.ClinicCode = query["ClinicCode"].ToString();
                    SETUPGROUPMETHODCLINIC.ClinicName = query["ClinicName"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHODCLINIC;
        }

        internal ReturnValue Update(SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODCLINIC] SET ");

                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND ClinicCode = @ClinicCode");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.GroupMethodID, false)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODCLINIC.ClinicCode)));
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
