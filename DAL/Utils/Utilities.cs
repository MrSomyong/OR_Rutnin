using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DAL
{
    public class Utilities
    {

        public static string GetCurrentPath()
        {
            FileInfo path = new FileInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location
                );
            return path.DirectoryName;
        }

        public static string SetEndBackslash(string path)
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Substring(path.Length - 1, 1).Equals(@"\"))
                {
                    retVal = path + Path.DirectorySeparatorChar;
                }
            }
            else
                throw new NullOrEmptyException("path is null or empty.");

            return retVal;
        }

        public static string RemoveNewline(string value)
        {
            if (value.Length > 0)
                return value.Replace("\r\n", "");
            else
                return value;
        }
        public static string ConvertYearEN(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                int y = int.Parse(value);
                if (y > 2500)
                {
                    value = (y - 543).ToString();
                }                
            }
            return value;
        }
        public static string ConvertYearTH(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int y = int.Parse(value);
                if (y < 2500)
                {
                    value = (y + 543).ToString();
                }
            }
            return value;
        }
        public static string ConvertYMD(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] date = value.Split('/');
                    value = date[2] + "/" + date[1] + "/" + date[0];
                }
            }
            catch { }
            return value;
        }
        public static bool ValidateEmail(string value)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);
            if (match.Success)
                return true;
            else
                return false;

        }
        public static bool ChkDateDMY(string value)
        {
            Regex regex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
            Match match = regex.Match(value);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}