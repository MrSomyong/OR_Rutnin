using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CultureInfo
    {
        public static DateTime GetDatetime(DateTime objDatetime, YearType yearType)
        {
            if (yearType == YearType.English)
            {
                int year = int.Parse(objDatetime.ToString("yyyy"));
                if (year > 2500)
                {
                    year = year - 543;
                    return new DateTime(year, objDatetime.Month, objDatetime.Day,
                        objDatetime.Hour, objDatetime.Minute, objDatetime.Second,
                        objDatetime.Millisecond);
                }
                else
                {
                    return objDatetime;
                }
            }
            else
            {
                int year = int.Parse(objDatetime.ToString("yyyy"));
                if (year < 2400)
                {
                    year = year + 543;
                    return new DateTime(year, objDatetime.Month, objDatetime.Day,
                        objDatetime.Hour, objDatetime.Minute, objDatetime.Second,
                        objDatetime.Millisecond);
                }
                else
                {
                    return objDatetime;
                }
            }
        }
        /// <summary>
        /// Get DateTime String with format yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="objDateTime">Datetime</param>
        /// <param name="iFormat">Format string</param>
        /// <param name="yearType">Year Type</param>
        /// <returns></returns>
        public static string GetDateTimeString(DateTime objDateTime, string iFormat, YearType yearType)
        {
            string retVal = string.Empty;
            int year, month, day;
            int hour, minute, second, milisecond;

            year = objDateTime.Year;
            month = objDateTime.Month;
            day = objDateTime.Day;

            hour = objDateTime.Hour;
            minute = objDateTime.Minute;
            second = objDateTime.Second;
            milisecond = objDateTime.Millisecond;

            if (yearType == YearType.English)
            {
                retVal = string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", year, month, day, hour, minute, second);
            }
            else
            {
                //int iYear = int.Parse(objDateTime.ToString("yyyy"));
                year += 543;
                retVal = string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", year, month, day, hour, minute, second);
            }

            return retVal;
        }
        /// <summary>
        /// Get DateTime String with format yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="objDateTime">Datetime</param>
        /// <param name="iFormat">Format string</param>
        /// <returns></returns>
        public static string GetDateTimeString(DateTime objDateTime, string iFormat)
        {
            return GetDateTimeString(objDateTime, iFormat, YearType.English);
        }

        public static string GetDateString(DateTime objDateTime, YearType yearType)
        {
            string retVal = string.Empty;
            int year, month, day;
            int hour, minute, second, milisecond;

            year = objDateTime.Year;
            month = objDateTime.Month;
            day = objDateTime.Day;

            hour = objDateTime.Hour;
            minute = objDateTime.Minute;
            second = objDateTime.Second;
            milisecond = objDateTime.Millisecond;

            if (yearType == YearType.English)
            {
                retVal = string.Format("{0:00}/{1:00}/{2:0000}", day, month, year);
            }
            else
            {
                //int iYear = int.Parse(objDateTime.ToString("yyyy"));
                year += 543;
                retVal = string.Format("{0:00}/{1:00}/{2:0000}", day, month, year);
            }

            return retVal;
        }
    }
}