using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    public class Nummeric
    {
        //public static bool IsNummeric(object obj)
        //{
        //    if (obj == null || obj == "")
        //        return false;
        //    else
        //    {
        //        try
        //        {
        //            decimal.Parse(obj.ToString());
        //            return true;
        //        }
        //        catch { return false; }
        //        return false;
        //    }
        //}

        /// <summary>
        /// Validate Number 123456
        /// </summary>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateNum(char key)
        {
            if (!(char.IsControl(key) || char.IsDigit(key)))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Validate Nummeric 123123
        /// </summary>
        /// <param name="content">content to check</param>
        /// <param name="lenght">lenght to check</param>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateNum(string content, int lenght, char key)
        {
            if ((int)key == 8) return false;
            else if (content.Length == lenght) return true;

            //if (key.ToString().Equals(":")) return false;
            if (!(char.IsControl(key) || char.IsDigit(key)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Validate Double  123.123
        /// </summary>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateDouble(char key)
        {
            if (key.ToString() == ".")
                return false;
            if (!(char.IsControl(key) || char.IsDigit(key)))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Validate Time 00:00:00
        /// </summary>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateTime(char key)
        {
            if (key.ToString().Equals(":"))
                return false;
            if (!(char.IsControl(key) || char.IsDigit(key)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Validate Time 00:00:00
        /// </summary>
        /// <param name="key">char key validate</param>
        /// <param name="content">string to validate</param>
        /// <param name="lenght">lenght to limite</param>
        /// <returns></returns>
        public static bool ValidateTime(char key,string content,int lenght)
        {
            if ((int)key == 8) return false;
            else if (content.Length == lenght) return true;

            if (key.ToString().Equals(":")) return false;
            if (!(char.IsControl(key) || char.IsDigit(key)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Validate Doblue 123.123
        /// </summary>
        /// <param name="textvalidate">text check</param>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateDouble(string textvalidate, char key)
        {
            if (textvalidate.Split('.').Length > 1 && key.ToString().Equals("."))
                return true;
            if (key.ToString() == ".")
                return false;
            if (!(char.IsControl(key) || char.IsDigit(key)))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Validate Normal
        /// </summary>
        /// <param name="key">char key validate</param>
        /// <returns></returns>
        public static bool ValidateNormal(char key)
        {
            Regex reg = new Regex("[^']");
            if (key.ToString() == " ")
                return false;
            else if (!(char.IsLower(key) || char.IsUpper(key) || char.IsDigit(key)))
            {
                if (key == '-')
                    return false;
                else
                    return true;
            }
            else if (key == '"' || !reg.IsMatch(key.ToString()))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Validate Int64
        /// </summary>
        /// <param name="SearchText">string to validate</param>
        /// <returns></returns>
        public static bool ValidateNummeric(string SearchText)
        {
            try
            {
                Int64.Parse(SearchText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateMoney(string SearchText)
        {
            try
            {
                Double.Parse(SearchText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateFloat(string SearchText)
        {
            try
            {
                float.Parse(SearchText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string FormatMoney(float money)
        {
            try
            {
                return money.ToString("#,##0.00");
            }
            catch
            {
                return money.ToString();
            }
        }

        public static int Str2Int(string _value)
        {
            int value = 0;
            try
            {
                if (!string.IsNullOrEmpty(_value))
                {
                    value = int.Parse(_value);
                }
                return value;
            }
            catch
            {
                return value;
            }
            
        }

        public static float Str2Float(string _value)
        {
            float value = 0;
            try
            {
                if (!string.IsNullOrEmpty(_value))
                {
                    value = float.Parse(_value);
                }
                return value;
            }
            catch
            {
                return value;
            }
        }
    }
}