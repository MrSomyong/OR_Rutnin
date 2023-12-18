using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAORUSER : DataAccess
    {
        private static string _tblUSR = "ORUSER";
        private static string _id = "id";
        private static string _username = "username";
        private static string _password = "password";
        private static string _email = "email";
        private static string _firstname = "firstname";
        private static string _lastname = "lastname";
        private static string _active = "active";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DAORUSER() { }
        public DAORUSER(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAORUSER(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAORUSER(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<ORUSERVO> SearchByKey(ORUSERVO _ORUSERVO)
        {
            List<ORUSERVO> retValue = new List<ORUSERVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblUSR + " where 1=1 ");
                if (!string.IsNullOrEmpty(_ORUSERVO.id))
                {
                    strQuery.Append(" and " + _id + " = @" + _id);
                }
                if (!string.IsNullOrEmpty(_ORUSERVO.username))
                {
                    strQuery.Append(" and " + _username + " = @" + _username);
                }
                strQuery.Append(" order by " + _firstname);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_id, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.id)));
                parameter.Add(new IParameter(_username, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.username)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORUSERVO ORUSERVO = new ORUSERVO();
                    ORUSERVO.id = query[_id].ToString();
                    ORUSERVO.email = query[_email].ToString();
                    ORUSERVO.username = query[_username].ToString();
                    ORUSERVO.password = query[_password].ToString();
                    ORUSERVO.firstname = query[_firstname].ToString();
                    ORUSERVO.lastname = query[_lastname].ToString();
                    ORUSERVO.active = query[_active].ToString() == "True" ? true : false;
                    retValue.Add(ORUSERVO);
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

        internal ReturnValue CheckDup(ORUSERVO _ORUSERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("select count(*) as num from " + _tblUSR);
                strQuery.Append(" where 1 = 1 ");

                if (!string.IsNullOrEmpty(_ORUSERVO.username))
                {
                    strQuery.Append(" and " + _username + " = @" + _username + " ");
                }
                if (!string.IsNullOrEmpty(_ORUSERVO.email))
                {
                    strQuery.Append(" and " + _email + " = @" + _email + " ");
                }

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_username, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.username)));
                parameter.Add(new IParameter(_email, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.email)));
                command = GetCommand(strQuery.ToString(), parameter);
                effected = GetExecuteScalar(command);
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

        internal ORUSERVO CheckLogin(ORUSERVO _ORUSERVO)
        {
            ORUSERVO ORUSERVO = new ORUSERVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblUSR + " where 1=1 ");
                strQuery.Append(" and " + _email + " = @" + _email + " ");
                strQuery.Append(" and " + _password + " = @" + _password + " ");
                strQuery.Append(" and " + _active + " = 1");
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_email, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.email)));
                parameter.Add(new IParameter(_password, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.password)));

                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    ORUSERVO.id = query[_id].ToString();
                    ORUSERVO.email = query[_email].ToString();
                    ORUSERVO.username = query[_username].ToString();
                    ORUSERVO.password = query[_password].ToString();
                    ORUSERVO.firstname = query[_firstname].ToString();
                    ORUSERVO.lastname = query[_lastname].ToString();
                    ORUSERVO.active = query[_active].ToString() == "True" ? true : false;
                }
                query.Close();
                command.Dispose();
                DisconnectDB();

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return ORUSERVO;
        }

        internal ORUSERVO GetUSERByid(ORUSERVO ORUSERVO)
        {
            ORUSERVO retValue = new ORUSERVO();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblUSR + " where ");
                strQuery.Append(_id + " = @" + _id);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_id, IDbType.VarChar, DBNullConvert.From(ORUSERVO.id)));


                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                int i = 1;
                while (query.Read())
                {
                    retValue.id = query[_id].ToString();
                    retValue.username = query[_username].ToString();
                    retValue.password = query[_password].ToString();
                    retValue.firstname = query[_firstname].ToString();
                    retValue.lastname = query[_lastname].ToString();
                    retValue.active = query[_active].ToString() == "True" ? true : false;
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

        internal ReturnValue Insert(ORUSERVO _ORUSERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblUSR + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_id);
                sbValue.Append("NEWID()");

                sbInsert.Append("," + _username);
                sbValue.Append(",@" + _username);

                sbInsert.Append("," + _password);
                sbValue.Append(",@" + _password);

                sbInsert.Append("," + _email);
                sbValue.Append(",@" + _email);

                sbInsert.Append("," + _firstname);
                sbValue.Append(",@" + _firstname);

                sbInsert.Append("," + _lastname);
                sbValue.Append(",@" + _lastname);

                sbInsert.Append("," + _active);
                sbValue.Append(",@" + _active);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();

                parameter.Add(new IParameter(_username, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.username)));
                parameter.Add(new IParameter(_password, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.password)));
                parameter.Add(new IParameter(_email, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.email)));
                parameter.Add(new IParameter(_firstname, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.firstname)));
                parameter.Add(new IParameter(_lastname, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.lastname)));
                parameter.Add(new IParameter(_active, IDbType.Bit, DBNullConvert.From(_ORUSERVO.active)));

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

        internal ReturnValue UpdateProfiles(ORUSERVO _ORUSERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblUSR + " SET ");
                sbQuery.Append("" + _username + " = @" + _username);
                sbQuery.Append("," + _email + " = @" + _email);
                sbQuery.Append("," + _firstname + " = @" + _firstname);
                sbQuery.Append("," + _lastname + " = @" + _lastname);
                sbQuery.Append("," + _active + " = @" + _active);
                sbQuery.Append(" WHERE " + _id + " = @" + _id);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_id, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.id)));
                parameter.Add(new IParameter(_username, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.username)));
                parameter.Add(new IParameter(_email, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.email)));
                parameter.Add(new IParameter(_firstname, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.firstname)));
                parameter.Add(new IParameter(_lastname, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.lastname)));
                parameter.Add(new IParameter(_active, IDbType.Bit, DBNullConvert.From(_ORUSERVO.active)));

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

        internal ReturnValue ChangePassword(ORUSERVO _ORUSERVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblUSR + " SET ");
                sbQuery.Append("" + _password + " = @" + _password);
                sbQuery.Append(" WHERE " + _id + " = @" + _id);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_id, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.id)));
                parameter.Add(new IParameter(_password, IDbType.VarChar, DBNullConvert.From(_ORUSERVO.password)));

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

        internal ReturnValue Delete(string id)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblUSR);
                sbQuery.Append(" WHERE " + _id + " = @" + _id);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_id, IDbType.VarChar, DBNullConvert.From(id)));
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
