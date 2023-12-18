using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DANURSEMASTER : DataAccess
    {

        private static string _VT_NURSEMASTER = "VT_NURSEMASTER";


        private static string _CODE = "CODE";
        private static string _NAME = "NAME";
        private static string _EDUCATIONSTANDARD = "EDUCATIONSTANDARD";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DANURSEMASTER() { }
        public DANURSEMASTER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DANURSEMASTER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DANURSEMASTER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<NURSEMASTERVO> SearchAll()
        {
            List<NURSEMASTERVO> retValue = new List<NURSEMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_NURSEMASTER);
                strQuery.Append(" where " + _NAME + " <> ''");
                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    NURSEMASTERVO NURSEMASTERVO = new NURSEMASTERVO();
                    NURSEMASTERVO.CODE = query[_CODE].ToString();
                    NURSEMASTERVO.NAME = query[_NAME].ToString();
                    NURSEMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    retValue.Add(NURSEMASTERVO);
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

        internal NURSEMASTERVO SearchByCode(string Code)
        {
            NURSEMASTERVO NURSEMASTERVO = new NURSEMASTERVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_NURSEMASTER);
                strQuery.Append(" where " + _NAME + " <> ''");

                if (!string.IsNullOrEmpty(Code))
                {
                    strQuery.Append(" and " + _CODE + " = @" + _CODE);
                }

                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {                    
                    NURSEMASTERVO.CODE = query[_CODE].ToString();
                    NURSEMASTERVO.NAME = query[_NAME].ToString();
                    NURSEMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return NURSEMASTERVO;
        }

        internal List<NURSEMASTERVO> SearchByKey(NURSEMASTERVO _NURSEMASTERVO)
        {
            List<NURSEMASTERVO> LstNURSEMASTERVO = new List<NURSEMASTERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_NURSEMASTER);
                strQuery.Append(" where " + _NAME + " <> ''");

                if (!string.IsNullOrEmpty(_NURSEMASTERVO.CODE))
                {
                    strQuery.Append(" and " + _CODE + " = @" + _CODE);
                }
                if (!string.IsNullOrEmpty(_NURSEMASTERVO.EDUCATIONSTANDARD))
                {
                    strQuery.Append(" and " + _EDUCATIONSTANDARD + " = @" + _EDUCATIONSTANDARD);
                }
                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(_NURSEMASTERVO.CODE)));
                parameter.Add(new IParameter(_EDUCATIONSTANDARD, IDbType.VarChar, DBNullConvert.From(_NURSEMASTERVO.EDUCATIONSTANDARD)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    NURSEMASTERVO NURSEMASTERVO = new NURSEMASTERVO();
                    NURSEMASTERVO.CODE = query[_CODE].ToString();
                    NURSEMASTERVO.NAME = query[_NAME].ToString();
                    NURSEMASTERVO.EDUCATIONSTANDARD = query[_EDUCATIONSTANDARD].ToString();
                    LstNURSEMASTERVO.Add(NURSEMASTERVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return LstNURSEMASTERVO;
        }

        internal DataTable SearchByCode_DS(string Code)
        {
            DataTable table = new DataTable();
            table.Columns.Add(_CODE, typeof(String));
            table.Columns.Add(_NAME, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_NURSEMASTER);
                strQuery.Append(" where " + _NAME + " <> ''");

                if (!string.IsNullOrEmpty(Code))
                {
                    strQuery.Append(" and " + _CODE + " = @" + _CODE);
                }

                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DataRow row = table.NewRow();
                    row[_CODE] = query[_CODE].ToString();
                    row[_NAME] = query[_NAME].ToString();

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
