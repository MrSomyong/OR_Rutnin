using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DADOCTORMASTER : DataAccess
    {

        private static string _VT_DOCTORMASTER = "VT_DOCTORMASTER";
        private static string _VT_DOCTORTREATMENT = "VT_DOCTORTREATMENT";


        private static string _DOCTOR = "DOCTOR";
        private static string _DoctorName = "DoctorName";
        private static string _EDUCATIONSTANDARD = "EDUCATIONSTANDARD";
        private static string _CHEQUECLIENTNAME = "CHEQUECLIENTNAME";
        private static string _DoctorType = "DoctorType";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DADOCTORMASTER() { }
        public DADOCTORMASTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DADOCTORMASTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DADOCTORMASTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOCTORMASTERVO> SearchAll()
        {
            List<DOCTORMASTERVO> retValue = new List<DOCTORMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORMASTER);

                strQuery.Append(" where " + _DoctorName + " <> ''");
                strQuery.Append(" order by " + _DoctorName);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = query[_DOCTOR].ToString();
                    DOCTORMASTERVO.DoctorName = query[_DoctorName].ToString();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    retValue.Add(DOCTORMASTERVO);
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

        internal DOCTORMASTERVO SearchByCode(string Code)
        {
            DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORMASTER);
                strQuery.Append(" where " + _DoctorName + " <> ''");
                strQuery.Append(" and " + _DOCTOR + " = @" + _DOCTOR);

                strQuery.Append(" order by " + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_DOCTOR, IDbType.VarChar, DBNullConvert.From(Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCTORMASTERVO.DOCTOR = query[_DOCTOR].ToString();
                    DOCTORMASTERVO.DoctorName = query[_DoctorName].ToString();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    DOCTORMASTERVO.CHEQUECLIENTNAME = query[_CHEQUECLIENTNAME].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return DOCTORMASTERVO;
        }

        internal List<DOCTORMASTERVO> SearchByKey(DOCTORMASTERVO _DOCTORMASTERVO)
        {
            List<DOCTORMASTERVO> LstDOCTORMASTERVO = new List<DOCTORMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORMASTER);
                strQuery.Append(" where " + _DoctorName + " <> ''");

                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.DOCTOR))
                {
                    strQuery.Append(" and " + _DOCTOR + " = @" + _DOCTOR);
                }
                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.EDUCATIONSTANDARD))
                {
                    strQuery.Append(" and " + _EDUCATIONSTANDARD + " = @" + _EDUCATIONSTANDARD);
                }
                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.DoctorName))
                {
                    strQuery.Append(" and " + _DoctorName + " Like @" + _DoctorName);
                }
                strQuery.Append(" order by " + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_DOCTOR, IDbType.VarChar, DBNullConvert.From(_DOCTORMASTERVO.DOCTOR)));
                parameter.Add(new IParameter(_EDUCATIONSTANDARD, IDbType.VarChar, DBNullConvert.From(_DOCTORMASTERVO.EDUCATIONSTANDARD)));
                parameter.Add(new IParameter(_DoctorName, IDbType.VarChar, DBNullConvert.From("%" + _DOCTORMASTERVO.DoctorName + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = query[_DOCTOR].ToString();
                    DOCTORMASTERVO.DoctorName = query[_DoctorName].ToString();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    LstDOCTORMASTERVO.Add(DOCTORMASTERVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return LstDOCTORMASTERVO;
        }

        internal List<DOCTORMASTERVO> SearchDDL(DOCTORMASTERVO _DOCTORMASTERVO)
        {
            List<DOCTORMASTERVO> LstDOCTORMASTERVO = new List<DOCTORMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORMASTER);
                strQuery.Append(" where " + _DoctorName + " <> ''");
                strQuery.Append(" and( " + _DOCTOR + " Like @" + _DOCTOR);
                strQuery.Append(" or " + _DoctorName + " Like @" + _DoctorName);
                strQuery.Append(" ) and " + _EDUCATIONSTANDARD + " = @" + _EDUCATIONSTANDARD);
                strQuery.Append(" order by " + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_DOCTOR, IDbType.VarChar, DBNullConvert.From("%" + _DOCTORMASTERVO.DOCTOR + "%")));
                parameter.Add(new IParameter(_EDUCATIONSTANDARD, IDbType.VarChar, DBNullConvert.From(_DOCTORMASTERVO.EDUCATIONSTANDARD)));
                parameter.Add(new IParameter(_DoctorName, IDbType.VarChar, DBNullConvert.From("%" + _DOCTORMASTERVO.DoctorName + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = query[_DOCTOR].ToString();
                    DOCTORMASTERVO.DoctorName = "("+ query[_DOCTOR].ToString() + ")"+query[_DoctorName].ToString();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    LstDOCTORMASTERVO.Add(DOCTORMASTERVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return LstDOCTORMASTERVO;
        }


        internal DataTable SearchByCode_DS(string[] arCode)
        {
            DataTable table = new DataTable();
            table.Columns.Add(_DOCTOR, typeof(String));
            table.Columns.Add(_DoctorName, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORMASTER);
                strQuery.Append(" where " + _DOCTOR + " in (");

                int i = 0;
                foreach (string Code in arCode)
                {
                    if (i > 0)
                        strQuery.Append(",");
                    //
                    strQuery.Append("@" + _DOCTOR + i);
                    i++;
                }

                strQuery.Append(") order by " + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                i = 0;
                foreach (string Code in arCode)
                {
                    parameter.Add(new IParameter(_DOCTOR + i, IDbType.VarChar, DBNullConvert.From(Code)));
                    i++;
                }
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DataRow row = table.NewRow();
                    row[_DOCTOR] = query[_DOCTOR].ToString();
                    row[_DoctorName] = query[_DoctorName].ToString();

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

        internal List<DOCTORMASTERVO> SearchByKeyTreatment(DOCTORMASTERVO _DOCTORMASTERVO)
        {
            List<DOCTORMASTERVO> LstDOCTORMASTERVO = new List<DOCTORMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_DOCTORTREATMENT);
                strQuery.Append(" where " + _DoctorName + " <> ''");

                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.DOCTOR))
                {
                    strQuery.Append(" and " + _DOCTOR + " = @" + _DOCTOR);
                }
                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.EDUCATIONSTANDARD))
                {
                    strQuery.Append(" and " + _EDUCATIONSTANDARD + " = @" + _EDUCATIONSTANDARD);
                }
                if (!string.IsNullOrEmpty(_DOCTORMASTERVO.DoctorName))
                {
                    strQuery.Append(" and " + _DoctorName + " Like @" + _DoctorName);
                }
                strQuery.Append(" order by " + _DoctorName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_DOCTOR, IDbType.VarChar, DBNullConvert.From(_DOCTORMASTERVO.DOCTOR)));
                parameter.Add(new IParameter(_EDUCATIONSTANDARD, IDbType.VarChar, DBNullConvert.From(_DOCTORMASTERVO.EDUCATIONSTANDARD)));
                parameter.Add(new IParameter(_DoctorName, IDbType.VarChar, DBNullConvert.From("%" + _DOCTORMASTERVO.DoctorName + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = query[_DOCTOR].ToString();
                    DOCTORMASTERVO.DoctorName = query[_DoctorName].ToString();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    LstDOCTORMASTERVO.Add(DOCTORMASTERVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return LstDOCTORMASTERVO;
        }

    }
}
