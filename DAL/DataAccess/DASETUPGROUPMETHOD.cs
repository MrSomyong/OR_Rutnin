using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;

namespace DAL
{
    class DASETUPGROUPMETHOD : DataAccess
    {
        public DASETUPGROUPMETHOD() { }
        public DASETUPGROUPMETHOD(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHOD(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHOD(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal ReturnValue Insert(SETUPGROUPMETHOD _SETUPGROUPMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                
                sbInsert.Append(" INSERT INTO [dbo].[SETUPGROUPMETHOD] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodCode");
                sbValue.Append("@GroupMethodCode");

                sbInsert.Append(", GroupMethodName");
                sbValue.Append(",@GroupMethodName");

                sbInsert.Append(", IsActive");
                sbValue.Append(",'1'");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodCode)));
                parameter.Add(new IParameter("GroupMethodName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodName)));
                command = GetCommand(sbInsert.ToString(), parameter);
                               
                effected = GetExecuteNonQuery(command);
                //int ret = (int)command.Parameters["@requestID"];
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

        internal List<SETUPGROUPMETHOD> SearchAll()
        {
            List<SETUPGROUPMETHOD> retValue = new List<SETUPGROUPMETHOD>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPGROUPMETHOD]");
                strQuery.Append(" where IsActive = 1");
                strQuery.Append(" Order by GroupMethodCode");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHOD SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                    SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    SETUPGROUPMETHOD.GroupMethodCode = query["GroupMethodCode"].ToString();
                    SETUPGROUPMETHOD.GroupMethodName = query["GroupMethodName"].ToString();
                    retValue.Add(SETUPGROUPMETHOD);
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

        internal List<SETUPGROUPMETHOD> SearchMedicineOnly()
        {
            List<SETUPGROUPMETHOD> retValue = new List<SETUPGROUPMETHOD>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select distinct A.* from [dbo].[SETUPGROUPMETHOD] A inner join [dbo].[SETUPGROUPMETHODMEDICINE] B on(a.[GroupMethodID] = b.[GroupMethodID] and b.[IsActive] = 1)");
                strQuery.Append(" where A.IsActive = 1");
                strQuery.Append(" Order by A.GroupMethodCode");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHOD SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                    SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    SETUPGROUPMETHOD.GroupMethodCode = query["GroupMethodCode"].ToString();
                    SETUPGROUPMETHOD.GroupMethodName = query["GroupMethodName"].ToString();
                    retValue.Add(SETUPGROUPMETHOD);
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

        internal List<SETUPGROUPMETHOD> SearchAllFilterBy(string doctorCode, string clinicCode , string computerCode)
        {
            List<SETUPGROUPMETHOD> retValue = new List<SETUPGROUPMETHOD>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT distinct m.* ");
                strQuery.Append(" FROM [dbo].[SETUPGROUPMETHOD] m ");
                strQuery.Append(" LEFT JOIN [dbo].[SETUPGROUPMETHODDOCTOR] d on m.GroupMethodID = d.GroupMethodID ");
                strQuery.Append(" LEFT JOIN [dbo].[SETUPGROUPMETHODCOMPUTER] c on m.GroupMethodID = c.GroupMethodID ");
                strQuery.Append(" LEFT JOIN [dbo].[SETUPGROUPMETHODCLINIC] cn on m.GroupMethodID = cn.GroupMethodID ");
                strQuery.Append(" WHERE m.IsActive = 1  ");
                //strQuery.Append("  and d.IsActive = 1 ");
                //strQuery.Append("  and c.IsActive = 1 ");
                //strQuery.Append("  and cn.IsActive = 1 ");
                strQuery.Append("  and ");
                strQuery.Append("  ( ");
                strQuery.Append("        (d.DoctorCode = @DoctorCode and d.IsActive = 1) ");
                strQuery.Append("     or (c.ComputerCode = @ComputerCode and c.IsActive = 1) ");
                strQuery.Append("     or (cn.ClinicCode = @ClinicCode and cn.IsActive = 1) ");
                strQuery.Append("  ) ");
                strQuery.Append(" Order by m.GroupMethodCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(doctorCode)));
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(computerCode)));
                parameter.Add(new IParameter("ClinicCode", IDbType.VarChar, DBNullConvert.From(clinicCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHOD SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                    SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    SETUPGROUPMETHOD.GroupMethodCode = query["GroupMethodCode"].ToString();
                    SETUPGROUPMETHOD.GroupMethodName = query["GroupMethodName"].ToString();
                    retValue.Add(SETUPGROUPMETHOD);
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
        internal ReturnValue CheckDup(SETUPGROUPMETHOD SETUPGROUPMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPGROUPMETHOD] ");
                strQuery.Append(" WHERE GroupMethodCode = @GroupMethodCode ");
                strQuery.Append(" AND IsActive = 1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(SETUPGROUPMETHOD.GroupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
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

        internal ReturnValue Update(SETUPGROUPMETHOD _SETUPGROUPMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHOD] SET ");
                sbQuery.Append(" GroupMethodName = @GroupMethodName  ");
                sbQuery.Append(" WHERE GroupMethodCode = @GroupMethodCode");
                sbQuery.Append(" AND GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodCode)));
                parameter.Add(new IParameter("GroupMethodName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodName)));
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodID, false)));
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

        internal ReturnValue InActive(SETUPGROUPMETHOD _SETUPGROUPMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHOD] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodCode = @GroupMethodCode");
                sbQuery.Append(" AND GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodCode)));
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHOD.GroupMethodID, false)));
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

        internal SETUPGROUPMETHOD GetSETUPGROUPMETHODByKey(string groupMethodCode)
        {
            SETUPGROUPMETHOD SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  m.*  from [dbo].[SETUPGROUPMETHOD] m ");
                strQuery.Append(" WHERE GroupMethodCode = @GroupMethodCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    SETUPGROUPMETHOD.GroupMethodCode = query["GroupMethodCode"].ToString();
                    SETUPGROUPMETHOD.GroupMethodName = query["GroupMethodName"].ToString();
                   
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHOD;
        }

    }
}
