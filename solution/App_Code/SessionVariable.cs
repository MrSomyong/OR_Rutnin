using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Linq.Expressions;
using System.Data.OleDb;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Web_Admin
{    
    public static class SessionVariable
    {
        private static string x_UserID = string.Empty;
        private static string x_UserFullName = string.Empty;
        private static DateTime x_EntryDateTime = new DateTime();
        private static Enum.UserType x_UserType = 0;
        private static string x_UserSetting = string.Empty;

        public static string uploadFilename;

        public static string UserID
        {
            get
            {
                if (HttpContext.Current.Session["x_UserID"] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session["x_UserID"].ToString(); }
            }
            set
            { HttpContext.Current.Session["x_UserID"] = value; }
        }
        public static string UserFullName
        {
            get
            {
                if (HttpContext.Current.Session["x_UserFullName"] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session["x_UserFullName"].ToString(); }
            }
            set
            { HttpContext.Current.Session["x_UserFullName"] = value; }
        }
        public static DateTime EntryDateTime
        {
            get
            {
                if (HttpContext.Current.Session["x_EntryDateTime"] == null)
                { return DateTime.MinValue; }
                else
                { return (DateTime)HttpContext.Current.Session["x_EntryDateTime"]; }
            }
            set
            { HttpContext.Current.Session["x_EntryDateTime"] = value; }
        }
        public static Enum.UserType UserType
        {
            get
            {
                if (HttpContext.Current.Session["x_UserType"] == null)
                { return Enum.UserType.None; }
                else
                { return (Enum.UserType)HttpContext.Current.Session["x_UserType"]; }
            }
            set
            { HttpContext.Current.Session["x_UserType"] = value; }
        }
        public static string UserSetting
        {
            get
            {
                if (HttpContext.Current.Session["x_UserSetting"] == null)
                { return string.Empty; }
                else
                { return HttpContext.Current.Session["x_UserSetting"].ToString(); }
            }
            set
            { HttpContext.Current.Session["x_UserSetting"] = value; }
        }

    }
    
}
