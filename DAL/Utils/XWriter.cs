using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DAL
{
    public class XWriter
    {
        public static void WriteDebug(object message)
        {
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "debug.txt", true);
            sw.WriteLine(message.ToString());
            sw.Flush();
            sw.Close();
        }
    }
}