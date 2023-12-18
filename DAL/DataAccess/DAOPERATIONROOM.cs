using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAOPERATIONROOM : DataAccess
    {

        private static string _VT_OPERATIONROOM = "VT_OPERATIONROOM";

        private static string _CODE = "CODE";
        private static string _NAME = "NAME";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAOPERATIONROOM() { }
        public DAOPERATIONROOM(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAOPERATIONROOM(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAOPERATIONROOM(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<OPERATIONROOMVO> SearchAll()
        {
            List<OPERATIONROOMVO> lstOPERATIONROOMVO = new List<OPERATIONROOMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_OPERATIONROOM);
                strQuery.Append(" where " + _NAME + " <> ''");
                strQuery.Append(" order by " + _NAME);
                ConnectDB();
                command = GetCommand(strQuery.ToString());
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    OPERATIONROOMVO OPERATIONROOMVO = new OPERATIONROOMVO();
                    OPERATIONROOMVO.CODE = query[_CODE].ToString();
                    OPERATIONROOMVO.NAME = query[_NAME].ToString();

                    lstOPERATIONROOMVO.Add(OPERATIONROOMVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lstOPERATIONROOMVO;
        }
        internal OPERATIONROOMVO SearchByCode(string Code)
        {
            OPERATIONROOMVO OPERATIONROOMVO = new OPERATIONROOMVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_OPERATIONROOM);
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
                    OPERATIONROOMVO.CODE = query[_CODE].ToString();
                    OPERATIONROOMVO.NAME = query[_NAME].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return OPERATIONROOMVO;
        }

        internal OPERATIONROOMVO SearchByCodeNotLike(string Code)
        {
            OPERATIONROOMVO OPERATIONROOMVO = new OPERATIONROOMVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_OPERATIONROOM);
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
                    OPERATIONROOMVO.CODE = query[_CODE].ToString();
                    OPERATIONROOMVO.NAME = query[_NAME].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return OPERATIONROOMVO;
        }

        internal DataTable SearchByCode_DS(string[] arCode)
        {
            DataTable table = new DataTable();
            table.Columns.Add(_CODE, typeof(String));
            table.Columns.Add(_NAME, typeof(String));
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _VT_OPERATIONROOM);
                strQuery.Append(" where "+_CODE+" in(");
                int i = 0;
                foreach(string code in arCode)
                {
                    if (i > 0)
                        strQuery.Append(",");
                    strQuery.Append(" @" + _CODE + i);
                    i++;
                }
                strQuery.Append(") order by " + _NAME);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                i = 0;
                foreach (string code in arCode)
                {
                    parameter.Add(new IParameter(_CODE+i, IDbType.VarChar, DBNullConvert.From(code)));
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
