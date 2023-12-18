using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
namespace DAL
{
    class DAVT_TREATMENTCODE : DataAccess
    {
        public DAVT_TREATMENTCODE() { }
        public DAVT_TREATMENTCODE(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_TREATMENTCODE(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_TREATMENTCODE(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_TREATMENTCODE> SearchAllByKey(int isDF)
        {
            List<VT_TREATMENTCODE> retValue = new List<VT_TREATMENTCODE>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select CODE , Name , StdPrice1 from VT_TREATMENTCODE");
                strQuery.Append(" where IsOff = 0 and DF = @Df");
                strQuery.Append(" order by CODE");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("DF", IDbType.Int, DBNullConvert.From(isDF, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command); 
                while (query.Read())
                {
                    VT_TREATMENTCODE _VT_TREATMENTCODE = new VT_TREATMENTCODE();
                    _VT_TREATMENTCODE.CODE = query["CODE"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.Name = query["Name"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.StdPrice1 = ADOUtil.GetDoubleFromQuery(query["StdPrice1"].ToString());
                    retValue.Add(_VT_TREATMENTCODE);
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

       

        internal VT_TREATMENTCODE GetTreatmentCodeByKey(string  _CODE , int fixRate = 1)
        {
            VT_TREATMENTCODE _VT_TREATMENTCODE = new VT_TREATMENTCODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_TREATMENTCODE ");
                strQuery.Append(" where CODE = @CODE");
                strQuery.Append(" and IsOff = 0");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("CODE", IDbType.VarChar, DBNullConvert.From(_CODE)));
               
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    _VT_TREATMENTCODE.CODE = query["CODE"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.Name = query["Name"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.StdPrice1 = ADOUtil.GetDoubleFromQuery(query["StdPrice1"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.StdPrice2 = ADOUtil.GetDoubleFromQuery(query["StdPrice2"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.StdPrice3 = ADOUtil.GetDoubleFromQuery(query["StdPrice3"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.StdPrice4 = ADOUtil.GetDoubleFromQuery(query["StdPrice4"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.MinPrice = ADOUtil.GetDoubleFromQuery(query["MinPrice"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.MaxPrice = ADOUtil.GetDoubleFromQuery(query["MaxPrice"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Activity = query["Activity"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.ActivityName = query["ActivityName"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.IsOff = ADOUtil.GetIntFromQuery(query["IsOff"].ToString());
                    _VT_TREATMENTCODE.TreatmentStyle = ADOUtil.GetIntFromQuery(query["TreatmentStyle"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.TreatmentCategory = query["TreatmentCategory"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.FixRate = fixRate;
                    _VT_TREATMENTCODE.ZeroPrice = ADOUtil.GetIntFromQuery(query["ZeroPrice"].ToString());
                    _VT_TREATMENTCODE.TimeType = Convert.ToInt32(query["TimeType"]);
                    _VT_TREATMENTCODE.Time01 = ADOUtil.GetDoubleFromQuery(query["Time01"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Time02 = ADOUtil.GetDoubleFromQuery(query["Time02"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Time03 = ADOUtil.GetDoubleFromQuery(query["Time03"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Time04 = ADOUtil.GetDoubleFromQuery(query["Time04"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Time05 = ADOUtil.GetDoubleFromQuery(query["Time05"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Time06 = ADOUtil.GetDoubleFromQuery(query["Time06"].ToString().TrimEnd('\0'));
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return _VT_TREATMENTCODE;
        }



        internal VT_TREATMENTCODE GetTreatmentCodeByKey(string _MethodCod, string _CODE)
        {
            VT_TREATMENTCODE _VT_TREATMENTCODE = new VT_TREATMENTCODE();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                //Edit 15.02 แก้ไขอ่าน group method
                //strQuery.Append(" select top 1 m.TreatmentCode , m.TreatmentName , m.StdPrice1 , t.Activity , t.ActivityName , t.IsOff , t.TreatmentStyle , t.TreatmentCategory ");
                //strQuery.Append(" from VT_TREATMENTMETHODCODE m left join VT_TREATMENTCODE t on m.TreatmentCode = t.CODE");
                //strQuery.Append(" where m.MethodCode = @MethodCode and  t.CODE = @CODE ");
                //strQuery.Append(" and t.IsOff = 0");

                strQuery.Append(" SELECT A.[GroupMethodCode] AS MethodCode,A.[GroupMethodName] AS MethodName,B.GroupMethodTreatmentID As Suffix ");
                strQuery.Append(" ,TreatmentCode,TreatmentName,AMT AS StdPrice1,CHARGECODE as Activity,null as ActivityName,TREATMENTENTRYSTYLE as TreatmentStyle,null as TreatmentCategory ");
                strQuery.Append(" ,B.IsActive ");
                strQuery.Append(" FROM [dbo].[SETUPGROUPMETHOD] A LEFT JOIN dbo.SETUPGROUPMETHODTREATMENT B on(A.GroupMethodID = B.GroupMethodID)  ");
                strQuery.Append(" WHERE B.GroupMethodID is not null AND B.IsActive = '1' AND A.[GroupMethodCode] = @MethodCode AND TreatmentCode = @CODE ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("MethodCode", IDbType.VarChar, DBNullConvert.From(_MethodCod)));
                parameter.Add(new IParameter("CODE", IDbType.VarChar, DBNullConvert.From(_CODE)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    _VT_TREATMENTCODE.CODE = query["TreatmentCode"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.Name = query["TreatmentName"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.StdPrice1 = ADOUtil.GetDoubleFromQuery(query["StdPrice1"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.Activity = query["Activity"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.ActivityName = query["ActivityName"].ToString().TrimEnd('\0');
                    _VT_TREATMENTCODE.IsOff = 0;
                    //_VT_TREATMENTCODE.IsOff = ADOUtil.GetIntFromQuery(query["IsActive"].ToString().TrimEnd('\0'));
                    _VT_TREATMENTCODE.TreatmentStyle = ADOUtil.GetIntFromQuery(query["TreatmentStyle"].ToString());
                    _VT_TREATMENTCODE.TreatmentCategory = query["TreatmentCategory"].ToString().TrimEnd('\0');

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return _VT_TREATMENTCODE;
        }


    }
}
