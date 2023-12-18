using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ADOUtils;

namespace DAL
{
     class DAVT_VNMASTER : DataAccess
    {
        public DAVT_VNMASTER() { }
        public DAVT_VNMASTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_VNMASTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_VNMASTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<VT_VNMASTER> SearchByKey(VT_VNMASTER _VT_VNMASTER)
        {
            List<VT_VNMASTER> retValue = new List<VT_VNMASTER>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                //strQuery.Append(" select   a.*  from dbo.VT_VNMASTER a ");
                strQuery.Append(" SELECT  distinct a.VISITDATE ,  "+
                                "         a.VN ,   " +
                                "         a.HN ,   " +
                                "         a.Initial ,   " +
                                "         a.FirstName ,   " +
                                "         a.LastName ,   " +
                                "         a.VISITINDATETIME ,   " +
                                "         a.VISITOUTDATETIME ,   " +
                                "         a.SUFFIX ,   " +
                                "         a.DOCTOR ,   " +
                                "         a.DoctorName ,   " +
                                "         a.CLINIC ,   " +
                                "         a.CLINICNAME ,   " +
                                "         a.RIGHTCODE  ,  " +
                                "         a.RIGHTNAME  ,  " +
                                "         a.CloseVisitFlag  ,  " +
                                "         a.HOLDBILL    " +
                                " FROM dbo.VT_VNMASTER a ");
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_VT_VNMASTER.HN))
                {
                    strQuery.Append(" and a.HN = @HN");
                }
                if (!string.IsNullOrEmpty(_VT_VNMASTER.VISITDATE.ToString()))
                {
                    strQuery.Append(" and a.VISITDATE = @VISITDATE");
                }
                if (!string.IsNullOrEmpty(_VT_VNMASTER.VN))
                {
                    strQuery.Append(" and a.VN = @VN");
                }
                if (!string.IsNullOrEmpty(_VT_VNMASTER.DOCTOR))
                {
                    strQuery.Append(" and a.DOCTOR = @DOCTOR");
                }

                string strClinic = string.Empty;
                if (!string.IsNullOrEmpty(_VT_VNMASTER.CLINIC))
                {
                    strClinic = "'" + _VT_VNMASTER.CLINIC.Replace(",", "','") + "'";
                    strQuery.Append(string.Format(" and a.CLINIC in ({0})", strClinic));

                }
                if (_VT_VNMASTER.OutFlag != null )
                {
                    strQuery.Append(" and a.OutFlag = @OutFlag");
                }
                if (_VT_VNMASTER.CloseVisitFlag != null)
                {
                    strQuery.Append(" and a.CloseVisitFlag = @CloseVisitFlag");
                }
                strQuery.Append(" Order by VISITINDATETIME , VN , SUFFIX ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("HN", IDbType.VarChar, DBNullConvert.From(_VT_VNMASTER.HN)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMASTER.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMASTER.VISITDATE)));
                parameter.Add(new IParameter("DOCTOR", IDbType.VarChar, DBNullConvert.From(_VT_VNMASTER.DOCTOR)));
                parameter.Add(new IParameter("OutFlag", IDbType.Bit, Convert.ToBoolean(_VT_VNMASTER.OutFlag)));
                parameter.Add(new IParameter("CloseVisitFlag", IDbType.Bit, Convert.ToBoolean(_VT_VNMASTER.CloseVisitFlag)));
                command = GetCommand(strQuery.ToString(), parameter);
                command.CommandTimeout = 0;
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    VT_VNMASTER VT_VNMASTER = new VT_VNMASTER();
                    VT_VNMASTER.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNMASTER.VN = query["VN"].ToString();
                    VT_VNMASTER.HN = query["HN"].ToString();
                    VT_VNMASTER.Initial = query["Initial"].ToString();
                    VT_VNMASTER.FirstName = query["FirstName"].ToString();
                    VT_VNMASTER.LastName = query["LastName"].ToString();
                    VT_VNMASTER.VISITINDATETIME = ADOUtil.GetDateFromQuery(query["VISITINDATETIME"].ToString());
                    VT_VNMASTER.VISITOUTDATETIME = ADOUtil.GetDateFromQuery(query["VISITOUTDATETIME"].ToString());
                    VT_VNMASTER.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNMASTER.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNMASTER.DoctorName = query["DoctorName"].ToString();
                    VT_VNMASTER.CLINIC = query["CLINIC"].ToString();
                    VT_VNMASTER.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNMASTER.RIGHTCODE =  query["RIGHTCODE"].ToString();
                    VT_VNMASTER.RIGHTNAME = query["RIGHTNAME"].ToString();
                    VT_VNMASTER.HoldBill = query["HOLDBILL"] == DBNull.Value ? false : Convert.ToBoolean(query["HOLDBILL"]);
                    VT_VNMASTER.CloseVisitFlag = query["CloseVisitFlag"] == DBNull.Value ? false : Convert.ToBoolean(query["CloseVisitFlag"]);
                    if (query["CloseVisitFlag"].ToString() == "1")
                    {
                        VT_VNMASTER.Close = "Y";
                    }
                    else
                    {
                        VT_VNMASTER.Close = "N";
                    }

                    retValue.Add(VT_VNMASTER);
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

        internal VT_VNMASTER GetVNDetailByKey(VT_VNMASTER _VT_VNMASTER)
        {
            VT_VNMASTER VT_VNMASTER = new VT_VNMASTER();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_VNMASTER where 1=1 ");
                strQuery.Append(" and HN = @HN");
                strQuery.Append(" and VN = @VN");
                strQuery.Append(" and VISITDATE = @VISITDATE");
                strQuery.Append(" and SUFFIX = @SUFFIX");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("HN", IDbType.VarChar, DBNullConvert.From(_VT_VNMASTER.HN)));
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(_VT_VNMASTER.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(_VT_VNMASTER.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(_VT_VNMASTER.SUFFIX,false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {

                    VT_VNMASTER.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    VT_VNMASTER.VN = query["VN"].ToString();
                    VT_VNMASTER.HN = query["HN"].ToString();
                    VT_VNMASTER.Initial = query["Initial"].ToString();
                    VT_VNMASTER.FirstName = query["FirstName"].ToString();
                    VT_VNMASTER.LastName = query["LastName"].ToString();
                    VT_VNMASTER.VISITINDATETIME = ADOUtil.GetDateFromQuery(query["VISITINDATETIME"].ToString());
                    VT_VNMASTER.VISITOUTDATETIME = ADOUtil.GetDateFromQuery(query["VISITOUTDATETIME"].ToString());
                    VT_VNMASTER.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    VT_VNMASTER.DOCTOR = query["DOCTOR"].ToString();
                    VT_VNMASTER.DoctorName = query["DoctorName"].ToString();
                    VT_VNMASTER.CLINIC = query["CLINIC"].ToString();
                    VT_VNMASTER.CLINICNAME = query["CLINICNAME"].ToString();
                    VT_VNMASTER.RIGHTCODE = query["RIGHTCODE"].ToString();
                    VT_VNMASTER.RIGHTNAME = query["RIGHTNAME"].ToString();
                    VT_VNMASTER.HoldBill = query["HOLDBILL"] == DBNull.Value ? false : Convert.ToBoolean(query["HOLDBILL"]);
                    VT_VNMASTER.TreatmentPriceType = query["TreatmentPriceType"].ToString();
                    VT_VNMASTER.MedicinePriceType = query["MedicinePriceType"].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            { 
                throw exc;
            }
            return VT_VNMASTER;
        }

        
    }
}
