using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data.SqlClient;
namespace DAL
{
    class DAVT_TREATMENTMETHODCODE : DataAccess
    {
        public DAVT_TREATMENTMETHODCODE() { }
        public DAVT_TREATMENTMETHODCODE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_TREATMENTMETHODCODE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_TREATMENTMETHODCODE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_TREATMENTMETHODCODE> GetTreatmentMethodAll()
        {
            List<VT_TREATMENTMETHODCODE> retValue = new List<VT_TREATMENTMETHODCODE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                // Edit 15.02 เปลี่ยนไปอ่าน group method
                //strQuery.Append(" select distinct MethodCode , MethodName from VT_TREATMENTMETHODCODE");
                //strQuery.Append(" order by MethodCode");

                strQuery.Append(" SELECT distinct A.[GroupMethodCode] As MethodCode,A.[GroupMethodName] As MethodName FROM [dbo].[SETUPGROUPMETHOD] A LEFT JOIN dbo.SETUPGROUPMETHODTREATMENT B on(A.GroupMethodID = B.GroupMethodID) WHERE B.GroupMethodID is not null ");
                strQuery.Append(" order by A.[GroupMethodCode]");

                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_TREATMENTMETHODCODE _VT_TREATMENTMETHODCODE = new VT_TREATMENTMETHODCODE();
                    _VT_TREATMENTMETHODCODE.MethodCode = query["MethodCode"].ToString();
                    _VT_TREATMENTMETHODCODE.MethodName = query["MethodName"].ToString();
                    retValue.Add(_VT_TREATMENTMETHODCODE);
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

        internal VT_TREATMENTMETHODCODE GetTreatmentMethodByKey(string methodCode)
        {
            VT_TREATMENTMETHODCODE _VT_TREATMENTMETHODCODE = new VT_TREATMENTMETHODCODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                //Edit 15.02 แก้ให้อ่าน group method
                //strQuery.Append(" select distinct top 1 MethodCode , MethodName from VT_TREATMENTMETHODCODE");
                //strQuery.Append(" where MethodCode = @MethodCode");

                strQuery.Append(" SELECT distinct top 1 A.[GroupMethodCode] as MethodCode,A.[GroupMethodName] as MethodName FROM [dbo].[SETUPGROUPMETHOD] A LEFT JOIN dbo.SETUPGROUPMETHODTREATMENT B on(A.GroupMethodID = B.GroupMethodID) ");
                strQuery.Append(" where A.[GroupMethodCode] = @MethodCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(methodCode)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                
                if (query.Read())
                {
                    
                    _VT_TREATMENTMETHODCODE.MethodCode = query["MethodCode"].ToString();
                    _VT_TREATMENTMETHODCODE.MethodName = query["MethodName"].ToString();
                   
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
                
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return _VT_TREATMENTMETHODCODE;

        }
        internal List<VT_TREATMENTMETHODCODE> GetTreatmentCodeByMethodCode(string _MethodCode)
        {
            List<VT_TREATMENTMETHODCODE> retValue = new List<VT_TREATMENTMETHODCODE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                //Edit 15.02 แก้ให้อ่านจาก group method
                //strQuery.Append(" select m.*  from VT_TREATMENTMETHODCODE m left join VT_TREATMENTCODE t");
                //strQuery.Append(" on m.TreatmentCode = t.Code");
                //strQuery.Append(" where MethodCode = @MethodCode");
                //strQuery.Append(" and (t.IsOff = 0 or t.Isoff is null)");
                //strQuery.Append(" order by m.SUFFIX ");

                strQuery.Append(" SELECT A.[GroupMethodCode] AS MethodCode,A.[GroupMethodName] AS MethodName,B.GroupMethodTreatmentID As Suffix ");
                strQuery.Append(" ,TreatmentCode,TreatmentName,AMT AS StdPrice1 ");
                strQuery.Append(" FROM [dbo].[SETUPGROUPMETHOD] A LEFT JOIN dbo.SETUPGROUPMETHODTREATMENT B on(A.GroupMethodID = B.GroupMethodID)  ");
                strQuery.Append(" WHERE B.GroupMethodID is not null AND B.IsActive = '1' AND A.[GroupMethodCode] = @MethodCode ");
                strQuery.Append(" order by GroupMethodTreatmentID ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(_MethodCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_TREATMENTMETHODCODE _VT_TREATMENTMETHODCODE = ExecuteReadDataReader(query);
                    retValue.Add(_VT_TREATMENTMETHODCODE);
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

        private static VT_TREATMENTMETHODCODE ExecuteReadDataReader(System.Data.IDataReader reader)
        {

            
                VT_TREATMENTMETHODCODE _VT_TREATMENTMETHODCODE = new VT_TREATMENTMETHODCODE();
                _VT_TREATMENTMETHODCODE.MethodCode = reader["MethodCode"].ToString().TrimEnd('\0');
                _VT_TREATMENTMETHODCODE.MethodName = reader["MethodName"].ToString().TrimEnd('\0');
                _VT_TREATMENTMETHODCODE.SUFFIX = ADOUtil.GetIntFromQuery(reader["SUFFIX"].ToString());
                _VT_TREATMENTMETHODCODE.StdPrice1 = ADOUtil.GetDoubleFromQuery(reader["StdPrice1"].ToString());
                _VT_TREATMENTMETHODCODE.TreatmentCode = reader["TreatmentCode"].ToString().TrimEnd('\0');
                _VT_TREATMENTMETHODCODE.TreatmentName = reader["TreatmentName"].ToString().TrimEnd('\0');
                return _VT_TREATMENTMETHODCODE;
          

        }

    }
}
