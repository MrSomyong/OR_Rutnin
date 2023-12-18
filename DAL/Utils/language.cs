using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class language
    {
        public static string LangEN(string message)
        {
            try 
            {
                string[] armessage = message.Split('|');
                return armessage[1];
            }
            catch
            {
                return message;
            }            
        }
        public static string LangTH(string message)
        {
            try
            {
                string[] armessage = message.Split('|');
                return armessage[0];
            }
            catch
            {
                return message;
            }
        }
    }
}
