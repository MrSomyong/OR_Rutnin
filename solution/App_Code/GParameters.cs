using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using DAL;
using System.Configuration;

/// <summary>
/// Summary description for GParameters
/// </summary>
public class GParameters
{
    public GParameters()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static DatabaseInfo _dbfo;

    /// <summary>
    /// Get or Set Database Info for iLink
    /// </summary>
    public static DatabaseInfo dbInfo
    {
        get
        {
            _dbfo = Initializing(Conn);
            return _dbfo;
        }
        //set { _dbStationaryInfo = value; }
    }

    private static DatabaseInfo Initializing(string connectionString)
    {
        DatabaseInfo dbInfo = null;
        string[] Conntex = connectionString.Split(';');
        string Hostname = string.Empty, Catalog = string.Empty, User = string.Empty, Password = string.Empty;
        if (Conntex.Length > 0)
        {
            foreach (string iString in Conntex)
            {
                if (iString.Contains("Data Source"))
                {
                    string[] temp = iString.Split('=');
                    Hostname = temp[1];
                }
                else if (iString.Contains("Catalog"))
                {
                    string[] temp = iString.Split('=');
                    Catalog = temp[1];
                }
                else if (iString.Contains("User"))
                {
                    string[] temp = iString.Split('=');
                    User = temp[1];
                }
                else if (iString.Contains("Password"))
                {
                    string[] temp = iString.Split('=');
                    Password = temp[1];
                }
            }

            dbInfo = new DatabaseInfo(Hostname, Catalog, User, Password, DBVender.SQLServer);
        }
        return dbInfo;
    }

    //private static string GetDomainName()
    //{
    //    return domainname;
    //}

    private static string Conn = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
    //private static string domainname = WebConfigurationManager.AppSettings["AppADPath"].ToString();


    //get current connection name 
    private static string instantConnectionName = GetAppSettingsValueByKey("InstantAppConnection");
    private static ConnectionStringSettings connectionName = ConfigurationManager.ConnectionStrings[instantConnectionName];

    //get external connection name 
    private static string externalConnectionName = GetAppSettingsValueByKey("ExternalAppConnection");
    private static ConnectionStringSettings extConnectionName = ConfigurationManager.ConnectionStrings[externalConnectionName];

    public static string InitAppConnection
    {
        get
        {
            return connectionName.ConnectionString;
        }
    }
    public static string InitExtConnection
    {
        get
        {
            return extConnectionName.ConnectionString;
        }
    }

    public static DatabaseInfo AppConnDBInfo
    {
        get
        {
            _dbfo = Initializing(InitAppConnection);
            return _dbfo;
        }
        //set { _dbStationaryInfo = value; }
    }

    public static DatabaseInfo ExtConnDBInfo
    {
        get
        {
            _dbfo = Initializing(InitExtConnection);
            return _dbfo;
        }
        //set { _dbStationaryInfo = value; }
    }


    public static string GetAppSettingsValueByKey(string sKey)
    {
        try
        {
            if (string.IsNullOrEmpty(sKey))
                throw new ArgumentNullException("sKey", "The AppSettings key name can't be Null or Empty.");

            if (ConfigurationManager.AppSettings[sKey] == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find the AppSettings Key named '{0}' in app/web.config.", sKey));

            return ConfigurationManager.AppSettings[sKey].ToString();
        }
        catch (Exception ex)
        {
            //bubble error.
            throw new Exception("ConfigurationHandler::GetAppSettingsValueByKey:Error occured.", ex);
        }
    }
}