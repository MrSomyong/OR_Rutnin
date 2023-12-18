using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ADOUtils;

namespace DAL
{
    class DataAccess
    {
        protected IDbConnection connection;
        protected IDbTransaction transaction;
        protected IDbCommand command;
        //protected SqlCommand command;
        protected IDataReader query;
        //protected SqlDataReader query;
        protected IDataAdapter queryadapter;
        //protected SqlDataAdapter queryadapter;
        protected IDataParameterCollection iParameterCollection;
        protected StringBuilder queryBuilder;
        protected int effected;
        protected DatabaseInfo DbInfo;
        protected bool useTransaction = false;
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        protected void ConnectDB()
        {            
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            if (DbInfo == null)
                DbInfo = new DatabaseInfo();

            //connection = new SqlConnection(StandardConstring
            //    .Replace("{0}", DBHostname)
            //    .Replace("{1}", DBCatalogname)
            //    .Replace("{2}", DBUsername)
            //    .Replace("{3}", DBPassword)
            //    );
            //connection = new SqlConnection(DbInfo.ConnectionString);

            //if (!useTransaction)
            //    if (connection == null)
            //connection = GetConnection(DbInfo);

            if (useTransaction)
            {
                if (connection == null || transaction == null)
                    connection = GetConnection(DbInfo);
            }else
                connection = GetConnection(DbInfo);

            // mod by jiggy 2009-05-11 09:41
            // reconnecting if state not open
            if (connection.State != ConnectionState.Open)
                connection.Open();

            if (useTransaction)
                if (transaction == null)
                    transaction = connection.BeginTransaction();
        }

        private IDbConnection GetConnection(DatabaseInfo DbInfo)
        {
            if (DbInfo.Vender == DBVender.SQLServer)
                return new SqlConnection(DbInfo.ConnectionString);
            //else if (DbInfo.Vender == DBVender.MySQL)
            //    return new MySqlConnection(DbInfo.ConnectionString);
            else
                throw new NotSupportedException("Database Vender " + DbInfo.Vender.ToString() + " not supported.");
        }

        protected void DisconnectDB()
        {
            try
            {
                if (!useTransaction || transaction == null)
                {
                    if (connection.State != ConnectionState.Closed) this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                //log.Debug(ex.Message);
                Console.Write(ex.Message);
            }
        }

        public IDbTransaction GetTransaction()
        {
            useTransaction = true;
            ConnectDB();
            return transaction;
        }

        public ReturnValue CommitTransaction()
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                transaction.Commit();
                transaction = null;
                useTransaction = false;
                DisconnectDB();
                retVal.Value = true;
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        public ReturnValue RollbackTransaction()
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                transaction.Rollback();
                transaction = null;
                useTransaction = false;
                DisconnectDB();
                retVal.Value = true;
            }
            catch (Exception exc) { retVal.Value = false; retVal.Exception = exc; }
            return retVal;
        }

        protected IDbCommand GetCommand(string StringQuery)
        {
            if (DbInfo.Vender == DBVender.SQLServer)
            {
                if (useTransaction && transaction != null)
                    return new SqlCommand(StringQuery, (SqlConnection)connection, (SqlTransaction)transaction);
                else
                    return new SqlCommand(StringQuery, (SqlConnection)connection);
            }
            //else if (DbInfo.Vender == DBVender.MySQL)
            //{
            //    if (useTransaction && transaction != null)
            //        return new MySqlCommand(StringQuery, (MySqlConnection)connection, (MySqlTransaction)transaction);
            //    else
            //        return new MySqlCommand(StringQuery, (MySqlConnection)connection);
            //}
            else
                throw new NotSupportedException("Database Vender " + DbInfo.Vender.ToString() + " not supported.");
        }

        protected IDbCommand GetCommand(string StringQuery, List<IParameter> iParameter)
        {
            if (DbInfo.Vender == DBVender.SQLServer)
            {
                SqlCommand sQLCommand;
                if (useTransaction && transaction != null)
                    sQLCommand = new SqlCommand(StringQuery, (SqlConnection)connection, (SqlTransaction)transaction);
                else
                    sQLCommand = new SqlCommand(StringQuery, (SqlConnection)connection);

                SetParameter4MSSQL(ref sQLCommand, iParameter);
                return sQLCommand;
            }
            //else if (DbInfo.Vender == DBVender.MySQL)
            //{
            //    MySqlCommand sQLCommand;
            //    if (useTransaction && transaction != null)
            //        sQLCommand = new MySqlCommand(StringQuery, (MySqlConnection)connection, (MySqlTransaction)transaction);
            //    else
            //        sQLCommand = new MySqlCommand(StringQuery, (MySqlConnection)connection);
            //    SetParameter4MySQL(ref sQLCommand, iParameter);
            //    return sQLCommand;
            //}
            else
                throw new NotSupportedException("Database Vender " + DbInfo.Vender.ToString() + " not supported.");
        }

        protected IDataReader GetExecuteReader(System.Data.IDbCommand command)
        {
            return command.ExecuteReader();
            //if (DbInfo.Vender == DBVender.SQLServer)
            //    return command.ExecuteReader();
            //else if (DbInfo.Vender == DBVender.MySQL)
            //    return command.ExecuteReader();
            //else
            //    throw new NotSupportedException();
        }

        protected int GetExecuteScalar(System.Data.IDbCommand command)
        {
            return (Int32)command.ExecuteScalar();
            //if (DbInfo.Vender == DBVender.SQLServer)
            //    return command.ExecuteReader();
            //else if (DbInfo.Vender == DBVender.MySQL)
            //    return command.ExecuteReader();
            //else
            //    throw new NotSupportedException();
        }
        

