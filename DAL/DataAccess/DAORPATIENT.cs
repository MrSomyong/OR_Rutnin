using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORPATIENT : DataAccess
    {
        //private static string _tblORPATIENTINFO = "ORPATIENTINFO";
        private static string _VT_PATIENTMASTER = "VT_PATIENTMASTER";
        //private static string _HN = "HN";
        private static string _PatientName = "PatientName";
        //private static string _Gender = "Gender";
        //private static string _BirthDateTime = "BirthDateTime";
        //private static string _IDCARD = "IDCARD";
        //private static string _Nationality = "Nationality";

        private static string _HN = "HN";
        private static string _HNName = "HNName"; 
        private static string _Initial = "Initial";
        private static string _FirstName = "FirstName";
        private static string _LastName = "LastName";
        private static string _BirthDateTime = "BirthDateTime";
        private static string _Age = "Age"; 
        private static string _ageyear = "ageyear";
        private static string _agemonth = "agemonth";
        private static string _Sex = "Sex";
        private static string _Gender = "Gender";        
        private static string _Ref = "Ref";
        private static string _Nationality = "Nationality";
        private static string _PictureFileName = "PictureFileName";
        private static string _PatientType = "PatientType";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAORPATIENT() { }
        public DAORPATIENT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORPATIENT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORPATIENT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORPATIENTVO> SearchByKey(string strsearch)
        {
            List<ORPATIENTVO> retValue = new List<ORPATIENTVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 50 * from " + _VT_PATIENTMASTER + " where 1=1 and (");

                strQuery.Append(" " + _HN + " like @strsearch" );
                strQuery.Append(" OR " + _FirstName + " like @strsearch");
                strQuery.Append(" OR " + _LastName + " like @strsearch");

                strQuery.Append(") ");
                strQuery.Append(" order by " + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("strsearch", IDbType.VarChar, DBNullConvert.From(strsearch + "%")));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
                    ORPATIENTVO.HN = query[_HN].ToString();
                    ORPATIENTVO.Initial = query[_Initial].ToString();
                    ORPATIENTVO.FirstName = query[_FirstName].ToString();
                    ORPATIENTVO.LastName = query[_LastName].ToString();
                    ORPATIENTVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORPATIENTVO.HNName = ORPATIENTVO.HN + " : " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORPATIENTVO.Sex = query[_Sex].ToString();
                    ORPATIENTVO.Gender = query[_Sex].ToString();
                    ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    ORPATIENTVO.Ref = query[_Ref].ToString();
                    ORPATIENTVO.PictureFileName = query[_PictureFileName].ToString();
                    ORPATIENTVO.PatientType = query[_PatientType].ToString();
                    retValue.Add(ORPATIENTVO);
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

        internal ORPATIENTVO SearchByHN(string HN)
        {
            ORPATIENTVO ORPATIENTVO = new ORPATIENTVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 1 * from " + _VT_PATIENTMASTER + " where 1=1 ");
                strQuery.Append(" and " + _HN + " = @" + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(HN)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORPATIENTVO.HN = query[_HN].ToString();
                    ORPATIENTVO.Initial = query[_Initial].ToString();
                    ORPATIENTVO.FirstName = query[_FirstName].ToString();
                    ORPATIENTVO.LastName = query[_LastName].ToString();
                    ORPATIENTVO.PatientName = ORPATIENTVO.Initial + " " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORPATIENTVO.HNName = ORPATIENTVO.HN + " : " + ORPATIENTVO.FirstName + " " + ORPATIENTVO.LastName;
                    ORPATIENTVO.Gender = query[_Sex].ToString();
                    ORPATIENTVO.Sex = query[_Sex].ToString();
                    ORPATIENTVO.BirthDateTime = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    ORPATIENTVO.Age = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    ORPATIENTVO.Nationality = query[_Nationality].ToString();
                    ORPATIENTVO.Ref = query[_Ref].ToString();
                    ORPATIENTVO.PictureFileName = query[_PictureFileName].ToString();
                    ORPATIENTVO.PatientType = query[_PatientType].ToString();

                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return ORPATIENTVO;
        }

        internal DataTable SearchByHN_DS(string HN)
        {
            DataTable table = new DataTable();

            table.Columns.Add(_HN, typeof(String));
            table.Columns.Add(_Initial, typeof(String));
            table.Columns.Add(_FirstName, typeof(String));
            table.Columns.Add(_LastName, typeof(String));
            table.Columns.Add(_PatientName, typeof(String));
            table.Columns.Add(_HNName, typeof(String));
            table.Columns.Add(_Gender, typeof(String));
            table.Columns.Add(_Sex, typeof(String));
            table.Columns.Add(_BirthDateTime, typeof(DateTime));
            table.Columns.Add(_Age, typeof(String));
            table.Columns.Add(_Nationality, typeof(String));
            table.Columns.Add(_Ref, typeof(String));


            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_PATIENTMASTER + " where 1=1 ");
                strQuery.Append(" and " + _HN + " = @" + _HN);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(HN)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DataRow row = table.NewRow();
                    row[_HN] = query[_HN].ToString();
                    row[_Initial] = query[_Initial].ToString();
                    row[_FirstName] = query[_FirstName].ToString();
                    row[_LastName] = query[_LastName].ToString();
                    row[_PatientName] = query[_Initial].ToString() + " " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    row[_HNName] = query[_HN].ToString() + " : " + query[_FirstName].ToString() + " " + query[_LastName].ToString();
                    row[_Gender] = query[_Sex].ToString();
                    row[_Sex] = query[_Sex].ToString();
                    row[_BirthDateTime] = ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString());
                    row[_Age] = ORUtils.getAge(ADOUtil.GetDateFromQuery(query[_BirthDateTime].ToString()));
                    row[_Nationality] = query[_Nationality].ToString();
                    row[_Ref] = query[_Ref].ToString();
                    table.Rows.Add(row);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return table;
        }
    }
}
