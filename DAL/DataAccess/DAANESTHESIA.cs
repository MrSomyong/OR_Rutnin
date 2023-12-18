using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAANESTHESIA : DataAccess
    {

        private static string _VT_ANESTHESIA = "VT_ANESTHESIA";


        private static string _CODE = "CODE";
        private static string _NAME = "NAME";


        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAANESTHESIA() { }
        public DAANESTHESIA(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAANESTHESIA(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAANESTHESIA(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ANESTHESIAVO> SearchAll()
        {
            List<ANESTHESIAVO> retValue = new List<ANESTHESIAVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_ANESTHESIA);
                strQuery.Append(" where " + _NAME + " <> ''");
                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ANESTHESIAVO ANESTHESIAVO = new ANESTHESIAVO();
                    ANESTHESIAVO.CODE = query[_CODE].ToString();
                    ANESTHESIAVO.NAME = query[_NAME].ToString();

                    retValue.Add(ANESTHESIAVO);
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

        internal List<ANESTHESIAVO> SearchByKey(ANESTHESIAVO _ANESTHESIAVO)
        {
            List<ANESTHESIAVO> retValue = new List<ANESTHESIAVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_ANESTHESIA);
                strQuery.Append(" where 1 = 1 ");

                if (!string.IsNullOrEmpty(_ANESTHESIAVO.CODE))
                {
                    strQuery.Append(" and " + _CODE + " = @" + _CODE);
                }

                strQuery.Append(" order by " + _NAME);
                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(_ANESTHESIAVO.CODE)));

                command = GetCommand(strQuery.ToString(), parameter);

                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ANESTHESIAVO ANESTHESIAVO = new ANESTHESIAVO();
                    ANESTHESIAVO.CODE = query[_CODE].ToString();
                    ANESTHESIAVO.NAME = query[_NAME].ToString();

                    retValue.Add(ANESTHESIAVO);
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

        internal ANESTHESIAVO SearchByCode(string Code)
        {
            ANESTHESIAVO ANESTHESIAVO = new ANESTHESIAVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_ANESTHESIA);
                strQuery.Append(" where " + _NAME + " <> ''");
                strQuery.Append(" and " + _CODE + " = @" + _CODE);
                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_CODE, IDbType.VarChar, DBNullConvert.From(Code)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ANESTHESIAVO.CODE = query[_CODE].ToString();
                    ANESTHESIAVO.NAME = query[_NAME].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return ANESTHESIAVO;
        }
        internal DataTable SearchByCode_DS(string[] arCode)
        {
            DataTable table = new DataTable();
            table.Columns.Add(_CODE, typeof(String));
            table.Columns.Add(_NAME, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_ANESTHESIA);
                strQuery.Append(" where " + _CODE + " in (");

                int i = 0;
                foreach (string Code in arCode)
                {
                    if (i > 0)
                        strQuery.Append(",");
                    //
                    strQuery.Append("@" + _CODE + i);
                    i++;
                }

                strQuery.Append(") order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                i = 0;
                foreach (string Code in arCode)
                {
                    parameter.Add(new IParameter(_CODE + i, IDbType.VarChar, DBNullConvert.From(Code)));
                    i++;
                }
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
