﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class UNIT
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameInfo
        {
            get
            {
                return string.Format("[{0}] {1}", Code, Name);
            }
        }
    }
}
