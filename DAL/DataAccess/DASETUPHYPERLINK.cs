using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;

namespace DAL
{
    class DASETUPHYPERLINK : DataAccess
    {
        DatabaseInfo extConnDBInfo = null;
        public DASETUPHYPERLINK() { }
        public DASETUPHYPERLINK(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPHYPERLINK(DatabaseInfo dbInfo, DatabaseInfo extConnDBInfo) { this.DbInfo = dbInfo; this.extConnDBInfo = extConnDBInfo; }
        public DASETUPHYPERLINK(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPHYPERLINK(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal ReturnValue Insert(SETUPHYPERLINK SETUPHYPERLINK)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                
                sbInsert.Append(" INSERT INTO  [dbo].[SETUPHYPERLINK] (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("LinkCode");
                sbValue.Append("@LinkCode");

                sbInsert.Append(", LinkName");
                sbValue.Append(",@LinkName");

                sbInsert.Append(", LinkURL");
                sbValue.Append(",@LinkURL");

                sbInsert.Append(", IsShow");
                sbValue.Append(",@IsShow");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("LinkCode", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkCode)));
                parameter.Add(new IParameter("LinkName", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkName)));
                parameter.Add(new IParameter("LinkURL", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkURL)));
                parameter.Add(new IParameter("isShow", IDbType.Bit, Convert.ToBoolean(SETUPHYPERLINK.IsShow)));
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

        internal List<SETUPHYPERLINK> SearchAll()
        {
            List<SETUPHYPERLINK> retValue = new List<SETUPHYPERLINK>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from [dbo].[SETUPHYPERLINK] l");
                strQuery.Append(" Order by LinkCode");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPHYPERLINK SETUPHYPERLINK = new SETUPHYPERLINK();
                    SETUPHYPERLINK.LinkCode = query["LinkCode"].ToString();
                    SETUPHYPERLINK.LinkName = query["LinkName"].ToString();
                    SETUPHYPERLINK.LinkURL = query["LinkURL"].ToString();
                    SETUPHYPERLINK.IsShow = Convert.ToBoolean(query["IsShow"]);
                    
                    retValue.Add(SETUPHYPERLINK);
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

        /*
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
        */


        internal ReturnValue CheckDup(SETUPHYPERLINK SETUPHYPERLINK)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from [dbo].[SETUPHYPERLINK] ");
                strQuery.Append(" WHERE LinkCode = @LinkCode ");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter("LinkCode", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkCode)));
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

        internal ReturnValue Update(SETUPHYPERLINK SETUPHYPERLINK, string oldLinkCode)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE [dbo].[SETUPHYPERLINK] SET ");
                sbQuery.Append("  LinkCode = @LinkCode  ");
                sbQuery.Append(" ,LinkName = @LinkName  ");
                sbQuery.Append(" ,LinkURL = @LinkURL  ");
                sbQuery.Append(" ,IsShow = @IsShow  ");
                sbQuery.Append(" WHERE LinkCode = @OldLinkCode");
                //sbQuery.Append(" AND IsActive = 1");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("OldLinkCode", IDbType.VarChar, DBNullConvert.From(oldLinkCode)));
                parameter.Add(new IParameter("LinkCode", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkCode)));
                parameter.Add(new IParameter("LinkName", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkName)));
                parameter.Add(new IParameter("LinkURL", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkURL)));
                parameter.Add(new IParameter("IsShow", IDbType.Bit, SETUPHYPERLINK.IsShow));
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

        internal ReturnValue Delete(SETUPHYPERLINK SETUPHYPERLINK)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM [dbo].[SETUPHYPERLINK]  ");
                sbQuery.Append(" WHERE LinkCode = @LinkCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("LinkCode", IDbType.VarChar, DBNullConvert.From(SETUPHYPERLINK.LinkCode)));
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
