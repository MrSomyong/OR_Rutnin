using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
namespace DAL
{
    class DAVT_TREATMENTCODE_SPLITDF : DataAccess
    {
        public DAVT_TREATMENTCODE_SPLITDF() { }
        public DAVT_TREATMENTCODE_SPLITDF(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAVT_TREATMENTCODE_SPLITDF(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAVT_TREATMENTCODE_SPLITDF(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal VT_TREATMENTCODE_SPLITDF GetTreatmentCodeSplitDFByKey(string treatmentCode)
        {
            VT_TREATMENTCODE_SPLITDF treatment = new VT_TREATMENTCODE_SPLITDF();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_TREATMENTCODE_SPLITDF ");
                strQuery.Append(" where TreatmentCode = @treatmentCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("treatmentCode", IDbType.VarChar, DBNullConvert.From(treatmentCode)));
               
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    treatment.TreatmentCode = query["TreatmentCode"].ToString().TrimEnd('\0');
                    treatment.TreatmentName  = query["TreatmentName"].ToString().TrimEnd('\0');
                    treatment.DFTreatmentCode = query["DFTreatmentCode"].ToString().TrimEnd('\0');
                    treatment.DFTreatmentName = query["DFTreatmentName"].ToString().TrimEnd('\0');
                    treatment.StdPrice1 = ADOUtil.GetDoubleFromQuery(query["StdPrice1"].ToString());
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return treatment;
        }


        internal VT_TREATMENTCODE_SPLITDF GetTreatmentCodeSplitDFByAllKey(string treatmentCode)
        {
            VT_TREATMENTCODE_SPLITDF treatment = new VT_TREATMENTCODE_SPLITDF();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from VT_TREATMENTCODE_SPLITDF ");
                strQuery.Append(" where TreatmentCode = @treatmentCode");
                strQuery.Append(" or DFTreatmentCode = @treatmentCode");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("treatmentCode", IDbType.VarChar, DBNullConvert.From(treatmentCode)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                if (query.Read())
                {
                    treatment.TreatmentCode = query["TreatmentCode"].ToString().TrimEnd('\0');
                    treatment.TreatmentName = query["TreatmentName"].ToString().TrimEnd('\0');
                    treatment.DFTreatmentCode = query["DFTreatmentCode"].ToString().TrimEnd('\0');
                    treatment.DFTreatmentName = query["DFTreatmentName"].ToString().TrimEnd('\0');
                    treatment.StdPrice1 = ADOUtil.GetDoubleFromQuery(query["StdPrice1"].ToString());
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return treatment;
        }




    }
}
