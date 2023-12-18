using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL
{
    class DASETUPGROUPMETHODDOCTOR : DataAccess
    {
        public DASETUPGROUPMETHODDOCTOR() { }
        public DASETUPGROUPMETHODDOCTOR(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPGROUPMETHODDOCTOR(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPGROUPMETHODDOCTOR(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }


        internal List<SETUPGROUPMETHODDOCTOR> GetSETUPGROUPMETHODDOCTORByKey(int groupMethodID)
        {
            List<SETUPGROUPMETHODDOCTOR> retValue = new List<SETUPGROUPMETHODDOCTOR>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPGROUPMETHODDOCTOR]");
                strQuery.Append(" where IsActive = 1 and  GroupMethodID = @GroupMethodID");
                strQuery.Append(" Order by DoctorCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(groupMethodID, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR = new SETUPGROUPMETHODDOCTOR();
                    _SETUPGROUPMETHODDOCTOR.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODDOCTOR.DoctorCode = query["DoctorCode"].ToString();
                    _SETUPGROUPMETHODDOCTOR.DoctorName = query["DoctorName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODDOCTOR);
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

        
        internal List<SETUPGROUPMETHODDOCTOR> GetSETUPGROUPMETHODDOCTORByGroupMethodCode(string  groupMethodCode)
        {
            List<SETUPGROUPMETHODDOCTOR> retValue = new List<SETUPGROUPMETHODDOCTOR>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* , b.GroupMethodCode  from [dbo].[SETUPGROUPMETHODDOCTOR]  a left join SETUPGROUPMETHOD b on a.GroupMethodID = b.GroupMethodID ");
                strQuery.Append(" where a.IsActive = 1 and  b.GroupMethodCode = @groupMethodCode");
                strQuery.Append(" Order by a.DoctorCode");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodCode", IDbType.VarChar, DBNullConvert.From(groupMethodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR = new SETUPGROUPMETHODDOCTOR();
                    _SETUPGROUPMETHODDOCTOR.GroupMethodID = Convert.ToInt32(query["GroupMethodID"]);
                    _SETUPGROUPMETHODDOCTOR.GroupMethodCode = query["GroupMethodCode"].ToString();
                    _SETUPGROUPMETHODDOCTOR.DoctorCode = query["DoctorCode"].ToString();
                    _SETUPGROUPMETHODDOCTOR.DoctorName = query["DoctorName"].ToString();
                    retValue.Add(_SETUPGROUPMETHODDOCTOR);
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


        internal bool CheckDup(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPGROUPMETHODDOCTOR] ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND DoctorCode = @DoctorCode");
                strQuery.Append(" AND IsActive = 1");
                
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.GroupMethodID,false)));
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorCode)));
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

        internal ReturnValue Insert(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append(" INSERT INTO  [dbo].[SETUPGROUPMETHODDOCTOR] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("GroupMethodID");
                sbValue.Append("@GroupMethodID");

                sbInsert.Append(", DoctorCode");
                sbValue.Append(",@DoctorCode");

                sbInsert.Append(", DoctorName");
                sbValue.Append(",@DoctorName");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.GroupMethodID, false)));
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorCode)));
                parameter.Add(new IParameter("DoctorName", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorName)));
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


        internal ReturnValue InActiveSETUPGROUPMETHODDOCTOR(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODDOCTOR] SET ");
                sbQuery.Append(" IsActive = 0");
                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND DoctorCode = @DoctorCode");
                sbQuery.Append(" AND IsActive = 1");
                

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.GroupMethodID, false)));
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorCode)));
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

        internal SETUPGROUPMETHODDOCTOR GetSETUPGROUPMETHODDOCTORByKey(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            SETUPGROUPMETHODDOCTOR SETUPGROUPMETHODDOCTOR = new SETUPGROUPMETHODDOCTOR();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  t.*  from [dbo].[SETUPGROUPMETHODDOCTOR] t ");
                strQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                strQuery.Append(" AND DoctorCode = @DoctorCode");
                strQuery.Append(" AND IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.GroupMethodID, false)));
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    SETUPGROUPMETHODDOCTOR.GroupMethodID = ADOUtil.GetIntFromQuery(query["GroupMethodID"].ToString());
                    SETUPGROUPMETHODDOCTOR.DoctorCode = query["DoctorCode"].ToString();
                    SETUPGROUPMETHODDOCTOR.DoctorName = query["DoctorName"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPGROUPMETHODDOCTOR;
        }

        internal ReturnValue Update(SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPGROUPMETHODDOCTOR] SET ");

                sbQuery.Append(" WHERE GroupMethodID = @GroupMethodID");
                sbQuery.Append(" AND DoctorCode = @DoctorCode");
                sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("GroupMethodID", IDbType.Int, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.GroupMethodID, false)));
                parameter.Add(new IParameter("DoctorCode", IDbType.VarChar, DBNullConvert.From(_SETUPGROUPMETHODDOCTOR.DoctorCode)));
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
