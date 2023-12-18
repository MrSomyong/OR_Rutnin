using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using DAL.Info;

namespace DAL
{
    class DATREATMENT_TEMP : DataAccess
    {
        DatabaseInfo appConnDBInfo = null;

        public DATREATMENT_TEMP() { }
        public DATREATMENT_TEMP(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DATREATMENT_TEMP(DatabaseInfo dbInfo, DatabaseInfo appConnDBInfo) { this.DbInfo = dbInfo; this.appConnDBInfo = appConnDBInfo ; }
        public DATREATMENT_TEMP(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DATREATMENT_TEMP(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        

        internal List<TREATMENT> GetTREATMENTByGroupMethodCode(TREATMENT treatment)
        {
            List<TREATMENT> retValue = new List<TREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE ,ac.* from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE left join dbo.VT_ACTIVITYCODE ac on t.CHARGECODE = ac.Activity ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                strQuery.Append(" and t.CXLDATETIME IS NULL"); 
                strQuery.Append(" and (t.GROUPREQUESTCODE IS NOT NULL or t.GROUPREQUESTCODE <> '') ");
                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , ISNULL(NULLIF(m.MEDICINE_THAINAME, ''), m.MEDICINE_ENGLISHNAME) as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE ,ac.*  from VT_IOR_VNMEDICINE m  left join dbo.VT_ACTIVITYCODE ac on m.CHARGECODE = ac.Activity ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.SUFFIX = @SUFFIX");
                strQuery.Append(" and m.CXLDATETIME IS NULL ");


                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(treatment.SUFFIX, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    TREATMENT _treatment = new TREATMENT();
                    _treatment.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    _treatment.VN = query["VN"].ToString();
                    _treatment.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    _treatment.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    _treatment.ITEMCODE = query["ITEMCODE"].ToString();
                    _treatment.ITEMNAME = query["ITEMNAME"].ToString();
                    _treatment.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    _treatment.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    _treatment.GroupType = query["GroupType"].ToString();
                    _treatment.GroupMethodCode = query["GROUPREQUESTCODE"].ToString();
                    _treatment.GroupMethodInfo = new DASETUPGROUPMETHOD(appConnDBInfo).GetSETUPGROUPMETHODByKey(_treatment.GroupMethodCode);
                    _treatment.CHARGECODE = query["CHARGECODE"].ToString();
                    _treatment.ActivityName = query["ActivityName"].ToString();

                    retValue.Add(_treatment);
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

        internal List<TREATMENT> GetAllItemCharge(TREATMENT treatment)
        {
            List<TREATMENT> retValue = new List<TREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT tm.* , ac.ActivityName ");
                strQuery.Append(" FROM ( ");
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE  from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = 0");
                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , CASE WHEN m.MEDICINE_THAINAME <> '' and  m.MEDICINE_THAINAME IS NOT NULL THEN m.MEDICINE_THAINAME ELSE m.MEDICINE_ENGLISHNAME END as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE from VT_IOR_VNMEDICINE m ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.SUFFIX = @SUFFIX");
                strQuery.Append(" and m.CXLDATETIME IS NULL ");
                strQuery.Append(" ) tm left join  dbo.VT_ACTIVITYCODE ac on tm.CHARGECODE = ac.Activity ");
                strQuery.Append(" order by ITEMCODE");


                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(treatment.SUFFIX, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    TREATMENT _treatment = new TREATMENT();
                    _treatment.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    _treatment.VN = query["VN"].ToString();
                    _treatment.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    _treatment.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    _treatment.ITEMCODE = query["ITEMCODE"].ToString();
                    _treatment.ITEMNAME = query["ITEMNAME"].ToString();
                    _treatment.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    _treatment.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    _treatment.GroupType = query["GroupType"].ToString();
                    _treatment.GroupMethodCode = query["GROUPREQUESTCODE"].ToString();
                    _treatment.GroupMethodInfo = new DASETUPGROUPMETHOD(appConnDBInfo).GetSETUPGROUPMETHODByKey(_treatment.GroupMethodCode);
                    _treatment.CHARGECODE = query["CHARGECODE"].ToString();
                    _treatment.ActivityName = query["ActivityName"].ToString();

                    retValue.Add(_treatment);
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

        internal List<TREATMENT> GetAllItemChargeAll(TREATMENT treatment)
        {
            List<TREATMENT> retValue = new List<TREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT tm.* , ac.ActivityName ");
                strQuery.Append(" FROM ( ");
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE  from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = 0");
                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , CASE WHEN m.MEDICINE_THAINAME <> '' and  m.MEDICINE_THAINAME IS NOT NULL THEN m.MEDICINE_THAINAME ELSE m.MEDICINE_ENGLISHNAME END as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE from VT_IOR_VNMEDICINE m ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.CXLDATETIME IS NULL ");
                strQuery.Append(" ) tm left join  dbo.VT_ACTIVITYCODE ac on tm.CHARGECODE = ac.Activity ");
                strQuery.Append(" order by ITEMCODE");


                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    TREATMENT _treatment = new TREATMENT();
                    _treatment.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    _treatment.VN = query["VN"].ToString();
                    _treatment.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    _treatment.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    _treatment.ITEMCODE = query["ITEMCODE"].ToString();
                    _treatment.ITEMNAME = query["ITEMNAME"].ToString();
                    _treatment.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    _treatment.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    _treatment.GroupType = query["GroupType"].ToString();
                    _treatment.GroupMethodCode = query["GROUPREQUESTCODE"].ToString();
                    _treatment.GroupMethodInfo = new DASETUPGROUPMETHOD(appConnDBInfo).GetSETUPGROUPMETHODByKey(_treatment.GroupMethodCode);
                    _treatment.CHARGECODE = query["CHARGECODE"].ToString();
                    _treatment.ActivityName = query["ActivityName"].ToString();

                    retValue.Add(_treatment);
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

        internal List<TREATMENT> GetAllTREATMENT(TREATMENT treatment , bool isDeleted)
        {
            List<TREATMENT> retValue = new List<TREATMENT>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" SELECT tm.* , ac.ActivityName ");
                strQuery.Append(" FROM ( ");
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE , t.CXLDATETIME , 0.0 as UNITPRICE from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                if (isDeleted == false)
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , CASE WHEN m.MEDICINE_THAINAME <> '' and  m.MEDICINE_THAINAME IS NOT NULL THEN m.MEDICINE_THAINAME ELSE m.MEDICINE_ENGLISHNAME END as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE , m.CXLDATETIME , m.UNITPRICE ");
                strQuery.Append(" from VT_IOR_VNMEDICINE m ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.SUFFIX = @SUFFIX");
                if (isDeleted == false)
                    strQuery.Append(" and m.CXLDATETIME IS NULL ");
                strQuery.Append(" ) tm left join  dbo.VT_ACTIVITYCODE ac on tm.CHARGECODE = ac.Activity  ");
                strQuery.Append(" order by ITEMCODE");


                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(treatment.SUFFIX, false)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    TREATMENT _treatment = new TREATMENT();
                    _treatment.VISITDATE = ADOUtil.GetDateFromQuery(query["VISITDATE"].ToString());
                    _treatment.VN = query["VN"].ToString();
                    _treatment.SUFFIX = ADOUtil.GetIntFromQuery(query["SUFFIX"].ToString());
                    _treatment.SUBSUFFIX = ADOUtil.GetIntFromQuery(query["SUBSUFFIX"].ToString());
                    _treatment.ITEMCODE = query["ITEMCODE"].ToString();
                    _treatment.ITEMNAME = query["ITEMNAME"].ToString();
                    _treatment.AMT = ADOUtil.GetDoubleFromQuery(query["AMT"].ToString());
                    _treatment.QTY = ADOUtil.GetDoubleFromQuery(query["QTY"].ToString());
                    _treatment.GroupType = query["GroupType"].ToString();
                    _treatment.GroupMethodCode = query["GROUPREQUESTCODE"].ToString();
                    _treatment.GroupMethodInfo = new DASETUPGROUPMETHOD(appConnDBInfo).GetSETUPGROUPMETHODByKey(_treatment.GroupMethodCode);
                    _treatment.CHARGECODE = query["CHARGECODE"].ToString();
                    _treatment.IsDeleted = query["CXLDATETIME"] == DBNull.Value ? true : false;
                    _treatment.ActivityName = query["ActivityName"].ToString();
                    _treatment.UNITPRICE = ADOUtil.GetDoubleFromQuery(query["UNITPRICE"].ToString());

                    if (_treatment.IsDeleted == true)
                    {
                        retValue.Add(_treatment);
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



        internal double GetTotalTreatmentPrice(TREATMENT treatment)
        {
            double retVal = default(double);
            try
            {
                StringBuilder strQuery = new StringBuilder();


                strQuery.Append(" SELECT SUM(AMT) as Total ");
                strQuery.Append(" FROM ( ");
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE  from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.SUFFIX = @SUFFIX");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = 0 ");
                
                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , CASE WHEN m.MEDICINE_THAINAME <> '' and  m.MEDICINE_THAINAME IS NOT NULL THEN m.MEDICINE_THAINAME ELSE m.MEDICINE_ENGLISHNAME END as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE from VT_IOR_VNMEDICINE m ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.SUFFIX = @SUFFIX");
                strQuery.Append(" and m.CXLDATETIME IS NULL ");
                strQuery.Append(" ) tm ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));
                parameter.Add(new IParameter("SUFFIX", IDbType.Int, DBNullConvert.From(treatment.SUFFIX, false)));
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


        internal double GetTotalTreatmentPriceAll(TREATMENT treatment)
        {
            double retVal = default(double);
            try
            {
                StringBuilder strQuery = new StringBuilder();


                strQuery.Append(" SELECT SUM(AMT) as Total ");
                strQuery.Append(" FROM ( ");
                strQuery.Append(" select t.VISITDATE , t.VN , t.SUFFIX , t.SUBSUFFIX , t.TREATMENTCODE as ITEMCODE , t.TREATMENTNAME as ITEMNAME , t.AMT, t.QTY , t.GROUPREQUESTCODE , 'Treatment' as GroupType , t.CHARGECODE  from VT_IOR_VNTREAT t ");
                strQuery.Append(" left join VT_TREATMENTCODE tc on t.TREATMENTCODE = tc.CODE ");
                strQuery.Append(" where  t.VN = @VN");
                strQuery.Append(" and t.VISITDATE = @VISITDATE");
                strQuery.Append(" and t.CXLDATETIME IS NULL");
                strQuery.Append(" and tc.DF = 0 ");

                //strQuery.Append(" order by SUBSUFFIX");
                strQuery.Append(" union ");
                strQuery.Append(" select m.VISITDATE , m.VN , m.SUFFIX , m.SUBSUFFIX , m.MEDICINECODE as ITEMCODE , CASE WHEN m.MEDICINE_THAINAME <> '' and  m.MEDICINE_THAINAME IS NOT NULL THEN m.MEDICINE_THAINAME ELSE m.MEDICINE_ENGLISHNAME END as ITEMNAME , m.AMT, m.QTY , '' as GROUPREQUESTCODE , 'Medicine' as GroupType , m.CHARGECODE from VT_IOR_VNMEDICINE m ");
                strQuery.Append(" where  m.VN = @VN");
                strQuery.Append(" and m.VISITDATE = @VISITDATE ");
                strQuery.Append(" and m.CXLDATETIME IS NULL ");
                strQuery.Append(" ) tm ");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("VN", IDbType.VarChar, DBNullConvert.From(treatment.VN)));
                parameter.Add(new IParameter("VISITDATE", IDbType.DateTime, DBNullConvert.From(treatment.VISITDATE)));
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
