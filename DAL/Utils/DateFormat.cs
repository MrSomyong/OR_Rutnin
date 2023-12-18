using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DateFormat
    {
        public static string dmy2ymd(string _str)
        {
            if (!string.IsNullOrEmpty(_str))
            { 
                string [] arstr = _str.Split('/');
                _str = arstr[2] + "/" + arstr[1] + "/" + arstr[0];
            }
            return _str;
        }

        public static string getDayTH (string _d)
        {
            string _day = string.Empty;
            if (_d.Substring(0,2) =="Su")
            {
                _day = "อาทิตย์";
            }
            else if (_d.Substring(0, 2) == "Mo")
            {
                _day = "จันทร์";
            }
            else if (_d.Substring(0, 2) == "Tu")
            {
                _day = "อังคาร";
            }
            else if (_d.Substring(0, 2) == "We")
            {
                _day = "พุธ";
            }
            else if (_d.Substring(0, 2) == "Th")
            {
                _day = "พฤหัสบดี";
            }
            else if (_d.Substring(0, 2) == "Fr")
            {
                _day = "ศุกร์";
            }
            else if (_d.Substring(0, 2) == "Sa")
            {
                _day = "เสาร์";
            }
            return _day;
        }

        public static string getMonthTH(int _m)
        {
            string _month = string.Empty;
            if (_m == 1)
            {
                _month = "มกราคม";
            }
            else if (_m == 2)
            {
                _month = "กุมภาพันธ์";
            }
            else if (_m == 3)
            {
                _month = "มีนาคม";
            }
            else if (_m == 4)
            {
                _month = "เมษายน";
            }
            else if (_m == 5)
            {
                _month = "พฤษภาคม";
            }
            else if (_m == 6)
            {
                _month = "มิถุนายน";
            }
            else if (_m == 7)
            {
                _month = "กรกฎาคม";
            }
            else if (_m == 8)
            {
                _month = "สิงหาคม";
            }
            else if (_m == 9)
            {
                _month = "กันยายน";
            }
            else if (_m == 10)
            {
                _month = "ตุลาคม";
            }
            else if (_m == 11)
            {
                _month = "พฤศจิกายน";
            }
            else if (_m == 12)
            {
                _month = "ธันวาคม";
            }
            return _month;
        }
    }
}
