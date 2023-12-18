using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;

namespace DAL
{
    class DAVT_VNTREAT_TEMP : DataAccess
    {
        DatabaseInfo AppConnDBInfo = null;

        public DAVT_VNTREAT_TEMP() { }
        public DAVT_VNTREAT_TEMP(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_VNTREAT_TEMP(DatabaseInfo dbInfo, DatabaseInfo appConnDBInfo) { this.DbInfo = dbInfo; this.AppConnDBInfo = appConnDBInfo; }
        public DAVT_VNTREAT_TEMP(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_VNTREAT_TEMP(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_VNTREAT_TEMP> SearchByKey(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            List<VT_VNTREAT_TEMP> retValue = new List<VT_VNTREAT_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.*,ac.*  from dbo.VT_IOR_VNTREAT t  left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" order by SUBSUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.ActivityName = query["ActivityName"].ToString();
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    VT_VNTREAT.GROUPREQUESTCODE = query["GROUPREQUESTCODE"].ToString();
                    VT_VNTREAT.GroupMethodInfo = !string.IsNullOrEmpty(VT_VNTREAT.GROUPREQUESTCODE) ? new DASETUPGROUPMETHOD(AppConnDBInfo).GetSETUPGROUPMETHODByKey(VT_VNTREAT.GROUPREQUESTCODE) : new Info.SETUPGROUPMETHOD();
                    retValue.Add(VT_VNTREAT);
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


        internal List<VT_VNTREAT_TEMP> SearchByKey(VT_VNTREAT_TEMP _VT_VNTREAT , bool isDF)
        {
            List<VT_VNTREAT_TEMP> retValue = new List<VT_VNTREAT_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.*,ac.*  from dbo.VT_IOR_VNTREAT t ");
                strQuery.Append(" left join dbo.VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = @DF");
                strQuery.Append(" order by SUBSUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("DF", IDbType.Bit, Convert.ToBoolean(isDF)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.ActivityName = query["ActivityName"].ToString();
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    retValue.Add(VT_VNTREAT);
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


        internal List<VT_VNTREAT_TEMP> DFTreatmentChargeAll(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            List<VT_VNTREAT_TEMP> retValue = new List<VT_VNTREAT_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.*,ac.*  from dbo.VT_IOR_VNTREAT t ");
                strQuery.Append(" left join dbo.VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = 1");
                strQuery.Append(" order by SUBSUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.ActivityName = query["ActivityName"].ToString();
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    retValue.Add(VT_VNTREAT);
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

        internal List<VT_VNTREAT_TEMP> GetAllVNTreatByKey(VT_VNTREAT_TEMP _VT_VNTREAT,bool isDeleted)
        {
            List<VT_VNTREAT_TEMP> retValue = new List<VT_VNTREAT_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.*,ac.*  from dbo.VT_IOR_VNTREAT t left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                if (isDeleted == false)
                    strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" order by SUBSUFFIX");


                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.ActivityName = query["ActivityName"].ToString();
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.CHARGEAMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    VT_VNTREAT.GROUPREQUESTCODE = query["GROUPREQUESTCODE"].ToString();
                    VT_VNTREAT.GroupMethodInfo = !string.IsNullOrEmpty(VT_VNTREAT.GROUPREQUESTCODE) ? new DASETUPGROUPMETHOD(AppConnDBInfo).GetSETUPGROUPMETHODByKey(VT_VNTREAT.GROUPREQUESTCODE) : new Info.SETUPGROUPMETHOD();
                    VT_VNTREAT.IsDeleted = query["CXLDATETIME"] == DBNull.Value ? true : false;

                    if (VT_VNTREAT.IsDeleted == true)
                    {
                        retValue.Add(VT_VNTREAT);
                    }
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

        internal List<VT_VNTREAT_TEMP> GetAllVNTreatByKey(VT_VNTREAT_TEMP _VT_VNTREAT, bool isDeleted, bool isDF)
        {
            List<VT_VNTREAT_TEMP> retValue = new List<VT_VNTREAT_TEMP>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.*,ac.*  from dbo.VT_IOR_VNTREAT t ");
                strQuery.Append(" left join dbo.VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                if (isDeleted == false)
                    strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = @DF");
                strQuery.Append(" order by SUBSUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("DF", IDbType.Bit, Convert.ToBoolean(isDF)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.ActivityName = query["ActivityName"].ToString();
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    VT_VNTREAT.IsDeleted = query["CXLDATETIME"] == DBNull.Value ? true : false;

                    if (VT_VNTREAT.IsDeleted == true)
                    {
                        retValue.Add(VT_VNTREAT);
                    }
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

        internal VT_VNTREAT_TEMP GetVT_VNTREATByKey(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            VT_VNTREAT_TEMP VT_VNTREAT = new VT_VNTREAT_TEMP();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select  a.*  from dbo.VT_IOR_VNTREAT a ");
                strQuery.Append(" where a.VN = @VN");
                strQuery.Append(" and a.VISITDATE = @VISITDATE");
                strQuery.Append(" and a.SUFFIX = @SUFFIX");
                strQuery.Append(" and a.SUBSUFFIX = @SUBSUFFIX");
                strQuery.Append(" and a.TREATMENTCODE = @TREATMENTCODE");
                strQuery.Append(" and a.CHARGECODE = @CHARGECODE");
                strQuery.Append(" and a.CXLDATETIME IS NULL");

                ConnectDB();
                
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUBSUFFIX, false)));
                parameter.Add(new IParameter("TREATMENTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.TREATMENTCODE)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CHARGECODE)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                   
                    VT_VNTREAT.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNTREAT.VN = query["VN"].ToString();
                    VT_VNTREAT.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNTREAT.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    VT_VNTREAT.TREATMENTCODE = query["TREATMENTCODE"].ToString();
                    VT_VNTREAT.TREATMENTNAME = query["TREATMENTNAME"].ToString();
                    VT_VNTREAT.CHARGECODE = query["CHARGECODE"].ToString().TrimEnd('\0');
                    VT_VNTREAT.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNTREAT.DOCTORNAME = query["DOCTORNAME"].ToString();
                    VT_VNTREAT.CLINIC = query["CLINIC"].ToString();
                    VT_VNTREAT.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNTREAT.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    VT_VNTREAT.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    VT_VNTREAT.PAIDAMT = ADOUtil.GetDoubleFromQuery(query["PAIDAMT"].ToString());
                    VT_VNTREAT.ENTRYBYUSERCODE = query["ENTRYBYUSERCODE"].ToString();
                    VT_VNTREAT.ENTRYBYUSERNAME = query["ENTRYBYUSERNAME"].ToString();
                    VT_VNTREAT.MAKEDATETIME = ADOUtil.GetDateFromQuery(query["MAKEDATETIME"].ToString());
                    VT_VNTREAT.REMARKS = query["REMARKS"].ToString();
                    VT_VNTREAT.TREATMENTENTRYSTYLE = ADOUtil.GetIntFromQuery(query["TREATMENTENTRYSTYLE"].ToString());
                    VT_VNTREAT.TYPEOFCHARGE = ADOUtil.GetIntFromQuery(query["TYPEOFCHARGE"].ToString());
                    VT_VNTREAT.GROUPREQUESTCODE = query["GROUPREQUESTCODE"].ToString();
                    VT_VNTREAT.TREATMENTDATETIMEFROM = query["TREATMENTDATETIMEFROM"] != DBNull.Value ? ADOUtil.GetDateFromQuery(query["TREATMENTDATETIMEFROM"].ToString()) : VT_VNTREAT.TREATMENTDATETIMEFROM = null;
                    VT_VNTREAT.TREATMENTDATETIMETO = query["TREATMENTDATETIMETO"] != DBNull.Value ? ADOUtil.GetDateFromQuery(query["TREATMENTDATETIMETO"].ToString()) : VT_VNTREAT.TREATMENTDATETIMETO = null;
                    VT_VNTREAT.TIMETYPE = query["TIMETYPE"] != DBNull.Value ? ADOUtil.GetIntFromQuery(query["TIMETYPE"].ToString()) : VT_VNTREAT.TIMETYPE = null;


                }
                query.Close();
                command.Dispose();
                DisconnectDB();
               
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return VT_VNTREAT;
        }

        internal ReturnValue DELVNTREAT(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE dbo.IOR_VNTREAT ");
                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : true);
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

        internal ReturnValue CXLVNTREAT(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE dbo.IOR_VNTREAT SET ");
                sbQuery.Append(" CXLDATETIME = GETDATE()");
                sbQuery.Append(" ,CXLBYUSERCODE = @CXLBYUSERCODE");
                sbQuery.Append(" ,REVERSE = 1 ");
                sbQuery.Append(" ,TYPEOFCHARGE = 3 ");
                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");
                sbQuery.Append(" AND SUBSUFFIX = @SUBSUFFIX");
                sbQuery.Append(" AND TREATMENTCODE = @TREATMENTCODE");
                //sbQuery.Append(" AND CHARGECODE = @CHARGECODE");
                sbQuery.Append(" AND CXLDATETIME IS NULL");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("CXLBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CXLBYUSERCODE)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUBSUFFIX, false)));
                parameter.Add(new IParameter("TREATMENTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.TREATMENTCODE)));
                //parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CHARGECODE)));
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

        internal ReturnValue UpdateVNTREAT(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE dbo.IOR_VNTREAT SET ");
                sbQuery.Append("  AMT = @AMT");
                sbQuery.Append(" ,QTY = @QTY");
                sbQuery.Append(" ,TYPEOFCHARGE = @TYPEOFCHARGE");
                sbQuery.Append(" ,REVERSE = @REVERSE");
                sbQuery.Append(" ,GROUPREQUESTCODE = @GROUPREQUESTCODE");
                sbQuery.Append(" ,PAIDFLAG = @PAIDFLAG");
                sbQuery.Append(" ,REMARKS = @REMARKS");
                sbQuery.Append(" ,ENTRYBYUSERCODE = @ENTRYBYUSERCODE");
                sbQuery.Append(" ,MAKEDATETIME = GETDATE()");
                sbQuery.Append(" ,DOCTOR = @DOCTOR");
                sbQuery.Append(" ,TREATMENTDATETIMEFROM = @TREATMENTDATETIMEFROM");
                sbQuery.Append(" ,TREATMENTDATETIMETO = @TREATMENTDATETIMETO");
                
                sbQuery.Append(" WHERE VISITDATE = @VISITDATE");
                sbQuery.Append(" AND VN = @VN");
                sbQuery.Append(" AND SUFFIX = @SUFFIX");
                sbQuery.Append(" AND SUBSUFFIX = @SUBSUFFIX");
                sbQuery.Append(" AND TREATMENTCODE = @TREATMENTCODE");
                sbQuery.Append(" AND CHARGECODE = @CHARGECODE");
                sbQuery.Append(" AND CXLDATETIME IS NULL");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUBSUFFIX, false)));
                parameter.Add(new IParameter("TREATMENTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.TREATMENTCODE)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CHARGECODE)));
                parameter.Add(new IParameter("REMARKS", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.REMARKS)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_VT_VNTREAT.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_VT_VNTREAT.QTY)));
                parameter.Add(new IParameter("TYPEOFCHARGE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.TYPEOFCHARGE, false)));
                parameter.Add(new IParameter("REVERSE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.REVERSE, false)));
                parameter.Add(new IParameter("GROUPREQUESTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.GROUPREQUESTCODE)));
                parameter.Add(new IParameter("ENTRYBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.ENTRYBYUSERCODE)));
                parameter.Add(new IParameter("PAIDFLAG", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.PAIDFLAG, false)));
                parameter.Add(new IParameter("DOCTOR", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.DOCTOR)));
                parameter.Add(new IParameter("TREATMENTDATETIMEFROM", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.TREATMENTDATETIMEFROM)));
                parameter.Add(new IParameter("TREATMENTDATETIMETO", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.TREATMENTDATETIMETO)));
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

        internal bool CheckDup(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            bool retVal = false;
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from dbo.IOR_VNTREAT ");
                strQuery.Append(" WHERE VISITDATE = @VISITDATE");
                strQuery.Append(" AND VN = @VN");
                strQuery.Append(" AND SUFFIX = @SUFFIX");
                strQuery.Append(" AND TREATMENTCODE = @TREATMENTCODE");
                strQuery.Append(" AND CHARGECODE = @CHARGECODE");
                strQuery.Append(" AND CXLDATETIME IS NULL");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CHARGECODE)));
                parameter.Add(new IParameter("TREATMENTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.TREATMENTCODE)));
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


        internal ReturnValue Insert(VT_VNTREAT_TEMP _VT_VNTREAT)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                sbInsert.Append("Declare @i int; ");

                sbInsert.Append("Select @i = coalesce(MAX(SUBSUFFIX), 0) + 1 FROM IOR_VNTREAT ");
                sbInsert.Append("where VISITDATE = @VISITDATE and VN = @VN and SUFFIX = @SUFFIX ; ");
                sbInsert.Append(" INSERT INTO  dbo.IOR_VNTREAT (");
                sbValue.Append(" VALUES(");

                sbInsert.Append("VISITDATE");
                sbValue.Append("@VISITDATE");

                sbInsert.Append(", VN");
                sbValue.Append(",@VN");

                sbInsert.Append(", SUFFIX");
                sbValue.Append(",@SUFFIX");

                sbInsert.Append(", SUBSUFFIX" );
                sbValue.Append(",@i" );

                sbInsert.Append(", TREATMENTCODE");
                sbValue.Append(",@TREATMENTCODE");

                sbInsert.Append(", CHARGECODE");
                sbValue.Append(",@CHARGECODE");

                sbInsert.Append(", DOCTOR");
                sbValue.Append(",@DOCTOR");

                sbInsert.Append(", AMT");
                sbValue.Append(",@AMT");

                sbInsert.Append(", QTY");
                sbValue.Append(",@QTY");

                sbInsert.Append(", MAKEDATETIME");
                sbValue.Append(", GETDATE() ");

                sbInsert.Append(", TREATMENTENTRYSTYLE");
                sbValue.Append(", @TREATMENTENTRYSTYLE");

                sbInsert.Append(", ENTRYBYUSERCODE");
                sbValue.Append(", @ENTRYBYUSERCODE");

                sbInsert.Append(", TYPEOFCHARGE");
                sbValue.Append(", @TYPEOFCHARGE");

                sbInsert.Append(", GROUPREQUESTCODE");
                sbValue.Append(", @GROUPREQUESTCODE");

                sbInsert.Append(", PAIDFLAG");
                sbValue.Append(", @PAIDFLAG");

                sbInsert.Append(", REVERSE");
                sbValue.Append(",@REVERSE");

                sbInsert.Append(", REMARKS");
                sbValue.Append(",@REMARKS");

                sbInsert.Append(", TIMETYPE");
                sbValue.Append(", @TIMETYPE");

                sbInsert.Append(", TREATMENTDATETIMEFROM");
                sbValue.Append(", @TREATMENTDATETIMEFROM");

                sbInsert.Append(", TREATMENTDATETIMETO");
                sbValue.Append(", @TREATMENTDATETIMETO");

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX,false)));
                parameter.Add(new IParameter("SUBSUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUBSUFFIX,false)));
                parameter.Add(new IParameter("TREATMENTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.TREATMENTCODE)));
                parameter.Add(new IParameter("CHARGECODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.CHARGECODE)));
                parameter.Add(new IParameter("AMT", IDbType.Float, DBNullConvert.From(_VT_VNTREAT.AMT)));
                parameter.Add(new IParameter("QTY", IDbType.Float, DBNullConvert.From(_VT_VNTREAT.QTY)));
                parameter.Add(new IParameter("MAKEDATETIME", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.MAKEDATETIME)));
                parameter.Add(new IParameter("TREATMENTENTRYSTYLE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.TREATMENTENTRYSTYLE,false)));
                parameter.Add(new IParameter("DOCTOR", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.DOCTOR)));
                parameter.Add(new IParameter("ENTRYBYUSERCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.ENTRYBYUSERCODE)));
                parameter.Add(new IParameter("GROUPREQUESTCODE", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.GROUPREQUESTCODE)));
                parameter.Add(new IParameter("TYPEOFCHARGE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.TYPEOFCHARGE, false)));
                parameter.Add(new IParameter("PAIDFLAG", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.PAIDFLAG, false)));
                parameter.Add(new IParameter("REVERSE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.REVERSE, false)));
                parameter.Add(new IParameter("REMARKS", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.REMARKS)));
                parameter.Add(new IParameter("TIMETYPE", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.TIMETYPE,false)));
                parameter.Add(new IParameter("TREATMENTDATETIMEFROM", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.TREATMENTDATETIMEFROM)));
                parameter.Add(new IParameter("TREATMENTDATETIMETO", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.TREATMENTDATETIMETO)));
                //parameter.Add(new IParameter("i", IDbType.Int, System.Data.ParameterDirection.Output));
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


        internal double GetTotalTreatmentPrice(VT_VNTREAT_TEMP _VT_VNTREAT, int statusType)
        {
            double retVal = default(double);
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT isnull(SUM(t.AMT * (isnull(t.QTY,1))),0) as Total ");
                strQuery.Append(" FROM dbo.IOR_VNTREAT  t ");
                strQuery.Append(" LEFT JOIN dbo.VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" WHERE t.VISITDATE = @VISITDATE");
                strQuery.Append(" AND t.VN = @VN");
                strQuery.Append(" AND t.SUFFIX = @SUFFIX");
                strQuery.Append(" AND t.CXLDATETIME IS NULL");
                switch (statusType)
                {
                    case 0:
                        strQuery.Append(" AND tc.DF = 0 ");
                        break;
                    case 1:
                        strQuery.Append(" AND tc.DF = 1 ");
                        break;
                    default:
                        strQuery.Append(" AND tc.DF = tc.DF ");
                        break;
                }

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNTREAT.SUFFIX, false)));
                command = GetCommand(strQuery.ToString(), parameter);
                retVal = Convert.ToDouble(command.ExecuteScalar());
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {

            }
            return retVal;
        }




        internal double GetTotalTreatmentPriceAll(VT_VNTREAT_TEMP _VT_VNTREAT, int statusType)
        {
            double retVal = default(double);
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT isnull(SUM(t.AMT * (isnull(t.QTY,1))),0) as Total ");
                strQuery.Append(" FROM dbo.IOR_VNTREAT  t ");
                strQuery.Append(" LEFT JOIN dbo.VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" WHERE t.VISITDATE = @VISITDATE");
                strQuery.Append(" AND t.VN = @VN");
                strQuery.Append(" AND t.CXLDATETIME IS NULL");
                switch (statusType)
                {
                    case 0:
                        strQuery.Append(" AND tc.DF = 0 ");
                        break;
                    case 1:
                        strQuery.Append(" AND tc.DF = 1 ");
                        break;
                    default:
                        strQuery.Append(" AND tc.DF = tc.DF ");
                        break;
                }

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNTREAT.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNTREAT.VISITDATE)));
                command = GetCommand(strQuery.ToString(), parameter);
                retVal = Convert.ToDouble(command.ExecuteScalar());
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {

            }
            return retVal;
        }

    }
}
