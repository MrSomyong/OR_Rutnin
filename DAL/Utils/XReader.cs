using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DAL
{
    public class XReader
    {
        public static List<string> ReadLine(FileInfo fileReader)
        {
            List<string> retVal = new List<string>();
            FileStream fs = new FileStream(fileReader.FullName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string _strLine = sr.ReadLine();
            while (_strLine != null)
            {
                if (_strLine.Length > 0)
                    retVal.Add(_strLine.Trim());

                _strLine = sr.ReadLine();
            }
            sr.Close();
            fs.Close();

            return retVal;
        }
    }
}