using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ORUtils
    {
        

        public static string getAge(DateTime? BirthDate)
        {
            int age = 0;
            string stage = string.Empty;
            
            try
            {
                age = (int)Math.Floor((DateTime.Now - BirthDate.Value).TotalDays / 365.25D);
                if (age < 7)
                {
                    TimeSpan TimeSpan = DateTime.Now.Subtract(BirthDate.Value);
                    decimal years, months, days;
                    months = 12 * (DateTime.Now.Year - BirthDate.Value.Year) + (DateTime.Now.Month - BirthDate.Value.Month);

                    if (DateTime.Now.Day < BirthDate.Value.Day)
                    {
                        months -= 1;

                        int x = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        days = x - BirthDate.Value.Day + DateTime.Now.Day - 1;
                    }
                    else
                    {
                        days = DateTime.Now.Day - BirthDate.Value.Day;
                    }
                    years = (int)Math.Floor(months / 12);
                    months -= years * 12;
                    stage = years.ToString() + "." + months.ToString();
                }              
                else
                    stage = age.ToString();
                
            }
            catch
            { }

            return stage;
        }

        public static string getGender(string _key)
        {
            string str = string.Empty;
            try
            {
                int key = int.Parse(_key);

                switch (key)
                {
                    case (int)EnumOR.Gender.Male:
                        str = EnumOR.Gender.Male.ToString();
                        break;
                    case (int)EnumOR.Gender.Female:
                        str = EnumOR.Gender.Female.ToString();
                        break;
                    case (int)EnumOR.Gender.Unspecify:
                        str = EnumOR.Gender.Unspecify.ToString();
                        break;
                    default:
                        str = EnumOR.Gender.None.ToString();
                        break;
                }

            }
            catch
            { }

            return str;
        }

        public static string getORSpecificType(string _key)
        {
            string str = string.Empty;
            try
            {
                int key = int.Parse(_key);

                switch (key)
                {
                    case (int)EnumOR.ORSpecificType.None:
                        str = EnumOR.ORSpecificType.None.ToString();
                        break;
                    case (int)EnumOR.ORSpecificType.Refer:
                        str = EnumOR.ORSpecificType.Refer.ToString();
                        break;
                    case (int)EnumOR.ORSpecificType.Specific:
                        str = EnumOR.ORSpecificType.Specific.ToString();
                        break;
                    default:
                        str = "";
                        break;
                }

            }
            catch
            { }

            return str;
        }

        public static string getORStatus(string _key)
        {
            string str = string.Empty;
            try
            {
                int key = int.Parse(_key);

                switch (key)
                {
                    case (int)EnumOR.ORStatus.IPD:
                        str = EnumOR.ORStatus.IPD.ToString();
                        break;
                    case (int)EnumOR.ORStatus.Observe:
                        str = EnumOR.ORStatus.Observe.ToString();
                        break;
                    case (int)EnumOR.ORStatus.OPD:
                        str = EnumOR.ORStatus.OPD.ToString();
                        break;
                    case (int)EnumOR.ORStatus.Reserve:
                        str = EnumOR.ORStatus.Reserve.ToString();
                        break;
                    default:
                        str = EnumOR.ORStatus.None.ToString();
                        break;
                }

            }
            catch
            { }

            return str;
        }

        public static string getAnesthesiaSign(string _key)
        {
            string str = string.Empty;
            try
            {
                int key = int.Parse(_key);

                switch (key)
                {
                    case 1:
                        str = "+";
                        break;
                    case 2:
                        str = "+-";
                        break;
                    default:
                        str = "";
                        break;
                }

            }
            catch
            { }

            return str;
        }

        public static string getSide(string _key)
        {
            string str = string.Empty;
            try
            {
                int key = int.Parse(_key);

                switch (key)
                {
                    case 1:
                        str = "+";
                        break;
                    case 2:
                        str = "+-";
                        break;
                    case 3:
                        str = "/";
                        break;
                    default:
                        str = "";
                        break;
                }

            }
            catch
            { }

            return str;
        }

    }
}
