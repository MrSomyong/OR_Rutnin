using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;

namespace DAL
{
    class DASETUPCOMPUTERMETHOD : DataAccess
    {
        DatabaseInfo extConnDBInfo = null;
        public DASETUPCOMPUTERMETHOD() { }
        public DASETUPCOMPUTERMETHOD(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPCOMPUTERMETHOD(DatabaseInfo dbInfo, DatabaseInfo extConnDBInfo) { this.DbInfo = dbInfo; this.extConnDBInfo = extConnDBInfo; }
        public DASETUPCOMPUTERMETHOD(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPCOMPUTERMETHOD(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        
        internal ReturnValue Insert(SETUPCOMPUTERMETHOD _SETUPCOMPUTERMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                
                sbInsert.Append(" INSERT INTO  [dbo].[SETUPCOMPUTERMETHOD] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("ComputerCode");
                sbValue.Append("@ComputerCode");

                sbInsert.Append(", MethodCode");
                sbValue.Append(",@MethodCode");

                sbInsert.Append(", MethodName");
                sbValue.Append(",@MethodName");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTERMETHOD.ComputerCode)));
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTERMETHOD.MethodCode)));
                parameter.Add(new IParameter("MethodName", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTERMETHOD.MethodName)));
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
        
      
        internal List<SETUPCOMPUTERMETHOD> GetAllByKey(string computerCode)
        {

            List<SETUPCOMPUTERMETHOD> SETUPCOMPUTERMETHODLIST = new List<SETUPCOMPUTERMETHOD>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  *  from [dbo].[SETUPCOMPUTERMETHOD] ");
                strQuery.Append(" WHERE ComputerCode = @ComputerCode and IsActive = 1");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(computerCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPCOMPUTERMETHOD SETUPCOMPUTERMETHOD = new SETUPCOMPUTERMETHOD();
                    SETUPCOMPUTERMETHOD.ComputerMethodID = (int)query["ComputerMethodID"];
                    SETUPCOMPUTERMETHOD.ComputerCode = query["ComputerCode"].ToString();
                    SETUPCOMPUTERMETHOD.MethodCode = query["MethodCode"].ToString();
                    SETUPCOMPUTERMETHOD.MethodName = query["MethodName"].ToString();
                    SETUPCOMPUTERMETHOD.IsActive = (bool)query["IsActive"];
                    SETUPCOMPUTERMETHODLIST.Add(SETUPCOMPUTERMETHOD);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPCOMPUTERMETHODLIST;
        }



        internal ReturnValue CheckDup(SETUPCOMPUTERMETHOD SETUPCOMPUTERMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPCOMPUTERMETHOD] ");
                strQuery.Append(" WHERE ComputerCode = @ComputerCode and MethodCode = @MethodCode");
                strQuery.Append(" AND IsActive = 1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTERMETHOD.ComputerCode)));
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTERMETHOD.MethodCode)));
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

        /*
        internal ReturnValue Update(SETUPCOMPUTER SETUPCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPCOMPUTER] SET ");
                sbQuery.Append(" ComputerName = @ComputerName  ");
                sbQuery.Append(" ,DefaultStoreCode = @DefaultStoreCode  ");
                sbQuery.Append(" WHERE ComputerCode = @ComputerCode");
                //sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerCode)));
                parameter.Add(new IParameter("ComputerName", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerName)));
                parameter.Add(new IParameter("DefaultStoreCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.DefaultStoreCode)));
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
        */
        internal ReturnValue Delete(SETUPCOMPUTERMETHOD SETUPCOMPUTERMETHOD)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM [dbo].[SETUPCOMPUTERMETHOD]  ");
                sbQuery.Append(" WHERE ComputerCode = @ComputerCode and MethodCode = @MethodCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTERMETHOD.ComputerCode)));
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTERMETHOD.MethodCode)));
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
