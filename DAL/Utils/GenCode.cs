using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class GenCode
    {
        public static string BookingCode(string running)
        {
            string bookcode = string.Empty;
            int intrunning = 0;
            string strrunning = "";
            try
            {
                if (string.IsNullOrEmpty(running))
                    strrunning = "00000";
                else
                    strrunning = running.Substring(5);
                intrunning = int.Parse(strrunning);
                intrunning = intrunning + 1;
                strrunning = intrunning.ToString().PadLeft(5, '0');
                bookcode = "BA" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + strrunning;
            }
            catch
            {
                
            }
            return bookcode;
        }
    }
}
