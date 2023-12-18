using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;

namespace DAL
{
    class DASETUPCOMPUTER : DataAccess
    {
        DatabaseInfo extConnDBInfo = null;
        public DASETUPCOMPUTER() { }
        public DASETUPCOMPUTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPCOMPUTER(DatabaseInfo dbInfo, DatabaseInfo extConnDBInfo) { this.DbInfo = dbInfo; this.extConnDBInfo = extConnDBInfo; }
        public DASETUPCOMPUTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPCOMPUTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal ReturnValue Insert(SETUPCOMPUTER _SETUPCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                
                sbInsert.Append(" INSERT INTO  [dbo].[SETUPCOMPUTER] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("ComputerCode");
                sbValue.Append("@ComputerCode");

                sbInsert.Append(", ComputerName");
                sbValue.Append(",@ComputerName");

                sbInsert.Append(", DefaultStoreCode");
                sbValue.Append(",@DefaultStoreCode");

                sbInsert.Append(", DefaultClinicCode");
                sbValue.Append(",@DefaultClinicCode");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTER.ComputerCode)));
                parameter.Add(new IParameter("ComputerName", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTER.ComputerName)));
                parameter.Add(new IParameter("DefaultStoreCode", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTER.DefaultStoreCode)));
                parameter.Add(new IParameter("DefaultClinicCode", IDbType.VarChar, DBNullConvert.From(_SETUPCOMPUTER.DefaultClinicCode)));
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

        internal List<SETUPCOMPUTER> SearchAll()
        {
            List<SETUPCOMPUTER> retValue = new List<SETUPCOMPUTER>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPCOMPUTER] c");
                //strQuery.Append(" LEFT JOIN [REH].[dbo].[VT_STORE] s ON c.DefaultStoreCode = s.StoreCode ");
                strQuery.Append(" Order by ComputerCode");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPCOMPUTER SETUPCOMPUTER = new SETUPCOMPUTER();
                    SETUPCOMPUTER.ComputerCode = query["ComputerCode"].ToString();
                    SETUPCOMPUTER.ComputerName = query["ComputerName"].ToString();
                    SETUPCOMPUTER.DefaultStoreCode = query["DefaultStoreCode"].ToString();
                    SETUPCOMPUTER.DefaultClinicCode = query["DefaultClinicCode"].ToString();

                    SETUPCOMPUTER.StoreInfo = new DAVT_STORE(extConnDBInfo).GetStoreByKey(SETUPCOMPUTER.DefaultStoreCode);
                    SETUPCOMPUTER.ClinicInfo = new DAVT_CLINIC(extConnDBInfo).GetClinicByKey(SETUPCOMPUTER.DefaultClinicCode);
                    //SETUPCOMPUTER.DefaultStoreName = query["StoreName"].ToString();
                    //SETUPCOMPUTER.StoreInfo.StoreCode = query["StoreCode"].ToString();
                    //SETUPCOMPUTER.StoreInfo.StoreName = query["StoreName"].ToString();
                    retValue.Add(SETUPCOMPUTER);
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

        internal SETUPCOMPUTER SearchByKey(string computerCode)
        {

            SETUPCOMPUTER SETUPCOMPUTER = new SETUPCOMPUTER();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  *  from [dbo].[SETUPCOMPUTER] ");
                strQuery.Append(" WHERE ComputerCode = @ComputerCode");

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(computerCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    SETUPCOMPUTER.ComputerCode = query["ComputerCode"].ToString();
                    SETUPCOMPUTER.ComputerName = query["ComputerName"].ToString();
                    SETUPCOMPUTER.DefaultStoreCode = query["DefaultStoreCode"].ToString();
                    SETUPCOMPUTER.DefaultClinicCode = query["DefaultClinicCode"].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPCOMPUTER;
        }



        internal ReturnValue CheckDup(SETUPCOMPUTER SETUPCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPCOMPUTER] ");
                strQuery.Append(" WHERE ComputerCode = @ComputerCode ");
                //strQuery.Append(" AND IsActive = 1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerCode)));
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

        internal ReturnValue Update(SETUPCOMPUTER SETUPCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPCOMPUTER] SET ");
                sbQuery.Append(" ComputerName = @ComputerName  ");
                sbQuery.Append(" ,DefaultStoreCode = @DefaultStoreCode  ");
                sbQuery.Append(" ,DefaultClinicCode = @DefaultClinicCode  ");
                sbQuery.Append(" WHERE ComputerCode = @ComputerCode");
                //sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerCode)));
                parameter.Add(new IParameter("ComputerName", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerName)));
                parameter.Add(new IParameter("DefaultStoreCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.DefaultStoreCode)));
                parameter.Add(new IParameter("DefaultClinicCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.DefaultClinicCode)));
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

        internal ReturnValue Delete(SETUPCOMPUTER SETUPCOMPUTER)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM [dbo].[SETUPCOMPUTER]  ");
                sbQuery.Append(" WHERE ComputerCode = @ComputerCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("ComputerCode", IDbType.VarChar, DBNullConvert.From(SETUPCOMPUTER.ComputerCode)));
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
