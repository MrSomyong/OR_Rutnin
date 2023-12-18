using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORMIGRATION : DataAccess
    {
        private static string _tblORMIGRATION = "ORMIGRATION";

        private static string _ID = "ID";
        private static string _HN = "HN";
        private static string _ORDate = "ORDate";
        private static string _Side = "Side";
        private static string _ProcedureMemo = "ProcedureMemo";
        private static string _Note = "Note";
        private static string _Surgeon = "Surgeon";
        private static string _SurgeonName = "SurgeonName";
        private static string _ORRoom = "ORRoom";
        private static string _ORRoomName = "ORRoomName";
        private static string _VT_DOCTORMASTER = "VT_DOCTORMASTER";
        private static string _DOCTOR = "DOCTOR";
        private static string _DoctorName = "DoctorName";

        private static string _tblSETUPORROOM = "SETUPORROOM";
        private static string _CODE = "CODE";
        private static string _Name = "Name";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAORMIGRATION() { }
        public DAORMIGRATION(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORMIGRATION(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORMIGRATION(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORMIGRATIONVO> SearchByKey(ORMIGRATIONVO _ORMIGRATIONVO)
        {
            List<ORMIGRATIONVO> retValue = new List<ORMIGRATIONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _DoctorName + " as " + _SurgeonName);
                strQuery.Append(" , c." + _Name + " as " + _ORRoomName);
                strQuery.Append(" from " + _tblORMIGRATION + " as a");
                strQuery.Append(" left join " + _VT_DOCTORMASTER + " as b on a." + _Surgeon + " = b." + _DOCTOR);
                strQuery.Append(" left join " + _tblSETUPORROOM + " as c on a." + _ORRoom + " = c." + _CODE);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_ORMIGRATIONVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(_ORMIGRATIONVO.HN))
                {
                    strQuery.Append(" and a." + _HN + " = @" + _HN);
                }
                strQuery.Append(" order by a." + _ORDate);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ID)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.HN)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORMIGRATIONVO ORMIGRATIONVO = new ORMIGRATIONVO();
                    ORMIGRATIONVO.ID = query[_ID].ToString();
                    ORMIGRATIONVO.Side = ADOUtil.GetIntFromQuery(query[_Side].ToString());
                    ORMIGRATIONVO.strSide = ((EnumOR.ORSide)ORMIGRATIONVO.Side).ToString();
                    ORMIGRATIONVO.ORDate = ADOUtil.GetDateFromQuery(query[_ORDate].ToString());
                    ORMIGRATIONVO.strORDate = CultureInfo.GetDatetime(DateTime.Parse(ADOUtil.GetDateFromQuery(query[_ORDate].ToString()).ToString()), YearType.English).ToString("dd-MM-yyyy");
                    ORMIGRATIONVO.HN = query[_HN].ToString();
                    ORMIGRATIONVO.ProcedureMemo = query[_ProcedureMemo].ToString();
                    ORMIGRATIONVO.Note = query[_Note].ToString();
                    ORMIGRATIONVO.Surgeon = query[_Surgeon].ToString();
                    ORMIGRATIONVO.SurgeonName = query[_SurgeonName].ToString();
                    ORMIGRATIONVO.ORRoom = query[_ORRoom].ToString();
                    ORMIGRATIONVO.ORRoomName = query[_ORRoomName].ToString();
                    retValue.Add(ORMIGRATIONVO);
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

        internal ReturnValue Insert(ORMIGRATIONVO _ORMIGRATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblORMIGRATION + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);
                
                sbInsert.Append("," + _HN);
                sbValue.Append(",@" + _HN);

                sbInsert.Append("," + _ORDate);
                sbValue.Append(",@" + _ORDate);

                sbInsert.Append("," + _Side);
                sbValue.Append(",@" + _Side);

                sbInsert.Append("," + _ProcedureMemo);
                sbValue.Append(",@" + _ProcedureMemo);

                sbInsert.Append("," + _Note);
                sbValue.Append(",@" + _Note);

                sbInsert.Append("," + _Surgeon);
                sbValue.Append(",@" + _Surgeon);

                sbInsert.Append("," + _ORRoom);
                sbValue.Append(",@" + _ORRoom);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ID)));
                parameter.Add(new IParameter(_Side, IDbType.Int, DBNullConvert.From(_ORMIGRATIONVO.Side, false)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.HN)));
                parameter.Add(new IParameter(_ORDate, IDbType.DateTime, DBNullConvert.From(_ORMIGRATIONVO.ORDate)));
                parameter.Add(new IParameter(_ProcedureMemo, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ProcedureMemo)));
                parameter.Add(new IParameter(_Note, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.Note)));
                parameter.Add(new IParameter(_Surgeon, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.Surgeon)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ORRoom))); 
                command = GetCommand(sbInsert.ToString(), parameter);

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

        internal ReturnValue Update(ORMIGRATIONVO _ORMIGRATIONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblORMIGRATION + " SET ");
                sbQuery.Append("" + _HN + " = @" + _HN);
                sbQuery.Append("," + _ORDate + " = @" + _ORDate);
                sbQuery.Append("," + _Side + " = @" + _Side);
                sbQuery.Append("," + _ProcedureMemo + " = @" + _ProcedureMemo);
                sbQuery.Append("," + _Note + " = @" + _Note);
                sbQuery.Append("," + _Surgeon + " = @" + _Surgeon);
                sbQuery.Append("," + _ORRoom + " = @" + _ORRoom);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ID)));
                parameter.Add(new IParameter(_Side, IDbType.Int, DBNullConvert.From(_ORMIGRATIONVO.Side, false)));
                parameter.Add(new IParameter(_HN, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.HN)));
                parameter.Add(new IParameter(_ORDate, IDbType.DateTime, DBNullConvert.From(_ORMIGRATIONVO.ORDate)));
                parameter.Add(new IParameter(_ProcedureMemo, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ProcedureMemo)));
                parameter.Add(new IParameter(_Note, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.Note)));
                parameter.Add(new IParameter(_Surgeon, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.Surgeon)));
                parameter.Add(new IParameter(_ORRoom, IDbType.VarChar, DBNullConvert.From(_ORMIGRATIONVO.ORRoom)));

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

        internal ReturnValue Delete(string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblORMIGRATION);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
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
    }
}
