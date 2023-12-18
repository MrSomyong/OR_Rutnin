using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;
namespace DAL 
{
    class DAVT_CLINIC : DataAccess
    {
        public DAVT_CLINIC() { }
        public DAVT_CLINIC(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_CLINIC(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_CLINIC(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_CLINIC> SearchAll()
        {
            List<VT_CLINIC> retValue = new List<VT_CLINIC>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT * FROM VT_CLINIC  ");
                strQuery.Append(" WHERE I_ENABLED = 1");
                strQuery.Append(" ORDER BY CLINIC_NAME_ENG");
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_CLINIC clinic = new VT_CLINIC();
                    clinic.CLINIC_CODE = query["CLINIC_CODE"].ToString();
                    clinic.CLINIC_NAME_TH = query["CLINIC_NAME_TH"].ToString();
                    clinic.CLINIC_NAME_ENG = query["CLINIC_NAME_ENG"].ToString();
                    retValue.Add(clinic);
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

        internal VT_CLINIC GetClinicByKey(string code)
        {
            VT_CLINIC VT_CLINIC = new VT_CLINIC();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_CLINIC ");
                strQuery.Append(" where CLINIC_CODE = @CLINIC_CODE");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("CLINIC_CODE", IDbType.VarChar, DBNullConvert.From(code)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_CLINIC.CLINIC_CODE = query["CLINIC_CODE"].ToString();
                    VT_CLINIC.CLINIC_NAME_TH = query["CLINIC_NAME_TH"].ToString();
                    VT_CLINIC.CLINIC_NAME_ENG = query["CLINIC_NAME_ENG"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_CLINIC;
        }
    }
}
