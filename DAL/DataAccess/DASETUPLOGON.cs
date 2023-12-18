using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DASETUPLOGON : DataAccess
    {
        private static string _tblSETUPLOGON = "SETUPLOGON";
        private static string _tblSETUPUSERTYPE = "SETUPUSERTYPE";
        private static string _tblSETUPACCESSMENU = "SETUPACCESSMENU";

        private static string _UserID = "UserID";
        private static string _Password = "Password";
        private static string _Username = "Username";
        private static string _UseReserve = "UseReserve";
        private static string _UseAppointment = "UseAppointment";
        private static string _UseOperation = "UseOperation";
        private static string _UseReport = "UseReport";
        private static string _UseSetup = "UseSetup";
        private static string _UseType = "UseType";
        private static string _Remark = "Remark";
        private static string _FirstName = "FirstName";
        private static string _LastName = "LastName";
        private static string _Active = "Active";
        private static string _AdminType = "AdminType";
        private static string _Doctor = "Doctor";
        private static string _ID = "ID";
        private static string _Description = "Description";

        private static string _AccessID = "AccessID";
        private static string _AccessCode = "AccessCode";
        private static string _AccessName = "AccessName";

        private static string _UseReserveReadOnly = "UseReserveReadOnly";
        private static string _UseViewBooking = "UseViewBooking";
        private static string _UsePostTreatment = "UsePostTreatment";
        private static string _UseEnquiryPrice = "UseEnquiryPrice";
        private static string _UseInjectionRoom = "UseInjectionRoom";
        private static string _UseReportPostOP = "UseReportPostOP";
        private static string _UseSetupGroupMethod = "UseSetupGroupMethod";

        private static string _UseSetupDoctor = "UseSetupDoctor";
        private static string _UseSetupNurse = "UseSetupNurse";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DASETUPLOGON() { }
        public DASETUPLOGON(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DASETUPLOGON(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DASETUPLOGON(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<SETUPLOGONVO> SearchByKey(SETUPLOGONVO _SETUPLOGONVO)
        {
            List<SETUPLOGONVO> retValue = new List<SETUPLOGONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*, b." + _Description + ",c.* from " + _tblSETUPLOGON + " a ");
                strQuery.Append(" left join " + _tblSETUPUSERTYPE + " b on a." + _UseType + " = b." + _ID);
                strQuery.Append(" left join " + _tblSETUPACCESSMENU + " c on a." + _AccessCode + " = c." + _AccessCode);
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.UserID))
                {
                    strQuery.Append(" and a." + _UserID + " = @" + _UserID);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.Username))
                {
                    strQuery.Append(" and a." + _Username + " = @" + _Username);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.FirstName))
                {
                    strQuery.Append(" and a." + _FirstName + " = @" + _FirstName);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.LastName))
                {
                    strQuery.Append(" and a." + _LastName + " = @" + _LastName);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.AdminType.ToString()))
                {
                    strQuery.Append(" and a." + _AdminType + " = @" + _AdminType);
                }
                strQuery.Append(" order by " + _Username);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UserID)));
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Username)));
                parameter.Add(new IParameter(_FirstName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.FirstName)));
                parameter.Add(new IParameter(_LastName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.LastName)));
                parameter.Add(new IParameter(_Active, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.Active)));
                parameter.Add(new IParameter(_AdminType, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.AdminType)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
                    SETUPLOGONVO.UserID = query[_UserID].ToString();
                    SETUPLOGONVO.Username = query[_Username].ToString();
                    SETUPLOGONVO.Password = query[_Password].ToString();
                    SETUPLOGONVO.FirstName = query[_FirstName].ToString();
                    SETUPLOGONVO.LastName = query[_LastName].ToString();
                    SETUPLOGONVO.Name = SETUPLOGONVO.FirstName + " " + SETUPLOGONVO.LastName;
                    SETUPLOGONVO.Active = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.AdminType = query[_AdminType].ToString() == "True" ? true : false;
                    SETUPLOGONVO.Remark = query[_Remark].ToString();                    
                    SETUPLOGONVO.UseType = query[_UseType].ToString();
                    SETUPLOGONVO.Doctor = query[_Doctor].ToString();
                    SETUPLOGONVO.AccessCode = query[_AccessCode].ToString();
                    SETUPLOGONVO.AccessName = query[_AccessName].ToString();
                     
                    SETUPLOGONVO.SETUPUSERTYPEVO = new SETUPUSERTYPEVO();
                    SETUPLOGONVO.SETUPUSERTYPEVO.Description = query[_Description].ToString();
                    SETUPLOGONVO.UseTypeName = query[_Description].ToString();
                    SETUPLOGONVO.strActive = query[_Active].ToString() == "True" ? "ใช้งาน" : "ระงับการใช้งาน";

                    SETUPLOGONVO.AccessID = query[_AccessID].ToString();
                    SETUPLOGONVO.UseReserve = query[_UseReserve].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseAppointment = query[_UseAppointment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseOperation = query[_UseOperation].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReport = query[_UseReport].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetup = query[_UseSetup].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReserveReadOnly = query[_UseReserveReadOnly].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseViewBooking = query[_UseViewBooking].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UsePostTreatment = query[_UsePostTreatment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseEnquiryPrice = query[_UseEnquiryPrice].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseInjectionRoom = query[_UseInjectionRoom].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReportPostOP = query[_UseReportPostOP].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetupGroupMethod = query[_UseSetupGroupMethod].ToString() == "True" ? true : false;

                    retValue.Add(SETUPLOGONVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                //throw exc;
            }
            return retValue;
        }

        internal List<SETUPLOGONVO> SearchDDL(SETUPLOGONVO param)
        {
            List<SETUPLOGONVO> retValue = new List<SETUPLOGONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPLOGON);
                strQuery.Append(" where ");
                strQuery.Append( _Username + " Like @" + _Username);
                strQuery.Append(" or " + _FirstName + " Like @" + _FirstName);
                strQuery.Append(" or " + _LastName + " Like @" + _LastName);
                strQuery.Append(" order by " + _FirstName);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From("%" + param.Username + "%")));
                parameter.Add(new IParameter(_FirstName, IDbType.VarChar, DBNullConvert.From("%" + param.FirstName + "%")));
                parameter.Add(new IParameter(_LastName, IDbType.VarChar, DBNullConvert.From("%" + param.LastName + "%")));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
                    SETUPLOGONVO.UserID = query[_UserID].ToString();
                    SETUPLOGONVO.Username = query[_Username].ToString();
                    SETUPLOGONVO.Password = query[_Password].ToString();
                    SETUPLOGONVO.FirstName = query[_FirstName].ToString();
                    SETUPLOGONVO.LastName = query[_LastName].ToString();
                    SETUPLOGONVO.Name = SETUPLOGONVO.FirstName + " " + SETUPLOGONVO.LastName;
                    retValue.Add(SETUPLOGONVO);
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

        internal SETUPLOGONVO SearchLogin(SETUPLOGONVO _SETUPLOGONVO)
        {
            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblSETUPLOGON + " where ");
                strQuery.Append(" " + _UserID + " = @" + _UserID + " ");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UserID)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPLOGONVO.UserID = query[_UserID].ToString();
                    SETUPLOGONVO.Username = query[_Username].ToString();
                    SETUPLOGONVO.Password = query[_Password].ToString();
                    SETUPLOGONVO.FirstName = query[_FirstName].ToString();
                    SETUPLOGONVO.LastName = query[_LastName].ToString();
                    SETUPLOGONVO.Name = SETUPLOGONVO.FirstName + " " + SETUPLOGONVO.LastName;
                    SETUPLOGONVO.Active = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.AdminType = query[_AdminType].ToString() == "True" ? true : false;
                    SETUPLOGONVO.Remark = query[_Remark].ToString();
                    SETUPLOGONVO.UseReserve = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseAppointment = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseOperation = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReport = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetup = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseType = query[_UseType].ToString();
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPLOGONVO;
        }

        internal SETUPLOGONVO CheckLogin(SETUPLOGONVO _SETUPLOGONVO)
        {
            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.*,b.* from " + _tblSETUPLOGON + " a ");
                strQuery.Append(" left join " + _tblSETUPACCESSMENU + " b on a.accesscode = b.accesscode");
                strQuery.Append(" where ");
                strQuery.Append(" " + _Username + " = @" + _Username + " ");
                strQuery.Append(" and " + _Password + " = @" + _Password + " ");
                strQuery.Append(" and " + _Active + " = 1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Username)));
                parameter.Add(new IParameter(_Password, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Password)));
                parameter.Add(new IParameter(_Active, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.Active)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {

                    SETUPLOGONVO.UserID = query[_UserID].ToString();
                    SETUPLOGONVO.Username = query[_Username].ToString();
                    SETUPLOGONVO.Password = query[_Password].ToString();
                    SETUPLOGONVO.FirstName = query[_FirstName].ToString();
                    SETUPLOGONVO.LastName = query[_LastName].ToString();
                    SETUPLOGONVO.Name = SETUPLOGONVO.FirstName + " " + SETUPLOGONVO.LastName;
                    SETUPLOGONVO.Active = query[_Active].ToString() == "True" ? true : false;
                    SETUPLOGONVO.AdminType = query[_AdminType].ToString() == "True" ? true : false;
                    SETUPLOGONVO.Remark = query[_Remark].ToString();
                    SETUPLOGONVO.UseReserve = query[_UseReserve].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseAppointment = query[_UseAppointment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseOperation = query[_UseOperation].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReport = query[_UseReport].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetup = query[_UseSetup].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseType = query[_UseType].ToString();
                    SETUPLOGONVO.Doctor = query[_Doctor].ToString();
                    SETUPLOGONVO.strActive = query[_Active].ToString() == "True" ? "ใช้งาน" : "ระงับการใช้งาน";

                    SETUPLOGONVO.AccessID = query[_AccessID].ToString();
                    SETUPLOGONVO.UseReserveReadOnly = query[_UseReserveReadOnly].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseViewBooking = query[_UseViewBooking].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UsePostTreatment = query[_UsePostTreatment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseEnquiryPrice = query[_UseEnquiryPrice].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseInjectionRoom = query[_UseInjectionRoom].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReportPostOP = query[_UseReportPostOP].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetupGroupMethod = query[_UseSetupGroupMethod].ToString() == "True" ? true : false;

                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return SETUPLOGONVO;
        }

        internal ReturnValue Checkdup(string Username)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblSETUPLOGON);
                strQuery.Append(" where " + _Username + " = @" + _Username);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(Username)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {
                retVal.Exception = ex;
                retVal.Value = false;
            }
            return retVal;
        }

        internal ReturnValue ChangePassword(SETUPLOGONVO _SETUPLOGONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPLOGON + " SET ");
                sbQuery.Append("" + _Password + " = @" + _Password);
                sbQuery.Append(" WHERE " + _UserID + " = @" + _UserID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UserID)));
                parameter.Add(new IParameter(_Password, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Password)));

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

        internal ReturnValue Insert(SETUPLOGONVO _SETUPLOGONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPLOGON + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_UserID);
                sbValue.Append("@" + _UserID);

                sbInsert.Append("," + _Username);
                sbValue.Append(",@" + _Username);

                sbInsert.Append("," + _Password);
                sbValue.Append(",@" + _Password);

                sbInsert.Append("," + _FirstName);
                sbValue.Append(",@" + _FirstName);

                sbInsert.Append("," + _LastName);
                sbValue.Append(",@" + _LastName);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);

                sbInsert.Append("," + _AdminType);
                sbValue.Append(",@" + _AdminType);

                sbInsert.Append("," + _Active);
                sbValue.Append(",@" + _Active);

                sbInsert.Append("," + _UseType);
                sbValue.Append(",@" + _UseType);

                sbInsert.Append("," + _Doctor);
                sbValue.Append(",@" + _Doctor);

                sbInsert.Append("," + _AccessCode);
                sbValue.Append(",@" + _AccessCode);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UserID)));
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Username)));
                parameter.Add(new IParameter(_Password, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Password)));
                parameter.Add(new IParameter(_FirstName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.FirstName)));
                parameter.Add(new IParameter(_LastName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.LastName)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Remark)));
                parameter.Add(new IParameter(_AdminType, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.AdminType)));
                parameter.Add(new IParameter(_Active, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.Active)));
                parameter.Add(new IParameter(_UseType, IDbType.NVarChar, DBNullConvert.From(_SETUPLOGONVO.UseType)));
                parameter.Add(new IParameter(_Doctor, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Doctor)));
                parameter.Add(new IParameter(_AccessCode, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessCode)));

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

        internal ReturnValue Update(SETUPLOGONVO _SETUPLOGONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPLOGON + " SET ");

                sbQuery.Append("" + _Username + " = @" + _Username);
                sbQuery.Append("," + _FirstName + " = @" + _FirstName);
                sbQuery.Append("," + _LastName + " = @" + _LastName);
                sbQuery.Append("," + _Remark + " = @" + _Remark);
                sbQuery.Append("," + _AdminType + " = @" + _AdminType);
                sbQuery.Append("," + _Active + " = @" + _Active);
                sbQuery.Append("," + _UseType + " = @" + _UseType); 
                sbQuery.Append("," + _Doctor + " = @" + _Doctor);
                sbQuery.Append("," + _AccessCode + " = @" + _AccessCode);

                sbQuery.Append(" WHERE " + _UserID + " = @" + _UserID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UserID)));
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Username)));
                parameter.Add(new IParameter(_FirstName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.FirstName)));
                parameter.Add(new IParameter(_LastName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.LastName)));
                parameter.Add(new IParameter(_Remark, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Remark)));
                parameter.Add(new IParameter(_AdminType, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.AdminType)));
                parameter.Add(new IParameter(_Active, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.Active)));
                parameter.Add(new IParameter(_UseType, IDbType.NVarChar, DBNullConvert.From(_SETUPLOGONVO.UseType)));
                parameter.Add(new IParameter(_Doctor, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.Doctor)));
                parameter.Add(new IParameter(_AccessCode, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessCode)));
                
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

        internal ReturnValue Delete(string UserID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPLOGON);
                sbQuery.Append(" WHERE " + _UserID + " = @" + _UserID);
                sbQuery.Append(" and " + _AdminType + " = @" + _AdminType);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_UserID, IDbType.VarChar, DBNullConvert.From(UserID)));
                parameter.Add(new IParameter(_AdminType, IDbType.Bit, DBNullConvert.From(false)));
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

        internal ReturnValue CheckdupAccessMenuCode(string Username)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblSETUPLOGON);
                strQuery.Append(" where " + _Username + " = @" + _Username);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Username, IDbType.VarChar, DBNullConvert.From(Username)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();

            }
            catch (Exception ex)
            {
                retVal.Exception = ex;
                retVal.Value = false;
            }
            return retVal;
        }

        internal ReturnValue InsertAccessMenuCode(SETUPLOGONVO _SETUPLOGONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblSETUPACCESSMENU + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_AccessID);
                sbValue.Append("@" + _AccessID);

                sbInsert.Append("," + _AccessCode);
                sbValue.Append(",@" + _AccessCode);

                sbInsert.Append("," + _AccessName);
                sbValue.Append(",@" + _AccessName);

                sbInsert.Append("," + _UseAppointment);
                sbValue.Append(",@" + _UseAppointment);

                sbInsert.Append("," + _UseOperation);
                sbValue.Append(",@" + _UseOperation);

                sbInsert.Append("," + _UseReport);
                sbValue.Append(",@" + _UseReport);

                sbInsert.Append("," + _UseReserve);
                sbValue.Append(",@" + _UseReserve);

                sbInsert.Append("," + _UseSetup);
                sbValue.Append(",@" + _UseSetup);

                sbInsert.Append("," + _UseReserveReadOnly);
                sbValue.Append(",@" + _UseReserveReadOnly);

                sbInsert.Append("," + _UseViewBooking);
                sbValue.Append(",@" + _UseViewBooking);

                sbInsert.Append("," + _UsePostTreatment);
                sbValue.Append(",@" + _UsePostTreatment);

                sbInsert.Append("," + _UseEnquiryPrice);
                sbValue.Append(",@" + _UseEnquiryPrice);

                sbInsert.Append("," + _UseInjectionRoom);
                sbValue.Append(",@" + _UseInjectionRoom);

                sbInsert.Append("," + _UseReportPostOP);
                sbValue.Append(",@" + _UseReportPostOP);

                sbInsert.Append("," + _UseSetupGroupMethod);
                sbValue.Append(",@" + _UseSetupGroupMethod);

                sbInsert.Append("," + _UseSetupDoctor);
                sbValue.Append(",@" + _UseSetupDoctor);

                sbInsert.Append("," + _UseSetupNurse);
                sbValue.Append(",@" + _UseSetupNurse);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AccessID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessID)));
                parameter.Add(new IParameter(_AccessCode, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessCode)));
                parameter.Add(new IParameter(_AccessName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessName)));
                parameter.Add(new IParameter(_UseAppointment, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseAppointment)));
                parameter.Add(new IParameter(_UseOperation, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseOperation)));
                parameter.Add(new IParameter(_UseReport, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseReport)));
                parameter.Add(new IParameter(_UseReserve, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseReserve)));
                parameter.Add(new IParameter(_UseSetup, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseSetup)));
                parameter.Add(new IParameter(_UseReserveReadOnly, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseReserveReadOnly)));
                parameter.Add(new IParameter(_UseViewBooking, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseViewBooking)));
                parameter.Add(new IParameter(_UsePostTreatment, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UsePostTreatment)));
                parameter.Add(new IParameter(_UseEnquiryPrice, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseEnquiryPrice)));
                parameter.Add(new IParameter(_UseInjectionRoom, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseInjectionRoom)));
                parameter.Add(new IParameter(_UseReportPostOP, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseReportPostOP)));
                parameter.Add(new IParameter(_UseSetupGroupMethod, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseSetupGroupMethod)));
                parameter.Add(new IParameter(_UseSetupDoctor, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseSetupDoctor)));
                parameter.Add(new IParameter(_UseSetupNurse, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseSetupNurse)));

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

        internal List<SETUPLOGONVO> SearchByKeyAccessMenuCode(SETUPLOGONVO _SETUPLOGONVO)
        {
            List<SETUPLOGONVO> retValue = new List<SETUPLOGONVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select a.* from " + _tblSETUPACCESSMENU + " a ");
                strQuery.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.AccessID))
                {
                    strQuery.Append(" and a." + _AccessID + " = @" + _AccessID);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.AccessCode))
                {
                    strQuery.Append(" and a." + _AccessCode + " = @" + _AccessCode);
                }
                if (!string.IsNullOrEmpty(_SETUPLOGONVO.AccessName))
                {
                    strQuery.Append(" and a." + _AccessName + " = @" + _AccessName);
                }

                strQuery.Append(" order by " + _AccessCode);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AccessID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessID)));
                parameter.Add(new IParameter(_AccessCode, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessCode)));
                parameter.Add(new IParameter(_AccessName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessName)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
                    SETUPLOGONVO.AccessID = query[_AccessID].ToString();
                    SETUPLOGONVO.AccessCode = query[_AccessCode].ToString();
                    SETUPLOGONVO.AccessName = query[_AccessName].ToString();
                    SETUPLOGONVO.UseReserve = query[_UseReserve].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseAppointment = query[_UseAppointment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseOperation = query[_UseOperation].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReport = query[_UseReport].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetup = query[_UseSetup].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReserveReadOnly = query[_UseReserveReadOnly].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseViewBooking = query[_UseViewBooking].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UsePostTreatment = query[_UsePostTreatment].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseEnquiryPrice = query[_UseEnquiryPrice].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseInjectionRoom = query[_UseInjectionRoom].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseReportPostOP = query[_UseReportPostOP].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetupGroupMethod = query[_UseSetupGroupMethod].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetupDoctor = query[_UseSetupDoctor].ToString() == "True" ? true : false;
                    SETUPLOGONVO.UseSetupNurse = query[_UseSetupNurse].ToString() == "True" ? true : false;

                    retValue.Add(SETUPLOGONVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                //throw exc;
            }
            return retValue;
        }

        internal ReturnValue UpdateAccessMenuCode(SETUPLOGONVO _SETUPLOGONVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblSETUPACCESSMENU + " SET ");

                sbQuery.Append("" + _AccessCode + " = @" + _AccessCode);
                sbQuery.Append("," + _AccessName + " = @" + _AccessName);
                sbQuery.Append("," + _UseAppointment + " = @" + _UseAppointment);
                sbQuery.Append("," + _UseOperation + " = @" + _UseOperation);
                sbQuery.Append("," + _UseReport + " = @" + _UseReport);
                sbQuery.Append("," + _UseReserve + " = @" + _UseReserve);
                sbQuery.Append("," + _UseSetup + " = @" + _UseSetup);
                sbQuery.Append("," + _UseReserveReadOnly + " = @" + _UseReserveReadOnly);
                sbQuery.Append("," + _UseViewBooking + " = @" + _UseViewBooking);
                sbQuery.Append("," + _UsePostTreatment + " = @" + _UsePostTreatment);
                sbQuery.Append("," + _UseEnquiryPrice + " = @" + _UseEnquiryPrice);
                sbQuery.Append("," + _UseInjectionRoom + " = @" + _UseInjectionRoom);
                sbQuery.Append("," + _UseReportPostOP + " = @" + _UseReportPostOP);
                sbQuery.Append("," + _UseSetupGroupMethod + " = @" + _UseSetupGroupMethod);
                sbQuery.Append("," + _UseSetupDoctor + " = @" + _UseSetupDoctor);
                sbQuery.Append("," + _UseSetupNurse + " = @" + _UseSetupNurse);

                sbQuery.Append(" WHERE " + _AccessID + " = @" + _AccessID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AccessID, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessID)));
                parameter.Add(new IParameter(_AccessCode, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessCode)));
                parameter.Add(new IParameter(_AccessName, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.AccessName)));
                parameter.Add(new IParameter(_UseAppointment, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseAppointment)));
                parameter.Add(new IParameter(_UseOperation, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseOperation)));
                parameter.Add(new IParameter(_UseReport, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseReport)));
                parameter.Add(new IParameter(_UseReserve, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseReserve)));
                parameter.Add(new IParameter(_UseSetup, IDbType.Bit, DBNullConvert.From(_SETUPLOGONVO.UseSetup)));

                parameter.Add(new IParameter(_UseReserveReadOnly, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseReserveReadOnly)));
                parameter.Add(new IParameter(_UseViewBooking, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseViewBooking)));
                parameter.Add(new IParameter(_UsePostTreatment, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UsePostTreatment)));
                parameter.Add(new IParameter(_UseEnquiryPrice, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseEnquiryPrice)));
                parameter.Add(new IParameter(_UseInjectionRoom, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseInjectionRoom)));
                parameter.Add(new IParameter(_UseReportPostOP, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseReportPostOP)));
                parameter.Add(new IParameter(_UseSetupGroupMethod, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseSetupGroupMethod)));

                parameter.Add(new IParameter(_UseSetupDoctor, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseSetupDoctor)));
                parameter.Add(new IParameter(_UseSetupNurse, IDbType.VarChar, DBNullConvert.From(_SETUPLOGONVO.UseSetupNurse)));

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

        internal ReturnValue DeleteAccessMenuCode(string AccessID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblSETUPACCESSMENU);
                sbQuery.Append(" WHERE " + _AccessID + " = @" + _AccessID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_AccessID, IDbType.VarChar, DBNullConvert.From(AccessID)));
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
