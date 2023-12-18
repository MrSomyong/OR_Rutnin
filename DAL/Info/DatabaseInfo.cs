using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    /// <summary>
    /// A Class Database Infomation
    /// </summary>
    [Serializable]
    public class DatabaseInfo
    {
        public DatabaseInfo() { }
        public DatabaseInfo(string Hostname, string Catalog,
            string Username, string Password, DBVender Vender)
        {
            this._Hostname = Hostname;
            this._Catalog = Catalog;
            this._Username = Username;
            this._Password = Password;
            this._dbVender = Vender;

        }

        public DatabaseInfo(string Hostname, string Catalog,
            string Username, string Password, DBVender Vender
            , int Port)
        {
            this._Hostname = Hostname;
            this._Catalog = Catalog;
            this._Username = Username;
            this._Password = Password;
            this._dbVender = Vender;
            this._Port = Port;
        }

        private string _connectionString;
        /// <summary>
        /// Get or Set Connection String
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (_dbVender == DBVender.SQLServer)
                    InitialSQLConstring();
                else if (_dbVender == DBVender.MySQL)
                    InitialMySQLConstring();
                else
                    throw new NotSupportedException("Databse vender " + _dbVender.ToString() + " not supported.");
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        private string _Hostname = "(local)";
        /// <summary>
        /// Get or Set Hostname
        /// </summary>
        public string Hostname
        {
            get { return _Hostname; }
            set { _Hostname = value; }
        }
        private int _Port = 0;
        /// <summary>
        /// Get or Set Database Port for connection default for sql 1433
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        private string _Catalog = "Stationary";
        /// <summary>
        /// Get or Set Catalog
        /// </summary>
        public string Catalog
        {
            get { return _Catalog; }
            set { _Catalog = value; }
        }

        private string _Username = "sa";
        /// <summary>
        /// Get or Set Username
        /// </summary>
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        private string _Password = "manager";
        /// <summary>
        /// Get or Set Password
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// Get or Set Sql ConnectionType
        /// </summary>
        public SqlConnectionType ConnectionType
        {
            get { return _ConnectionType; }
            set { _ConnectionType = value; }
        }

        /// <summary>
        /// Database Vender
        /// </summary>
        public DBVender Vender
        {
            get { return _dbVender; }
            set { _dbVender = value; }
        }
        /// <summary>
        /// Get Database ConnectionString
        /// </summary>
        /// <returns>ConnectionString</returns>
        public override string ToString()
        {
            //return base.ToString();
            if (_dbVender == DBVender.SQLServer)
                InitialSQLConstring();
            else if (_dbVender == DBVender.MySQL)
                InitialMySQLConstring();
            else
                throw new NotSupportedException("Databse vender " + _dbVender.ToString() + " not supported.");
            return _connectionString;
        }

        private void InitialMySQLConstring()
        {
            StringBuilder sbConString = new StringBuilder();
            if (_Port == 0)
            {
                sbConString.Append(StandardMySQLConstring);
                sbConString.Replace("{0}", _Hostname)
                       .Replace("{1}", _Catalog)
                       .Replace("{2}", _Username)
                       .Replace("{3}", _Password);
            }
            else
            {
                sbConString.Append(SpecifyMySQLConstring);
                sbConString.Replace("{0}", _Hostname)
                       .Replace("{1}", _Catalog)
                       .Replace("{2}", _Username)
                       .Replace("{3}", _Password)
                       .Replace("{4}", _Port.ToString());
            }
            _connectionString = sbConString.ToString();
        }

        private void InitialSQLConstring()
        {
            StringBuilder sbConString = new StringBuilder();
            if (_ConnectionType == SqlConnectionType.Standard)
            {
                sbConString.Append(StandardSQLConstring);
                sbConString.Replace("{0}", _Hostname)
                       .Replace("{1}", _Catalog)
                       .Replace("{2}", _Username)
                       .Replace("{3}", _Password);
            }
            else if (_ConnectionType == SqlConnectionType.Trusted)
            {
                sbConString.Append(TrustedSQLConstring);
                sbConString.Replace("{0}", _Hostname)
                       .Replace("{1}", _Catalog);
            }

            _connectionString = sbConString.ToString();
        }

        DBVender _dbVender = DBVender.SQLServer;
        SqlConnectionType _ConnectionType = SqlConnectionType.Standard;
        string StandardSQLConstring = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};";
        string TrustedSQLConstring = "Data Source={0};Initial Catalog={1};Integrated Security=SSPI;";
        string StandardMySQLConstring = "Server={0};Database={1};Uid={2};Pwd={3};";
        //string StandardMySQLConstring = "Driver={MySQL ODBC 5.1 Driver};Server={0};Database={1};User={2}; Password={3};Option=3;";
        string SpecifyMySQLConstring = "Server={0};Database={1};Uid={2};Pwd={3};Port={4};";
        //string SpecifyMySQLConstring = "Driver={MySQL ODBC 5.1 Driver};Server={0};Port={4};Database={1};User={2}; Password={3};Option=3;";
    }
}