        protected int GetExecuteNonQuery(System.Data.IDbCommand command)
        {
            return command.ExecuteNonQuery();
            //if (DbInfo.Vender == DBVender.SQLServer)
            //    return command.ExecuteNonQuery();
            //else if (DbInfo.Vender == DBVender.MySQL)
            //    return command.ExecuteNonQuery();
            //else
            //    throw new NotSupportedException();
        }

        protected void SetQueryAdapter(System.Data.IDbCommand command)
        {
            if (DbInfo.Vender == DBVender.SQLServer)
            {
                queryadapter = new SqlDataAdapter();
                ((SqlDataAdapter)queryadapter).SelectCommand = command as SqlCommand;
            }
            //else if (DbInfo.Vender == DBVender.MySQL)
            //{
            //    queryadapter = new MySqlDataAdapter();
            //    ((MySqlDataAdapter)queryadapter).SelectCommand = command as MySqlCommand;
            //}
            else
                throw new NotSupportedException();
            //throw new NotImplementedException();
        }

        private void SetParameter4MSSQL(ref SqlCommand sQLCommand, List<IParameter> iParameter)
        {
            if (DbInfo.Vender == DBVender.SQLServer)
            {
                foreach (IParameter iparam in iParameter)
                    sQLCommand.Parameters.Add("@" + iparam.ParamName, GetSqlDbType(iparam.IDbType)).Value = iparam.Value;
                
            }else
                throw new NotSupportedException("Database Vender " + DbInfo.Vender.ToString() + " not supported.");
        }

        //private void SetParameter4MySQL(ref MySqlCommand sQLCommand, List<IParameter> iParameter)
        //{
        //    if (DbInfo.Vender == DBVender.MySQL)
        //        foreach (IParameter iparam in iParameter)
        //            sQLCommand.Parameters.Add("@" + iparam.ParamName, GetMySqlDbType(iparam.IDbType)).Value = iparam.Value;
        //}

        private SqlDbType GetSqlDbType(IDbType iDbType)
        {
            return (SqlDbType)iDbType;
        }

        //private MySqlDbType GetMySqlDbType(IDbType iDbType)
        //{
        //    MySqlDbType mysqldbtype = MySqlDbType.VarChar;
        //    switch (iDbType)
        //    {
        //        case IDbType.BigInt: mysqldbtype = MySqlDbType.Int64; break;
        //        case IDbType.Binary: mysqldbtype = MySqlDbType.Binary; break;
        //        case IDbType.Bit: mysqldbtype = MySqlDbType.Bit; break;
        //        case IDbType.Char: mysqldbtype = MySqlDbType.String; break;
        //        case IDbType.Date: mysqldbtype = MySqlDbType.Date; break;
        //        case IDbType.DateTime: mysqldbtype = MySqlDbType.DateTime; break;
        //        case IDbType.DateTime2: mysqldbtype = MySqlDbType.DateTime; break;
        //        case IDbType.DateTimeOffset: mysqldbtype = MySqlDbType.DateTime; break;
        //        case IDbType.Decimal: mysqldbtype = MySqlDbType.Decimal; break;
        //        case IDbType.Float: mysqldbtype = MySqlDbType.Float; break;
        //        case IDbType.Image: mysqldbtype = MySqlDbType.Blob; break;
        //        case IDbType.Int: mysqldbtype = MySqlDbType.Int32; break;
        //        case IDbType.Money: mysqldbtype = MySqlDbType.Decimal; break;
        //        case IDbType.NChar: mysqldbtype = MySqlDbType.String; break;
        //        case IDbType.NText: mysqldbtype = MySqlDbType.Text; break;
        //        case IDbType.NVarChar: mysqldbtype = MySqlDbType.VarString; break;
        //        case IDbType.Real: mysqldbtype = MySqlDbType.Double; break;
        //        case IDbType.SmallDateTime: mysqldbtype = MySqlDbType.DateTime; break;
        //        case IDbType.SmallInt: mysqldbtype = MySqlDbType.Int32; break;
        //        case IDbType.SmallMoney: mysqldbtype = MySqlDbType.Decimal; break;
        //        //case IDbType.Structured:mysqldbtype = MySqlDbType.
        //        case IDbType.Text: mysqldbtype = MySqlDbType.Text; break;
        //        case IDbType.Time: mysqldbtype = MySqlDbType.Time; break;
        //        case IDbType.Timestamp: mysqldbtype = MySqlDbType.Timestamp; break;
        //        case IDbType.TinyInt: mysqldbtype = MySqlDbType.Int32; break;
        //        //case IDbType.Udt:
        //        case IDbType.UniqueIdentifier: mysqldbtype = MySqlDbType.Guid; break;
        //        case IDbType.VarBinary: mysqldbtype = MySqlDbType.VarBinary; break;
        //        case IDbType.VarChar: mysqldbtype = MySqlDbType.VarChar; break;
        //        case IDbType.Variant: mysqldbtype = MySqlDbType.VarString; break;
        //        case IDbType.Xml: mysqldbtype = MySqlDbType.Blob; break;

        //        default: throw new NotSupportedException("DbType " + iDbType.ToString() + " not support for " + DbInfo.Vender.ToString());
        //    }

        //    return mysqldbtype;

        //    //return (MySqlDbType)iDbType;
        //}
    }
}