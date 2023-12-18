using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StringFormat
    {
        public static string NameKey(string _str)
        {
            return _str.Replace(" ",string.Empty).ToUpper();
        }

        public static string Bool2String(bool _bool)
        {
            if (_bool)
                return "1";
            else
                return "0";
        }
    }
}
